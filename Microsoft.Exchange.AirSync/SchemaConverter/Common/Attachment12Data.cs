using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000177 RID: 375
	[Serializable]
	internal class Attachment12Data
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0005C896 File Offset: 0x0005AA96
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0005C89E File Offset: 0x0005AA9E
		public string ContentId { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0005C8A7 File Offset: 0x0005AAA7
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0005C8AF File Offset: 0x0005AAAF
		public string ContentLocation { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0005C8B8 File Offset: 0x0005AAB8
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x0005C8C0 File Offset: 0x0005AAC0
		public string DisplayName { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0005C8C9 File Offset: 0x0005AAC9
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x0005C8D1 File Offset: 0x0005AAD1
		public long EstimatedDataSize { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0005C8DA File Offset: 0x0005AADA
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0005C8E2 File Offset: 0x0005AAE2
		public string FileReference { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0005C8EB File Offset: 0x0005AAEB
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0005C8F3 File Offset: 0x0005AAF3
		public bool IsInline { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0005C8FC File Offset: 0x0005AAFC
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0005C904 File Offset: 0x0005AB04
		public byte Method { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0005C90D File Offset: 0x0005AB0D
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0005C915 File Offset: 0x0005AB15
		public AttachmentId Id { get; set; }
	}
}
