using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C7 RID: 199
	internal class DataStreamImmediateReader : DataStreamImmediate
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		internal DataStreamImmediateReader(DataColumn column, DataTableCursor cursor, DataRow row, int sequence) : base(column, cursor, row, sequence)
		{
			try
			{
				this.length = (long)(Api.RetrieveColumnSize(cursor.Session, cursor.TableId, column.ColumnId, base.Sequence, RetrieveColumnGrbit.None) ?? 0);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001C450 File Offset: 0x0001A650
		protected DataStreamImmediateReader(DataStreamImmediateWriter rhs) : base(rhs.Column, rhs.Cursor, rhs.DataRow, rhs.Sequence)
		{
			this.length = rhs.Length;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001C47C File Offset: 0x0001A67C
		protected DataStreamImmediateReader(DataStreamImmediateReader rhs) : base(rhs.Column, rhs.Cursor, rhs.DataRow, rhs.Sequence)
		{
			this.length = rhs.Length;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001C4AB File Offset: 0x0001A6AB
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001C4AE File Offset: 0x0001A6AE
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001C4B1 File Offset: 0x0001A6B1
		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001C4BB File Offset: 0x0001A6BB
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001C4C2 File Offset: 0x0001A6C2
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001C4C9 File Offset: 0x0001A6C9
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.InternalRead(buffer, offset, count, this.cursor);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001C4DA File Offset: 0x0001A6DA
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
