using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x0200001E RID: 30
	internal interface IConfigCache
	{
		// Token: 0x06000102 RID: 258
		void Refresh(IConfigurationSession session);
	}
}
