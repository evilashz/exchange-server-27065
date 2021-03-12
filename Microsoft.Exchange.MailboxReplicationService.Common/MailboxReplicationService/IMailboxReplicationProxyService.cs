using System;
using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000020 RID: 32
	[ServiceContract(SessionMode = SessionMode.Required)]
	internal interface IMailboxReplicationProxyService
	{
		// Token: 0x060001CD RID: 461
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion);

		// Token: 0x060001CE RID: 462
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void CloseHandle(long handle);

		// Token: 0x060001CF RID: 463
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		ProxyServerInformation FindServerByDatabaseOrMailbox(string databaseId, Guid? physicalMailboxGuid, Guid? primaryMailboxGuid, byte[] tenantPartitionHintBytes);

		// Token: 0x060001D0 RID: 464
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		IEnumerable<ContainerMailboxInformation> GetMailboxContainerMailboxes(Guid mdbGuid, Guid primaryMailboxGuid);

		// Token: 0x060001D1 RID: 465
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		bool ArePublicFoldersReadyForMigrationCompletion();

		// Token: 0x060001D2 RID: 466
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		List<Guid> GetPublicFolderMailboxesExchangeGuids();

		// Token: 0x060001D3 RID: 467
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		DataExportBatch DataExport_ExportData2(long dataExportHandle);

		// Token: 0x060001D4 RID: 468
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void DataExport_CancelExport(long dataExportHandle);

		// Token: 0x060001D5 RID: 469
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDataImport_ImportBuffer(long dataImportHandle, int opcode, byte[] data);

		// Token: 0x060001D6 RID: 470
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDataImport_Flush(long dataImportHandle);

		// Token: 0x060001D7 RID: 471
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		FolderRec IFolder_GetFolderRec2(long folderHandle, int[] additionalPtagsToLoad);

		// Token: 0x060001D8 RID: 472
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		FolderRec IFolder_GetFolderRec3(long folderHandle, int[] additionalPtagsToLoad, int flags);

		// Token: 0x060001D9 RID: 473
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<MessageRec> IFolder_EnumerateMessagesPaged2(long folderHandle, EnumerateMessagesFlags emFlags, int[] additionalPtagsToLoad, out bool moreData);

		// Token: 0x060001DA RID: 474
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<MessageRec> IFolder_EnumerateMessagesNextBatch(long folderHandle, out bool moreData);

		// Token: 0x060001DB RID: 475
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		byte[] IFolder_GetSecurityDescriptor(long folderHandle, int secProp);

		// Token: 0x060001DC RID: 476
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IFolder_SetContentsRestriction(long folderHandle, RestrictionData restriction);

		// Token: 0x060001DD RID: 477
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		PropValueData[] IFolder_GetProps(long folderHandle, int[] pta);

		// Token: 0x060001DE RID: 478
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IFolder_DeleteMessages(long folderHandle, byte[][] entryIds);

		// Token: 0x060001DF RID: 479
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		RuleData[] IFolder_GetRules(long folderHandle, int[] extraProps);

		// Token: 0x060001E0 RID: 480
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropValueData[][] IFolder_GetACL(long folderHandle, int secProp);

		// Token: 0x060001E1 RID: 481
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropValueData[][] IFolder_GetExtendedAcl(long folderHandle, int aclFlags);

		// Token: 0x060001E2 RID: 482
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IFolder_GetSearchCriteria(long folderHandle, out RestrictionData restriction, out byte[][] entryIDs, out int searchState);

		// Token: 0x060001E3 RID: 483
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<MessageRec> IFolder_LookupMessages(long folderHandle, int ptagToLookup, byte[][] keysToLookup, int[] additionalPtagsToLoad);

		// Token: 0x060001E4 RID: 484
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropProblemData[] IFolder_SetProps(long folderHandle, PropValueData[] pva);

		// Token: 0x060001E5 RID: 485
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		PropValueData[] ISourceFolder_GetProps(long folderHandle, int[] pta);

		// Token: 0x060001E6 RID: 486
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void ISourceFolder_GetSearchCriteria(long folderHandle, out RestrictionData restriction, out byte[][] entryIDs, out int searchState);

		// Token: 0x060001E7 RID: 487
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		DataExportBatch ISourceFolder_CopyTo(long folderHandle, int flags, int[] excludeTags, byte[] targetObjectData);

		// Token: 0x060001E8 RID: 488
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		DataExportBatch ISourceFolder_Export2(long folderHandle, int[] excludeTags, byte[] targetObjectData);

		// Token: 0x060001E9 RID: 489
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		DataExportBatch ISourceFolder_ExportMessages(long folderHandle, int flags, byte[][] entryIds, byte[] targetObjectData);

		// Token: 0x060001EA RID: 490
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		FolderChangesManifest ISourceFolder_EnumerateChanges(long folderHandle, bool catchup);

		// Token: 0x060001EB RID: 491
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		FolderChangesManifest ISourceFolder_EnumerateChanges2(long folderHandle, int flags, int maxChanges);

		// Token: 0x060001EC RID: 492
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		List<MessageRec> ISourceFolder_EnumerateMessagesPaged(long folderHandle, int maxPageSize);

		// Token: 0x060001ED RID: 493
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		int ISourceFolder_GetEstimatedItemCount(long folderHandle);

		// Token: 0x060001EE RID: 494
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropProblemData[] IDestinationFolder_SetProps(long folderHandle, PropValueData[] pva);

		// Token: 0x060001EF RID: 495
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropProblemData[] IDestinationFolder_SetSecurityDescriptor(long folderHandle, int secProp, byte[] sdData);

		// Token: 0x060001F0 RID: 496
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		bool IDestinationFolder_SetSearchCriteria(long folderHandle, RestrictionData restriction, byte[][] entryIDs, int searchFlags);

		// Token: 0x060001F1 RID: 497
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		long IDestinationFolder_GetFxProxy(long folderHandle, out byte[] objectData);

		// Token: 0x060001F2 RID: 498
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		long IDestinationFolder_GetFxProxy2(long folderHandle, int flags, out byte[] objectData);

		// Token: 0x060001F3 RID: 499
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationFolder_DeleteMessages(long folderHandle, byte[][] entryIds);

		// Token: 0x060001F4 RID: 500
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationFolder_SetReadFlagsOnMessages(long folderHandle, int flags, byte[][] entryIds);

		// Token: 0x060001F5 RID: 501
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationFolder_SetMessageProps(long folderHandle, byte[] entryId, PropValueData[] propValues);

		// Token: 0x060001F6 RID: 502
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationFolder_SetRules(long folderHandle, RuleData[] rules);

		// Token: 0x060001F7 RID: 503
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationFolder_SetACL(long folderHandle, int secProp, PropValueData[][] aclData);

		// Token: 0x060001F8 RID: 504
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationFolder_SetExtendedAcl(long folderHandle, int aclFlags, PropValueData[][] aclData);

		// Token: 0x060001F9 RID: 505
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		Guid IDestinationFolder_LinkMailPublicFolder(long folderHandle, LinkMailPublicFolderFlags flags, byte[] objectId);

		// Token: 0x060001FA RID: 506
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		Guid IReservationManager_ReserveResources(Guid mailboxGuid, byte[] partitionHintBytes, Guid mdbGuid, int flags);

		// Token: 0x060001FB RID: 507
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IReservationManager_ReleaseResources(Guid reservationId);

		// Token: 0x060001FC RID: 508
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		int IMailbox_ReserveResources(Guid reservationId, Guid resourceId, int reservationType);

		// Token: 0x060001FD RID: 509
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IMailbox_Config3(Guid primaryMailboxGuid, Guid physicalMailboxGuid, Guid mdbGuid, string mdbName, [MessageParameter(Name = "options")] MailboxType mbxType, int proxyControlFlags);

		// Token: 0x060001FE RID: 510
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IMailbox_Config4(Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags);

		// Token: 0x060001FF RID: 511
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		long IMailbox_Config5(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags);

		// Token: 0x06000200 RID: 512
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IMailbox_Config6(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, string filePath, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags);

		// Token: 0x06000201 RID: 513
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IMailbox_Config7(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags, Guid? mailboxContainerGuid);

		// Token: 0x06000202 RID: 514
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigureProxyService(ProxyConfiguration configuration);

		// Token: 0x06000203 RID: 515
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigADConnection(long mailboxHandle, string domainControllerName, string userName, string userDomain, string userPassword);

		// Token: 0x06000204 RID: 516
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_ConfigEas(long mailboxHandle, string password, string address);

		// Token: 0x06000205 RID: 517
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigEas2(long mailboxHandle, string password, string address, Guid mailboxGuid, string remoteHostName);

		// Token: 0x06000206 RID: 518
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigPreferredADConnection(long mailboxHandle, string preferredDomainControllerName);

		// Token: 0x06000207 RID: 519
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_ConfigPst(long mailboxHandle, string filePath, int? contentCodePage);

		// Token: 0x06000208 RID: 520
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigRestore(long mailboxHandle, int restoreFlags);

		// Token: 0x06000209 RID: 521
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigOlc(long mailboxHandle, OlcMailboxConfiguration config);

		// Token: 0x0600020A RID: 522
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		MailboxInformation IMailbox_GetMailboxInformation(long mailboxHandle);

		// Token: 0x0600020B RID: 523
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_Connect(long mailboxHandle);

		// Token: 0x0600020C RID: 524
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_Connect2(long mailboxHandle, int connectFlags);

		// Token: 0x0600020D RID: 525
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_Disconnect(long mailboxHandle);

		// Token: 0x0600020E RID: 526
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_ConfigMailboxOptions(long mailboxHandle, int options);

		// Token: 0x0600020F RID: 527
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MailboxServerInformation IMailbox_GetMailboxServerInformation(long mailboxHandle);

		// Token: 0x06000210 RID: 528
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_SetOtherSideVersion(long mailboxHandle, VersionInformation otherSideInfo);

		// Token: 0x06000211 RID: 529
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_SetInTransitStatus(long mailboxHandle, int status, out bool onlineMoveSupported);

		// Token: 0x06000212 RID: 530
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_SeedMBICache(long mailboxHandle);

		// Token: 0x06000213 RID: 531
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<FolderRec> IMailbox_EnumerateFolderHierarchyPaged2(long mailboxHandle, EnumerateFolderHierarchyFlags flags, int[] additionalPtagsToLoad, out bool moreData);

		// Token: 0x06000214 RID: 532
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<FolderRec> IMailbox_EnumerateFolderHierarchyNextBatch(long mailboxHandle, out bool moreData);

		// Token: 0x06000215 RID: 533
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		List<WellKnownFolder> IMailbox_DiscoverWellKnownFolders(long mailboxHandle, int flags);

		// Token: 0x06000216 RID: 534
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		bool IMailbox_IsMailboxCapabilitySupported(long mailboxHandle, MailboxCapabilities capability);

		// Token: 0x06000217 RID: 535
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		bool IMailbox_IsMailboxCapabilitySupported2(long mailboxHandle, int capability);

		// Token: 0x06000218 RID: 536
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_DeleteMailbox(long mailboxHandle, int flags);

		// Token: 0x06000219 RID: 537
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		NamedPropData[] IMailbox_GetNamesFromIDs(long mailboxHandle, int[] pta);

		// Token: 0x0600021A RID: 538
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		int[] IMailbox_GetIDsFromNames(long mailboxHandle, bool createIfNotExists, NamedPropData[] npa);

		// Token: 0x0600021B RID: 539
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		byte[] IMailbox_GetSessionSpecificEntryId(long mailboxHandle, byte[] entryId);

		// Token: 0x0600021C RID: 540
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		bool IMailbox_UpdateRemoteHostName(long mailboxHandle, string value);

		// Token: 0x0600021D RID: 541
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		string IMailbox_GetADUser(long mailboxHandle);

		// Token: 0x0600021E RID: 542
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_UpdateMovedMailbox(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries);

		// Token: 0x0600021F RID: 543
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IMailbox_UpdateMovedMailbox2(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus);

		// Token: 0x06000220 RID: 544
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_UpdateMovedMailbox3(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus, int updateMovedMailboxFlags);

		// Token: 0x06000221 RID: 545
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_UpdateMovedMailbox4(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus, int updateMovedMailboxFlags, Guid? newMailboxContainerGuid, byte[] newUnifiedMailboxIdData);

		// Token: 0x06000222 RID: 546
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MappedPrincipal[] IMailbox_GetPrincipalsFromMailboxGuids(long mailboxHandle, Guid[] mailboxGuids);

		// Token: 0x06000223 RID: 547
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		Guid[] IMailbox_GetMailboxGuidsFromPrincipals(long mailboxHandle, MappedPrincipal[] principals);

		// Token: 0x06000224 RID: 548
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		MappedPrincipal[] IMailbox_ResolvePrincipals(long mailboxHandle, MappedPrincipal[] principals);

		// Token: 0x06000225 RID: 549
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		byte[] IMailbox_GetMailboxSecurityDescriptor(long mailboxHandle);

		// Token: 0x06000226 RID: 550
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		byte[] IMailbox_GetUserSecurityDescriptor(long mailboxHandle);

		// Token: 0x06000227 RID: 551
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_AddMoveHistoryEntry(long mailboxHandle, string mheData, int maxMoveHistoryLength);

		// Token: 0x06000228 RID: 552
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IMailbox_CheckServerHealth(long mailboxHandle);

		// Token: 0x06000229 RID: 553
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		ServerHealthStatus IMailbox_CheckServerHealth2(long mailboxHandle);

		// Token: 0x0600022A RID: 554
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropValueData[] IMailbox_GetProps(long mailboxHandle, int[] ptags);

		// Token: 0x0600022B RID: 555
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		byte[] IMailbox_GetReceiveFolderEntryId(long mailboxHandle, string msgClass);

		// Token: 0x0600022C RID: 556
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		SessionStatistics IMailbox_GetSessionStatistics(long mailboxHandle, int statisticsTypes);

		// Token: 0x0600022D RID: 557
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		Guid IMailbox_StartIsInteg(long mailboxHandle, List<uint> mailboxCorruptionTypes);

		// Token: 0x0600022E RID: 558
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<StoreIntegrityCheckJob> IMailbox_QueryIsInteg(long mailboxHandle, Guid isIntegRequestGuid);

		// Token: 0x0600022F RID: 559
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		byte[] ISourceMailbox_GetMailboxBasicInfo(long mailboxHandle);

		// Token: 0x06000230 RID: 560
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		byte[] ISourceMailbox_GetMailboxBasicInfo2(long mailboxHandle, int signatureFlags);

		// Token: 0x06000231 RID: 561
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		long ISourceMailbox_GetFolder(long mailboxHandle, byte[] entryId);

		// Token: 0x06000232 RID: 562
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		PropValueData[] ISourceMailbox_GetProps(long mailboxHandle, int[] ptags);

		// Token: 0x06000233 RID: 563
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		DataExportBatch ISourceMailbox_Export2(long mailboxHandle, int[] excludeProps, byte[] targetObjectData);

		// Token: 0x06000234 RID: 564
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		MailboxChangesManifest ISourceMailbox_EnumerateHierarchyChanges(long mailboxHandle, bool catchup);

		// Token: 0x06000235 RID: 565
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MailboxChangesManifest ISourceMailbox_EnumerateHierarchyChanges2(long mailboxHandle, int flags, int maxChanges);

		// Token: 0x06000236 RID: 566
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		DataExportBatch ISourceMailbox_GetMailboxSyncState(long mailboxHandle);

		// Token: 0x06000237 RID: 567
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long ISourceMailbox_SetMailboxSyncState(long mailboxHandle, DataExportBatch firstBatch);

		// Token: 0x06000238 RID: 568
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		DataExportBatch ISourceMailbox_ExportMessageBatch2(long mailboxHandle, List<MessageRec> messages, byte[] targetObjectData);

		// Token: 0x06000239 RID: 569
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		DataExportBatch ISourceMailbox_ExportMessages(long mailboxHandle, List<MessageRec> messages, int flags, int[] excludeProps, byte[] targetObjectData);

		// Token: 0x0600023A RID: 570
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		DataExportBatch ISourceMailbox_ExportFolders(long mailboxHandle, List<byte[]> folderIds, int exportFoldersDataToCopyFlags, int folderRecFlags, int[] additionalFolderRecProps, int copyPropertiesFlags, int[] excludeProps, int extendedAclFlags);

		// Token: 0x0600023B RID: 571
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<ReplayActionResult> ISourceMailbox_ReplayActions(long mailboxHandle, List<ReplayAction> actions);

		// Token: 0x0600023C RID: 572
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		List<ItemPropertiesBase> ISourceMailbox_GetMailboxSettings(long mailboxHandle, int flags);

		// Token: 0x0600023D RID: 573
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		bool IDestinationMailbox_MailboxExists(long mailboxHandle);

		// Token: 0x0600023E RID: 574
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		CreateMailboxResult IDestinationMailbox_CreateMailbox(long mailboxHandle, byte[] mailboxData);

		// Token: 0x0600023F RID: 575
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		CreateMailboxResult IDestinationMailbox_CreateMailbox2(long mailboxHandle, byte[] mailboxData, int sourceSignatureFlags);

		// Token: 0x06000240 RID: 576
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationMailbox_ProcessMailboxSignature(long mailboxHandle, byte[] mailboxData);

		// Token: 0x06000241 RID: 577
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		DataExportBatch IDestinationMailbox_LoadSyncState2(long mailboxHandle, byte[] key);

		// Token: 0x06000242 RID: 578
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IDestinationMailbox_SaveSyncState2(long mailboxHandle, byte[] key, DataExportBatch firstBatch);

		// Token: 0x06000243 RID: 579
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IDestinationMailbox_GetFolder(long mailboxHandle, byte[] entryId);

		// Token: 0x06000244 RID: 580
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		long IDestinationMailbox_GetFxProxy(long mailboxHandle, out byte[] objectData);

		// Token: 0x06000245 RID: 581
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		PropProblemData[] IDestinationMailbox_SetProps(long mailboxHandle, PropValueData[] pva);

		// Token: 0x06000246 RID: 582
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		long IDestinationMailbox_GetFxProxyPool(long mailboxHandle, byte[][] folderIds, out byte[] objectData);

		// Token: 0x06000247 RID: 583
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationMailbox_CreateFolder(long mailboxHandle, FolderRec sourceFolder, bool failIfExists);

		// Token: 0x06000248 RID: 584
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationMailbox_CreateFolder2(long mailboxHandle, FolderRec folderRec, bool failIfExists, out byte[] newFolderId);

		// Token: 0x06000249 RID: 585
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationMailbox_CreateFolder3(long mailboxHandle, FolderRec folderRec, int createFolderFlags, out byte[] newFolderId);

		// Token: 0x0600024A RID: 586
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationMailbox_MoveFolder(long mailboxHandle, byte[] folderId, byte[] oldParentId, byte[] newParentId);

		// Token: 0x0600024B RID: 587
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationMailbox_DeleteFolder(long mailboxHandle, FolderRec folderRec);

		// Token: 0x0600024C RID: 588
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void IDestinationMailbox_SetMailboxSecurityDescriptor(long mailboxHandle, byte[] sdData);

		// Token: 0x0600024D RID: 589
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationMailbox_SetUserSecurityDescriptor(long mailboxHandle, byte[] sdData);

		// Token: 0x0600024E RID: 590
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationMailbox_PreFinalSyncDataProcessing(long mailboxHandle, int? sourceMailboxVersion);

		// Token: 0x0600024F RID: 591
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		int IDestinationMailbox_CheckDataGuarantee(long mailboxHandle, DateTime commitTimestamp, out byte[] failureReasonData);

		// Token: 0x06000250 RID: 592
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationMailbox_ForceLogRoll(long mailboxHandle);

		// Token: 0x06000251 RID: 593
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		List<ReplayAction> IDestinationMailbox_GetActions(long mailboxHandle, string replaySyncState, int maxNumberOfActions);

		// Token: 0x06000252 RID: 594
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void IDestinationMailbox_SetMailboxSettings(long mailboxHandle, ItemPropertiesBase item);

		// Token: 0x06000253 RID: 595
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MigrationAccount[] SelectAccountsToMigrate(long maximumAccounts, long? maximumTotalSize, int? constraintId);
	}
}
