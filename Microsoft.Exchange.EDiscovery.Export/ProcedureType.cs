using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000079 RID: 121
	internal enum ProcedureType : uint
	{
		// Token: 0x040002ED RID: 749
		Prepare = 1U,
		// Token: 0x040002EE RID: 750
		Export,
		// Token: 0x040002EF RID: 751
		Stop,
		// Token: 0x040002F0 RID: 752
		Rollback
	}
}
