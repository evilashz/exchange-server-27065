using System;

namespace System.Reflection.Emit
{
	// Token: 0x020005FB RID: 1531
	[Flags]
	internal enum DynamicAssemblyFlags
	{
		// Token: 0x04001D88 RID: 7560
		None = 0,
		// Token: 0x04001D89 RID: 7561
		AllCritical = 1,
		// Token: 0x04001D8A RID: 7562
		Aptca = 2,
		// Token: 0x04001D8B RID: 7563
		Critical = 4,
		// Token: 0x04001D8C RID: 7564
		Transparent = 8,
		// Token: 0x04001D8D RID: 7565
		TreatAsSafe = 16
	}
}
