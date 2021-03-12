using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A9 RID: 1193
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementFailedUnexpectedFileFoundException : LastLogReplacementException
	{
		// Token: 0x06002D09 RID: 11529 RVA: 0x000C0832 File Offset: 0x000BEA32
		public LastLogReplacementFailedUnexpectedFileFoundException(string dbCopy, string unexpectedFile, string e00log) : base(ReplayStrings.LastLogReplacementFailedUnexpectedFileFoundException(dbCopy, unexpectedFile, e00log))
		{
			this.dbCopy = dbCopy;
			this.unexpectedFile = unexpectedFile;
			this.e00log = e00log;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000C085C File Offset: 0x000BEA5C
		public LastLogReplacementFailedUnexpectedFileFoundException(string dbCopy, string unexpectedFile, string e00log, Exception innerException) : base(ReplayStrings.LastLogReplacementFailedUnexpectedFileFoundException(dbCopy, unexpectedFile, e00log), innerException)
		{
			this.dbCopy = dbCopy;
			this.unexpectedFile = unexpectedFile;
			this.e00log = e00log;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000C0888 File Offset: 0x000BEA88
		protected LastLogReplacementFailedUnexpectedFileFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.unexpectedFile = (string)info.GetValue("unexpectedFile", typeof(string));
			this.e00log = (string)info.GetValue("e00log", typeof(string));
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000C08FD File Offset: 0x000BEAFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("unexpectedFile", this.unexpectedFile);
			info.AddValue("e00log", this.e00log);
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000C093A File Offset: 0x000BEB3A
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000C0942 File Offset: 0x000BEB42
		public string UnexpectedFile
		{
			get
			{
				return this.unexpectedFile;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000C094A File Offset: 0x000BEB4A
		public string E00log
		{
			get
			{
				return this.e00log;
			}
		}

		// Token: 0x0400150C RID: 5388
		private readonly string dbCopy;

		// Token: 0x0400150D RID: 5389
		private readonly string unexpectedFile;

		// Token: 0x0400150E RID: 5390
		private readonly string e00log;
	}
}
