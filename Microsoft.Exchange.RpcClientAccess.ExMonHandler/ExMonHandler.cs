using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000010 RID: 16
	internal class ExMonHandler : MapiExMonLogger, IRopHandler, IConnectionHandler, IDisposable
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public RopResult Abort(IServerObject serverObject, AbortResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.Abort(serverObject, resultFactory);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public RopResult AbortSubmit(IServerObject serverObject, StoreId folderId, StoreId messageId, AbortSubmitResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.AbortSubmit(serverObject, folderId, messageId, resultFactory);
			base.OnMid(messageId);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C2C File Offset: 0x00000E2C
		public RopResult AddressTypes(IServerObject serverObject, AddressTypesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.AddressTypes(serverObject, resultFactory);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C50 File Offset: 0x00000E50
		public RopResult CloneStream(IServerObject serverObject, CloneStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CloneStream(serverObject, resultFactory);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C74 File Offset: 0x00000E74
		public RopResult CollapseRow(IServerObject serverObject, StoreId categoryId, CollapseRowResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CollapseRow(serverObject, categoryId, resultFactory);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C98 File Offset: 0x00000E98
		public RopResult CommitStream(IServerObject serverObject, CommitStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CommitStream(serverObject, resultFactory);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002CBC File Offset: 0x00000EBC
		public RopResult CreateBookmark(IServerObject serverObject, CreateBookmarkResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CreateBookmark(serverObject, resultFactory);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public RopResult CopyFolder(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, bool recurse, StoreId sourceSubFolderId, string destinationSubFolderName, CopyFolderResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.CopyFolder(sourceServerObject, destinationServerObject, reportProgress, recurse, sourceSubFolderId, destinationSubFolderName, resultFactory);
			base.OnFid(sourceSubFolderId);
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D14 File Offset: 0x00000F14
		public RopResult CopyProperties(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] propertyTags, CopyPropertiesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CopyProperties(sourceServerObject, destinationServerObject, reportProgress, copyPropertiesFlags, propertyTags, resultFactory);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D3C File Offset: 0x00000F3C
		public RopResult CopyTo(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CopyTo(sourceServerObject, destinationServerObject, reportProgress, copySubObjects, copyPropertiesFlags, excludePropertyTags, resultFactory);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D68 File Offset: 0x00000F68
		public RopResult CopyToExtended(IServerObject sourceServerObject, IServerObject destinationServerObject, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToExtendedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CopyToExtended(sourceServerObject, destinationServerObject, copySubObjects, copyPropertiesFlags, excludePropertyTags, resultFactory);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D90 File Offset: 0x00000F90
		public RopResult CopyToStream(IServerObject sourceServerObject, IServerObject destinationServerObject, ulong bytesToCopy, CopyToStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CopyToStream(sourceServerObject, destinationServerObject, bytesToCopy, resultFactory);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public RopResult CreateAttachment(IServerObject serverObject, CreateAttachmentResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CreateAttachment(serverObject, resultFactory);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public RopResult CreateFolder(IServerObject serverObject, FolderType folderType, CreateFolderFlags flags, string displayName, string folderComment, StoreLongTermId? longTermId, CreateFolderResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.CreateFolder(serverObject, folderType, flags, displayName, folderComment, longTermId, resultFactory);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E04 File Offset: 0x00001004
		public RopResult CreateMessage(IServerObject serverObject, ushort codePageId, StoreId folderId, bool createAssociated, CreateMessageResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.CreateMessage(serverObject, codePageId, folderId, createAssociated, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E34 File Offset: 0x00001034
		public RopResult CreateMessageExtended(IServerObject serverObject, ushort codePageId, StoreId folderId, CreateMessageExtendedFlags createFlags, CreateMessageExtendedResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.CreateMessageExtended(serverObject, codePageId, folderId, createFlags, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E64 File Offset: 0x00001064
		public RopResult DeleteAttachment(IServerObject serverObject, uint attachmentNumber, DeleteAttachmentResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.DeleteAttachment(serverObject, attachmentNumber, resultFactory);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E88 File Offset: 0x00001088
		public RopResult DeleteFolder(IServerObject serverObject, DeleteFolderFlags deleteFolderFlags, StoreId folderId, DeleteFolderResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.DeleteFolder(serverObject, deleteFolderFlags, folderId, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002EB4 File Offset: 0x000010B4
		public RopResult DeleteMessages(IServerObject serverObject, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds, DeleteMessagesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.DeleteMessages(serverObject, reportProgress, isOkToSendNonReadNotification, messageIds, resultFactory);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EDC File Offset: 0x000010DC
		public RopResult DeleteProperties(IServerObject serverObject, PropertyTag[] propertyTags, DeletePropertiesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.DeleteProperties(serverObject, propertyTags, resultFactory);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F00 File Offset: 0x00001100
		public RopResult DeletePropertiesNoReplicate(IServerObject serverObject, PropertyTag[] propertyTags, DeletePropertiesNoReplicateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.DeletePropertiesNoReplicate(serverObject, propertyTags, resultFactory);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F24 File Offset: 0x00001124
		public RopResult EchoBinary(byte[] inputParameter, EchoBinaryResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.EchoBinary(inputParameter, resultFactory);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F48 File Offset: 0x00001148
		public RopResult EchoInt(int inputParameter, EchoIntResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.EchoInt(inputParameter, resultFactory);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F6C File Offset: 0x0000116C
		public RopResult EchoString(string inputParameter, EchoStringResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.EchoString(inputParameter, resultFactory);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F90 File Offset: 0x00001190
		public RopResult EmptyFolder(IServerObject serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, EmptyFolderResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.EmptyFolder(serverObject, reportProgress, emptyFolderFlags, resultFactory);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FB4 File Offset: 0x000011B4
		public RopResult ExpandRow(IServerObject serverObject, short maxRows, StoreId categoryId, ExpandRowResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ExpandRow(serverObject, maxRows, categoryId, resultFactory);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002FD8 File Offset: 0x000011D8
		public RopResult FastTransferDestinationCopyOperationConfigure(IServerObject serverObject, FastTransferCopyOperation copyOperation, FastTransferCopyPropertiesFlag flags, FastTransferDestinationCopyOperationConfigureResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferDestinationCopyOperationConfigure(serverObject, copyOperation, flags, resultFactory);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002FFC File Offset: 0x000011FC
		public RopResult FastTransferDestinationPutBuffer(IServerObject serverObject, ArraySegment<byte>[] dataChunks, FastTransferDestinationPutBufferResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferDestinationPutBuffer(serverObject, dataChunks, resultFactory);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003020 File Offset: 0x00001220
		public RopResult FastTransferDestinationPutBufferExtended(IServerObject serverObject, ArraySegment<byte>[] dataChunks, FastTransferDestinationPutBufferExtendedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferDestinationPutBufferExtended(serverObject, dataChunks, resultFactory);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003044 File Offset: 0x00001244
		public RopResult FastTransferGetIncrementalState(IServerObject serverObject, FastTransferGetIncrementalStateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferGetIncrementalState(serverObject, resultFactory);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003068 File Offset: 0x00001268
		public RopResult FastTransferSourceCopyFolder(IServerObject serverObject, FastTransferCopyFolderFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyFolderResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferSourceCopyFolder(serverObject, flags, sendOptions, resultFactory);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000308C File Offset: 0x0000128C
		public RopResult FastTransferSourceCopyMessages(IServerObject serverObject, StoreId[] messageIds, FastTransferCopyMessagesFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyMessagesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferSourceCopyMessages(serverObject, messageIds, flags, sendOptions, resultFactory);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030B4 File Offset: 0x000012B4
		public RopResult FastTransferSourceCopyProperties(IServerObject serverObject, byte level, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] propertyTags, FastTransferSourceCopyPropertiesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferSourceCopyProperties(serverObject, level, flags, sendOptions, propertyTags, resultFactory);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030DC File Offset: 0x000012DC
		public RopResult FastTransferSourceCopyTo(IServerObject serverObject, byte level, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, PropertyTag[] excludedPropertyTags, FastTransferSourceCopyToResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferSourceCopyTo(serverObject, level, flags, sendOptions, excludedPropertyTags, resultFactory);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003104 File Offset: 0x00001304
		public RopResult FastTransferSourceGetBuffer(IServerObject serverObject, ushort bufferSize, FastTransferSourceGetBufferResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferSourceGetBuffer(serverObject, bufferSize, resultFactory);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003128 File Offset: 0x00001328
		public RopResult FastTransferSourceGetBufferExtended(IServerObject serverObject, ushort bufferSize, FastTransferSourceGetBufferExtendedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FastTransferSourceGetBufferExtended(serverObject, bufferSize, resultFactory);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000314C File Offset: 0x0000134C
		public RopResult FindRow(IServerObject serverObject, FindRowFlags flags, Restriction restriction, BookmarkOrigin bookmarkOrigin, byte[] bookmark, FindRowResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FindRow(serverObject, flags, restriction, bookmarkOrigin, bookmark, resultFactory);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003174 File Offset: 0x00001374
		public RopResult FlushRecipients(IServerObject serverObject, PropertyTag[] extraPropertyTags, RecipientRow[] recipientRows, FlushRecipientsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FlushRecipients(serverObject, extraPropertyTags, recipientRows, resultFactory);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003198 File Offset: 0x00001398
		public RopResult FreeBookmark(IServerObject serverObject, byte[] bookmark, FreeBookmarkResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.FreeBookmark(serverObject, bookmark, resultFactory);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000031BC File Offset: 0x000013BC
		public RopResult GetAllPerUserLongTermIds(IServerObject serverObject, StoreLongTermId startId, GetAllPerUserLongTermIdsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetAllPerUserLongTermIds(serverObject, startId, resultFactory);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000031E0 File Offset: 0x000013E0
		public RopResult GetAttachmentTable(IServerObject serverObject, TableFlags tableFlags, GetAttachmentTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetAttachmentTable(serverObject, tableFlags, resultFactory);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003204 File Offset: 0x00001404
		public RopResult GetCollapseState(IServerObject serverObject, StoreId rowId, uint rowInstanceNumber, GetCollapseStateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetCollapseState(serverObject, rowId, rowInstanceNumber, resultFactory);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003228 File Offset: 0x00001428
		public RopResult GetContentsTable(IServerObject serverObject, TableFlags tableFlags, GetContentsTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetContentsTable(serverObject, tableFlags, resultFactory);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000324C File Offset: 0x0000144C
		public RopResult GetContentsTableExtended(IServerObject serverObject, ExtendedTableFlags extendedTableFlags, GetContentsTableExtendedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetContentsTableExtended(serverObject, extendedTableFlags, resultFactory);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003270 File Offset: 0x00001470
		public RopResult GetEffectiveRights(IServerObject serverObject, byte[] addressBookId, StoreId folderId, GetEffectiveRightsResultFactory resultFactory)
		{
			RopResult effectiveRights = this.connectionHandler.RopHandler.GetEffectiveRights(serverObject, addressBookId, folderId, resultFactory);
			base.OnFid(folderId);
			return effectiveRights;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000329C File Offset: 0x0000149C
		public RopResult GetHierarchyTable(IServerObject serverObject, TableFlags tableFlags, GetHierarchyTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetHierarchyTable(serverObject, tableFlags, resultFactory);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000032C0 File Offset: 0x000014C0
		public RopResult GetIdsFromNames(IServerObject serverObject, GetIdsFromNamesFlags flags, NamedProperty[] namedProperties, GetIdsFromNamesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetIdsFromNames(serverObject, flags, namedProperties, resultFactory);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000032E4 File Offset: 0x000014E4
		public RopResult GetLocalReplicationIds(IServerObject serverObject, uint idCount, GetLocalReplicationIdsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetLocalReplicationIds(serverObject, idCount, resultFactory);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003308 File Offset: 0x00001508
		public RopResult GetMessageStatus(IServerObject serverObject, StoreId messageId, GetMessageStatusResultFactory resultFactory)
		{
			RopResult messageStatus = this.connectionHandler.RopHandler.GetMessageStatus(serverObject, messageId, resultFactory);
			base.OnMid(messageId);
			return messageStatus;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003334 File Offset: 0x00001534
		public RopResult GetNamesFromIDs(IServerObject serverObject, PropertyId[] propertyIds, GetNamesFromIDsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetNamesFromIDs(serverObject, propertyIds, resultFactory);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003358 File Offset: 0x00001558
		public RopResult GetOptionsData(IServerObject serverObject, string addressType, bool wantWin32, GetOptionsDataResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetOptionsData(serverObject, addressType, wantWin32, resultFactory);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000337C File Offset: 0x0000157C
		public RopResult GetOwningServers(IServerObject serverObject, StoreId folderId, GetOwningServersResultFactory resultFactory)
		{
			RopResult owningServers = this.connectionHandler.RopHandler.GetOwningServers(serverObject, folderId, resultFactory);
			base.OnFid(folderId);
			return owningServers;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033A8 File Offset: 0x000015A8
		public RopResult GetPermissionsTable(IServerObject serverObject, TableFlags tableFlags, GetPermissionsTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetPermissionsTable(serverObject, tableFlags, resultFactory);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033CC File Offset: 0x000015CC
		public RopResult GetPerUserGuid(IServerObject serverObject, StoreLongTermId publicFolderLongTermId, GetPerUserGuidResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetPerUserGuid(serverObject, publicFolderLongTermId, resultFactory);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000033F0 File Offset: 0x000015F0
		public RopResult GetPerUserLongTermIds(IServerObject serverObject, Guid databaseGuid, GetPerUserLongTermIdsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetPerUserLongTermIds(serverObject, databaseGuid, resultFactory);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003414 File Offset: 0x00001614
		public RopResult GetPropertiesAll(IServerObject serverObject, ushort streamLimit, GetPropertiesFlags flags, GetPropertiesAllResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetPropertiesAll(serverObject, streamLimit, flags, resultFactory);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003438 File Offset: 0x00001638
		public RopResult GetPropertyList(IServerObject serverObject, GetPropertyListResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetPropertyList(serverObject, resultFactory);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000345C File Offset: 0x0000165C
		public RopResult GetPropertiesSpecific(IServerObject serverObject, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags, GetPropertiesSpecificResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetPropertiesSpecific(serverObject, streamLimit, flags, propertyTags, resultFactory);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003484 File Offset: 0x00001684
		public RopResult GetReceiveFolder(IServerObject serverObject, string messageClass, GetReceiveFolderResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetReceiveFolder(serverObject, messageClass, resultFactory);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000034A8 File Offset: 0x000016A8
		public RopResult GetReceiveFolderTable(IServerObject serverObject, GetReceiveFolderTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetReceiveFolderTable(serverObject, resultFactory);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034CC File Offset: 0x000016CC
		public RopResult GetRulesTable(IServerObject serverObject, TableFlags tableFlags, GetRulesTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetRulesTable(serverObject, tableFlags, resultFactory);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034F0 File Offset: 0x000016F0
		public RopResult GetSearchCriteria(IServerObject serverObject, GetSearchCriteriaFlags flags, GetSearchCriteriaResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetSearchCriteria(serverObject, flags, resultFactory);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003514 File Offset: 0x00001714
		public RopResult GetStatus(IServerObject serverObject, GetStatusResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetStatus(serverObject, resultFactory);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003538 File Offset: 0x00001738
		public RopResult GetStoreState(IServerObject serverObject, GetStoreStateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetStoreState(serverObject, resultFactory);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000355C File Offset: 0x0000175C
		public RopResult GetStreamSize(IServerObject serverObject, GetStreamSizeResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.GetStreamSize(serverObject, resultFactory);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003580 File Offset: 0x00001780
		public RopResult HardEmptyFolder(IServerObject serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, HardEmptyFolderResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.HardEmptyFolder(serverObject, reportProgress, emptyFolderFlags, resultFactory);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000035A4 File Offset: 0x000017A4
		public RopResult HardDeleteMessages(IServerObject serverObject, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds, HardDeleteMessagesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.HardDeleteMessages(serverObject, reportProgress, isOkToSendNonReadNotification, messageIds, resultFactory);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000035CC File Offset: 0x000017CC
		public RopResult IdFromLongTermId(IServerObject serverObject, StoreLongTermId longTermId, IdFromLongTermIdResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.IdFromLongTermId(serverObject, longTermId, resultFactory);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035F0 File Offset: 0x000017F0
		public RopResult ImportDelete(IServerObject serverObject, ImportDeleteFlags importDeleteFlags, PropertyValue[] deleteChanges, ImportDeleteResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ImportDelete(serverObject, importDeleteFlags, deleteChanges, resultFactory);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003614 File Offset: 0x00001814
		public RopResult ImportHierarchyChange(IServerObject serverObject, PropertyValue[] hierarchyPropertyValues, PropertyValue[] folderPropertyValues, ImportHierarchyChangeResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ImportHierarchyChange(serverObject, hierarchyPropertyValues, folderPropertyValues, resultFactory);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003638 File Offset: 0x00001838
		public RopResult ImportMessageChange(IServerObject serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangeResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ImportMessageChange(serverObject, importMessageChangeFlags, propertyValues, resultFactory);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000365C File Offset: 0x0000185C
		public RopResult ImportMessageChangePartial(IServerObject serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangePartialResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ImportMessageChangePartial(serverObject, importMessageChangeFlags, propertyValues, resultFactory);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003680 File Offset: 0x00001880
		public RopResult ImportMessageMove(IServerObject serverObject, byte[] sourceFolder, byte[] sourceMessage, byte[] predecessorChangeList, byte[] destinationMessage, byte[] destinationChangeNumber, ImportMessageMoveResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ImportMessageMove(serverObject, sourceFolder, sourceMessage, predecessorChangeList, destinationMessage, destinationChangeNumber, resultFactory);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000036AC File Offset: 0x000018AC
		public RopResult ImportReads(IServerObject serverObject, MessageReadState[] messageReadStates, ImportReadsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ImportReads(serverObject, messageReadStates, resultFactory);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000036D0 File Offset: 0x000018D0
		public RopResult IncrementalConfig(IServerObject serverObject, IncrementalConfigOption configOptions, FastTransferSendOption sendOptions, SyncFlag syncFlags, Restriction restriction, SyncExtraFlag extraFlags, PropertyTag[] propertyTags, StoreId[] messageIds, IncrementalConfigResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.IncrementalConfig(serverObject, configOptions, sendOptions, syncFlags, restriction, extraFlags, propertyTags, messageIds, resultFactory);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003700 File Offset: 0x00001900
		public RopResult LockRegionStream(IServerObject serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, LockRegionStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.LockRegionStream(serverObject, offset, regionLength, lockType, resultFactory);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003728 File Offset: 0x00001928
		public RopResult Logon(LogonFlags logonFlags, OpenFlags openFlags, StoreState storeState, LogonExtendedRequestFlags extendedFlags, MailboxId? mailboxId, LocaleInfo? localeInfo, string applicationId, AuthenticationContext authenticationContext, byte[] tenantHint, LogonResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.Logon(logonFlags, openFlags, storeState, extendedFlags, mailboxId, localeInfo, applicationId, authenticationContext, tenantHint, resultFactory);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003758 File Offset: 0x00001958
		public RopResult LongTermIdFromId(IServerObject serverObject, StoreId storeId, LongTermIdFromIdResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.LongTermIdFromId(serverObject, storeId, resultFactory);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000377C File Offset: 0x0000197C
		public RopResult ModifyPermissions(IServerObject serverObject, ModifyPermissionsFlags modifyPermissionsFlags, ModifyTableRow[] permissions, ModifyPermissionsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ModifyPermissions(serverObject, modifyPermissionsFlags, permissions, resultFactory);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000037A0 File Offset: 0x000019A0
		public RopResult ModifyRules(IServerObject serverObject, ModifyRulesFlags modifyRulesFlags, ModifyTableRow[] rulesData, ModifyRulesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ModifyRules(serverObject, modifyRulesFlags, rulesData, resultFactory);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000037C4 File Offset: 0x000019C4
		public RopResult MoveCopyMessages(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, MoveCopyMessagesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.MoveCopyMessages(sourceServerObject, destinationServerObject, messageIds, reportProgress, isCopy, resultFactory);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000037EC File Offset: 0x000019EC
		public RopResult MoveCopyMessagesExtended(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, MoveCopyMessagesExtendedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.MoveCopyMessagesExtended(sourceServerObject, destinationServerObject, messageIds, reportProgress, isCopy, propertyValues, resultFactory);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003818 File Offset: 0x00001A18
		public RopResult MoveCopyMessagesExtendedWithEntryIds(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, MoveCopyMessagesExtendedWithEntryIdsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.MoveCopyMessagesExtendedWithEntryIds(sourceServerObject, destinationServerObject, messageIds, reportProgress, isCopy, propertyValues, resultFactory);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003844 File Offset: 0x00001A44
		public RopResult MoveFolder(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, StoreId sourceSubFolderId, string destinationSubFolderName, MoveFolderResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.MoveFolder(sourceServerObject, destinationServerObject, reportProgress, sourceSubFolderId, destinationSubFolderName, resultFactory);
			base.OnFid(sourceSubFolderId);
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003874 File Offset: 0x00001A74
		public RopResult OpenAttachment(IServerObject serverObject, OpenMode openMode, uint attachmentNumber, OpenAttachmentResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.OpenAttachment(serverObject, openMode, attachmentNumber, resultFactory);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003898 File Offset: 0x00001A98
		public RopResult OpenCollector(IServerObject serverObject, bool wantMessageCollector, OpenCollectorResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.OpenCollector(serverObject, wantMessageCollector, resultFactory);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000038BC File Offset: 0x00001ABC
		public RopResult OpenFolder(IServerObject serverObject, StoreId folderId, OpenMode openMode, OpenFolderResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.OpenFolder(serverObject, folderId, openMode, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000038E8 File Offset: 0x00001AE8
		public RopResult OpenMessage(IServerObject serverObject, ushort codePageId, StoreId folderId, OpenMode openMode, StoreId messageId, OpenMessageResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.OpenMessage(serverObject, codePageId, folderId, openMode, messageId, resultFactory);
			base.OnMid(messageId);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003920 File Offset: 0x00001B20
		public RopResult OpenEmbeddedMessage(IServerObject serverObject, ushort codePageId, OpenMode openMode, OpenEmbeddedMessageResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.OpenEmbeddedMessage(serverObject, codePageId, openMode, resultFactory);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003944 File Offset: 0x00001B44
		public RopResult OpenStream(IServerObject serverObject, PropertyTag propertyTag, OpenMode openMode, OpenStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.OpenStream(serverObject, propertyTag, openMode, resultFactory);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003968 File Offset: 0x00001B68
		public RopResult PrereadMessages(IServerObject serverObject, StoreIdPair[] messages, PrereadMessagesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.PrereadMessages(serverObject, messages, resultFactory);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000398C File Offset: 0x00001B8C
		public RopResult Progress(IServerObject serverObject, bool wantCancel, ProgressResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.Progress(serverObject, wantCancel, resultFactory);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000039B0 File Offset: 0x00001BB0
		public RopResult PublicFolderIsGhosted(IServerObject serverObject, StoreId folderId, PublicFolderIsGhostedResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.PublicFolderIsGhosted(serverObject, folderId, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000039DC File Offset: 0x00001BDC
		public RopResult QueryColumnsAll(IServerObject serverObject, QueryColumnsAllResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.QueryColumnsAll(serverObject, resultFactory);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003A00 File Offset: 0x00001C00
		public RopResult QueryNamedProperties(IServerObject serverObject, QueryNamedPropertyFlags queryFlags, Guid? propertyGuid, QueryNamedPropertiesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.QueryNamedProperties(serverObject, queryFlags, propertyGuid, resultFactory);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003A24 File Offset: 0x00001C24
		public RopResult QueryPosition(IServerObject serverObject, QueryPositionResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.QueryPosition(serverObject, resultFactory);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A48 File Offset: 0x00001C48
		public RopResult QueryRows(IServerObject serverObject, QueryRowsFlags flags, bool useForwardDirection, ushort rowCount, QueryRowsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.QueryRows(serverObject, flags, useForwardDirection, rowCount, resultFactory);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003A70 File Offset: 0x00001C70
		public RopResult ReadPerUserInformation(IServerObject serverObject, StoreLongTermId longTermId, bool wantIfChanged, uint dataOffset, ushort maxDataSize, ReadPerUserInformationResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ReadPerUserInformation(serverObject, longTermId, wantIfChanged, dataOffset, maxDataSize, resultFactory);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003A98 File Offset: 0x00001C98
		public RopResult ReadRecipients(IServerObject serverObject, uint recipientRowId, PropertyTag[] extraUnicodePropertyTags, ReadRecipientsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ReadRecipients(serverObject, recipientRowId, extraUnicodePropertyTags, resultFactory);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003ABC File Offset: 0x00001CBC
		public RopResult ReadStream(IServerObject serverObject, ushort byteCount, ReadStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ReadStream(serverObject, byteCount, resultFactory);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public RopResult RegisterNotification(IServerObject serverObject, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId, RegisterNotificationResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.RegisterNotification(serverObject, flags, eventFlags, wantGlobalScope, folderId, messageId, resultFactory);
			base.OnMid(messageId);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003B1C File Offset: 0x00001D1C
		public RopResult RegisterSynchronizationNotifications(IServerObject serverObject, StoreId[] folderIds, uint[] changeNumbers, RegisterSynchronizationNotificationsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.RegisterSynchronizationNotifications(serverObject, folderIds, changeNumbers, resultFactory);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003B40 File Offset: 0x00001D40
		public void Release(IServerObject serverObject)
		{
			this.connectionHandler.RopHandler.Release(serverObject);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003B54 File Offset: 0x00001D54
		public RopResult ReloadCachedInformation(IServerObject serverObject, PropertyTag[] extraUnicodePropertyTags, ReloadCachedInformationResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ReloadCachedInformation(serverObject, extraUnicodePropertyTags, resultFactory);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003B78 File Offset: 0x00001D78
		public RopResult RemoveAllRecipients(IServerObject serverObject, RemoveAllRecipientsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.RemoveAllRecipients(serverObject, resultFactory);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003B9C File Offset: 0x00001D9C
		public RopResult ResetTable(IServerObject serverObject, ResetTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.ResetTable(serverObject, resultFactory);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public RopResult Restrict(IServerObject serverObject, RestrictFlags flags, Restriction restriction, RestrictResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.Restrict(serverObject, flags, restriction, resultFactory);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public RopResult SaveChangesAttachment(IServerObject serverObject, SaveChangesMode saveChangesMode, SaveChangesAttachmentResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SaveChangesAttachment(serverObject, saveChangesMode, resultFactory);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003C08 File Offset: 0x00001E08
		public RopResult SaveChangesMessage(IServerObject serverObject, SaveChangesMode saveChangesMode, SaveChangesMessageResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SaveChangesMessage(serverObject, saveChangesMode, resultFactory);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003C2C File Offset: 0x00001E2C
		public RopResult SeekRow(IServerObject serverObject, BookmarkOrigin bookmarkOrigin, int rowCount, bool wantMoveCount, SeekRowResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SeekRow(serverObject, bookmarkOrigin, rowCount, wantMoveCount, resultFactory);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003C54 File Offset: 0x00001E54
		public RopResult SeekRowApproximate(IServerObject serverObject, uint numerator, uint denominator, SeekRowApproximateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SeekRowApproximate(serverObject, numerator, denominator, resultFactory);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003C78 File Offset: 0x00001E78
		public RopResult SeekRowBookmark(IServerObject serverObject, byte[] bookmark, int rowCount, bool wantMoveCount, SeekRowBookmarkResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SeekRowBookmark(serverObject, bookmark, rowCount, wantMoveCount, resultFactory);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public RopResult SeekStream(IServerObject serverObject, StreamSeekOrigin streamSeekOrigin, long offset, SeekStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SeekStream(serverObject, streamSeekOrigin, offset, resultFactory);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public RopResult SetCollapseState(IServerObject serverObject, byte[] collapseState, SetCollapseStateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetCollapseState(serverObject, collapseState, resultFactory);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public RopResult SetColumns(IServerObject serverObject, SetColumnsFlags flags, PropertyTag[] propertyTags, SetColumnsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetColumns(serverObject, flags, propertyTags, resultFactory);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003D0C File Offset: 0x00001F0C
		public RopResult SetLocalReplicaMidsetDeleted(IServerObject serverObject, LongTermIdRange[] longTermIdRanges, SetLocalReplicaMidsetDeletedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetLocalReplicaMidsetDeleted(serverObject, longTermIdRanges, resultFactory);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D30 File Offset: 0x00001F30
		public RopResult SetMessageFlags(IServerObject serverObject, StoreId messageId, MessageFlags flags, MessageFlags flagsMask, SetMessageFlagsResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.SetMessageFlags(serverObject, messageId, flags, flagsMask, resultFactory);
			base.OnMid(messageId);
			return result;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003D60 File Offset: 0x00001F60
		public RopResult SetMessageStatus(IServerObject serverObject, StoreId messageId, MessageStatusFlags status, MessageStatusFlags statusMask, SetMessageStatusResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.SetMessageStatus(serverObject, messageId, status, statusMask, resultFactory);
			base.OnMid(messageId);
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D90 File Offset: 0x00001F90
		public RopResult SetProperties(IServerObject serverObject, PropertyValue[] propertyValues, SetPropertiesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetProperties(serverObject, propertyValues, resultFactory);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public RopResult SetPropertiesNoReplicate(IServerObject serverObject, PropertyValue[] propertyValues, SetPropertiesNoReplicateResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetPropertiesNoReplicate(serverObject, propertyValues, resultFactory);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public RopResult SetReadFlag(IServerObject serverObject, SetReadFlagFlags flags, SetReadFlagResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetReadFlag(serverObject, flags, resultFactory);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003DFC File Offset: 0x00001FFC
		public RopResult SetReadFlags(IServerObject serverObject, bool reportProgress, SetReadFlagFlags flags, StoreId[] messageIds, SetReadFlagsResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetReadFlags(serverObject, reportProgress, flags, messageIds, resultFactory);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003E24 File Offset: 0x00002024
		public RopResult SetReceiveFolder(IServerObject serverObject, StoreId folderId, string messageClass, SetReceiveFolderResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.SetReceiveFolder(serverObject, folderId, messageClass, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E50 File Offset: 0x00002050
		public RopResult SetSearchCriteria(IServerObject serverObject, Restriction restriction, StoreId[] folderIds, SetSearchCriteriaFlags setSearchCriteriaFlags, SetSearchCriteriaResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetSearchCriteria(serverObject, restriction, folderIds, setSearchCriteriaFlags, resultFactory);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003E78 File Offset: 0x00002078
		public RopResult SetSizeStream(IServerObject serverObject, ulong streamSize, SetSizeStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetSizeStream(serverObject, streamSize, resultFactory);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003E9C File Offset: 0x0000209C
		public RopResult SetSpooler(IServerObject serverObject, SetSpoolerResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetSpooler(serverObject, resultFactory);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003EC0 File Offset: 0x000020C0
		public RopResult SetSynchronizationNotificationGuid(IServerObject serverObject, Guid notificationGuid, SetSynchronizationNotificationGuidResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetSynchronizationNotificationGuid(serverObject, notificationGuid, resultFactory);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003EE4 File Offset: 0x000020E4
		public RopResult SetTransport(IServerObject serverObject, SetTransportResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SetTransport(serverObject, resultFactory);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003F08 File Offset: 0x00002108
		public RopResult SortTable(IServerObject serverObject, SortTableFlags flags, ushort categoryCount, ushort expandedCount, SortOrder[] sortOrders, SortTableResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SortTable(serverObject, flags, categoryCount, expandedCount, sortOrders, resultFactory);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003F30 File Offset: 0x00002130
		public RopResult SpoolerLockMessage(IServerObject serverObject, StoreId messageId, LockState lockState, SpoolerLockMessageResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.SpoolerLockMessage(serverObject, messageId, lockState, resultFactory);
			base.OnMid(messageId);
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003F5C File Offset: 0x0000215C
		public RopResult SpoolerRules(IServerObject serverObject, StoreId folderId, SpoolerRulesResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.SpoolerRules(serverObject, folderId, resultFactory);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003F88 File Offset: 0x00002188
		public RopResult SubmitMessage(IServerObject serverObject, SubmitMessageFlags submitFlags, SubmitMessageResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SubmitMessage(serverObject, submitFlags, resultFactory);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003FAC File Offset: 0x000021AC
		public RopResult SynchronizationOpenAdvisor(IServerObject serverObject, SynchronizationOpenAdvisorResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.SynchronizationOpenAdvisor(serverObject, resultFactory);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003FD0 File Offset: 0x000021D0
		public RopResult TellVersion(IServerObject serverObject, ushort productVersion, ushort buildMajorVersion, ushort buildMinorVersion, TellVersionResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.TellVersion(serverObject, productVersion, buildMajorVersion, buildMinorVersion, resultFactory);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003FF8 File Offset: 0x000021F8
		public RopResult TransportDeliverMessage(IServerObject serverObject, TransportRecipientType recipientType, TransportDeliverMessageResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.TransportDeliverMessage(serverObject, recipientType, resultFactory);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000401C File Offset: 0x0000221C
		public RopResult TransportDeliverMessage2(IServerObject serverObject, TransportRecipientType recipientType, TransportDeliverMessage2ResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.TransportDeliverMessage2(serverObject, recipientType, resultFactory);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004040 File Offset: 0x00002240
		public RopResult TransportDoneWithMessage(IServerObject serverObject, TransportDoneWithMessageResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.TransportDoneWithMessage(serverObject, resultFactory);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004064 File Offset: 0x00002264
		public RopResult TransportDuplicateDeliveryCheck(IServerObject serverObject, byte flags, ExDateTime submitTime, string internetMessageId, TransportDuplicateDeliveryCheckResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.TransportDuplicateDeliveryCheck(serverObject, flags, submitTime, internetMessageId, resultFactory);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000408C File Offset: 0x0000228C
		public RopResult TransportNewMail(IServerObject serverObject, StoreId folderId, StoreId messageId, string messageClass, MessageFlags messageFlags, TransportNewMailResultFactory resultFactory)
		{
			RopResult result = this.connectionHandler.RopHandler.TransportNewMail(serverObject, folderId, messageId, messageClass, messageFlags, resultFactory);
			base.OnMid(messageId);
			base.OnFid(folderId);
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000040C4 File Offset: 0x000022C4
		public RopResult TransportSend(IServerObject serverObject, TransportSendResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.TransportSend(serverObject, resultFactory);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000040E8 File Offset: 0x000022E8
		public RopResult UnlockRegionStream(IServerObject serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, UnlockRegionStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.UnlockRegionStream(serverObject, offset, regionLength, lockType, resultFactory);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004110 File Offset: 0x00002310
		public RopResult UpdateDeferredActionMessages(IServerObject serverObject, byte[] serverEntryId, byte[] clientEntryId, UpdateDeferredActionMessagesResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.UpdateDeferredActionMessages(serverObject, serverEntryId, clientEntryId, resultFactory);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004134 File Offset: 0x00002334
		public RopResult UploadStateStreamBegin(IServerObject serverObject, PropertyTag propertyTag, uint size, UploadStateStreamBeginResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.UploadStateStreamBegin(serverObject, propertyTag, size, resultFactory);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004158 File Offset: 0x00002358
		public RopResult UploadStateStreamContinue(IServerObject serverObject, ArraySegment<byte> data, UploadStateStreamContinueResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.UploadStateStreamContinue(serverObject, data, resultFactory);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000417C File Offset: 0x0000237C
		public RopResult UploadStateStreamEnd(IServerObject serverObject, UploadStateStreamEndResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.UploadStateStreamEnd(serverObject, resultFactory);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000041A0 File Offset: 0x000023A0
		public RopResult WriteCommitStream(IServerObject serverObject, byte[] data, WriteCommitStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.WriteCommitStream(serverObject, data, resultFactory);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000041C4 File Offset: 0x000023C4
		public RopResult WritePerUserInformation(IServerObject serverObject, StoreLongTermId longTermId, bool hasFinished, uint dataOffset, byte[] data, Guid? replicaGuid, WritePerUserInformationResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.WritePerUserInformation(serverObject, longTermId, hasFinished, dataOffset, data, replicaGuid, resultFactory);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000041F0 File Offset: 0x000023F0
		public RopResult WriteStream(IServerObject serverObject, ArraySegment<byte> data, WriteStreamResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.WriteStream(serverObject, data, resultFactory);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004214 File Offset: 0x00002414
		public RopResult WriteStreamExtended(IServerObject serverObject, ArraySegment<byte>[] dataChunks, WriteStreamExtendedResultFactory resultFactory)
		{
			return this.connectionHandler.RopHandler.WriteStreamExtended(serverObject, dataChunks, resultFactory);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004236 File Offset: 0x00002436
		public ExMonHandler(bool enableTestMode, int sessionId, string accessingPrincipalLegacyDN, string clientAddress, MapiVersion clientVersion, IConnectionHandler baseConnectionHandler, string serviceName) : base(enableTestMode, sessionId, accessingPrincipalLegacyDN, clientAddress, clientVersion, serviceName)
		{
			this.connectionHandler = baseConnectionHandler;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000424F File Offset: 0x0000244F
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004256 File Offset: 0x00002456
		public static bool IsEnabled
		{
			get
			{
				return ExMonHandler.isEnabled;
			}
			set
			{
				ExMonHandler.isEnabled = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000425E File Offset: 0x0000245E
		public IRopHandler RopHandler
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004261 File Offset: 0x00002461
		public INotificationHandler NotificationHandler
		{
			get
			{
				return this.connectionHandler.NotificationHandler;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000426E File Offset: 0x0000246E
		protected IConnectionHandler ConnectionHandler
		{
			get
			{
				return this.connectionHandler;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004278 File Offset: 0x00002478
		public void BeginRopProcessing(AuxiliaryData auxiliaryData)
		{
			JET_THREADSTATS threadStats = JET_THREADSTATS.Create(0, 0, 0, 0, 0, 0, 0);
			base.BeginRopProcessing(threadStats);
			this.connectionHandler.BeginRopProcessing(auxiliaryData);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000042A5 File Offset: 0x000024A5
		public void EndRopProcessing(AuxiliaryData auxiliaryData)
		{
			base.EndRopProcessing();
			this.connectionHandler.EndRopProcessing(auxiliaryData);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000042B9 File Offset: 0x000024B9
		public new void LogInputRops(IEnumerable<RopId> rops)
		{
			base.LogInputRops(rops);
			this.connectionHandler.LogInputRops(rops);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000042CE File Offset: 0x000024CE
		public new void LogPrepareForRop(RopId ropId)
		{
			base.LogPrepareForRop(ropId);
			this.connectionHandler.LogPrepareForRop(ropId);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000042E3 File Offset: 0x000024E3
		public new void LogCompletedRop(RopId ropId, ErrorCode errorCode)
		{
			base.LogCompletedRop(ropId, errorCode);
			this.connectionHandler.LogCompletedRop(ropId, errorCode);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000042FA File Offset: 0x000024FA
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExMonHandler>(this);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004302 File Offset: 0x00002502
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.ConnectionHandler);
			base.InternalDispose();
		}

		// Token: 0x04000062 RID: 98
		private static bool isEnabled = true;

		// Token: 0x04000063 RID: 99
		private readonly IConnectionHandler connectionHandler;
	}
}
