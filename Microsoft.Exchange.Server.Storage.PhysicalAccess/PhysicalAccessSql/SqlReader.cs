using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000FD RID: 253
	internal sealed class SqlReader : Reader
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00034542 File Offset: 0x00032742
		internal SqlReader(ISqlDataReader reader, Connection connection, int skipTo, SimpleQueryOperator simpleQueryOperator, bool disposeQueryOperator) : base(connection, simpleQueryOperator, disposeQueryOperator)
		{
			this.reader = reader;
			this.skipTo = skipTo;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0003455D File Offset: 0x0003275D
		public override bool IsClosed
		{
			get
			{
				return this.reader.IsClosed;
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0003456C File Offset: 0x0003276C
		public override bool Read(out int rowsSkipped)
		{
			rowsSkipped = 0;
			bool result;
			try
			{
				bool flag;
				using (base.Connection.TrackDbOperationExecution(base.SimpleQueryOperator))
				{
					for (;;)
					{
						flag = this.reader.Read();
						if (!flag || this.skipTo <= 0)
						{
							break;
						}
						this.skipTo--;
						rowsSkipped++;
					}
				}
				this.UpdateRowsRead();
				if (flag)
				{
					this.TraceReadRecord(this.reader);
				}
				result = flag;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "Read", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00034634 File Offset: 0x00032834
		public override bool? GetNullableBoolean(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			bool? result;
			try
			{
				bool? flag = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					flag = new bool?(this.reader.GetBoolean(ordinal));
					this.UpdateBytesRead(2L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = flag;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetNullableBoolean", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000346D0 File Offset: 0x000328D0
		public override bool GetBoolean(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			bool boolean;
			try
			{
				this.UpdateBytesRead(1L);
				boolean = this.reader.GetBoolean(ordinal);
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetBoolean", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return boolean;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00034744 File Offset: 0x00032944
		public override long? GetNullableInt64(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			long? result;
			try
			{
				long? num = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					num = new long?(this.reader.GetInt64(ordinal));
					this.UpdateBytesRead(9L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = num;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetNullableInt64", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000347E0 File Offset: 0x000329E0
		public override long GetInt64(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			long @int;
			try
			{
				this.UpdateBytesRead(8L);
				@int = this.reader.GetInt64(ordinal);
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetInt64", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return @int;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00034854 File Offset: 0x00032A54
		public override int? GetNullableInt32(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			int? result;
			try
			{
				int? num = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					num = new int?(this.reader.GetInt32(ordinal));
					this.UpdateBytesRead(5L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = num;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetNullableInt32", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000348F0 File Offset: 0x00032AF0
		public override int GetInt32(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			int @int;
			try
			{
				this.UpdateBytesRead(4L);
				@int = this.reader.GetInt32(ordinal);
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetInt32", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return @int;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00034964 File Offset: 0x00032B64
		public override short? GetNullableInt16(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			short? result;
			try
			{
				short? num = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					num = new short?(this.reader.GetInt16(ordinal));
					this.UpdateBytesRead(3L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = num;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetNullableInt16", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00034A00 File Offset: 0x00032C00
		public override short GetInt16(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			short @int;
			try
			{
				this.UpdateBytesRead(2L);
				@int = this.reader.GetInt16(ordinal);
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetInt16", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return @int;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00034A74 File Offset: 0x00032C74
		public override Guid? GetNullableGuid(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			Guid? result;
			try
			{
				Guid? guid = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					guid = new Guid?(this.reader.GetGuid(ordinal));
					this.UpdateBytesRead(17L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = guid;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetNullableGuid", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00034B10 File Offset: 0x00032D10
		public override Guid GetGuid(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			Guid guid;
			try
			{
				this.UpdateBytesRead(16L);
				guid = this.reader.GetGuid(ordinal);
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetGuid", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return guid;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00034B84 File Offset: 0x00032D84
		public override DateTime GetDateTime(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			DateTime result;
			try
			{
				this.UpdateBytesRead(8L);
				DateTime dateTime = this.reader.GetDateTime(ordinal);
				result = dateTime;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetDateTime", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00034BF8 File Offset: 0x00032DF8
		public override DateTime? GetNullableDateTime(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			DateTime? result;
			try
			{
				DateTime? dateTime = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					dateTime = new DateTime?(this.GetDateTime(column));
					this.UpdateBytesRead(8L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = dateTime;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetNullableDateTime", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00034C8C File Offset: 0x00032E8C
		public override byte[] GetBinary(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			byte[] result;
			try
			{
				SqlBinary sqlBinary = null;
				byte[] array = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					array = this.reader.GetSqlBinary(ordinal).Value;
					this.UpdateBytesRead((long)(array.Length + 1));
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = array;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetBinary", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00034D30 File Offset: 0x00032F30
		public override string GetString(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			string result;
			try
			{
				string text = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					text = this.reader.GetString(ordinal);
					this.UpdateBytesRead((long)(text.Length * 2 + 1));
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = text;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetString", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00034DC8 File Offset: 0x00032FC8
		public override object GetValue(Column column)
		{
			int ordinal = this.GetOrdinal(column.Name);
			object result;
			try
			{
				object obj = null;
				if (!this.reader.IsDBNull(ordinal))
				{
					obj = this.reader.GetValue(ordinal);
					if (obj != null && obj is byte[] && column.Type != typeof(byte[]))
					{
						obj = SerializedValue.Parse((byte[])obj);
					}
					this.UpdateBytesRead(10L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = obj;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetValue", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00034E84 File Offset: 0x00033084
		public override long GetChars(Column column, long dataIndex, char[] outBuffer, int bufferIndex, int length)
		{
			int ordinal = this.GetOrdinal(column.Name);
			long result;
			try
			{
				long num = 0L;
				if (!this.reader.IsDBNull(ordinal))
				{
					num = this.reader.GetChars(ordinal, dataIndex, outBuffer, bufferIndex, length);
					this.UpdateBytesRead(num + 1L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = num;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetChars", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00034F1C File Offset: 0x0003311C
		public override long GetBytes(Column column, long dataIndex, byte[] outBuffer, int bufferIndex, int length)
		{
			int ordinal = this.GetOrdinal(column.Name);
			long result;
			try
			{
				long bytes = this.reader.GetBytes(ordinal, dataIndex, outBuffer, bufferIndex, length);
				this.UpdateBytesRead(bytes);
				result = bytes;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetBytes", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00034F94 File Offset: 0x00033194
		public string GetStringByOrdinal(int i)
		{
			string result;
			try
			{
				string text = null;
				if (!this.reader.IsDBNull(i))
				{
					text = this.reader.GetString(i);
					this.UpdateBytesRead((long)(text.Length * 2 + 1));
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = text;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetString", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00035020 File Offset: 0x00033220
		public object GetValueByOrdinal(int i)
		{
			object result;
			try
			{
				object obj = null;
				if (!this.reader.IsDBNull(i))
				{
					obj = this.reader.GetValue(i);
					this.UpdateBytesRead(10L);
				}
				else
				{
					this.UpdateBytesRead(1L);
				}
				result = obj;
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetValue", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return result;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000350A4 File Offset: 0x000332A4
		public override void Close()
		{
			if (!this.IsClosed)
			{
				try
				{
					if (ExTraceGlobals.DbIOTracer.IsTraceEnabled(TraceType.PerformanceTrace))
					{
						while (this.reader.NextResult())
						{
						}
					}
					this.reader.Close();
				}
				catch (SqlException ex)
				{
					base.Connection.OnExceptionCatch(ex);
					SqlConnection.LogSQLError("Reader", "Close", ex);
					throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
				}
			}
			base.Connection.AddRowStatsCounter(null, RowStatsCounterType.Read, this.rowsRead);
			base.Connection.AddRowStatsCounter(null, RowStatsCounterType.ReadBytes, (int)this.bytesRead);
			base.Close();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0003514C File Offset: 0x0003334C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlReader>(this);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00035154 File Offset: 0x00033354
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Close();
				this.reader.Dispose();
			}
			if (base.DisposeQueryOperator && base.SimpleQueryOperator != null)
			{
				base.SimpleQueryOperator.Dispose();
			}
			this.reader = null;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0003518C File Offset: 0x0003338C
		private void UpdateBytesRead(long bytesRead)
		{
			this.bytesRead += bytesRead;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0003519C File Offset: 0x0003339C
		private void UpdateRowsRead()
		{
			this.rowsRead++;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000351AC File Offset: 0x000333AC
		private int GetOrdinal(string name)
		{
			int ordinal;
			try
			{
				ordinal = this.reader.GetOrdinal(name);
			}
			catch (SqlException ex)
			{
				base.Connection.OnExceptionCatch(ex);
				SqlConnection.LogSQLError("Reader", "GetOrdinal", ex);
				throw ((SqlConnection)base.Connection).ProcessSqlError(ex);
			}
			return ordinal;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00035208 File Offset: 0x00033408
		private void TraceReadRecord(ISqlDataReader reader)
		{
			if (ExTraceGlobals.DbInteractionIntermediateTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" <<< Read row  cols:[");
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(reader.GetName(i));
					stringBuilder.Append("=[");
					object value = reader.GetValue(i);
					if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || !(value is byte[]) || ((byte[])value).Length <= 32)
					{
						stringBuilder.AppendAsString(value);
					}
					else
					{
						stringBuilder.Append("<long_blob>");
					}
					stringBuilder.Append("]");
				}
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x04000372 RID: 882
		private ISqlDataReader reader;

		// Token: 0x04000373 RID: 883
		private int skipTo;

		// Token: 0x04000374 RID: 884
		private long bytesRead;

		// Token: 0x04000375 RID: 885
		private int rowsRead;
	}
}
