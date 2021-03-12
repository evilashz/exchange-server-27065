using System;

namespace Microsoft.Isam.Esent.Interop.Server2003
{
	// Token: 0x020002E2 RID: 738
	[Flags]
	public enum UpdateGrbit
	{
		// Token: 0x0400091F RID: 2335
		None = 0,
		// Token: 0x04000920 RID: 2336
		[Obsolete("Only needed for legacy replication applications.")]
		CheckESE97Compatibility = 1
	}
}
