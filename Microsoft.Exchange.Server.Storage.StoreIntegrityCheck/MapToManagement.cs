using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200001D RID: 29
	public class MapToManagement : Attribute
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000056B2 File Offset: 0x000038B2
		public MapToManagement(string mapName = null, bool skip = false)
		{
			this.skip = skip;
			this.mapName = (mapName ?? string.Empty);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000056D1 File Offset: 0x000038D1
		public bool Skip
		{
			get
			{
				return this.skip;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000056D9 File Offset: 0x000038D9
		public string MapName
		{
			get
			{
				return this.mapName;
			}
		}

		// Token: 0x04000066 RID: 102
		private readonly bool skip;

		// Token: 0x04000067 RID: 103
		private readonly string mapName;
	}
}
