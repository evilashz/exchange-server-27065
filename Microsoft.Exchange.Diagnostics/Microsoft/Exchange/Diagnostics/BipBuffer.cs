using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000003 RID: 3
	public class BipBuffer
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public BipBuffer(int bufferSize)
		{
			this.buffer = new byte[bufferSize];
			this.regionAHead = 0;
			this.regionATail = 0;
			this.regionBSize = 0;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002101 File Offset: 0x00000301
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002109 File Offset: 0x00000309
		public int AllocatedSize
		{
			get
			{
				return this.regionATail - this.regionAHead + this.regionBSize;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public int Allocate(int requestedSize)
		{
			int result = -1;
			if (this.regionBSize == 0 && this.buffer.Length - this.regionATail >= requestedSize)
			{
				result = this.regionATail;
				this.regionATail += requestedSize;
			}
			else if (this.regionAHead - this.regionBSize >= requestedSize)
			{
				result = this.regionBSize;
				this.regionBSize += requestedSize;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002188 File Offset: 0x00000388
		public void Release(int releasedSize)
		{
			while (releasedSize > 0)
			{
				int num = Math.Min(this.regionATail - this.regionAHead, releasedSize);
				this.regionAHead += num;
				releasedSize -= num;
				if (this.regionAHead == this.regionATail)
				{
					this.regionAHead = 0;
					this.regionATail = this.regionBSize;
					this.regionBSize = 0;
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021EC File Offset: 0x000003EC
		public void Extract(byte[] destinationBuffer, int destinationOffset, int extractedSize)
		{
			int num = 0;
			if (extractedSize > 0)
			{
				int num2 = Math.Min(this.regionATail - this.regionAHead, extractedSize);
				Array.Copy(this.buffer, this.regionAHead, destinationBuffer, destinationOffset, num2);
				destinationOffset += num2;
				num += num2;
				extractedSize -= num2;
			}
			if (extractedSize > 0)
			{
				Array.Copy(this.buffer, 0, destinationBuffer, destinationOffset, extractedSize);
				num += extractedSize;
				extractedSize = 0;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000224F File Offset: 0x0000044F
		public void Extract(int skipBytes, out byte destinationBuffer)
		{
			this.Release(skipBytes);
			destinationBuffer = this.buffer[this.regionAHead];
			this.Release(1);
		}

		// Token: 0x04000009 RID: 9
		private byte[] buffer;

		// Token: 0x0400000A RID: 10
		private int regionAHead;

		// Token: 0x0400000B RID: 11
		private int regionATail;

		// Token: 0x0400000C RID: 12
		private int regionBSize;
	}
}
