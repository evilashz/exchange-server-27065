using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000265 RID: 613
	internal static class DiscoveryUtils
	{
		// Token: 0x06001464 RID: 5220 RVA: 0x0003C474 File Offset: 0x0003A674
		public static bool IsFrontendTransportRoleInstalled()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			return (instance.ExchangeServerRoleEndpoint == null && FfoLocalEndpointManager.IsFrontendTransportRoleInstalled) || (instance.ExchangeServerRoleEndpoint != null && instance.ExchangeServerRoleEndpoint.IsFrontendTransportRoleInstalled);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003C4B0 File Offset: 0x0003A6B0
		public static bool IsHubTransportRoleInstalled()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			return (instance.ExchangeServerRoleEndpoint == null && FfoLocalEndpointManager.IsHubTransportRoleInstalled) || (instance.ExchangeServerRoleEndpoint != null && instance.ExchangeServerRoleEndpoint.IsBridgeheadRoleInstalled);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		public static bool IsGatewayRoleInstalled()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			return instance.ExchangeServerRoleEndpoint != null && instance.ExchangeServerRoleEndpoint.IsGatewayRoleInstalled;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0003C514 File Offset: 0x0003A714
		public static bool IsMailboxRoleInstalled()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			return instance.ExchangeServerRoleEndpoint != null && instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0003C53C File Offset: 0x0003A73C
		public static bool IsForefrontForOfficeDatacenter()
		{
			return DatacenterRegistry.IsForefrontForOffice();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0003C544 File Offset: 0x0003A744
		public static bool IsMultiTenancyEnabled()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled || DiscoveryUtils.IsForefrontForOfficeDatacenter() || DatacenterRegistry.IsPartnerHostedOnly();
		}
	}
}
