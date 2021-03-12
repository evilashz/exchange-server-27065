using System;
using System.IO;
using System.Threading;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x0200009C RID: 156
	internal class BufferedStream : Stream
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x00016C3C File Offset: 0x00014E3C
		public BufferedStream(Stream parentStream) : this(parentStream, DataStream.JetChunkSize)
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00016C4A File Offset: 0x00014E4A
		public BufferedStream(Stream parentStream, int bufferSize)
		{
			this.parentStream = parentStream;
			this.bufferedStream = new BufferedStream(parentStream, bufferSize);
			this.identity = Interlocked.Increment(ref BufferedStream.uniqueId);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00016C76 File Offset: 0x00014E76
		public override bool CanRead
		{
			get
			{
				return this.bufferedStream.CanRead;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00016C83 File Offset: 0x00014E83
		public override bool CanWrite
		{
			get
			{
				return this.bufferedStream.CanWrite;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00016C90 File Offset: 0x00014E90
		public override bool CanSeek
		{
			get
			{
				return this.bufferedStream.CanSeek;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00016C9D File Offset: 0x00014E9D
		public override long Length
		{
			get
			{
				return this.bufferedStream.Length;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00016CAA File Offset: 0x00014EAA
		public Stream ContainedStream
		{
			get
			{
				return this.parentStream;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00016CB2 File Offset: 0x00014EB2
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00016CBF File Offset: 0x00014EBF
		public override long Position
		{
			get
			{
				return this.bufferedStream.Position;
			}
			set
			{
				if (value == this.bufferedStream.Position)
				{
					return;
				}
				this.bufferedStream.Position = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00016CDC File Offset: 0x00014EDC
		internal int Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00016CE4 File Offset: 0x00014EE4
		public override void Flush()
		{
			this.bufferedStream.Flush();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00016CF1 File Offset: 0x00014EF1
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.bufferedStream.Read(buffer, offset, count);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00016D01 File Offset: 0x00014F01
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.bufferedStream.Seek(offset, origin);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00016D10 File Offset: 0x00014F10
		public override void SetLength(long value)
		{
			if (this.Length != value)
			{
				this.bufferedStream.SetLength(value);
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00016D27 File Offset: 0x00014F27
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.bufferedStream.Write(buffer, offset, count);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016D37 File Offset: 0x00014F37
		public void WrapStream(Stream newStream)
		{
			if (this.bufferedStream != null)
			{
				this.bufferedStream.Close();
			}
			this.bufferedStream = new BufferedStream(newStream, DataStream.JetChunkSize);
			this.parentStream = newStream;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00016D64 File Offset: 0x00014F64
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.bufferedStream != null)
				{
					this.bufferedStream.Close();
					this.bufferedStream = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x040002CB RID: 715
		private static int uniqueId;

		// Token: 0x040002CC RID: 716
		private readonly int identity;

		// Token: 0x040002CD RID: 717
		private BufferedStream bufferedStream;

		// Token: 0x040002CE RID: 718
		private Stream parentStream;
	}
}
