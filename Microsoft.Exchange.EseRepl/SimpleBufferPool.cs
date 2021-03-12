using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000018 RID: 24
	internal class SimpleBufferPool : Pool<SimpleBuffer>, ISimpleBufferPool, IPool<SimpleBuffer>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000037E5 File Offset: 0x000019E5
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000037ED File Offset: 0x000019ED
		public int BufferSize { get; private set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x000037F8 File Offset: 0x000019F8
		public SimpleBufferPool(int bufSize, int preAllocCount) : base(preAllocCount)
		{
			this.BufferSize = bufSize;
			for (int i = 0; i < preAllocCount; i++)
			{
				SimpleBuffer o = new SimpleBuffer(bufSize, true);
				this.TryReturnObject(o);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000382F File Offset: 0x00001A2F
		public SimpleBuffer TryGetObject(int bufSizeRequired)
		{
			if (bufSizeRequired > this.BufferSize)
			{
				return null;
			}
			return base.TryGetObject();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003842 File Offset: 0x00001A42
		public override bool TryReturnObject(SimpleBuffer b)
		{
			return b.Buffer.Length == this.BufferSize && base.TryReturnObject(b);
		}
	}
}
