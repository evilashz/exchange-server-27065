using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x0200000E RID: 14
	internal class MdbDocumentAdapter : Disposable, IPersistableDocumentAdapter, IDocumentAdapter
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000355C File Offset: 0x0000175C
		internal MdbDocumentAdapter(MdbCompositeItemIdentity id, PropertyDefinition[] propertiesToLoad, MailboxSession session, MdbPropertyMap propertyMap) : this(id, propertiesToLoad, null, session, propertyMap, true)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000356B File Offset: 0x0000176B
		internal MdbDocumentAdapter(MdbCompositeItemIdentity id, PropertyDefinition[] propertiesToLoad, IItem item, MdbPropertyMap propertyMap) : this(id, propertiesToLoad, item, null, propertyMap, true)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000357C File Offset: 0x0000177C
		internal MdbDocumentAdapter(MdbCompositeItemIdentity id, PropertyDefinition[] propertiesToLoad, IItem item, MailboxSession session, MdbPropertyMap propertyMap, bool allowItemBind = true)
		{
			Util.ThrowOnNullArgument(id, "id");
			Util.ThrowOnNullArgument(propertyMap, "propertyMap");
			this.id = id;
			this.propertyMap = propertyMap;
			this.mappedPropertiesToLoadOnBind = MdbDocumentAdapter.GetMappings(this.propertyMap, propertiesToLoad);
			ExAssert.RetailAssert(this.mappedPropertiesToLoadOnBind != null, "Store Properties to load is null");
			this.Item = item;
			this.Session = session;
			if (this.Item == null && this.Session == null)
			{
				throw new ArgumentException("session and item are both null");
			}
			this.shouldDisposeItem = false;
			this.allowItemBind = allowItemBind;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("MdbDocumentAdapter", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.MdbDocumentAdapterTracer, (long)this.GetHashCode());
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000363C File Offset: 0x0000183C
		internal MdbDocumentAdapter(IDictionary<StorePropertyDefinition, object> preloadedProperties, MdbPropertyMap propertyMap)
		{
			Util.ThrowOnNullOrEmptyArgument<KeyValuePair<StorePropertyDefinition, object>>(preloadedProperties, "preloadedProperties");
			Util.ThrowOnNullArgument(propertyMap, "propertyMap");
			this.preloadedProperties = preloadedProperties;
			this.propertyMap = propertyMap;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("MdbDocumentAdapter", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.MdbDocumentAdapterTracer, (long)this.GetHashCode());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003699 File Offset: 0x00001899
		public bool ContainsPropertyMapping(PropertyDefinition property)
		{
			Util.ThrowOnNullArgument(property, "property");
			return this.propertyMap.ContainsKey(property);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000036B4 File Offset: 0x000018B4
		public bool TryGetProperty(PropertyDefinition property, out object result)
		{
			object property2 = this.GetProperty(property);
			result = property2;
			return property2 != null;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036D4 File Offset: 0x000018D4
		public object GetProperty(PropertyDefinition property)
		{
			Util.ThrowOnNullArgument(property, "property");
			MdbPropertyMapping mdbMapping;
			if (this.propertyMap.TryGetValue(property, out mdbMapping))
			{
				return this.InternalGetProperty(mdbMapping);
			}
			throw new OperationFailedException(Strings.PropertyMappingFailed(property.ToString()));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003714 File Offset: 0x00001914
		public void SetProperty(PropertyDefinition property, object value)
		{
			Util.ThrowOnNullArgument(property, "property");
			Util.ThrowOnNullArgument(value, "value");
			MdbPropertyMapping mdbPropertyMapping;
			if (!this.propertyMap.TryGetValue(property, out mdbPropertyMapping))
			{
				throw new OperationFailedException(Strings.PropertyMappingFailed(property.ToString()));
			}
			if (mdbPropertyMapping.IsReadOnly)
			{
				throw new OperationFailedException(Strings.SetPropertyFailed(property.ToString()));
			}
			this.InternalSetProperty(mdbPropertyMapping, value);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003779 File Offset: 0x00001979
		public virtual void Save()
		{
			this.Save(true);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000037C4 File Offset: 0x000019C4
		public virtual void Save(bool reload)
		{
			if (this.Item == null)
			{
				throw new OperationFailedException(Strings.SaveWithNoItemError);
			}
			if (this.Item.IsDirty)
			{
				MdbDocumentAdapter.CallXsoAndMapExceptions(this.diagnosticsSession, this.id.MailboxGuid, delegate
				{
					this.Item.Save(SaveMode.ResolveConflicts);
					if (reload)
					{
						this.Item.Load(this.mappedPropertiesToLoadOnBind);
					}
				});
				return;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003830 File Offset: 0x00001A30
		internal static IList<PropertyDefinition> GetMappings(MdbPropertyMap map, PropertyDefinition[] propertiesToLoad)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			if (propertiesToLoad != null)
			{
				list = new List<PropertyDefinition>(propertiesToLoad.Length);
				foreach (PropertyDefinition propertyDefinition in propertiesToLoad)
				{
					MdbPropertyMapping mdbPropertyMapping;
					if (!map.TryGetValue(propertyDefinition, out mdbPropertyMapping))
					{
						throw new KeyNotFoundException(string.Format("Could not get mapping for {0}.", propertyDefinition));
					}
					list.AddRange(mdbPropertyMapping.StorePropertyDefinitions);
				}
			}
			return list;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003894 File Offset: 0x00001A94
		internal static object CheckPropertyValue(object propertyValue)
		{
			PropertyError propertyError = propertyValue as PropertyError;
			if (propertyError == null)
			{
				return propertyValue;
			}
			if (propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
			{
				return null;
			}
			throw PropertyError.ToException(new PropertyError[]
			{
				propertyError
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000038C9 File Offset: 0x00001AC9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MdbDocumentAdapter>(this);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000038D1 File Offset: 0x00001AD1
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.Item != null)
			{
				if (this.shouldDisposeItem)
				{
					this.Item.Dispose();
				}
				this.Item = null;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003914 File Offset: 0x00001B14
		private static TReturnValue CallXsoAndMapExceptionsWithReturnValue<TReturnValue>(IDiagnosticsSession tracer, Guid mailboxGuid, MdbDocumentAdapter.CallXsoWithReturnValue<TReturnValue> xsoCall)
		{
			TReturnValue result = default(TReturnValue);
			MdbDocumentAdapter.CallXsoAndMapExceptions(tracer, mailboxGuid, delegate
			{
				result = xsoCall();
			});
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003954 File Offset: 0x00001B54
		private static void CallXsoAndMapExceptions(IDiagnosticsSession tracer, Guid mailboxGuid, MdbDocumentAdapter.CallXso xsoCall)
		{
			try
			{
				xsoCall();
			}
			catch (ConnectionFailedTransientException ex)
			{
				tracer.TraceError<Guid, ConnectionFailedTransientException>("Failed to connect to mailbox {0}, exception: {1}", mailboxGuid, ex);
				throw new ComponentFailedTransientException(Strings.ConnectionToMailboxFailed(mailboxGuid), ex);
			}
			catch (ConnectionFailedPermanentException ex2)
			{
				tracer.TraceError<Guid, ConnectionFailedPermanentException>("Failed to connect to mailbox {0}, exception: {1}", mailboxGuid, ex2);
				throw new ComponentFailedPermanentException(Strings.ConnectionToMailboxFailed(mailboxGuid), ex2);
			}
			catch (ObjectNotFoundException ex3)
			{
				tracer.TraceDebug<Guid, ObjectNotFoundException>("Got exception from XSO (MDB: {0}): {1}", mailboxGuid, ex3);
				throw new DocumentFailureException(ex3);
			}
			catch (CorruptDataException ex4)
			{
				tracer.TraceDebug<Guid, CorruptDataException>("Got exception from XSO (MDB: {0}): {1}", mailboxGuid, ex4);
				throw new DocumentFailureException(ex4);
			}
			catch (PropertyErrorException ex5)
			{
				tracer.TraceDebug<Guid, PropertyErrorException>("Got exception from XSO (MDB: {0}): {1}", mailboxGuid, ex5);
				throw new DocumentFailureException(ex5);
			}
			catch (MailboxUnavailableException ex6)
			{
				tracer.TraceDebug<Guid, MailboxUnavailableException>("Got exception from XSO (MDB: {0}): {1}", mailboxGuid, ex6);
				throw new DocumentFailureException(ex6);
			}
			catch (StoragePermanentException innerException)
			{
				throw new ComponentFailedPermanentException(innerException);
			}
			catch (StorageTransientException innerException2)
			{
				throw new ComponentFailedTransientException(innerException2);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003ABC File Offset: 0x00001CBC
		protected virtual object InternalGetProperty(MdbPropertyMapping mdbMapping)
		{
			object result = null;
			if (this.allowItemBind)
			{
				this.InitializeItemIfNecessary();
				result = MdbDocumentAdapter.CallXsoAndMapExceptionsWithReturnValue<object>(this.diagnosticsSession, this.id.MailboxGuid, () => MdbDocumentAdapter.CheckPropertyValue(mdbMapping.GetPropertyValue(this.Item, new MdbDocumentAdapter.MdbDocAdapterPropertyContext
				{
					MailboxSession = this.Session
				})));
			}
			else if (this.AllPropertiesContained(mdbMapping.StorePropertyDefinitions))
			{
				return MdbDocumentAdapter.CheckPropertyValue(mdbMapping.GetPropertyValue(this.preloadedProperties));
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B44 File Offset: 0x00001D44
		private bool AllPropertiesContained(IEnumerable<StorePropertyDefinition> providerPropertyDefinitions)
		{
			foreach (StorePropertyDefinition key in providerPropertyDefinitions)
			{
				if (!this.preloadedProperties.ContainsKey(key))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual void InternalSetProperty(MdbPropertyMapping mdbMapping, object value)
		{
			if (this.allowItemBind)
			{
				this.InitializeItemIfNecessary();
				MdbDocumentAdapter.CallXsoAndMapExceptions(this.diagnosticsSession, this.id.MailboxGuid, delegate
				{
					mdbMapping.SetPropertyValue(this.Item, value, new MdbDocumentAdapter.MdbDocAdapterPropertyContext
					{
						MailboxSession = this.Session
					});
				});
				if (this.mappedPropertiesToLoadOnBind != null)
				{
					foreach (StorePropertyDefinition item in mdbMapping.StorePropertyDefinitions)
					{
						if (!this.mappedPropertiesToLoadOnBind.Contains(item))
						{
							this.mappedPropertiesToLoadOnBind.Add(item);
						}
					}
					return;
				}
			}
			else
			{
				mdbMapping.SetPropertyValue(this.preloadedProperties, value);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003D00 File Offset: 0x00001F00
		protected virtual IItem GetItem()
		{
			IItem item = null;
			MdbDocumentAdapter.CallXsoAndMapExceptions(this.diagnosticsSession, this.id.MailboxGuid, delegate
			{
				HashSet<PropertyDefinition> propsToReturn = new HashSet<PropertyDefinition>(this.mappedPropertiesToLoadOnBind);
				item = Microsoft.Exchange.Data.Storage.Item.Bind(this.Session, this.id.ItemId, ItemBindOption.None, propsToReturn);
				item.OpenAsReadWrite();
			});
			return item;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003D49 File Offset: 0x00001F49
		private void InitializeItemIfNecessary()
		{
			if (this.Item == null)
			{
				this.Item = this.GetItem();
				ExAssert.RetailAssert(this.Item != null, "Item is null");
				this.shouldDisposeItem = true;
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000037 RID: 55
		private readonly bool allowItemBind;

		// Token: 0x04000038 RID: 56
		private readonly MdbPropertyMap propertyMap;

		// Token: 0x04000039 RID: 57
		protected IItem Item;

		// Token: 0x0400003A RID: 58
		private readonly MdbCompositeItemIdentity id;

		// Token: 0x0400003B RID: 59
		protected MailboxSession Session;

		// Token: 0x0400003C RID: 60
		private bool shouldDisposeItem;

		// Token: 0x0400003D RID: 61
		private readonly IList<PropertyDefinition> mappedPropertiesToLoadOnBind;

		// Token: 0x0400003E RID: 62
		private readonly IDictionary<StorePropertyDefinition, object> preloadedProperties;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x06000056 RID: 86
		internal delegate void CallXso();

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x0600005A RID: 90
		internal delegate TReturnValue CallXsoWithReturnValue<TReturnValue>();

		// Token: 0x02000011 RID: 17
		private class MdbDocAdapterPropertyContext : IMdbPropertyMappingContext
		{
			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00003D7C File Offset: 0x00001F7C
			// (set) Token: 0x0600005E RID: 94 RVA: 0x00003D84 File Offset: 0x00001F84
			public MailboxSession MailboxSession { get; internal set; }
		}
	}
}
