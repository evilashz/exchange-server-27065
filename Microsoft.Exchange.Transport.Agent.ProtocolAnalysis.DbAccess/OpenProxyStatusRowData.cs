using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200000A RID: 10
	internal class OpenProxyStatusRowData : DataRowAccessBase<OpenProxyStatusTable, OpenProxyStatusRowData>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000275F File Offset: 0x0000095F
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002777 File Offset: 0x00000977
		[PrimaryKey]
		public string SenderAddress
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[0]).Value;
			}
			private set
			{
				((ColumnCache<string>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002790 File Offset: 0x00000990
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000027A8 File Offset: 0x000009A8
		public DateTime LastAccessTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[1]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027C1 File Offset: 0x000009C1
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000027D9 File Offset: 0x000009D9
		public DateTime LastDetectionTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[2]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027F2 File Offset: 0x000009F2
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000280A File Offset: 0x00000A0A
		public bool Processing
		{
			get
			{
				return ((ColumnCache<bool>)base.Columns[3]).Value;
			}
			set
			{
				((ColumnCache<bool>)base.Columns[3]).Value = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002823 File Offset: 0x00000A23
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000283B File Offset: 0x00000A3B
		public string Message
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[4]).Value;
			}
			set
			{
				((ColumnCache<string>)base.Columns[4]).Value = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000286C File Offset: 0x00000A6C
		public int OpenProxyStatus
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[5]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[5]).Value = value;
			}
		}
	}
}
