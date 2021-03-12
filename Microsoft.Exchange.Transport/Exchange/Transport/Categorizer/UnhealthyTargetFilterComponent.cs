using System;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000277 RID: 631
	internal class UnhealthyTargetFilterComponent : ITransportComponent
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0006FD53 File Offset: 0x0006DF53
		public UnhealthyTargetFilter<IPAddressPortPair> UnhealthyTargetIPAddressFilter
		{
			get
			{
				return this.unhealthyTargetIpAddressFilter;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0006FD5B File Offset: 0x0006DF5B
		public UnhealthyTargetFilter<FqdnPortPair> UnhealthyTargetFqdnFilter
		{
			get
			{
				return this.unhealthyTargetFqdnFilter;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0006FD63 File Offset: 0x0006DF63
		public UnhealthyTargetFilterConfiguration UnhealthyTargetFilterConfiguration
		{
			get
			{
				return this.unhealthyTargetFilterConfiguration;
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0006FD6C File Offset: 0x0006DF6C
		public void Load()
		{
			this.unhealthyTargetFilterConfiguration = new UnhealthyTargetFilterConfiguration();
			this.unhealthyTargetIpAddressFilter = new UnhealthyTargetFilter<IPAddressPortPair>(this.unhealthyTargetFilterConfiguration, null, null);
			this.unhealthyTargetFqdnFilter = new UnhealthyTargetFilter<FqdnPortPair>(this.unhealthyTargetFilterConfiguration, null, null);
			Components.Configuration.LocalServerChanged += this.UpdateConfiguration;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0006FDC0 File Offset: 0x0006DFC0
		public void Unload()
		{
			Components.Configuration.LocalServerChanged -= this.UpdateConfiguration;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0006FDD8 File Offset: 0x0006DFD8
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006FDDC File Offset: 0x0006DFDC
		public void CleanupExpiredEntries()
		{
			UnhealthyTargetFilter<FqdnPortPair> unhealthyTargetFilter = this.unhealthyTargetFqdnFilter;
			if (unhealthyTargetFilter != null)
			{
				unhealthyTargetFilter.CleanupExpiredEntries();
			}
			UnhealthyTargetFilter<IPAddressPortPair> unhealthyTargetFilter2 = this.unhealthyTargetIpAddressFilter;
			if (unhealthyTargetFilter2 != null)
			{
				unhealthyTargetFilter2.CleanupExpiredEntries();
			}
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0006FE0C File Offset: 0x0006E00C
		private void UpdateConfiguration(TransportServerConfiguration transportServerConfiguration)
		{
			if (this.unhealthyTargetFilterConfiguration != null)
			{
				this.unhealthyTargetFilterConfiguration.ConfigChanged(Components.Configuration.AppConfig.RemoteDelivery.LoadBalancingForServerFailoverEnabled, Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryCount, Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryInterval, transportServerConfiguration.TransportServer.TransientFailureRetryCount, transportServerConfiguration.TransportServer.TransientFailureRetryInterval, transportServerConfiguration.TransportServer.OutboundConnectionFailureRetryInterval);
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0006FE93 File Offset: 0x0006E093
		public TimeSpan GetServerLatency(IPAddressPortPair target)
		{
			return this.unhealthyTargetIpAddressFilter.GetServerLatency(target);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0006FEA1 File Offset: 0x0006E0A1
		public void UpdateServerLatency(SmtpOutTargetHostPicker.SmtpTarget smtpTarget, TimeSpan latency)
		{
			this.unhealthyTargetFqdnFilter.UpdateServerLatency(new FqdnPortPair(smtpTarget.TargetHostName, smtpTarget.Port), latency);
			this.UnhealthyTargetIPAddressFilter.UpdateServerLatency(new IPAddressPortPair(smtpTarget.Address, smtpTarget.Port), latency);
		}

		// Token: 0x04000CDD RID: 3293
		private UnhealthyTargetFilter<IPAddressPortPair> unhealthyTargetIpAddressFilter;

		// Token: 0x04000CDE RID: 3294
		private UnhealthyTargetFilter<FqdnPortPair> unhealthyTargetFqdnFilter;

		// Token: 0x04000CDF RID: 3295
		private UnhealthyTargetFilterConfiguration unhealthyTargetFilterConfiguration;
	}
}
