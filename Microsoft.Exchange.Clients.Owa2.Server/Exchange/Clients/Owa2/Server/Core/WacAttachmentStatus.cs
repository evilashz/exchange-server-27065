using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200041A RID: 1050
	public enum WacAttachmentStatus
	{
		// Token: 0x04001351 RID: 4945
		Success,
		// Token: 0x04001352 RID: 4946
		UnknownError,
		// Token: 0x04001353 RID: 4947
		NotFound,
		// Token: 0x04001354 RID: 4948
		ProtectedByUnsupportedIrm,
		// Token: 0x04001355 RID: 4949
		UnsupportedObjectType,
		// Token: 0x04001356 RID: 4950
		InvalidRequest,
		// Token: 0x04001357 RID: 4951
		AttachmentDataProviderError,
		// Token: 0x04001358 RID: 4952
		UploadFailed,
		// Token: 0x04001359 RID: 4953
		WacDiscoveryFailed
	}
}
