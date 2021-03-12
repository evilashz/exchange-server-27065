using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C3 RID: 1219
	internal class ContentFilterPhraseQueryFilter : QueryFilter
	{
		// Token: 0x0600375C RID: 14172 RVA: 0x000D85FE File Offset: 0x000D67FE
		public ContentFilterPhraseQueryFilter(string phrase)
		{
			this.phrase = phrase;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000D860D File Offset: 0x000D680D
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.phrase);
			sb.Append(")");
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000D8634 File Offset: 0x000D6834
		public string Phrase
		{
			get
			{
				return this.phrase;
			}
		}

		// Token: 0x0400256B RID: 9579
		private string phrase;
	}
}
