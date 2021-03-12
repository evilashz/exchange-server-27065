using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000EB RID: 235
	public enum TnefPropertyId : short
	{
		// Token: 0x04000826 RID: 2086
		Null = 1,
		// Token: 0x04000827 RID: 2087
		PropIdSecureMin = 26608,
		// Token: 0x04000828 RID: 2088
		PropIdSecureMax = 26623,
		// Token: 0x04000829 RID: 2089
		AcknowledgementMode = 1,
		// Token: 0x0400082A RID: 2090
		AlternateRecipientAllowed,
		// Token: 0x0400082B RID: 2091
		AuthorizingUsers,
		// Token: 0x0400082C RID: 2092
		AutoForwardComment,
		// Token: 0x0400082D RID: 2093
		AutoForwarded,
		// Token: 0x0400082E RID: 2094
		ContentConfidentialityAlgorithmId,
		// Token: 0x0400082F RID: 2095
		ContentCorrelator,
		// Token: 0x04000830 RID: 2096
		ContentIdentifier,
		// Token: 0x04000831 RID: 2097
		ContentLength,
		// Token: 0x04000832 RID: 2098
		ContentReturnRequested,
		// Token: 0x04000833 RID: 2099
		ConversationKey,
		// Token: 0x04000834 RID: 2100
		ConversionEits,
		// Token: 0x04000835 RID: 2101
		ConversionWithLossProhibited,
		// Token: 0x04000836 RID: 2102
		ConvertedEits,
		// Token: 0x04000837 RID: 2103
		DeferredDeliveryTime,
		// Token: 0x04000838 RID: 2104
		DeliverTime,
		// Token: 0x04000839 RID: 2105
		DiscardReason,
		// Token: 0x0400083A RID: 2106
		DisclosureOfRecipients,
		// Token: 0x0400083B RID: 2107
		DlExpansionHistory,
		// Token: 0x0400083C RID: 2108
		DlExpansionProhibited,
		// Token: 0x0400083D RID: 2109
		ExpiryTime,
		// Token: 0x0400083E RID: 2110
		ImplicitConversionProhibited,
		// Token: 0x0400083F RID: 2111
		Importance,
		// Token: 0x04000840 RID: 2112
		IpmId,
		// Token: 0x04000841 RID: 2113
		LatestDeliveryTime,
		// Token: 0x04000842 RID: 2114
		MessageClass,
		// Token: 0x04000843 RID: 2115
		MessageDeliveryId,
		// Token: 0x04000844 RID: 2116
		MessageSecurityLabel = 30,
		// Token: 0x04000845 RID: 2117
		ObsoletedIpms,
		// Token: 0x04000846 RID: 2118
		OriginallyIntendedRecipientName,
		// Token: 0x04000847 RID: 2119
		OriginalEits,
		// Token: 0x04000848 RID: 2120
		OriginatorCertificate,
		// Token: 0x04000849 RID: 2121
		OriginatorDeliveryReportRequested,
		// Token: 0x0400084A RID: 2122
		OriginatorReturnAddress,
		// Token: 0x0400084B RID: 2123
		ParentKey,
		// Token: 0x0400084C RID: 2124
		Priority,
		// Token: 0x0400084D RID: 2125
		OriginCheck,
		// Token: 0x0400084E RID: 2126
		ProofOfSubmissionRequested,
		// Token: 0x0400084F RID: 2127
		ReadReceiptRequested,
		// Token: 0x04000850 RID: 2128
		ReceiptTime,
		// Token: 0x04000851 RID: 2129
		RecipientReassignmentProhibited,
		// Token: 0x04000852 RID: 2130
		RedirectionHistory,
		// Token: 0x04000853 RID: 2131
		RelatedIpms,
		// Token: 0x04000854 RID: 2132
		OriginalSensitivity,
		// Token: 0x04000855 RID: 2133
		Languages,
		// Token: 0x04000856 RID: 2134
		ReplyTime,
		// Token: 0x04000857 RID: 2135
		ReportTag,
		// Token: 0x04000858 RID: 2136
		ReportTime,
		// Token: 0x04000859 RID: 2137
		ReturnedIpm,
		// Token: 0x0400085A RID: 2138
		Security,
		// Token: 0x0400085B RID: 2139
		IncompleteCopy,
		// Token: 0x0400085C RID: 2140
		Sensitivity,
		// Token: 0x0400085D RID: 2141
		Subject,
		// Token: 0x0400085E RID: 2142
		SubjectIpm,
		// Token: 0x0400085F RID: 2143
		ClientSubmitTime,
		// Token: 0x04000860 RID: 2144
		ReportName,
		// Token: 0x04000861 RID: 2145
		SentRepresentingSearchKey,
		// Token: 0x04000862 RID: 2146
		X400ContentType,
		// Token: 0x04000863 RID: 2147
		SubjectPrefix,
		// Token: 0x04000864 RID: 2148
		NonReceiptReason,
		// Token: 0x04000865 RID: 2149
		ReceivedByEntryId,
		// Token: 0x04000866 RID: 2150
		ReceivedByName,
		// Token: 0x04000867 RID: 2151
		SentRepresentingEntryId,
		// Token: 0x04000868 RID: 2152
		SentRepresentingName,
		// Token: 0x04000869 RID: 2153
		RcvdRepresentingEntryId,
		// Token: 0x0400086A RID: 2154
		RcvdRepresentingName,
		// Token: 0x0400086B RID: 2155
		ReportEntryId,
		// Token: 0x0400086C RID: 2156
		ReadReceiptEntryId,
		// Token: 0x0400086D RID: 2157
		MessageSubmissionId,
		// Token: 0x0400086E RID: 2158
		ProviderSubmitTime,
		// Token: 0x0400086F RID: 2159
		OriginalSubject,
		// Token: 0x04000870 RID: 2160
		DiscVal,
		// Token: 0x04000871 RID: 2161
		OrigMessageClass,
		// Token: 0x04000872 RID: 2162
		OriginalAuthorEntryId,
		// Token: 0x04000873 RID: 2163
		OriginalAuthorName,
		// Token: 0x04000874 RID: 2164
		OriginalSubmitTime,
		// Token: 0x04000875 RID: 2165
		ReplyRecipientEntries,
		// Token: 0x04000876 RID: 2166
		ReplyRecipientNames,
		// Token: 0x04000877 RID: 2167
		ReceivedBySearchKey,
		// Token: 0x04000878 RID: 2168
		RcvdRepresentingSearchKey,
		// Token: 0x04000879 RID: 2169
		ReadReceiptSearchKey,
		// Token: 0x0400087A RID: 2170
		ReportSearchKey,
		// Token: 0x0400087B RID: 2171
		OriginalDeliveryTime,
		// Token: 0x0400087C RID: 2172
		OriginalAuthorSearchKey,
		// Token: 0x0400087D RID: 2173
		MessageToMe,
		// Token: 0x0400087E RID: 2174
		MessageCcMe,
		// Token: 0x0400087F RID: 2175
		MessageRecipMe,
		// Token: 0x04000880 RID: 2176
		OriginalSenderName,
		// Token: 0x04000881 RID: 2177
		OriginalSenderEntryId,
		// Token: 0x04000882 RID: 2178
		OriginalSenderSearchKey,
		// Token: 0x04000883 RID: 2179
		OriginalSentRepresentingName,
		// Token: 0x04000884 RID: 2180
		OriginalSentRepresentingEntryId,
		// Token: 0x04000885 RID: 2181
		OriginalSentRepresentingSearchKey,
		// Token: 0x04000886 RID: 2182
		StartDate,
		// Token: 0x04000887 RID: 2183
		EndDate,
		// Token: 0x04000888 RID: 2184
		OwnerApptId,
		// Token: 0x04000889 RID: 2185
		ResponseRequested,
		// Token: 0x0400088A RID: 2186
		SentRepresentingAddrtype,
		// Token: 0x0400088B RID: 2187
		SentRepresentingEmailAddress,
		// Token: 0x0400088C RID: 2188
		OriginalSenderAddrtype,
		// Token: 0x0400088D RID: 2189
		OriginalSenderEmailAddress,
		// Token: 0x0400088E RID: 2190
		OriginalSentRepresentingAddrtype,
		// Token: 0x0400088F RID: 2191
		OriginalSentRepresentingEmailAddress,
		// Token: 0x04000890 RID: 2192
		ConversationTopic = 112,
		// Token: 0x04000891 RID: 2193
		ConversationIndex,
		// Token: 0x04000892 RID: 2194
		OriginalDisplayBcc,
		// Token: 0x04000893 RID: 2195
		OriginalDisplayCc,
		// Token: 0x04000894 RID: 2196
		OriginalDisplayTo,
		// Token: 0x04000895 RID: 2197
		ReceivedByAddrtype,
		// Token: 0x04000896 RID: 2198
		ReceivedByEmailAddress,
		// Token: 0x04000897 RID: 2199
		RcvdRepresentingAddrtype,
		// Token: 0x04000898 RID: 2200
		RcvdRepresentingEmailAddress,
		// Token: 0x04000899 RID: 2201
		OriginalAuthorAddrtype,
		// Token: 0x0400089A RID: 2202
		OriginalAuthorEmailAddress,
		// Token: 0x0400089B RID: 2203
		OriginallyIntendedRecipAddrtype,
		// Token: 0x0400089C RID: 2204
		OriginallyIntendedRecipEmailAddress,
		// Token: 0x0400089D RID: 2205
		TransportMessageHeaders,
		// Token: 0x0400089E RID: 2206
		Delegation,
		// Token: 0x0400089F RID: 2207
		TnefCorrelationKey,
		// Token: 0x040008A0 RID: 2208
		ContentIntegrityCheck = 3072,
		// Token: 0x040008A1 RID: 2209
		ExplicitConversion,
		// Token: 0x040008A2 RID: 2210
		IpmReturnRequested,
		// Token: 0x040008A3 RID: 2211
		MessageToken,
		// Token: 0x040008A4 RID: 2212
		NdrReasonCode,
		// Token: 0x040008A5 RID: 2213
		NdrDiagCode,
		// Token: 0x040008A6 RID: 2214
		NonReceiptNotificationRequested,
		// Token: 0x040008A7 RID: 2215
		DeliveryPoint,
		// Token: 0x040008A8 RID: 2216
		OriginatorNonDeliveryReportRequested,
		// Token: 0x040008A9 RID: 2217
		OriginatorRequestedAlternateRecipient,
		// Token: 0x040008AA RID: 2218
		PhysicalDeliveryBureauFaxDelivery,
		// Token: 0x040008AB RID: 2219
		PhysicalDeliveryMode,
		// Token: 0x040008AC RID: 2220
		PhysicalDeliveryReportRequest,
		// Token: 0x040008AD RID: 2221
		PhysicalForwardingAddress,
		// Token: 0x040008AE RID: 2222
		PhysicalForwardingAddressRequested,
		// Token: 0x040008AF RID: 2223
		PhysicalForwardingProhibited,
		// Token: 0x040008B0 RID: 2224
		PhysicalRenditionAttributes,
		// Token: 0x040008B1 RID: 2225
		ProofOfDelivery,
		// Token: 0x040008B2 RID: 2226
		ProofOfDeliveryRequested,
		// Token: 0x040008B3 RID: 2227
		RecipientCertificate,
		// Token: 0x040008B4 RID: 2228
		RecipientNumberForAdvice,
		// Token: 0x040008B5 RID: 2229
		RecipientType,
		// Token: 0x040008B6 RID: 2230
		RegisteredMailType,
		// Token: 0x040008B7 RID: 2231
		ReplyRequested,
		// Token: 0x040008B8 RID: 2232
		RequestedDeliveryMethod,
		// Token: 0x040008B9 RID: 2233
		SenderEntryId,
		// Token: 0x040008BA RID: 2234
		SenderName,
		// Token: 0x040008BB RID: 2235
		SupplementaryInfo,
		// Token: 0x040008BC RID: 2236
		TypeOfMtsUser,
		// Token: 0x040008BD RID: 2237
		SenderSearchKey,
		// Token: 0x040008BE RID: 2238
		SenderAddrtype,
		// Token: 0x040008BF RID: 2239
		SenderEmailAddress,
		// Token: 0x040008C0 RID: 2240
		NdrStatusCode,
		// Token: 0x040008C1 RID: 2241
		CurrentVersion = 3584,
		// Token: 0x040008C2 RID: 2242
		DeleteAfterSubmit,
		// Token: 0x040008C3 RID: 2243
		DisplayBcc,
		// Token: 0x040008C4 RID: 2244
		DisplayCc,
		// Token: 0x040008C5 RID: 2245
		DisplayTo,
		// Token: 0x040008C6 RID: 2246
		ParentDisplay,
		// Token: 0x040008C7 RID: 2247
		MessageDeliveryTime,
		// Token: 0x040008C8 RID: 2248
		MessageFlags,
		// Token: 0x040008C9 RID: 2249
		MessageSize,
		// Token: 0x040008CA RID: 2250
		ParentEntryId,
		// Token: 0x040008CB RID: 2251
		SentmailEntryId,
		// Token: 0x040008CC RID: 2252
		Correlate = 3596,
		// Token: 0x040008CD RID: 2253
		CorrelateMtsid,
		// Token: 0x040008CE RID: 2254
		DiscreteValues,
		// Token: 0x040008CF RID: 2255
		Responsibility,
		// Token: 0x040008D0 RID: 2256
		SpoolerStatus,
		// Token: 0x040008D1 RID: 2257
		TransportStatus,
		// Token: 0x040008D2 RID: 2258
		MessageRecipients,
		// Token: 0x040008D3 RID: 2259
		MessageAttachments,
		// Token: 0x040008D4 RID: 2260
		SubmitFlags,
		// Token: 0x040008D5 RID: 2261
		RecipientStatus,
		// Token: 0x040008D6 RID: 2262
		TransportKey,
		// Token: 0x040008D7 RID: 2263
		MsgStatus,
		// Token: 0x040008D8 RID: 2264
		MessageDownloadTime,
		// Token: 0x040008D9 RID: 2265
		CreationVersion,
		// Token: 0x040008DA RID: 2266
		ModifyVersion,
		// Token: 0x040008DB RID: 2267
		Hasattach,
		// Token: 0x040008DC RID: 2268
		BodyCrc,
		// Token: 0x040008DD RID: 2269
		NormalizedSubject,
		// Token: 0x040008DE RID: 2270
		RtfInSync = 3615,
		// Token: 0x040008DF RID: 2271
		AttachSize,
		// Token: 0x040008E0 RID: 2272
		AttachNum,
		// Token: 0x040008E1 RID: 2273
		Preprocess,
		// Token: 0x040008E2 RID: 2274
		InternetArticleNumber,
		// Token: 0x040008E3 RID: 2275
		NewsgroupName,
		// Token: 0x040008E4 RID: 2276
		OriginatingMtaCertificate,
		// Token: 0x040008E5 RID: 2277
		ProofOfSubmission,
		// Token: 0x040008E6 RID: 2278
		NtSecurityDescriptor,
		// Token: 0x040008E7 RID: 2279
		Access = 4084,
		// Token: 0x040008E8 RID: 2280
		RowType,
		// Token: 0x040008E9 RID: 2281
		InstanceKey,
		// Token: 0x040008EA RID: 2282
		AccessLevel,
		// Token: 0x040008EB RID: 2283
		MappingSignature,
		// Token: 0x040008EC RID: 2284
		RecordKey,
		// Token: 0x040008ED RID: 2285
		StoreRecordKey,
		// Token: 0x040008EE RID: 2286
		StoreEntryId,
		// Token: 0x040008EF RID: 2287
		MiniIcon,
		// Token: 0x040008F0 RID: 2288
		Icon,
		// Token: 0x040008F1 RID: 2289
		ObjectType,
		// Token: 0x040008F2 RID: 2290
		EntryId,
		// Token: 0x040008F3 RID: 2291
		Body,
		// Token: 0x040008F4 RID: 2292
		ReportText,
		// Token: 0x040008F5 RID: 2293
		OriginatorAndDlExpansionHistory,
		// Token: 0x040008F6 RID: 2294
		ReportingDlName,
		// Token: 0x040008F7 RID: 2295
		ReportingMtaCertificate,
		// Token: 0x040008F8 RID: 2296
		RtfSyncBodyCrc = 4102,
		// Token: 0x040008F9 RID: 2297
		RtfSyncBodyCount,
		// Token: 0x040008FA RID: 2298
		RtfSyncBodyTag,
		// Token: 0x040008FB RID: 2299
		RtfCompressed,
		// Token: 0x040008FC RID: 2300
		RtfSyncPrefixCount = 4112,
		// Token: 0x040008FD RID: 2301
		RtfSyncTrailingCount,
		// Token: 0x040008FE RID: 2302
		OriginallyIntendedRecipEntryId,
		// Token: 0x040008FF RID: 2303
		BodyHtml,
		// Token: 0x04000900 RID: 2304
		BodyContentLocation,
		// Token: 0x04000901 RID: 2305
		BodyContentId,
		// Token: 0x04000902 RID: 2306
		InternetApproved = 4144,
		// Token: 0x04000903 RID: 2307
		InternetControl,
		// Token: 0x04000904 RID: 2308
		InternetDistribution,
		// Token: 0x04000905 RID: 2309
		InternetFollowupTo,
		// Token: 0x04000906 RID: 2310
		InternetLines,
		// Token: 0x04000907 RID: 2311
		InternetMessageId,
		// Token: 0x04000908 RID: 2312
		InternetNewsgroups,
		// Token: 0x04000909 RID: 2313
		InternetOrganization,
		// Token: 0x0400090A RID: 2314
		InternetNntpPath,
		// Token: 0x0400090B RID: 2315
		InternetReferences,
		// Token: 0x0400090C RID: 2316
		Supersedes,
		// Token: 0x0400090D RID: 2317
		PostFolderEntries,
		// Token: 0x0400090E RID: 2318
		PostFolderNames,
		// Token: 0x0400090F RID: 2319
		PostReplyFolderEntries,
		// Token: 0x04000910 RID: 2320
		PostReplyFolderNames,
		// Token: 0x04000911 RID: 2321
		PostReplyDenied,
		// Token: 0x04000912 RID: 2322
		NntpXref,
		// Token: 0x04000913 RID: 2323
		InternetPrecedence,
		// Token: 0x04000914 RID: 2324
		InReplyToId,
		// Token: 0x04000915 RID: 2325
		ListHelp,
		// Token: 0x04000916 RID: 2326
		ListSubscribe,
		// Token: 0x04000917 RID: 2327
		ListUnsubscribe,
		// Token: 0x04000918 RID: 2328
		Rowid = 12288,
		// Token: 0x04000919 RID: 2329
		DisplayName,
		// Token: 0x0400091A RID: 2330
		Addrtype,
		// Token: 0x0400091B RID: 2331
		EmailAddress,
		// Token: 0x0400091C RID: 2332
		Comment,
		// Token: 0x0400091D RID: 2333
		Depth,
		// Token: 0x0400091E RID: 2334
		ProviderDisplay,
		// Token: 0x0400091F RID: 2335
		CreationTime,
		// Token: 0x04000920 RID: 2336
		LastModificationTime,
		// Token: 0x04000921 RID: 2337
		ResourceFlags,
		// Token: 0x04000922 RID: 2338
		ProviderDllName,
		// Token: 0x04000923 RID: 2339
		SearchKey,
		// Token: 0x04000924 RID: 2340
		ProviderUid,
		// Token: 0x04000925 RID: 2341
		ProviderOrdinal,
		// Token: 0x04000926 RID: 2342
		Puid,
		// Token: 0x04000927 RID: 2343
		OrigEntryId,
		// Token: 0x04000928 RID: 2344
		FormVersion = 13057,
		// Token: 0x04000929 RID: 2345
		FormClsid,
		// Token: 0x0400092A RID: 2346
		FormContactName,
		// Token: 0x0400092B RID: 2347
		FormCategory,
		// Token: 0x0400092C RID: 2348
		FormCategorySub,
		// Token: 0x0400092D RID: 2349
		FormHostMap,
		// Token: 0x0400092E RID: 2350
		FormHidden,
		// Token: 0x0400092F RID: 2351
		FormDesignerName,
		// Token: 0x04000930 RID: 2352
		FormDesignerGuid,
		// Token: 0x04000931 RID: 2353
		FormMessageBehavior,
		// Token: 0x04000932 RID: 2354
		DefaultStore = 13312,
		// Token: 0x04000933 RID: 2355
		StoreSupportMask = 13325,
		// Token: 0x04000934 RID: 2356
		StoreState,
		// Token: 0x04000935 RID: 2357
		IpmSubtreeSearchKey = 13328,
		// Token: 0x04000936 RID: 2358
		IpmOutboxSearchKey,
		// Token: 0x04000937 RID: 2359
		IpmWastebasketSearchKey,
		// Token: 0x04000938 RID: 2360
		IpmSentmailSearchKey,
		// Token: 0x04000939 RID: 2361
		MdbProvider,
		// Token: 0x0400093A RID: 2362
		ReceiveFolderSettings,
		// Token: 0x0400093B RID: 2363
		ValidFolderMask = 13791,
		// Token: 0x0400093C RID: 2364
		IpmSubtreeEntryId,
		// Token: 0x0400093D RID: 2365
		IpmOutboxEntryId = 13794,
		// Token: 0x0400093E RID: 2366
		IpmWastebasketEntryId,
		// Token: 0x0400093F RID: 2367
		IpmSentmailEntryId,
		// Token: 0x04000940 RID: 2368
		ViewsEntryId,
		// Token: 0x04000941 RID: 2369
		CommonViewsEntryId,
		// Token: 0x04000942 RID: 2370
		FinderEntryId,
		// Token: 0x04000943 RID: 2371
		ContainerFlags = 13824,
		// Token: 0x04000944 RID: 2372
		FolderType,
		// Token: 0x04000945 RID: 2373
		ContentCount,
		// Token: 0x04000946 RID: 2374
		ContentUnread,
		// Token: 0x04000947 RID: 2375
		CreateTemplates,
		// Token: 0x04000948 RID: 2376
		DetailsTable,
		// Token: 0x04000949 RID: 2377
		Search = 13831,
		// Token: 0x0400094A RID: 2378
		Selectable = 13833,
		// Token: 0x0400094B RID: 2379
		Subfolders,
		// Token: 0x0400094C RID: 2380
		Status,
		// Token: 0x0400094D RID: 2381
		Anr,
		// Token: 0x0400094E RID: 2382
		ContentsSortOrder,
		// Token: 0x0400094F RID: 2383
		ContainerHierarchy,
		// Token: 0x04000950 RID: 2384
		ContainerContents,
		// Token: 0x04000951 RID: 2385
		FolderAssociatedContents,
		// Token: 0x04000952 RID: 2386
		DefCreateDl,
		// Token: 0x04000953 RID: 2387
		DefCreateMailuser,
		// Token: 0x04000954 RID: 2388
		ContainerClass,
		// Token: 0x04000955 RID: 2389
		ContainerModifyVersion,
		// Token: 0x04000956 RID: 2390
		AbProviderId,
		// Token: 0x04000957 RID: 2391
		DefaultViewEntryId,
		// Token: 0x04000958 RID: 2392
		AssocContentCount,
		// Token: 0x04000959 RID: 2393
		ExpandBeginTime,
		// Token: 0x0400095A RID: 2394
		ExpandEndTime,
		// Token: 0x0400095B RID: 2395
		ExpandedBeginTime,
		// Token: 0x0400095C RID: 2396
		ExpandedEndTime,
		// Token: 0x0400095D RID: 2397
		AttachmentX400Parameters = 14080,
		// Token: 0x0400095E RID: 2398
		AttachData,
		// Token: 0x0400095F RID: 2399
		AttachEncoding,
		// Token: 0x04000960 RID: 2400
		AttachExtension,
		// Token: 0x04000961 RID: 2401
		AttachFilename,
		// Token: 0x04000962 RID: 2402
		AttachMethod,
		// Token: 0x04000963 RID: 2403
		AttachLongFilename = 14087,
		// Token: 0x04000964 RID: 2404
		AttachPathname,
		// Token: 0x04000965 RID: 2405
		AttachRendering,
		// Token: 0x04000966 RID: 2406
		AttachTag,
		// Token: 0x04000967 RID: 2407
		RenderingPosition,
		// Token: 0x04000968 RID: 2408
		AttachTransportName,
		// Token: 0x04000969 RID: 2409
		AttachLongPathname,
		// Token: 0x0400096A RID: 2410
		AttachMimeTag,
		// Token: 0x0400096B RID: 2411
		AttachAdditionalInfo,
		// Token: 0x0400096C RID: 2412
		AttachMimeSequence,
		// Token: 0x0400096D RID: 2413
		AttachContentBase,
		// Token: 0x0400096E RID: 2414
		AttachContentId,
		// Token: 0x0400096F RID: 2415
		AttachContentLocation,
		// Token: 0x04000970 RID: 2416
		AttachFlags,
		// Token: 0x04000971 RID: 2417
		AttachNetscapeMacInfo,
		// Token: 0x04000972 RID: 2418
		AttachDisposition,
		// Token: 0x04000973 RID: 2419
		LockBranchId = 14336,
		// Token: 0x04000974 RID: 2420
		LockResourceFid,
		// Token: 0x04000975 RID: 2421
		LockResourceDid,
		// Token: 0x04000976 RID: 2422
		LockResourceMid,
		// Token: 0x04000977 RID: 2423
		LockEnlistmentContext,
		// Token: 0x04000978 RID: 2424
		LockType,
		// Token: 0x04000979 RID: 2425
		LockScope,
		// Token: 0x0400097A RID: 2426
		LockPersistent,
		// Token: 0x0400097B RID: 2427
		LockDepth,
		// Token: 0x0400097C RID: 2428
		LockTimeout,
		// Token: 0x0400097D RID: 2429
		LockExpiryTime,
		// Token: 0x0400097E RID: 2430
		DisplayType = 14592,
		// Token: 0x0400097F RID: 2431
		Templateid = 14594,
		// Token: 0x04000980 RID: 2432
		PrimaryCapability = 14596,
		// Token: 0x04000981 RID: 2433
		SmtpAddress = 14846,
		// Token: 0x04000982 RID: 2434
		SevenBitDisplayName,
		// Token: 0x04000983 RID: 2435
		Account,
		// Token: 0x04000984 RID: 2436
		AlternateRecipient,
		// Token: 0x04000985 RID: 2437
		CallbackTelephoneNumber,
		// Token: 0x04000986 RID: 2438
		ConversionProhibited,
		// Token: 0x04000987 RID: 2439
		DiscloseRecipients,
		// Token: 0x04000988 RID: 2440
		Generation,
		// Token: 0x04000989 RID: 2441
		GivenName,
		// Token: 0x0400098A RID: 2442
		GovernmentIdNumber,
		// Token: 0x0400098B RID: 2443
		OfficeTelephoneNumber,
		// Token: 0x0400098C RID: 2444
		HomeTelephoneNumber,
		// Token: 0x0400098D RID: 2445
		Initials,
		// Token: 0x0400098E RID: 2446
		Keyword,
		// Token: 0x0400098F RID: 2447
		Language,
		// Token: 0x04000990 RID: 2448
		Location,
		// Token: 0x04000991 RID: 2449
		MailPermission,
		// Token: 0x04000992 RID: 2450
		MhsCommonName,
		// Token: 0x04000993 RID: 2451
		OrganizationalIdNumber,
		// Token: 0x04000994 RID: 2452
		Surname,
		// Token: 0x04000995 RID: 2453
		OriginalEntryId,
		// Token: 0x04000996 RID: 2454
		OriginalDisplayName,
		// Token: 0x04000997 RID: 2455
		OriginalSearchKey,
		// Token: 0x04000998 RID: 2456
		PostalAddress,
		// Token: 0x04000999 RID: 2457
		CompanyName,
		// Token: 0x0400099A RID: 2458
		Title,
		// Token: 0x0400099B RID: 2459
		DepartmentName,
		// Token: 0x0400099C RID: 2460
		OfficeLocation,
		// Token: 0x0400099D RID: 2461
		PrimaryTelephoneNumber,
		// Token: 0x0400099E RID: 2462
		Office2TelephoneNumber,
		// Token: 0x0400099F RID: 2463
		Business2TelephoneNumber = 14875,
		// Token: 0x040009A0 RID: 2464
		MobileTelephoneNumber,
		// Token: 0x040009A1 RID: 2465
		RadioTelephoneNumber,
		// Token: 0x040009A2 RID: 2466
		CarTelephoneNumber,
		// Token: 0x040009A3 RID: 2467
		OtherTelephoneNumber,
		// Token: 0x040009A4 RID: 2468
		TransmitableDisplayName,
		// Token: 0x040009A5 RID: 2469
		PagerTelephoneNumber,
		// Token: 0x040009A6 RID: 2470
		BeeperTelephoneNumber = 14881,
		// Token: 0x040009A7 RID: 2471
		UserCertificate,
		// Token: 0x040009A8 RID: 2472
		PrimaryFaxNumber,
		// Token: 0x040009A9 RID: 2473
		BusinessFaxNumber,
		// Token: 0x040009AA RID: 2474
		HomeFaxNumber,
		// Token: 0x040009AB RID: 2475
		BusinessAddressCountry,
		// Token: 0x040009AC RID: 2476
		Country = 14886,
		// Token: 0x040009AD RID: 2477
		BusinessAddressCity,
		// Token: 0x040009AE RID: 2478
		Locality = 14887,
		// Token: 0x040009AF RID: 2479
		StateOrProvince,
		// Token: 0x040009B0 RID: 2480
		BusinessAddressStreet,
		// Token: 0x040009B1 RID: 2481
		StreetAddress = 14889,
		// Token: 0x040009B2 RID: 2482
		PostalCode,
		// Token: 0x040009B3 RID: 2483
		BusinessAddressPostalCode = 14890,
		// Token: 0x040009B4 RID: 2484
		PostOfficeBox,
		// Token: 0x040009B5 RID: 2485
		TelexNumber,
		// Token: 0x040009B6 RID: 2486
		IsdnNumber,
		// Token: 0x040009B7 RID: 2487
		AssistantTelephoneNumber,
		// Token: 0x040009B8 RID: 2488
		Home2TelephoneNumber,
		// Token: 0x040009B9 RID: 2489
		Assistant,
		// Token: 0x040009BA RID: 2490
		SendRichInfo = 14912,
		// Token: 0x040009BB RID: 2491
		WeddingAnniversary,
		// Token: 0x040009BC RID: 2492
		Birthday,
		// Token: 0x040009BD RID: 2493
		Hobbies,
		// Token: 0x040009BE RID: 2494
		MiddleName,
		// Token: 0x040009BF RID: 2495
		DisplayNamePrefix,
		// Token: 0x040009C0 RID: 2496
		Profession,
		// Token: 0x040009C1 RID: 2497
		ReferredByName,
		// Token: 0x040009C2 RID: 2498
		PreferredByName = 14919,
		// Token: 0x040009C3 RID: 2499
		SpouseName,
		// Token: 0x040009C4 RID: 2500
		ComputerNetworkName,
		// Token: 0x040009C5 RID: 2501
		CustomerId,
		// Token: 0x040009C6 RID: 2502
		TtytddPhoneNumber,
		// Token: 0x040009C7 RID: 2503
		FtpSite,
		// Token: 0x040009C8 RID: 2504
		Gender,
		// Token: 0x040009C9 RID: 2505
		ManagerName,
		// Token: 0x040009CA RID: 2506
		Nickname,
		// Token: 0x040009CB RID: 2507
		PersonalHomePage,
		// Token: 0x040009CC RID: 2508
		BusinessHomePage,
		// Token: 0x040009CD RID: 2509
		ContactVersion,
		// Token: 0x040009CE RID: 2510
		ContactEntryIds,
		// Token: 0x040009CF RID: 2511
		ContactAddrtypes,
		// Token: 0x040009D0 RID: 2512
		ContactDefaultAddressIndex,
		// Token: 0x040009D1 RID: 2513
		ContactEmailAddresses,
		// Token: 0x040009D2 RID: 2514
		CompanyMainPhoneNumber,
		// Token: 0x040009D3 RID: 2515
		ChildrensNames,
		// Token: 0x040009D4 RID: 2516
		HomeAddressCity,
		// Token: 0x040009D5 RID: 2517
		HomeAddressCountry,
		// Token: 0x040009D6 RID: 2518
		HomeAddressPostalCode,
		// Token: 0x040009D7 RID: 2519
		HomeAddressStateOrProvince,
		// Token: 0x040009D8 RID: 2520
		HomeAddressStreet,
		// Token: 0x040009D9 RID: 2521
		HomeAddressPostOfficeBox,
		// Token: 0x040009DA RID: 2522
		OtherAddressCity,
		// Token: 0x040009DB RID: 2523
		OtherAddressCountry,
		// Token: 0x040009DC RID: 2524
		OtherAddressPostalCode,
		// Token: 0x040009DD RID: 2525
		OtherAddressStateOrProvince,
		// Token: 0x040009DE RID: 2526
		OtherAddressStreet,
		// Token: 0x040009DF RID: 2527
		OtherAddressPostOfficeBox,
		// Token: 0x040009E0 RID: 2528
		UserX509Certificate = 14960,
		// Token: 0x040009E1 RID: 2529
		SendInternetEncoding,
		// Token: 0x040009E2 RID: 2530
		StoreProviders = 15616,
		// Token: 0x040009E3 RID: 2531
		AbProviders,
		// Token: 0x040009E4 RID: 2532
		TransportProviders,
		// Token: 0x040009E5 RID: 2533
		DefaultProfile = 15620,
		// Token: 0x040009E6 RID: 2534
		AbSearchPath,
		// Token: 0x040009E7 RID: 2535
		AbDefaultDir,
		// Token: 0x040009E8 RID: 2536
		AbDefaultPab,
		// Token: 0x040009E9 RID: 2537
		FilteringHooks,
		// Token: 0x040009EA RID: 2538
		ServiceName,
		// Token: 0x040009EB RID: 2539
		ServiceDllName,
		// Token: 0x040009EC RID: 2540
		ServiceEntryName,
		// Token: 0x040009ED RID: 2541
		ServiceUid,
		// Token: 0x040009EE RID: 2542
		ServiceExtraUids,
		// Token: 0x040009EF RID: 2543
		Services,
		// Token: 0x040009F0 RID: 2544
		ServiceSupportFiles,
		// Token: 0x040009F1 RID: 2545
		ServiceDeleteFiles,
		// Token: 0x040009F2 RID: 2546
		AbSearchPathUpdate,
		// Token: 0x040009F3 RID: 2547
		ProfileName,
		// Token: 0x040009F4 RID: 2548
		IdentityDisplay = 15872,
		// Token: 0x040009F5 RID: 2549
		IdentityEntryId,
		// Token: 0x040009F6 RID: 2550
		ResourceMethods,
		// Token: 0x040009F7 RID: 2551
		ResourceType,
		// Token: 0x040009F8 RID: 2552
		StatusCode,
		// Token: 0x040009F9 RID: 2553
		IdentitySearchKey,
		// Token: 0x040009FA RID: 2554
		OwnStoreEntryId,
		// Token: 0x040009FB RID: 2555
		ResourcePath,
		// Token: 0x040009FC RID: 2556
		StatusString,
		// Token: 0x040009FD RID: 2557
		X400DeferredDeliveryCancel,
		// Token: 0x040009FE RID: 2558
		HeaderFolderEntryId,
		// Token: 0x040009FF RID: 2559
		RemoteProgress,
		// Token: 0x04000A00 RID: 2560
		RemoteProgressText,
		// Token: 0x04000A01 RID: 2561
		RemoteValidateOk,
		// Token: 0x04000A02 RID: 2562
		ControlFlags = 16128,
		// Token: 0x04000A03 RID: 2563
		ControlStructure,
		// Token: 0x04000A04 RID: 2564
		ControlType,
		// Token: 0x04000A05 RID: 2565
		Deltax,
		// Token: 0x04000A06 RID: 2566
		Deltay,
		// Token: 0x04000A07 RID: 2567
		Xpos,
		// Token: 0x04000A08 RID: 2568
		Ypos,
		// Token: 0x04000A09 RID: 2569
		ControlId,
		// Token: 0x04000A0A RID: 2570
		InitialDetailsPane,
		// Token: 0x04000A0B RID: 2571
		InternetCPID = 16350,
		// Token: 0x04000A0C RID: 2572
		AutoResponseSuppress,
		// Token: 0x04000A0D RID: 2573
		MessageLocaleID = 16369,
		// Token: 0x04000A0E RID: 2574
		MessageCodepage = 16381,
		// Token: 0x04000A0F RID: 2575
		OofReplyType = 16512,
		// Token: 0x04000A10 RID: 2576
		INetMailOverrideFormat = 22786,
		// Token: 0x04000A11 RID: 2577
		INetMailOverrideCharset,
		// Token: 0x04000A12 RID: 2578
		LocallyDelivered = 26437,
		// Token: 0x04000A13 RID: 2579
		SendRecallReport = 26627,
		// Token: 0x04000A14 RID: 2580
		AttachmentFlags = 32765,
		// Token: 0x04000A15 RID: 2581
		AttachHidden
	}
}
