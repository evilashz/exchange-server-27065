using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Connections.Eas.Commands.FolderSync;
using Microsoft.Exchange.Connections.Eas.Model.Extensions;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EasMailbox : MailboxProviderBase, IMailbox, IDisposable
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003F63 File Offset: 0x00002163
		public EasMailbox() : base(LocalMailboxFlags.EasSync)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003F7B File Offset: 0x0000217B
		protected EasMailbox(EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters) : this()
		{
			this.EasConnectionParameters = connectionParameters;
			this.EasAuthenticationParameters = authenticationParameters;
			this.EasDeviceParameters = deviceParameters;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003F98 File Offset: 0x00002198
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003FA0 File Offset: 0x000021A0
		public override int ServerVersion { get; protected set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003FA9 File Offset: 0x000021A9
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003FB1 File Offset: 0x000021B1
		internal EasConnectionWrapper EasConnectionWrapper { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003FBC File Offset: 0x000021BC
		internal EasConnectionWrapper CrawlerConnectionWrapper
		{
			get
			{
				if (this.crawlerConnectionWrapper == null)
				{
					lock (this.syncRoot)
					{
						if (this.crawlerConnectionWrapper == null && this.ConnectionIsOperational)
						{
							EasConnectionWrapper easConnectionWrapper = new EasConnectionWrapper(new EasCrawlerConnection(this.EasConnectionParameters, this.EasAuthenticationParameters, this.EasDeviceParameters));
							easConnectionWrapper.Connect();
							this.crawlerConnectionWrapper = easConnectionWrapper;
						}
					}
				}
				return this.crawlerConnectionWrapper;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004040 File Offset: 0x00002240
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004048 File Offset: 0x00002248
		private protected EasConnectionParameters EasConnectionParameters { protected get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004051 File Offset: 0x00002251
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004059 File Offset: 0x00002259
		private protected EasAuthenticationParameters EasAuthenticationParameters { protected get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004062 File Offset: 0x00002262
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000406A File Offset: 0x0000226A
		private protected EasDeviceParameters EasDeviceParameters { protected get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004073 File Offset: 0x00002273
		protected bool ConnectionIsOperational
		{
			get
			{
				return this.EasConnectionWrapper != null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004081 File Offset: 0x00002281
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004089 File Offset: 0x00002289
		private protected EntryIdMap<Add> EasFolderCache { protected get; private set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00004092 File Offset: 0x00002292
		public override SyncProtocol GetSyncProtocol()
		{
			return SyncProtocol.Eas;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004098 File Offset: 0x00002298
		void IMailbox.ConfigEas(NetworkCredential userCredential, SmtpAddress smtpAddress, Guid mailboxGuid, string remoteHostName)
		{
			this.EasAuthenticationParameters = new EasAuthenticationParameters(userCredential, smtpAddress.Local, smtpAddress.Domain, base.TestIntegration.EasAutodiscoverUrlOverride ?? remoteHostName);
			this.EasConnectionParameters = new EasConnectionParameters(new UniquelyNamedObject(), new NullLog(), EasProtocolVersion.Version140, false, false, null);
			string deviceIdPrefix = mailboxGuid.ToString("N").Substring(0, EasMailbox.EasDeviceIdPrefixLength);
			this.EasDeviceParameters = new EasDeviceParameters("0123456789ABCDEF", "EasConnectionDeviceType", "ExchangeMrsAgent", deviceIdPrefix);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004120 File Offset: 0x00002320
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("EasMailbox.IMailbox.Config", new object[0]);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000413D File Offset: 0x0000233D
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004144 File Offset: 0x00002344
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000414B File Offset: 0x0000234B
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004152 File Offset: 0x00002352
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004154 File Offset: 0x00002354
		bool IMailbox.IsConnected()
		{
			MrsTracer.Provider.Function("EasMailbox.IMailbox.IsConnected", new object[0]);
			return this.ConnectionIsOperational;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004174 File Offset: 0x00002374
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			return capability == MailboxCapabilities.ReplayActions;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000418E File Offset: 0x0000238E
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.Provider.Function("EasMailbox.IMailbox.GetMailboxInformation", new object[0]);
			return EasMailbox.mailboxInformationSingleton;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000041AC File Offset: 0x000023AC
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("EasMailbox.IMailbox.Connect", new object[0]);
			EasConnectionWrapper easConnectionWrapper = new EasConnectionWrapper(EasConnection.CreateInstance(this.EasConnectionParameters, this.EasAuthenticationParameters, this.EasDeviceParameters));
			easConnectionWrapper.Connect();
			this.EasConnectionWrapper = easConnectionWrapper;
			this.AfterConnect();
			MrsTracer.Provider.Debug("EasMailbox.IMailbox.Connect succeeded.", new object[0]);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000421C File Offset: 0x0000241C
		void IMailbox.Disconnect()
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("EasMailbox.IMailbox.Disconnect", new object[0]);
			lock (this.syncRoot)
			{
				if (this.ConnectionIsOperational)
				{
					this.EasConnectionWrapper.Disconnect();
					this.EasConnectionWrapper = null;
					if (this.crawlerConnectionWrapper != null)
					{
						this.crawlerConnectionWrapper.Disconnect();
						this.crawlerConnectionWrapper = null;
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000042A8 File Offset: 0x000024A8
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			return new MailboxServerInformation
			{
				MailboxServerName = this.EasConnectionWrapper.ServerName
			};
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042CD File Offset: 0x000024CD
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042D4 File Offset: 0x000024D4
		void IMailbox.SeedMBICache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000042DC File Offset: 0x000024DC
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("EasMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			this.RefreshFolderCache();
			List<FolderRec> list = new List<FolderRec>(this.EasFolderCache.Count + 2);
			foreach (Add add in this.EasFolderCache.Values)
			{
				list.Add(this.CreateGenericFolderRec(add));
			}
			list.Add(EasSyntheticFolder.RootFolder.FolderRec);
			list.Add(EasSyntheticFolder.IpmSubtreeFolder.FolderRec);
			return list;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004398 File Offset: 0x00002598
		void IMailbox.DeleteMailbox(int flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000439F File Offset: 0x0000259F
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043C0 File Offset: 0x000025C0
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			MrsTracer.Provider.Function("EasMailbox.IMailbox.GetIDsFromNames", new object[0]);
			if (createIfNotExists)
			{
				throw new GetIdsFromNamesCalledOnDestinationException();
			}
			return SyncEmailUtils.GetIDsFromNames(npda, (NamedPropData propTag) => StringComparer.OrdinalIgnoreCase.Equals(propTag.Name, "ObjectType"));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000440E File Offset: 0x0000260E
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004415 File Offset: 0x00002615
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000441C File Offset: 0x0000261C
		ADUser IMailbox.GetADUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004423 File Offset: 0x00002623
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid targetDatabaseGuid, Guid targetArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000442C File Offset: 0x0000262C
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			List<WellKnownFolder> list = new List<WellKnownFolder>(this.EasFolderCache.Count + 1);
			foreach (Add add in this.EasFolderCache.Values)
			{
				WellKnownFolderType wkfType;
				if (EasMailbox.folderTypeMap.TryGetValue(add.GetEasFolderType(), out wkfType))
				{
					list.Add(new WellKnownFolder((int)wkfType, EasMailbox.GetEntryId(add.ServerId)));
				}
			}
			list.Add(new WellKnownFolder(3, EasSyntheticFolder.IpmSubtreeFolder.EntryId));
			return list;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000044D4 File Offset: 0x000026D4
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000044DB File Offset: 0x000026DB
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000044E2 File Offset: 0x000026E2
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000044E9 File Offset: 0x000026E9
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000044F0 File Offset: 0x000026F0
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000044F7 File Offset: 0x000026F7
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			MrsTracer.Provider.Function("EasMailbox.IMailbox.CheckServerHealth", new object[0]);
			return ServerHealthStatus.Healthy;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004522 File Offset: 0x00002722
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			MrsTracer.Provider.Function("EasMailbox.GetProps", new object[0]);
			return Array.ConvertAll<PropTag, PropValueData>(ptags, (PropTag propTag) => new PropValueData(propTag.ChangePropType(PropType.Null), null));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000455C File Offset: 0x0000275C
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004563 File Offset: 0x00002763
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			return new SessionStatistics();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000456A File Offset: 0x0000276A
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004571 File Offset: 0x00002771
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004578 File Offset: 0x00002778
		internal static byte[] GetEntryId(string stringId)
		{
			return Encoding.UTF8.GetBytes(stringId);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004585 File Offset: 0x00002785
		internal static string GetStringId(byte[] entryId)
		{
			return Encoding.UTF8.GetString(entryId);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004594 File Offset: 0x00002794
		internal EasFolderSyncState GetPersistedSyncState(SyncContentsManifestState syncBlob)
		{
			if (syncBlob.Data != null)
			{
				return EasFolderSyncState.Deserialize(syncBlob.Data);
			}
			return new EasFolderSyncState
			{
				SyncKey = "0",
				CrawlerSyncKey = "0"
			};
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000045D2 File Offset: 0x000027D2
		internal EasFolderSyncState GetPersistedSyncState(byte[] folderId)
		{
			return this.GetPersistedSyncState(this.SyncState[folderId]);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000045E6 File Offset: 0x000027E6
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000045F8 File Offset: 0x000027F8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EasMailbox>(this);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004600 File Offset: 0x00002800
		protected EasHierarchySyncState RefreshFolderCache()
		{
			EasHierarchySyncState hierarchySyncState = this.GetHierarchySyncState();
			this.RefreshFolderCache(hierarchySyncState);
			return hierarchySyncState;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000461C File Offset: 0x0000281C
		protected void RefreshFolderCache(EasHierarchySyncState state)
		{
			IReadOnlyCollection<Add> folders = state.Folders;
			EntryIdMap<Add> entryIdMap = new EntryIdMap<Add>(folders.Count);
			this.defaultCalendarId = null;
			foreach (Add add in folders)
			{
				EasFolderType easFolderType = add.GetEasFolderType();
				if (easFolderType == EasFolderType.Calendar)
				{
					this.defaultCalendarId = EasMailbox.GetEntryId(add.ServerId);
				}
				EasFolderType easFolderType2 = easFolderType;
				if (easFolderType2 == EasFolderType.UserGeneric)
				{
					goto IL_8B;
				}
				bool flag;
				switch (easFolderType2)
				{
				case EasFolderType.Contacts:
				case EasFolderType.UserContacts:
					flag = !ConfigBase<MRSConfigSchema>.GetConfig<bool>("DisableContactSync");
					goto IL_9D;
				case EasFolderType.UserMail:
				case EasFolderType.UserCalendar:
					goto IL_8B;
				}
				flag = EasMailbox.folderTypeMap.ContainsKey(easFolderType);
				IL_9D:
				if (flag)
				{
					entryIdMap.Add(EasMailbox.GetEntryId(add.ServerId), add);
					continue;
				}
				MrsTracer.Provider.Debug("EasMailbox.RefreshFolderCache: ignore {0} folder '{1}' since it is not supported yet", new object[]
				{
					easFolderType,
					add.DisplayName
				});
				continue;
				IL_8B:
				flag = true;
				goto IL_9D;
			}
			this.EasFolderCache = entryIdMap;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000473C File Offset: 0x0000293C
		private static string GetFolderClass(Add add)
		{
			switch (add.GetEasFolderType())
			{
			case EasFolderType.UserGeneric:
				return "Generic";
			case EasFolderType.Tasks:
			case EasFolderType.UserTasks:
				return "Task";
			case EasFolderType.Calendar:
			case EasFolderType.UserCalendar:
				return "Calendar";
			case EasFolderType.Contacts:
			case EasFolderType.UserContacts:
				return "Contact";
			case EasFolderType.Notes:
			case EasFolderType.UserNotes:
				return "Note";
			case EasFolderType.Journal:
			case EasFolderType.UserJournal:
				return "Journal";
			}
			return "Mail";
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000047C7 File Offset: 0x000029C7
		private FolderRec CreateGenericFolderRec(Add add)
		{
			return new FolderRec(EasMailbox.GetEntryId(add.ServerId), this.GetParentId(add), FolderType.Generic, EasMailbox.GetFolderClass(add), add.DisplayName, DateTime.MinValue, null);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000047F3 File Offset: 0x000029F3
		private byte[] GetParentId(Add add)
		{
			if (add.GetEasFolderType() != EasFolderType.UserCalendar)
			{
				return EasMailbox.GetEntryId(add.ParentId);
			}
			return this.defaultCalendarId;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004814 File Offset: 0x00002A14
		private EasHierarchySyncState GetHierarchySyncState()
		{
			SyncHierarchyManifestState hierarchyData = this.SyncState.HierarchyData;
			EasHierarchySyncState easHierarchySyncState;
			if (hierarchyData != null && !string.IsNullOrEmpty(hierarchyData.ProviderSyncState) && hierarchyData.ManualSyncData == null)
			{
				MrsTracer.Provider.Debug("EasMailbox.GetHierarchySyncState: Deserialize folder state from hierState.ProviderSyncState", new object[0]);
				easHierarchySyncState = EasHierarchySyncState.Deserialize(hierarchyData.ProviderSyncState);
			}
			else
			{
				MrsTracer.Provider.Debug("EasMailbox.GetHierarchySyncState: Get all the folders from the EAS server", new object[0]);
				string syncKey;
				Add[] allFoldersOnServer = this.GetAllFoldersOnServer(out syncKey);
				easHierarchySyncState = new EasHierarchySyncState(allFoldersOnServer, syncKey);
				hierarchyData.ProviderSyncState = easHierarchySyncState.Serialize(false);
			}
			hierarchyData.ManualSyncData = null;
			return easHierarchySyncState;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000048A4 File Offset: 0x00002AA4
		private Add[] GetAllFoldersOnServer(out string newSyncKey)
		{
			FolderSyncResponse folderSyncResponse = this.EasConnectionWrapper.FolderSync();
			List<Add> additions = folderSyncResponse.Changes.Additions;
			newSyncKey = folderSyncResponse.SyncKey;
			return additions.ToArray();
		}

		// Token: 0x0400002E RID: 46
		private const string ProviderName = "EasProvider";

		// Token: 0x0400002F RID: 47
		private const string EasDeviceId = "0123456789ABCDEF";

		// Token: 0x04000030 RID: 48
		private const string EasDeviceType = "EasConnectionDeviceType";

		// Token: 0x04000031 RID: 49
		private const string EasUserAgent = "ExchangeMrsAgent";

		// Token: 0x04000032 RID: 50
		private static readonly int EasDeviceIdPrefixLength = 32 - "0123456789ABCDEF".Length;

		// Token: 0x04000033 RID: 51
		private static readonly MailboxInformation mailboxInformationSingleton = new MailboxInformation
		{
			ProviderName = "EasProvider"
		};

		// Token: 0x04000034 RID: 52
		private static readonly Dictionary<EasFolderType, WellKnownFolderType> folderTypeMap = new Dictionary<EasFolderType, WellKnownFolderType>
		{
			{
				EasFolderType.Inbox,
				WellKnownFolderType.Inbox
			},
			{
				EasFolderType.DeletedItems,
				WellKnownFolderType.DeletedItems
			},
			{
				EasFolderType.Drafts,
				WellKnownFolderType.Drafts
			},
			{
				EasFolderType.SentItems,
				WellKnownFolderType.SentItems
			},
			{
				EasFolderType.Outbox,
				WellKnownFolderType.Outbox
			},
			{
				EasFolderType.JunkEmail,
				WellKnownFolderType.JunkEmail
			},
			{
				EasFolderType.Calendar,
				WellKnownFolderType.Calendar
			},
			{
				EasFolderType.Contacts,
				WellKnownFolderType.Contacts
			}
		};

		// Token: 0x04000035 RID: 53
		private readonly object syncRoot = new object();

		// Token: 0x04000036 RID: 54
		private EasConnectionWrapper crawlerConnectionWrapper;

		// Token: 0x04000037 RID: 55
		private byte[] defaultCalendarId;
	}
}
