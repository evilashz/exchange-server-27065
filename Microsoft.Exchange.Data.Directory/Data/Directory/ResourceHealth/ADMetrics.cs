using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009E9 RID: 2537
	internal sealed class ADMetrics
	{
		// Token: 0x060075AF RID: 30127 RVA: 0x00182C08 File Offset: 0x00180E08
		internal ADMetrics(string forestFqdn)
		{
			this.forestFqdn = forestFqdn;
		}

		// Token: 0x17002A2B RID: 10795
		// (get) Token: 0x060075B0 RID: 30128 RVA: 0x00182C40 File Offset: 0x00180E40
		public string ForestFqdn
		{
			get
			{
				return this.forestFqdn;
			}
		}

		// Token: 0x060075B1 RID: 30129 RVA: 0x00182C48 File Offset: 0x00180E48
		public static ADMetrics GetMetricsForForest(ADMetrics previousMetrics, string forestFqdn)
		{
			ADMetrics admetrics = new ADMetrics(forestFqdn);
			TopologyProvider instance = TopologyProvider.GetInstance();
			if (previousMetrics != null && previousMetrics.serversList != null && ExDateTime.UtcNow - previousMetrics.lastTopologyUpdateTime < ADMetrics.topologyRediscoveryInterval && (instance == null || instance.GetTopologyVersion(forestFqdn) == previousMetrics.topologyVersion))
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug(0L, "[ADMetrics::GetMetrics] Using existing topology for forest " + forestFqdn);
				admetrics.lastTopologyUpdateTime = previousMetrics.lastTopologyUpdateTime;
				admetrics.serversList = previousMetrics.serversList;
				admetrics.rediscoverTopology = false;
				admetrics.topologyVersion = previousMetrics.topologyVersion;
			}
			else
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug(0L, "[ADMetrics::GetMetrics] Rediscovering topology.");
				admetrics.rediscoverTopology = true;
			}
			if (admetrics.Populate(previousMetrics))
			{
				return admetrics;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADHealthFailed, null, new object[0]);
			return null;
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x00182D24 File Offset: 0x00180F24
		internal Dictionary<string, ADServerMetrics> GetTopNDomainControllers(int n)
		{
			Dictionary<string, ADServerMetrics> dictionary = new Dictionary<string, ADServerMetrics>();
			if (n > 0)
			{
				IOrderedEnumerable<KeyValuePair<string, ADServerMetrics>> source = from pair in this.allADServerMetrics
				orderby pair.Value.IncomingDebt descending
				select pair;
				IEnumerable<KeyValuePair<string, ADServerMetrics>> enumerable = source.Take(n);
				foreach (KeyValuePair<string, ADServerMetrics> keyValuePair in enumerable)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x00182DBC File Offset: 0x00180FBC
		internal void AddServerMetrics(ADServerMetrics metrics)
		{
			if (metrics == null)
			{
				throw new ArgumentNullException("metrics");
			}
			if (metrics.DnsHostName == null)
			{
				throw new ArgumentNullException("metrics.DnsHostName");
			}
			this.allADServerMetrics.Add(metrics.DnsHostName, metrics);
		}

		// Token: 0x17002A2C RID: 10796
		// (get) Token: 0x060075B4 RID: 30132 RVA: 0x00182DF1 File Offset: 0x00180FF1
		public ICollection<ADServerMetrics> AllServerMetrics
		{
			get
			{
				if (this.allADServerMetrics != null)
				{
					return this.allADServerMetrics.Values;
				}
				return null;
			}
		}

		// Token: 0x17002A2D RID: 10797
		public ADServerMetrics this[string dnsHostName]
		{
			get
			{
				ADServerMetrics result;
				try
				{
					result = this.allADServerMetrics[dnsHostName];
				}
				catch (KeyNotFoundException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17002A2E RID: 10798
		// (get) Token: 0x060075B6 RID: 30134 RVA: 0x00182E54 File Offset: 0x00181054
		public ExDateTime UpdateTime
		{
			get
			{
				if (this.updateTime == null && this.AllServerMetrics != null)
				{
					if (this.AllServerMetrics.Any((ADServerMetrics i) => i.IsSuitable))
					{
						this.updateTime = new ExDateTime?((from i in this.AllServerMetrics
						where i.IsSuitable
						select i).Min((ADServerMetrics i) => i.UpdateTime));
					}
					else
					{
						this.updateTime = new ExDateTime?(ExDateTime.MinValue);
					}
				}
				ExDateTime? exDateTime = this.updateTime;
				if (exDateTime == null)
				{
					return ExDateTime.MinValue;
				}
				return exDateTime.GetValueOrDefault();
			}
		}

		// Token: 0x17002A2F RID: 10799
		// (get) Token: 0x060075B7 RID: 30135 RVA: 0x00182F2A File Offset: 0x0018112A
		// (set) Token: 0x060075B8 RID: 30136 RVA: 0x00182F32 File Offset: 0x00181132
		public int IncomingHealth { get; set; }

		// Token: 0x17002A30 RID: 10800
		// (get) Token: 0x060075B9 RID: 30137 RVA: 0x00182F3B File Offset: 0x0018113B
		// (set) Token: 0x060075BA RID: 30138 RVA: 0x00182F43 File Offset: 0x00181143
		public string MinIncomingHealthServer { get; set; }

		// Token: 0x17002A31 RID: 10801
		// (get) Token: 0x060075BB RID: 30139 RVA: 0x00182F4C File Offset: 0x0018114C
		// (set) Token: 0x060075BC RID: 30140 RVA: 0x00182F54 File Offset: 0x00181154
		public int OutgoingHealth { get; set; }

		// Token: 0x17002A32 RID: 10802
		// (get) Token: 0x060075BD RID: 30141 RVA: 0x00182F5D File Offset: 0x0018115D
		// (set) Token: 0x060075BE RID: 30142 RVA: 0x00182F65 File Offset: 0x00181165
		public string MinOutgoingHealthServer { get; set; }

		// Token: 0x060075BF RID: 30143 RVA: 0x00182FCC File Offset: 0x001811CC
		private bool Populate(ADMetrics previousMetrics)
		{
			try
			{
				if (this.rediscoverTopology)
				{
					this.PopulateTopologyVersion();
					this.PopulateADServersList();
				}
				foreach (ADServer dc in this.serversList)
				{
					this.AddServerMetrics(new ADServerMetrics(dc));
				}
			}
			catch (ADTransientException arg)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<ADTransientException>((long)this.GetHashCode(), "[ADMetrics::Populate] Failed to get read a list of DC: {0}", arg);
				return false;
			}
			catch (ADOperationException arg2)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<ADOperationException>((long)this.GetHashCode(), "[ADMetrics::Populate] Failed to get read a list of DC: {0}", arg2);
				return false;
			}
			List<ADServerMetrics> list = new List<ADServerMetrics>();
			foreach (ADServerMetrics adserverMetrics in this.AllServerMetrics)
			{
				ADServerMetrics adserverMetrics2 = (previousMetrics != null) ? previousMetrics[adserverMetrics.DnsHostName] : null;
				if (this.rediscoverTopology || adserverMetrics2 == null || adserverMetrics2.IsSuitable)
				{
					list.Add(adserverMetrics);
				}
			}
			if (list.Count <= 0)
			{
				goto IL_1C3;
			}
			using (ActivityContext.SuppressThreadScope())
			{
				CancellationTokenSource cts = new CancellationTokenSource();
				using (new Timer(delegate(object _)
				{
					cts.Cancel();
				}, null, 120000, -1))
				{
					try
					{
						Parallel.ForEach<ADServerMetrics>(list, new ParallelOptions
						{
							CancellationToken = cts.Token
						}, delegate(ADServerMetrics item)
						{
							try
							{
								Interlocked.Increment(ref this.pooledDiscoveryCount);
								this.PopulateSingleServerMetrics(item);
							}
							finally
							{
								Interlocked.Decrement(ref this.pooledDiscoveryCount);
							}
						});
					}
					catch (OperationCanceledException arg3)
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceError<OperationCanceledException>((long)this.GetHashCode(), "[ADMetrics::PopulateSingleServerMetrics] Timed out trying to read AD metrics from DCs: {0}", arg3);
					}
				}
				goto IL_1C3;
			}
			IL_1B9:
			Thread.Sleep(500);
			IL_1C3:
			if (this.pooledDiscoveryCount <= 0)
			{
				return this.AllServerMetrics.Any((ADServerMetrics server) => server.IsSuitable);
			}
			goto IL_1B9;
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x00183228 File Offset: 0x00181428
		private void PopulateTopologyVersion()
		{
			TopologyProvider instance = TopologyProvider.GetInstance();
			if (instance != null)
			{
				this.topologyVersion = instance.GetTopologyVersion(this.forestFqdn);
			}
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x00183250 File Offset: 0x00181450
		private void PopulateADServersList()
		{
			List<ADServer> list = new List<ADServer>();
			foreach (ADDomain addomain in ADForest.GetForest(new PartitionId(this.forestFqdn)).FindDomains())
			{
				foreach (ADServer adserver in addomain.FindAllDomainControllers(true))
				{
					if (adserver.DnsHostName != null)
					{
						list.Add(adserver);
					}
				}
			}
			this.lastTopologyUpdateTime = ExDateTime.UtcNow;
			this.serversList = list;
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x001833E0 File Offset: 0x001815E0
		private void PopulateSingleServerMetrics(ADServerMetrics dcMetrics)
		{
			Exception ex = null;
			try
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "[ADMetrics::PopulateSingleServerMetrics] Reading metrics for {0}.", dcMetrics.DnsHostName);
				string text;
				LocalizedString localizedString;
				if (!SuitabilityVerifier.IsServerSuitableIgnoreExceptions(dcMetrics.DnsHostName, false, null, out text, out localizedString))
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string, LocalizedString>((long)this.GetHashCode(), "[ADMetrics::PopulateSingleServerMetrics] Suitability checks failed for {0}: {1}", dcMetrics.DnsHostName, localizedString);
					dcMetrics.ErrorMessage = localizedString;
					return;
				}
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(dcMetrics.DnsHostName, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(new PartitionId(this.forestFqdn)), 470, "PopulateSingleServerMetrics", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\ResourceHealth\\ADMetrics.cs");
				topologyConfigurationSession.UseGlobalCatalog = false;
				ExDateTime utcNow = ExDateTime.UtcNow;
				RootDse rootDse = topologyConfigurationSession.GetRootDse();
				dcMetrics.IsSynchronized = rootDse.IsSynchronized;
				if (!dcMetrics.IsSynchronized)
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string>((long)this.GetHashCode(), "[ADMetrics::PopulateSingleServerMetrics] {0} is not synchronized yet.", dcMetrics.DnsHostName);
					return;
				}
				MultiValuedProperty<ReplicationCursor> source;
				MultiValuedProperty<ReplicationNeighbor> neighbors;
				topologyConfigurationSession.ReadReplicationData(topologyConfigurationSession.ConfigurationNamingContext, out source, out neighbors);
				IEnumerable<ReplicationCursor> replicationCursors = from cursor in source
				where neighbors.Any((ReplicationNeighbor neighbor) => this.NotNullAndEquals(neighbor.SourceDsa, cursor.SourceDsa))
				select cursor;
				ICollection<ADReplicationLinkMetrics> configReplicationMetrics = this.ReadReplicationMetrics(replicationCursors);
				topologyConfigurationSession.UseConfigNC = false;
				topologyConfigurationSession.ReadReplicationData(dcMetrics.ServerId.DomainId, out source, out neighbors);
				replicationCursors = from cursor in source
				where neighbors.Any((ReplicationNeighbor neighbor) => this.NotNullAndEquals(neighbor.SourceDsa, cursor.SourceDsa))
				select cursor;
				ICollection<ADReplicationLinkMetrics> domainReplicationMetrics = this.ReadReplicationMetrics(replicationCursors);
				dcMetrics.Initialize(utcNow, rootDse.HighestCommittedUSN, configReplicationMetrics, domainReplicationMetrics);
				dcMetrics.IsSuitable = true;
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "[ADMetrics::PopulateSingleServerMetrics] Finished reading metrics for {0}.", dcMetrics.DnsHostName);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADOperationException ex3)
			{
				ex = ex3;
			}
			catch (OperationCanceledException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "[ADMetrics::PopulateSingleServerMetrics] Failed to get read AD metrics from {0}: {1}", dcMetrics.DnsHostName, ex);
				dcMetrics.ErrorMessage = new LocalizedString(ex.Message);
			}
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x00183618 File Offset: 0x00181818
		private bool NotNullAndEquals(ADObjectId id1, ADObjectId id2)
		{
			return id1 != null && id2 != null && id1.DistinguishedName == id2.DistinguishedName;
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x00183634 File Offset: 0x00181834
		private ICollection<ADReplicationLinkMetrics> ReadReplicationMetrics(IEnumerable<ReplicationCursor> replicationCursors)
		{
			List<ADReplicationLinkMetrics> list = new List<ADReplicationLinkMetrics>(2);
			foreach (ReplicationCursor replicationCursor in replicationCursors)
			{
				if (replicationCursor.SourceDsa != null)
				{
					using (IEnumerator<ADServerMetrics> enumerator2 = this.AllServerMetrics.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ADServerMetrics adserverMetrics = enumerator2.Current;
							if (adserverMetrics.IsSuitable && string.Equals(replicationCursor.SourceDsa.Parent.Name, adserverMetrics.ServerId.Name, StringComparison.OrdinalIgnoreCase))
							{
								list.Add(new ADReplicationLinkMetrics(adserverMetrics.DnsHostName, replicationCursor.UpToDatenessUsn));
								break;
							}
						}
						continue;
					}
				}
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[ADMetrics::ReadReplicationMetrics] Replication cursor with SourceInvocationId={0} does not have SourceDsa.", replicationCursor.SourceInvocationId);
			}
			return list;
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x00183728 File Offset: 0x00181928
		public string GetReport()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
			{
				xmlWriter.WriteStartElement("ADMetrics");
				xmlWriter.WriteAttributeString("InHealth", this.IncomingHealth.ToString());
				if (!string.IsNullOrEmpty(this.MinIncomingHealthServer))
				{
					xmlWriter.WriteAttributeString("MinInHealthServer", this.MinIncomingHealthServer.Split(new char[]
					{
						'.'
					})[0]);
				}
				xmlWriter.WriteAttributeString("OutHealth", this.OutgoingHealth.ToString());
				if (!string.IsNullOrEmpty(this.MinOutgoingHealthServer))
				{
					xmlWriter.WriteAttributeString("MinOutHealthServer", this.MinOutgoingHealthServer.Split(new char[]
					{
						'.'
					})[0]);
				}
				foreach (ADServerMetrics adserverMetrics in this.AllServerMetrics)
				{
					if (adserverMetrics.IsSuitable)
					{
						xmlWriter.WriteStartElement("Server");
						xmlWriter.WriteAttributeString("Name", adserverMetrics.DnsHostName.Split(new char[]
						{
							'.'
						})[0]);
						xmlWriter.WriteAttributeString("UpdateTime", adserverMetrics.UpdateTime.ToString());
						xmlWriter.WriteAttributeString("HighestUsn", adserverMetrics.HighestUsn.ToString());
						xmlWriter.WriteAttributeString("InjectRate", adserverMetrics.InjectionRate.ToString());
						xmlWriter.WriteAttributeString("InDebt", adserverMetrics.IncomingDebt.ToString());
						xmlWriter.WriteAttributeString("OutDebt", adserverMetrics.OutgoingDebt.ToString());
						xmlWriter.WriteAttributeString("InHealth", adserverMetrics.IncomingHealth.ToString());
						xmlWriter.WriteAttributeString("OutHealth", adserverMetrics.OutgoingHealth.ToString());
						if (adserverMetrics.ConfigReplicationMetrics != null && adserverMetrics.ConfigReplicationMetrics.Count > 0)
						{
							xmlWriter.WriteStartElement("Config");
							foreach (ADReplicationLinkMetrics adreplicationLinkMetrics in adserverMetrics.ConfigReplicationMetrics)
							{
								xmlWriter.WriteStartElement("Link");
								xmlWriter.WriteAttributeString("Neighbor", adreplicationLinkMetrics.NeighborDnsHostName.Split(new char[]
								{
									'.'
								})[0]);
								xmlWriter.WriteAttributeString("Usn", adreplicationLinkMetrics.UpToDatenessUsn.ToString());
								xmlWriter.WriteAttributeString("Debt", adreplicationLinkMetrics.Debt.ToString());
								xmlWriter.WriteEndElement();
							}
							xmlWriter.WriteEndElement();
						}
						if (adserverMetrics.DomainReplicationMetrics != null && adserverMetrics.DomainReplicationMetrics.Count > 0)
						{
							xmlWriter.WriteStartElement("Domain");
							foreach (ADReplicationLinkMetrics adreplicationLinkMetrics2 in adserverMetrics.DomainReplicationMetrics)
							{
								xmlWriter.WriteStartElement("Link");
								xmlWriter.WriteAttributeString("Neighbor", adreplicationLinkMetrics2.NeighborDnsHostName.Split(new char[]
								{
									'.'
								})[0]);
								xmlWriter.WriteAttributeString("Usn", adreplicationLinkMetrics2.UpToDatenessUsn.ToString());
								xmlWriter.WriteAttributeString("Debt", adreplicationLinkMetrics2.Debt.ToString());
								xmlWriter.WriteEndElement();
							}
							xmlWriter.WriteEndElement();
						}
						xmlWriter.WriteEndElement();
					}
				}
				xmlWriter.WriteEndElement();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04004B7B RID: 19323
		private static readonly TimeSpan topologyRediscoveryInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04004B7C RID: 19324
		private Dictionary<string, ADServerMetrics> allADServerMetrics = new Dictionary<string, ADServerMetrics>();

		// Token: 0x04004B7D RID: 19325
		private ExDateTime? updateTime = null;

		// Token: 0x04004B7E RID: 19326
		private ExDateTime lastTopologyUpdateTime = ExDateTime.MinValue;

		// Token: 0x04004B7F RID: 19327
		private List<ADServer> serversList;

		// Token: 0x04004B80 RID: 19328
		private bool rediscoverTopology = true;

		// Token: 0x04004B81 RID: 19329
		private int topologyVersion;

		// Token: 0x04004B82 RID: 19330
		private int pooledDiscoveryCount;

		// Token: 0x04004B83 RID: 19331
		private readonly string forestFqdn;

		// Token: 0x020009EA RID: 2538
		private sealed class AsyncState
		{
			// Token: 0x060075CD RID: 30157 RVA: 0x00183B49 File Offset: 0x00181D49
			public AsyncState(ADServerMetrics dcMetrics, ADMetrics adMetrics)
			{
				this.ServerMetrics = dcMetrics;
				this.ADMetrics = adMetrics;
			}

			// Token: 0x17002A33 RID: 10803
			// (get) Token: 0x060075CE RID: 30158 RVA: 0x00183B5F File Offset: 0x00181D5F
			// (set) Token: 0x060075CF RID: 30159 RVA: 0x00183B67 File Offset: 0x00181D67
			public ADServerMetrics ServerMetrics { get; private set; }

			// Token: 0x17002A34 RID: 10804
			// (get) Token: 0x060075D0 RID: 30160 RVA: 0x00183B70 File Offset: 0x00181D70
			// (set) Token: 0x060075D1 RID: 30161 RVA: 0x00183B78 File Offset: 0x00181D78
			public ADMetrics ADMetrics { get; private set; }
		}
	}
}
