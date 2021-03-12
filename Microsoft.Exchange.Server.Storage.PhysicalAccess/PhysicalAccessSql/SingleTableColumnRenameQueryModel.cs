using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000FC RID: 252
	public class SingleTableColumnRenameQueryModel : SingleTableQueryModel
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x000344EE File Offset: 0x000326EE
		public SingleTableColumnRenameQueryModel(string viewName, IReadOnlyDictionary<Column, Column> renameDictionary) : base(viewName)
		{
			this.renameDictionary = renameDictionary;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00034500 File Offset: 0x00032700
		public override void AppendColumnToQuery(Column column, ColumnUse use, SqlCommand command)
		{
			Column column2;
			if (this.renameDictionary != null && this.renameDictionary.TryGetValue(column, out column2))
			{
				((ISqlColumn)column2).AppendExpressionToQuery(this, use, command);
				return;
			}
			((ISqlColumn)column).AppendExpressionToQuery(this, use, command);
		}

		// Token: 0x04000371 RID: 881
		private IReadOnlyDictionary<Column, Column> renameDictionary;
	}
}
