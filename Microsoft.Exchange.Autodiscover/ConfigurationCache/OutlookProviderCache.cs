using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000028 RID: 40
	internal sealed class OutlookProviderCache : SimpleConfigCache<OutlookProvider, string>
	{
		// Token: 0x06000139 RID: 313 RVA: 0x000074C8 File Offset: 0x000056C8
		protected override string[] KeysFromConfig(OutlookProvider config)
		{
			return new string[]
			{
				config.Name
			};
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000074E6 File Offset: 0x000056E6
		protected override string KeyFromSourceObject(string id)
		{
			return id;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000773C File Offset: 0x0000593C
		internal IEnumerable<OutlookProvider> Providers()
		{
			OutlookProvider op = base.GetConfigFromSourceObject("EXCH");
			if (op != null)
			{
				yield return op;
			}
			op = base.GetConfigFromSourceObject("EXPR");
			if (op != null)
			{
				yield return op;
			}
			foreach (OutlookProvider opc in this.cache.Values)
			{
				if (string.Compare(opc.Name, "EXCH", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(opc.Name, "EXPR", StringComparison.OrdinalIgnoreCase) != 0)
				{
					yield return opc;
				}
			}
			yield break;
		}

		// Token: 0x0400015B RID: 347
		internal const string ExchangeExternalAccess = "EXPR";

		// Token: 0x0400015C RID: 348
		internal const string ExchangeInternalAccess = "EXCH";

		// Token: 0x0400015D RID: 349
		internal const string ExchangeWebAccess = "WEB";
	}
}
