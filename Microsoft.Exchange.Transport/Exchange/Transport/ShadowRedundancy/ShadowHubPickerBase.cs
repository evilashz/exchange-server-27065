using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200036F RID: 879
	internal abstract class ShadowHubPickerBase
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x000945F2 File Offset: 0x000927F2
		public ShadowHubPickerBase(IShadowRedundancyConfigurationSource configurationSource)
		{
			this.configurationSource = configurationSource;
		}

		// Token: 0x0600262D RID: 9773
		public abstract bool TrySelectShadowServers(out IEnumerable<INextHopServer> shadowServers);

		// Token: 0x04001383 RID: 4995
		protected IShadowRedundancyConfigurationSource configurationSource;
	}
}
