using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004AC RID: 1196
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementUnexpectedTempFilesException : LastLogReplacementException
	{
		// Token: 0x06002D1F RID: 11551 RVA: 0x000C0BED File Offset: 0x000BEDED
		public LastLogReplacementUnexpectedTempFilesException(string dbCopy, string logPath) : base(ReplayStrings.LastLogReplacementUnexpectedTempFilesException(dbCopy, logPath))
		{
			this.dbCopy = dbCopy;
			this.logPath = logPath;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000C0C0F File Offset: 0x000BEE0F
		public LastLogReplacementUnexpectedTempFilesException(string dbCopy, string logPath, Exception innerException) : base(ReplayStrings.LastLogReplacementUnexpectedTempFilesException(dbCopy, logPath), innerException)
		{
			this.dbCopy = dbCopy;
			this.logPath = logPath;
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000C0C34 File Offset: 0x000BEE34
		protected LastLogReplacementUnexpectedTempFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.logPath = (string)info.GetValue("logPath", typeof(string));
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000C0C89 File Offset: 0x000BEE89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("logPath", this.logPath);
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000C0CB5 File Offset: 0x000BEEB5
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000C0CBD File Offset: 0x000BEEBD
		public string LogPath
		{
			get
			{
				return this.logPath;
			}
		}

		// Token: 0x04001516 RID: 5398
		private readonly string dbCopy;

		// Token: 0x04001517 RID: 5399
		private readonly string logPath;
	}
}
