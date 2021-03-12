using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200000F RID: 15
	internal interface IPagedView
	{
		// Token: 0x06000052 RID: 82
		object[][] GetRows(int rowCount);
	}
}
