using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008F6 RID: 2294
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		// Token: 0x040029C2 RID: 10690
		FAppObject = 1,
		// Token: 0x040029C3 RID: 10691
		FCanCreate = 2,
		// Token: 0x040029C4 RID: 10692
		FLicensed = 4,
		// Token: 0x040029C5 RID: 10693
		FPreDeclId = 8,
		// Token: 0x040029C6 RID: 10694
		FHidden = 16,
		// Token: 0x040029C7 RID: 10695
		FControl = 32,
		// Token: 0x040029C8 RID: 10696
		FDual = 64,
		// Token: 0x040029C9 RID: 10697
		FNonExtensible = 128,
		// Token: 0x040029CA RID: 10698
		FOleAutomation = 256,
		// Token: 0x040029CB RID: 10699
		FRestricted = 512,
		// Token: 0x040029CC RID: 10700
		FAggregatable = 1024,
		// Token: 0x040029CD RID: 10701
		FReplaceable = 2048,
		// Token: 0x040029CE RID: 10702
		FDispatchable = 4096,
		// Token: 0x040029CF RID: 10703
		FReverseBind = 8192
	}
}
