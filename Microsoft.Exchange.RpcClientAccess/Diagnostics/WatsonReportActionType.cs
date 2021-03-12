using System;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000041 RID: 65
	internal enum WatsonReportActionType
	{
		// Token: 0x040001E6 RID: 486
		[Obsolete("Invalid. Use any other type.")]
		None,
		// Token: 0x040001E7 RID: 487
		Connection,
		// Token: 0x040001E8 RID: 488
		IcsDownload,
		// Token: 0x040001E9 RID: 489
		MessageAdaptor,
		// Token: 0x040001EA RID: 490
		FolderAdaptor,
		// Token: 0x040001EB RID: 491
		FastTransferState
	}
}
