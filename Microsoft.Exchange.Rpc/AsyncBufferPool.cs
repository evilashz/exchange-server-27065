using System;
using System.Collections.Generic;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E2 RID: 482
	internal class AsyncBufferPool
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x0001B7D4 File Offset: 0x0001ABD4
		public AsyncBufferPool(int bufferSize)
		{
			this.acquireTotal = 0;
			this.releaseTotal = 0;
			this.acquireMiss = 0;
			this.releaseMiss = 0;
			this.poolLock = new object();
			this.pool = new Stack<byte[]>();
			if (bufferSize > 1048576)
			{
				throw new ArgumentOutOfRangeException("bufferSize too large");
			}
			if (bufferSize < 1024)
			{
				throw new ArgumentOutOfRangeException("bufferSize too small");
			}
			this.countLimit = Math.Min(256, 20971520 / bufferSize);
			this.bufferSize = bufferSize;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0001B860 File Offset: 0x0001AC60
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001C5B0 File Offset: 0x0001B9B0
		public byte[] Acquire()
		{
			@lock @lock = null;
			@lock lock2 = new @lock(this.poolLock);
			byte[] result;
			try
			{
				@lock = lock2;
				this.acquireTotal++;
				if (this.pool.Count <= 0)
				{
					goto IL_4B;
				}
				result = this.pool.Pop();
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			return result;
			IL_4B:
			try
			{
				this.acquireMiss++;
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			return new byte[this.bufferSize];
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001C668 File Offset: 0x0001BA68
		public void Release(byte[] buffer)
		{
			@lock @lock = null;
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = this.bufferSize;
			if (buffer.Length != num)
			{
				throw new ArgumentException("buffer wrong size");
			}
			Array.Clear(buffer, 0, num);
			@lock lock2 = new @lock(this.poolLock);
			try
			{
				@lock = lock2;
				this.releaseTotal++;
				if (this.pool.Count < this.countLimit)
				{
					this.pool.Push(buffer);
				}
				else
				{
					this.releaseMiss++;
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x04000B97 RID: 2967
		private int countLimit;

		// Token: 0x04000B98 RID: 2968
		private int bufferSize;

		// Token: 0x04000B99 RID: 2969
		private object poolLock;

		// Token: 0x04000B9A RID: 2970
		private Stack<byte[]> pool;

		// Token: 0x04000B9B RID: 2971
		private int acquireTotal;

		// Token: 0x04000B9C RID: 2972
		private int releaseTotal;

		// Token: 0x04000B9D RID: 2973
		private int acquireMiss;

		// Token: 0x04000B9E RID: 2974
		private int releaseMiss;
	}
}
