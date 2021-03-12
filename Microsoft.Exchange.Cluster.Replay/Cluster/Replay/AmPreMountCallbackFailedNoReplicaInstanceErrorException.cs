using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000421 RID: 1057
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmPreMountCallbackFailedNoReplicaInstanceErrorException : AmServerException
	{
		// Token: 0x06002A1E RID: 10782 RVA: 0x000BAE81 File Offset: 0x000B9081
		public AmPreMountCallbackFailedNoReplicaInstanceErrorException(string dbName, string server, string errMsg) : base(ReplayStrings.AmPreMountCallbackFailedNoReplicaInstanceErrorException(dbName, server, errMsg))
		{
			this.dbName = dbName;
			this.server = server;
			this.errMsg = errMsg;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000BAEAB File Offset: 0x000B90AB
		public AmPreMountCallbackFailedNoReplicaInstanceErrorException(string dbName, string server, string errMsg, Exception innerException) : base(ReplayStrings.AmPreMountCallbackFailedNoReplicaInstanceErrorException(dbName, server, errMsg), innerException)
		{
			this.dbName = dbName;
			this.server = server;
			this.errMsg = errMsg;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000BAED8 File Offset: 0x000B90D8
		protected AmPreMountCallbackFailedNoReplicaInstanceErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000BAF4D File Offset: 0x000B914D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("server", this.server);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000BAF8A File Offset: 0x000B918A
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000BAF92 File Offset: 0x000B9192
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x000BAF9A File Offset: 0x000B919A
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001441 RID: 5185
		private readonly string dbName;

		// Token: 0x04001442 RID: 5186
		private readonly string server;

		// Token: 0x04001443 RID: 5187
		private readonly string errMsg;
	}
}
