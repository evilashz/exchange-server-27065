using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000415 RID: 1045
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceCouldNotFindReplayConfigException : ReplayDbOperationException
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x000BA539 File Offset: 0x000B8739
		public ReplayServiceCouldNotFindReplayConfigException(string database, string server) : base(ReplayStrings.ReplayServiceCouldNotFindReplayConfigException(database, server))
		{
			this.database = database;
			this.server = server;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000BA55B File Offset: 0x000B875B
		public ReplayServiceCouldNotFindReplayConfigException(string database, string server, Exception innerException) : base(ReplayStrings.ReplayServiceCouldNotFindReplayConfigException(database, server), innerException)
		{
			this.database = database;
			this.server = server;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000BA580 File Offset: 0x000B8780
		protected ReplayServiceCouldNotFindReplayConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000BA5D5 File Offset: 0x000B87D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("server", this.server);
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000BA601 File Offset: 0x000B8801
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000BA609 File Offset: 0x000B8809
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0400142B RID: 5163
		private readonly string database;

		// Token: 0x0400142C RID: 5164
		private readonly string server;
	}
}
