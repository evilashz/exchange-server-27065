using System;
using System.Text;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000672 RID: 1650
	internal class CachableString : CachableItem
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000367EE File Offset: 0x000349EE
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000367F6 File Offset: 0x000349F6
		public string StringItem { get; private set; }

		// Token: 0x06001DDF RID: 7647 RVA: 0x000367FF File Offset: 0x000349FF
		public CachableString(string value)
		{
			this.StringItem = value;
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0003680E File Offset: 0x00034A0E
		public override long ItemSize
		{
			get
			{
				return (long)Encoding.Unicode.GetByteCount(this.StringItem);
			}
		}
	}
}
