using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000370 RID: 880
	internal class RoutingBasedHubPicker : ShadowHubPickerBase
	{
		// Token: 0x0600262E RID: 9774 RVA: 0x00094601 File Offset: 0x00092801
		public RoutingBasedHubPicker(IShadowRedundancyConfigurationSource configurationSource, IMailRouter mailRouter) : base(configurationSource)
		{
			if (mailRouter == null)
			{
				throw new ArgumentNullException("mailRouter");
			}
			this.mailRouter = mailRouter;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0009461F File Offset: 0x0009281F
		public override bool TrySelectShadowServers(out IEnumerable<INextHopServer> shadowServers)
		{
			return this.mailRouter.TrySelectHubServersForShadow(new ShadowRoutingConfiguration(this.configurationSource.ShadowMessagePreference, this.configurationSource.MaxRemoteShadowAttempts, this.configurationSource.MaxLocalShadowAttempts), out shadowServers);
		}

		// Token: 0x04001384 RID: 4996
		private IMailRouter mailRouter;
	}
}
