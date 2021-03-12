using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoCopyOnServerException : ThirdPartyReplicationException
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000031CD File Offset: 0x000013CD
		public NoCopyOnServerException(Guid dbId, string dbName, string serverName) : base(ThirdPartyReplication.NoCopyOnServer(dbId, dbName, serverName))
		{
			this.dbId = dbId;
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000031F7 File Offset: 0x000013F7
		public NoCopyOnServerException(Guid dbId, string dbName, string serverName, Exception innerException) : base(ThirdPartyReplication.NoCopyOnServer(dbId, dbName, serverName), innerException)
		{
			this.dbId = dbId;
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003224 File Offset: 0x00001424
		protected NoCopyOnServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbId = (Guid)info.GetValue("dbId", typeof(Guid));
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000329C File Offset: 0x0000149C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbId", this.dbId);
			info.AddValue("dbName", this.dbName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000032E9 File Offset: 0x000014E9
		public Guid DbId
		{
			get
			{
				return this.dbId;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000032F1 File Offset: 0x000014F1
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000032F9 File Offset: 0x000014F9
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000022 RID: 34
		private readonly Guid dbId;

		// Token: 0x04000023 RID: 35
		private readonly string dbName;

		// Token: 0x04000024 RID: 36
		private readonly string serverName;
	}
}
