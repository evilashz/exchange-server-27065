using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000144 RID: 324
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ByteQueue
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x00037DE7 File Offset: 0x00035FE7
		public ByteQueue(int size)
		{
			this.buffer = new byte[size];
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00037DFB File Offset: 0x00035FFB
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00037E03 File Offset: 0x00036003
		public void Enqueue(byte[] inputBuffer)
		{
			this.EnqueueInternal(inputBuffer.Length, inputBuffer, 0);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00037E10 File Offset: 0x00036010
		public void Enqueue(int inputCount, byte[] inputBuffer, int inputOffset)
		{
			this.EnqueueInternal(inputCount, inputBuffer, inputOffset);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00037E1C File Offset: 0x0003601C
		public byte[] Dequeue(int count)
		{
			int num = Math.Min(count, this.count);
			byte[] array = new byte[num];
			this.DequeueInternal(array.Length, array, 0);
			return array;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00037E4A File Offset: 0x0003604A
		public int Dequeue(byte[] outputBuffer)
		{
			return this.DequeueInternal(outputBuffer.Length, outputBuffer, 0);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00037E57 File Offset: 0x00036057
		public int Dequeue(int outputBufferSize, byte[] outputBuffer, int outputOffset)
		{
			return this.DequeueInternal(outputBufferSize, outputBuffer, outputOffset);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00037E64 File Offset: 0x00036064
		private void EnqueueInternal(int inputCount, byte[] inputBuffer, int inputOffset)
		{
			int num = this.offset + this.count;
			while (inputCount > 0)
			{
				if (num >= this.buffer.Length)
				{
					num -= this.buffer.Length;
				}
				this.buffer[num] = inputBuffer[inputOffset];
				num++;
				this.count++;
				inputOffset++;
				inputCount--;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00037EC4 File Offset: 0x000360C4
		private int DequeueInternal(int outputBufferSize, byte[] outputBuffer, int outputOffset)
		{
			int num = 0;
			while (outputBufferSize > 0 && this.count > 0)
			{
				if (this.offset >= this.buffer.Length)
				{
					this.offset -= this.buffer.Length;
				}
				outputBuffer[outputOffset] = this.buffer[this.offset];
				this.offset++;
				this.count--;
				outputOffset++;
				outputBufferSize--;
				num++;
			}
			return num;
		}

		// Token: 0x0400070C RID: 1804
		private byte[] buffer;

		// Token: 0x0400070D RID: 1805
		private int offset;

		// Token: 0x0400070E RID: 1806
		private int count;
	}
}
