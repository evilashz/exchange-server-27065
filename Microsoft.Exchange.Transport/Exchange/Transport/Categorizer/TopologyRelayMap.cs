﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000208 RID: 520
	internal abstract class TopologyRelayMap<TPath> where TPath : TopologyPath
	{
		// Token: 0x0600171D RID: 5917 RVA: 0x0005DA1A File Offset: 0x0005BC1A
		protected TopologyRelayMap(int siteCount, DateTime timestamp)
		{
			this.timestamp = timestamp;
			if (siteCount <= 0)
			{
				throw new InvalidOperationException("siteCount expected to be > 0, but is " + siteCount);
			}
			this.routes = new Dictionary<ITopologySite, TPath>(siteCount - 1);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005DA54 File Offset: 0x0005BC54
		protected void CalculateRoutes(ITopologySite localSite, RoutingContextCore contextCore)
		{
			this.DebugTrace("[LCP] Calculating least cost paths to remote sites");
			TPath tpath = default(TPath);
			ITopologySite topologySite = localSite;
			while (topologySite != null)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string, object>((long)this.GetHashCode(), "[{0}] [LCP] Relaxing links from site {1}; path: {2}", this.timestamp, topologySite.Name, tpath ?? "<Local>");
				foreach (ITopologySiteLink topologySiteLink in topologySite.TopologySiteLinks)
				{
					foreach (ITopologySite topologySite2 in topologySiteLink.TopologySites)
					{
						if (topologySite2 != topologySite && topologySite2 != localSite)
						{
							RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "[{0}] [LCP] Relaxing link {1},cost={2} to site {3}", new object[]
							{
								this.timestamp,
								topologySiteLink.Name,
								topologySiteLink.Cost,
								topologySite2.Name
							});
							TPath arg = default(TPath);
							if (this.routes.TryGetValue(topologySite2, out arg))
							{
								RoutingDiag.Tracer.TraceDebug<DateTime, TPath>((long)this.GetHashCode(), "[{0}] [LCP] Detected existing path: {1}", this.timestamp, arg);
								arg.ReplaceIfBetter(tpath, topologySiteLink, this.timestamp);
							}
							else
							{
								TPath tpath2 = this.CreateTopologyPath(tpath, topologySite2, topologySiteLink, contextCore);
								this.routes.Add(topologySite2, tpath2);
								RoutingDiag.Tracer.TraceDebug<DateTime, TPath>((long)this.GetHashCode(), "[{0}] [LCP] Added new topology path: {1}", this.timestamp, tpath2);
							}
						}
					}
				}
				if (tpath != null)
				{
					tpath.Optimal = true;
				}
				topologySite = null;
				tpath = default(TPath);
				int num = int.MaxValue;
				foreach (KeyValuePair<ITopologySite, TPath> keyValuePair in this.routes)
				{
					TPath value = keyValuePair.Value;
					if (!value.Optimal)
					{
						TPath value2 = keyValuePair.Value;
						if (value2.TotalCost < num)
						{
							TPath value3 = keyValuePair.Value;
							num = value3.TotalCost;
							topologySite = keyValuePair.Key;
							tpath = keyValuePair.Value;
						}
					}
				}
			}
		}

		// Token: 0x0600171F RID: 5919
		protected abstract TPath CreateTopologyPath(TPath prePath, ITopologySite targetSite, ITopologySiteLink link, RoutingContextCore contextCore);

		// Token: 0x06001720 RID: 5920 RVA: 0x0005DD08 File Offset: 0x0005BF08
		protected void DebugTrace(string s)
		{
			RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] {1}", this.timestamp, s);
		}

		// Token: 0x04000B6C RID: 2924
		protected DateTime timestamp;

		// Token: 0x04000B6D RID: 2925
		protected Dictionary<ITopologySite, TPath> routes;
	}
}
