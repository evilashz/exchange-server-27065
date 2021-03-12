using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006BC RID: 1724
	internal sealed class TopologyServer : MiniTopologyServer
	{
		// Token: 0x06004FAE RID: 20398 RVA: 0x0012651A File Offset: 0x0012471A
		internal TopologyServer(MiniTopologyServer server)
		{
			this.propertyBag = server.propertyBag;
			this.m_Session = server.Session;
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0012653A File Offset: 0x0012473A
		internal TopologyServer(Server server)
		{
			base.SetProperties(server);
			this.m_Session = server.Session;
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x00126555 File Offset: 0x00124755
		// (set) Token: 0x06004FB1 RID: 20401 RVA: 0x0012655D File Offset: 0x0012475D
		public TopologySite TopologySite
		{
			get
			{
				return this.topologySite;
			}
			internal set
			{
				this.topologySite = value;
			}
		}

		// Token: 0x04003668 RID: 13928
		private TopologySite topologySite;
	}
}
