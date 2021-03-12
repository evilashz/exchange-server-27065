using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x0200010A RID: 266
	public class SqlTableFunction : TableFunction
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00036A1C File Offset: 0x00034C1C
		public SqlTableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns) : base(name, getTableContents, getColumnFromRow, visibility, parameterTypes, indexes, columns)
		{
		}
	}
}
