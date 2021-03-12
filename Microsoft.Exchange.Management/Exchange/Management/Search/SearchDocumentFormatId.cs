using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	public class SearchDocumentFormatId : ObjectId
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003A484 File Offset: 0x00038684
		public SearchDocumentFormatId(string id)
		{
			this.identity = id;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003A493 File Offset: 0x00038693
		public SearchDocumentFormatId(byte[] id)
		{
			this.identity = Encoding.Unicode.GetString(id);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003A4AC File Offset: 0x000386AC
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.identity);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003A4BE File Offset: 0x000386BE
		public override string ToString()
		{
			return this.identity;
		}

		// Token: 0x04000629 RID: 1577
		private readonly string identity;
	}
}
