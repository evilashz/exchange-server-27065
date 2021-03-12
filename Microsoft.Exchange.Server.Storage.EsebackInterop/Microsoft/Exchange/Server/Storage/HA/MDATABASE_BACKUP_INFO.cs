using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200000A RID: 10
	public struct MDATABASE_BACKUP_INFO
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00001000 File Offset: 0x00000400
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00001014 File Offset: 0x00000414
		public string Name
		{
			get
			{
				return this.<backing_store>Name;
			}
			set
			{
				this.<backing_store>Name = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00001028 File Offset: 0x00000428
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000103C File Offset: 0x0000043C
		public string Path
		{
			get
			{
				return this.<backing_store>Path;
			}
			set
			{
				this.<backing_store>Path = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00001050 File Offset: 0x00000450
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00001068 File Offset: 0x00000468
		public Guid Guid
		{
			get
			{
				return this.<backing_store>Guid;
			}
			set
			{
				this.<backing_store>Guid = value;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000107C File Offset: 0x0000047C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00001090 File Offset: 0x00000490
		public DatabaseBackupInfoFlags Flags
		{
			get
			{
				return this.<backing_store>Flags;
			}
			set
			{
				this.<backing_store>Flags = value;
			}
		}

		// Token: 0x0400008C RID: 140
		private string <backing_store>Name;

		// Token: 0x0400008D RID: 141
		private string <backing_store>Path;

		// Token: 0x0400008E RID: 142
		private Guid <backing_store>Guid;

		// Token: 0x0400008F RID: 143
		private DatabaseBackupInfoFlags <backing_store>Flags;
	}
}
