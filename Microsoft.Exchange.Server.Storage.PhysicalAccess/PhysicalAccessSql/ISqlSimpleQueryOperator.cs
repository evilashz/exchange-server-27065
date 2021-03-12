using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D7 RID: 215
	public interface ISqlSimpleQueryOperator
	{
		// Token: 0x0600095A RID: 2394
		void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded);

		// Token: 0x0600095B RID: 2395
		void BuildCteForSqlStatement(SqlCommand sqlCommand);

		// Token: 0x0600095C RID: 2396
		bool NeedCteForSqlStatement();

		// Token: 0x0600095D RID: 2397
		void AppendSelectList(SqlCommand sqlCommand, SqlQueryModel model, bool orderedResultsNeeded);

		// Token: 0x0600095E RID: 2398
		void AddToInsert(SqlCommand sqlCommand);
	}
}
