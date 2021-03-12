using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000258 RID: 600
	internal class NetworkNodeEndPoints
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000619DC File Offset: 0x0005FBDC
		internal List<NetworkEndPoint> EndPoints
		{
			get
			{
				return this.m_endPoints;
			}
		}

		// Token: 0x0400093A RID: 2362
		private List<NetworkEndPoint> m_endPoints = new List<NetworkEndPoint>();
	}
}
