using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000551 RID: 1361
	internal enum CausalityRelation
	{
		// Token: 0x04001AC8 RID: 6856
		AssignDelegate,
		// Token: 0x04001AC9 RID: 6857
		Join,
		// Token: 0x04001ACA RID: 6858
		Choice,
		// Token: 0x04001ACB RID: 6859
		Cancel,
		// Token: 0x04001ACC RID: 6860
		Error
	}
}
