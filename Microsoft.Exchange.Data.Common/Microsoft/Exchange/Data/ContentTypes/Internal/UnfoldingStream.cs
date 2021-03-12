using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000DE RID: 222
	internal class UnfoldingStream : Stream
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		public UnfoldingStream(Stream inputStream)
		{
			this.inputStream = inputStream;
			this.unprocessedBuffer = new byte[1024];
			this.foldingIndices = new Stack<int>();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00030022 File Offset: 0x0002E222
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.isClosed && this.inputStream != null)
			{
				this.inputStream.Dispose();
				this.inputStream = null;
			}
			this.isClosed = true;
			base.Dispose(disposing);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00030057 File Offset: 0x0002E257
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed("CanRead:get");
				return true;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00030065 File Offset: 0x0002E265
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed("CanWrite:get");
				return false;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00030073 File Offset: 0x0002E273
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("CanSeek:get");
				return false;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00030081 File Offset: 0x0002E281
		public override long Length
		{
			get
			{
				this.CheckDisposed("Length:Get");
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00030093 File Offset: 0x0002E293
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x000300A5 File Offset: 0x0002E2A5
		public override long Position
		{
			get
			{
				this.CheckDisposed("Position:get");
				throw new NotSupportedException();
			}
			set
			{
				this.CheckDisposed("Position:set");
				throw new NotSupportedException();
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000300B7 File Offset: 0x0002E2B7
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Write");
			throw new NotSupportedException();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000300CC File Offset: 0x0002E2CC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.endOfInputStream && this.bufferIdx >= this.bufferCount - 3)
			{
				if (this.bufferIdx != 0)
				{
					for (int i = 0; i < this.bufferCount - this.bufferIdx; i++)
					{
						this.unprocessedBuffer[i] = this.unprocessedBuffer[this.bufferIdx + i];
					}
				}
				this.bufferCount -= this.bufferIdx;
				this.bufferIdx = 0;
				this.foldingIndices.Clear();
				int num = this.inputStream.Read(this.unprocessedBuffer, this.bufferCount, this.unprocessedBuffer.Length - this.bufferCount);
				if (num == 0)
				{
					this.endOfInputStream = true;
				}
				this.bufferCount += num;
			}
			int num2 = 0;
			int num3 = this.bufferCount - this.bufferIdx;
			while (count > 0 && num3 > 0 && (num3 >= 3 || this.endOfInputStream))
			{
				if (num3 >= 3 && this.unprocessedBuffer[this.bufferIdx] == 13 && this.unprocessedBuffer[this.bufferIdx + 1] == 10 && (this.unprocessedBuffer[this.bufferIdx + 2] == 32 || this.unprocessedBuffer[this.bufferIdx + 2] == 9))
				{
					this.foldingIndices.Push(this.bufferIdx);
					this.bufferIdx += 3;
					num3 -= 3;
				}
				else
				{
					buffer[offset++] = this.unprocessedBuffer[this.bufferIdx++];
					count--;
					num3--;
					num2++;
				}
			}
			if (num2 == 0 && !this.endOfInputStream)
			{
				return this.Read(buffer, offset, count);
			}
			return num2;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0003027C File Offset: 0x0002E47C
		public void Rewind(int countBytes)
		{
			this.bufferIdx -= countBytes;
			while (this.foldingIndices.Count > 0 && this.foldingIndices.Peek() > this.bufferIdx)
			{
				this.foldingIndices.Pop();
				this.bufferIdx -= 3;
			}
			if (this.bufferIdx < 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000302E3 File Offset: 0x0002E4E3
		public override void SetLength(long value)
		{
			this.CheckDisposed("SetLength");
			throw new NotSupportedException();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000302F5 File Offset: 0x0002E4F5
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("Seek");
			throw new NotSupportedException();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00030307 File Offset: 0x0002E507
		public override void Flush()
		{
			this.CheckDisposed("Flush");
			throw new NotSupportedException();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00030319 File Offset: 0x0002E519
		private void CheckDisposed(string methodName)
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException("UnfoldingStream", methodName);
			}
		}

		// Token: 0x04000766 RID: 1894
		private Stream inputStream;

		// Token: 0x04000767 RID: 1895
		private byte[] unprocessedBuffer;

		// Token: 0x04000768 RID: 1896
		private Stack<int> foldingIndices;

		// Token: 0x04000769 RID: 1897
		private int bufferIdx;

		// Token: 0x0400076A RID: 1898
		private int bufferCount;

		// Token: 0x0400076B RID: 1899
		private bool endOfInputStream;

		// Token: 0x0400076C RID: 1900
		private bool isClosed;
	}
}
