using System;
using System.IO;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000086 RID: 134
	public class PhysicalColumnStream : Stream
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0001C183 File Offset: 0x0001A383
		public PhysicalColumnStream(IColumnStreamAccess columnStreamAccess, PhysicalColumn column, bool readOnly)
		{
			this.columnStreamAccess = columnStreamAccess;
			this.column = column;
			this.valid = true;
			this.length = null;
			this.readOnly = readOnly;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001C1B3 File Offset: 0x0001A3B3
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001C1B6 File Offset: 0x0001A3B6
		public override bool CanWrite
		{
			get
			{
				return !this.readOnly;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001C1C1 File Offset: 0x0001A3C1
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		public override long Length
		{
			get
			{
				if (this.length == null)
				{
					this.length = new long?((long)this.columnStreamAccess.GetColumnSize(this.column));
				}
				return this.length.Value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001C1FB File Offset: 0x0001A3FB
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0001C203 File Offset: 0x0001A403
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (value >= 0L)
				{
					this.position = value;
					return;
				}
				throw new ArgumentOutOfRangeException("Position", value, "Position must be greater than zero");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001C227 File Offset: 0x0001A427
		internal bool IsValid
		{
			get
			{
				return this.valid;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001C230 File Offset: 0x0001A430
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.AssertValid();
			int num = this.columnStreamAccess.ReadStream(this.column, this.position, buffer, offset, count);
			this.position += (long)num;
			return num;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001C270 File Offset: 0x0001A470
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertValid();
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.Position = offset;
				break;
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				this.Position = this.Length + offset;
				break;
			}
			return this.Position;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001C2C6 File Offset: 0x0001A4C6
		public override void SetLength(long len)
		{
			this.AssertValid();
			throw new NotSupportedException("SetLength is not supported");
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertValid();
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Write is not supported");
			}
			this.columnStreamAccess.WriteStream(this.column, this.position, buffer, offset, count);
			this.position += (long)count;
			this.length = null;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001C333 File Offset: 0x0001A533
		public override void Flush()
		{
			this.AssertValid();
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001C33B File Offset: 0x0001A53B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.valid)
			{
				this.valid = false;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001C34F File Offset: 0x0001A54F
		private void AssertValid()
		{
			if (!this.valid)
			{
				throw new ObjectDisposedException("This stream is closed");
			}
		}

		// Token: 0x040001C5 RID: 453
		private readonly bool readOnly;

		// Token: 0x040001C6 RID: 454
		private readonly PhysicalColumn column;

		// Token: 0x040001C7 RID: 455
		private IColumnStreamAccess columnStreamAccess;

		// Token: 0x040001C8 RID: 456
		private bool valid;

		// Token: 0x040001C9 RID: 457
		private long position;

		// Token: 0x040001CA RID: 458
		private long? length;
	}
}
