using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x0200010C RID: 268
	public class SqlTableOperator : TableOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x00036FFC File Offset: 0x000351FC
		internal SqlTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : this(connectionProvider, new TableOperator.TableOperatorDefinition(culture, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, true, frequentOperation))
		{
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0003702C File Offset: 0x0003522C
		internal SqlTableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00037038 File Offset: 0x00035238
		public static IList<Column> RemoveDuplicateColumns(IList<Column> columns)
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<Column> list = new List<Column>(columns.Count);
			foreach (Column column in columns)
			{
				if (!hashSet.Contains(column.Name))
				{
					hashSet.Add(column.Name);
					list.Add(column);
				}
			}
			return list;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000370B0 File Offset: 0x000352B0
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			this.BuildSqlStatement();
			Reader result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = this.sqlCommand.ExecuteReader(Connection.TransactionOption.DontNeedTransaction, base.SkipTo, this, disposeQueryOperator);
			}
			return result;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00037114 File Offset: 0x00035314
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			this.BuildSqlStatement();
			object result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = this.sqlCommand.ExecuteScalar(Connection.TransactionOption.DontNeedTransaction);
			}
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003717C File Offset: 0x0003537C
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003717E File Offset: 0x0003537E
		public bool NeedCteForSqlStatement()
		{
			return false;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00037184 File Offset: 0x00035384
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			SqlQueryModel model = new SingleTableColumnRenameQueryModel(base.Table.Name, base.RenameDictionary);
			sqlCommand.Append("SELECT ");
			if (base.MaxRows != 0)
			{
				sqlCommand.Append("TOP(");
				sqlCommand.Append((base.SkipTo + base.MaxRows).ToString());
				sqlCommand.Append(") ");
			}
			this.AppendSelectList(sqlCommand, model, true, orderedResultsNeeded);
			sqlCommand.Append(" FROM [Exchange].[");
			sqlCommand.Append(base.Table.Name);
			sqlCommand.Append("]");
			this.AppendWhereClause(sqlCommand, model);
			if (this.sqlCommand == sqlCommand || base.MaxRows > 0)
			{
				sqlCommand.Append(" ORDER BY ");
				this.AppendOrderByList(sqlCommand);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003724C File Offset: 0x0003544C
		private static void AppendKeyRangeCriteria(CultureInfo culture, Connection connection, SqlCommand sqlCommand, SortOrder keySortOrder, bool backwards, KeyRange keyRange, int commonPrefixLength)
		{
			sqlCommand.Append("(");
			if (keyRange.StartKey.Count > commonPrefixLength)
			{
				sqlCommand.Append("(");
				for (int i = commonPrefixLength; i < keyRange.StartKey.Count; i++)
				{
					if (i > commonPrefixLength)
					{
						sqlCommand.Append(" OR ");
					}
					sqlCommand.Append("(");
					for (int j = commonPrefixLength; j < i; j++)
					{
						SqlTableOperator.AppendKeyColumnComparison(culture, sqlCommand, keySortOrder[j].Column, SearchCriteriaCompare.SearchRelOp.Equal, keyRange.StartKey.Values[j]);
						sqlCommand.Append(" AND ");
					}
					bool flag = keySortOrder.Ascending[i];
					if (backwards)
					{
						flag = !flag;
					}
					bool flag2 = i == keyRange.StartKey.Count - 1;
					bool flag3 = flag2 && keyRange.StartKey.Inclusive;
					SearchCriteriaCompare.SearchRelOp relOp = flag ? (flag3 ? SearchCriteriaCompare.SearchRelOp.GreaterThanEqual : SearchCriteriaCompare.SearchRelOp.GreaterThan) : (flag3 ? SearchCriteriaCompare.SearchRelOp.LessThanEqual : SearchCriteriaCompare.SearchRelOp.LessThan);
					SqlTableOperator.AppendKeyColumnComparison(culture, sqlCommand, keySortOrder[i].Column, relOp, keyRange.StartKey.Values[i]);
					sqlCommand.Append(")");
				}
				sqlCommand.Append(")");
			}
			if (keyRange.StopKey.Count > commonPrefixLength)
			{
				if (keyRange.StartKey.Count > commonPrefixLength)
				{
					sqlCommand.Append(" AND ");
				}
				sqlCommand.Append("(");
				for (int k = commonPrefixLength; k < keyRange.StopKey.Count; k++)
				{
					if (k > commonPrefixLength)
					{
						sqlCommand.Append(" OR ");
					}
					sqlCommand.Append("(");
					for (int l = 0; l < k; l++)
					{
						SqlTableOperator.AppendKeyColumnComparison(culture, sqlCommand, keySortOrder[l].Column, SearchCriteriaCompare.SearchRelOp.Equal, keyRange.StopKey.Values[l]);
						sqlCommand.Append(" AND ");
					}
					bool flag4 = keySortOrder.Ascending[k];
					if (backwards)
					{
						flag4 = !flag4;
					}
					flag4 = !flag4;
					bool flag5 = k == keyRange.StopKey.Count - 1;
					bool flag6 = flag5 && keyRange.StopKey.Inclusive;
					SearchCriteriaCompare.SearchRelOp relOp2 = flag4 ? (flag6 ? SearchCriteriaCompare.SearchRelOp.GreaterThanEqual : SearchCriteriaCompare.SearchRelOp.GreaterThan) : (flag6 ? SearchCriteriaCompare.SearchRelOp.LessThanEqual : SearchCriteriaCompare.SearchRelOp.LessThan);
					SqlTableOperator.AppendKeyColumnComparison(culture, sqlCommand, keySortOrder[k].Column, relOp2, keyRange.StopKey.Values[k]);
					sqlCommand.Append(")");
				}
				sqlCommand.Append(")");
			}
			sqlCommand.Append(")");
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00037508 File Offset: 0x00035708
		private static int AppendCommonKeyRangeCriteria(CultureInfo culture, Connection connection, SqlCommand sqlCommand, SortOrder keySortOrder, IList<KeyRange> keyRanges)
		{
			int num = 0;
			CompareInfo compareInfo = (culture == null) ? null : culture.CompareInfo;
			for (int i = 0; i < keyRanges.Count; i++)
			{
				int num2 = StartStopKey.CommonKeyPrefix(keyRanges[i].StartKey, keyRanges[i].StopKey, compareInfo);
				if (i == 0)
				{
					num = num2;
				}
				else
				{
					num = Math.Min(num, num2);
					num2 = StartStopKey.CommonKeyPrefix(keyRanges[i].StartKey, keyRanges[i - 1].StopKey, compareInfo);
					num = Math.Min(num, num2);
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (j != 0)
				{
					sqlCommand.Append(" AND ");
				}
				Column column = keySortOrder[j].Column;
				SearchCriteriaCompare.SearchRelOp relOp = SearchCriteriaCompare.SearchRelOp.Equal;
				StartStopKey startKey = keyRanges[0].StartKey;
				SqlTableOperator.AppendKeyColumnComparison(culture, sqlCommand, column, relOp, startKey.Values[j]);
			}
			return num;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000375E8 File Offset: 0x000357E8
		private void AppendSelectList(SqlCommand sqlCommand, SqlQueryModel model, bool baseTable, bool orderedResultsNeeded)
		{
			IList<Column> list = SqlTableOperator.RemoveDuplicateColumns(base.ColumnsToFetch);
			for (int i = 0; i < list.Count; i++)
			{
				if (i != 0)
				{
					sqlCommand.Append(", ");
				}
				Column column = list[i];
				if (base.RenameDictionary != null)
				{
					column = base.ResolveColumn(column);
				}
				model.AppendColumnToQuery(column, ColumnUse.FetchList, sqlCommand);
				sqlCommand.Append(" AS ");
				((ISqlColumn)list[i]).AppendNameToQuery(sqlCommand);
			}
			if (orderedResultsNeeded)
			{
				SortOrder sortOrder = base.SortOrder;
				for (int j = 0; j < sortOrder.Count; j++)
				{
					sqlCommand.Append(", ");
					if (baseTable)
					{
						model.AppendColumnToQuery(sortOrder[j].Column, ColumnUse.FetchList, sqlCommand);
						sqlCommand.Append(" AS ");
					}
					sqlCommand.Append("OB_");
					((ISqlColumn)sortOrder[j].Column).AppendNameToQuery(sqlCommand);
				}
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000376DA File Offset: 0x000358DA
		public void AppendSelectList(SqlCommand sqlCommand, SqlQueryModel model, bool orderedResultsNeeded)
		{
			this.AppendSelectList(sqlCommand, model, false, orderedResultsNeeded);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000376E6 File Offset: 0x000358E6
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000376F0 File Offset: 0x000358F0
		internal void AddToUpdateDelete(SqlCommand sqlCommand)
		{
			this.AppendWhereClause(sqlCommand, SqlQueryModel.Shorthand);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000376FE File Offset: 0x000358FE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlTableOperator>(this);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00037706 File Offset: 0x00035906
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00037720 File Offset: 0x00035920
		private void BuildSqlStatement()
		{
			if (this.sqlCommand == null)
			{
				this.sqlCommand = new SqlCommand(base.Connection);
			}
			this.sqlCommand.StartNewStatement(Connection.OperationType.Query);
			if (this.NeedCteForSqlStatement())
			{
				this.sqlCommand.Append("WITH ");
				this.BuildCteForSqlStatement(this.sqlCommand);
				this.sqlCommand.Append(" ");
			}
			this.BuildSqlStatement(this.sqlCommand, true);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000377A4 File Offset: 0x000359A4
		private void AppendWhereClause(SqlCommand sqlCommand, SqlQueryModel model)
		{
			if (base.KeyRanges.Count == 0)
			{
				sqlCommand.Append(" WHERE (0 = 1) ");
				return;
			}
			StartStopKey startKey = base.KeyRanges[0].StartKey;
			if (startKey.IsEmpty)
			{
				StartStopKey stopKey = base.KeyRanges[0].StopKey;
				if (stopKey.IsEmpty && base.Criteria == null)
				{
					return;
				}
			}
			bool flag = false;
			sqlCommand.Append(" WHERE ");
			StartStopKey startKey2 = base.KeyRanges[0].StartKey;
			if (startKey2.IsEmpty)
			{
				StartStopKey stopKey2 = base.KeyRanges[0].StopKey;
				if (stopKey2.IsEmpty)
				{
					goto IL_C4;
				}
			}
			SqlTableOperator.AppendKeyRangeCriteria(base.Culture, base.Connection, sqlCommand, base.SortOrder, base.Backwards, base.KeyRanges);
			flag = true;
			IL_C4:
			if (base.Criteria != null)
			{
				if (flag)
				{
					sqlCommand.Append(" AND ");
				}
				sqlCommand.Append("(");
				this.AppendCriteria(sqlCommand, model, base.Criteria);
				sqlCommand.Append(")");
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000378B0 File Offset: 0x00035AB0
		private void AppendOrderByList(SqlCommand sqlCommand)
		{
			int count = base.SortOrder.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					sqlCommand.Append(", ");
				}
				sqlCommand.Append(base.SortOrder.Columns[i].Name);
				SqlCollationHelper.AppendCollation(base.SortOrder.Columns[i], base.Culture, sqlCommand);
				bool flag = !base.SortOrder.Ascending[i];
				if (base.Backwards)
				{
					flag = !flag;
				}
				if (flag)
				{
					sqlCommand.Append(" DESC");
				}
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00037964 File Offset: 0x00035B64
		private void AppendCriteria(SqlCommand sqlCommand, SqlQueryModel model, SearchCriteria restriction)
		{
			ISqlSearchCriteria sqlSearchCriteria = restriction as ISqlSearchCriteria;
			sqlSearchCriteria.AppendQueryText(base.Culture, model, sqlCommand);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00037988 File Offset: 0x00035B88
		internal static void AppendKeyColumnComparison(CultureInfo culture, SqlCommand sqlCommand, Column column, SearchCriteriaCompare.SearchRelOp relOp, object value)
		{
			switch (relOp)
			{
			case SearchCriteriaCompare.SearchRelOp.Equal:
				if (value == null)
				{
					if (!column.IsNullable)
					{
						sqlCommand.Append("1=0");
						return;
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append(" IS NULL");
					return;
				}
				else
				{
					if (column.IsNullable)
					{
						sqlCommand.Append("(");
						sqlCommand.Append(column.Name);
						sqlCommand.Append(" IS NOT NULL AND ");
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append("=");
					sqlCommand.AppendParameter(value);
					string collation = SqlCollationHelper.GetCollation(column.Type, culture);
					if (collation != null)
					{
						sqlCommand.Append(" COLLATE ");
						sqlCommand.Append(collation);
					}
					if (column.IsNullable)
					{
						sqlCommand.Append(")");
						return;
					}
				}
				break;
			case SearchCriteriaCompare.SearchRelOp.NotEqual:
				if (value == null)
				{
					if (!column.IsNullable)
					{
						sqlCommand.Append("1=1");
						return;
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append(" IS NOT NULL");
					return;
				}
				else
				{
					if (column.IsNullable)
					{
						sqlCommand.Append("(");
						sqlCommand.Append(column.Name);
						sqlCommand.Append(" IS NULL OR ");
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append("<>");
					sqlCommand.AppendParameter(value);
					string collation2 = SqlCollationHelper.GetCollation(column.Type, culture);
					if (collation2 != null)
					{
						sqlCommand.Append(" COLLATE ");
						sqlCommand.Append(collation2);
					}
					if (column.IsNullable)
					{
						sqlCommand.Append(")");
						return;
					}
				}
				break;
			case SearchCriteriaCompare.SearchRelOp.LessThan:
			{
				if (value == null)
				{
					sqlCommand.Append("1=0");
					return;
				}
				if (column.IsNullable)
				{
					sqlCommand.Append("(");
					sqlCommand.Append(column.Name);
					sqlCommand.Append(" IS NULL OR ");
				}
				sqlCommand.Append(column.Name);
				sqlCommand.Append("<");
				sqlCommand.AppendParameter(value);
				string collation3 = SqlCollationHelper.GetCollation(column.Type, culture);
				if (collation3 != null)
				{
					sqlCommand.Append(" COLLATE ");
					sqlCommand.Append(collation3);
				}
				if (column.IsNullable)
				{
					sqlCommand.Append(")");
					return;
				}
				break;
			}
			case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
				if (value == null)
				{
					if (!column.IsNullable)
					{
						sqlCommand.Append("1=0");
						return;
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append(" IS NULL");
					return;
				}
				else
				{
					if (column.IsNullable)
					{
						sqlCommand.Append("(");
						sqlCommand.Append(column.Name);
						sqlCommand.Append(" IS NULL OR ");
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append("<=");
					sqlCommand.AppendParameter(value);
					string collation4 = SqlCollationHelper.GetCollation(column.Type, culture);
					if (collation4 != null)
					{
						sqlCommand.Append(" COLLATE ");
						sqlCommand.Append(collation4);
					}
					if (column.IsNullable)
					{
						sqlCommand.Append(")");
						return;
					}
				}
				break;
			case SearchCriteriaCompare.SearchRelOp.GreaterThan:
				if (value == null)
				{
					if (!column.IsNullable)
					{
						sqlCommand.Append("1=1");
						return;
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append(" IS NOT NULL");
					return;
				}
				else
				{
					if (column.IsNullable)
					{
						sqlCommand.Append("(");
						sqlCommand.Append(column.Name);
						sqlCommand.Append(" IS NOT NULL AND ");
					}
					sqlCommand.Append(column.Name);
					sqlCommand.Append(">");
					sqlCommand.AppendParameter(value);
					string collation5 = SqlCollationHelper.GetCollation(column.Type, culture);
					if (collation5 != null)
					{
						sqlCommand.Append(" COLLATE ");
						sqlCommand.Append(collation5);
					}
					if (column.IsNullable)
					{
						sqlCommand.Append(")");
						return;
					}
				}
				break;
			case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
			{
				if (value == null)
				{
					sqlCommand.Append("1=1");
					return;
				}
				if (column.IsNullable)
				{
					sqlCommand.Append("(");
					sqlCommand.Append(column.Name);
					sqlCommand.Append(" IS NOT NULL AND ");
				}
				sqlCommand.Append(column.Name);
				sqlCommand.Append(">=");
				sqlCommand.AppendParameter(value);
				string collation6 = SqlCollationHelper.GetCollation(column.Type, culture);
				if (collation6 != null)
				{
					sqlCommand.Append(" COLLATE ");
					sqlCommand.Append(collation6);
				}
				if (column.IsNullable)
				{
					sqlCommand.Append(")");
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00037DA8 File Offset: 0x00035FA8
		internal static void AppendKeyRangeCriteria(CultureInfo culture, Connection connection, SqlCommand sqlCommand, SortOrder keySortOrder, bool backwards, IList<KeyRange> keyRanges)
		{
			sqlCommand.Append("(");
			bool flag = false;
			bool flag2 = false;
			int num = SqlTableOperator.AppendCommonKeyRangeCriteria(culture, connection, sqlCommand, keySortOrder, keyRanges);
			int i = 0;
			while (i < keyRanges.Count)
			{
				StartStopKey startKey = keyRanges[i].StartKey;
				if (startKey.Count > num)
				{
					goto IL_54;
				}
				StartStopKey stopKey = keyRanges[i].StopKey;
				if (stopKey.Count > num)
				{
					goto IL_54;
				}
				IL_A4:
				i++;
				continue;
				IL_54:
				if (!flag && num > 0)
				{
					sqlCommand.Append(" AND (");
					flag = true;
				}
				else if (flag2)
				{
					sqlCommand.Append(" OR ");
				}
				sqlCommand.Append("(");
				SqlTableOperator.AppendKeyRangeCriteria(culture, connection, sqlCommand, keySortOrder, backwards, keyRanges[i], num);
				sqlCommand.Append(")");
				flag2 = true;
				goto IL_A4;
			}
			if (flag)
			{
				sqlCommand.Append(")");
			}
			sqlCommand.Append(")");
		}

		// Token: 0x0400037D RID: 893
		private SqlCommand sqlCommand;
	}
}
