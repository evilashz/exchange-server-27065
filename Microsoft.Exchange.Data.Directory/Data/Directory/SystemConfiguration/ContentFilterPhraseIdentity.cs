using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	internal class ContentFilterPhraseIdentity : ObjectId
	{
		// Token: 0x06003759 RID: 14169 RVA: 0x000D85D5 File Offset: 0x000D67D5
		public ContentFilterPhraseIdentity(string phrase)
		{
			this.phrase = phrase;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000D85E4 File Offset: 0x000D67E4
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.phrase);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000D85F6 File Offset: 0x000D67F6
		public override string ToString()
		{
			return this.phrase;
		}

		// Token: 0x0400256A RID: 9578
		private string phrase;
	}
}
