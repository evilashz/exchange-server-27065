using System;
using System.IO;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000056 RID: 86
	public class ReadOnlyStream : Stream
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00008685 File Offset: 0x00006885
		public ReadOnlyStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("stream");
			}
			this.stream = stream;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000086B5 File Offset: 0x000068B5
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000086B8 File Offset: 0x000068B8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000086BB File Offset: 0x000068BB
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000086C8 File Offset: 0x000068C8
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000086D5 File Offset: 0x000068D5
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000086E2 File Offset: 0x000068E2
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000086F0 File Offset: 0x000068F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008700 File Offset: 0x00006900
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008707 File Offset: 0x00006907
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008715 File Offset: 0x00006915
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008722 File Offset: 0x00006922
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008731 File Offset: 0x00006931
		public override void Close()
		{
			this.stream.Flush();
			this.stream.Dispose();
		}

		// Token: 0x04000193 RID: 403
		private Stream stream;
	}
}
