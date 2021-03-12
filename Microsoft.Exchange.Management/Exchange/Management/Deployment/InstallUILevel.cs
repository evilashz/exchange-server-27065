using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002AA RID: 682
	[Flags]
	internal enum InstallUILevel
	{
		// Token: 0x04000A78 RID: 2680
		NoChange = 0,
		// Token: 0x04000A79 RID: 2681
		Default = 1,
		// Token: 0x04000A7A RID: 2682
		None = 2,
		// Token: 0x04000A7B RID: 2683
		Basic = 3,
		// Token: 0x04000A7C RID: 2684
		Reduced = 4,
		// Token: 0x04000A7D RID: 2685
		Full = 5,
		// Token: 0x04000A7E RID: 2686
		EndDialog = 128,
		// Token: 0x04000A7F RID: 2687
		ProgressOnly = 64,
		// Token: 0x04000A80 RID: 2688
		HideCancel = 32,
		// Token: 0x04000A81 RID: 2689
		SourceResOnly = 256
	}
}
