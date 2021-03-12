using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200034F RID: 847
	[Flags]
	internal enum SubmitMessageFlags : byte
	{
		// Token: 0x04000ACD RID: 2765
		None = 0,
		// Token: 0x04000ACE RID: 2766
		Preprocess = 1,
		// Token: 0x04000ACF RID: 2767
		NeedsSpooler = 2,
		// Token: 0x04000AD0 RID: 2768
		IgnoreSendAsRight = 4
	}
}
