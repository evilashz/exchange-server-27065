using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004AB RID: 1195
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementTooManyTempFilesException : LastLogReplacementException
	{
		// Token: 0x06002D17 RID: 11543 RVA: 0x000C0A72 File Offset: 0x000BEC72
		public LastLogReplacementTooManyTempFilesException(string dbCopy, string filter, int count, string logPath) : base(ReplayStrings.LastLogReplacementTooManyTempFilesException(dbCopy, filter, count, logPath))
		{
			this.dbCopy = dbCopy;
			this.filter = filter;
			this.count = count;
			this.logPath = logPath;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000C0AA6 File Offset: 0x000BECA6
		public LastLogReplacementTooManyTempFilesException(string dbCopy, string filter, int count, string logPath, Exception innerException) : base(ReplayStrings.LastLogReplacementTooManyTempFilesException(dbCopy, filter, count, logPath), innerException)
		{
			this.dbCopy = dbCopy;
			this.filter = filter;
			this.count = count;
			this.logPath = logPath;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000C0ADC File Offset: 0x000BECDC
		protected LastLogReplacementTooManyTempFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.filter = (string)info.GetValue("filter", typeof(string));
			this.count = (int)info.GetValue("count", typeof(int));
			this.logPath = (string)info.GetValue("logPath", typeof(string));
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000C0B74 File Offset: 0x000BED74
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("filter", this.filter);
			info.AddValue("count", this.count);
			info.AddValue("logPath", this.logPath);
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000C0BCD File Offset: 0x000BEDCD
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000C0BD5 File Offset: 0x000BEDD5
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000C0BDD File Offset: 0x000BEDDD
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000C0BE5 File Offset: 0x000BEDE5
		public string LogPath
		{
			get
			{
				return this.logPath;
			}
		}

		// Token: 0x04001512 RID: 5394
		private readonly string dbCopy;

		// Token: 0x04001513 RID: 5395
		private readonly string filter;

		// Token: 0x04001514 RID: 5396
		private readonly int count;

		// Token: 0x04001515 RID: 5397
		private readonly string logPath;
	}
}
