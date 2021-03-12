using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000F4 RID: 244
	internal sealed class SystemMailbox : IDisposable
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x00041FE4 File Offset: 0x000401E4
		private SystemMailbox(DatabaseInfo databaseInfo)
		{
			if (databaseInfo == null)
			{
				throw new ArgumentNullException("databaseInfo");
			}
			this.databaseInfo = databaseInfo;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0004200C File Offset: 0x0004020C
		private void InitializeSettingsFolderId()
		{
			this.ConnectSystemMailboxSession();
			try
			{
				this.VoiceSettingsFolderId = this.GetSettingsFolderID("VoiceNotificationSettingsFolder");
				this.TextSettingsFolderId = this.GetSettingsFolderID("CalendarNotificationSettingsFolder");
			}
			finally
			{
				this.DisconnectSystemMailboxSession();
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0004205C File Offset: 0x0004025C
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00042064 File Offset: 0x00040264
		internal StoreObjectId TextSettingsFolderId { get; private set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0004206D File Offset: 0x0004026D
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00042075 File Offset: 0x00040275
		internal StoreObjectId VoiceSettingsFolderId { get; private set; }

		// Token: 0x06000A0C RID: 2572 RVA: 0x00042080 File Offset: 0x00040280
		internal static SystemMailbox GetInstance(DatabaseInfo databaseInfo)
		{
			SystemMailbox.Tracer.TraceDebug<Guid>((long)typeof(SystemMailbox).GetHashCode(), "Entering SystemMailbox.GetInstance for database {0}", databaseInfo.Guid);
			SystemMailbox systemMailbox;
			if (!SystemMailbox.instanceDictionary.TryGetValue(databaseInfo.Guid, out systemMailbox))
			{
				systemMailbox = new SystemMailbox(databaseInfo);
				systemMailbox.InitializeSettingsFolderId();
				if (!SystemMailbox.instanceDictionary.TryAdd(databaseInfo.Guid, systemMailbox))
				{
					SystemMailbox.Tracer.TraceDebug<Guid>((long)typeof(SystemMailbox).GetHashCode(), "Disposing created system mailbox for database {0}, using existing one", databaseInfo.Guid);
					systemMailbox.Dispose();
					systemMailbox = SystemMailbox.instanceDictionary[databaseInfo.Guid];
				}
			}
			return systemMailbox;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00042124 File Offset: 0x00040324
		internal static void RemoveInstance(DatabaseInfo databaseInfo)
		{
			if (SystemMailbox.instanceDictionary.ContainsKey(databaseInfo.Guid))
			{
				SystemMailbox systemMailbox;
				if (SystemMailbox.instanceDictionary.TryRemove(databaseInfo.Guid, out systemMailbox))
				{
					systemMailbox.Dispose();
					return;
				}
				SystemMailbox.Tracer.TraceDebug<Guid>((long)typeof(SystemMailbox).GetHashCode(), "We can't remove cached systemMailbox for database {0}", databaseInfo.Guid);
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00042183 File Offset: 0x00040383
		internal static string FormatExternalDirectoryOrganizationId(Guid tenantGuid)
		{
			return tenantGuid.ToString("D");
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00042191 File Offset: 0x00040391
		internal static string FormatItemSubject(Guid tenantGuid, string mailboxOwnerLegacyDN)
		{
			return SystemMailbox.FormatExternalDirectoryOrganizationId(tenantGuid) + mailboxOwnerLegacyDN;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000421A0 File Offset: 0x000403A0
		internal void GetSerializedData(Stream target)
		{
			lock (this.systemMailboxMutex)
			{
				if (!this.isDisposed)
				{
					this.ConnectSystemMailboxSession();
					try
					{
						using (Folder folder = Folder.Bind(this.systemMailboxSession, this.TextSettingsFolderId))
						{
							using (UserConfiguration userConfiguration = UserConfiguration.Create(folder, new UserConfigurationName("CalendarNotificationSerializedData", ConfigurationNameKind.ItemClass), UserConfigurationTypes.Stream))
							{
								using (Stream stream = userConfiguration.GetStream())
								{
									byte[] array = new byte[1024];
									int count;
									while ((count = stream.Read(array, 0, array.Length)) > 0)
									{
										target.Write(array, 0, count);
									}
								}
							}
						}
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
				}
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000422A4 File Offset: 0x000404A4
		internal void SaveSerializedData(Stream source)
		{
			lock (this.systemMailboxMutex)
			{
				if (!this.isDisposed)
				{
					this.ConnectSystemMailboxSession();
					try
					{
						using (Folder folder = Folder.Bind(this.systemMailboxSession, this.TextSettingsFolderId))
						{
							using (UserConfiguration userConfiguration = UserConfiguration.Create(folder, new UserConfigurationName("CalendarNotificationSerializedData", ConfigurationNameKind.ItemClass), UserConfigurationTypes.Stream))
							{
								using (Stream stream = userConfiguration.GetStream())
								{
									byte[] array = new byte[1024];
									int count;
									while ((count = source.Read(array, 0, array.Length)) > 0)
									{
										stream.Write(array, 0, count);
									}
									userConfiguration.Save();
								}
							}
						}
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
				}
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000423AC File Offset: 0x000405AC
		internal void SetValid()
		{
			lock (this.systemMailboxMutex)
			{
				if (!this.isDisposed)
				{
					this.ConnectSystemMailboxSession();
					try
					{
						using (Item item = MessageItem.Create(this.systemMailboxSession, this.TextSettingsFolderId))
						{
							item.ClassName = "IPM.Configuration.UserCalendarNotification";
							item[ItemSchema.Subject] = "CalendarNotificationSettingsFolderValid";
							item.Save(SaveMode.ResolveConflicts);
						}
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
				}
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00042458 File Offset: 0x00040658
		internal bool IsValid()
		{
			bool result;
			lock (this.systemMailboxMutex)
			{
				if (this.isDisposed)
				{
					result = false;
				}
				else
				{
					this.ConnectSystemMailboxSession();
					try
					{
						using (Folder folder = Folder.Bind(this.systemMailboxSession, this.TextSettingsFolderId))
						{
							QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, "CalendarNotificationSettingsFolderValid");
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, SystemMailbox.updatingUserSettingsProperties))
							{
								result = (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter) && queryResult.SeekToCondition(SeekReference.OriginCurrent, SystemMailbox.SettingsItemClassFilter) && 0 < queryResult.GetPropertyBags(1).Length);
							}
						}
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
				}
			}
			return result;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00042548 File Offset: 0x00040748
		internal void Update(Guid externalDirectoryOrganizationId, string mailboxOwnerLegacyDN, string settings, StoreObjectId settingsFolderId)
		{
			VersionedId versionedId = null;
			string text = SystemMailbox.FormatItemSubject(externalDirectoryOrganizationId, mailboxOwnerLegacyDN);
			QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, text);
			lock (this.systemMailboxMutex)
			{
				if (!this.isDisposed)
				{
					this.ConnectSystemMailboxSession();
					try
					{
						using (Folder folder = Folder.Bind(this.systemMailboxSession, settingsFolderId))
						{
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, SystemMailbox.sortBySubject, SystemMailbox.updatingUserSettingsProperties))
							{
								IStorePropertyBag[] propertyBags;
								if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter) && queryResult.SeekToCondition(SeekReference.OriginCurrent, SystemMailbox.SettingsItemClassFilter) && 0 < (propertyBags = queryResult.GetPropertyBags(1)).Length)
								{
									versionedId = (VersionedId)propertyBags[0].TryGetProperty(ItemSchema.Id);
								}
							}
						}
						if (string.IsNullOrEmpty(settings))
						{
							if (versionedId != null)
							{
								this.systemMailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
								{
									versionedId
								});
								SystemMailbox.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "Notification disabled {0} under folder {1}", text, settingsFolderId);
							}
						}
						else
						{
							using (Item item = (versionedId != null) ? MessageItem.Bind(this.systemMailboxSession, versionedId) : MessageItem.Create(this.systemMailboxSession, settingsFolderId))
							{
								if (versionedId == null)
								{
									item.ClassName = "IPM.Configuration.UserCalendarNotification";
									item[ItemSchema.Subject] = text;
								}
								item[ItemSchema.TextBody] = settings;
								item.Save(SaveMode.ResolveConflicts);
								SystemMailbox.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "Notification settings of user {0} have been saved under folder {1}", text, settingsFolderId);
							}
						}
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
				}
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00042760 File Offset: 0x00040960
		internal Dictionary<string, UserSettings> GetAllCalendarNotificationUsers()
		{
			Dictionary<string, UserSettings> dictionary = new Dictionary<string, UserSettings>(StringComparer.InvariantCultureIgnoreCase);
			Dictionary<string, UserSettings> result;
			lock (this.systemMailboxMutex)
			{
				if (this.isDisposed)
				{
					ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "We didn't get user's settings because system mailbox is disposed");
					result = dictionary;
				}
				else
				{
					this.ConnectSystemMailboxSession();
					try
					{
						ExDateTime now = ExDateTime.Now;
						NotificationFactories.Instance.GetAllUsersSettingsFromSystemMailbox(dictionary, this, this.systemMailboxSession);
						ExTraceGlobals.AssistantTracer.TraceDebug<int, double>((long)this.GetHashCode(), "Total time to load {0} users: {1} milliseconds", dictionary.Count, (ExDateTime.Now - now).TotalMilliseconds);
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00042830 File Offset: 0x00040A30
		internal bool MoveCalendarNotificationSettingsToSettingsFolder()
		{
			bool result;
			lock (this.systemMailboxMutex)
			{
				if (this.isDisposed)
				{
					result = false;
				}
				else
				{
					this.ConnectSystemMailboxSession();
					try
					{
						StoreObjectId defaultFolderId = this.systemMailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration);
						using (Folder folder = Folder.Bind(this.systemMailboxSession, defaultFolderId))
						{
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, SystemMailbox.SortByItemClass, SystemMailbox.updatingUserSettingsProperties))
							{
								List<VersionedId> list = new List<VersionedId>();
								if (queryResult.SeekToCondition(SeekReference.OriginBeginning, SystemMailbox.SettingsItemClassFilter))
								{
									bool flag2 = false;
									do
									{
										IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
										if (propertyBags.Length <= 0)
										{
											break;
										}
										foreach (IStorePropertyBag storePropertyBag in propertyBags)
										{
											if (!string.Equals(storePropertyBag.TryGetProperty(StoreObjectSchema.ItemClass) as string, "IPM.Configuration.UserCalendarNotification", StringComparison.OrdinalIgnoreCase))
											{
												flag2 = true;
												break;
											}
											list.Add((VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id));
										}
									}
									while (!flag2);
								}
								if (list.Count == 0)
								{
									result = false;
								}
								else
								{
									GroupOperationResult groupOperationResult = folder.MoveItems(this.TextSettingsFolderId, list.ToArray());
									if (groupOperationResult.OperationResult != OperationResult.Succeeded)
									{
										result = false;
									}
									else
									{
										result = true;
									}
								}
							}
						}
					}
					finally
					{
						this.DisconnectSystemMailboxSession();
					}
				}
			}
			return result;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000429E0 File Offset: 0x00040BE0
		private StoreObjectId GetSettingsFolderID(string settingsFolderName)
		{
			StoreObjectId defaultFolderId = this.systemMailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration);
			StoreObjectId objectId;
			using (Folder folder = Folder.Create(this.systemMailboxSession, defaultFolderId, StoreObjectType.Folder, settingsFolderName, CreateMode.OpenIfExists))
			{
				if (folder.Id == null)
				{
					FolderSaveResult folderSaveResult = folder.Save();
					if (folderSaveResult.OperationResult != OperationResult.Succeeded)
					{
						SystemMailbox.Tracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "Failed to open settings folder in the config folder:{0}", this.systemMailboxSession.MailboxOwner);
						throw new TransientMailboxException(Strings.descFailedToCreateTempFolder(this.systemMailboxSession.MailboxOwner.MailboxInfo.DisplayName, this.databaseInfo.DisplayName));
					}
					folder.Load();
				}
				objectId = folder.Id.ObjectId;
			}
			return objectId;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00042AA0 File Offset: 0x00040CA0
		private void ConnectSystemMailboxSession()
		{
			if (this.isDisposed)
			{
				throw new InvalidOperationException("System mailbox is disposed");
			}
			if (this.systemMailboxSession == null)
			{
				this.systemMailboxSession = this.databaseInfo.GetSystemMailbox(ClientType.EventBased, "MSExchangeMailboxAssistants");
			}
			if (!this.systemMailboxSession.IsConnected)
			{
				this.systemMailboxSession.Connect();
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00042AF7 File Offset: 0x00040CF7
		private void DisconnectSystemMailboxSession()
		{
			if (this.isDisposed)
			{
				throw new InvalidOperationException("System mailbox is disposed");
			}
			if (this.systemMailboxSession != null && this.systemMailboxSession.IsConnected)
			{
				this.systemMailboxSession.Disconnect();
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00042B2C File Offset: 0x00040D2C
		public void Dispose()
		{
			if (this.isDisposed)
			{
				return;
			}
			lock (this.systemMailboxMutex)
			{
				if (!this.isDisposed)
				{
					if (this.systemMailboxSession != null)
					{
						this.systemMailboxSession.Dispose();
						this.systemMailboxSession = null;
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x0400068F RID: 1679
		internal const string SettingsItemClass = "IPM.Configuration.UserCalendarNotification";

		// Token: 0x04000690 RID: 1680
		private const string SerializedDataItemClass = "CalendarNotificationSerializedData";

		// Token: 0x04000691 RID: 1681
		private const string VoiceSettingsFolderName = "VoiceNotificationSettingsFolder";

		// Token: 0x04000692 RID: 1682
		private const string TextSettingsFolderName = "CalendarNotificationSettingsFolder";

		// Token: 0x04000693 RID: 1683
		internal const string SettingsFolderValidFlag = "CalendarNotificationSettingsFolderValid";

		// Token: 0x04000694 RID: 1684
		internal static readonly int TenantGuidStringLength = SystemMailbox.FormatExternalDirectoryOrganizationId(Guid.Empty).Length;

		// Token: 0x04000695 RID: 1685
		private static ConcurrentDictionary<Guid, SystemMailbox> instanceDictionary = new ConcurrentDictionary<Guid, SystemMailbox>();

		// Token: 0x04000696 RID: 1686
		internal static readonly QueryFilter SettingsItemClassFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Configuration.UserCalendarNotification");

		// Token: 0x04000697 RID: 1687
		private static readonly SortBy[] sortBySubject = new SortBy[]
		{
			new SortBy(ItemSchema.Subject, SortOrder.Descending)
		};

		// Token: 0x04000698 RID: 1688
		internal static readonly SortBy[] SortByItemClass = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Descending)
		};

		// Token: 0x04000699 RID: 1689
		internal static readonly PropertyDefinition[] RetrievingUserSettingsProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			ItemSchema.TextBody
		};

		// Token: 0x0400069A RID: 1690
		private static readonly PropertyDefinition[] updatingUserSettingsProperties = new PropertyDefinition[]
		{
			ItemSchema.Subject,
			StoreObjectSchema.ItemClass,
			ItemSchema.Id
		};

		// Token: 0x0400069B RID: 1691
		private static readonly Trace Tracer = ExTraceGlobals.SystemMailboxTracer;

		// Token: 0x0400069C RID: 1692
		private MailboxSession systemMailboxSession;

		// Token: 0x0400069D RID: 1693
		private DatabaseInfo databaseInfo;

		// Token: 0x0400069E RID: 1694
		private object systemMailboxMutex = new object();

		// Token: 0x0400069F RID: 1695
		private bool isDisposed;
	}
}
