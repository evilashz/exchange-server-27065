using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000071 RID: 113
	internal interface IPlaybackEventArgs
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004C4 RID: 1220
		// (set) Token: 0x060004C5 RID: 1221
		TimeSpan PlayTime { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004C6 RID: 1222
		// (set) Token: 0x060004C7 RID: 1223
		int LastPrompt { get; set; }
	}
}
