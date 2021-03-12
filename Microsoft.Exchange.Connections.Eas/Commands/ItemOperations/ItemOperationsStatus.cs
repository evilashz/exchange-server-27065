using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.ItemOperations
{
	// Token: 0x02000054 RID: 84
	[Flags]
	public enum ItemOperationsStatus
	{
		// Token: 0x04000161 RID: 353
		Success = 1,
		// Token: 0x04000162 RID: 354
		ProtocolError = 4098,
		// Token: 0x04000163 RID: 355
		ServerError = 259,
		// Token: 0x04000164 RID: 356
		DocLibAcccessError = 4100,
		// Token: 0x04000165 RID: 357
		DocLibAccessDenied = 4101,
		// Token: 0x04000166 RID: 358
		DocLibObjectNotFoundOrAccessDenied = 4102,
		// Token: 0x04000167 RID: 359
		DocLibFailedToConnectToServer = 263,
		// Token: 0x04000168 RID: 360
		BadByteRange = 4104,
		// Token: 0x04000169 RID: 361
		BadStore = 4105,
		// Token: 0x0400016A RID: 362
		FileIsEmpty = 266,
		// Token: 0x0400016B RID: 363
		RequestedDataSizeTooLarge = 267,
		// Token: 0x0400016C RID: 364
		DownloadFailure = 268,
		// Token: 0x0400016D RID: 365
		ItemConversionFailed = 270,
		// Token: 0x0400016E RID: 366
		AfpInvalidAttachmentOrAttachmentId = 4111,
		// Token: 0x0400016F RID: 367
		ResourceAccessDenied = 4112,
		// Token: 0x04000170 RID: 368
		PartialSuccess = 273,
		// Token: 0x04000171 RID: 369
		CredentialsRequired = 274,
		// Token: 0x04000172 RID: 370
		ServerBusy = 8302,
		// Token: 0x04000173 RID: 371
		MissingElements = 4251,
		// Token: 0x04000174 RID: 372
		ActionNotSupported = 4252
	}
}
