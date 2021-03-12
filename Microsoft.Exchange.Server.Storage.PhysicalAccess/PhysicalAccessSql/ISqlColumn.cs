using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D2 RID: 210
	public interface ISqlColumn
	{
		// Token: 0x06000929 RID: 2345
		void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command);

		// Token: 0x0600092A RID: 2346
		void AppendNameToQuery(SqlCommand command);

		// Token: 0x0600092B RID: 2347
		void AppendQueryText(SqlQueryModel model, SqlCommand command);
	}
}
