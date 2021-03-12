using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F4 RID: 500
	internal enum PropertyId : ushort
	{
		// Token: 0x040004A3 RID: 1187
		Null,
		// Token: 0x040004A4 RID: 1188
		ParentEntryId = 3593,
		// Token: 0x040004A5 RID: 1189
		InstanceKey = 4086,
		// Token: 0x040004A6 RID: 1190
		RecordKey = 4089,
		// Token: 0x040004A7 RID: 1191
		EntryId = 4095,
		// Token: 0x040004A8 RID: 1192
		Body,
		// Token: 0x040004A9 RID: 1193
		RtfCompressed = 4105,
		// Token: 0x040004AA RID: 1194
		RtfInSync = 3615,
		// Token: 0x040004AB RID: 1195
		Html = 4115,
		// Token: 0x040004AC RID: 1196
		NativeBodyInfo = 4118,
		// Token: 0x040004AD RID: 1197
		Preview = 16345,
		// Token: 0x040004AE RID: 1198
		SentMailEntryId = 26432,
		// Token: 0x040004AF RID: 1199
		DamOrgMsgEntryId,
		// Token: 0x040004B0 RID: 1200
		ReplyRecipientEntries = 79,
		// Token: 0x040004B1 RID: 1201
		ContentCount = 13826,
		// Token: 0x040004B2 RID: 1202
		Subject = 55,
		// Token: 0x040004B3 RID: 1203
		SubjectPrefix = 61,
		// Token: 0x040004B4 RID: 1204
		NormalizedSubject = 3613,
		// Token: 0x040004B5 RID: 1205
		ObjectType = 4094,
		// Token: 0x040004B6 RID: 1206
		StoreSupportMask = 13325,
		// Token: 0x040004B7 RID: 1207
		ReplicaServer = 26180,
		// Token: 0x040004B8 RID: 1208
		ReplicaVersion = 26187,
		// Token: 0x040004B9 RID: 1209
		MdbProvider = 13332,
		// Token: 0x040004BA RID: 1210
		StoreEntryId = 4091,
		// Token: 0x040004BB RID: 1211
		StoreRecordKey = 4090,
		// Token: 0x040004BC RID: 1212
		LongtermEntryId = 26224,
		// Token: 0x040004BD RID: 1213
		MappingSignature = 4088,
		// Token: 0x040004BE RID: 1214
		MessageSubmissionId = 71,
		// Token: 0x040004BF RID: 1215
		RuleFolderEntryId = 26193,
		// Token: 0x040004C0 RID: 1216
		PostReplyFolderEntries = 4157,
		// Token: 0x040004C1 RID: 1217
		MsgStatus = 3607,
		// Token: 0x040004C2 RID: 1218
		MessageSize = 3592,
		// Token: 0x040004C3 RID: 1219
		AccessLevel = 4087,
		// Token: 0x040004C4 RID: 1220
		Access = 4084,
		// Token: 0x040004C5 RID: 1221
		DisplayTo = 3588,
		// Token: 0x040004C6 RID: 1222
		DisplayCc = 3587,
		// Token: 0x040004C7 RID: 1223
		DisplayBcc = 3586,
		// Token: 0x040004C8 RID: 1224
		HasAttach = 3611,
		// Token: 0x040004C9 RID: 1225
		ConflictEntryId = 16368,
		// Token: 0x040004CA RID: 1226
		SearchKey = 12299,
		// Token: 0x040004CB RID: 1227
		MessageFlags = 3591,
		// Token: 0x040004CC RID: 1228
		DisplayType = 14592,
		// Token: 0x040004CD RID: 1229
		OfflineFlags = 26173,
		// Token: 0x040004CE RID: 1230
		ParentFid = 26441,
		// Token: 0x040004CF RID: 1231
		Depth = 12293,
		// Token: 0x040004D0 RID: 1232
		CodePageId = 26307,
		// Token: 0x040004D1 RID: 1233
		LocaleId = 26273,
		// Token: 0x040004D2 RID: 1234
		SortLocaleId = 26373,
		// Token: 0x040004D3 RID: 1235
		FolderType = 13825,
		// Token: 0x040004D4 RID: 1236
		DisplayName = 12289,
		// Token: 0x040004D5 RID: 1237
		AssociatedCount = 13847,
		// Token: 0x040004D6 RID: 1238
		ContentUnread = 13827,
		// Token: 0x040004D7 RID: 1239
		DeletedMsgCt = 26176,
		// Token: 0x040004D8 RID: 1240
		DeletedAssocMsgCt = 26179,
		// Token: 0x040004D9 RID: 1241
		DeletedFolderCt = 26177,
		// Token: 0x040004DA RID: 1242
		DeletedMessageSizeExtended = 26267,
		// Token: 0x040004DB RID: 1243
		DeletedNormalMessageSizeExtended,
		// Token: 0x040004DC RID: 1244
		DeletedAssocMessageSizeExtended,
		// Token: 0x040004DD RID: 1245
		SubFolders = 13834,
		// Token: 0x040004DE RID: 1246
		FolderChildCt = 26168,
		// Token: 0x040004DF RID: 1247
		Rights,
		// Token: 0x040004E0 RID: 1248
		ACLData = 16352,
		// Token: 0x040004E1 RID: 1249
		ExtendedACLData = 16382,
		// Token: 0x040004E2 RID: 1250
		PublishInAddressBook = 16358,
		// Token: 0x040004E3 RID: 1251
		DesignInProgress = 16356,
		// Token: 0x040004E4 RID: 1252
		AddressBookEntryId = 26171,
		// Token: 0x040004E5 RID: 1253
		RulesData = 16353,
		// Token: 0x040004E6 RID: 1254
		SecureOrigination = 16357,
		// Token: 0x040004E7 RID: 1255
		PromotedProperties = 26181,
		// Token: 0x040004E8 RID: 1256
		RuleFolderFID = 26434,
		// Token: 0x040004E9 RID: 1257
		SentMailSvrEID = 26432,
		// Token: 0x040004EA RID: 1258
		DamOrgMsgSvrEID,
		// Token: 0x040004EB RID: 1259
		MessageSubmissionIdFromClient = 16428,
		// Token: 0x040004EC RID: 1260
		PidMessageReadOnlyMin = 26176,
		// Token: 0x040004ED RID: 1261
		PidMessageReadOnlyMax = 26199,
		// Token: 0x040004EE RID: 1262
		ClientActions = 26181,
		// Token: 0x040004EF RID: 1263
		DamBackPatched = 26183,
		// Token: 0x040004F0 RID: 1264
		RuleError,
		// Token: 0x040004F1 RID: 1265
		RuleActionType,
		// Token: 0x040004F2 RID: 1266
		RuleActionNumber = 26192,
		// Token: 0x040004F3 RID: 1267
		PidInternalNonTransMin = 26432,
		// Token: 0x040004F4 RID: 1268
		PidInternalNonTransMax = 26624,
		// Token: 0x040004F5 RID: 1269
		PidMessageWriteableMin = 26200,
		// Token: 0x040004F6 RID: 1270
		PidMessageWriteableMax = 26219,
		// Token: 0x040004F7 RID: 1271
		IPMSubtreeFolder = 13792,
		// Token: 0x040004F8 RID: 1272
		IPMInboxFolder,
		// Token: 0x040004F9 RID: 1273
		IPMOutboxFolder,
		// Token: 0x040004FA RID: 1274
		IPMSentmailFolder = 13796,
		// Token: 0x040004FB RID: 1275
		IPMWastebasketFolder = 13795,
		// Token: 0x040004FC RID: 1276
		IPMFinderFolder = 13799,
		// Token: 0x040004FD RID: 1277
		IPMShortcutsFolder = 26160,
		// Token: 0x040004FE RID: 1278
		IPMViewsFolder = 13797,
		// Token: 0x040004FF RID: 1279
		IPMCommonViewsFolder,
		// Token: 0x04000500 RID: 1280
		IPMDafFolder = 26143,
		// Token: 0x04000501 RID: 1281
		NonIPMSubtreeFolder,
		// Token: 0x04000502 RID: 1282
		EformsRegistryFolder,
		// Token: 0x04000503 RID: 1283
		SplusFreeBusyFolder,
		// Token: 0x04000504 RID: 1284
		OfflineAddrBookFolder,
		// Token: 0x04000505 RID: 1285
		ArticleIndexFolder = 26250,
		// Token: 0x04000506 RID: 1286
		LocaleEformsRegistryFolder = 26148,
		// Token: 0x04000507 RID: 1287
		LocalSiteFreeBusyFolder,
		// Token: 0x04000508 RID: 1288
		LocalSiteAddrBookFolder,
		// Token: 0x04000509 RID: 1289
		MTSInFolder = 26152,
		// Token: 0x0400050A RID: 1290
		MTSOutFolder,
		// Token: 0x0400050B RID: 1291
		ScheduleFolder = 26142,
		// Token: 0x0400050C RID: 1292
		ValidFolderMask = 13791,
		// Token: 0x0400050D RID: 1293
		OOFState = 26141,
		// Token: 0x0400050E RID: 1294
		OfflineMessageEntryId = 26151,
		// Token: 0x0400050F RID: 1295
		AddrbookMid = 26447,
		// Token: 0x04000510 RID: 1296
		HierarchyServer = 26163,
		// Token: 0x04000511 RID: 1297
		SentMailServerId = 26432,
		// Token: 0x04000512 RID: 1298
		RecipientType = 3093,
		// Token: 0x04000513 RID: 1299
		SendRichInfo = 14912,
		// Token: 0x04000514 RID: 1300
		Responsibility = 3599,
		// Token: 0x04000515 RID: 1301
		RowId = 12288,
		// Token: 0x04000516 RID: 1302
		AddressType = 12290,
		// Token: 0x04000517 RID: 1303
		EmailAddress,
		// Token: 0x04000518 RID: 1304
		SimpleDisplayName = 14847,
		// Token: 0x04000519 RID: 1305
		TransmittableDisplayName = 14880,
		// Token: 0x0400051A RID: 1306
		SendInternetEncoding = 14961,
		// Token: 0x0400051B RID: 1307
		Invalid = 65535
	}
}
