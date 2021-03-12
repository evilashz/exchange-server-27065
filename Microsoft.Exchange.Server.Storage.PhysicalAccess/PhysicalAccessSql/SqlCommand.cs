using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000E8 RID: 232
	public class SqlCommand : DisposableBase
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0003057F File Offset: 0x0002E77F
		public Connection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00030587 File Offset: 0x0002E787
		public StringBuilder Sb
		{
			get
			{
				return this.sb;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0003058F File Offset: 0x0002E78F
		public int Length
		{
			get
			{
				if (this.sb == null)
				{
					return 0;
				}
				return this.sb.Length;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000305A8 File Offset: 0x0002E7A8
		private SqlCommand(Connection connection, bool createStringBuilder)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.connection = (connection as SqlConnection);
				if (createStringBuilder)
				{
					this.sb = new StringBuilder(200);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00030608 File Offset: 0x0002E808
		public SqlCommand(Connection connection) : this(connection, true)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00030614 File Offset: 0x0002E814
		public SqlCommand(Connection connection, Connection.OperationType operationType) : this(connection, true)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.StartNewStatement(operationType);
				disposeGuard.Success();
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00030660 File Offset: 0x0002E860
		public SqlCommand(Connection connection, string statement, Connection.OperationType operationType) : this(connection, false)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.StartNewStatement(operationType);
				this.command = this.connection.CreateSqlCommand();
				this.command.CommandText = statement;
				disposeGuard.Success();
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000306C8 File Offset: 0x0002E8C8
		public void Append(string str)
		{
			this.sb.Append(str);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000306D7 File Offset: 0x0002E8D7
		public void Append(char c)
		{
			this.sb.Append(c);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000306E6 File Offset: 0x0002E8E6
		public void AppendStatement(string statement, Connection.OperationType operationType)
		{
			this.StartNewStatement(operationType);
			this.Append(statement);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000306F6 File Offset: 0x0002E8F6
		public void AppendParameter(SqlDbType sqlDbType, int size, string parameterName)
		{
			if (this.command == null)
			{
				this.command = this.connection.CreateSqlCommand();
			}
			this.command.Parameters.Add(parameterName, sqlDbType, size);
			this.sb.Append(parameterName);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00030732 File Offset: 0x0002E932
		public void AppendParameter(SqlDbType sqlDbType, string parameterName)
		{
			if (this.command == null)
			{
				this.command = this.connection.CreateSqlCommand();
			}
			this.command.Parameters.Add(parameterName, sqlDbType);
			this.sb.Append(parameterName);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0003076D File Offset: 0x0002E96D
		public void AppendParameter(object parameterValue)
		{
			if (parameterValue == null)
			{
				this.sb.Append("NULL");
				return;
			}
			this.AppendParameter(this.NextParameterName(), null, parameterValue);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00030792 File Offset: 0x0002E992
		public void AppendParameter(Column column, object parameterValue)
		{
			if (parameterValue == null && column == null)
			{
				this.sb.Append("NULL");
				return;
			}
			this.AppendParameter(this.NextParameterName(), column, parameterValue);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000307C0 File Offset: 0x0002E9C0
		public void AppendParameter(string parameterName, object parameterValue)
		{
			this.AppendParameter(parameterName, null, parameterValue);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000307CC File Offset: 0x0002E9CC
		public void AppendParameter(string parameterName, Column column, object parameterValue)
		{
			if (this.command == null)
			{
				this.command = this.connection.CreateSqlCommand();
			}
			if (parameterValue is Array && !(parameterValue is byte[]))
			{
				parameterValue = SerializedValue.Serialize(parameterValue);
			}
			else
			{
				if (parameterValue is ICustomParameter)
				{
					throw new StoreException((LID)51797U, ErrorCodeValue.NotSupported);
				}
				if (parameterValue is ArraySegment<byte>)
				{
					ArraySegment<byte> arraySegment = (ArraySegment<byte>)parameterValue;
					byte[] array = new byte[arraySegment.Count];
					Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
					parameterValue = array;
				}
			}
			SqlParameter sqlParameter = this.command.Parameters.AddWithValue(parameterName, parameterValue);
			bool flag = false;
			if (column != null)
			{
				flag = SqlCommand.SetParameterMetadataFromColumn(sqlParameter, column);
			}
			if (parameterValue != null)
			{
				if (!flag && parameterValue.GetType() == typeof(string))
				{
					string text = parameterValue as string;
					if (text.Length >= 4000)
					{
						sqlParameter.SqlDbType = SqlDbType.NText;
					}
					sqlParameter.Size = (text.Length / 4000 + 1) * 4000;
				}
				else if (!flag && parameterValue is byte[])
				{
					byte[] array2 = parameterValue as byte[];
					sqlParameter.Size = (array2.Length / 4000 + 1) * 4000;
				}
				else if (parameterValue.GetType() == typeof(DateTime))
				{
					DateTime dateTime = (DateTime)parameterValue;
					sqlParameter.SqlDbType = SqlDbType.DateTime2;
				}
			}
			else
			{
				sqlParameter.SqlValue = DBNull.Value;
			}
			this.sb.Append(parameterName);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00030954 File Offset: 0x0002EB54
		private static bool SetParameterMetadataFromColumn(SqlParameter param, Column column)
		{
			SqlDbType sqlDbType;
			if (!SqlCommand.SqlDbTypeFromClrType(column.Type, out sqlDbType))
			{
				return false;
			}
			if (sqlDbType == SqlDbType.NVarChar)
			{
				if (column.MaxLength != 0)
				{
					param.SqlDbType = sqlDbType;
					param.Size = column.MaxLength;
				}
				else
				{
					param.SqlDbType = SqlDbType.NChar;
					param.Size = column.Size;
				}
			}
			else if (sqlDbType == SqlDbType.VarBinary)
			{
				if (column.MaxLength != 0)
				{
					param.SqlDbType = sqlDbType;
					param.Size = column.MaxLength;
				}
				else
				{
					param.SqlDbType = SqlDbType.Binary;
					param.Size = column.Size;
				}
			}
			else
			{
				param.SqlDbType = sqlDbType;
			}
			return true;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000309EC File Offset: 0x0002EBEC
		private static bool SqlDbTypeFromClrType(Type clrType, out SqlDbType sqlDbType)
		{
			switch (ValueTypeHelper.GetExtendedTypeCode(clrType))
			{
			case ExtendedTypeCode.Boolean:
				sqlDbType = SqlDbType.Bit;
				return true;
			case ExtendedTypeCode.Int16:
				sqlDbType = SqlDbType.SmallInt;
				return true;
			case ExtendedTypeCode.Int32:
				sqlDbType = SqlDbType.Int;
				return true;
			case ExtendedTypeCode.Int64:
				sqlDbType = SqlDbType.BigInt;
				return true;
			case ExtendedTypeCode.Single:
				sqlDbType = SqlDbType.Real;
				return true;
			case ExtendedTypeCode.Double:
				sqlDbType = SqlDbType.Float;
				return true;
			case ExtendedTypeCode.DateTime:
				sqlDbType = SqlDbType.DateTime2;
				return true;
			case ExtendedTypeCode.Guid:
				sqlDbType = SqlDbType.UniqueIdentifier;
				return true;
			case ExtendedTypeCode.String:
				sqlDbType = SqlDbType.NVarChar;
				return true;
			case ExtendedTypeCode.Binary:
				sqlDbType = SqlDbType.VarBinary;
				return true;
			case ExtendedTypeCode.MVInt16:
			case ExtendedTypeCode.MVInt32:
			case ExtendedTypeCode.MVInt64:
			case ExtendedTypeCode.MVSingle:
			case ExtendedTypeCode.MVDouble:
			case ExtendedTypeCode.MVDateTime:
			case ExtendedTypeCode.MVGuid:
			case ExtendedTypeCode.MVString:
			case ExtendedTypeCode.MVBinary:
				sqlDbType = SqlDbType.VarBinary;
				return true;
			}
			throw new InvalidOperationException(string.Format("Unknown or unexpected type {0}", clrType));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		private static string[] BuildParamNameArray()
		{
			string[] array = new string[200];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "@P" + i.ToString();
			}
			return array;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00030AFF File Offset: 0x0002ECFF
		public void AppendColumn(Column column, SqlQueryModel model, ColumnUse use)
		{
			model.AppendColumnToQuery(column, use, this);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00030B0A File Offset: 0x0002ED0A
		public void AppendFromList(SqlQueryModel model)
		{
			model.AppendFromList(this);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00030B13 File Offset: 0x0002ED13
		public void AppendSelectList(IList<Column> columnsToFetch, SqlQueryModel model)
		{
			model.AppendSelectList(columnsToFetch, this);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00030B1D File Offset: 0x0002ED1D
		public void AppendOrderByList(CultureInfo culture, Microsoft.Exchange.Server.Storage.PhysicalAccess.SortOrder sortOrder, SqlQueryModel model)
		{
			model.AppendOrderByList(culture, sortOrder, false, this);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00030B29 File Offset: 0x0002ED29
		public void AppendOrderByList(CultureInfo culture, Microsoft.Exchange.Server.Storage.PhysicalAccess.SortOrder sortOrder, bool reverse, SqlQueryModel model)
		{
			model.AppendOrderByList(culture, sortOrder, reverse, this);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00030B38 File Offset: 0x0002ED38
		public void StartNewStatement(Connection.OperationType operationType)
		{
			this.connection.CountStatement(operationType);
			this.numberOfStatements++;
			if (this.maxOperationTypeForBatch < operationType)
			{
				this.maxOperationTypeForBatch = operationType;
			}
			if (this.sb != null && this.sb.Length != 0)
			{
				this.sb.Append(";");
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00030B95 File Offset: 0x0002ED95
		public Reader ExecuteReader(Connection.TransactionOption transactionOption, int skipTo, SimpleQueryOperator simpleQueryOperator, bool disposeQueryOperator)
		{
			return this.connection.ExecuteReader(this.ToSqlCommand(), this.numberOfStatements, transactionOption, skipTo, simpleQueryOperator, disposeQueryOperator);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00030BB3 File Offset: 0x0002EDB3
		public int ExecuteNonQuery()
		{
			return this.ExecuteNonQuery(Connection.TransactionOption.NeedTransaction);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00030BBC File Offset: 0x0002EDBC
		public int ExecuteNonQuery(Connection.TransactionOption transactionOption)
		{
			return this.connection.ExecuteNonQuery(this.ToSqlCommand(), this.numberOfStatements, transactionOption);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00030BD6 File Offset: 0x0002EDD6
		public object ExecuteScalar()
		{
			return this.ExecuteScalar(Connection.TransactionOption.DontNeedTransaction);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00030BDF File Offset: 0x0002EDDF
		public object ExecuteScalar(Connection.TransactionOption transactionOption)
		{
			return this.connection.ExecuteScalar(this.ToSqlCommand(), this.numberOfStatements, transactionOption);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00030BF9 File Offset: 0x0002EDF9
		public ISqlCommand ToSqlCommand()
		{
			if (this.command == null)
			{
				this.command = this.connection.CreateSqlCommand();
			}
			if (this.sb != null)
			{
				this.command.CommandText = this.sb.ToString();
			}
			return this.command;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00030C38 File Offset: 0x0002EE38
		public override string ToString()
		{
			return this.sb.ToString();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00030C48 File Offset: 0x0002EE48
		private string NextParameterName()
		{
			int num = this.parameterCounter++;
			string result;
			if (num < SqlCommand.ParamNameArray.Length)
			{
				result = SqlCommand.ParamNameArray[num];
			}
			else
			{
				result = "@P" + num.ToString();
			}
			return result;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00030C8E File Offset: 0x0002EE8E
		internal void AppendQueryHints(bool frequentOperation)
		{
			if (!frequentOperation)
			{
				this.Append(" OPTION (LOOP JOIN, FORCE ORDER, RECOMPILE)");
				return;
			}
			this.Append(" OPTION (LOOP JOIN, FORCE ORDER)");
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00030CAA File Offset: 0x0002EEAA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlCommand>(this);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00030CB2 File Offset: 0x0002EEB2
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.command != null)
			{
				this.command.Dispose();
				this.command = null;
			}
			this.connection = null;
		}

		// Token: 0x0400034D RID: 845
		private const int AverageSize = 200;

		// Token: 0x0400034E RID: 846
		private static readonly string[] ParamNameArray = SqlCommand.BuildParamNameArray();

		// Token: 0x0400034F RID: 847
		private SqlConnection connection;

		// Token: 0x04000350 RID: 848
		private StringBuilder sb;

		// Token: 0x04000351 RID: 849
		private ISqlCommand command;

		// Token: 0x04000352 RID: 850
		private int parameterCounter;

		// Token: 0x04000353 RID: 851
		private int numberOfStatements;

		// Token: 0x04000354 RID: 852
		private Connection.OperationType maxOperationTypeForBatch;
	}
}
