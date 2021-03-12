using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000060 RID: 96
	internal class ThrottleHelper : IThrottleHelper
	{
		// Token: 0x06000316 RID: 790 RVA: 0x000142CE File Offset: 0x000124CE
		public string[] GetServersInGroup(string groupName)
		{
			return ExchangeThrottleSettings.ResolveKnownExchangeGroupToServers(groupName);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000142D8 File Offset: 0x000124D8
		public int GetServerVersion(string serverName)
		{
			Server exchangeServerByName = DirectoryAccessor.Instance.GetExchangeServerByName(serverName);
			if (exchangeServerByName != null)
			{
				return exchangeServerByName.AdminDisplayVersion.ToInt();
			}
			throw new ADOperationException(Strings.ServerVersionNotFound(serverName));
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0001430B File Offset: 0x0001250B
		public ThrottleSettingsBase Settings
		{
			get
			{
				return ExchangeThrottleSettings.Instance;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00014314 File Offset: 0x00012514
		public GlobalTunables Tunables
		{
			get
			{
				if (ThrottleHelper.globalTunables != null)
				{
					return ThrottleHelper.globalTunables;
				}
				GlobalTunables globalTunables = new GlobalTunables
				{
					LocalMachineName = Environment.MachineName,
					ThrottleGroupCacheRefreshFrequency = TimeSpan.FromMinutes(5.0),
					ThrottleGroupCacheRefreshStartDelay = TimeSpan.FromMinutes(1.0),
					ThrottlingV2SupportedServerVersion = new ServerVersion(15, 0, 785, 0).ToInt(),
					IsRunningMock = false
				};
				ThrottleHelper.globalTunables = globalTunables;
				return ThrottleHelper.globalTunables;
			}
		}

		// Token: 0x04000264 RID: 612
		private static GlobalTunables globalTunables;
	}
}
