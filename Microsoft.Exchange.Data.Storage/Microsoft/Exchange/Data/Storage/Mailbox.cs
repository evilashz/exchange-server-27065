using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Mailbox : IXSOMailbox, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, INotificationSource
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00003674 File Offset: 0x00001874
		internal Mailbox(MailboxStoreObject mailboxStoreObject)
		{
			this.mailboxStoreObject = mailboxStoreObject;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003683 File Offset: 0x00001883
		public bool IsDirty
		{
			get
			{
				return this.mailboxStoreObject.IsDirty;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003690 File Offset: 0x00001890
		public void Load()
		{
			this.Load(null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003699 File Offset: 0x00001899
		public void Load(params PropertyDefinition[] propertyDefinitions)
		{
			this.Load((ICollection<PropertyDefinition>)propertyDefinitions);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000036A7 File Offset: 0x000018A7
		public void Load(ICollection<PropertyDefinition> propertyDefinitions)
		{
			this.mailboxStoreObject.Load(propertyDefinitions);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000036B5 File Offset: 0x000018B5
		public Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			return this.mailboxStoreObject.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000036C4 File Offset: 0x000018C4
		public object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			return this.mailboxStoreObject.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000036D2 File Offset: 0x000018D2
		public bool IsPropertyDirty(PropertyDefinition propertyDefinition)
		{
			return this.mailboxStoreObject.IsPropertyDirty(propertyDefinition);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000036E0 File Offset: 0x000018E0
		public void Delete(PropertyDefinition propertyDefinition)
		{
			this.mailboxStoreObject.Delete(propertyDefinition);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000036EE File Offset: 0x000018EE
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			return this.mailboxStoreObject.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000036FD File Offset: 0x000018FD
		public void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			this.mailboxStoreObject.SetOrDeleteProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x17000004 RID: 4
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.mailboxStoreObject[propertyDefinition];
			}
			set
			{
				this.mailboxStoreObject[propertyDefinition] = value;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003729 File Offset: 0x00001929
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			return this.mailboxStoreObject.GetProperties(propertyDefinitionArray);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003737 File Offset: 0x00001937
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			this.mailboxStoreObject.SetProperties(propertyDefinitionArray, propertyValuesArray);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003746 File Offset: 0x00001946
		public void Save()
		{
			this.mailboxStoreObject.Save();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003754 File Offset: 0x00001954
		public Guid InstanceGuid
		{
			get
			{
				StoreSession session = this.CoreObject.Session;
				bool flag = false;
				Guid mailboxInstanceGuid;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mailboxInstanceGuid = this.mailboxStoreObject.MapiStore.GetMailboxInstanceGuid();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetProperties, ex, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Could not retrieve a Mailbox InstanceGuid", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetProperties, ex2, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Could not retrieve a Mailbox InstanceGuid", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag)
							{
								session.EndServerHealthCall();
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
				return mailboxInstanceGuid;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003884 File Offset: 0x00001A84
		public bool IsContentIndexingEnabled
		{
			get
			{
				return this.mailboxStoreObject.IsContentIndexingEnabled;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003891 File Offset: 0x00001A91
		public void ForceReload(params PropertyDefinition[] propsToLoad)
		{
			this.mailboxStoreObject.ForceReload(propsToLoad);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000389F File Offset: 0x00001A9F
		public ICoreObject CoreObject
		{
			get
			{
				return this.mailboxStoreObject.CoreObject;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000038AC File Offset: 0x00001AAC
		public MapiStore MapiStore
		{
			get
			{
				return this.mailboxStoreObject.MapiStore;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000038B9 File Offset: 0x00001AB9
		internal StoreObjectId StoreObjectId
		{
			get
			{
				return this.mailboxStoreObject.StoreObjectId;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000038C8 File Offset: 0x00001AC8
		internal MapiNotificationHandle Advise(byte[] entryId, AdviseFlags eventMask, MapiNotificationHandler handler, NotificationCallbackMode callbackMode)
		{
			StoreSession session = this.CoreObject.Session;
			object thisObject = null;
			bool flag = false;
			MapiNotificationHandle result;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = this.MapiStore.Advise(entryId, eventMask, handler, callbackMode, (MapiNotificationClientFlags)0);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotAddNotification, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Mailbox::Advise.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotAddNotification, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Mailbox::Advise.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
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

		// Token: 0x06000045 RID: 69 RVA: 0x000039F8 File Offset: 0x00001BF8
		public void Unadvise(object notificationHandle)
		{
			Util.ThrowOnNullArgument(notificationHandle, "notificationHandle");
			StoreSession session = this.CoreObject.Session;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				if (!this.MapiStore.IsDisposed && !this.MapiStore.IsDead)
				{
					this.MapiStore.Unadvise((MapiNotificationHandle)notificationHandle);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotRemoveNotification, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Mailbox::Unadvise. NotificationHandle = {0}", notificationHandle),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotRemoveNotification, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Mailbox::Unadvise. NotificationHandle = {0}", notificationHandle),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
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
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003B40 File Offset: 0x00001D40
		public bool IsDisposedOrDead
		{
			get
			{
				return this.mailboxStoreObject == null || this.mailboxStoreObject.IsDisposed || this.MapiStore == null || this.MapiStore.IsDisposed || this.MapiStore.IsDead;
			}
		}

		// Token: 0x04000004 RID: 4
		private readonly MailboxStoreObject mailboxStoreObject;
	}
}
