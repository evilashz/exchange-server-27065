using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000253 RID: 595
	[Flags]
	public enum CreateTableColumnIndexGrbit
	{
		// Token: 0x04000404 RID: 1028
		None = 0,
		// Token: 0x04000405 RID: 1029
		FixedDDL = 1,
		// Token: 0x04000406 RID: 1030
		TemplateTable = 2,
		// Token: 0x04000407 RID: 1031
		NoFixedVarColumnsInDerivedTables = 4
	}
}
