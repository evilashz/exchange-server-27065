using System;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000265 RID: 613
	internal class RoutingServerInfo : INextHopServer
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x0006D6FA File Offset: 0x0006B8FA
		public RoutingServerInfo(RoutingMiniServer server)
		{
			ArgumentValidator.ThrowIfNull("server", server);
			this.server = server;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0006D714 File Offset: 0x0006B914
		public ADObjectId Id
		{
			get
			{
				return this.server.Id;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0006D721 File Offset: 0x0006B921
		public ADObjectId ADSite
		{
			get
			{
				return this.server.ServerSite;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0006D72E File Offset: 0x0006B92E
		public bool DagRoutingEnabled
		{
			get
			{
				return this.server.IsE15OrLater && this.server.DatabaseAvailabilityGroup != null;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0006D750 File Offset: 0x0006B950
		public ADObjectId DatabaseAvailabilityGroup
		{
			get
			{
				return this.server.DatabaseAvailabilityGroup;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0006D75D File Offset: 0x0006B95D
		public string ExchangeLegacyDN
		{
			get
			{
				return this.server.ExchangeLegacyDN;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0006D76A File Offset: 0x0006B96A
		public string Fqdn
		{
			get
			{
				return this.server.Fqdn;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0006D777 File Offset: 0x0006B977
		public ADObjectId HomeRoutingGroup
		{
			get
			{
				return this.server.HomeRoutingGroup;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0006D784 File Offset: 0x0006B984
		public bool IsExchange2007OrLater
		{
			get
			{
				return this.server.IsExchange2007OrLater;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0006D791 File Offset: 0x0006B991
		public bool IsEdgeTransportServer
		{
			get
			{
				return this.server.IsEdgeServer;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0006D79E File Offset: 0x0006B99E
		public bool IsFrontendTransportServer
		{
			get
			{
				return this.server.IsFrontendTransportServer;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0006D7AB File Offset: 0x0006B9AB
		public bool IsFrontendTransportActive
		{
			get
			{
				if (this.frontendComponentState == RoutingServerInfo.ComponentState.Unknown)
				{
					this.frontendComponentState = (RoutingServerInfo.IsFrontendComponentActive(this.server.IsFrontendTransportServer, this.server.ComponentStates) ? RoutingServerInfo.ComponentState.Active : RoutingServerInfo.ComponentState.Inactive);
				}
				return this.frontendComponentState == RoutingServerInfo.ComponentState.Active;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0006D7E5 File Offset: 0x0006B9E5
		public bool IsHubTransportServer
		{
			get
			{
				return this.server.IsHubTransportServer;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0006D7F4 File Offset: 0x0006B9F4
		public bool IsHubTransportActive
		{
			get
			{
				if (this.hubComponentState == RoutingServerInfo.ComponentState.Unknown)
				{
					this.hubComponentState = (RoutingServerInfo.IsHubComponentActive(this.server.IsHubTransportServer, this.server.IsE15OrLater, this.server.ComponentStates) ? RoutingServerInfo.ComponentState.Active : RoutingServerInfo.ComponentState.Inactive);
				}
				return this.hubComponentState == RoutingServerInfo.ComponentState.Active;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0006D844 File Offset: 0x0006BA44
		public bool IsMailboxServer
		{
			get
			{
				return this.server.IsMailboxServer;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0006D851 File Offset: 0x0006BA51
		public int MajorVersion
		{
			get
			{
				return this.server.MajorVersion;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0006D85E File Offset: 0x0006BA5E
		bool INextHopServer.IsIPAddress
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0006D861 File Offset: 0x0006BA61
		IPAddress INextHopServer.Address
		{
			get
			{
				throw new InvalidOperationException("INextHopServer.IPAddress must not be requested from RoutingServerInfo objects");
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0006D86D File Offset: 0x0006BA6D
		public bool IsFrontendAndHubColocatedServer
		{
			get
			{
				return this.IsHubTransportServer && this.IsFrontendTransportServer;
			}
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0006D87F File Offset: 0x0006BA7F
		public static bool IsFrontendComponentActive(Server server)
		{
			return RoutingServerInfo.IsFrontendComponentActive(server.IsFrontendTransportServer, server.ComponentStates);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0006D892 File Offset: 0x0006BA92
		public static bool IsFrontendComponentActive(TopologyServer topologyServer)
		{
			return RoutingServerInfo.IsFrontendComponentActive(topologyServer.IsFrontendTransportServer, topologyServer.ComponentStates);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0006D8A5 File Offset: 0x0006BAA5
		public static bool IsHubComponentActive(Server server)
		{
			return RoutingServerInfo.IsHubComponentActive(server.IsHubTransportServer, server.IsE15OrLater, server.ComponentStates);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0006D8BE File Offset: 0x0006BABE
		public static bool IsHubComponentActive(TopologyServer topologyServer)
		{
			return RoutingServerInfo.IsHubComponentActive(topologyServer.IsHubTransportServer, topologyServer.IsE15OrLater, topologyServer.ComponentStates);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0006D8D7 File Offset: 0x0006BAD7
		public bool IsSameServerAs(Server topologyServer)
		{
			return string.Equals(this.server.Fqdn, topologyServer.Fqdn, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0006D8F0 File Offset: 0x0006BAF0
		public bool IsSameServerAs(TopologyServer topologyServer)
		{
			return string.Equals(this.server.Fqdn, topologyServer.Fqdn, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0006D909 File Offset: 0x0006BB09
		public bool IsInSameSite(Server topologyServer)
		{
			return this.ADSite.Equals(topologyServer.ServerSite);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0006D91C File Offset: 0x0006BB1C
		public bool IsInSameSite(TopologyServer topologyServer)
		{
			return this.ADSite.Equals(topologyServer.ServerSite);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0006D92F File Offset: 0x0006BB2F
		public bool IsSameVersionAs(Server topologyServer)
		{
			return this.MajorVersion == topologyServer.MajorVersion;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006D93F File Offset: 0x0006BB3F
		public bool IsSameVersionAs(TopologyServer topologyServer)
		{
			return this.MajorVersion == topologyServer.MajorVersion;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0006D94F File Offset: 0x0006BB4F
		public bool IsSameSiteAndVersionAs(Server topologyServer)
		{
			return this.IsSameVersionAs(topologyServer) && this.IsInSameSite(topologyServer);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0006D963 File Offset: 0x0006BB63
		public bool IsSameSiteAndVersionAs(TopologyServer topologyServer)
		{
			return this.IsSameVersionAs(topologyServer) && this.IsInSameSite(topologyServer);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0006D978 File Offset: 0x0006BB78
		public bool Match(RoutingServerInfo other)
		{
			if (!ADObjectId.Equals(this.Id, other.Id) || !RoutingUtils.MatchStrings(this.Fqdn, other.Fqdn) || this.MajorVersion != other.MajorVersion || this.server.CurrentServerRole != other.server.CurrentServerRole || !RoutingUtils.MatchStrings(this.ExchangeLegacyDN, other.ExchangeLegacyDN))
			{
				return false;
			}
			if (this.IsExchange2007OrLater)
			{
				if (!ADObjectId.Equals(this.ADSite, other.ADSite))
				{
					return false;
				}
				if (this.server.IsE15OrLater && !ADObjectId.Equals(this.DatabaseAvailabilityGroup, other.DatabaseAvailabilityGroup))
				{
					return false;
				}
			}
			else if (!ADObjectId.Equals(this.HomeRoutingGroup, other.HomeRoutingGroup))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0006DA3B File Offset: 0x0006BC3B
		private static bool IsFrontendComponentActive(bool isFrontendTransportServer, MultiValuedProperty<string> componentStates)
		{
			if (!isFrontendTransportServer)
			{
				throw new InvalidOperationException("Provided server is not a FrontendTransport server");
			}
			return RoutingServerInfo.IsServerComponentActive(componentStates, RoutingServerInfo.FrontendComponent);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0006DA56 File Offset: 0x0006BC56
		private static bool IsHubComponentActive(bool isHubTransportServer, bool isE15OrLater, MultiValuedProperty<string> componentStates)
		{
			if (!isHubTransportServer)
			{
				throw new InvalidOperationException("Provided server is not a HubTransport server");
			}
			return !isE15OrLater || RoutingServerInfo.IsServerComponentActive(componentStates, RoutingServerInfo.HubComponent);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0006DA76 File Offset: 0x0006BC76
		private static bool IsServerComponentActive(MultiValuedProperty<string> componentStates, string component)
		{
			return ServiceState.Active == ServerComponentStates.ReadEffectiveComponentState(null, componentStates, ServerComponentStateSources.AD, component, ServiceStateHelper.GetDefaultServiceState());
		}

		// Token: 0x04000CB1 RID: 3249
		private static readonly string FrontendComponent = ServerComponentStates.GetComponentId(ServerComponentEnum.FrontendTransport);

		// Token: 0x04000CB2 RID: 3250
		private static readonly string HubComponent = ServerComponentStates.GetComponentId(ServerComponentEnum.HubTransport);

		// Token: 0x04000CB3 RID: 3251
		private readonly RoutingMiniServer server;

		// Token: 0x04000CB4 RID: 3252
		private RoutingServerInfo.ComponentState frontendComponentState;

		// Token: 0x04000CB5 RID: 3253
		private RoutingServerInfo.ComponentState hubComponentState;

		// Token: 0x02000266 RID: 614
		private enum ComponentState : byte
		{
			// Token: 0x04000CB7 RID: 3255
			Unknown,
			// Token: 0x04000CB8 RID: 3256
			Inactive,
			// Token: 0x04000CB9 RID: 3257
			Active
		}
	}
}
