using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000208 RID: 520
	public class SpamRulePackageData
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0004360D File Offset: 0x0004180D
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x00043615 File Offset: 0x00041815
		public SpamRuleData[] Rules { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0004361E File Offset: 0x0004181E
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x00043626 File Offset: 0x00041826
		public ProcessorData[] Processors { get; set; }
	}
}
