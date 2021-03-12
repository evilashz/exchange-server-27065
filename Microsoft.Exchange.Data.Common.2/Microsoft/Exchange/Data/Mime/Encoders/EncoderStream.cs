using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x02000097 RID: 151
	public class EncoderStream : Stream, ICloneableStream
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00022A00 File Offset: 0x00020C00
		public EncoderStream(Stream stream, ByteEncoder encoder, EncoderStreamAccess access)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoder == null)
			{
				throw new ArgumentNullException("encoder");
			}
			if (access == EncoderStreamAccess.Read)
			{
				if (!stream.CanRead)
				{
					throw new NotSupportedException(EncodersStrings.EncStrCannotRead);
				}
			}
			else if (!stream.CanWrite)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotWrite);
			}
			this.stream = stream;
			this.encoder = encoder;
			this.access = access;
			this.ownsStream = true;
			this.length = long.MaxValue;
			this.buffer = new byte[4096];
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00022A94 File Offset: 0x00020C94
		internal EncoderStream(Stream stream, ByteEncoder encoder, EncoderStreamAccess access, bool ownsStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoder == null)
			{
				throw new ArgumentNullException("encoder");
			}
			if (access == EncoderStreamAccess.Read)
			{
				if (!stream.CanRead)
				{
					throw new NotSupportedException(EncodersStrings.EncStrCannotRead);
				}
			}
			else if (!stream.CanWrite)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotWrite);
			}
			this.stream = stream;
			this.encoder = encoder;
			this.access = access;
			this.ownsStream = ownsStream;
			this.length = long.MaxValue;
			this.buffer = new byte[4096];
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00022B28 File Offset: 0x00020D28
		public sealed override bool CanRead
		{
			get
			{
				return this.access == EncoderStreamAccess.Read && this.IsOpen;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00022B3A File Offset: 0x00020D3A
		public sealed override bool CanWrite
		{
			get
			{
				return EncoderStreamAccess.Write == this.access && this.IsOpen;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00022B4D File Offset: 0x00020D4D
		public sealed override bool CanSeek
		{
			get
			{
				return this.access == EncoderStreamAccess.Read && this.IsOpen && this.stream.CanSeek;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00022B6C File Offset: 0x00020D6C
		public sealed override long Length
		{
			get
			{
				this.AssertOpen();
				if (this.access == EncoderStreamAccess.Read)
				{
					if (9223372036854775807L == this.length)
					{
						Stream stream = this.Clone();
						long num = this.position;
						byte[] array = new byte[4096];
						long num2;
						do
						{
							num2 = (long)stream.Read(array, 0, 4096);
							num += num2;
						}
						while (num2 > 0L);
						this.length = num;
					}
					return this.length;
				}
				return this.position;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00022BE0 File Offset: 0x00020DE0
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00022BEE File Offset: 0x00020DEE
		public sealed override long Position
		{
			get
			{
				this.AssertOpen();
				return this.position;
			}
			set
			{
				this.AssertOpen();
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00022BFF File Offset: 0x00020DFF
		public Stream BaseStream
		{
			get
			{
				this.AssertOpen();
				return this.stream;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00022C0D File Offset: 0x00020E0D
		public ByteEncoder ByteEncoder
		{
			get
			{
				this.AssertOpen();
				return this.encoder;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00022C1B File Offset: 0x00020E1B
		private bool IsOpen
		{
			get
			{
				return null != this.stream;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00022C2C File Offset: 0x00020E2C
		public Stream Clone()
		{
			this.AssertOpen();
			if (EncoderStreamAccess.Write == this.access)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotCloneWriteableStream);
			}
			ICloneableStream cloneableStream = this.stream as ICloneableStream;
			if (cloneableStream == null && this.stream.CanSeek)
			{
				this.stream = new AutoPositionReadOnlyStream(this.stream, this.ownsStream);
				this.ownsStream = true;
				cloneableStream = (this.stream as ICloneableStream);
			}
			if (cloneableStream != null)
			{
				EncoderStream encoderStream = base.MemberwiseClone() as EncoderStream;
				encoderStream.buffer = (this.buffer.Clone() as byte[]);
				encoderStream.stream = cloneableStream.Clone();
				encoderStream.encoder = this.encoder.Clone();
				return encoderStream;
			}
			throw new NotSupportedException(EncodersStrings.EncStrCannotCloneChildStream(this.stream.GetType().ToString()));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00022CF8 File Offset: 0x00020EF8
		public sealed override int Read(byte[] array, int offset, int count)
		{
			this.AssertOpen();
			if (!this.CanRead)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotRead);
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset + count > array.Length)
			{
				throw new ArgumentOutOfRangeException("offset, count", EncodersStrings.EncStrLengthExceeded(offset + count, array.Length));
			}
			if (0 > offset || 0 > count)
			{
				throw new ArgumentOutOfRangeException((offset < 0) ? "offset" : "count");
			}
			int num = 0;
			while (!this.endOfFile && count != 0)
			{
				if (this.bufferCount == 0)
				{
					this.bufferPos = 0;
					this.bufferCount = this.stream.Read(this.buffer, 0, 4096);
				}
				int num2;
				int num3;
				bool flag;
				this.encoder.Convert(this.buffer, this.bufferPos, this.bufferCount, array, offset, count, this.bufferCount == 0, out num2, out num3, out flag);
				if (this.bufferCount == 0 && flag)
				{
					this.endOfFile = true;
				}
				count -= num3;
				offset += num3;
				num += num3;
				this.position += (long)num3;
				this.bufferPos += num2;
				this.bufferCount -= num2;
			}
			return num;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00022E24 File Offset: 0x00021024
		public sealed override void Write(byte[] array, int offset, int count)
		{
			this.AssertOpen();
			if (!this.CanWrite)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotWrite);
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count + offset > array.Length)
			{
				throw new ArgumentException(EncodersStrings.EncStrLengthExceeded(offset + count, array.Length), "array");
			}
			if (0 > offset || 0 > count)
			{
				throw new ArgumentOutOfRangeException((offset < 0) ? "offset" : "count");
			}
			while (count != 0)
			{
				int num;
				int count2;
				bool flag;
				this.encoder.Convert(array, offset, count, this.buffer, 0, this.buffer.Length, false, out num, out count2, out flag);
				count -= num;
				offset += num;
				this.position += (long)num;
				this.stream.Write(this.buffer, 0, count2);
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00022EE8 File Offset: 0x000210E8
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertOpen();
			if (!this.CanSeek)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotSeek);
			}
			if (origin == SeekOrigin.Current)
			{
				offset += this.position;
			}
			else if (origin == SeekOrigin.End)
			{
				if (this.length == 9223372036854775807L && offset == 0L)
				{
					offset = long.MaxValue;
				}
				else
				{
					offset += this.Length;
				}
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset < this.position)
			{
				this.bufferPos = 0;
				this.bufferCount = 0;
				this.endOfFile = false;
				this.encoder.Reset();
				this.stream.Seek(0L, SeekOrigin.Begin);
				this.position = 0L;
			}
			if (offset > this.position)
			{
				long num = offset - this.position;
				byte[] array = new byte[4096];
				while (num > 0L)
				{
					int num2 = (int)Math.Min(num, 4096L);
					num2 = this.Read(array, 0, num2);
					if (num2 == 0)
					{
						if (this.length == 9223372036854775807L)
						{
							this.length = this.position;
						}
						offset = this.position;
						break;
					}
					num -= (long)num2;
				}
			}
			return offset;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002300B File Offset: 0x0002120B
		public sealed override void SetLength(long value)
		{
			this.AssertOpen();
			throw new NotSupportedException();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00023018 File Offset: 0x00021218
		public sealed override void Flush()
		{
			this.AssertOpen();
			if (!this.CanWrite)
			{
				throw new NotSupportedException(EncodersStrings.EncStrCannotWrite);
			}
			this.FlushEncoder(false);
			this.stream.Flush();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00023048 File Offset: 0x00021248
		private void FlushEncoder(bool done)
		{
			bool flag = false;
			int num2;
			do
			{
				int num;
				this.encoder.Convert(null, 0, 0, this.buffer, 0, this.buffer.Length, done, out num, out num2, out flag);
				this.stream.Write(this.buffer, 0, num2);
			}
			while (0 < num2 && !flag);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00023095 File Offset: 0x00021295
		private void AssertOpen()
		{
			if (!this.IsOpen)
			{
				throw new ObjectDisposedException("EncoderStream");
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000230AC File Offset: 0x000212AC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.IsOpen)
			{
				if (this.CanWrite)
				{
					this.FlushEncoder(true);
				}
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.encoder != null)
				{
					this.encoder.Dispose();
					this.encoder = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000480 RID: 1152
		private const int BlockSize = 4096;

		// Token: 0x04000481 RID: 1153
		private Stream stream;

		// Token: 0x04000482 RID: 1154
		private EncoderStreamAccess access;

		// Token: 0x04000483 RID: 1155
		private bool ownsStream;

		// Token: 0x04000484 RID: 1156
		private bool endOfFile;

		// Token: 0x04000485 RID: 1157
		private long length;

		// Token: 0x04000486 RID: 1158
		private long position;

		// Token: 0x04000487 RID: 1159
		private ByteEncoder encoder;

		// Token: 0x04000488 RID: 1160
		private byte[] buffer;

		// Token: 0x04000489 RID: 1161
		private int bufferPos;

		// Token: 0x0400048A RID: 1162
		private int bufferCount;
	}
}
