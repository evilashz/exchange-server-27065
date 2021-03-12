using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000029 RID: 41
	internal class ServerCache : SimpleConfigCache<Server, ServerId>
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00007764 File Offset: 0x00005964
		protected override string[] KeysFromConfig(Server server)
		{
			List<string> list = new List<string>(base.KeysFromConfig(server));
			if (!string.IsNullOrEmpty(server.ExchangeLegacyDN))
			{
				list.Add(server.ExchangeLegacyDN);
			}
			return list.ToArray();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000779D File Offset: 0x0000599D
		protected override string KeyFromSourceObject(ServerId id)
		{
			return id.Key;
		}
	}
}
