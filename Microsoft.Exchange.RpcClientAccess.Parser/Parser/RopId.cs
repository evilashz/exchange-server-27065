using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E2 RID: 738
	internal enum RopId : byte
	{
		// Token: 0x04000871 RID: 2161
		None,
		// Token: 0x04000872 RID: 2162
		Release,
		// Token: 0x04000873 RID: 2163
		OpenFolder,
		// Token: 0x04000874 RID: 2164
		OpenMessage,
		// Token: 0x04000875 RID: 2165
		GetHierarchyTable,
		// Token: 0x04000876 RID: 2166
		GetContentsTable,
		// Token: 0x04000877 RID: 2167
		CreateMessage,
		// Token: 0x04000878 RID: 2168
		GetPropertiesSpecific,
		// Token: 0x04000879 RID: 2169
		GetPropertiesAll,
		// Token: 0x0400087A RID: 2170
		GetPropertyList,
		// Token: 0x0400087B RID: 2171
		SetProperties,
		// Token: 0x0400087C RID: 2172
		DeleteProperties,
		// Token: 0x0400087D RID: 2173
		SaveChangesMessage,
		// Token: 0x0400087E RID: 2174
		RemoveAllRecipients,
		// Token: 0x0400087F RID: 2175
		FlushRecipients,
		// Token: 0x04000880 RID: 2176
		ReadRecipients,
		// Token: 0x04000881 RID: 2177
		ReloadCachedInformation,
		// Token: 0x04000882 RID: 2178
		SetReadFlag,
		// Token: 0x04000883 RID: 2179
		SetColumns,
		// Token: 0x04000884 RID: 2180
		SortTable,
		// Token: 0x04000885 RID: 2181
		Restrict,
		// Token: 0x04000886 RID: 2182
		QueryRows,
		// Token: 0x04000887 RID: 2183
		GetStatus,
		// Token: 0x04000888 RID: 2184
		QueryPosition,
		// Token: 0x04000889 RID: 2185
		SeekRow,
		// Token: 0x0400088A RID: 2186
		SeekRowBookmark,
		// Token: 0x0400088B RID: 2187
		SeekRowApproximate,
		// Token: 0x0400088C RID: 2188
		CreateBookmark,
		// Token: 0x0400088D RID: 2189
		CreateFolder,
		// Token: 0x0400088E RID: 2190
		DeleteFolder,
		// Token: 0x0400088F RID: 2191
		DeleteMessages,
		// Token: 0x04000890 RID: 2192
		GetMessageStatus,
		// Token: 0x04000891 RID: 2193
		SetMessageStatus,
		// Token: 0x04000892 RID: 2194
		GetAttachmentTable,
		// Token: 0x04000893 RID: 2195
		OpenAttachment,
		// Token: 0x04000894 RID: 2196
		CreateAttachment,
		// Token: 0x04000895 RID: 2197
		DeleteAttachment,
		// Token: 0x04000896 RID: 2198
		SaveChangesAttachment,
		// Token: 0x04000897 RID: 2199
		SetReceiveFolder,
		// Token: 0x04000898 RID: 2200
		GetReceiveFolder,
		// Token: 0x04000899 RID: 2201
		SpoolerRules,
		// Token: 0x0400089A RID: 2202
		RegisterNotification,
		// Token: 0x0400089B RID: 2203
		Notify,
		// Token: 0x0400089C RID: 2204
		OpenStream,
		// Token: 0x0400089D RID: 2205
		ReadStream,
		// Token: 0x0400089E RID: 2206
		WriteStream,
		// Token: 0x0400089F RID: 2207
		SeekStream,
		// Token: 0x040008A0 RID: 2208
		SetSizeStream,
		// Token: 0x040008A1 RID: 2209
		SetSearchCriteria,
		// Token: 0x040008A2 RID: 2210
		GetSearchCriteria,
		// Token: 0x040008A3 RID: 2211
		SubmitMessage,
		// Token: 0x040008A4 RID: 2212
		MoveCopyMessages,
		// Token: 0x040008A5 RID: 2213
		AbortSubmit,
		// Token: 0x040008A6 RID: 2214
		MoveFolder,
		// Token: 0x040008A7 RID: 2215
		CopyFolder,
		// Token: 0x040008A8 RID: 2216
		QueryColumnsAll,
		// Token: 0x040008A9 RID: 2217
		Abort,
		// Token: 0x040008AA RID: 2218
		CopyTo,
		// Token: 0x040008AB RID: 2219
		CopyToStream,
		// Token: 0x040008AC RID: 2220
		CloneStream,
		// Token: 0x040008AD RID: 2221
		RegisterTableNotification,
		// Token: 0x040008AE RID: 2222
		DeregisterTableNotification,
		// Token: 0x040008AF RID: 2223
		GetPermissionsTable,
		// Token: 0x040008B0 RID: 2224
		GetRulesTable,
		// Token: 0x040008B1 RID: 2225
		ModifyPermissions,
		// Token: 0x040008B2 RID: 2226
		ModifyRules,
		// Token: 0x040008B3 RID: 2227
		GetOwningServers,
		// Token: 0x040008B4 RID: 2228
		LongTermIdFromId,
		// Token: 0x040008B5 RID: 2229
		IdFromLongTermId,
		// Token: 0x040008B6 RID: 2230
		PublicFolderIsGhosted,
		// Token: 0x040008B7 RID: 2231
		OpenEmbeddedMessage,
		// Token: 0x040008B8 RID: 2232
		SetSpooler,
		// Token: 0x040008B9 RID: 2233
		SpoolerLockMessage,
		// Token: 0x040008BA RID: 2234
		AddressTypes,
		// Token: 0x040008BB RID: 2235
		TransportSend,
		// Token: 0x040008BC RID: 2236
		FastTransferSourceCopyMessages,
		// Token: 0x040008BD RID: 2237
		FastTransferSourceCopyFolder,
		// Token: 0x040008BE RID: 2238
		FastTransferSourceCopyTo,
		// Token: 0x040008BF RID: 2239
		FastTransferSourceGetBuffer,
		// Token: 0x040008C0 RID: 2240
		FindRow,
		// Token: 0x040008C1 RID: 2241
		Progress,
		// Token: 0x040008C2 RID: 2242
		TransportNewMail,
		// Token: 0x040008C3 RID: 2243
		GetValidAttachments,
		// Token: 0x040008C4 RID: 2244
		FastTransferDestinationCopyOperationConfigure,
		// Token: 0x040008C5 RID: 2245
		FastTransferDestinationPutBuffer,
		// Token: 0x040008C6 RID: 2246
		GetNamesFromIDs,
		// Token: 0x040008C7 RID: 2247
		GetIdsFromNames,
		// Token: 0x040008C8 RID: 2248
		UpdateDeferredActionMessages,
		// Token: 0x040008C9 RID: 2249
		EmptyFolder,
		// Token: 0x040008CA RID: 2250
		ExpandRow,
		// Token: 0x040008CB RID: 2251
		CollapseRow,
		// Token: 0x040008CC RID: 2252
		LockRegionStream,
		// Token: 0x040008CD RID: 2253
		UnlockRegionStream,
		// Token: 0x040008CE RID: 2254
		CommitStream,
		// Token: 0x040008CF RID: 2255
		GetStreamSize,
		// Token: 0x040008D0 RID: 2256
		QueryNamedProperties,
		// Token: 0x040008D1 RID: 2257
		GetPerUserLongTermIds,
		// Token: 0x040008D2 RID: 2258
		GetPerUserGuid,
		// Token: 0x040008D3 RID: 2259
		FlushPerUser,
		// Token: 0x040008D4 RID: 2260
		ReadPerUserInformation,
		// Token: 0x040008D5 RID: 2261
		WritePerUserInformation,
		// Token: 0x040008D6 RID: 2262
		CacheCcnRead,
		// Token: 0x040008D7 RID: 2263
		SetReadFlags,
		// Token: 0x040008D8 RID: 2264
		CopyProperties,
		// Token: 0x040008D9 RID: 2265
		GetReceiveFolderTable,
		// Token: 0x040008DA RID: 2266
		FastTransferSourceCopyProperties,
		// Token: 0x040008DB RID: 2267
		FXDstCopyProps,
		// Token: 0x040008DC RID: 2268
		GetCollapseState,
		// Token: 0x040008DD RID: 2269
		SetCollapseState,
		// Token: 0x040008DE RID: 2270
		SetTransport,
		// Token: 0x040008DF RID: 2271
		Pending,
		// Token: 0x040008E0 RID: 2272
		GetOptionsData,
		// Token: 0x040008E1 RID: 2273
		IncrementalConfig,
		// Token: 0x040008E2 RID: 2274
		IncrState,
		// Token: 0x040008E3 RID: 2275
		ImportMessageChange,
		// Token: 0x040008E4 RID: 2276
		ImportHierarchyChange,
		// Token: 0x040008E5 RID: 2277
		ImportDelete,
		// Token: 0x040008E6 RID: 2278
		UploadStateStreamBegin,
		// Token: 0x040008E7 RID: 2279
		UploadStateStreamContinue,
		// Token: 0x040008E8 RID: 2280
		UploadStateStreamEnd,
		// Token: 0x040008E9 RID: 2281
		ImportMessageMove,
		// Token: 0x040008EA RID: 2282
		SetPropertiesNoReplicate,
		// Token: 0x040008EB RID: 2283
		DeletePropertiesNoReplicate,
		// Token: 0x040008EC RID: 2284
		GetStoreState,
		// Token: 0x040008ED RID: 2285
		GetEffectiveRights,
		// Token: 0x040008EE RID: 2286
		GetAllPerUserLongTermIds,
		// Token: 0x040008EF RID: 2287
		OpenCollector,
		// Token: 0x040008F0 RID: 2288
		GetLocalReplicationIds,
		// Token: 0x040008F1 RID: 2289
		ImportReads,
		// Token: 0x040008F2 RID: 2290
		ResetTable,
		// Token: 0x040008F3 RID: 2291
		FastTransferGetIncrementalState,
		// Token: 0x040008F4 RID: 2292
		SynchronizationOpenAdvisor,
		// Token: 0x040008F5 RID: 2293
		RegisterSynchronizationNotifications,
		// Token: 0x040008F6 RID: 2294
		OpenCStream,
		// Token: 0x040008F7 RID: 2295
		TellVersion,
		// Token: 0x040008F8 RID: 2296
		OpenFolderByName,
		// Token: 0x040008F9 RID: 2297
		SetSynchronizationNotificationGuid,
		// Token: 0x040008FA RID: 2298
		FreeBookmark,
		// Token: 0x040008FB RID: 2299
		DeleteFolderByName,
		// Token: 0x040008FC RID: 2300
		ConfigNntpNewsfeed,
		// Token: 0x040008FD RID: 2301
		CheckMsgIds,
		// Token: 0x040008FE RID: 2302
		BeginNntpArticle,
		// Token: 0x040008FF RID: 2303
		WriteNntpArticle,
		// Token: 0x04000900 RID: 2304
		SaveNntpArticle,
		// Token: 0x04000901 RID: 2305
		WriteCommitStream,
		// Token: 0x04000902 RID: 2306
		HardDeleteMessages,
		// Token: 0x04000903 RID: 2307
		HardEmptyFolder,
		// Token: 0x04000904 RID: 2308
		SetLocalReplicaMidsetDeleted,
		// Token: 0x04000905 RID: 2309
		TransportDeliverMessage,
		// Token: 0x04000906 RID: 2310
		TransportDoneWithMessage,
		// Token: 0x04000907 RID: 2311
		IdFromLegacyDN,
		// Token: 0x04000908 RID: 2312
		SetAuthenticatedContext,
		// Token: 0x04000909 RID: 2313
		CopyToExtended,
		// Token: 0x0400090A RID: 2314
		ImportMessageChangePartial,
		// Token: 0x0400090B RID: 2315
		SetMessageFlags,
		// Token: 0x0400090C RID: 2316
		MoveCopyMessagesExtended,
		// Token: 0x0400090D RID: 2317
		FastTransferSourceGetBufferExtended,
		// Token: 0x0400090E RID: 2318
		FastTransferDestinationPutBufferExtended,
		// Token: 0x0400090F RID: 2319
		TransportDeliverMessage2,
		// Token: 0x04000910 RID: 2320
		CreateMessageExtended,
		// Token: 0x04000911 RID: 2321
		MoveCopyMessagesExtendedWithEntryIds,
		// Token: 0x04000912 RID: 2322
		TransportDuplicateDeliveryCheck,
		// Token: 0x04000913 RID: 2323
		PrereadMessages,
		// Token: 0x04000914 RID: 2324
		WriteStreamExtended,
		// Token: 0x04000915 RID: 2325
		GetContentsTableExtended,
		// Token: 0x04000916 RID: 2326
		EchoString = 200,
		// Token: 0x04000917 RID: 2327
		EchoInt,
		// Token: 0x04000918 RID: 2328
		EchoBinary,
		// Token: 0x04000919 RID: 2329
		Backoff = 249,
		// Token: 0x0400091A RID: 2330
		DiagnosticContext,
		// Token: 0x0400091B RID: 2331
		BookmarkReturned,
		// Token: 0x0400091C RID: 2332
		FidReturned,
		// Token: 0x0400091D RID: 2333
		SOHTReturned,
		// Token: 0x0400091E RID: 2334
		Logon,
		// Token: 0x0400091F RID: 2335
		BufferTooSmall
	}
}
