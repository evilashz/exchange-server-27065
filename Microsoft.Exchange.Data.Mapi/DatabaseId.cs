using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public sealed class DatabaseId : MapiObjectId
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000256C File Offset: 0x0000076C
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.databaseName))
			{
				return this.Guid.ToString();
			}
			return this.databaseName;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025A1 File Offset: 0x000007A1
		public DatabaseId()
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025A9 File Offset: 0x000007A9
		public DatabaseId(byte[] bytes) : base(bytes)
		{
			this.guid = new Guid(bytes);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025BE File Offset: 0x000007BE
		public DatabaseId(Guid guid) : base(new MapiEntryId(guid.ToByteArray()))
		{
			this.guid = guid;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025D9 File Offset: 0x000007D9
		public DatabaseId(string serverName, string dbName)
		{
			this.serverName = serverName;
			this.databaseName = dbName;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025EF File Offset: 0x000007EF
		internal DatabaseId(MapiEntryId entryId, string serverName, string dbName, Guid guid) : base(entryId)
		{
			this.serverName = serverName;
			this.databaseName = dbName;
			this.guid = guid;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000260E File Offset: 0x0000080E
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002616 File Offset: 0x00000816
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000261E File Offset: 0x0000081E
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000005 RID: 5
		private readonly Guid guid;

		// Token: 0x04000006 RID: 6
		private readonly string serverName;

		// Token: 0x04000007 RID: 7
		private readonly string databaseName;
	}
}
