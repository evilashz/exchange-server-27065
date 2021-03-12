using System;
using System.Net;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000016 RID: 22
	internal class WorkItemData
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00004028 File Offset: 0x00002228
		public WorkItemData()
		{
			this.workType = WorkItemType.InvalidType;
			this.blockPeriod = 0;
			this.blockComment = string.Empty;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004049 File Offset: 0x00002249
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004051 File Offset: 0x00002251
		public WorkItemPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000405A File Offset: 0x0000225A
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004062 File Offset: 0x00002262
		public IPAddress SenderAddress
		{
			get
			{
				return this.senderAddress;
			}
			set
			{
				this.senderAddress = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000406B File Offset: 0x0000226B
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004073 File Offset: 0x00002273
		public int BlockPeriod
		{
			get
			{
				return this.blockPeriod;
			}
			set
			{
				this.blockPeriod = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000407C File Offset: 0x0000227C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004084 File Offset: 0x00002284
		public string BlockComment
		{
			get
			{
				return this.blockComment;
			}
			set
			{
				this.blockComment = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000408D File Offset: 0x0000228D
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00004095 File Offset: 0x00002295
		public WorkItemType WorkType
		{
			get
			{
				return this.workType;
			}
			set
			{
				this.workType = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000409E File Offset: 0x0000229E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000040A6 File Offset: 0x000022A6
		public DateTime InsertTime
		{
			get
			{
				return this.insertTime;
			}
			set
			{
				this.insertTime = value;
			}
		}

		// Token: 0x04000042 RID: 66
		private WorkItemPriority priority;

		// Token: 0x04000043 RID: 67
		private IPAddress senderAddress;

		// Token: 0x04000044 RID: 68
		private DateTime insertTime;

		// Token: 0x04000045 RID: 69
		private int blockPeriod;

		// Token: 0x04000046 RID: 70
		private string blockComment;

		// Token: 0x04000047 RID: 71
		private WorkItemType workType;
	}
}
