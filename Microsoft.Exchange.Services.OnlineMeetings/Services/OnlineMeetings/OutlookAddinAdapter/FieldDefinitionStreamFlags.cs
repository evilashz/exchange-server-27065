using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000BA RID: 186
	[Flags]
	internal enum FieldDefinitionStreamFlags
	{
		// Token: 0x040002EB RID: 747
		PDO_IS_CUSTOM = 1,
		// Token: 0x040002EC RID: 748
		PDO_REQUIRED = 2,
		// Token: 0x040002ED RID: 749
		PDO_PRINT_SAVEAS = 4,
		// Token: 0x040002EE RID: 750
		PDO_CALC_AUTO = 8,
		// Token: 0x040002EF RID: 751
		PDO_FT_CONCAT = 16,
		// Token: 0x040002F0 RID: 752
		PDO_FT_SWITCH = 32,
		// Token: 0x040002F1 RID: 753
		PDO_PRINT_SAVEAS_DEF = 64
	}
}
