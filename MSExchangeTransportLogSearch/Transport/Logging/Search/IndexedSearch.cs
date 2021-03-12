using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000037 RID: 55
	internal class IndexedSearch
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00008F64 File Offset: 0x00007164
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00008F6C File Offset: 0x0000716C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008F74 File Offset: 0x00007174
		public IndexedSearch(string columnName, string value)
		{
			this.columnName = columnName;
			this.value = value;
		}

		// Token: 0x040000D6 RID: 214
		private string columnName;

		// Token: 0x040000D7 RID: 215
		private string value;
	}
}
