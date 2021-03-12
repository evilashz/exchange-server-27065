using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B3 RID: 1459
	[Serializable]
	internal class PerimeterQueueId : ObjectId
	{
		// Token: 0x06003331 RID: 13105 RVA: 0x000D0759 File Offset: 0x000CE959
		public PerimeterQueueId(string identity)
		{
			this.identity = identity;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000D0768 File Offset: 0x000CE968
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.identity);
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000D077A File Offset: 0x000CE97A
		public override string ToString()
		{
			return this.identity;
		}

		// Token: 0x040023BA RID: 9146
		private readonly string identity;
	}
}
