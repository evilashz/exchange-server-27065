using System;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x02000007 RID: 7
	internal interface IRawContent
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43
		string FileName { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44
		long Rawsize { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45
		string RawFileName { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002E RID: 46
		long StreamOffset { get; }
	}
}
