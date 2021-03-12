using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000420 RID: 1056
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmPreMountCallbackFailedNoReplicaInstanceException : AmServerException
	{
		// Token: 0x06002A18 RID: 10776 RVA: 0x000BADA9 File Offset: 0x000B8FA9
		public AmPreMountCallbackFailedNoReplicaInstanceException(string dbName, string server) : base(ReplayStrings.AmPreMountCallbackFailedNoReplicaInstanceException(dbName, server))
		{
			this.dbName = dbName;
			this.server = server;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000BADCB File Offset: 0x000B8FCB
		public AmPreMountCallbackFailedNoReplicaInstanceException(string dbName, string server, Exception innerException) : base(ReplayStrings.AmPreMountCallbackFailedNoReplicaInstanceException(dbName, server), innerException)
		{
			this.dbName = dbName;
			this.server = server;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000BADF0 File Offset: 0x000B8FF0
		protected AmPreMountCallbackFailedNoReplicaInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000BAE45 File Offset: 0x000B9045
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("server", this.server);
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000BAE71 File Offset: 0x000B9071
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000BAE79 File Offset: 0x000B9079
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0400143F RID: 5183
		private readonly string dbName;

		// Token: 0x04001440 RID: 5184
		private readonly string server;
	}
}
