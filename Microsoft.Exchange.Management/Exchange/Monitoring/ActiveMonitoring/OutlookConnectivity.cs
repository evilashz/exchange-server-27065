using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004FF RID: 1279
	internal static class OutlookConnectivity
	{
		// Token: 0x06002DEA RID: 11754 RVA: 0x000B77E0 File Offset: 0x000B59E0
		internal static ProbeIdentity ResolveIdentity(string identityParameter, bool isDcOrDedicated)
		{
			IEnumerable<ProbeIdentity> enumerable = OutlookConnectivity.AllProbes;
			ProbeIdentity probeIdentity = null;
			if ((!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(identityParameter) || !ExchangeComponent.WellKnownComponents.ContainsKey(MonitoringItemIdentity.MonitorIdentityId.GetHealthSet(identityParameter))) && !MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(identityParameter = "\\" + identityParameter))
			{
				throw new ArgumentException(Strings.InvalidOutlookProbeIdentity(identityParameter));
			}
			string healthSetLookup = MonitoringItemIdentity.MonitorIdentityId.GetHealthSet(identityParameter);
			if (!string.IsNullOrEmpty(healthSetLookup))
			{
				if (ExchangeComponent.WellKnownComponents.ContainsKey(healthSetLookup))
				{
					enumerable = from probe in enumerable
					where probe.Component.Name.Equals(healthSetLookup, StringComparison.InvariantCultureIgnoreCase)
					select probe;
				}
				else
				{
					identityParameter = "\\" + identityParameter;
				}
			}
			string monitor = MonitoringItemIdentity.MonitorIdentityId.GetMonitor(identityParameter);
			string targetResource = MonitoringItemIdentity.MonitorIdentityId.GetTargetResource(identityParameter);
			foreach (ProbeIdentity probeIdentity2 in enumerable)
			{
				if (probeIdentity2.Name.IndexOf(monitor, StringComparison.InvariantCultureIgnoreCase) >= 0)
				{
					if (probeIdentity != null)
					{
						throw new ArgumentException(Strings.AmbiguousOutlookProbeIdentity(identityParameter, probeIdentity.ToString(), probeIdentity2.ForTargetResource(targetResource).ToString()));
					}
					probeIdentity = probeIdentity2.ForTargetResource(targetResource);
				}
			}
			if (probeIdentity != null && isDcOrDedicated && probeIdentity.Name.IndexOf("CTP", StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				probeIdentity = null;
			}
			if (probeIdentity == null)
			{
				throw new ArgumentException(Strings.OutlookProbeIdentityNotFound(identityParameter));
			}
			return probeIdentity;
		}

		// Token: 0x040020DA RID: 8410
		internal const string RpcArchiveScenario = "OutlookLogonToArchiveRpc";

		// Token: 0x040020DB RID: 8411
		internal const string MailboxRpcScenario = "Rpc";

		// Token: 0x040020DC RID: 8412
		internal const string MapiHttpArchiveScenario = "OutlookLogonToArchiveMapiHttp";

		// Token: 0x040020DD RID: 8413
		public const string RpcProxyApplicationPoolName = "MSExchangeRpcProxyAppPool";

		// Token: 0x040020DE RID: 8414
		public const string RpcProxyFrontEndApplicationPoolName = "MSExchangeRpcProxyFrontEndAppPool";

		// Token: 0x040020DF RID: 8415
		public static readonly ProbeIdentity DeepTest = ProbeIdentity.Create(ExchangeComponent.OutlookProtocol, ProbeType.DeepTest, "Rpc", null);

		// Token: 0x040020E0 RID: 8416
		public static readonly ProbeIdentity MapiHttpDeepTest = ProbeIdentity.Create(ExchangeComponent.OutlookMapiHttpProtocol, ProbeType.DeepTest, null, null);

		// Token: 0x040020E1 RID: 8417
		public static readonly ProbeIdentity RpcSelfTest = ProbeIdentity.Create(ExchangeComponent.OutlookProtocol, ProbeType.SelfTest, "Rpc", null);

		// Token: 0x040020E2 RID: 8418
		public static readonly ProbeIdentity MapiHttpSelfTest = ProbeIdentity.Create(ExchangeComponent.OutlookMapiHttpProtocol, ProbeType.SelfTest, null, null);

		// Token: 0x040020E3 RID: 8419
		public static readonly ProbeIdentity ProxyTest = ProbeIdentity.Create(ExchangeComponent.OutlookProxy, ProbeType.ProxyTest, null, "MSExchangeRpcProxyFrontEndAppPool");

		// Token: 0x040020E4 RID: 8420
		public static readonly ProbeIdentity Ctp = ProbeIdentity.Create(ExchangeComponent.Outlook, ProbeType.Ctp, "Rpc", null);

		// Token: 0x040020E5 RID: 8421
		public static readonly ProbeIdentity MapiHttpCtp = ProbeIdentity.Create(ExchangeComponent.OutlookMapiHttp, ProbeType.Ctp, null, null);

		// Token: 0x040020E6 RID: 8422
		public static readonly ProbeIdentity ArchiveCtp = ProbeIdentity.Create(ExchangeComponent.Compliance, ProbeType.Ctp, "OutlookLogonToArchiveRpc", null);

		// Token: 0x040020E7 RID: 8423
		public static readonly ProbeIdentity MapiHttpArchiveCtp = ProbeIdentity.Create(ExchangeComponent.Compliance, ProbeType.Ctp, "OutlookLogonToArchiveMapiHttp", null);

		// Token: 0x040020E8 RID: 8424
		public static readonly ProbeIdentity[] AllProbes = new ProbeIdentity[]
		{
			OutlookConnectivity.DeepTest,
			OutlookConnectivity.RpcSelfTest,
			OutlookConnectivity.ProxyTest,
			OutlookConnectivity.Ctp,
			OutlookConnectivity.ArchiveCtp,
			OutlookConnectivity.MapiHttpDeepTest,
			OutlookConnectivity.MapiHttpSelfTest,
			OutlookConnectivity.MapiHttpCtp,
			OutlookConnectivity.MapiHttpArchiveCtp
		};
	}
}
