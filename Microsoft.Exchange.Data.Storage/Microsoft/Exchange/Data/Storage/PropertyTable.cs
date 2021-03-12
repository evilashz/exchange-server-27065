using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PropertyTable : DisposableObject, IModifyTable, IDisposable
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00038B10 File Offset: 0x00036D10
		internal PropertyTable(CoreFolder coreFolder, NativeStorePropertyDefinition property, ModifyTableOptions options, IModifyTableRestriction modifyTableRestriction)
		{
			Util.ThrowOnNullArgument(coreFolder, "coreFolder");
			Util.ThrowOnNullArgument(property, "property");
			EnumValidator.ThrowIfInvalid<ModifyTableOptions>(options);
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.mapiModifyTable = PropertyTable.GetMapiModifyTable(coreFolder, property);
				this.session = coreFolder.Session;
				this.propertyReference = this.session.Mailbox.MapiStore;
				this.tableNameForTracing = property.Name;
				this.options = options;
				this.modifyTableRestriction = modifyTableRestriction;
				disposeGuard.Success();
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00038BC4 File Offset: 0x00036DC4
		public void Clear()
		{
			this.CheckDisposed(null);
			this.replaceAllRows = true;
			this.pendingOperations.Clear();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00038BDF File Offset: 0x00036DDF
		public void AddRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingOperation(new PropertyTable.Operation(ModifyTableOperationType.Add, propValues));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00038BF5 File Offset: 0x00036DF5
		public void ModifyRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingOperation(new PropertyTable.Operation(ModifyTableOperationType.Modify, propValues));
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00038C0B File Offset: 0x00036E0B
		public void RemoveRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingOperation(new PropertyTable.Operation(ModifyTableOperationType.Remove, propValues));
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00038C24 File Offset: 0x00036E24
		public IQueryResult GetQueryResult(QueryFilter queryFilter, ICollection<PropertyDefinition> columns)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(columns, "columns");
			GetTableFlags getTableFlags = GetTableFlags.None;
			if ((this.options & ModifyTableOptions.FreeBusyAware) == ModifyTableOptions.FreeBusyAware)
			{
				getTableFlags |= GetTableFlags.FreeBusy;
			}
			IQueryResult result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MapiTable mapiTable = null;
				StoreSession storeSession = this.session;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mapiTable = this.mapiModifyTable.GetTable(getTableFlags);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetMapiTable, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("PropertyTable.GetQueryResult. Failed to get the MapiTable from the MapiModifyTable.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetMapiTable, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("PropertyTable.GetQueryResult. Failed to get the MapiTable from the MapiModifyTable.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				disposeGuard.Add<MapiTable>(mapiTable);
				QueryExecutor.SetTableFilter(this.session, this.propertyReference, mapiTable, queryFilter);
				QueryResult queryResult = new QueryResult(mapiTable, columns, null, this.session, true);
				disposeGuard.Success();
				result = queryResult;
			}
			return result;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00038E24 File Offset: 0x00037024
		public void ApplyPendingChanges()
		{
			this.CheckDisposed(null);
			ModifyTableFlags modifyTableFlags = ModifyTableFlags.None;
			if ((this.options & ModifyTableOptions.FreeBusyAware) == ModifyTableOptions.FreeBusyAware)
			{
				modifyTableFlags |= ModifyTableFlags.FreeBusy;
			}
			if (this.replaceAllRows)
			{
				modifyTableFlags |= ModifyTableFlags.RowListReplace;
			}
			ICollection<RowEntry> rowList = from operation in this.pendingOperations
			select operation.ToRowEntry(this.session, this.propertyReference);
			this.EnforceRestriction(this.pendingOperations);
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.mapiModifyTable.ModifyTable(modifyTableFlags, rowList);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotModifyPropertyTable(this.tableNameForTracing), ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTable::Modify. Unable to modify a table.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotModifyPropertyTable(this.tableNameForTracing), ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTable::Modify. Unable to modify a table.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			this.pendingOperations.Clear();
			this.replaceAllRows = false;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00038FB4 File Offset: 0x000371B4
		public void SuppressRestriction()
		{
			this.CheckDisposed(null);
			this.propertyTableRestrictionSuppressed = true;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00038FC4 File Offset: 0x000371C4
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed(null);
				return this.session;
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00038FD3 File Offset: 0x000371D3
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PropertyTable>(this);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00038FDB File Offset: 0x000371DB
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.mapiModifyTable);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00038FF4 File Offset: 0x000371F4
		private static MapiModifyTable GetMapiModifyTable(CoreFolder coreFolder, NativeStorePropertyDefinition property)
		{
			PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(coreFolder);
			PropTag propTag = PropertyTagCache.Cache.PropTagFromPropertyDefinition(persistablePropertyBag.MapiProp, coreFolder.Session, property);
			StoreSession storeSession = coreFolder.Session;
			object thisObject = null;
			bool flag = false;
			MapiModifyTable result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = (MapiModifyTable)persistablePropertyBag.MapiProp.OpenProperty(propTag, PropertyTable.IExchangeModifyTableInterfaceId, 0, OpenPropertyFlags.DeferredErrors);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetMapiTable, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTable.GetMapiModifyTable. Unable to get MapiModifyTable.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetMapiTable, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTable.GetMapiModifyTable. Unable to get MapiModifyTable.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0003914C File Offset: 0x0003734C
		private void AddPendingOperation(PropertyTable.Operation pendingOperation)
		{
			this.pendingOperations.Add(pendingOperation);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00039160 File Offset: 0x00037360
		private void EnforceRestriction(ICollection<PropertyTable.Operation> operations)
		{
			if (!this.propertyTableRestrictionSuppressed && this.modifyTableRestriction != null)
			{
				this.modifyTableRestriction.Enforce(this, from operation in operations
				select operation);
			}
		}

		// Token: 0x040001F6 RID: 502
		private static readonly Guid IExchangeModifyTableInterfaceId = new Guid("2d734cb0-53fd-101b-b19d-08002b3056e3");

		// Token: 0x040001F7 RID: 503
		private readonly StoreSession session;

		// Token: 0x040001F8 RID: 504
		private readonly MapiProp propertyReference;

		// Token: 0x040001F9 RID: 505
		private readonly MapiModifyTable mapiModifyTable;

		// Token: 0x040001FA RID: 506
		private readonly string tableNameForTracing;

		// Token: 0x040001FB RID: 507
		private readonly ModifyTableOptions options;

		// Token: 0x040001FC RID: 508
		private readonly IModifyTableRestriction modifyTableRestriction;

		// Token: 0x040001FD RID: 509
		private readonly ICollection<PropertyTable.Operation> pendingOperations = new List<PropertyTable.Operation>();

		// Token: 0x040001FE RID: 510
		private bool replaceAllRows;

		// Token: 0x040001FF RID: 511
		private bool propertyTableRestrictionSuppressed;

		// Token: 0x02000067 RID: 103
		private sealed class Operation : ModifyTableOperation
		{
			// Token: 0x06000746 RID: 1862 RVA: 0x000391EE File Offset: 0x000373EE
			internal Operation(ModifyTableOperationType operationType, PropValue[] propValues) : base(operationType, propValues)
			{
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00039208 File Offset: 0x00037408
			internal RowEntry ToRowEntry(StoreSession session, MapiProp propertyMappingReference)
			{
				PropertyTable.Operation.MapiRowFactory mapiRowFactory = null;
				switch (base.Operation)
				{
				case ModifyTableOperationType.Add:
					mapiRowFactory = new PropertyTable.Operation.MapiRowFactory(RowEntry.Add);
					break;
				case ModifyTableOperationType.Modify:
					mapiRowFactory = new PropertyTable.Operation.MapiRowFactory(RowEntry.Modify);
					break;
				case ModifyTableOperationType.Remove:
					mapiRowFactory = new PropertyTable.Operation.MapiRowFactory(RowEntry.Remove);
					break;
				}
				ICollection<PropTag> collection = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(propertyMappingReference, session, from propValue in base.Properties
				select (NativeStorePropertyDefinition)propValue.Property);
				PropValue[] array = new PropValue[base.Properties.Length];
				int num = 0;
				foreach (PropTag propTag in collection)
				{
					array[num] = MapiPropertyBag.GetPropValueFromValue(session, session.ExTimeZone, propTag, base.Properties[num].Value);
					num++;
				}
				return mapiRowFactory(array);
			}

			// Token: 0x02000068 RID: 104
			// (Invoke) Token: 0x0600074A RID: 1866
			internal delegate RowEntry MapiRowFactory(ICollection<PropValue> propValues);
		}
	}
}
