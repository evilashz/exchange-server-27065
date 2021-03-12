using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200023D RID: 573
	[Flags]
	public enum OpenTableGrbit
	{
		// Token: 0x04000380 RID: 896
		None = 0,
		// Token: 0x04000381 RID: 897
		DenyWrite = 1,
		// Token: 0x04000382 RID: 898
		DenyRead = 2,
		// Token: 0x04000383 RID: 899
		ReadOnly = 4,
		// Token: 0x04000384 RID: 900
		Updatable = 8,
		// Token: 0x04000385 RID: 901
		PermitDDL = 16,
		// Token: 0x04000386 RID: 902
		NoCache = 32,
		// Token: 0x04000387 RID: 903
		Preread = 64,
		// Token: 0x04000388 RID: 904
		Sequential = 32768,
		// Token: 0x04000389 RID: 905
		TableClass1 = 65536,
		// Token: 0x0400038A RID: 906
		TableClass2 = 131072,
		// Token: 0x0400038B RID: 907
		TableClass3 = 196608,
		// Token: 0x0400038C RID: 908
		TableClass4 = 262144,
		// Token: 0x0400038D RID: 909
		TableClass5 = 327680,
		// Token: 0x0400038E RID: 910
		TableClass6 = 393216,
		// Token: 0x0400038F RID: 911
		TableClass7 = 458752,
		// Token: 0x04000390 RID: 912
		TableClass8 = 524288,
		// Token: 0x04000391 RID: 913
		TableClass9 = 589824,
		// Token: 0x04000392 RID: 914
		TableClass10 = 655360,
		// Token: 0x04000393 RID: 915
		TableClass11 = 720896,
		// Token: 0x04000394 RID: 916
		TableClass12 = 786432,
		// Token: 0x04000395 RID: 917
		TableClass13 = 851968,
		// Token: 0x04000396 RID: 918
		TableClass14 = 917504,
		// Token: 0x04000397 RID: 919
		TableClass15 = 983040
	}
}
