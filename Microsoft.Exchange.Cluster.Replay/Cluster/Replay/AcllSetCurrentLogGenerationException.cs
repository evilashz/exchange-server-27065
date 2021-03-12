using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000435 RID: 1077
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllSetCurrentLogGenerationException : TransientException
	{
		// Token: 0x06002A93 RID: 10899 RVA: 0x000BBD81 File Offset: 0x000B9F81
		public AcllSetCurrentLogGenerationException(string dbCopy, string e00logPath, string err) : base(ReplayStrings.AcllSetCurrentLogGenerationException(dbCopy, e00logPath, err))
		{
			this.dbCopy = dbCopy;
			this.e00logPath = e00logPath;
			this.err = err;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000BBDA6 File Offset: 0x000B9FA6
		public AcllSetCurrentLogGenerationException(string dbCopy, string e00logPath, string err, Exception innerException) : base(ReplayStrings.AcllSetCurrentLogGenerationException(dbCopy, e00logPath, err), innerException)
		{
			this.dbCopy = dbCopy;
			this.e00logPath = e00logPath;
			this.err = err;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000BBDD0 File Offset: 0x000B9FD0
		protected AcllSetCurrentLogGenerationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.e00logPath = (string)info.GetValue("e00logPath", typeof(string));
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000BBE45 File Offset: 0x000BA045
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("e00logPath", this.e00logPath);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000BBE82 File Offset: 0x000BA082
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000BBE8A File Offset: 0x000BA08A
		public string E00logPath
		{
			get
			{
				return this.e00logPath;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000BBE92 File Offset: 0x000BA092
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04001466 RID: 5222
		private readonly string dbCopy;

		// Token: 0x04001467 RID: 5223
		private readonly string e00logPath;

		// Token: 0x04001468 RID: 5224
		private readonly string err;
	}
}
