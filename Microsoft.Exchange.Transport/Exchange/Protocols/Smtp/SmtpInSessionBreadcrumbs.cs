using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000439 RID: 1081
	internal enum SmtpInSessionBreadcrumbs
	{
		// Token: 0x04001841 RID: 6209
		Init,
		// Token: 0x04001842 RID: 6210
		Start,
		// Token: 0x04001843 RID: 6211
		OnConnectCompleted,
		// Token: 0x04001844 RID: 6212
		ShutdownConnection,
		// Token: 0x04001845 RID: 6213
		RawDataReceivedCompleted,
		// Token: 0x04001846 RID: 6214
		Shutdown,
		// Token: 0x04001847 RID: 6215
		ShutdownWithArg,
		// Token: 0x04001848 RID: 6216
		RetryShutdownCompleted,
		// Token: 0x04001849 RID: 6217
		Cleanup,
		// Token: 0x0400184A RID: 6218
		ShutdownCompleted,
		// Token: 0x0400184B RID: 6219
		StartProcessingCommand,
		// Token: 0x0400184C RID: 6220
		PostParseCommand,
		// Token: 0x0400184D RID: 6221
		PostParseCommandCompleted,
		// Token: 0x0400184E RID: 6222
		ContinuePostParseCommand,
		// Token: 0x0400184F RID: 6223
		RaiseOnRejectEvent,
		// Token: 0x04001850 RID: 6224
		OnRejectCallback,
		// Token: 0x04001851 RID: 6225
		ContinueOnReject,
		// Token: 0x04001852 RID: 6226
		ProcessCommand,
		// Token: 0x04001853 RID: 6227
		DelayResponseCompleted,
		// Token: 0x04001854 RID: 6228
		DelayResponseIfNecessary,
		// Token: 0x04001855 RID: 6229
		EndProcessingCommand,
		// Token: 0x04001856 RID: 6230
		ReadLineComplete,
		// Token: 0x04001857 RID: 6231
		HandleError,
		// Token: 0x04001858 RID: 6232
		RemoteDisconnected,
		// Token: 0x04001859 RID: 6233
		TlsNegotiationComplete,
		// Token: 0x0400185A RID: 6234
		EHLOInboundParseCommand,
		// Token: 0x0400185B RID: 6235
		EHLOInboundProcessCommand,
		// Token: 0x0400185C RID: 6236
		HELOInboundParseCommand,
		// Token: 0x0400185D RID: 6237
		HELOInboundProcessCommand,
		// Token: 0x0400185E RID: 6238
		MailInboundParseCommand,
		// Token: 0x0400185F RID: 6239
		MailInboundProcessCommand,
		// Token: 0x04001860 RID: 6240
		QuitInboundParseCommand,
		// Token: 0x04001861 RID: 6241
		QuitInboundProcessCommand,
		// Token: 0x04001862 RID: 6242
		RcptInboundParseCommand,
		// Token: 0x04001863 RID: 6243
		RcptInboundProcessCommand,
		// Token: 0x04001864 RID: 6244
		RsetInboundParseCommand,
		// Token: 0x04001865 RID: 6245
		RsetInboundProcessCommand,
		// Token: 0x04001866 RID: 6246
		StarttlsInboundParseCommand,
		// Token: 0x04001867 RID: 6247
		StarttlsInboundProcessCommand,
		// Token: 0x04001868 RID: 6248
		Xexch50InboundParseCommand,
		// Token: 0x04001869 RID: 6249
		Xexch50InboundProcessCommand,
		// Token: 0x0400186A RID: 6250
		WrongSequence,
		// Token: 0x0400186B RID: 6251
		DATAInboundParseCommand,
		// Token: 0x0400186C RID: 6252
		DATAInboundProcessCommand,
		// Token: 0x0400186D RID: 6253
		RaiseOnRejectIfNecessaryAsync,
		// Token: 0x0400186E RID: 6254
		RaiseOnRejectIfNecessarySync,
		// Token: 0x0400186F RID: 6255
		ToShutdown,
		// Token: 0x04001870 RID: 6256
		CloseConnection,
		// Token: 0x04001871 RID: 6257
		Xexch50NotAuthorized,
		// Token: 0x04001872 RID: 6258
		ConnectionDisconnected,
		// Token: 0x04001873 RID: 6259
		StartRead,
		// Token: 0x04001874 RID: 6260
		StartReadLine,
		// Token: 0x04001875 RID: 6261
		RawDataFoundEndOfMessage,
		// Token: 0x04001876 RID: 6262
		DiscardingMessage,
		// Token: 0x04001877 RID: 6263
		ContinueEndOfHeadersAsync,
		// Token: 0x04001878 RID: 6264
		ContinueEndOfHeadersSync,
		// Token: 0x04001879 RID: 6265
		ContinueOnEodAsync,
		// Token: 0x0400187A RID: 6266
		ContinueOnEodSync,
		// Token: 0x0400187B RID: 6267
		RestoreCookedMode,
		// Token: 0x0400187C RID: 6268
		OnEodAsync,
		// Token: 0x0400187D RID: 6269
		OnEodSync,
		// Token: 0x0400187E RID: 6270
		FinishEodSequenceAsync,
		// Token: 0x0400187F RID: 6271
		FinishEodSequenceSync,
		// Token: 0x04001880 RID: 6272
		EodDoneAsync,
		// Token: 0x04001881 RID: 6273
		EodDoneSync,
		// Token: 0x04001882 RID: 6274
		DisconnectRawMode,
		// Token: 0x04001883 RID: 6275
		SubmitTransportMailItemSync,
		// Token: 0x04001884 RID: 6276
		GetCommitResults,
		// Token: 0x04001885 RID: 6277
		CommitCallback,
		// Token: 0x04001886 RID: 6278
		Xexch50NotEnabled,
		// Token: 0x04001887 RID: 6279
		SubmitMessage,
		// Token: 0x04001888 RID: 6280
		SubmitMessageFailed,
		// Token: 0x04001889 RID: 6281
		MessageRejectedByAgent,
		// Token: 0x0400188A RID: 6282
		SubmitMessageFailedNoResponse,
		// Token: 0x0400188B RID: 6283
		RaiseEODEvent,
		// Token: 0x0400188C RID: 6284
		ContinueEndOfData,
		// Token: 0x0400188D RID: 6285
		RaisingOnRejectIfNecessary1,
		// Token: 0x0400188E RID: 6286
		RaisingOnRejectIfNecessary2,
		// Token: 0x0400188F RID: 6287
		RaisingOnRejectIfNecessary3,
		// Token: 0x04001890 RID: 6288
		EndProcessingCommandWriteResponse,
		// Token: 0x04001891 RID: 6289
		EndProcessingCommandWriteResponseToBuffer,
		// Token: 0x04001892 RID: 6290
		ContinueSubmitMessage,
		// Token: 0x04001893 RID: 6291
		ParserEndOfHeadersCallback,
		// Token: 0x04001894 RID: 6292
		DeleteTransportMailItem,
		// Token: 0x04001895 RID: 6293
		RaisingOnRejectIfNecessary4,
		// Token: 0x04001896 RID: 6294
		SessionDisconnectByAgent,
		// Token: 0x04001897 RID: 6295
		DiscardOnException,
		// Token: 0x04001898 RID: 6296
		DisconnectFromContinueOnReject,
		// Token: 0x04001899 RID: 6297
		DisconnectFromContinuePostParseCommand,
		// Token: 0x0400189A RID: 6298
		DisconnectFromRawDataReceivedCompleted,
		// Token: 0x0400189B RID: 6299
		DisconnectFromEndProcessingCommand,
		// Token: 0x0400189C RID: 6300
		RaisingOnRejectIfNecessary5,
		// Token: 0x0400189D RID: 6301
		XShadowInboundParseCommand,
		// Token: 0x0400189E RID: 6302
		XShadowInboundProcessCommand,
		// Token: 0x0400189F RID: 6303
		XShadowNotEnabled,
		// Token: 0x040018A0 RID: 6304
		XQDiscardInboundParseCommand,
		// Token: 0x040018A1 RID: 6305
		XQDiscardInboundProcessCommand,
		// Token: 0x040018A2 RID: 6306
		XShadowNotAuthorized,
		// Token: 0x040018A3 RID: 6307
		DelayedAckStarted,
		// Token: 0x040018A4 RID: 6308
		DelayedAckCompletedByDelivery,
		// Token: 0x040018A5 RID: 6309
		DelayedAckCompletedByExpiry,
		// Token: 0x040018A6 RID: 6310
		DelayedAckCompletedTooEarly,
		// Token: 0x040018A7 RID: 6311
		RaisingOnRejectIfNecessary6,
		// Token: 0x040018A8 RID: 6312
		StartTlsNegotiation,
		// Token: 0x040018A9 RID: 6313
		BeginReadLine,
		// Token: 0x040018AA RID: 6314
		BeginRead,
		// Token: 0x040018AB RID: 6315
		DelayedAckCompletedBySkipping,
		// Token: 0x040018AC RID: 6316
		RaisingOnRejectIfNecessary7,
		// Token: 0x040018AD RID: 6317
		XProxyInboundParseCommand,
		// Token: 0x040018AE RID: 6318
		XProxyInboundProcessCommand,
		// Token: 0x040018AF RID: 6319
		ContinueProcessCommand,
		// Token: 0x040018B0 RID: 6320
		OnMserveLookupComplete,
		// Token: 0x040018B1 RID: 6321
		OnDnsLookupComplete,
		// Token: 0x040018B2 RID: 6322
		ConnectToProxyTarget,
		// Token: 0x040018B3 RID: 6323
		ProxyTargetConnectComplete,
		// Token: 0x040018B4 RID: 6324
		HandleProxySessionDisconnection,
		// Token: 0x040018B5 RID: 6325
		StartProxying,
		// Token: 0x040018B6 RID: 6326
		StartReadFromProxyClient,
		// Token: 0x040018B7 RID: 6327
		StartReadFromProxyTarget,
		// Token: 0x040018B8 RID: 6328
		StartWriteToProxyClient,
		// Token: 0x040018B9 RID: 6329
		StartWriteToProxyTarget,
		// Token: 0x040018BA RID: 6330
		ReadCompleteFromProxyClient,
		// Token: 0x040018BB RID: 6331
		WriteToProxyTargetCompleted,
		// Token: 0x040018BC RID: 6332
		ReadCompleteFromProxyTarget,
		// Token: 0x040018BD RID: 6333
		WriteToProxyClientCompleted,
		// Token: 0x040018BE RID: 6334
		HandleErrorDuringBlindProxying,
		// Token: 0x040018BF RID: 6335
		InboundProxyBaseDataParserEndOfHeadersCallback,
		// Token: 0x040018C0 RID: 6336
		InboundProxyBaseDataRaiseOnRejectIfNecessary,
		// Token: 0x040018C1 RID: 6337
		InboundProxyBaseDataContinueOnReject,
		// Token: 0x040018C2 RID: 6338
		InboundProxyDataInboundParseCommand,
		// Token: 0x040018C3 RID: 6339
		InboundProxyDataInboundProcessCommand,
		// Token: 0x040018C4 RID: 6340
		InboundProxyDataDiscardingMessage,
		// Token: 0x040018C5 RID: 6341
		InboundProxyDataContinueEndOfHeaders,
		// Token: 0x040018C6 RID: 6342
		InboundProxyDataFinishEodSequence,
		// Token: 0x040018C7 RID: 6343
		InboundProxyBdatInboundParseCommand,
		// Token: 0x040018C8 RID: 6344
		InboundProxyBdatInboundProcessCommand,
		// Token: 0x040018C9 RID: 6345
		InboundProxyBdatDiscardingMessage,
		// Token: 0x040018CA RID: 6346
		InboundProxyBdatParserEndOfHeadersCallback,
		// Token: 0x040018CB RID: 6347
		InboundProxyBdatContinueEndOfHeaders,
		// Token: 0x040018CC RID: 6348
		InbountProxyBdatRaiseOnRejectIfNecessary,
		// Token: 0x040018CD RID: 6349
		InboundProxyDataRaiseOnProxyInboundMessageEvent,
		// Token: 0x040018CE RID: 6350
		InboundProxyBdatRaiseOnProxyInboundMessageEvent,
		// Token: 0x040018CF RID: 6351
		InboundProxyBdatContinueOnReject,
		// Token: 0x040018D0 RID: 6352
		InboundProxyBdatFinishEodSequence,
		// Token: 0x040018D1 RID: 6353
		InboundProxySessionDisconnectByAgent,
		// Token: 0x040018D2 RID: 6354
		InboundProxyMessageRejectedByAgent,
		// Token: 0x040018D3 RID: 6355
		XProxyFromInboundParseCommand,
		// Token: 0x040018D4 RID: 6356
		XProxyFromInboundProcessCommand,
		// Token: 0x040018D5 RID: 6357
		XProxyFromNotEnabled,
		// Token: 0x040018D6 RID: 6358
		XProxyFromNotAuthorized,
		// Token: 0x040018D7 RID: 6359
		XShadowRequestInboundParseCommand,
		// Token: 0x040018D8 RID: 6360
		XShadowRequestInboundProcessCommand,
		// Token: 0x040018D9 RID: 6361
		XShadowRequestNotEnabled,
		// Token: 0x040018DA RID: 6362
		XShadowRequestNotAuthorized,
		// Token: 0x040018DB RID: 6363
		XProxyToInboundParseCommand,
		// Token: 0x040018DC RID: 6364
		XProxyToInboundProcessCommand,
		// Token: 0x040018DD RID: 6365
		XProxyToNotEnabled,
		// Token: 0x040018DE RID: 6366
		XProxyToNotAuthorized,
		// Token: 0x040018DF RID: 6367
		XSessionParamsInboundParseCommand,
		// Token: 0x040018E0 RID: 6368
		XSessionParamsInboundProcessCommand,
		// Token: 0x040018E1 RID: 6369
		XSessionParamsNotEnabled,
		// Token: 0x040018E2 RID: 6370
		XSessionParamsNotAuthorized,
		// Token: 0x040018E3 RID: 6371
		XProxyNotAuthorized,
		// Token: 0x040018E4 RID: 6372
		DeserializeExtendedPropertiesBlob,
		// Token: 0x040018E5 RID: 6373
		DeserializeAdrcBlob,
		// Token: 0x040018E6 RID: 6374
		DeserializeFastIndexBlob,
		// Token: 0x040018E7 RID: 6375
		DeserializeBlobFailed,
		// Token: 0x040018E8 RID: 6376
		BdatBlobInboundProcessCommand,
		// Token: 0x040018E9 RID: 6377
		BdatInboundProcessCommand,
		// Token: 0x040018EA RID: 6378
		ShutdownCompletedFromMEx,
		// Token: 0x040018EB RID: 6379
		RaiseOnDisconnectEvent,
		// Token: 0x040018EC RID: 6380
		ShutdownAgentDisconnect,
		// Token: 0x040018ED RID: 6381
		HandleBlindProxySetupSuccess,
		// Token: 0x040018EE RID: 6382
		HandleBlindProxySetupFailure,
		// Token: 0x040018EF RID: 6383
		WriteQuitToProxyTargetCompleted,
		// Token: 0x040018F0 RID: 6384
		WriteXRsetResponseToProxyClientCompleted,
		// Token: 0x040018F1 RID: 6385
		StartWriteQuitToProxyTarget,
		// Token: 0x040018F2 RID: 6386
		ReceivedAndProcessedXRsetProxyToCommand,
		// Token: 0x040018F3 RID: 6387
		StartWriteXRsetResponseToProxyClient,
		// Token: 0x040018F4 RID: 6388
		WriteToProxyClientSkipped,
		// Token: 0x040018F5 RID: 6389
		WriteXRsetResponseToProxyClientSkipped,
		// Token: 0x040018F6 RID: 6390
		MessageDiscardedByAgent,
		// Token: 0x040018F7 RID: 6391
		Rcpt2InboundParseCommand,
		// Token: 0x040018F8 RID: 6392
		Rcpt2InboundProcessCommand,
		// Token: 0x040018F9 RID: 6393
		Rcpt2NotAuthorized,
		// Token: 0x040018FA RID: 6394
		Rcpt2AlreadyReceived
	}
}
