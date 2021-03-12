using System;
using System.IO;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000EC RID: 236
	internal class BoundedStream : Stream
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0000E19C File Offset: 0x0000C39C
		public BoundedStream(Stream stream, long offset, long size)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.internalStream = stream;
			this.expectedSize = size;
			this.startOffset = offset;
			if (!this.internalStream.CanRead)
			{
				throw new ArgumentException();
			}
			if (this.internalStream.CanSeek && this.internalStream.Position != offset)
			{
				this.internalStream.Position = offset;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000E22C File Offset: 0x0000C42C
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000E233 File Offset: 0x0000C433
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000E23A File Offset: 0x0000C43A
		protected override void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000E259 File Offset: 0x0000C459
		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000E268 File Offset: 0x0000C468
		private void InternalDispose(bool disposing)
		{
			if (disposing && this.internalStream != null)
			{
				this.internalStream.Dispose();
				this.internalStream = null;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000E287 File Offset: 0x0000C487
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("Seek");
			if (!this.CanSeek)
			{
				throw new NotSupportedException();
			}
			long num = this.Position;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num += offset;
				break;
			case SeekOrigin.End:
				num = this.expectedSize - offset;
				break;
			default:
				throw new ArgumentOutOfRangeException("origin");
			}
			this.Position = num;
			return num;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000E30B File Offset: 0x0000C50B
		public override void Flush()
		{
			this.CheckDisposed("Flush");
			this.internalStream.Flush();
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000E323 File Offset: 0x0000C523
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000E32C File Offset: 0x0000C52C
		public override long Position
		{
			get
			{
				return this.bytesRead;
			}
			set
			{
				this.CheckDisposed("Position::set");
				if (!this.CanSeek)
				{
					throw new NotSupportedException();
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.bytesRead = value;
				long num = Math.Min(this.expectedSize, value);
				this.internalStream.Position = this.startOffset + num;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000E389 File Offset: 0x0000C589
		public override long Length
		{
			get
			{
				return this.expectedSize;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000E391 File Offset: 0x0000C591
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("CanSeek::get");
				return this.internalStream.CanSeek;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000E3A9 File Offset: 0x0000C5A9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Read");
			int num = Math.Min(count, (int)(this.expectedSize - this.bytesRead));
			if (num > 0)
			{
				int num2 = this.internalStream.Read(buffer, offset, num);
				this.bytesRead += (long)num2;
				return num2;
			}
			return 0;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000E404 File Offset: 0x0000C604
		public override int ReadByte()
		{
			this.CheckDisposed("ReadByte");
			if (this.bytesRead >= this.expectedSize)
			{
				return -1;
			}
			int num = this.internalStream.ReadByte();
			if (num != -1)
			{
				this.bytesRead += 1L;
			}
			return num;
		}

		// Token: 0x040003AC RID: 940
		private readonly long expectedSize;

		// Token: 0x040003AD RID: 941
		private long bytesRead;

		// Token: 0x040003AE RID: 942
		private readonly long startOffset;

		// Token: 0x040003AF RID: 943
		private Stream internalStream;

		// Token: 0x040003B0 RID: 944
		private bool isDisposed;
	}
}
