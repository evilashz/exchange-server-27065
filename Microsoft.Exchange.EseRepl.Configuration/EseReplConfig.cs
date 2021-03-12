using System;
using Microsoft.Exchange.EseRepl.Common;

namespace Microsoft.Exchange.EseRepl.Configuration
{
	// Token: 0x02000002 RID: 2
	internal class EseReplConfig : IEseReplConfig
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private EseReplConfig()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020ED File Offset: 0x000002ED
		public static EseReplConfig Instance
		{
			get
			{
				return EseReplConfig.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000002F4
		public string RegistryRootKeyName
		{
			get
			{
				return this.registryRootKeyName;
			}
		}

		// Token: 0x04000001 RID: 1
		private static EseReplConfig instance = new EseReplConfig();

		// Token: 0x04000002 RID: 2
		private readonly string registryRootKeyName = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\Replay\\Parameters", "v15");
	}
}
