using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200000C RID: 12
	public struct MLOGSHIP_INFO
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00001124 File Offset: 0x00000524
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00001138 File Offset: 0x00000538
		public ESE_LOGSHIP Type
		{
			get
			{
				return this.<backing_store>Type;
			}
			set
			{
				this.<backing_store>Type = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00001150 File Offset: 0x00000550
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00001164 File Offset: 0x00000564
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

		// Token: 0x04000093 RID: 147
		private ESE_LOGSHIP <backing_store>Type;

		// Token: 0x04000094 RID: 148
		private string <backing_store>Name;
	}
}
