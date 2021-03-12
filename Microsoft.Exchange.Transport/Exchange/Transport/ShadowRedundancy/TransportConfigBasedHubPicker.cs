using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200039F RID: 927
	internal class TransportConfigBasedHubPicker : ShadowHubPickerBase
	{
		// Token: 0x0600296F RID: 10607 RVA: 0x000A3FC2 File Offset: 0x000A21C2
		public TransportConfigBasedHubPicker(IShadowRedundancyConfigurationSource configurationSource) : base(configurationSource)
		{
			this.enabled = (Components.TransportAppConfig.ShadowRedundancy.ShadowHubList.Count > 0);
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000A3FE8 File Offset: 0x000A21E8
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000A3FF0 File Offset: 0x000A21F0
		public override bool TrySelectShadowServers(out IEnumerable<INextHopServer> shadowServers)
		{
			if (!this.enabled)
			{
				throw new InvalidOperationException("Configuration switch is not enabled");
			}
			shadowServers = Components.TransportAppConfig.ShadowRedundancy.ShadowHubList;
			return true;
		}

		// Token: 0x04001542 RID: 5442
		private readonly bool enabled;
	}
}
