using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A53 RID: 2643
	internal interface IBingResultSet
	{
		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06004AFE RID: 19198
		IBingResult[] Results { get; }

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06004AFF RID: 19199
		IBingError[] Errors { get; }
	}
}
