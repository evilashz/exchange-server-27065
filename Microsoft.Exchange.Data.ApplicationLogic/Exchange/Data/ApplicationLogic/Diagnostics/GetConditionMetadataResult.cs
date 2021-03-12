using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D9 RID: 217
	public class GetConditionMetadataResult
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00024EFC File Offset: 0x000230FC
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00024F04 File Offset: 0x00023104
		public ActiveConditionalMetadataResult[] ActiveConditions { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00024F0D File Offset: 0x0002310D
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00024F15 File Offset: 0x00023115
		public ConditionalMetadataResult[] CompletedConditions { get; set; }
	}
}
