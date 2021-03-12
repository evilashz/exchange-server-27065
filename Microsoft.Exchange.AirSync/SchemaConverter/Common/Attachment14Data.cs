using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000178 RID: 376
	internal class Attachment14Data : Attachment12Data
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x0005C926 File Offset: 0x0005AB26
		public Attachment14Data()
		{
			this.Duration = -1;
			this.Order = -1;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0005C93C File Offset: 0x0005AB3C
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0005C944 File Offset: 0x0005AB44
		public int Duration { get; set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0005C94D File Offset: 0x0005AB4D
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0005C955 File Offset: 0x0005AB55
		public int Order { get; set; }
	}
}
