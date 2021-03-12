using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000433 RID: 1075
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllLastLogTimeErrorException : TransientException
	{
		// Token: 0x06002A86 RID: 10886 RVA: 0x000BBB99 File Offset: 0x000B9D99
		public AcllLastLogTimeErrorException(string dbCopy, string logfilePath, string err) : base(ReplayStrings.AcllLastLogTimeErrorException(dbCopy, logfilePath, err))
		{
			this.dbCopy = dbCopy;
			this.logfilePath = logfilePath;
			this.err = err;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000BBBBE File Offset: 0x000B9DBE
		public AcllLastLogTimeErrorException(string dbCopy, string logfilePath, string err, Exception innerException) : base(ReplayStrings.AcllLastLogTimeErrorException(dbCopy, logfilePath, err), innerException)
		{
			this.dbCopy = dbCopy;
			this.logfilePath = logfilePath;
			this.err = err;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000BBBE8 File Offset: 0x000B9DE8
		protected AcllLastLogTimeErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.logfilePath = (string)info.GetValue("logfilePath", typeof(string));
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000BBC5D File Offset: 0x000B9E5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("logfilePath", this.logfilePath);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000BBC9A File Offset: 0x000B9E9A
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x000BBCA2 File Offset: 0x000B9EA2
		public string LogfilePath
		{
			get
			{
				return this.logfilePath;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x000BBCAA File Offset: 0x000B9EAA
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04001461 RID: 5217
		private readonly string dbCopy;

		// Token: 0x04001462 RID: 5218
		private readonly string logfilePath;

		// Token: 0x04001463 RID: 5219
		private readonly string err;
	}
}
