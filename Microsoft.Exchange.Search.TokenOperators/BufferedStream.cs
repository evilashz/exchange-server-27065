using System;
using System.IO;
using System.Threading;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000005 RID: 5
	internal sealed class BufferedStream : Stream
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000031C4 File Offset: 0x000013C4
		public BufferedStream(Stream stream)
		{
			this.bufferedStream = new BufferedStream(stream);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031D8 File Offset: 0x000013D8
		public BufferedStream(Stream stream, int bufferSize)
		{
			this.bufferedStream = new BufferedStream(stream, bufferSize);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000031ED File Offset: 0x000013ED
		public override bool CanRead
		{
			get
			{
				return this.bufferedStream.CanRead;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000031FA File Offset: 0x000013FA
		public override bool CanWrite
		{
			get
			{
				return this.bufferedStream.CanWrite;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003207 File Offset: 0x00001407
		public override bool CanSeek
		{
			get
			{
				return this.bufferedStream.CanSeek;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003214 File Offset: 0x00001414
		public override long Length
		{
			get
			{
				return this.bufferedStream.Length;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003221 File Offset: 0x00001421
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00003242 File Offset: 0x00001442
		public override long Position
		{
			get
			{
				if (this.bufferedStream.CanSeek)
				{
					return this.bufferedStream.Position;
				}
				return this.position;
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

		// Token: 0x06000039 RID: 57 RVA: 0x0000325F File Offset: 0x0000145F
		public override void Flush()
		{
			this.bufferedStream.Flush();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000326C File Offset: 0x0000146C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result = this.bufferedStream.Read(buffer, offset, count);
			Interlocked.Add(ref this.position, (long)count);
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003297 File Offset: 0x00001497
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.bufferedStream.Seek(offset, origin);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032A6 File Offset: 0x000014A6
		public override void SetLength(long value)
		{
			if (this.Length != value)
			{
				this.bufferedStream.SetLength(value);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000032BD File Offset: 0x000014BD
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.bufferedStream.Write(buffer, offset, count);
			Interlocked.Add(ref this.position, (long)count);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000032DC File Offset: 0x000014DC
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

		// Token: 0x04000019 RID: 25
		private long position;

		// Token: 0x0400001A RID: 26
		private BufferedStream bufferedStream;
	}
}
