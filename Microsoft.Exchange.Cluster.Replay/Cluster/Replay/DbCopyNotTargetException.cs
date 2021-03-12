using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200043C RID: 1084
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbCopyNotTargetException : SeedPrepareException
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x000BC254 File Offset: 0x000BA454
		public DbCopyNotTargetException(string dbName, string serverName) : base(ReplayStrings.DbCopyNotTargetException(dbName, serverName))
		{
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000BC276 File Offset: 0x000BA476
		public DbCopyNotTargetException(string dbName, string serverName, Exception innerException) : base(ReplayStrings.DbCopyNotTargetException(dbName, serverName), innerException)
		{
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000BC29C File Offset: 0x000BA49C
		protected DbCopyNotTargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000BC2F1 File Offset: 0x000BA4F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000BC31D File Offset: 0x000BA51D
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000BC325 File Offset: 0x000BA525
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04001471 RID: 5233
		private readonly string dbName;

		// Token: 0x04001472 RID: 5234
		private readonly string serverName;
	}
}
