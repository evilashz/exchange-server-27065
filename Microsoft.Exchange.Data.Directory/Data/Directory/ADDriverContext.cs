using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal class ADDriverContext
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		internal ADDriverContext(ADServerSettings serverSettings, ContextMode mode)
		{
			this.serverSettings = serverSettings;
			this.mode = mode;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000EBFA File Offset: 0x0000CDFA
		internal ADServerSettings ServerSettings
		{
			get
			{
				return this.serverSettings;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000EC02 File Offset: 0x0000CE02
		internal ContextMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x04000099 RID: 153
		private ADServerSettings serverSettings;

		// Token: 0x0400009A RID: 154
		private ContextMode mode;
	}
}
