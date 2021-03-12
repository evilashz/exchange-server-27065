using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000422 RID: 1058
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmPreMountCallbackFailedMountInhibitException : AmServerException
	{
		// Token: 0x06002A25 RID: 10789 RVA: 0x000BAFA2 File Offset: 0x000B91A2
		public AmPreMountCallbackFailedMountInhibitException(string dbName, string server, string errMsg) : base(ReplayStrings.AmPreMountCallbackFailedMountInhibitException(dbName, server, errMsg))
		{
			this.dbName = dbName;
			this.server = server;
			this.errMsg = errMsg;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000BAFCC File Offset: 0x000B91CC
		public AmPreMountCallbackFailedMountInhibitException(string dbName, string server, string errMsg, Exception innerException) : base(ReplayStrings.AmPreMountCallbackFailedMountInhibitException(dbName, server, errMsg), innerException)
		{
			this.dbName = dbName;
			this.server = server;
			this.errMsg = errMsg;
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000BAFF8 File Offset: 0x000B91F8
		protected AmPreMountCallbackFailedMountInhibitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000BB06D File Offset: 0x000B926D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("server", this.server);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000BB0AA File Offset: 0x000B92AA
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000BB0B2 File Offset: 0x000B92B2
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000BB0BA File Offset: 0x000B92BA
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001444 RID: 5188
		private readonly string dbName;

		// Token: 0x04001445 RID: 5189
		private readonly string server;

		// Token: 0x04001446 RID: 5190
		private readonly string errMsg;
	}
}
