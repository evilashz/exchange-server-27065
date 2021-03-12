using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E71 RID: 3697
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserSyncStateMetadata
	{
		// Token: 0x0600802E RID: 32814 RVA: 0x00230C47 File Offset: 0x0022EE47
		public UserSyncStateMetadata(MailboxSession mailboxSession)
		{
			this.Name = mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
			this.MailboxGuid = mailboxSession.MailboxGuid;
		}

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x0600802F RID: 32815 RVA: 0x00230C7C File Offset: 0x0022EE7C
		// (set) Token: 0x06008030 RID: 32816 RVA: 0x00230C84 File Offset: 0x0022EE84
		public string Name { get; private set; }

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x06008031 RID: 32817 RVA: 0x00230C8D File Offset: 0x0022EE8D
		// (set) Token: 0x06008032 RID: 32818 RVA: 0x00230C95 File Offset: 0x0022EE95
		public Guid MailboxGuid { get; private set; }

		// Token: 0x06008033 RID: 32819 RVA: 0x00230CA0 File Offset: 0x0022EEA0
		public DeviceSyncStateMetadata TryRemove(DeviceIdentity deviceIdentity, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			DeviceSyncStateMetadata result;
			bool arg = this.devices.TryRemove(deviceIdentity, out result);
			syncLogger.TraceDebug<DeviceIdentity, bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.TryRemove] Tried to remove '{0}'.  Success? {1}", deviceIdentity, arg);
			return result;
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x00230CE0 File Offset: 0x0022EEE0
		public void Clear()
		{
			this.devices.Clear();
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x00230CF0 File Offset: 0x0022EEF0
		public DeviceSyncStateMetadata GetDevice(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			DeviceSyncStateMetadata result;
			if (this.devices.TryGetValue(deviceIdentity, out result))
			{
				syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetDevice] Cache hit for device: {0}", deviceIdentity);
				return result;
			}
			syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetDevice] Cache MISS for device: {0}", deviceIdentity);
			DeviceSyncStateMetadata result2;
			using (Folder syncRootFolder = this.GetSyncRootFolder(mailboxSession, syncLogger))
			{
				using (QueryResult queryResult = syncRootFolder.FolderQuery(FolderQueryFlags.None, null, UserSyncStateMetadata.displayNameSort, new PropertyDefinition[]
				{
					FolderSchema.DisplayName,
					FolderSchema.Id
				}))
				{
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, deviceIdentity.CompositeKey)))
					{
						IStorePropertyBag storePropertyBag = queryResult.GetPropertyBags(1)[0];
						StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(FolderSchema.Id)).ObjectId;
						string text = (string)storePropertyBag.TryGetProperty(FolderSchema.DisplayName);
						DeviceSyncStateMetadata deviceSyncStateMetadata = new DeviceSyncStateMetadata(mailboxSession, objectId, syncLogger);
						DeviceSyncStateMetadata orAdd = this.devices.GetOrAdd(deviceIdentity, deviceSyncStateMetadata);
						if (!object.ReferenceEquals(deviceSyncStateMetadata, orAdd))
						{
							syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetDevice] Race condition adding new device '{0}' to cache.  Disarding new instance.", deviceIdentity);
						}
						else
						{
							syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetDevice] Added new device instance to user cache: {0}", deviceIdentity);
						}
						result2 = orAdd;
					}
					else
					{
						syncLogger.TraceDebug<SmtpAddress, DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetDevice] Mailbox '{0}' does not contain a device folder for '{1}'", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, deviceIdentity);
						result2 = null;
					}
				}
			}
			return result2;
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x00230EA0 File Offset: 0x0022F0A0
		public DeviceSyncStateMetadata GetOrAdd(DeviceSyncStateMetadata toAdd)
		{
			return this.devices.GetOrAdd(toAdd.Id, toAdd);
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x00230F0C File Offset: 0x0022F10C
		public List<DeviceSyncStateMetadata> GetAllDevices(MailboxSession mailboxSession, bool forceRefresh, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			syncLogger.TraceDebug<SmtpAddress, bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetAllDevices] Getting all devies for Mailbox: {0}, forceRefresh: {1}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, forceRefresh);
			List<DeviceSyncStateMetadata> list = null;
			using (Folder syncRootFolder = this.GetSyncRootFolder(mailboxSession, syncLogger))
			{
				using (QueryResult queryResult = syncRootFolder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id,
					FolderSchema.DisplayName
				}))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows == null || rows.Length == 0)
						{
							break;
						}
						object[][] array = rows;
						for (int i = 0; i < array.Length; i++)
						{
							object[] array2 = array[i];
							StoreObjectId deviceFolderId = ((VersionedId)array2[0]).ObjectId;
							DeviceIdentity deviceIdentity = new DeviceIdentity((string)array2[1]);
							syncLogger.TraceDebug<DeviceIdentity>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetAllDevices] Found device: {0}", deviceIdentity);
							DeviceSyncStateMetadata item;
							if (forceRefresh)
							{
								item = this.devices.AddOrUpdate(deviceIdentity, new DeviceSyncStateMetadata(mailboxSession, deviceFolderId, syncLogger), (DeviceIdentity key, DeviceSyncStateMetadata old) => new DeviceSyncStateMetadata(mailboxSession, deviceFolderId, syncLogger));
							}
							else
							{
								item = this.devices.GetOrAdd(deviceIdentity, (DeviceIdentity key) => new DeviceSyncStateMetadata(mailboxSession, deviceFolderId, syncLogger));
							}
							if (list == null)
							{
								list = new List<DeviceSyncStateMetadata>();
							}
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x00231114 File Offset: 0x0022F314
		public override string ToString()
		{
			return string.Format("User: {0} - {1}", this.Name, this.MailboxGuid);
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x00231134 File Offset: 0x0022F334
		private Folder GetSyncRootFolder(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			Folder result = null;
			try
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.SyncRoot);
			}
			catch (ObjectNotFoundException)
			{
				syncLogger.TraceDebug<SmtpAddress>(ExTraceGlobals.SyncTracer, (long)this.GetHashCode(), "[UserSyncStateMetadata.GetDevice] Missing SyncRoot folder for mailbox {0}.  Recreating.", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
				mailboxSession.CreateDefaultFolder(DefaultFolderType.SyncRoot);
				result = Folder.Bind(mailboxSession, DefaultFolderType.SyncRoot);
			}
			return result;
		}

		// Token: 0x0400568A RID: 22154
		private static readonly SortBy[] displayNameSort = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x0400568B RID: 22155
		private ConcurrentDictionary<DeviceIdentity, DeviceSyncStateMetadata> devices = new ConcurrentDictionary<DeviceIdentity, DeviceSyncStateMetadata>();
	}
}
