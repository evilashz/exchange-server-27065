using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000447 RID: 1095
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CIStatusFailedException : SeedPrepareException
	{
		// Token: 0x06002AF4 RID: 10996 RVA: 0x000BC8ED File Offset: 0x000BAAED
		public CIStatusFailedException(string server, string db) : base(ReplayStrings.CIStatusFailedException(server, db))
		{
			this.server = server;
			this.db = db;
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000BC90F File Offset: 0x000BAB0F
		public CIStatusFailedException(string server, string db, Exception innerException) : base(ReplayStrings.CIStatusFailedException(server, db), innerException)
		{
			this.server = server;
			this.db = db;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000BC934 File Offset: 0x000BAB34
		protected CIStatusFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.db = (string)info.GetValue("db", typeof(string));
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000BC989 File Offset: 0x000BAB89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("db", this.db);
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000BC9B5 File Offset: 0x000BABB5
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x000BC9BD File Offset: 0x000BABBD
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x0400147F RID: 5247
		private readonly string server;

		// Token: 0x04001480 RID: 5248
		private readonly string db;
	}
}
