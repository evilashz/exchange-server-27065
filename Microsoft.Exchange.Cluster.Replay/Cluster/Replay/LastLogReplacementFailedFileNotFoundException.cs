using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A8 RID: 1192
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementFailedFileNotFoundException : LastLogReplacementException
	{
		// Token: 0x06002D02 RID: 11522 RVA: 0x000C0711 File Offset: 0x000BE911
		public LastLogReplacementFailedFileNotFoundException(string dbCopy, string missingFile, string e00log) : base(ReplayStrings.LastLogReplacementFailedFileNotFoundException(dbCopy, missingFile, e00log))
		{
			this.dbCopy = dbCopy;
			this.missingFile = missingFile;
			this.e00log = e00log;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000C073B File Offset: 0x000BE93B
		public LastLogReplacementFailedFileNotFoundException(string dbCopy, string missingFile, string e00log, Exception innerException) : base(ReplayStrings.LastLogReplacementFailedFileNotFoundException(dbCopy, missingFile, e00log), innerException)
		{
			this.dbCopy = dbCopy;
			this.missingFile = missingFile;
			this.e00log = e00log;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000C0768 File Offset: 0x000BE968
		protected LastLogReplacementFailedFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.missingFile = (string)info.GetValue("missingFile", typeof(string));
			this.e00log = (string)info.GetValue("e00log", typeof(string));
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000C07DD File Offset: 0x000BE9DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("missingFile", this.missingFile);
			info.AddValue("e00log", this.e00log);
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000C081A File Offset: 0x000BEA1A
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x000C0822 File Offset: 0x000BEA22
		public string MissingFile
		{
			get
			{
				return this.missingFile;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x000C082A File Offset: 0x000BEA2A
		public string E00log
		{
			get
			{
				return this.e00log;
			}
		}

		// Token: 0x04001509 RID: 5385
		private readonly string dbCopy;

		// Token: 0x0400150A RID: 5386
		private readonly string missingFile;

		// Token: 0x0400150B RID: 5387
		private readonly string e00log;
	}
}
