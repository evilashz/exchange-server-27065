using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B2 RID: 946
	public enum AttachmentResultCode
	{
		// Token: 0x0400110F RID: 4367
		Success,
		// Token: 0x04001110 RID: 4368
		GenericFailure,
		// Token: 0x04001111 RID: 4369
		AccessDenied,
		// Token: 0x04001112 RID: 4370
		Timeout,
		// Token: 0x04001113 RID: 4371
		NotFound,
		// Token: 0x04001114 RID: 4372
		UploadError,
		// Token: 0x04001115 RID: 4373
		UpdatePermissionsError,
		// Token: 0x04001116 RID: 4374
		ExchangeOAuthError,
		// Token: 0x04001117 RID: 4375
		RestResponseParseError,
		// Token: 0x04001118 RID: 4376
		GroupNotFound,
		// Token: 0x04001119 RID: 4377
		GroupDocumentsUrlNotFound,
		// Token: 0x0400111A RID: 4378
		Cancelled,
		// Token: 0x0400111B RID: 4379
		GetUploadFolderError
	}
}
