using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000097 RID: 151
	internal enum TableStatus : byte
	{
		// Token: 0x04000252 RID: 594
		Complete,
		// Token: 0x04000253 RID: 595
		QChanged = 7,
		// Token: 0x04000254 RID: 596
		Sorting = 9,
		// Token: 0x04000255 RID: 597
		SortError,
		// Token: 0x04000256 RID: 598
		SettingColumns,
		// Token: 0x04000257 RID: 599
		SetColumnsError = 13,
		// Token: 0x04000258 RID: 600
		Restricting,
		// Token: 0x04000259 RID: 601
		RestrictError
	}
}
