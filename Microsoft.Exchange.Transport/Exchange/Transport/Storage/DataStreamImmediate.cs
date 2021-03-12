using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C6 RID: 198
	internal abstract class DataStreamImmediate : DataStream
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001C3A3 File Offset: 0x0001A5A3
		internal DataStreamImmediate(DataColumn column, DataTableCursor cursor, DataRow row, int sequence) : base(column, row, sequence)
		{
			this.cursor = cursor;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001C3B6 File Offset: 0x0001A5B6
		public DataTableCursor Cursor
		{
			get
			{
				return this.cursor;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001C3BE File Offset: 0x0001A5BE
		protected override void Dispose(bool disposing)
		{
			this.cursor = null;
			base.Dispose(disposing);
		}

		// Token: 0x04000361 RID: 865
		protected DataTableCursor cursor;
	}
}
