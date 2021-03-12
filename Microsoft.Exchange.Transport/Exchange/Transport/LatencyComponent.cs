using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000179 RID: 377
	internal enum LatencyComponent : ushort
	{
		// Token: 0x0400085B RID: 2139
		None,
		// Token: 0x0400085C RID: 2140
		CategorizerOnResolvedMessage,
		// Token: 0x0400085D RID: 2141
		CategorizerOnRoutedMessage,
		// Token: 0x0400085E RID: 2142
		CategorizerOnSubmittedMessage,
		// Token: 0x0400085F RID: 2143
		CategorizerOnCategorizedMessage,
		// Token: 0x04000860 RID: 2144
		DeliveryAgentOnOpenConnection,
		// Token: 0x04000861 RID: 2145
		DeliveryAgentOnDeliverMailItem,
		// Token: 0x04000862 RID: 2146
		SmtpReceiveOnDataCommand,
		// Token: 0x04000863 RID: 2147
		SmtpReceiveOnEndOfData,
		// Token: 0x04000864 RID: 2148
		SmtpReceiveOnEndOfHeaders,
		// Token: 0x04000865 RID: 2149
		SmtpReceiveOnRcptCommand,
		// Token: 0x04000866 RID: 2150
		SmtpReceiveOnRcpt2Command,
		// Token: 0x04000867 RID: 2151
		SmtpReceiveOnProxyInboundMessage,
		// Token: 0x04000868 RID: 2152
		StoreDriverOnCreatedMessage,
		// Token: 0x04000869 RID: 2153
		StoreDriverOnInitializedMessage,
		// Token: 0x0400086A RID: 2154
		StoreDriverOnPromotedMessage,
		// Token: 0x0400086B RID: 2155
		StoreDriverOnDeliveredMessage,
		// Token: 0x0400086C RID: 2156
		StoreDriverOnDemotedMessage,
		// Token: 0x0400086D RID: 2157
		MailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage,
		// Token: 0x0400086E RID: 2158
		StoreDriverOnCompletedMessage,
		// Token: 0x0400086F RID: 2159
		MaxMExEventComponent,
		// Token: 0x04000870 RID: 2160
		Agent,
		// Token: 0x04000871 RID: 2161
		Categorizer,
		// Token: 0x04000872 RID: 2162
		CategorizerBifurcation,
		// Token: 0x04000873 RID: 2163
		CategorizerContentConversion,
		// Token: 0x04000874 RID: 2164
		CategorizerFinal,
		// Token: 0x04000875 RID: 2165
		CategorizerLocking,
		// Token: 0x04000876 RID: 2166
		CategorizerResolver,
		// Token: 0x04000877 RID: 2167
		CategorizerRouting,
		// Token: 0x04000878 RID: 2168
		ContentAggregation,
		// Token: 0x04000879 RID: 2169
		ContentAggregationMailItemCommit,
		// Token: 0x0400087A RID: 2170
		Deferral,
		// Token: 0x0400087B RID: 2171
		Delivery,
		// Token: 0x0400087C RID: 2172
		DeliveryAgent,
		// Token: 0x0400087D RID: 2173
		DeliveryQueueInternal,
		// Token: 0x0400087E RID: 2174
		DeliveryQueueExternal,
		// Token: 0x0400087F RID: 2175
		DeliveryQueueMailbox,
		// Token: 0x04000880 RID: 2176
		DeliveryQueueMailboxDeliverAgentTransientFailure,
		// Token: 0x04000881 RID: 2177
		DeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded,
		// Token: 0x04000882 RID: 2178
		DeliveryQueueMailboxInsufficientResources,
		// Token: 0x04000883 RID: 2179
		DeliveryQueueMailboxMailboxDatabaseOffline,
		// Token: 0x04000884 RID: 2180
		DeliveryQueueMailboxMailboxServerOffline,
		// Token: 0x04000885 RID: 2181
		DeliveryQueueMailboxMapiExceptionLockViolation,
		// Token: 0x04000886 RID: 2182
		DeliveryQueueMailboxMapiExceptionTimeout,
		// Token: 0x04000887 RID: 2183
		DeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded,
		// Token: 0x04000888 RID: 2184
		DeliveryQueueMailboxRecipientThreadLimitExceeded,
		// Token: 0x04000889 RID: 2185
		DeliveryQueueLocking,
		// Token: 0x0400088A RID: 2186
		DsnGenerator,
		// Token: 0x0400088B RID: 2187
		Dumpster,
		// Token: 0x0400088C RID: 2188
		ExternalServers,
		// Token: 0x0400088D RID: 2189
		ExternalPartnerServers,
		// Token: 0x0400088E RID: 2190
		Heartbeat,
		// Token: 0x0400088F RID: 2191
		MailboxMove,
		// Token: 0x04000890 RID: 2192
		MailboxRules,
		// Token: 0x04000891 RID: 2193
		MailboxTransportSubmissionService,
		// Token: 0x04000892 RID: 2194
		SubmissionAssistant,
		// Token: 0x04000893 RID: 2195
		SubmissionAssistantThrottling,
		// Token: 0x04000894 RID: 2196
		MailboxTransportSubmissionStoreDriverSubmission,
		// Token: 0x04000895 RID: 2197
		MailboxTransportSubmissionStoreDriverSubmissionAD,
		// Token: 0x04000896 RID: 2198
		MailboxTransportSubmissionStoreDriverSubmissionContentConversion,
		// Token: 0x04000897 RID: 2199
		MailboxTransportSubmissionStoreDriverSubmissionHubSelector,
		// Token: 0x04000898 RID: 2200
		MailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap,
		// Token: 0x04000899 RID: 2201
		MailboxTransportSubmissionStoreDriverSubmissionSmtp,
		// Token: 0x0400089A RID: 2202
		MailboxTransportSubmissionStoreDriverSubmissionSmtpOut,
		// Token: 0x0400089B RID: 2203
		MailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession,
		// Token: 0x0400089C RID: 2204
		MailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession,
		// Token: 0x0400089D RID: 2205
		MailboxTransportSubmissionStoreDriverSubmissionStoreStats,
		// Token: 0x0400089E RID: 2206
		MailSubmissionService,
		// Token: 0x0400089F RID: 2207
		MailSubmissionServiceFailedAttempt,
		// Token: 0x040008A0 RID: 2208
		MailSubmissionServiceNotify,
		// Token: 0x040008A1 RID: 2209
		MailSubmissionServiceNotifyRetrySchedule,
		// Token: 0x040008A2 RID: 2210
		MailSubmissionServiceShadowResubmitDecision,
		// Token: 0x040008A3 RID: 2211
		MailSubmissionServiceThrottling,
		// Token: 0x040008A4 RID: 2212
		MexRuntimeThreadpoolQueue,
		// Token: 0x040008A5 RID: 2213
		NonSmtpGateway,
		// Token: 0x040008A6 RID: 2214
		OriginalMailDsn,
		// Token: 0x040008A7 RID: 2215
		Pickup,
		// Token: 0x040008A8 RID: 2216
		PoisonQueue,
		// Token: 0x040008A9 RID: 2217
		ProcessingScheduler,
		// Token: 0x040008AA RID: 2218
		ProcessingSchedulerScoped,
		// Token: 0x040008AB RID: 2219
		QuarantineReleaseOrReport,
		// Token: 0x040008AC RID: 2220
		Replay,
		// Token: 0x040008AD RID: 2221
		RmsAcquireB2BRac,
		// Token: 0x040008AE RID: 2222
		RmsAcquireB2BLicense,
		// Token: 0x040008AF RID: 2223
		RmsAcquireCertificationMexData,
		// Token: 0x040008B0 RID: 2224
		RmsAcquireClc,
		// Token: 0x040008B1 RID: 2225
		RmsAcquireLicense,
		// Token: 0x040008B2 RID: 2226
		RmsAcquireServerLicensingMexData,
		// Token: 0x040008B3 RID: 2227
		RmsAcquirePrelicense,
		// Token: 0x040008B4 RID: 2228
		RmsAcquireServerBoxRac,
		// Token: 0x040008B5 RID: 2229
		RmsAcquireTemplateInfo,
		// Token: 0x040008B6 RID: 2230
		RmsAcquireTemplates,
		// Token: 0x040008B7 RID: 2231
		RmsFindServiceLocation,
		// Token: 0x040008B8 RID: 2232
		RmsRequestDelegationToken,
		// Token: 0x040008B9 RID: 2233
		ServiceRestart,
		// Token: 0x040008BA RID: 2234
		ShadowQueue,
		// Token: 0x040008BB RID: 2235
		SmtpReceive,
		// Token: 0x040008BC RID: 2236
		SmtpReceiveCommit,
		// Token: 0x040008BD RID: 2237
		SmtpReceiveCommitLocal,
		// Token: 0x040008BE RID: 2238
		SmtpReceiveCommitRemote,
		// Token: 0x040008BF RID: 2239
		SmtpReceiveDataInternal,
		// Token: 0x040008C0 RID: 2240
		SmtpReceiveDataExternal,
		// Token: 0x040008C1 RID: 2241
		SmtpSend,
		// Token: 0x040008C2 RID: 2242
		SmtpSendConnect,
		// Token: 0x040008C3 RID: 2243
		SmtpSendMailboxDelivery,
		// Token: 0x040008C4 RID: 2244
		StoreDriverDelivery,
		// Token: 0x040008C5 RID: 2245
		StoreDriverDeliveryAD,
		// Token: 0x040008C6 RID: 2246
		StoreDriverDeliveryContentConversion,
		// Token: 0x040008C7 RID: 2247
		StoreDriverDeliveryMailboxDatabaseThrottling,
		// Token: 0x040008C8 RID: 2248
		StoreDriverDeliveryMessageConcurrency,
		// Token: 0x040008C9 RID: 2249
		StoreDriverDeliveryRpc,
		// Token: 0x040008CA RID: 2250
		StoreDriverDeliveryStore,
		// Token: 0x040008CB RID: 2251
		StoreDriverSubmissionAD,
		// Token: 0x040008CC RID: 2252
		StoreDriverSubmissionRpc,
		// Token: 0x040008CD RID: 2253
		StoreDriverSubmissionStore,
		// Token: 0x040008CE RID: 2254
		StoreDriverSubmit,
		// Token: 0x040008CF RID: 2255
		SubmissionQueue,
		// Token: 0x040008D0 RID: 2256
		UnderThreshold,
		// Token: 0x040008D1 RID: 2257
		Unknown,
		// Token: 0x040008D2 RID: 2258
		UnreachableQueue,
		// Token: 0x040008D3 RID: 2259
		Total,
		// Token: 0x040008D4 RID: 2260
		Process,
		// Token: 0x040008D5 RID: 2261
		TooManyComponents
	}
}
