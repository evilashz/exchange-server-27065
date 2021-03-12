using System;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200001B RID: 27
	internal class Block<ElementType> : IBlock where ElementType : struct
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004EC7 File Offset: 0x000030C7
		internal Block(int blockSize)
		{
			this.written = 0;
			this.free = blockSize;
			this.buffer = new ElementType[blockSize];
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004EE9 File Offset: 0x000030E9
		internal override int Written
		{
			get
			{
				return this.written;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004EF1 File Offset: 0x000030F1
		internal override int Free
		{
			get
			{
				return this.free;
			}
		}

		// Token: 0x17000014 RID: 20
		internal ElementType this[int index]
		{
			get
			{
				return this.buffer[index];
			}
			set
			{
				this.buffer[index] = value;
			}
		}

		// Token: 0x04000063 RID: 99
		protected ElementType[] buffer;

		// Token: 0x04000064 RID: 100
		protected int written;

		// Token: 0x04000065 RID: 101
		protected int free;
	}
}
