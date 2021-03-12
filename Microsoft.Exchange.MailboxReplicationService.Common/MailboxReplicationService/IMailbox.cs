using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200013F RID: 319
	internal interface IMailbox : IDisposable
	{
		// Token: 0x06000A81 RID: 2689
		LatencyInfo GetLatencyInfo();

		// Token: 0x06000A82 RID: 2690
		SessionStatistics GetSessionStatistics(SessionStatisticsFlags statisticsTypes);

		// Token: 0x06000A83 RID: 2691
		bool IsConnected();

		// Token: 0x06000A84 RID: 2692
		void ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred);

		// Token: 0x06000A85 RID: 2693
		void ConfigPreferredADConnection(string preferredDomainControllerName);

		// Token: 0x06000A86 RID: 2694
		void Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid = null);

		// Token: 0x06000A87 RID: 2695
		void ConfigRestore(MailboxRestoreType restoreFlags);

		// Token: 0x06000A88 RID: 2696
		void ConfigMDBByName(string mdbName);

		// Token: 0x06000A89 RID: 2697
		void ConfigMailboxOptions(MailboxOptions options);

		// Token: 0x06000A8A RID: 2698
		void ConfigPst(string filePath, int? contentCodePage);

		// Token: 0x06000A8B RID: 2699
		void ConfigEas(NetworkCredential userCredential, SmtpAddress smtpAddress, Guid mailboxGuid, string remoteHostName = null);

		// Token: 0x06000A8C RID: 2700
		void ConfigOlc(OlcMailboxConfiguration config);

		// Token: 0x06000A8D RID: 2701
		MailboxInformation GetMailboxInformation();

		// Token: 0x06000A8E RID: 2702
		void Connect(MailboxConnectFlags connectFlags);

		// Token: 0x06000A8F RID: 2703
		bool IsCapabilitySupported(MRSProxyCapabilities capability);

		// Token: 0x06000A90 RID: 2704
		bool IsMailboxCapabilitySupported(MailboxCapabilities capability);

		// Token: 0x06000A91 RID: 2705
		void Disconnect();

		// Token: 0x06000A92 RID: 2706
		void SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported);

		// Token: 0x06000A93 RID: 2707
		void SeedMBICache();

		// Token: 0x06000A94 RID: 2708
		MailboxServerInformation GetMailboxServerInformation();

		// Token: 0x06000A95 RID: 2709
		VersionInformation GetVersion();

		// Token: 0x06000A96 RID: 2710
		void SetOtherSideVersion(VersionInformation otherSideVersion);

		// Token: 0x06000A97 RID: 2711
		List<FolderRec> EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad);

		// Token: 0x06000A98 RID: 2712
		List<WellKnownFolder> DiscoverWellKnownFolders(int flags);

		// Token: 0x06000A99 RID: 2713
		void DeleteMailbox(int flags);

		// Token: 0x06000A9A RID: 2714
		NamedPropData[] GetNamesFromIDs(PropTag[] pta);

		// Token: 0x06000A9B RID: 2715
		PropTag[] GetIDsFromNames(bool createIfNotExists, NamedPropData[] npa);

		// Token: 0x06000A9C RID: 2716
		byte[] GetSessionSpecificEntryId(byte[] entryId);

		// Token: 0x06000A9D RID: 2717
		MappedPrincipal[] ResolvePrincipals(MappedPrincipal[] principals);

		// Token: 0x06000A9E RID: 2718
		bool UpdateRemoteHostName(string value);

		// Token: 0x06000A9F RID: 2719
		ADUser GetADUser();

		// Token: 0x06000AA0 RID: 2720
		void UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId);

		// Token: 0x06000AA1 RID: 2721
		RawSecurityDescriptor GetMailboxSecurityDescriptor();

		// Token: 0x06000AA2 RID: 2722
		RawSecurityDescriptor GetUserSecurityDescriptor();

		// Token: 0x06000AA3 RID: 2723
		void AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength);

		// Token: 0x06000AA4 RID: 2724
		ServerHealthStatus CheckServerHealth();

		// Token: 0x06000AA5 RID: 2725
		PropValueData[] GetProps(PropTag[] ptags);

		// Token: 0x06000AA6 RID: 2726
		byte[] GetReceiveFolderEntryId(string msgClass);

		// Token: 0x06000AA7 RID: 2727
		Guid[] ResolvePolicyTag(string policyTagStr);

		// Token: 0x06000AA8 RID: 2728
		string LoadSyncState(byte[] key);

		// Token: 0x06000AA9 RID: 2729
		MessageRec SaveSyncState(byte[] key, string syncState);

		// Token: 0x06000AAA RID: 2730
		Guid StartIsInteg(List<uint> mailboxCorruptionTypes);

		// Token: 0x06000AAB RID: 2731
		List<StoreIntegrityCheckJob> QueryIsInteg(Guid isIntegRequestGuid);
	}
}
