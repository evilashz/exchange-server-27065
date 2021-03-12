using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F9 RID: 249
	public class SqlDataRow : DataRow
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00033F2D File Offset: 0x0003212D
		internal SqlDataRow(DataRow.CreateDataRowFlag createFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] initialValues) : base(createFlag, culture, connectionProvider, table, writeThrough, initialValues)
		{
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00033F3E File Offset: 0x0003213E
		internal SqlDataRow(DataRow.OpenDataRowFlag openFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] primaryKeyValues) : base(openFlag, culture, connectionProvider, table, writeThrough, primaryKeyValues)
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00033F4F File Offset: 0x0003214F
		internal SqlDataRow(DataRow.OpenDataRowFlag openFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader) : base(openFlag, culture, connectionProvider, table, writeThrough, reader)
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00033F60 File Offset: 0x00032160
		public override bool CheckTableExists(IConnectionProvider connectionProvider)
		{
			return true;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00033F64 File Offset: 0x00032164
		protected override int? ColumnSize(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			int? result = null;
			using (SqlCommand sqlCommand = new SqlCommand(connectionProvider.GetConnection(), Connection.OperationType.Query))
			{
				sqlCommand.Append("SELECT CAST(DATALENGTH(");
				sqlCommand.Append(column.Name);
				sqlCommand.Append(") AS int)");
				this.AppendFromClause(sqlCommand);
				this.AppendWhereClause(sqlCommand);
				object obj = sqlCommand.ExecuteScalar();
				if (obj != null)
				{
					result = new int?((int)obj);
				}
			}
			return result;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00033FEC File Offset: 0x000321EC
		protected override int ReadBytesFromStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			int result = 0;
			using (SqlCommand sqlCommand = new SqlCommand(connectionProvider.GetConnection(), Connection.OperationType.Query))
			{
				sqlCommand.Append("SELECT SUBSTRING(");
				((ISqlColumn)column).AppendNameToQuery(sqlCommand);
				sqlCommand.Append(",");
				sqlCommand.AppendParameter(position + 1L);
				sqlCommand.Append(",");
				sqlCommand.AppendParameter(count);
				sqlCommand.Append(") AS ");
				((ISqlColumn)column).AppendNameToQuery(sqlCommand);
				this.AppendFromClause(sqlCommand);
				this.AppendWhereClause(sqlCommand);
				Statistics.StatementLength.Averages.AddSample("DataRow.Read", sqlCommand.Length);
				using (Reader reader = sqlCommand.ExecuteReader(Connection.TransactionOption.DontNeedTransaction, 0, null, false))
				{
					reader.Read();
					result = (int)reader.GetBytes(column, 0L, buffer, offset, count);
				}
			}
			return result;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000340E4 File Offset: 0x000322E4
		protected override void WriteBytesToStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			if (offset != 0 || count != buffer.Length)
			{
				byte[] array = new byte[count];
				Buffer.BlockCopy(buffer, offset, array, 0, count);
				buffer = array;
			}
			using (SqlCommand sqlCommand = new SqlCommand(connectionProvider.GetConnection(), Connection.OperationType.Update))
			{
				sqlCommand.Append("UPDATE [Exchange].");
				sqlCommand.Append(base.Table.Name);
				sqlCommand.Append(" SET ");
				((ISqlColumn)column).AppendNameToQuery(sqlCommand);
				sqlCommand.Append(".WRITE(");
				sqlCommand.AppendParameter(buffer);
				sqlCommand.Append(",");
				sqlCommand.AppendParameter(position);
				sqlCommand.Append(",");
				sqlCommand.AppendParameter((long)count);
				sqlCommand.Append(")");
				this.AppendWhereClause(sqlCommand);
				Statistics.StatementLength.Averages.AddSample("DataRow.WriteStream", sqlCommand.Length);
				sqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000341E4 File Offset: 0x000323E4
		private void AppendFromClause(SqlCommand command)
		{
			command.Append(" FROM [Exchange].");
			command.Append(base.Table.Name);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00034204 File Offset: 0x00032404
		private void AppendWhereClause(SqlCommand command)
		{
			bool flag = false;
			command.Append(" WHERE ");
			for (int i = 0; i < base.Table.PrimaryKeyIndex.ColumnCount; i++)
			{
				Column column = base.Table.PrimaryKeyIndex.Columns[i];
				if (flag)
				{
					command.Append(" AND ");
				}
				else
				{
					flag = true;
				}
				command.AppendColumn(column, SqlQueryModel.Shorthand, ColumnUse.Criteria);
				command.Append("=");
				command.AppendParameter(column, base.PrimaryKey[i]);
			}
		}
	}
}
