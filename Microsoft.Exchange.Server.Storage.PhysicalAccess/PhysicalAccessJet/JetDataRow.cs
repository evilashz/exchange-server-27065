using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B4 RID: 180
	public class JetDataRow : DataRow
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00026108 File Offset: 0x00024308
		internal JetDataRow(DataRow.CreateDataRowFlag createFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] initialValues) : base(createFlag, culture, connectionProvider, table, writeThrough, initialValues)
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00026119 File Offset: 0x00024319
		internal JetDataRow(DataRow.OpenDataRowFlag openFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] primaryKeyValues) : base(openFlag, culture, connectionProvider, table, writeThrough, primaryKeyValues)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002612A File Offset: 0x0002432A
		internal JetDataRow(DataRow.OpenDataRowFlag openFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader) : base(openFlag, culture, connectionProvider, table, writeThrough, reader)
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002613C File Offset: 0x0002433C
		public override bool CheckTableExists(IConnectionProvider connectionProvider)
		{
			JetConnection jetConnection = (JetConnection)connectionProvider.GetConnection();
			bool flag;
			return jetConnection.CheckTableExists(base.Table, base.PrimaryKey, false, out flag);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002616C File Offset: 0x0002436C
		protected override int? ColumnSize(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			JetPhysicalColumn column2 = column as JetPhysicalColumn;
			int? result = null;
			StartStopKey startStopKey = new StartStopKey(true, base.PrimaryKey);
			using (JetTableOperator jetTableOperator = (JetTableOperator)Factory.CreateTableOperator(base.Culture, connectionProvider, base.Table, base.Table.PrimaryKeyIndex, null, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
			{
				int num = 0;
				if (jetTableOperator.MoveFirst(true, Connection.OperationType.Query, ref num))
				{
					result = jetTableOperator.GetPhysicalColumnNullableSize(column2);
				}
			}
			return result;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000261FC File Offset: 0x000243FC
		protected override int ReadBytesFromStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			JetPhysicalColumn jetPhysicalColumn = column as JetPhysicalColumn;
			int result = 0;
			StartStopKey startStopKey = new StartStopKey(true, base.PrimaryKey);
			using (JetTableOperator jetTableOperator = (JetTableOperator)Factory.CreateTableOperator(base.Culture, connectionProvider, base.Table, base.Table.PrimaryKeyIndex, null, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
			{
				int num = 0;
				if (jetTableOperator.MoveFirst(true, Connection.OperationType.Query, ref num))
				{
					result = jetTableOperator.ReadBytesFromStream(jetPhysicalColumn, position, buffer, offset, count);
				}
			}
			return result;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002628C File Offset: 0x0002448C
		protected override void WriteBytesToStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			JetPhysicalColumn jetPhysicalColumn = column as JetPhysicalColumn;
			StartStopKey startStopKey = new StartStopKey(true, base.PrimaryKey);
			using (JetTableOperator jetTableOperator = (JetTableOperator)Factory.CreateTableOperator(base.Culture, connectionProvider, base.Table, base.Table.PrimaryKeyIndex, null, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
			{
				int num = 0;
				if (jetTableOperator.MoveFirst(true, Connection.OperationType.Update, ref num))
				{
					jetTableOperator.WriteBytesToStream(jetPhysicalColumn, position, buffer, offset, count);
				}
			}
		}
	}
}
