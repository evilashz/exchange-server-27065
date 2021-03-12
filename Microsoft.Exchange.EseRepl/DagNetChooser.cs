using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000004 RID: 4
	public class DagNetChooser
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021F0 File Offset: 0x000003F0
		private static ITracer Tracer
		{
			get
			{
				return Dependencies.DagNetChooserTracer;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public static DagNetRoute[] ProposeRoutes(string targetServer, out DagNetConfig dagNetConfig)
		{
			DagNetChooser netChooser = DagNetEnvironment.NetChooser;
			return netChooser.BuildRoutes(targetServer, out dagNetConfig);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002213 File Offset: 0x00000413
		public DagNetRoute[] BuildRoutes(string targetServer, out DagNetConfig dagNetConfig)
		{
			return this.BuildRoutes(targetServer, false, null, out dagNetConfig);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002220 File Offset: 0x00000420
		public DagNetRoute[] BuildRoutes(string targetServer, bool dnsOnly, string chooseNetworkByName, out DagNetConfig dagNetConfig)
		{
			lock (this)
			{
				this.LoadMapIfNeeded();
				DagNetChooser.OutboundNetInfo[] array = this.FetchNetworkOrdering();
				if (array.Length > 0)
				{
					List<DagNetRoute> list = new List<DagNetRoute>(array.Length);
					foreach (DagNetChooser.OutboundNetInfo outboundNetInfo in array)
					{
						DagNetChooser.OutboundNetInfo.TargetNicInfo targetNicInfo;
						if (outboundNetInfo.Targets.TryGetValue(targetServer, out targetNicInfo) && (!dnsOnly || outboundNetInfo.Network.IsDnsMapped) && (chooseNetworkByName == null || StringUtil.IsEqualIgnoreCase(chooseNetworkByName, outboundNetInfo.Network.Name)))
						{
							list.Add(new DagNetRoute
							{
								NetworkName = outboundNetInfo.Network.Name,
								SourceIPAddr = outboundNetInfo.SourceIPAddr,
								TargetPort = targetNicInfo.TargetPort,
								TargetIPAddr = targetNicInfo.IPAddr,
								IsCrossSubnet = targetNicInfo.IsCrossSubnet
							});
							if (dnsOnly || chooseNetworkByName != null)
							{
								break;
							}
						}
					}
					dagNetConfig = this.netConfig;
					return list.ToArray();
				}
			}
			dagNetConfig = this.netConfig;
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002350 File Offset: 0x00000550
		private void LoadMapIfNeeded()
		{
			DagNetConfig dagNetConfig = DagNetEnvironment.FetchNetConfig();
			if (!object.ReferenceEquals(dagNetConfig, this.netConfig))
			{
				this.LoadMap(dagNetConfig);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002378 File Offset: 0x00000578
		private void LoadMap(DagNetConfig cfg)
		{
			this.netConfig = cfg;
			Dictionary<string, DagNetChooser.OutboundNetInfo> dictionary = new Dictionary<string, DagNetChooser.OutboundNetInfo>(cfg.Networks.Count);
			foreach (DagNetwork dagNetwork in cfg.Networks)
			{
				if (dagNetwork.ReplicationEnabled)
				{
					DagNetChooser.OutboundNetInfo outboundNetInfo = new DagNetChooser.OutboundNetInfo();
					outboundNetInfo.Network = dagNetwork;
					dictionary.Add(dagNetwork.Name, outboundNetInfo);
				}
			}
			foreach (DagNode dagNode in cfg.Nodes)
			{
				foreach (DagNode.Nic nic in dagNode.Nics)
				{
					IPAddress ipaddress = NetworkUtil.ConvertStringToIpAddress(nic.IpAddress);
					DagNetChooser.OutboundNetInfo outboundNetInfo2;
					if (ipaddress != null && dictionary.TryGetValue(nic.NetworkName, out outboundNetInfo2))
					{
						if (MachineName.IsEffectivelyLocalComputerName(dagNode.Name))
						{
							outboundNetInfo2.SourceIPAddr = ipaddress;
						}
						else
						{
							if (outboundNetInfo2.Targets == null)
							{
								outboundNetInfo2.Targets = new Dictionary<string, DagNetChooser.OutboundNetInfo.TargetNicInfo>(cfg.Nodes.Count, MachineName.Comparer);
							}
							DagNetChooser.OutboundNetInfo.TargetNicInfo targetNicInfo;
							if (outboundNetInfo2.Targets.TryGetValue(dagNode.Name, out targetNicInfo))
							{
								DagNetChooser.Tracer.TraceError((long)this.GetHashCode(), "LoadMap found dup ipAddr for node {0} on net {1} ip1 {2} ip2 {3}", new object[]
								{
									dagNode.Name,
									nic.NetworkName,
									targetNicInfo.IPAddr,
									nic.IpAddress
								});
							}
							else
							{
								targetNicInfo = new DagNetChooser.OutboundNetInfo.TargetNicInfo();
								targetNicInfo.IPAddr = ipaddress;
								targetNicInfo.IsCrossSubnet = true;
								targetNicInfo.TargetPort = this.netConfig.ReplicationPort;
								if (dagNode.ReplicationPort != 0)
								{
									targetNicInfo.TargetPort = dagNode.ReplicationPort;
								}
								outboundNetInfo2.Targets.Add(dagNode.Name, targetNicInfo);
							}
						}
					}
				}
			}
			List<DagNetChooser.OutboundNetInfo> list = new List<DagNetChooser.OutboundNetInfo>(cfg.Networks.Count);
			int num = 0;
			foreach (DagNetChooser.OutboundNetInfo outboundNetInfo3 in dictionary.Values)
			{
				if (outboundNetInfo3.SourceIPAddr != null && outboundNetInfo3.Targets != null)
				{
					if (outboundNetInfo3.Network.IsDnsMapped)
					{
						list.Add(outboundNetInfo3);
					}
					else
					{
						list.Insert(0, outboundNetInfo3);
						num++;
					}
				}
			}
			this.roundRobinIndex = 0;
			this.outboundNets = list.ToArray();
			if (num > 0)
			{
				this.roundRobinLimit = num;
				return;
			}
			this.roundRobinLimit = this.outboundNets.Length;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002690 File Offset: 0x00000890
		private DagNetChooser.OutboundNetInfo[] FetchNetworkOrdering()
		{
			DagNetChooser.OutboundNetInfo[] array = new DagNetChooser.OutboundNetInfo[this.outboundNets.Length];
			int num = this.roundRobinIndex;
			this.roundRobinIndex = DagNetEnvironment.CircularIncrement(this.roundRobinIndex, this.roundRobinLimit);
			int num2 = 0;
			for (int i = num; i < this.roundRobinLimit; i++)
			{
				array[num2++] = this.outboundNets[i];
			}
			for (int i = 0; i < num; i++)
			{
				array[num2++] = this.outboundNets[i];
			}
			for (int i = this.roundRobinLimit; i < this.outboundNets.Length; i++)
			{
				array[num2++] = this.outboundNets[i];
			}
			return array;
		}

		// Token: 0x0400000D RID: 13
		private DagNetConfig netConfig;

		// Token: 0x0400000E RID: 14
		private DagNetChooser.OutboundNetInfo[] outboundNets;

		// Token: 0x0400000F RID: 15
		private int roundRobinLimit;

		// Token: 0x04000010 RID: 16
		private int roundRobinIndex;

		// Token: 0x02000005 RID: 5
		private class OutboundNetInfo
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000016 RID: 22 RVA: 0x00002736 File Offset: 0x00000936
			// (set) Token: 0x06000017 RID: 23 RVA: 0x0000273E File Offset: 0x0000093E
			public DagNetwork Network { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000018 RID: 24 RVA: 0x00002747 File Offset: 0x00000947
			// (set) Token: 0x06000019 RID: 25 RVA: 0x0000274F File Offset: 0x0000094F
			public IPAddress SourceIPAddr { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600001A RID: 26 RVA: 0x00002758 File Offset: 0x00000958
			// (set) Token: 0x0600001B RID: 27 RVA: 0x00002760 File Offset: 0x00000960
			public Dictionary<string, DagNetChooser.OutboundNetInfo.TargetNicInfo> Targets { get; set; }

			// Token: 0x02000006 RID: 6
			public class TargetNicInfo
			{
				// Token: 0x0600001D RID: 29 RVA: 0x00002771 File Offset: 0x00000971
				public TargetNicInfo()
				{
					this.IsCrossSubnet = true;
				}

				// Token: 0x1700000A RID: 10
				// (get) Token: 0x0600001E RID: 30 RVA: 0x00002780 File Offset: 0x00000980
				// (set) Token: 0x0600001F RID: 31 RVA: 0x00002788 File Offset: 0x00000988
				public IPAddress IPAddr { get; set; }

				// Token: 0x1700000B RID: 11
				// (get) Token: 0x06000020 RID: 32 RVA: 0x00002791 File Offset: 0x00000991
				// (set) Token: 0x06000021 RID: 33 RVA: 0x00002799 File Offset: 0x00000999
				public bool IsCrossSubnet { get; set; }

				// Token: 0x1700000C RID: 12
				// (get) Token: 0x06000022 RID: 34 RVA: 0x000027A2 File Offset: 0x000009A2
				// (set) Token: 0x06000023 RID: 35 RVA: 0x000027AA File Offset: 0x000009AA
				public int TargetPort { get; set; }
			}
		}
	}
}
