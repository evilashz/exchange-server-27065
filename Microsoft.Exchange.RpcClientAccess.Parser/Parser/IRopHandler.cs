using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B5 RID: 437
	internal interface IRopHandler : IDisposable
	{
		// Token: 0x060008B1 RID: 2225
		RopResult Abort(IServerObject serverObject, AbortResultFactory resultFactory);

		// Token: 0x060008B2 RID: 2226
		RopResult AbortSubmit(IServerObject serverObject, StoreId folderId, StoreId messageId, AbortSubmitResultFactory resultFactory);

		// Token: 0x060008B3 RID: 2227
		RopResult AddressTypes(IServerObject serverObject, AddressTypesResultFactory resultFactory);

		// Token: 0x060008B4 RID: 2228
		RopResult CloneStream(IServerObject serverObject, CloneStreamResultFactory resultFactory);

		// Token: 0x060008B5 RID: 2229
		RopResult CollapseRow(IServerObject serverObject, StoreId categoryId, CollapseRowResultFactory resultFactory);

		// Token: 0x060008B6 RID: 2230
		RopResult CommitStream(IServerObject serverObject, CommitStreamResultFactory resultFactory);

		// Token: 0x060008B7 RID: 2231
		RopResult CopyFolder(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, bool recurse, StoreId sourceSubFolderId, string destinationSubFolderName, CopyFolderResultFactory resultFactory);

		// Token: 0x060008B8 RID: 2232
		RopResult CopyProperties(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] propertyTags, CopyPropertiesResultFactory resultFactory);

		// Token: 0x060008B9 RID: 2233
		RopResult CopyTo(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToResultFactory resultFactory);

		// Token: 0x060008BA RID: 2234
		RopResult CopyToExtended(IServerObject sourceServerObject, IServerObject destinationServerObject, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToExtendedResultFactory resultFactory);

		// Token: 0x060008BB RID: 2235
		RopResult CopyToStream(IServerObject sourceServerObject, IServerObject destinationServerObject, ulong bytesToCopy, CopyToStreamResultFactory resultFactory);

		// Token: 0x060008BC RID: 2236
		RopResult CreateAttachment(IServerObject serverObject, CreateAttachmentResultFactory resultFactory);

		// Token: 0x060008BD RID: 2237
		RopResult CreateBookmark(IServerObject serverObject, CreateBookmarkResultFactory resultFactory);

		// Token: 0x060008BE RID: 2238
		RopResult CreateFolder(IServerObject serverObject, FolderType folderType, CreateFolderFlags flags, string displayName, string folderComment, StoreLongTermId? longTermId, CreateFolderResultFactory resultFactory);

		// Token: 0x060008BF RID: 2239
		RopResult CreateMessage(IServerObject serverObject, ushort codePageId, StoreId folderId, bool createAssociated, CreateMessageResultFactory resultFactory);

		// Token: 0x060008C0 RID: 2240
		RopResult CreateMessageExtended(IServerObject serverObject, ushort codePageId, StoreId folderId, CreateMessageExtendedFlags createFlags, CreateMessageExtendedResultFactory resultFactory);

		// Token: 0x060008C1 RID: 2241
		RopResult DeleteAttachment(IServerObject serverObject, uint attachmentNumber, DeleteAttachmentResultFactory resultFactory);

		// Token: 0x060008C2 RID: 2242
		RopResult DeleteFolder(IServerObject serverObject, DeleteFolderFlags deleteFolderFlags, StoreId folderId, DeleteFolderResultFactory resultFactory);

		// Token: 0x060008C3 RID: 2243
		RopResult DeleteMessages(IServerObject serverObject, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds, DeleteMessagesResultFactory resultFactory);

		// Token: 0x060008C4 RID: 2244
		RopResult DeleteProperties(IServerObject serverObject, PropertyTag[] propertyTags, DeletePropertiesResultFactory resultFactory);

		// Token: 0x060008C5 RID: 2245
		RopResult DeletePropertiesNoReplicate(IServerObject serverObject, PropertyTag[] propertyTags, DeletePropertiesNoReplicateResultFactory resultFactory);

		// Token: 0x060008C6 RID: 2246
		RopResult EchoBinary(byte[] inputParameter, EchoBinaryResultFactory resultFactory);

		// Token: 0x060008C7 RID: 2247
		RopResult EchoInt(int inputParameter, EchoIntResultFactory resultFactory);

		// Token: 0x060008C8 RID: 2248
		RopResult EchoString(string inputParameter, EchoStringResultFactory resultFactory);

		// Token: 0x060008C9 RID: 2249
		RopResult EmptyFolder(IServerObject serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, EmptyFolderResultFactory resultFactory);

		// Token: 0x060008CA RID: 2250
		RopResult ExpandRow(IServerObject serverObject, short maxRows, StoreId categoryId, ExpandRowResultFactory resultFactory);

		// Token: 0x060008CB RID: 2251
		RopResult FastTransferDestinationCopyOperationConfigure(IServerObject serverObject, FastTransferCopyOperation copyOperation, FastTransferCopyPropertiesFlag flags, FastTransferDestinationCopyOperationConfigureResultFactory resultFactory);

		// Token: 0x060008CC RID: 2252
		RopResult FastTransferDestinationPutBuffer(IServerObject serverObject, ArraySegment<byte>[] dataChunks, FastTransferDestinationPutBufferResultFactory resultFactory);

		// Token: 0x060008CD RID: 2253
		RopResult FastTransferDestinationPutBufferExtended(IServerObject serverObject, ArraySegment<byte>[] dataChunks, FastTransferDestinationPutBufferExtendedResultFactory resultFactory);

		// Token: 0x060008CE RID: 2254
		RopResult FastTransferGetIncrementalState(IServerObject serverObject, FastTransferGetIncrementalStateResultFactory resultFactory);

		// Token: 0x060008CF RID: 2255
		RopResult FastTransferSourceCopyFolder(IServerObject serverObject, FastTransferCopyFolderFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyFolderResultFactory resultFactory);

		// Token: 0x060008D0 RID: 2256
		RopResult FastTransferSourceCopyMessages(IServerObject serverObject, StoreId[] messageIds, FastTransferCopyMessagesFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyMessagesResultFactory resultFactory);

		// Token: 0x060008D1 RID: 2257
		RopResult FastTransferSourceCopyProperties(IServerObject serverObject, byte level, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] propertyTags, FastTransferSourceCopyPropertiesResultFactory resultFactory);

		// Token: 0x060008D2 RID: 2258
		RopResult FastTransferSourceCopyTo(IServerObject serverObject, byte level, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, PropertyTag[] excludedPropertyTags, FastTransferSourceCopyToResultFactory resultFactory);

		// Token: 0x060008D3 RID: 2259
		RopResult FastTransferSourceGetBuffer(IServerObject serverObject, ushort bufferSize, FastTransferSourceGetBufferResultFactory resultFactory);

		// Token: 0x060008D4 RID: 2260
		RopResult FastTransferSourceGetBufferExtended(IServerObject serverObject, ushort bufferSize, FastTransferSourceGetBufferExtendedResultFactory resultFactory);

		// Token: 0x060008D5 RID: 2261
		RopResult FindRow(IServerObject serverObject, FindRowFlags flags, Restriction restriction, BookmarkOrigin bookmarkOrigin, byte[] bookmark, FindRowResultFactory resultFactory);

		// Token: 0x060008D6 RID: 2262
		RopResult FlushRecipients(IServerObject serverObject, PropertyTag[] extraPropertyTags, RecipientRow[] recipientRows, FlushRecipientsResultFactory resultFactory);

		// Token: 0x060008D7 RID: 2263
		RopResult FreeBookmark(IServerObject serverObject, byte[] bookmark, FreeBookmarkResultFactory resultFactory);

		// Token: 0x060008D8 RID: 2264
		RopResult GetAllPerUserLongTermIds(IServerObject serverObject, StoreLongTermId startId, GetAllPerUserLongTermIdsResultFactory resultFactory);

		// Token: 0x060008D9 RID: 2265
		RopResult GetAttachmentTable(IServerObject serverObject, TableFlags tableFlags, GetAttachmentTableResultFactory resultFactory);

		// Token: 0x060008DA RID: 2266
		RopResult GetCollapseState(IServerObject serverObject, StoreId rowId, uint rowInstanceNumber, GetCollapseStateResultFactory resultFactory);

		// Token: 0x060008DB RID: 2267
		RopResult GetContentsTable(IServerObject serverObject, TableFlags tableFlags, GetContentsTableResultFactory resultFactory);

		// Token: 0x060008DC RID: 2268
		RopResult GetContentsTableExtended(IServerObject serverObject, ExtendedTableFlags extendedTableFlags, GetContentsTableExtendedResultFactory resultFactory);

		// Token: 0x060008DD RID: 2269
		RopResult GetEffectiveRights(IServerObject serverObject, byte[] addressBookId, StoreId folderId, GetEffectiveRightsResultFactory resultFactory);

		// Token: 0x060008DE RID: 2270
		RopResult GetHierarchyTable(IServerObject serverObject, TableFlags tableFlags, GetHierarchyTableResultFactory resultFactory);

		// Token: 0x060008DF RID: 2271
		RopResult GetIdsFromNames(IServerObject serverObject, GetIdsFromNamesFlags flags, NamedProperty[] namedProperties, GetIdsFromNamesResultFactory resultFactory);

		// Token: 0x060008E0 RID: 2272
		RopResult GetLocalReplicationIds(IServerObject serverObject, uint idCount, GetLocalReplicationIdsResultFactory resultFactory);

		// Token: 0x060008E1 RID: 2273
		RopResult GetMessageStatus(IServerObject serverObject, StoreId messageId, GetMessageStatusResultFactory resultFactory);

		// Token: 0x060008E2 RID: 2274
		RopResult GetNamesFromIDs(IServerObject serverObject, PropertyId[] propertyIds, GetNamesFromIDsResultFactory resultFactory);

		// Token: 0x060008E3 RID: 2275
		RopResult GetOptionsData(IServerObject serverObject, string addressType, bool wantWin32, GetOptionsDataResultFactory resultFactory);

		// Token: 0x060008E4 RID: 2276
		RopResult GetOwningServers(IServerObject serverObject, StoreId folderId, GetOwningServersResultFactory resultFactory);

		// Token: 0x060008E5 RID: 2277
		RopResult GetPermissionsTable(IServerObject serverObject, TableFlags tableFlags, GetPermissionsTableResultFactory resultFactory);

		// Token: 0x060008E6 RID: 2278
		RopResult GetPerUserGuid(IServerObject serverObject, StoreLongTermId publicFolderLongTermId, GetPerUserGuidResultFactory resultFactory);

		// Token: 0x060008E7 RID: 2279
		RopResult GetPerUserLongTermIds(IServerObject serverObject, Guid databaseGuid, GetPerUserLongTermIdsResultFactory resultFactory);

		// Token: 0x060008E8 RID: 2280
		RopResult GetPropertiesAll(IServerObject serverObject, ushort streamLimit, GetPropertiesFlags flags, GetPropertiesAllResultFactory resultFactory);

		// Token: 0x060008E9 RID: 2281
		RopResult GetPropertiesSpecific(IServerObject serverObject, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags, GetPropertiesSpecificResultFactory resultFactory);

		// Token: 0x060008EA RID: 2282
		RopResult GetPropertyList(IServerObject serverObject, GetPropertyListResultFactory resultFactory);

		// Token: 0x060008EB RID: 2283
		RopResult GetReceiveFolder(IServerObject serverObject, string messageClass, GetReceiveFolderResultFactory resultFactory);

		// Token: 0x060008EC RID: 2284
		RopResult GetReceiveFolderTable(IServerObject serverObject, GetReceiveFolderTableResultFactory resultFactory);

		// Token: 0x060008ED RID: 2285
		RopResult GetRulesTable(IServerObject serverObject, TableFlags tableFlags, GetRulesTableResultFactory resultFactory);

		// Token: 0x060008EE RID: 2286
		RopResult GetSearchCriteria(IServerObject serverObject, GetSearchCriteriaFlags flags, GetSearchCriteriaResultFactory resultFactory);

		// Token: 0x060008EF RID: 2287
		RopResult GetStatus(IServerObject serverObject, GetStatusResultFactory resultFactory);

		// Token: 0x060008F0 RID: 2288
		RopResult GetStoreState(IServerObject serverObject, GetStoreStateResultFactory resultFactory);

		// Token: 0x060008F1 RID: 2289
		RopResult GetStreamSize(IServerObject serverObject, GetStreamSizeResultFactory resultFactory);

		// Token: 0x060008F2 RID: 2290
		RopResult HardDeleteMessages(IServerObject serverObject, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds, HardDeleteMessagesResultFactory resultFactory);

		// Token: 0x060008F3 RID: 2291
		RopResult HardEmptyFolder(IServerObject serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, HardEmptyFolderResultFactory resultFactory);

		// Token: 0x060008F4 RID: 2292
		RopResult IdFromLongTermId(IServerObject serverObject, StoreLongTermId longTermId, IdFromLongTermIdResultFactory resultFactory);

		// Token: 0x060008F5 RID: 2293
		RopResult ImportDelete(IServerObject serverObject, ImportDeleteFlags importDeleteFlags, PropertyValue[] deleteChanges, ImportDeleteResultFactory resultFactory);

		// Token: 0x060008F6 RID: 2294
		RopResult ImportHierarchyChange(IServerObject serverObject, PropertyValue[] hierarchyPropertyValues, PropertyValue[] folderPropertyValues, ImportHierarchyChangeResultFactory resultFactory);

		// Token: 0x060008F7 RID: 2295
		RopResult ImportMessageChange(IServerObject serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangeResultFactory resultFactory);

		// Token: 0x060008F8 RID: 2296
		RopResult ImportMessageChangePartial(IServerObject serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangePartialResultFactory resultFactory);

		// Token: 0x060008F9 RID: 2297
		RopResult ImportMessageMove(IServerObject serverObject, byte[] sourceFolder, byte[] sourceMessage, byte[] predecessorChangeList, byte[] destinationMessage, byte[] destinationChangeNumber, ImportMessageMoveResultFactory resultFactory);

		// Token: 0x060008FA RID: 2298
		RopResult ImportReads(IServerObject serverObject, MessageReadState[] messageReadStates, ImportReadsResultFactory resultFactory);

		// Token: 0x060008FB RID: 2299
		RopResult IncrementalConfig(IServerObject serverObject, IncrementalConfigOption configOptions, FastTransferSendOption sendOptions, SyncFlag syncFlags, Restriction restriction, SyncExtraFlag extraFlags, PropertyTag[] propertyTags, StoreId[] messageIds, IncrementalConfigResultFactory resultFactory);

		// Token: 0x060008FC RID: 2300
		RopResult LockRegionStream(IServerObject serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, LockRegionStreamResultFactory resultFactory);

		// Token: 0x060008FD RID: 2301
		RopResult Logon(LogonFlags logonFlags, OpenFlags openFlags, StoreState storeState, LogonExtendedRequestFlags extendedFlags, MailboxId? mailboxId, LocaleInfo? localeInfo, string applicationId, AuthenticationContext authenticationContext, byte[] tenantHint, LogonResultFactory resultFactory);

		// Token: 0x060008FE RID: 2302
		RopResult LongTermIdFromId(IServerObject serverObject, StoreId storeId, LongTermIdFromIdResultFactory resultFactory);

		// Token: 0x060008FF RID: 2303
		RopResult ModifyPermissions(IServerObject serverObject, ModifyPermissionsFlags modifyPermissionsFlags, ModifyTableRow[] permissions, ModifyPermissionsResultFactory resultFactory);

		// Token: 0x06000900 RID: 2304
		RopResult ModifyRules(IServerObject serverObject, ModifyRulesFlags modifyRulesFlags, ModifyTableRow[] rulesData, ModifyRulesResultFactory resultFactory);

		// Token: 0x06000901 RID: 2305
		RopResult MoveCopyMessages(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, MoveCopyMessagesResultFactory resultFactory);

		// Token: 0x06000902 RID: 2306
		RopResult MoveCopyMessagesExtended(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, MoveCopyMessagesExtendedResultFactory resultFactory);

		// Token: 0x06000903 RID: 2307
		RopResult MoveCopyMessagesExtendedWithEntryIds(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, MoveCopyMessagesExtendedWithEntryIdsResultFactory resultFactory);

		// Token: 0x06000904 RID: 2308
		RopResult MoveFolder(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, StoreId sourceSubFolderId, string destinationSubFolderName, MoveFolderResultFactory resultFactory);

		// Token: 0x06000905 RID: 2309
		RopResult OpenAttachment(IServerObject serverObject, OpenMode openMode, uint attachmentNumber, OpenAttachmentResultFactory resultFactory);

		// Token: 0x06000906 RID: 2310
		RopResult OpenCollector(IServerObject serverObject, bool wantMessageCollector, OpenCollectorResultFactory resultFactory);

		// Token: 0x06000907 RID: 2311
		RopResult OpenEmbeddedMessage(IServerObject serverObject, ushort codePageId, OpenMode openMode, OpenEmbeddedMessageResultFactory resultFactory);

		// Token: 0x06000908 RID: 2312
		RopResult OpenFolder(IServerObject serverObject, StoreId folderId, OpenMode openMode, OpenFolderResultFactory resultFactory);

		// Token: 0x06000909 RID: 2313
		RopResult OpenMessage(IServerObject serverObject, ushort codePageId, StoreId folderId, OpenMode openMode, StoreId messageId, OpenMessageResultFactory resultFactory);

		// Token: 0x0600090A RID: 2314
		RopResult OpenStream(IServerObject serverObject, PropertyTag propertyTag, OpenMode openMode, OpenStreamResultFactory resultFactory);

		// Token: 0x0600090B RID: 2315
		RopResult PrereadMessages(IServerObject serverObject, StoreIdPair[] messages, PrereadMessagesResultFactory resultFactory);

		// Token: 0x0600090C RID: 2316
		RopResult Progress(IServerObject serverObject, bool wantCancel, ProgressResultFactory resultFactory);

		// Token: 0x0600090D RID: 2317
		RopResult PublicFolderIsGhosted(IServerObject serverObject, StoreId folderId, PublicFolderIsGhostedResultFactory resultFactory);

		// Token: 0x0600090E RID: 2318
		RopResult QueryColumnsAll(IServerObject serverObject, QueryColumnsAllResultFactory resultFactory);

		// Token: 0x0600090F RID: 2319
		RopResult QueryNamedProperties(IServerObject serverObject, QueryNamedPropertyFlags queryFlags, Guid? propertyGuid, QueryNamedPropertiesResultFactory resultFactory);

		// Token: 0x06000910 RID: 2320
		RopResult QueryPosition(IServerObject serverObject, QueryPositionResultFactory resultFactory);

		// Token: 0x06000911 RID: 2321
		RopResult QueryRows(IServerObject serverObject, QueryRowsFlags flags, bool useForwardDirection, ushort rowCount, QueryRowsResultFactory resultFactory);

		// Token: 0x06000912 RID: 2322
		RopResult ReadPerUserInformation(IServerObject serverObject, StoreLongTermId longTermId, bool wantIfChanged, uint dataOffset, ushort maxDataSize, ReadPerUserInformationResultFactory resultFactory);

		// Token: 0x06000913 RID: 2323
		RopResult ReadRecipients(IServerObject serverObject, uint recipientRowId, PropertyTag[] extraUnicodePropertyTags, ReadRecipientsResultFactory resultFactory);

		// Token: 0x06000914 RID: 2324
		RopResult ReadStream(IServerObject serverObject, ushort byteCount, ReadStreamResultFactory resultFactory);

		// Token: 0x06000915 RID: 2325
		RopResult RegisterNotification(IServerObject serverObject, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId, RegisterNotificationResultFactory resultFactory);

		// Token: 0x06000916 RID: 2326
		RopResult RegisterSynchronizationNotifications(IServerObject serverObject, StoreId[] folderIds, uint[] changeNumbers, RegisterSynchronizationNotificationsResultFactory resultFactory);

		// Token: 0x06000917 RID: 2327
		void Release(IServerObject serverObject);

		// Token: 0x06000918 RID: 2328
		RopResult ReloadCachedInformation(IServerObject serverObject, PropertyTag[] extraUnicodePropertyTags, ReloadCachedInformationResultFactory resultFactory);

		// Token: 0x06000919 RID: 2329
		RopResult RemoveAllRecipients(IServerObject serverObject, RemoveAllRecipientsResultFactory resultFactory);

		// Token: 0x0600091A RID: 2330
		RopResult ResetTable(IServerObject serverObject, ResetTableResultFactory resultFactory);

		// Token: 0x0600091B RID: 2331
		RopResult Restrict(IServerObject serverObject, RestrictFlags flags, Restriction restriction, RestrictResultFactory resultFactory);

		// Token: 0x0600091C RID: 2332
		RopResult SaveChangesAttachment(IServerObject serverObject, SaveChangesMode saveChangesMode, SaveChangesAttachmentResultFactory resultFactory);

		// Token: 0x0600091D RID: 2333
		RopResult SaveChangesMessage(IServerObject serverObject, SaveChangesMode saveChangesMode, SaveChangesMessageResultFactory resultFactory);

		// Token: 0x0600091E RID: 2334
		RopResult SeekRow(IServerObject serverObject, BookmarkOrigin bookmarkOrigin, int rowCount, bool wantMoveCount, SeekRowResultFactory resultFactory);

		// Token: 0x0600091F RID: 2335
		RopResult SeekRowApproximate(IServerObject serverObject, uint numerator, uint denominator, SeekRowApproximateResultFactory resultFactory);

		// Token: 0x06000920 RID: 2336
		RopResult SeekRowBookmark(IServerObject serverObject, byte[] bookmark, int rowCount, bool wantMoveCount, SeekRowBookmarkResultFactory resultFactory);

		// Token: 0x06000921 RID: 2337
		RopResult SeekStream(IServerObject serverObject, StreamSeekOrigin streamSeekOrigin, long offset, SeekStreamResultFactory resultFactory);

		// Token: 0x06000922 RID: 2338
		RopResult SetCollapseState(IServerObject serverObject, byte[] collapseState, SetCollapseStateResultFactory resultFactory);

		// Token: 0x06000923 RID: 2339
		RopResult SetColumns(IServerObject serverObject, SetColumnsFlags flags, PropertyTag[] propertyTags, SetColumnsResultFactory resultFactory);

		// Token: 0x06000924 RID: 2340
		RopResult SetLocalReplicaMidsetDeleted(IServerObject serverObject, LongTermIdRange[] longTermIdRanges, SetLocalReplicaMidsetDeletedResultFactory resultFactory);

		// Token: 0x06000925 RID: 2341
		RopResult SetMessageFlags(IServerObject serverObject, StoreId messageId, MessageFlags flags, MessageFlags flagsMask, SetMessageFlagsResultFactory resultFactory);

		// Token: 0x06000926 RID: 2342
		RopResult SetMessageStatus(IServerObject serverObject, StoreId messageId, MessageStatusFlags status, MessageStatusFlags statusMask, SetMessageStatusResultFactory resultFactory);

		// Token: 0x06000927 RID: 2343
		RopResult SetProperties(IServerObject serverObject, PropertyValue[] propertyValues, SetPropertiesResultFactory resultFactory);

		// Token: 0x06000928 RID: 2344
		RopResult SetPropertiesNoReplicate(IServerObject serverObject, PropertyValue[] propertyValues, SetPropertiesNoReplicateResultFactory resultFactory);

		// Token: 0x06000929 RID: 2345
		RopResult SetReadFlag(IServerObject serverObject, SetReadFlagFlags flags, SetReadFlagResultFactory resultFactory);

		// Token: 0x0600092A RID: 2346
		RopResult SetReadFlags(IServerObject serverObject, bool reportProgress, SetReadFlagFlags flags, StoreId[] messageIds, SetReadFlagsResultFactory resultFactory);

		// Token: 0x0600092B RID: 2347
		RopResult SetReceiveFolder(IServerObject serverObject, StoreId folderId, string messageClass, SetReceiveFolderResultFactory resultFactory);

		// Token: 0x0600092C RID: 2348
		RopResult SetSearchCriteria(IServerObject serverObject, Restriction restriction, StoreId[] folderIds, SetSearchCriteriaFlags setSearchCriteriaFlags, SetSearchCriteriaResultFactory resultFactory);

		// Token: 0x0600092D RID: 2349
		RopResult SetSizeStream(IServerObject serverObject, ulong streamSize, SetSizeStreamResultFactory resultFactory);

		// Token: 0x0600092E RID: 2350
		RopResult SetSpooler(IServerObject serverObject, SetSpoolerResultFactory resultFactory);

		// Token: 0x0600092F RID: 2351
		RopResult SetSynchronizationNotificationGuid(IServerObject serverObject, Guid notificationGuid, SetSynchronizationNotificationGuidResultFactory resultFactory);

		// Token: 0x06000930 RID: 2352
		RopResult SetTransport(IServerObject serverObject, SetTransportResultFactory resultFactory);

		// Token: 0x06000931 RID: 2353
		RopResult SortTable(IServerObject serverObject, SortTableFlags flags, ushort categoryCount, ushort expandedCount, SortOrder[] sortOrders, SortTableResultFactory resultFactory);

		// Token: 0x06000932 RID: 2354
		RopResult SpoolerLockMessage(IServerObject serverObject, StoreId messageId, LockState lockState, SpoolerLockMessageResultFactory resultFactory);

		// Token: 0x06000933 RID: 2355
		RopResult SpoolerRules(IServerObject serverObject, StoreId folderId, SpoolerRulesResultFactory resultFactory);

		// Token: 0x06000934 RID: 2356
		RopResult SubmitMessage(IServerObject serverObject, SubmitMessageFlags submitFlags, SubmitMessageResultFactory resultFactory);

		// Token: 0x06000935 RID: 2357
		RopResult SynchronizationOpenAdvisor(IServerObject serverObject, SynchronizationOpenAdvisorResultFactory resultFactory);

		// Token: 0x06000936 RID: 2358
		RopResult TellVersion(IServerObject serverObject, ushort productVersion, ushort buildMajorVersion, ushort buildMinorVersion, TellVersionResultFactory resultFactory);

		// Token: 0x06000937 RID: 2359
		RopResult TransportDeliverMessage(IServerObject serverObject, TransportRecipientType recipientType, TransportDeliverMessageResultFactory resultFactory);

		// Token: 0x06000938 RID: 2360
		RopResult TransportDeliverMessage2(IServerObject serverObject, TransportRecipientType recipientType, TransportDeliverMessage2ResultFactory resultFactory);

		// Token: 0x06000939 RID: 2361
		RopResult TransportDoneWithMessage(IServerObject serverObject, TransportDoneWithMessageResultFactory resultFactory);

		// Token: 0x0600093A RID: 2362
		RopResult TransportDuplicateDeliveryCheck(IServerObject serverObject, byte flags, ExDateTime submitTime, string internetMessageId, TransportDuplicateDeliveryCheckResultFactory resultFactory);

		// Token: 0x0600093B RID: 2363
		RopResult TransportNewMail(IServerObject serverObject, StoreId folderId, StoreId messageId, string messageClass, MessageFlags messageFlags, TransportNewMailResultFactory resultFactory);

		// Token: 0x0600093C RID: 2364
		RopResult TransportSend(IServerObject serverObject, TransportSendResultFactory resultFactory);

		// Token: 0x0600093D RID: 2365
		RopResult UnlockRegionStream(IServerObject serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, UnlockRegionStreamResultFactory resultFactory);

		// Token: 0x0600093E RID: 2366
		RopResult UpdateDeferredActionMessages(IServerObject serverObject, byte[] serverEntryId, byte[] clientEntryId, UpdateDeferredActionMessagesResultFactory resultFactory);

		// Token: 0x0600093F RID: 2367
		RopResult UploadStateStreamBegin(IServerObject serverObject, PropertyTag propertyTag, uint size, UploadStateStreamBeginResultFactory resultFactory);

		// Token: 0x06000940 RID: 2368
		RopResult UploadStateStreamContinue(IServerObject serverObject, ArraySegment<byte> data, UploadStateStreamContinueResultFactory resultFactory);

		// Token: 0x06000941 RID: 2369
		RopResult UploadStateStreamEnd(IServerObject serverObject, UploadStateStreamEndResultFactory resultFactory);

		// Token: 0x06000942 RID: 2370
		RopResult WriteCommitStream(IServerObject serverObject, byte[] data, WriteCommitStreamResultFactory resultFactory);

		// Token: 0x06000943 RID: 2371
		RopResult WritePerUserInformation(IServerObject serverObject, StoreLongTermId longTermId, bool hasFinished, uint dataOffset, byte[] data, Guid? replicaGuid, WritePerUserInformationResultFactory resultFactory);

		// Token: 0x06000944 RID: 2372
		RopResult WriteStream(IServerObject serverObject, ArraySegment<byte> data, WriteStreamResultFactory resultFactory);

		// Token: 0x06000945 RID: 2373
		RopResult WriteStreamExtended(IServerObject serverObject, ArraySegment<byte>[] dataChunks, WriteStreamExtendedResultFactory resultFactory);
	}
}
