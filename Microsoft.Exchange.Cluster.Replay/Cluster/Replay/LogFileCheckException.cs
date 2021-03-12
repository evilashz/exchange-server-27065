using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D1 RID: 977
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogFileCheckException : FileCheckException
	{
		// Token: 0x0600287C RID: 10364 RVA: 0x000B7F97 File Offset: 0x000B6197
		public LogFileCheckException(string logFileName, string errMsg) : base(ReplayStrings.LogFileCheckError(logFileName, errMsg))
		{
			this.logFileName = logFileName;
			this.errMsg = errMsg;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000B7FB9 File Offset: 0x000B61B9
		public LogFileCheckException(string logFileName, string errMsg, Exception innerException) : base(ReplayStrings.LogFileCheckError(logFileName, errMsg), innerException)
		{
			this.logFileName = logFileName;
			this.errMsg = errMsg;
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000B7FDC File Offset: 0x000B61DC
		protected LogFileCheckException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logFileName = (string)info.GetValue("logFileName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000B8031 File Offset: 0x000B6231
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logFileName", this.logFileName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x000B805D File Offset: 0x000B625D
		public string LogFileName
		{
			get
			{
				return this.logFileName;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x000B8065 File Offset: 0x000B6265
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040013DF RID: 5087
		private readonly string logFileName;

		// Token: 0x040013E0 RID: 5088
		private readonly string errMsg;
	}
}
