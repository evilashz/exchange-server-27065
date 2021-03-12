using System;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class QuarantineMessageHeader
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000058AC File Offset: 0x00003AAC
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000058B4 File Offset: 0x00003AB4
		public string Identity { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000058BD File Offset: 0x00003ABD
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000058C5 File Offset: 0x00003AC5
		public string Organization { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000058CE File Offset: 0x00003ACE
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000058D6 File Offset: 0x00003AD6
		public string Header { get; set; }
	}
}
