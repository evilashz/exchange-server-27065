using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000038 RID: 56
	internal class SocketStreamAsyncArgsPool : Pool<SocketStreamAsyncArgs>, IDisposable
	{
		// Token: 0x060001DB RID: 475 RVA: 0x000078A0 File Offset: 0x00005AA0
		public SocketStreamAsyncArgsPool(int preAllocCount) : base(preAllocCount)
		{
			for (int i = 0; i < preAllocCount; i++)
			{
				SocketStreamAsyncArgs o = new SocketStreamAsyncArgs(true);
				this.TryReturnObject(o);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000078CF File Offset: 0x00005ACF
		public void Dispose()
		{
			this.EmptyPool();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000078D8 File Offset: 0x00005AD8
		public void EmptyPool()
		{
			SocketStreamAsyncArgs socketStreamAsyncArgs;
			while ((socketStreamAsyncArgs = this.TryGetObject()) != null)
			{
				socketStreamAsyncArgs.Dispose();
			}
		}
	}
}
