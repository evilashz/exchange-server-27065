using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200000B RID: 11
	public struct MINSTANCE_BACKUP_INFO
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000010A8 File Offset: 0x000004A8
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000010C0 File Offset: 0x000004C0
		public JET_INSTANCE Instance
		{
			get
			{
				return this.<backing_store>Instance;
			}
			set
			{
				this.<backing_store>Instance = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000010D4 File Offset: 0x000004D4
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000010E8 File Offset: 0x000004E8
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000010FC File Offset: 0x000004FC
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00001110 File Offset: 0x00000510
		public MDATABASE_BACKUP_INFO[] Databases
		{
			get
			{
				return this.<backing_store>Databases;
			}
			set
			{
				this.<backing_store>Databases = value;
			}
		}

		// Token: 0x04000090 RID: 144
		private JET_INSTANCE <backing_store>Instance;

		// Token: 0x04000091 RID: 145
		private string <backing_store>Name;

		// Token: 0x04000092 RID: 146
		private MDATABASE_BACKUP_INFO[] <backing_store>Databases;
	}
}
