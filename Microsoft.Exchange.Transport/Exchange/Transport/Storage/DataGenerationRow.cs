using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000E2 RID: 226
	internal class DataGenerationRow : DataRow
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x00020C44 File Offset: 0x0001EE44
		public DataGenerationRow(DataTable table) : base(table)
		{
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00020C4D File Offset: 0x0001EE4D
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00020C65 File Offset: 0x0001EE65
		public int GenerationId
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[0]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00020C7E File Offset: 0x0001EE7E
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00020C96 File Offset: 0x0001EE96
		public DateTime StartTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[1]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00020CAF File Offset: 0x0001EEAF
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00020CC7 File Offset: 0x0001EEC7
		public DateTime EndTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[2]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00020CF8 File Offset: 0x0001EEF8
		public int Category
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[3]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[3]).Value = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00020D11 File Offset: 0x0001EF11
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00020D29 File Offset: 0x0001EF29
		public string Name
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[4]).Value;
			}
			set
			{
				((ColumnCache<string>)base.Columns[4]).Value = value;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00020D44 File Offset: 0x0001EF44
		public static DataGenerationRow LoadFromRow(DataTableCursor cursor)
		{
			DataGenerationRow dataGenerationRow = new DataGenerationRow(cursor.Table);
			dataGenerationRow.LoadFromCurrentRow(cursor);
			return dataGenerationRow;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00020D65 File Offset: 0x0001EF65
		public void Commit(Transaction transaction)
		{
			base.Materialize(transaction);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00020D6E File Offset: 0x0001EF6E
		public new void Commit()
		{
			base.Commit();
		}
	}
}
