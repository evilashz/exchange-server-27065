using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004AA RID: 1194
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementFailedErrorException : LastLogReplacementException
	{
		// Token: 0x06002D10 RID: 11536 RVA: 0x000C0952 File Offset: 0x000BEB52
		public LastLogReplacementFailedErrorException(string dbCopy, string e00log, string error) : base(ReplayStrings.LastLogReplacementFailedErrorException(dbCopy, e00log, error))
		{
			this.dbCopy = dbCopy;
			this.e00log = e00log;
			this.error = error;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000C097C File Offset: 0x000BEB7C
		public LastLogReplacementFailedErrorException(string dbCopy, string e00log, string error, Exception innerException) : base(ReplayStrings.LastLogReplacementFailedErrorException(dbCopy, e00log, error), innerException)
		{
			this.dbCopy = dbCopy;
			this.e00log = e00log;
			this.error = error;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000C09A8 File Offset: 0x000BEBA8
		protected LastLogReplacementFailedErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.e00log = (string)info.GetValue("e00log", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000C0A1D File Offset: 0x000BEC1D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("e00log", this.e00log);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000C0A5A File Offset: 0x000BEC5A
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000C0A62 File Offset: 0x000BEC62
		public string E00log
		{
			get
			{
				return this.e00log;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000C0A6A File Offset: 0x000BEC6A
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400150F RID: 5391
		private readonly string dbCopy;

		// Token: 0x04001510 RID: 5392
		private readonly string e00log;

		// Token: 0x04001511 RID: 5393
		private readonly string error;
	}
}
