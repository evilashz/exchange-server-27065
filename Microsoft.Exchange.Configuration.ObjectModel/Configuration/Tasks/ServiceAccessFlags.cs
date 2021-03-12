using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000BB RID: 187
	[Flags]
	internal enum ServiceAccessFlags
	{
		// Token: 0x040001C1 RID: 449
		QueryConfig = 1,
		// Token: 0x040001C2 RID: 450
		ChangeConfig = 2,
		// Token: 0x040001C3 RID: 451
		QueryStatus = 4,
		// Token: 0x040001C4 RID: 452
		EnumerateDependents = 8,
		// Token: 0x040001C5 RID: 453
		Start = 16,
		// Token: 0x040001C6 RID: 454
		Stop = 32,
		// Token: 0x040001C7 RID: 455
		PauseContinue = 64,
		// Token: 0x040001C8 RID: 456
		Interrogate = 128,
		// Token: 0x040001C9 RID: 457
		UserDefinedControl = 256,
		// Token: 0x040001CA RID: 458
		ReadControl = 131072,
		// Token: 0x040001CB RID: 459
		WriteDac = 262144,
		// Token: 0x040001CC RID: 460
		AllAccess = 393727
	}
}
