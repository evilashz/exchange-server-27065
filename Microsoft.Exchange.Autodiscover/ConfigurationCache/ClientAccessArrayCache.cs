using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000020 RID: 32
	internal class ClientAccessArrayCache : SimpleConfigCache<ClientAccessArray, ServerId>
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00006E2C File Offset: 0x0000502C
		protected override string[] KeysFromConfig(ClientAccessArray array)
		{
			List<string> list = new List<string>(base.KeysFromConfig(array));
			if (!string.IsNullOrEmpty(array.ExchangeLegacyDN))
			{
				list.Add(array.ExchangeLegacyDN);
			}
			return list.ToArray();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006E65 File Offset: 0x00005065
		protected override string KeyFromSourceObject(ServerId id)
		{
			return id.Key;
		}
	}
}
