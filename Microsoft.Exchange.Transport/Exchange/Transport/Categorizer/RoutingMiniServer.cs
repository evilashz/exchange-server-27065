using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000264 RID: 612
	internal class RoutingMiniServer
	{
		// Token: 0x06001A85 RID: 6789 RVA: 0x0006D44C File Offset: 0x0006B64C
		public RoutingMiniServer(Server server)
		{
			ArgumentValidator.ThrowIfNull("server", server);
			this.Id = server.Id;
			this.ServerSite = server.ServerSite;
			this.DatabaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
			this.ExchangeLegacyDN = server.ExchangeLegacyDN;
			this.Fqdn = server.Fqdn;
			this.HomeRoutingGroup = server.HomeRoutingGroup;
			this.IsExchange2007OrLater = server.IsExchange2007OrLater;
			this.IsEdgeServer = server.IsEdgeServer;
			this.IsFrontendTransportServer = server.IsFrontendTransportServer;
			this.IsHubTransportServer = server.IsHubTransportServer;
			this.IsMailboxServer = server.IsMailboxServer;
			this.MajorVersion = server.MajorVersion;
			this.CurrentServerRole = server.CurrentServerRole;
			this.IsE15OrLater = server.IsE15OrLater;
			this.ComponentStates = new MultiValuedProperty<string>(server.ComponentStates);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0006D524 File Offset: 0x0006B724
		public RoutingMiniServer(TopologyServer server)
		{
			ArgumentValidator.ThrowIfNull("server", server);
			this.Id = server.Id;
			this.ServerSite = server.ServerSite;
			this.DatabaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
			this.ExchangeLegacyDN = server.ExchangeLegacyDN;
			this.Fqdn = server.Fqdn;
			this.HomeRoutingGroup = server.HomeRoutingGroup;
			this.IsExchange2007OrLater = server.IsExchange2007OrLater;
			this.IsEdgeServer = server.IsEdgeServer;
			this.IsFrontendTransportServer = server.IsFrontendTransportServer;
			this.IsHubTransportServer = server.IsHubTransportServer;
			this.IsMailboxServer = server.IsMailboxServer;
			this.MajorVersion = server.MajorVersion;
			this.CurrentServerRole = server.CurrentServerRole;
			this.IsE15OrLater = server.IsE15OrLater;
			this.ComponentStates = new MultiValuedProperty<string>(server.ComponentStates);
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0006D5FB File Offset: 0x0006B7FB
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0006D603 File Offset: 0x0006B803
		public ADObjectId Id { get; private set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0006D60C File Offset: 0x0006B80C
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0006D614 File Offset: 0x0006B814
		public ADObjectId ServerSite { get; private set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0006D61D File Offset: 0x0006B81D
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x0006D625 File Offset: 0x0006B825
		public ADObjectId DatabaseAvailabilityGroup { get; private set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0006D62E File Offset: 0x0006B82E
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0006D636 File Offset: 0x0006B836
		public string ExchangeLegacyDN { get; private set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0006D63F File Offset: 0x0006B83F
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x0006D647 File Offset: 0x0006B847
		public string Fqdn { get; private set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0006D650 File Offset: 0x0006B850
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x0006D658 File Offset: 0x0006B858
		public ADObjectId HomeRoutingGroup { get; private set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0006D661 File Offset: 0x0006B861
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x0006D669 File Offset: 0x0006B869
		public bool IsExchange2007OrLater { get; private set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0006D672 File Offset: 0x0006B872
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x0006D67A File Offset: 0x0006B87A
		public bool IsEdgeServer { get; private set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0006D683 File Offset: 0x0006B883
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x0006D68B File Offset: 0x0006B88B
		public bool IsFrontendTransportServer { get; private set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0006D694 File Offset: 0x0006B894
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0006D69C File Offset: 0x0006B89C
		public bool IsHubTransportServer { get; private set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0006D6A5 File Offset: 0x0006B8A5
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x0006D6AD File Offset: 0x0006B8AD
		public bool IsMailboxServer { get; private set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0006D6B6 File Offset: 0x0006B8B6
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x0006D6BE File Offset: 0x0006B8BE
		public int MajorVersion { get; private set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0006D6C7 File Offset: 0x0006B8C7
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0006D6CF File Offset: 0x0006B8CF
		public ServerRole CurrentServerRole { get; private set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0006D6D8 File Offset: 0x0006B8D8
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x0006D6E0 File Offset: 0x0006B8E0
		public bool IsE15OrLater { get; private set; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0006D6E9 File Offset: 0x0006B8E9
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x0006D6F1 File Offset: 0x0006B8F1
		public MultiValuedProperty<string> ComponentStates { get; private set; }
	}
}
