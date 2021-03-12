using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Imap;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	internal abstract class ImapMailbox : MailboxProviderBase, IMailbox, IDisposable
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003382 File Offset: 0x00001582
		public ImapMailbox(ImapConnection imapConnection) : base(LocalMailboxFlags.None)
		{
			this.ImapConnection = imapConnection;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000339D File Offset: 0x0000159D
		public ImapMailbox(ConnectionParameters connectionParameters, ImapAuthenticationParameters authenticationParameters, ImapServerParameters serverParameters, SmtpServerParameters smtpParameters) : base(LocalMailboxFlags.None)
		{
			this.ConnectionParameters = connectionParameters;
			this.AuthenticationParameters = authenticationParameters;
			this.ServerParameters = serverParameters;
			this.SmtpParameters = smtpParameters;
			this.ImapConnection = ImapConnection.CreateInstance(connectionParameters);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000033DA File Offset: 0x000015DA
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000033E1 File Offset: 0x000015E1
		public override int ServerVersion
		{
			get
			{
				throw new NotImplementedException();
			}
			protected set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000033E8 File Offset: 0x000015E8
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000033F0 File Offset: 0x000015F0
		internal ImapConnection ImapConnection { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000033F9 File Offset: 0x000015F9
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003401 File Offset: 0x00001601
		private protected ImapServerParameters ServerParameters { protected get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000340A File Offset: 0x0000160A
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003412 File Offset: 0x00001612
		private protected SmtpServerParameters SmtpParameters { protected get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000341B File Offset: 0x0000161B
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003423 File Offset: 0x00001623
		private protected ImapAuthenticationParameters AuthenticationParameters { protected get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000342C File Offset: 0x0000162C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003434 File Offset: 0x00001634
		private protected ConnectionParameters ConnectionParameters { protected get; private set; }

		// Token: 0x06000079 RID: 121 RVA: 0x0000343D File Offset: 0x0000163D
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003444 File Offset: 0x00001644
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000344B File Offset: 0x0000164B
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003452 File Offset: 0x00001652
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003459 File Offset: 0x00001659
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000345B File Offset: 0x0000165B
		bool IMailbox.IsConnected()
		{
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.IsConnected", new object[0]);
			return this.ImapConnection.IsConnected();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003480 File Offset: 0x00001680
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			return capability == MailboxCapabilities.ReplayActions;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000349C File Offset: 0x0000169C
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.GetMailboxInformation", new object[0]);
			return new MailboxInformation
			{
				ProviderName = "ImapProvider"
			};
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034D0 File Offset: 0x000016D0
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.Connect", new object[0]);
			IServerCapabilities capabilities = new ImapServerCapabilities().Add("IMAP4REV1");
			this.ImapConnection.ConnectAndAuthenticate(this.ServerParameters, this.AuthenticationParameters, capabilities);
			this.AfterConnect();
			MrsTracer.Provider.Debug("ImapClient.Connect: Imap Connection, Authentication and Capabilities check succeeded", new object[0]);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000353C File Offset: 0x0000173C
		void IMailbox.Disconnect()
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.Disconnect", new object[0]);
			lock (this.syncRoot)
			{
				if (this.ImapConnection != null)
				{
					if (this.ImapConnection.IsConnected())
					{
						this.ImapConnection.LogOff();
					}
					this.ImapConnection.Dispose();
					this.ImapConnection = null;
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000035C4 File Offset: 0x000017C4
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.GetMailboxServerInformation", new object[0]);
			return new MailboxServerInformation
			{
				MailboxServerName = this.ImapConnection.ConnectionContext.Server
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003603 File Offset: 0x00001803
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000360A File Offset: 0x0000180A
		void IMailbox.SeedMBICache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003614 File Offset: 0x00001814
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("ImapMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			List<FolderRec> list = null;
			List<ImapClientFolder> list2 = this.EnumerateFolderHierarchy();
			list2.Add(new ImapClientFolder(ImapMailbox.Root)
			{
				IsSelectable = false
			});
			list2.Add(new ImapClientFolder(ImapMailbox.IpmSubtree)
			{
				IsSelectable = false
			});
			ImapClientFolder.FindWellKnownFolders(list2);
			this.folderCache = new EntryIdMap<ImapClientFolder>();
			list = new List<FolderRec>(list2.Count);
			foreach (ImapClientFolder imapClientFolder in list2)
			{
				FolderRec folderRec = this.CreateFolderRec(imapClientFolder);
				list.Add(folderRec);
				this.folderCache.Add(folderRec.EntryId, imapClientFolder);
			}
			return list;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003708 File Offset: 0x00001908
		void IMailbox.DeleteMailbox(int flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000370F File Offset: 0x0000190F
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003716 File Offset: 0x00001916
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.GetIDsFromNames", new object[0]);
			if (createIfNotExists)
			{
				throw new GetIdsFromNamesCalledOnDestinationException();
			}
			return SyncEmailUtils.GetIDsFromNames(npda, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000373D File Offset: 0x0000193D
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003744 File Offset: 0x00001944
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000374B File Offset: 0x0000194B
		ADUser IMailbox.GetADUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003752 File Offset: 0x00001952
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid targetDatabaseGuid, Guid targetArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000375C File Offset: 0x0000195C
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			List<WellKnownFolder> list = new List<WellKnownFolder>();
			list.Add(new WellKnownFolder(3, ImapMailbox.IpmSubtreeEntryId));
			foreach (ImapClientFolder imapClientFolder in this.folderCache.Values)
			{
				if (imapClientFolder.WellKnownFolderType != WellKnownFolderType.None)
				{
					WellKnownFolderType wellKnownFolderType = imapClientFolder.WellKnownFolderType;
					byte[] entryId = ImapEntryId.CreateFolderEntryId(imapClientFolder.Name);
					list.Add(new WellKnownFolder((int)wellKnownFolderType, entryId));
				}
			}
			return list;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000037F0 File Offset: 0x000019F0
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037F7 File Offset: 0x000019F7
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000037FE File Offset: 0x000019FE
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003805 File Offset: 0x00001A05
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000380C File Offset: 0x00001A0C
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003813 File Offset: 0x00001A13
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.CheckServerHealth", new object[0]);
			return new ServerHealthStatus(ServerHealthState.Healthy);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003830 File Offset: 0x00001A30
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			MrsTracer.Provider.Function("ImapMailbox.GetProps", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			PropValueData[] array = new PropValueData[ptags.Length];
			for (int i = 0; i < ptags.Length; i++)
			{
				PropTag propTag = ptags[i];
				propTag = propTag.ChangePropType(PropType.Null);
				array[i] = new PropValueData(propTag, null);
			}
			return array;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003887 File Offset: 0x00001A87
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000388E File Offset: 0x00001A8E
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			return new SessionStatistics();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003895 File Offset: 0x00001A95
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000389C File Offset: 0x00001A9C
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000038A4 File Offset: 0x00001AA4
		public T GetFolder<T>(byte[] folderId) where T : ImapFolder, new()
		{
			MrsTracer.Provider.Function("ImapMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(folderId)
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			ImapClientFolder folder;
			if (!this.folderCache.TryGetValue(folderId, out folder))
			{
				MrsTracer.Provider.Debug("Folder with entryId {0} does not exist", new object[]
				{
					folderId
				});
				return default(T);
			}
			T result = Activator.CreateInstance<T>();
			result.Config(folderId, folder, this);
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003928 File Offset: 0x00001B28
		public FolderRec CreateFolderRec(ImapClientFolder folder)
		{
			if (folder.Name.Equals(ImapMailbox.Root))
			{
				return new FolderRec(ImapMailbox.RootEntryId, null, FolderType.Root, string.Empty, DateTime.MinValue, null);
			}
			if (folder.Name.Equals(ImapMailbox.IpmSubtree))
			{
				return new FolderRec(ImapMailbox.IpmSubtreeEntryId, ImapMailbox.RootEntryId, FolderType.Generic, "Top of Information Store", DateTime.MinValue, null);
			}
			byte[] entryId = ImapEntryId.CreateFolderEntryId(folder.Name);
			string parentFolderPath = folder.ParentFolderPath;
			if (string.IsNullOrEmpty(parentFolderPath))
			{
				return new FolderRec(entryId, ImapMailbox.IpmSubtreeEntryId, FolderType.Generic, folder.ShortFolderName, DateTime.MinValue, null);
			}
			return new FolderRec(entryId, ImapEntryId.CreateFolderEntryId(parentFolderPath), FolderType.Generic, folder.ShortFolderName, DateTime.MinValue, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000039DA File Offset: 0x00001BDA
		internal void SetImapConnectionFromTestOnly(ImapConnection imapConnection)
		{
			this.ImapConnection = imapConnection;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000039E3 File Offset: 0x00001BE3
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039F5 File Offset: 0x00001BF5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ImapMailbox>(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A00 File Offset: 0x00001C00
		private List<ImapClientFolder> EnumerateFolderHierarchy()
		{
			ImapMailbox.EnsureDefaultFolderMappingTable(this.ConnectionParameters.Log);
			List<ImapClientFolder> list = new List<ImapClientFolder>();
			int num = 0;
			int num2 = 0;
			while (num2 < 2 && num < 20)
			{
				num++;
				char latestSeparatorCharacter = this.GetLatestSeparatorCharacter(list);
				IList<ImapMailbox> list2 = this.ImapConnection.ListImapMailboxesByLevel(num, latestSeparatorCharacter);
				if (list2.Count == 0)
				{
					num2++;
				}
				else
				{
					num2 = 0;
					MrsTracer.Provider.Debug("Number of folders: {0}", new object[]
					{
						list2.Count
					});
					List<ImapMailbox> list3 = new List<ImapMailbox>(list2.Count);
					list3.AddRange(list2);
					foreach (ImapMailbox imapMailbox in list3)
					{
						if (imapMailbox.IsSelectable)
						{
							ImapMailbox folder = this.ImapConnection.SelectImapMailbox(imapMailbox);
							list.Add(new ImapClientFolder(folder));
						}
						else
						{
							list.Add(new ImapClientFolder(imapMailbox));
						}
						if (!string.Equals(imapMailbox.Name, "INBOX", StringComparison.OrdinalIgnoreCase) && imapMailbox.Separator != null)
						{
							this.inboxFolderHierarchySeparator = new char?(imapMailbox.Separator.Value);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B60 File Offset: 0x00001D60
		private char GetLatestSeparatorCharacter(IList<ImapClientFolder> folders)
		{
			if (folders.Count > 0 && folders[folders.Count - 1].Separator != null)
			{
				return folders[folders.Count - 1].Separator.Value;
			}
			if (this.inboxFolderHierarchySeparator == null)
			{
				return '/';
			}
			return this.inboxFolderHierarchySeparator.Value;
		}

		// Token: 0x04000029 RID: 41
		private const string IpmSubtreeDisplayName = "Top of Information Store";

		// Token: 0x0400002A RID: 42
		private const string ProviderName = "ImapProvider";

		// Token: 0x0400002B RID: 43
		private const int NumLevelsStopHierarchyEnumeration = 2;

		// Token: 0x0400002C RID: 44
		internal static readonly int ImapTimeout = 10800000;

		// Token: 0x0400002D RID: 45
		private static readonly string Root = WellKnownFolderType.Root.ToString();

		// Token: 0x0400002E RID: 46
		private static readonly byte[] RootEntryId = ImapEntryId.CreateFolderEntryId(ImapMailbox.Root);

		// Token: 0x0400002F RID: 47
		private static readonly string IpmSubtree = WellKnownFolderType.IpmSubtree.ToString();

		// Token: 0x04000030 RID: 48
		private static readonly byte[] IpmSubtreeEntryId = ImapEntryId.CreateFolderEntryId(ImapMailbox.IpmSubtree);

		// Token: 0x04000031 RID: 49
		private readonly object syncRoot = new object();

		// Token: 0x04000032 RID: 50
		private char? inboxFolderHierarchySeparator;

		// Token: 0x04000033 RID: 51
		private EntryIdMap<ImapClientFolder> folderCache;
	}
}
