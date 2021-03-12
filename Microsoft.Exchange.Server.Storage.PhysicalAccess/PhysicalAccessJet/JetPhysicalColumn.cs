using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B2 RID: 178
	internal class JetPhysicalColumn : PhysicalColumn, IJetColumn, IColumn
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x00025FA8 File Offset: 0x000241A8
		internal JetPhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table, int index, int maxInlineLength) : base(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, schemaExtension, visibility, maxLength, size, table, index, maxInlineLength)
		{
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00025FD4 File Offset: 0x000241D4
		internal JetPhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table, int index, int maxInlineLength) : base(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, visibility, maxLength, size, table, index, maxInlineLength)
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00026000 File Offset: 0x00024200
		internal JET_COLUMNID GetJetColumnId(JetConnection connection)
		{
			if (!this.jetColumnIdSet)
			{
				try
				{
					JET_TABLEID openTable = connection.GetOpenTable(base.Table, base.Table.Name, null, Connection.OperationType.Query);
					using (connection.TrackTimeInDatabase())
					{
						JET_COLUMNDEF jet_COLUMNDEF;
						Api.JetGetTableColumnInfo(connection.JetSession, openTable, base.PhysicalName, out jet_COLUMNDEF);
						this.jetColumnId = jet_COLUMNDEF.columnid;
						this.jetColumnIdSet = true;
						Api.JetCloseTable(connection.JetSession, openTable);
					}
				}
				catch (EsentErrorException ex)
				{
					connection.OnExceptionCatch(ex);
					throw connection.ProcessJetError((LID)59848U, "JetPhysicalColumn.GetJetColumnId", ex);
				}
			}
			return this.jetColumnId;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000260C0 File Offset: 0x000242C0
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			return cursor.GetPhysicalColumnValueAsBytes(this);
		}

		// Token: 0x040002E1 RID: 737
		private JET_COLUMNID jetColumnId;

		// Token: 0x040002E2 RID: 738
		private bool jetColumnIdSet;
	}
}
