using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000490 RID: 1168
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayConfigNotFoundException : TransientException
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x000BF789 File Offset: 0x000BD989
		public ReplayConfigNotFoundException(string dbName, string serverName) : base(ReplayStrings.ReplayConfigNotFoundException(dbName, serverName))
		{
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000BF7A6 File Offset: 0x000BD9A6
		public ReplayConfigNotFoundException(string dbName, string serverName, Exception innerException) : base(ReplayStrings.ReplayConfigNotFoundException(dbName, serverName), innerException)
		{
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000BF7C4 File Offset: 0x000BD9C4
		protected ReplayConfigNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000BF819 File Offset: 0x000BDA19
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000BF845 File Offset: 0x000BDA45
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000BF84D File Offset: 0x000BDA4D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040014E5 RID: 5349
		private readonly string dbName;

		// Token: 0x040014E6 RID: 5350
		private readonly string serverName;
	}
}
