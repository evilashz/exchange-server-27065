using System;
using System.IO;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200000D RID: 13
	internal class BoundedStream : StreamWrapper
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002884 File Offset: 0x00000A84
		public BoundedStream(Stream wrappedStream, bool canDispose, long indexOfFirstByte, long indexOfLastByte) : base(wrappedStream, canDispose)
		{
			this.indexOfFirstByte = indexOfFirstByte;
			this.indexOfLastByte = indexOfLastByte;
			this.boundedLength = this.indexOfLastByte - this.indexOfFirstByte + 1L;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028B3 File Offset: 0x00000AB3
		public override long Length
		{
			get
			{
				return Math.Min(base.Length, this.boundedLength);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028C6 File Offset: 0x00000AC6
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000028D5 File Offset: 0x00000AD5
		public override long Position
		{
			get
			{
				return base.Position - this.indexOfFirstByte;
			}
			set
			{
				if (value > this.boundedLength)
				{
					throw this.BoundsViolationException();
				}
				base.Position = value + this.indexOfFirstByte;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028F5 File Offset: 0x00000AF5
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.Read(buffer, offset, Math.Min(checked((int)(this.boundedLength - this.Position)), count));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002913 File Offset: 0x00000B13
		public override int ReadByte()
		{
			if (this.Position < this.boundedLength)
			{
				return base.ReadByte();
			}
			return -1;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000292C File Offset: 0x00000B2C
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length - offset;
				break;
			default:
				throw new ArgumentException(string.Empty, "origin");
			}
			if (num > this.boundedLength)
			{
				throw this.BoundsViolationException();
			}
			return base.Seek(this.indexOfFirstByte + num, SeekOrigin.Begin);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002998 File Offset: 0x00000B98
		public override void SetLength(long value)
		{
			if (value > this.boundedLength)
			{
				throw this.BoundsViolationException();
			}
			base.SetLength(value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029B1 File Offset: 0x00000BB1
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.Position + (long)count > this.boundedLength)
			{
				throw this.BoundsViolationException();
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029D4 File Offset: 0x00000BD4
		public override void WriteByte(byte value)
		{
			if (this.Position >= this.boundedLength)
			{
				throw this.BoundsViolationException();
			}
			base.WriteByte(value);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029F2 File Offset: 0x00000BF2
		private Exception BoundsViolationException()
		{
			return new IOException(string.Format("The stream is opened for positions ({0}, {1}). Can't operate outside of this range.", this.indexOfFirstByte, this.indexOfLastByte));
		}

		// Token: 0x04000017 RID: 23
		private readonly long indexOfFirstByte;

		// Token: 0x04000018 RID: 24
		private readonly long indexOfLastByte;

		// Token: 0x04000019 RID: 25
		private readonly long boundedLength;
	}
}
