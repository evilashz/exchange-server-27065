using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000CC RID: 204
	public interface IConfigurationContext
	{
		// Token: 0x0600080E RID: 2062
		bool IsFeatureEnabled(Feature feature);

		// Token: 0x0600080F RID: 2063
		Feature GetEnabledFeatures();
	}
}
