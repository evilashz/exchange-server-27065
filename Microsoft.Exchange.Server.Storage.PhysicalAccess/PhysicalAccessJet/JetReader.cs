using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B6 RID: 182
	internal sealed class JetReader : Reader
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x00026988 File Offset: 0x00024B88
		internal JetReader(IConnectionProvider connectionProvider, SimpleQueryOperator simpleQueryOperator, bool disposeQueryOperator) : base(connectionProvider, simpleQueryOperator, disposeQueryOperator)
		{
			this.readerOpen = true;
			this.operationData = base.Connection.RecordOperationImpl(simpleQueryOperator);
			this.savedOperationData = base.Connection.SetCurrentOperationStatisticsObject(this.operationData);
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000269C3 File Offset: 0x00024BC3
		private IJetSimpleQueryOperator JetQueryOperator
		{
			get
			{
				return (IJetSimpleQueryOperator)base.SimpleQueryOperator;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x000269D0 File Offset: 0x00024BD0
		public override bool IsClosed
		{
			get
			{
				return !this.readerOpen;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x000269DB File Offset: 0x00024BDB
		public override bool Interrupted
		{
			get
			{
				return this.JetQueryOperator.Interrupted;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000269E8 File Offset: 0x00024BE8
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			bool flag = base.SimpleQueryOperator.EnableInterrupts(interruptControl);
			this.interruptible = (interruptControl != null && flag);
			return flag;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00026A10 File Offset: 0x00024C10
		public override bool Read(out int rowsSkipped)
		{
			return this.Read(true, out rowsSkipped);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00026A1C File Offset: 0x00024C1C
		internal bool Read(bool traceRead, out int rowsSkipped)
		{
			rowsSkipped = 0;
			if (this.interruptible && this.Interrupted)
			{
				this.operationData = base.Connection.RecordOperationImpl(base.SimpleQueryOperator);
				this.savedOperationData = base.Connection.SetCurrentOperationStatisticsObject(this.operationData);
			}
			bool flag = false;
			if (this.readerOpen)
			{
				using (this.TrackReaderSubOperation())
				{
					using (base.Connection.TrackTimeInDatabase())
					{
						if (!this.fetchFirst)
						{
							flag = this.JetQueryOperator.MoveFirst(out rowsSkipped);
							this.fetchFirst = true;
						}
						else
						{
							flag = this.JetQueryOperator.MoveNext();
						}
						if (flag && this.interruptible && this.Interrupted)
						{
							this.operationData = null;
							this.savedOperationData = null;
						}
					}
				}
				if (flag)
				{
					if (traceRead && (!this.interruptible || !this.Interrupted))
					{
						this.TraceReadRecord();
					}
				}
				else
				{
					this.Close();
				}
			}
			return flag;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00026B38 File Offset: 0x00024D38
		public override bool? GetNullableBoolean(Column column)
		{
			bool? result;
			using (this.TrackReaderSubOperation())
			{
				result = (bool?)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00026B80 File Offset: 0x00024D80
		public override bool GetBoolean(Column column)
		{
			bool result;
			using (this.TrackReaderSubOperation())
			{
				result = (bool)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00026BC8 File Offset: 0x00024DC8
		public override long? GetNullableInt64(Column column)
		{
			long? result;
			using (this.TrackReaderSubOperation())
			{
				result = (long?)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00026C10 File Offset: 0x00024E10
		public override long GetInt64(Column column)
		{
			long result;
			using (this.TrackReaderSubOperation())
			{
				result = (long)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00026C58 File Offset: 0x00024E58
		public override int? GetNullableInt32(Column column)
		{
			int? result;
			using (this.TrackReaderSubOperation())
			{
				result = (int?)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00026CA0 File Offset: 0x00024EA0
		public override int GetInt32(Column column)
		{
			int result;
			using (this.TrackReaderSubOperation())
			{
				result = (int)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00026CE8 File Offset: 0x00024EE8
		public override short? GetNullableInt16(Column column)
		{
			short? result;
			using (this.TrackReaderSubOperation())
			{
				result = (short?)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00026D30 File Offset: 0x00024F30
		public override short GetInt16(Column column)
		{
			short result;
			using (this.TrackReaderSubOperation())
			{
				result = (short)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00026D78 File Offset: 0x00024F78
		public override Guid? GetNullableGuid(Column column)
		{
			Guid? result;
			using (this.TrackReaderSubOperation())
			{
				result = (Guid?)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026DC0 File Offset: 0x00024FC0
		public override Guid GetGuid(Column column)
		{
			Guid result;
			using (this.TrackReaderSubOperation())
			{
				result = (Guid)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026E08 File Offset: 0x00025008
		public override DateTime GetDateTime(Column column)
		{
			DateTime result;
			using (this.TrackReaderSubOperation())
			{
				object columnValue = this.JetQueryOperator.GetColumnValue(column);
				DateTime dateTime = (DateTime)columnValue;
				result = dateTime;
			}
			return result;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00026E54 File Offset: 0x00025054
		public override DateTime? GetNullableDateTime(Column column)
		{
			DateTime? result;
			using (this.TrackReaderSubOperation())
			{
				object columnValue = this.JetQueryOperator.GetColumnValue(column);
				if (columnValue != null)
				{
					DateTime value = (DateTime)columnValue;
					result = new DateTime?(value);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00026EB4 File Offset: 0x000250B4
		public override byte[] GetBinary(Column column)
		{
			byte[] result;
			using (this.TrackReaderSubOperation())
			{
				result = (byte[])this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00026EFC File Offset: 0x000250FC
		public override string GetString(Column column)
		{
			string result;
			using (this.TrackReaderSubOperation())
			{
				result = (string)this.JetQueryOperator.GetColumnValue(column);
			}
			return result;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00026F44 File Offset: 0x00025144
		public override object GetValue(Column column)
		{
			object columnValue;
			using (this.TrackReaderSubOperation())
			{
				columnValue = this.JetQueryOperator.GetColumnValue(column);
			}
			return columnValue;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00026F88 File Offset: 0x00025188
		public override object[] GetValues(IEnumerable<Column> columns)
		{
			object[] values;
			using (this.TrackReaderSubOperation())
			{
				using (base.Connection.TrackTimeInDatabase())
				{
					IGetColumnValues getColumnValues = this.JetQueryOperator as IGetColumnValues;
					if (getColumnValues != null)
					{
						using (this.TrackReaderSubOperation())
						{
							return getColumnValues.GetColumnValues(columns);
						}
					}
					values = base.GetValues(columns);
				}
			}
			return values;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00027028 File Offset: 0x00025228
		public override long GetChars(Column column, long dataIndex, char[] outBuffer, int bufferIndex, int length)
		{
			long result;
			using (this.TrackReaderSubOperation())
			{
				int num = 0;
				object columnValue = this.JetQueryOperator.GetColumnValue(column);
				if (columnValue != null)
				{
					string text = (string)columnValue;
					char[] array = text.ToCharArray((int)dataIndex, length);
					num = array.Length;
					Array.Copy(array, 0, outBuffer, bufferIndex, num);
				}
				result = (long)num;
			}
			return result;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00027098 File Offset: 0x00025298
		public override long GetBytes(Column column, long dataIndex, byte[] outBuffer, int bufferIndex, int length)
		{
			long result;
			using (this.TrackReaderSubOperation())
			{
				object columnValue = this.JetQueryOperator.GetColumnValue(column);
				if (columnValue != null)
				{
					byte[] array = (byte[])columnValue;
					if ((long)array.Length - dataIndex > 0L)
					{
						int num = Math.Min(length, array.Length - (int)dataIndex);
						Array.Copy(array, (int)dataIndex, outBuffer, bufferIndex, num);
						return (long)num;
					}
				}
				result = 0L;
			}
			return result;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00027114 File Offset: 0x00025314
		public override void Close()
		{
			Connection connection = base.Connection;
			if (connection != null)
			{
				base.Connection.SetCurrentOperationStatisticsObject(this.savedOperationData);
			}
			this.readerOpen = false;
			base.Close();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002714A File Offset: 0x0002534A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetReader>(this);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00027152 File Offset: 0x00025352
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.readerOpen)
				{
					this.Close();
				}
				if (base.DisposeQueryOperator && base.SimpleQueryOperator != null)
				{
					base.SimpleQueryOperator.Dispose();
				}
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00027180 File Offset: 0x00025380
		private void TraceReadRecord()
		{
			if (ExTraceGlobals.DbInteractionIntermediateTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.DoTraceReadRecord();
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00027198 File Offset: 0x00025398
		private void DoTraceReadRecord()
		{
			using (this.TrackReaderSubOperation())
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" <<< Read row  cols:[");
				for (int i = 0; i < base.SimpleQueryOperator.ColumnsToFetch.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					base.SimpleQueryOperator.ColumnsToFetch[i].AppendToString(stringBuilder, StringFormatOptions.None);
					stringBuilder.Append("=[");
					Column column = base.SimpleQueryOperator.ColumnsToFetch[i];
					object columnValue = this.JetQueryOperator.GetColumnValue(column);
					if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || !(columnValue is byte[]) || ((byte[])columnValue).Length <= 32)
					{
						stringBuilder.AppendAsString(columnValue);
					}
					else
					{
						stringBuilder.Append("<long_blob>");
					}
					stringBuilder.Append("]");
				}
				stringBuilder.Append("]");
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000272B4 File Offset: 0x000254B4
		private JetReader.JetReaderSubOperationTrackingFrame TrackReaderSubOperation()
		{
			return new JetReader.JetReaderSubOperationTrackingFrame(this);
		}

		// Token: 0x040002E4 RID: 740
		private bool fetchFirst;

		// Token: 0x040002E5 RID: 741
		private bool readerOpen;

		// Token: 0x040002E6 RID: 742
		private DatabaseOperationStatistics operationData;

		// Token: 0x040002E7 RID: 743
		private DatabaseOperationStatistics savedOperationData;

		// Token: 0x040002E8 RID: 744
		private bool interruptible;

		// Token: 0x020000B7 RID: 183
		private struct JetReaderSubOperationTrackingFrame : IDisposable
		{
			// Token: 0x06000804 RID: 2052 RVA: 0x000272BC File Offset: 0x000254BC
			internal JetReaderSubOperationTrackingFrame(JetReader reader)
			{
				this.connection = reader.Connection;
				this.savedOperationData = this.connection.SetCurrentOperationStatisticsObject(reader.operationData);
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x000272E1 File Offset: 0x000254E1
			public void Dispose()
			{
				this.connection.SetCurrentOperationStatisticsObject(this.savedOperationData);
			}

			// Token: 0x040002E9 RID: 745
			private readonly Connection connection;

			// Token: 0x040002EA RID: 746
			private DatabaseOperationStatistics savedOperationData;
		}
	}
}
