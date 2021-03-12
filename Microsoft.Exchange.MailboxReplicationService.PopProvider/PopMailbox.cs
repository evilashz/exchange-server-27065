using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Pop;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal abstract class PopMailbox : MailboxProviderBase, IMailbox, IDisposable
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000259C File Offset: 0x0000079C
		public PopMailbox(Pop3Connection popConnection) : base(LocalMailboxFlags.None)
		{
			this.PopConnection = popConnection;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025C4 File Offset: 0x000007C4
		public PopMailbox(ConnectionParameters connectionParameters, Pop3AuthenticationParameters authenticationParameters, Pop3ServerParameters serverParameters, SmtpServerParameters smtpParameters) : base(LocalMailboxFlags.None)
		{
			this.ConnectionParameters = connectionParameters;
			this.AuthenticationParameters = authenticationParameters;
			this.ServerParameters = serverParameters;
			this.SmtpParameters = smtpParameters;
			this.PopConnection = Pop3Connection.CreateInstance(connectionParameters);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002617 File Offset: 0x00000817
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000261E File Offset: 0x0000081E
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002625 File Offset: 0x00000825
		internal Dictionary<string, int> UniqueIdMap
		{
			get
			{
				return this.uniqueIdMap;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000262D File Offset: 0x0000082D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002635 File Offset: 0x00000835
		internal IPop3Connection PopConnection { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000263E File Offset: 0x0000083E
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002646 File Offset: 0x00000846
		private protected Pop3ServerParameters ServerParameters { protected get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000264F File Offset: 0x0000084F
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002657 File Offset: 0x00000857
		private protected SmtpServerParameters SmtpParameters { protected get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002668 File Offset: 0x00000868
		private protected Pop3AuthenticationParameters AuthenticationParameters { protected get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002679 File Offset: 0x00000879
		private protected ConnectionParameters ConnectionParameters { protected get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002682 File Offset: 0x00000882
		public override SyncProtocol GetSyncProtocol()
		{
			return SyncProtocol.Pop;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002685 File Offset: 0x00000885
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("PopMailbox.IMailbox.Config", new object[0]);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026A2 File Offset: 0x000008A2
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026A9 File Offset: 0x000008A9
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026B0 File Offset: 0x000008B0
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026B7 File Offset: 0x000008B7
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026B9 File Offset: 0x000008B9
		bool IMailbox.IsConnected()
		{
			MrsTracer.Provider.Function("PopMailbox.IMailbox.IsConnected", new object[0]);
			return this.PopConnection.ConnectionContext.Client != null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026E5 File Offset: 0x000008E5
		bool IMailbox.IsCapabilitySupported(MRSProxyCapabilities capability)
		{
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026E8 File Offset: 0x000008E8
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026EC File Offset: 0x000008EC
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.Provider.Function("PopMailbox.IMailbox.GetMailboxInformation", new object[0]);
			return new MailboxInformation
			{
				ProviderName = "PopProvider"
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002720 File Offset: 0x00000920
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("PopMailbox.IMailbox.Connect", new object[0]);
			this.PopConnection.ConnectAndAuthenticate(this.ServerParameters, this.AuthenticationParameters);
			this.AfterConnect();
			MrsTracer.Provider.Debug("PopClient.Connect: Pop Connection, Authentication and Capabilities check succeeded", new object[0]);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000277C File Offset: 0x0000097C
		void IMailbox.Disconnect()
		{
			base.CheckDisposed();
			MrsTracer.Provider.Function("PopMailbox.IMailbox.Disconnect", new object[0]);
			lock (this.syncRoot)
			{
				if (this.PopConnection != null)
				{
					this.PopConnection.Dispose();
					this.PopConnection = null;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027EC File Offset: 0x000009EC
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			MrsTracer.Provider.Function("PopMailbox.IMailbox.GetMailboxServerInformation", new object[0]);
			return new MailboxServerInformation
			{
				MailboxServerName = this.PopConnection.ConnectionContext.Server
			};
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000282B File Offset: 0x00000A2B
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002832 File Offset: 0x00000A32
		void IMailbox.SeedMBICache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000283C File Offset: 0x00000A3C
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("PopMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			return PopMailbox.folderHierarchy;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000286E File Offset: 0x00000A6E
		void IMailbox.DeleteMailbox(int flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002875 File Offset: 0x00000A75
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000287C File Offset: 0x00000A7C
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			MrsTracer.Provider.Function("ImapMailbox.IMailbox.GetIDsFromNames", new object[0]);
			if (createIfNotExists)
			{
				throw new GetIdsFromNamesCalledOnDestinationException();
			}
			return SyncEmailUtils.GetIDsFromNames(npda, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028A3 File Offset: 0x00000AA3
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028AA File Offset: 0x00000AAA
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028B1 File Offset: 0x00000AB1
		ADUser IMailbox.GetADUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028B8 File Offset: 0x00000AB8
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid targetDatabaseGuid, Guid targetArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028BF File Offset: 0x00000ABF
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			return PopMailbox.wellKnownFolders;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028C6 File Offset: 0x00000AC6
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028CD File Offset: 0x00000ACD
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000028D4 File Offset: 0x00000AD4
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028DB File Offset: 0x00000ADB
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000028E2 File Offset: 0x00000AE2
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000028E9 File Offset: 0x00000AE9
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			MrsTracer.Provider.Function("PopMailbox.IMailbox.CheckServerHealth", new object[0]);
			return ServerHealthStatus.Healthy;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002905 File Offset: 0x00000B05
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000290C File Offset: 0x00000B0C
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002914 File Offset: 0x00000B14
		public FolderRec GetFolderRec(byte[] folderId)
		{
			foreach (FolderRec folderRec in PopMailbox.folderHierarchy)
			{
				if (folderRec.EntryId == folderId)
				{
					return folderRec;
				}
			}
			return null;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002970 File Offset: 0x00000B70
		public T GetFolder<T>(byte[] folderId) where T : PopFolder, new()
		{
			MrsTracer.Provider.Function("PopMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(folderId)
			});
			T result = Activator.CreateInstance<T>();
			result.Config(folderId, this);
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029B3 File Offset: 0x00000BB3
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PopMailbox>(this);
		}

		// Token: 0x0400000D RID: 13
		private const string IpmSubtreeDisplayName = "Top of Information Store";

		// Token: 0x0400000E RID: 14
		private const string ProviderName = "PopProvider";

		// Token: 0x0400000F RID: 15
		internal static readonly string IpmSubtree = WellKnownFolderType.IpmSubtree.ToString();

		// Token: 0x04000010 RID: 16
		internal static readonly string Inbox = WellKnownFolderType.Inbox.ToString();

		// Token: 0x04000011 RID: 17
		internal static readonly byte[] IpmSubtreeEntryId = PopEntryId.CreateFolderEntryId(PopMailbox.IpmSubtree);

		// Token: 0x04000012 RID: 18
		internal static readonly byte[] InboxEntryId = PopEntryId.CreateFolderEntryId(PopMailbox.Inbox);

		// Token: 0x04000013 RID: 19
		private static readonly string Root = WellKnownFolderType.Root.ToString();

		// Token: 0x04000014 RID: 20
		private static readonly byte[] RootEntryId = PopEntryId.CreateFolderEntryId(PopMailbox.Root);

		// Token: 0x04000015 RID: 21
		private static readonly List<FolderRec> folderHierarchy = new List<FolderRec>
		{
			new FolderRec(PopMailbox.RootEntryId, null, FolderType.Root, string.Empty, DateTime.MinValue, null),
			new FolderRec(PopMailbox.IpmSubtreeEntryId, PopMailbox.RootEntryId, FolderType.Generic, "Top of Information Store", DateTime.MinValue, null),
			new FolderRec(PopMailbox.InboxEntryId, PopMailbox.IpmSubtreeEntryId, FolderType.Generic, PopMailbox.Inbox, DateTime.MinValue, null)
		};

		// Token: 0x04000016 RID: 22
		private static readonly List<WellKnownFolder> wellKnownFolders = new List<WellKnownFolder>
		{
			new WellKnownFolder(3, PopMailbox.IpmSubtreeEntryId),
			new WellKnownFolder(10, PopMailbox.InboxEntryId)
		};

		// Token: 0x04000017 RID: 23
		private readonly object syncRoot = new object();

		// Token: 0x04000018 RID: 24
		private Dictionary<string, int> uniqueIdMap = new Dictionary<string, int>();
	}
}
