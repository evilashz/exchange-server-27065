using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000448 RID: 1096
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGetCopyStatusException : SeedPrepareException
	{
		// Token: 0x06002AFA RID: 11002 RVA: 0x000BC9C5 File Offset: 0x000BABC5
		public FailedToGetCopyStatusException(string server, string db) : base(ReplayStrings.FailedToGetCopyStatus(server, db))
		{
			this.server = server;
			this.db = db;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000BC9E7 File Offset: 0x000BABE7
		public FailedToGetCopyStatusException(string server, string db, Exception innerException) : base(ReplayStrings.FailedToGetCopyStatus(server, db), innerException)
		{
			this.server = server;
			this.db = db;
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000BCA0C File Offset: 0x000BAC0C
		protected FailedToGetCopyStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.db = (string)info.GetValue("db", typeof(string));
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000BCA61 File Offset: 0x000BAC61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("db", this.db);
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000BCA8D File Offset: 0x000BAC8D
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x000BCA95 File Offset: 0x000BAC95
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x04001481 RID: 5249
		private readonly string server;

		// Token: 0x04001482 RID: 5250
		private readonly string db;
	}
}
