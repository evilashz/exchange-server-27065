using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000232 RID: 562
	[Flags]
	public enum CompactGrbit
	{
		// Token: 0x0400035D RID: 861
		None = 0,
		// Token: 0x0400035E RID: 862
		Stats = 32,
		// Token: 0x0400035F RID: 863
		[Obsolete("Use esentutl repair functionality instead.")]
		Repair = 64
	}
}
