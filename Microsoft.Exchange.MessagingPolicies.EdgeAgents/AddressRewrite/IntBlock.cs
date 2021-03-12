using System;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200001F RID: 31
	internal class IntBlock : Block<uint>
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000053FD File Offset: 0x000035FD
		public IntBlock() : base(IntBlock.BlockSize)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000540C File Offset: 0x0000360C
		internal void Add(uint data)
		{
			this.buffer[this.written++] = data;
			this.free--;
		}

		// Token: 0x04000070 RID: 112
		internal static int BlockSize = 16384;
	}
}
