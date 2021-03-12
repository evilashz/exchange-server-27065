using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001C2 RID: 450
	[Flags]
	internal enum SearchState
	{
		// Token: 0x040005EE RID: 1518
		None = 0,
		// Token: 0x040005EF RID: 1519
		Running = 1,
		// Token: 0x040005F0 RID: 1520
		Rebuild = 2,
		// Token: 0x040005F1 RID: 1521
		Recursive = 4,
		// Token: 0x040005F2 RID: 1522
		Foreground = 8,
		// Token: 0x040005F3 RID: 1523
		UseCiForComplexQueries = 16384,
		// Token: 0x040005F4 RID: 1524
		Static = 65536,
		// Token: 0x040005F5 RID: 1525
		MaybeStatic = 131072,
		// Token: 0x040005F6 RID: 1526
		ImpliedRestrictions = 262144,
		// Token: 0x040005F7 RID: 1527
		StatisticsOnly = 524288,
		// Token: 0x040005F8 RID: 1528
		CiOnly = 1048576,
		// Token: 0x040005F9 RID: 1529
		Failed = 2097152,
		// Token: 0x040005FA RID: 1530
		EstimateCountOnly = 4194304,
		// Token: 0x040005FB RID: 1531
		CiTotally = 16777216,
		// Token: 0x040005FC RID: 1532
		CiWithTwirResidual = 33554432,
		// Token: 0x040005FD RID: 1533
		TwirMostly = 67108864,
		// Token: 0x040005FE RID: 1534
		TwirTotally = 134217728,
		// Token: 0x040005FF RID: 1535
		Error = 268435456
	}
}
