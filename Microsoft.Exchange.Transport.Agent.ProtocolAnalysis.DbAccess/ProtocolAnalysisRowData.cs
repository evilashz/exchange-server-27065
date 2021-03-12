using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000008 RID: 8
	internal class ProtocolAnalysisRowData : DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000253C File Offset: 0x0000073C
		public ProtocolAnalysisRowData()
		{
			this.Processing = false;
			this.ReverseDns = string.Empty;
			this.DataBlob = new byte[0];
			this.LastQueryTime = DateTime.UtcNow;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000256D File Offset: 0x0000076D
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002585 File Offset: 0x00000785
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000259E File Offset: 0x0000079E
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000025B6 File Offset: 0x000007B6
		public DateTime LastUpdateTime
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025CF File Offset: 0x000007CF
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000025E7 File Offset: 0x000007E7
		public byte[] DataBlob
		{
			get
			{
				return ((ColumnCache<byte[]>)base.Columns[2]).Value;
			}
			set
			{
				((ColumnCache<byte[]>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002618 File Offset: 0x00000818
		public string ReverseDns
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[3]).Value;
			}
			set
			{
				((ColumnCache<string>)base.Columns[3]).Value = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002631 File Offset: 0x00000831
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002649 File Offset: 0x00000849
		public DateTime LastQueryTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[4]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[4]).Value = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002662 File Offset: 0x00000862
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000267A File Offset: 0x0000087A
		public bool Processing
		{
			get
			{
				return ((ColumnCache<bool>)base.Columns[5]).Value;
			}
			set
			{
				((ColumnCache<bool>)base.Columns[5]).Value = value;
			}
		}
	}
}
