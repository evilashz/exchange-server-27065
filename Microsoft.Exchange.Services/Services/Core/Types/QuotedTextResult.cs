using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F6E RID: 3950
	public class QuotedTextResult
	{
		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06006412 RID: 25618 RVA: 0x0013865D File Offset: 0x0013685D
		// (set) Token: 0x06006413 RID: 25619 RVA: 0x00138665 File Offset: 0x00136865
		public string NewMsg { get; set; }

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06006414 RID: 25620 RVA: 0x0013866E File Offset: 0x0013686E
		// (set) Token: 0x06006415 RID: 25621 RVA: 0x00138676 File Offset: 0x00136876
		public string QuotedText { get; set; }

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06006416 RID: 25622 RVA: 0x0013867F File Offset: 0x0013687F
		public List<QuotedPart> QuotedParts
		{
			get
			{
				return this.quotedParts;
			}
		}

		// Token: 0x0400353A RID: 13626
		protected List<QuotedPart> quotedParts = new List<QuotedPart>();
	}
}
