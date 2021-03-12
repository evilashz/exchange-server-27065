using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000470 RID: 1136
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMoveOperationOnTimeoutFailureCancelled : AmDbActionException
	{
		// Token: 0x06002BC8 RID: 11208 RVA: 0x000BE04A File Offset: 0x000BC24A
		public AmDbMoveOperationOnTimeoutFailureCancelled(string dbName, string fromServer) : base(ReplayStrings.AmDbMoveOperationOnTimeoutFailureCancelled(dbName, fromServer))
		{
			this.dbName = dbName;
			this.fromServer = fromServer;
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000BE06C File Offset: 0x000BC26C
		public AmDbMoveOperationOnTimeoutFailureCancelled(string dbName, string fromServer, Exception innerException) : base(ReplayStrings.AmDbMoveOperationOnTimeoutFailureCancelled(dbName, fromServer), innerException)
		{
			this.dbName = dbName;
			this.fromServer = fromServer;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000BE090 File Offset: 0x000BC290
		protected AmDbMoveOperationOnTimeoutFailureCancelled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.fromServer = (string)info.GetValue("fromServer", typeof(string));
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000BE0E5 File Offset: 0x000BC2E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("fromServer", this.fromServer);
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000BE111 File Offset: 0x000BC311
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000BE119 File Offset: 0x000BC319
		public string FromServer
		{
			get
			{
				return this.fromServer;
			}
		}

		// Token: 0x040014AF RID: 5295
		private readonly string dbName;

		// Token: 0x040014B0 RID: 5296
		private readonly string fromServer;
	}
}
