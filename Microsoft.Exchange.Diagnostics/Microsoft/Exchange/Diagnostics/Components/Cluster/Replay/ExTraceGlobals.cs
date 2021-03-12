using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Cluster.Replay
{
	// Token: 0x02000377 RID: 887
	public static class ExTraceGlobals
	{
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000542C1 File Offset: 0x000524C1
		public static Trace ReplayApiTracer
		{
			get
			{
				if (ExTraceGlobals.replayApiTracer == null)
				{
					ExTraceGlobals.replayApiTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.replayApiTracer;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x000542DF File Offset: 0x000524DF
		public static Trace EseutilWrapperTracer
		{
			get
			{
				if (ExTraceGlobals.eseutilWrapperTracer == null)
				{
					ExTraceGlobals.eseutilWrapperTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.eseutilWrapperTracer;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x000542FD File Offset: 0x000524FD
		public static Trace StateTracer
		{
			get
			{
				if (ExTraceGlobals.stateTracer == null)
				{
					ExTraceGlobals.stateTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.stateTracer;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0005431B File Offset: 0x0005251B
		public static Trace LogReplayerTracer
		{
			get
			{
				if (ExTraceGlobals.logReplayerTracer == null)
				{
					ExTraceGlobals.logReplayerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.logReplayerTracer;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00054339 File Offset: 0x00052539
		public static Trace ReplicaInstanceTracer
		{
			get
			{
				if (ExTraceGlobals.replicaInstanceTracer == null)
				{
					ExTraceGlobals.replicaInstanceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.replicaInstanceTracer;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00054357 File Offset: 0x00052557
		public static Trace CmdletsTracer
		{
			get
			{
				if (ExTraceGlobals.cmdletsTracer == null)
				{
					ExTraceGlobals.cmdletsTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.cmdletsTracer;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00054375 File Offset: 0x00052575
		public static Trace ShipLogTracer
		{
			get
			{
				if (ExTraceGlobals.shipLogTracer == null)
				{
					ExTraceGlobals.shipLogTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.shipLogTracer;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00054393 File Offset: 0x00052593
		public static Trace LogCopyTracer
		{
			get
			{
				if (ExTraceGlobals.logCopyTracer == null)
				{
					ExTraceGlobals.logCopyTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.logCopyTracer;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x000543B1 File Offset: 0x000525B1
		public static Trace LogInspectorTracer
		{
			get
			{
				if (ExTraceGlobals.logInspectorTracer == null)
				{
					ExTraceGlobals.logInspectorTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.logInspectorTracer;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000543CF File Offset: 0x000525CF
		public static Trace ReplayManagerTracer
		{
			get
			{
				if (ExTraceGlobals.replayManagerTracer == null)
				{
					ExTraceGlobals.replayManagerTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.replayManagerTracer;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000543EE File Offset: 0x000525EE
		public static Trace CReplicaSeederTracer
		{
			get
			{
				if (ExTraceGlobals.cReplicaSeederTracer == null)
				{
					ExTraceGlobals.cReplicaSeederTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.cReplicaSeederTracer;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0005440D File Offset: 0x0005260D
		public static Trace NetShareTracer
		{
			get
			{
				if (ExTraceGlobals.netShareTracer == null)
				{
					ExTraceGlobals.netShareTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.netShareTracer;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0005442C File Offset: 0x0005262C
		public static Trace ReplicaVssWriterInteropTracer
		{
			get
			{
				if (ExTraceGlobals.replicaVssWriterInteropTracer == null)
				{
					ExTraceGlobals.replicaVssWriterInteropTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.replicaVssWriterInteropTracer;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0005444B File Offset: 0x0005264B
		public static Trace StateLockTracer
		{
			get
			{
				if (ExTraceGlobals.stateLockTracer == null)
				{
					ExTraceGlobals.stateLockTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.stateLockTracer;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0005446A File Offset: 0x0005266A
		public static Trace FileCheckerTracer
		{
			get
			{
				if (ExTraceGlobals.fileCheckerTracer == null)
				{
					ExTraceGlobals.fileCheckerTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.fileCheckerTracer;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00054489 File Offset: 0x00052689
		public static Trace ClusterTracer
		{
			get
			{
				if (ExTraceGlobals.clusterTracer == null)
				{
					ExTraceGlobals.clusterTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.clusterTracer;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x000544A8 File Offset: 0x000526A8
		public static Trace SeederWrapperTracer
		{
			get
			{
				if (ExTraceGlobals.seederWrapperTracer == null)
				{
					ExTraceGlobals.seederWrapperTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.seederWrapperTracer;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000544C7 File Offset: 0x000526C7
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x000544E6 File Offset: 0x000526E6
		public static Trace IncrementalReseederTracer
		{
			get
			{
				if (ExTraceGlobals.incrementalReseederTracer == null)
				{
					ExTraceGlobals.incrementalReseederTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.incrementalReseederTracer;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00054505 File Offset: 0x00052705
		public static Trace DumpsterTracer
		{
			get
			{
				if (ExTraceGlobals.dumpsterTracer == null)
				{
					ExTraceGlobals.dumpsterTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.dumpsterTracer;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00054524 File Offset: 0x00052724
		public static Trace CLogShipContextTracer
		{
			get
			{
				if (ExTraceGlobals.cLogShipContextTracer == null)
				{
					ExTraceGlobals.cLogShipContextTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.cLogShipContextTracer;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00054543 File Offset: 0x00052743
		public static Trace ClusDBWriteTracer
		{
			get
			{
				if (ExTraceGlobals.clusDBWriteTracer == null)
				{
					ExTraceGlobals.clusDBWriteTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.clusDBWriteTracer;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00054562 File Offset: 0x00052762
		public static Trace ReplayConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.replayConfigurationTracer == null)
				{
					ExTraceGlobals.replayConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.replayConfigurationTracer;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00054581 File Offset: 0x00052781
		public static Trace NetPathTracer
		{
			get
			{
				if (ExTraceGlobals.netPathTracer == null)
				{
					ExTraceGlobals.netPathTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.netPathTracer;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000545A0 File Offset: 0x000527A0
		public static Trace HealthChecksTracer
		{
			get
			{
				if (ExTraceGlobals.healthChecksTracer == null)
				{
					ExTraceGlobals.healthChecksTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.healthChecksTracer;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x000545BF File Offset: 0x000527BF
		public static Trace ReplayServiceRpcTracer
		{
			get
			{
				if (ExTraceGlobals.replayServiceRpcTracer == null)
				{
					ExTraceGlobals.replayServiceRpcTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.replayServiceRpcTracer;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000545DE File Offset: 0x000527DE
		public static Trace ActiveManagerTracer
		{
			get
			{
				if (ExTraceGlobals.activeManagerTracer == null)
				{
					ExTraceGlobals.activeManagerTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.activeManagerTracer;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000545FD File Offset: 0x000527FD
		public static Trace SeederServerTracer
		{
			get
			{
				if (ExTraceGlobals.seederServerTracer == null)
				{
					ExTraceGlobals.seederServerTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.seederServerTracer;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0005461C File Offset: 0x0005281C
		public static Trace SeederClientTracer
		{
			get
			{
				if (ExTraceGlobals.seederClientTracer == null)
				{
					ExTraceGlobals.seederClientTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.seederClientTracer;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0005463B File Offset: 0x0005283B
		public static Trace LogTruncaterTracer
		{
			get
			{
				if (ExTraceGlobals.logTruncaterTracer == null)
				{
					ExTraceGlobals.logTruncaterTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.logTruncaterTracer;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0005465A File Offset: 0x0005285A
		public static Trace FailureItemTracer
		{
			get
			{
				if (ExTraceGlobals.failureItemTracer == null)
				{
					ExTraceGlobals.failureItemTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.failureItemTracer;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00054679 File Offset: 0x00052879
		public static Trace LogCopyServerTracer
		{
			get
			{
				if (ExTraceGlobals.logCopyServerTracer == null)
				{
					ExTraceGlobals.logCopyServerTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.logCopyServerTracer;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00054698 File Offset: 0x00052898
		public static Trace LogCopyClientTracer
		{
			get
			{
				if (ExTraceGlobals.logCopyClientTracer == null)
				{
					ExTraceGlobals.logCopyClientTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.logCopyClientTracer;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x000546B7 File Offset: 0x000528B7
		public static Trace TcpChannelTracer
		{
			get
			{
				if (ExTraceGlobals.tcpChannelTracer == null)
				{
					ExTraceGlobals.tcpChannelTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.tcpChannelTracer;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000546D6 File Offset: 0x000528D6
		public static Trace TcpClientTracer
		{
			get
			{
				if (ExTraceGlobals.tcpClientTracer == null)
				{
					ExTraceGlobals.tcpClientTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.tcpClientTracer;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x000546F5 File Offset: 0x000528F5
		public static Trace TcpServerTracer
		{
			get
			{
				if (ExTraceGlobals.tcpServerTracer == null)
				{
					ExTraceGlobals.tcpServerTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.tcpServerTracer;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00054714 File Offset: 0x00052914
		public static Trace RemoteDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.remoteDataProviderTracer == null)
				{
					ExTraceGlobals.remoteDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.remoteDataProviderTracer;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00054733 File Offset: 0x00052933
		public static Trace MonitoredDatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.monitoredDatabaseTracer == null)
				{
					ExTraceGlobals.monitoredDatabaseTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.monitoredDatabaseTracer;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00054752 File Offset: 0x00052952
		public static Trace NetworkManagerTracer
		{
			get
			{
				if (ExTraceGlobals.networkManagerTracer == null)
				{
					ExTraceGlobals.networkManagerTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.networkManagerTracer;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00054771 File Offset: 0x00052971
		public static Trace NetworkChannelTracer
		{
			get
			{
				if (ExTraceGlobals.networkChannelTracer == null)
				{
					ExTraceGlobals.networkChannelTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.networkChannelTracer;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00054790 File Offset: 0x00052990
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000547AF File Offset: 0x000529AF
		public static Trace GranularWriterTracer
		{
			get
			{
				if (ExTraceGlobals.granularWriterTracer == null)
				{
					ExTraceGlobals.granularWriterTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.granularWriterTracer;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000547CE File Offset: 0x000529CE
		public static Trace GranularReaderTracer
		{
			get
			{
				if (ExTraceGlobals.granularReaderTracer == null)
				{
					ExTraceGlobals.granularReaderTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.granularReaderTracer;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x000547ED File Offset: 0x000529ED
		public static Trace ThirdPartyClientTracer
		{
			get
			{
				if (ExTraceGlobals.thirdPartyClientTracer == null)
				{
					ExTraceGlobals.thirdPartyClientTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.thirdPartyClientTracer;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0005480C File Offset: 0x00052A0C
		public static Trace ThirdPartyManagerTracer
		{
			get
			{
				if (ExTraceGlobals.thirdPartyManagerTracer == null)
				{
					ExTraceGlobals.thirdPartyManagerTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.thirdPartyManagerTracer;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x0005482B File Offset: 0x00052A2B
		public static Trace ThirdPartyServiceTracer
		{
			get
			{
				if (ExTraceGlobals.thirdPartyServiceTracer == null)
				{
					ExTraceGlobals.thirdPartyServiceTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.thirdPartyServiceTracer;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0005484A File Offset: 0x00052A4A
		public static Trace ClusterEventsTracer
		{
			get
			{
				if (ExTraceGlobals.clusterEventsTracer == null)
				{
					ExTraceGlobals.clusterEventsTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.clusterEventsTracer;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00054869 File Offset: 0x00052A69
		public static Trace AmNetworkMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.amNetworkMonitorTracer == null)
				{
					ExTraceGlobals.amNetworkMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 47);
				}
				return ExTraceGlobals.amNetworkMonitorTracer;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00054888 File Offset: 0x00052A88
		public static Trace AmConfigManagerTracer
		{
			get
			{
				if (ExTraceGlobals.amConfigManagerTracer == null)
				{
					ExTraceGlobals.amConfigManagerTracer = new Trace(ExTraceGlobals.componentGuid, 48);
				}
				return ExTraceGlobals.amConfigManagerTracer;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000548A7 File Offset: 0x00052AA7
		public static Trace AmSystemManagerTracer
		{
			get
			{
				if (ExTraceGlobals.amSystemManagerTracer == null)
				{
					ExTraceGlobals.amSystemManagerTracer = new Trace(ExTraceGlobals.componentGuid, 49);
				}
				return ExTraceGlobals.amSystemManagerTracer;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x000548C6 File Offset: 0x00052AC6
		public static Trace AmServiceMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.amServiceMonitorTracer == null)
				{
					ExTraceGlobals.amServiceMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.amServiceMonitorTracer;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x000548E5 File Offset: 0x00052AE5
		public static Trace ServiceOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.serviceOperationsTracer == null)
				{
					ExTraceGlobals.serviceOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.serviceOperationsTracer;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00054904 File Offset: 0x00052B04
		public static Trace AmServerNameCacheTracer
		{
			get
			{
				if (ExTraceGlobals.amServerNameCacheTracer == null)
				{
					ExTraceGlobals.amServerNameCacheTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.amServerNameCacheTracer;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00054923 File Offset: 0x00052B23
		public static Trace KernelWatchdogTimerTracer
		{
			get
			{
				if (ExTraceGlobals.kernelWatchdogTimerTracer == null)
				{
					ExTraceGlobals.kernelWatchdogTimerTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.kernelWatchdogTimerTracer;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00054942 File Offset: 0x00052B42
		public static Trace FailureItemHealthMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.failureItemHealthMonitorTracer == null)
				{
					ExTraceGlobals.failureItemHealthMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.failureItemHealthMonitorTracer;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00054961 File Offset: 0x00052B61
		public static Trace ReplayServiceDiagnosticsTracer
		{
			get
			{
				if (ExTraceGlobals.replayServiceDiagnosticsTracer == null)
				{
					ExTraceGlobals.replayServiceDiagnosticsTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.replayServiceDiagnosticsTracer;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00054980 File Offset: 0x00052B80
		public static Trace LogRepairTracer
		{
			get
			{
				if (ExTraceGlobals.logRepairTracer == null)
				{
					ExTraceGlobals.logRepairTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.logRepairTracer;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0005499F File Offset: 0x00052B9F
		public static Trace PassiveBlockModeTracer
		{
			get
			{
				if (ExTraceGlobals.passiveBlockModeTracer == null)
				{
					ExTraceGlobals.passiveBlockModeTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.passiveBlockModeTracer;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x000549BE File Offset: 0x00052BBE
		public static Trace LogCopierTracer
		{
			get
			{
				if (ExTraceGlobals.logCopierTracer == null)
				{
					ExTraceGlobals.logCopierTracer = new Trace(ExTraceGlobals.componentGuid, 59);
				}
				return ExTraceGlobals.logCopierTracer;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000549DD File Offset: 0x00052BDD
		public static Trace DiskHeartbeatTracer
		{
			get
			{
				if (ExTraceGlobals.diskHeartbeatTracer == null)
				{
					ExTraceGlobals.diskHeartbeatTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.diskHeartbeatTracer;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000549FC File Offset: 0x00052BFC
		public static Trace MonitoringTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringTracer == null)
				{
					ExTraceGlobals.monitoringTracer = new Trace(ExTraceGlobals.componentGuid, 61);
				}
				return ExTraceGlobals.monitoringTracer;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00054A1B File Offset: 0x00052C1B
		public static Trace ServerLocatorServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serverLocatorServiceTracer == null)
				{
					ExTraceGlobals.serverLocatorServiceTracer = new Trace(ExTraceGlobals.componentGuid, 62);
				}
				return ExTraceGlobals.serverLocatorServiceTracer;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00054A3A File Offset: 0x00052C3A
		public static Trace ServerLocatorServiceClientTracer
		{
			get
			{
				if (ExTraceGlobals.serverLocatorServiceClientTracer == null)
				{
					ExTraceGlobals.serverLocatorServiceClientTracer = new Trace(ExTraceGlobals.componentGuid, 63);
				}
				return ExTraceGlobals.serverLocatorServiceClientTracer;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00054A59 File Offset: 0x00052C59
		public static Trace LatencyCheckerTracer
		{
			get
			{
				if (ExTraceGlobals.latencyCheckerTracer == null)
				{
					ExTraceGlobals.latencyCheckerTracer = new Trace(ExTraceGlobals.componentGuid, 64);
				}
				return ExTraceGlobals.latencyCheckerTracer;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00054A78 File Offset: 0x00052C78
		public static Trace VolumeManagerTracer
		{
			get
			{
				if (ExTraceGlobals.volumeManagerTracer == null)
				{
					ExTraceGlobals.volumeManagerTracer = new Trace(ExTraceGlobals.componentGuid, 65);
				}
				return ExTraceGlobals.volumeManagerTracer;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00054A97 File Offset: 0x00052C97
		public static Trace AutoReseedTracer
		{
			get
			{
				if (ExTraceGlobals.autoReseedTracer == null)
				{
					ExTraceGlobals.autoReseedTracer = new Trace(ExTraceGlobals.componentGuid, 66);
				}
				return ExTraceGlobals.autoReseedTracer;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00054AB6 File Offset: 0x00052CB6
		public static Trace DiskReclaimerTracer
		{
			get
			{
				if (ExTraceGlobals.diskReclaimerTracer == null)
				{
					ExTraceGlobals.diskReclaimerTracer = new Trace(ExTraceGlobals.componentGuid, 67);
				}
				return ExTraceGlobals.diskReclaimerTracer;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00054AD5 File Offset: 0x00052CD5
		public static Trace ADCacheTracer
		{
			get
			{
				if (ExTraceGlobals.aDCacheTracer == null)
				{
					ExTraceGlobals.aDCacheTracer = new Trace(ExTraceGlobals.componentGuid, 68);
				}
				return ExTraceGlobals.aDCacheTracer;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00054AF4 File Offset: 0x00052CF4
		public static Trace DbTrackerTracer
		{
			get
			{
				if (ExTraceGlobals.dbTrackerTracer == null)
				{
					ExTraceGlobals.dbTrackerTracer = new Trace(ExTraceGlobals.componentGuid, 69);
				}
				return ExTraceGlobals.dbTrackerTracer;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00054B13 File Offset: 0x00052D13
		public static Trace DatabaseCopyLayoutTracer
		{
			get
			{
				if (ExTraceGlobals.databaseCopyLayoutTracer == null)
				{
					ExTraceGlobals.databaseCopyLayoutTracer = new Trace(ExTraceGlobals.componentGuid, 70);
				}
				return ExTraceGlobals.databaseCopyLayoutTracer;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00054B32 File Offset: 0x00052D32
		public static Trace CompositeKeyTracer
		{
			get
			{
				if (ExTraceGlobals.compositeKeyTracer == null)
				{
					ExTraceGlobals.compositeKeyTracer = new Trace(ExTraceGlobals.componentGuid, 71);
				}
				return ExTraceGlobals.compositeKeyTracer;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00054B51 File Offset: 0x00052D51
		public static Trace ClusdbKeyTracer
		{
			get
			{
				if (ExTraceGlobals.clusdbKeyTracer == null)
				{
					ExTraceGlobals.clusdbKeyTracer = new Trace(ExTraceGlobals.componentGuid, 72);
				}
				return ExTraceGlobals.clusdbKeyTracer;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00054B70 File Offset: 0x00052D70
		public static Trace DxStoreKeyTracer
		{
			get
			{
				if (ExTraceGlobals.dxStoreKeyTracer == null)
				{
					ExTraceGlobals.dxStoreKeyTracer = new Trace(ExTraceGlobals.componentGuid, 73);
				}
				return ExTraceGlobals.dxStoreKeyTracer;
			}
		}

		// Token: 0x0400196A RID: 6506
		private static Guid componentGuid = new Guid("404a3308-17e1-4ac3-9167-1b09c36850bd");

		// Token: 0x0400196B RID: 6507
		private static Trace replayApiTracer = null;

		// Token: 0x0400196C RID: 6508
		private static Trace eseutilWrapperTracer = null;

		// Token: 0x0400196D RID: 6509
		private static Trace stateTracer = null;

		// Token: 0x0400196E RID: 6510
		private static Trace logReplayerTracer = null;

		// Token: 0x0400196F RID: 6511
		private static Trace replicaInstanceTracer = null;

		// Token: 0x04001970 RID: 6512
		private static Trace cmdletsTracer = null;

		// Token: 0x04001971 RID: 6513
		private static Trace shipLogTracer = null;

		// Token: 0x04001972 RID: 6514
		private static Trace logCopyTracer = null;

		// Token: 0x04001973 RID: 6515
		private static Trace logInspectorTracer = null;

		// Token: 0x04001974 RID: 6516
		private static Trace replayManagerTracer = null;

		// Token: 0x04001975 RID: 6517
		private static Trace cReplicaSeederTracer = null;

		// Token: 0x04001976 RID: 6518
		private static Trace netShareTracer = null;

		// Token: 0x04001977 RID: 6519
		private static Trace replicaVssWriterInteropTracer = null;

		// Token: 0x04001978 RID: 6520
		private static Trace stateLockTracer = null;

		// Token: 0x04001979 RID: 6521
		private static Trace fileCheckerTracer = null;

		// Token: 0x0400197A RID: 6522
		private static Trace clusterTracer = null;

		// Token: 0x0400197B RID: 6523
		private static Trace seederWrapperTracer = null;

		// Token: 0x0400197C RID: 6524
		private static Trace pFDTracer = null;

		// Token: 0x0400197D RID: 6525
		private static Trace incrementalReseederTracer = null;

		// Token: 0x0400197E RID: 6526
		private static Trace dumpsterTracer = null;

		// Token: 0x0400197F RID: 6527
		private static Trace cLogShipContextTracer = null;

		// Token: 0x04001980 RID: 6528
		private static Trace clusDBWriteTracer = null;

		// Token: 0x04001981 RID: 6529
		private static Trace replayConfigurationTracer = null;

		// Token: 0x04001982 RID: 6530
		private static Trace netPathTracer = null;

		// Token: 0x04001983 RID: 6531
		private static Trace healthChecksTracer = null;

		// Token: 0x04001984 RID: 6532
		private static Trace replayServiceRpcTracer = null;

		// Token: 0x04001985 RID: 6533
		private static Trace activeManagerTracer = null;

		// Token: 0x04001986 RID: 6534
		private static Trace seederServerTracer = null;

		// Token: 0x04001987 RID: 6535
		private static Trace seederClientTracer = null;

		// Token: 0x04001988 RID: 6536
		private static Trace logTruncaterTracer = null;

		// Token: 0x04001989 RID: 6537
		private static Trace failureItemTracer = null;

		// Token: 0x0400198A RID: 6538
		private static Trace logCopyServerTracer = null;

		// Token: 0x0400198B RID: 6539
		private static Trace logCopyClientTracer = null;

		// Token: 0x0400198C RID: 6540
		private static Trace tcpChannelTracer = null;

		// Token: 0x0400198D RID: 6541
		private static Trace tcpClientTracer = null;

		// Token: 0x0400198E RID: 6542
		private static Trace tcpServerTracer = null;

		// Token: 0x0400198F RID: 6543
		private static Trace remoteDataProviderTracer = null;

		// Token: 0x04001990 RID: 6544
		private static Trace monitoredDatabaseTracer = null;

		// Token: 0x04001991 RID: 6545
		private static Trace networkManagerTracer = null;

		// Token: 0x04001992 RID: 6546
		private static Trace networkChannelTracer = null;

		// Token: 0x04001993 RID: 6547
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001994 RID: 6548
		private static Trace granularWriterTracer = null;

		// Token: 0x04001995 RID: 6549
		private static Trace granularReaderTracer = null;

		// Token: 0x04001996 RID: 6550
		private static Trace thirdPartyClientTracer = null;

		// Token: 0x04001997 RID: 6551
		private static Trace thirdPartyManagerTracer = null;

		// Token: 0x04001998 RID: 6552
		private static Trace thirdPartyServiceTracer = null;

		// Token: 0x04001999 RID: 6553
		private static Trace clusterEventsTracer = null;

		// Token: 0x0400199A RID: 6554
		private static Trace amNetworkMonitorTracer = null;

		// Token: 0x0400199B RID: 6555
		private static Trace amConfigManagerTracer = null;

		// Token: 0x0400199C RID: 6556
		private static Trace amSystemManagerTracer = null;

		// Token: 0x0400199D RID: 6557
		private static Trace amServiceMonitorTracer = null;

		// Token: 0x0400199E RID: 6558
		private static Trace serviceOperationsTracer = null;

		// Token: 0x0400199F RID: 6559
		private static Trace amServerNameCacheTracer = null;

		// Token: 0x040019A0 RID: 6560
		private static Trace kernelWatchdogTimerTracer = null;

		// Token: 0x040019A1 RID: 6561
		private static Trace failureItemHealthMonitorTracer = null;

		// Token: 0x040019A2 RID: 6562
		private static Trace replayServiceDiagnosticsTracer = null;

		// Token: 0x040019A3 RID: 6563
		private static Trace logRepairTracer = null;

		// Token: 0x040019A4 RID: 6564
		private static Trace passiveBlockModeTracer = null;

		// Token: 0x040019A5 RID: 6565
		private static Trace logCopierTracer = null;

		// Token: 0x040019A6 RID: 6566
		private static Trace diskHeartbeatTracer = null;

		// Token: 0x040019A7 RID: 6567
		private static Trace monitoringTracer = null;

		// Token: 0x040019A8 RID: 6568
		private static Trace serverLocatorServiceTracer = null;

		// Token: 0x040019A9 RID: 6569
		private static Trace serverLocatorServiceClientTracer = null;

		// Token: 0x040019AA RID: 6570
		private static Trace latencyCheckerTracer = null;

		// Token: 0x040019AB RID: 6571
		private static Trace volumeManagerTracer = null;

		// Token: 0x040019AC RID: 6572
		private static Trace autoReseedTracer = null;

		// Token: 0x040019AD RID: 6573
		private static Trace diskReclaimerTracer = null;

		// Token: 0x040019AE RID: 6574
		private static Trace aDCacheTracer = null;

		// Token: 0x040019AF RID: 6575
		private static Trace dbTrackerTracer = null;

		// Token: 0x040019B0 RID: 6576
		private static Trace databaseCopyLayoutTracer = null;

		// Token: 0x040019B1 RID: 6577
		private static Trace compositeKeyTracer = null;

		// Token: 0x040019B2 RID: 6578
		private static Trace clusdbKeyTracer = null;

		// Token: 0x040019B3 RID: 6579
		private static Trace dxStoreKeyTracer = null;
	}
}
