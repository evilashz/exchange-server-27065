using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000009 RID: 9
	internal class SenderReputationRowData : DataRowAccessBase<SenderReputationTable, SenderReputationRowData>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000026AB File Offset: 0x000008AB
		[PrimaryKey]
		public byte[] SenderAddrHash
		{
			get
			{
				return ((ColumnCache<byte[]>)base.Columns[0]).Value;
			}
			private set
			{
				((ColumnCache<byte[]>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026C4 File Offset: 0x000008C4
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026DC File Offset: 0x000008DC
		public int Srl
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[1]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026F5 File Offset: 0x000008F5
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000270D File Offset: 0x0000090D
		public bool OpenProxy
		{
			get
			{
				return ((ColumnCache<bool>)base.Columns[2]).Value;
			}
			set
			{
				((ColumnCache<bool>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002726 File Offset: 0x00000926
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000273E File Offset: 0x0000093E
		public DateTime ExpirationTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[3]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[3]).Value = value;
			}
		}
	}
}
