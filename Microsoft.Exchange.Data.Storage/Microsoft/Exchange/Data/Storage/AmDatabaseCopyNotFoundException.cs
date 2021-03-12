using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D0 RID: 208
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDatabaseCopyNotFoundException : AmServerException
	{
		// Token: 0x0600128D RID: 4749 RVA: 0x00067D7C File Offset: 0x00065F7C
		public AmDatabaseCopyNotFoundException(string dbName, string serverName) : base(ServerStrings.AmDatabaseCopyNotFoundException(dbName, serverName))
		{
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00067D9E File Offset: 0x00065F9E
		public AmDatabaseCopyNotFoundException(string dbName, string serverName, Exception innerException) : base(ServerStrings.AmDatabaseCopyNotFoundException(dbName, serverName), innerException)
		{
			this.dbName = dbName;
			this.serverName = serverName;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00067DC4 File Offset: 0x00065FC4
		protected AmDatabaseCopyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00067E19 File Offset: 0x00066019
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00067E45 File Offset: 0x00066045
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00067E4D File Offset: 0x0006604D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400095F RID: 2399
		private readonly string dbName;

		// Token: 0x04000960 RID: 2400
		private readonly string serverName;
	}
}
