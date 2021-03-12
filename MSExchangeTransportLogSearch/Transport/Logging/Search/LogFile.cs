using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000022 RID: 34
	internal class LogFile
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000088 RID: 136 RVA: 0x00004D75 File Offset: 0x00002F75
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x00004D83 File Offset: 0x00002F83
		public event ProcessRowEventHandler ProcessRow
		{
			add
			{
				this.logFileInfo.ProcessRow += value;
			}
			remove
			{
				this.logFileInfo.ProcessRow -= value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600008A RID: 138 RVA: 0x00004D91 File Offset: 0x00002F91
		// (remove) Token: 0x0600008B RID: 139 RVA: 0x00004D9F File Offset: 0x00002F9F
		public event EventHandler LogFileClosed
		{
			add
			{
				this.logFileInfo.LogFileClosed += value;
			}
			remove
			{
				this.logFileInfo.LogFileClosed -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600008C RID: 140 RVA: 0x00004DAD File Offset: 0x00002FAD
		// (remove) Token: 0x0600008D RID: 141 RVA: 0x00004DBB File Offset: 0x00002FBB
		public event EventHandler LogFileDeleted
		{
			add
			{
				this.logFileInfo.LogFileDeleted += value;
			}
			remove
			{
				this.logFileInfo.LogFileDeleted -= value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004DC9 File Offset: 0x00002FC9
		public string Prefix
		{
			get
			{
				return this.logFileInfo.Prefix;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public LogFile(LogFileInfo logFileInfo)
		{
			this.logFileInfo = logFileInfo;
		}

		// Token: 0x04000054 RID: 84
		public const string MessageTrackingPrefix = "MSGTRK";

		// Token: 0x04000055 RID: 85
		public const string TransportSyncHealthPrefix = "SYNCHEALTHLOG";

		// Token: 0x04000056 RID: 86
		private LogFileInfo logFileInfo;
	}
}
