using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C8 RID: 200
	internal class DataStreamLazyReader : DataStream, ICloneable
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		internal DataStreamLazyReader(DataColumn column, DataTableCursor cursor, DataRow row, int sequence) : base(column, row, sequence)
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

		// Token: 0x06000705 RID: 1797 RVA: 0x0001C560 File Offset: 0x0001A760
		internal DataStreamLazyReader(DataStream rhs) : base(rhs)
		{
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001C569 File Offset: 0x0001A769
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001C56C File Offset: 0x0001A76C
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001C56F File Offset: 0x0001A76F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001C572 File Offset: 0x0001A772
		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001C575 File Offset: 0x0001A775
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001C57C File Offset: 0x0001A77C
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C583 File Offset: 0x0001A783
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001C58A File Offset: 0x0001A78A
		object ICloneable.Clone()
		{
			if (this.row == null)
			{
				throw new ObjectDisposedException("DataStreamLazyReader");
			}
			return new DataStreamLazyReader(this);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.Position == this.Length)
			{
				return 0;
			}
			int result;
			using (DataTableCursor cursor = this.row.Table.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					this.row.SeekCurrent(cursor);
					result = base.InternalRead(buffer, offset, count, cursor);
				}
			}
			return result;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001C628 File Offset: 0x0001A828
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
