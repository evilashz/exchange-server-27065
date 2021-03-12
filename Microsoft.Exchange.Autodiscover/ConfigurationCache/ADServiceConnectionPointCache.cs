using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x0200002A RID: 42
	internal class ADServiceConnectionPointCache : SimpleConfigCache<ADServiceConnectionPoint, string>
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000077AD File Offset: 0x000059AD
		internal override IEnumerable<ADServiceConnectionPoint> StartSearch(IConfigurationSession session)
		{
			return session.FindPaged<ADServiceConnectionPoint>(null, QueryScope.SubTree, new TextFilter(ADServiceConnectionPointSchema.Keywords, "77378F46-2C66-4aa9-A6A6-3E7A48B19596", MatchOptions.FullString, MatchFlags.IgnoreCase), null, 0);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000077CC File Offset: 0x000059CC
		protected override string[] KeysFromConfig(ADServiceConnectionPoint adScp)
		{
			ServerId src = new ServerId(adScp.Id.Parent.Parent.Parent);
			Server configFromSourceObject = ServerConfigurationCache.Singleton.ServerCache.GetConfigFromSourceObject(src);
			if (configFromSourceObject == null)
			{
				return new string[0];
			}
			return new string[]
			{
				configFromSourceObject.Fqdn
			};
		}

		// Token: 0x0400015E RID: 350
		private const string UrlScpGuidString = "77378F46-2C66-4aa9-A6A6-3E7A48B19596";
	}
}
