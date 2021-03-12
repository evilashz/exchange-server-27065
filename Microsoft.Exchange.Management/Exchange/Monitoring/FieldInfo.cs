using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000029 RID: 41
	internal struct FieldInfo
	{
		// Token: 0x06000123 RID: 291 RVA: 0x0000602B File Offset: 0x0000422B
		public FieldInfo(int columnNumber, string columnName)
		{
			this.ColumnName = columnName;
			this.ColumnNumber = columnNumber;
		}

		// Token: 0x040000D3 RID: 211
		internal readonly string ColumnName;

		// Token: 0x040000D4 RID: 212
		internal readonly int ColumnNumber;
	}
}
