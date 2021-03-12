using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200006B RID: 107
	public abstract class Reader : DisposableBase, ITWIR
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x00016D30 File Offset: 0x00014F30
		internal Reader(IConnectionProvider connectionProvider, SimpleQueryOperator simpleQueryOperator, bool disposeQueryOperator)
		{
			Globals.AssertInternal(connectionProvider != null, "connection provider must be supplied");
			this.connectionProvider = connectionProvider;
			this.simpleQueryOperator = simpleQueryOperator;
			this.disposeQueryOperator = disposeQueryOperator;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00016D5E File Offset: 0x00014F5E
		public SimpleQueryOperator SimpleQueryOperator
		{
			get
			{
				return this.simpleQueryOperator;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00016D66 File Offset: 0x00014F66
		public bool DisposeQueryOperator
		{
			get
			{
				return this.disposeQueryOperator;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00016D6E File Offset: 0x00014F6E
		public Connection Connection
		{
			get
			{
				return this.connectionProvider.GetConnection();
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00016D7B File Offset: 0x00014F7B
		public IConnectionProvider ConnectionProvider
		{
			get
			{
				return this.connectionProvider;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00016D83 File Offset: 0x00014F83
		public Database Database
		{
			get
			{
				return this.Connection.Database;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004B4 RID: 1204
		public abstract bool IsClosed { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00016D90 File Offset: 0x00014F90
		public virtual bool Interrupted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016D94 File Offset: 0x00014F94
		public bool Read()
		{
			int num;
			return this.Read(out num);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016DA9 File Offset: 0x00014FA9
		public virtual bool EnableInterrupts(IInterruptControl interruptControl)
		{
			return false;
		}

		// Token: 0x060004B8 RID: 1208
		public abstract bool Read(out int rowsSkipped);

		// Token: 0x060004B9 RID: 1209
		public abstract bool? GetNullableBoolean(Column column);

		// Token: 0x060004BA RID: 1210
		public abstract bool GetBoolean(Column column);

		// Token: 0x060004BB RID: 1211
		public abstract long? GetNullableInt64(Column column);

		// Token: 0x060004BC RID: 1212
		public abstract long GetInt64(Column column);

		// Token: 0x060004BD RID: 1213
		public abstract int? GetNullableInt32(Column column);

		// Token: 0x060004BE RID: 1214
		public abstract int GetInt32(Column column);

		// Token: 0x060004BF RID: 1215
		public abstract short? GetNullableInt16(Column column);

		// Token: 0x060004C0 RID: 1216
		public abstract short GetInt16(Column column);

		// Token: 0x060004C1 RID: 1217
		public abstract Guid? GetNullableGuid(Column column);

		// Token: 0x060004C2 RID: 1218
		public abstract Guid GetGuid(Column column);

		// Token: 0x060004C3 RID: 1219
		public abstract DateTime GetDateTime(Column column);

		// Token: 0x060004C4 RID: 1220
		public abstract DateTime? GetNullableDateTime(Column column);

		// Token: 0x060004C5 RID: 1221
		public abstract byte[] GetBinary(Column column);

		// Token: 0x060004C6 RID: 1222
		public abstract string GetString(Column column);

		// Token: 0x060004C7 RID: 1223
		public abstract object GetValue(Column column);

		// Token: 0x060004C8 RID: 1224 RVA: 0x00016DAC File Offset: 0x00014FAC
		public virtual object[] GetValues(IEnumerable<Column> columns)
		{
			IList<Column> list = columns as IList<Column>;
			object[] array;
			if (list != null)
			{
				array = new object[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = this.GetValue(list[i]);
				}
			}
			else
			{
				int num = columns.Count<Column>();
				array = new object[num];
				int i = 0;
				foreach (Column column in columns)
				{
					array[i] = this.GetValue(column);
					i++;
				}
			}
			return array;
		}

		// Token: 0x060004C9 RID: 1225
		public abstract long GetChars(Column column, long dataIndex, char[] outBuffer, int bufferIndex, int length);

		// Token: 0x060004CA RID: 1226
		public abstract long GetBytes(Column column, long dataIndex, byte[] outBuffer, int bufferIndex, int length);

		// Token: 0x060004CB RID: 1227 RVA: 0x00016E4C File Offset: 0x0001504C
		public virtual void Close()
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00016E50 File Offset: 0x00015050
		int ITWIR.GetColumnSize(Column column)
		{
			return ((IColumn)column).GetSize(this);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00016E66 File Offset: 0x00015066
		object ITWIR.GetColumnValue(Column column)
		{
			return this.GetValue(column);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00016E70 File Offset: 0x00015070
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			object value = this.GetValue(column);
			return SizeOfColumn.GetColumnSize(column, value).GetValueOrDefault();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00016E96 File Offset: 0x00015096
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			return this.GetValue(column);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00016EA0 File Offset: 0x000150A0
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			object value = this.GetValue(column);
			return SizeOfColumn.GetColumnSize(column, value).GetValueOrDefault();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00016EC6 File Offset: 0x000150C6
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			return this.GetValue(column);
		}

		// Token: 0x0400016E RID: 366
		private IConnectionProvider connectionProvider;

		// Token: 0x0400016F RID: 367
		private SimpleQueryOperator simpleQueryOperator;

		// Token: 0x04000170 RID: 368
		private bool disposeQueryOperator;
	}
}
