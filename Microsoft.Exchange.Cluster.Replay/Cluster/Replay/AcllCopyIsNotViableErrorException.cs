using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000432 RID: 1074
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllCopyIsNotViableErrorException : TransientException
	{
		// Token: 0x06002A80 RID: 10880 RVA: 0x000BBACD File Offset: 0x000B9CCD
		public AcllCopyIsNotViableErrorException(string dbCopy, string err) : base(ReplayStrings.AcllCopyIsNotViableErrorException(dbCopy, err))
		{
			this.dbCopy = dbCopy;
			this.err = err;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000BBAEA File Offset: 0x000B9CEA
		public AcllCopyIsNotViableErrorException(string dbCopy, string err, Exception innerException) : base(ReplayStrings.AcllCopyIsNotViableErrorException(dbCopy, err), innerException)
		{
			this.dbCopy = dbCopy;
			this.err = err;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000BBB08 File Offset: 0x000B9D08
		protected AcllCopyIsNotViableErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000BBB5D File Offset: 0x000B9D5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000BBB89 File Offset: 0x000B9D89
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x000BBB91 File Offset: 0x000B9D91
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x0400145F RID: 5215
		private readonly string dbCopy;

		// Token: 0x04001460 RID: 5216
		private readonly string err;
	}
}
