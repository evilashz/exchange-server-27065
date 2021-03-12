using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020001AA RID: 426
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ActiveManagerClientPerfmonInstance : PerformanceCounterInstance
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x00071018 File Offset: 0x0006F218
		internal ActiveManagerClientPerfmonInstance(string instanceName, ActiveManagerClientPerfmonInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Active Manager Client")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.GetServerForDatabaseClientCalls = new ExPerformanceCounter(base.CategoryName, "Client-side Calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCalls);
				this.GetServerForDatabaseClientCallsPerSec = new ExPerformanceCounter(base.CategoryName, "Client-side Calls/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCallsPerSec);
				this.GetServerForDatabaseClientCacheHits = new ExPerformanceCounter(base.CategoryName, "Client-side Cache Hits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCacheHits);
				this.GetServerForDatabaseClientCacheMisses = new ExPerformanceCounter(base.CategoryName, "Client-side Cache Misses", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCacheMisses);
				this.GetServerForDatabaseClientCallsWithReadThrough = new ExPerformanceCounter(base.CategoryName, "Client-side Calls ReadThrough", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCallsWithReadThrough);
				this.GetServerForDatabaseClientRpcCalls = new ExPerformanceCounter(base.CategoryName, "Client-side RPC Calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientRpcCalls);
				this.GetServerForDatabaseClientUniqueDatabases = new ExPerformanceCounter(base.CategoryName, "Unique databases queried", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientUniqueDatabases);
				this.GetServerForDatabaseClientUniqueServers = new ExPerformanceCounter(base.CategoryName, "Unique servers queried", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientUniqueServers);
				this.GetServerForDatabaseClientLocationCacheEntries = new ExPerformanceCounter(base.CategoryName, "Location cache entries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientLocationCacheEntries);
				this.CacheUpdateTimeInSec = new ExPerformanceCounter(base.CategoryName, "Location cache update time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheUpdateTimeInSec);
				this.GetServerForDatabaseClientServerInformationCacheHits = new ExPerformanceCounter(base.CategoryName, "Server-Information Cache Hits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientServerInformationCacheHits);
				this.GetServerForDatabaseClientServerInformationCacheMisses = new ExPerformanceCounter(base.CategoryName, "Server-Information Cache Misses", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientServerInformationCacheMisses);
				this.GetServerForDatabaseClientServerInformationCacheEntries = new ExPerformanceCounter(base.CategoryName, "Server-Information Cache Entries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientServerInformationCacheEntries);
				this.CalculatePreferredHomeServerCalls = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerCalls);
				this.CalculatePreferredHomeServerCallsPerSec = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Calls/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerCallsPerSec);
				this.CalculatePreferredHomeServerRedirects = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Redirects", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerRedirects);
				this.CalculatePreferredHomeServerRedirectsPerSec = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Redirects/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerRedirectsPerSec);
				this.GetServerForDatabaseWCFLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLatency);
				this.GetServerForDatabaseWCFLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLatencyTimeBase);
				this.GetServerForDatabaseWCFLocalLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to the local server latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalLatency);
				this.GetServerForDatabaseWCFLocalLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to the local server time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalLatencyTimeBase);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the local site latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteLatency);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the local site latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteLatencyTimeBase);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the remote site latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatency);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the remote site latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatencyTimeBase);
				this.GetServerForDatabaseWCFRemoteDomainLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in a remote domain latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainLatency);
				this.GetServerForDatabaseWCFRemoteDomainLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in a remote domain latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainLatencyTimeBase);
				this.GetServerForDatabaseWCFCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFCalls);
				this.GetServerForDatabaseWCFCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFCallsPerSec);
				this.GetServerForDatabaseWCFLocalCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to local server", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalCalls);
				this.GetServerForDatabaseWCFLocalCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to local server", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalCallsPerSec);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to a remote server in local domain and local site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteCalls);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to a remote server in local domain and local site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteCallsPerSec);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to a remote server in remote site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteCalls);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to a remote server in remote site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteCallsPerSec);
				this.GetServerForDatabaseWCFRemoteDomainCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to a remote server in a remote domain", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainCalls);
				this.GetServerForDatabaseWCFRemoteDomainCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to a remote server in a remote domain", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainCallsPerSec);
				this.GetServerForDatabaseWCFErrors = new ExPerformanceCounter(base.CategoryName, "WCF calls returning an error", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFErrors);
				this.GetServerForDatabaseWCFErrorsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec returning an error", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFErrorsPerSec);
				this.GetServerForDatabaseWCFTimeouts = new ExPerformanceCounter(base.CategoryName, "WCF calls timing out", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFTimeouts);
				this.GetServerForDatabaseWCFTimeoutsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec timing out", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFTimeoutsPerSec);
				long num = this.GetServerForDatabaseClientCalls.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000717A8 File Offset: 0x0006F9A8
		internal ActiveManagerClientPerfmonInstance(string instanceName) : base(instanceName, "MSExchange Active Manager Client")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.GetServerForDatabaseClientCalls = new ExPerformanceCounter(base.CategoryName, "Client-side Calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCalls);
				this.GetServerForDatabaseClientCallsPerSec = new ExPerformanceCounter(base.CategoryName, "Client-side Calls/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCallsPerSec);
				this.GetServerForDatabaseClientCacheHits = new ExPerformanceCounter(base.CategoryName, "Client-side Cache Hits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCacheHits);
				this.GetServerForDatabaseClientCacheMisses = new ExPerformanceCounter(base.CategoryName, "Client-side Cache Misses", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCacheMisses);
				this.GetServerForDatabaseClientCallsWithReadThrough = new ExPerformanceCounter(base.CategoryName, "Client-side Calls ReadThrough", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientCallsWithReadThrough);
				this.GetServerForDatabaseClientRpcCalls = new ExPerformanceCounter(base.CategoryName, "Client-side RPC Calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientRpcCalls);
				this.GetServerForDatabaseClientUniqueDatabases = new ExPerformanceCounter(base.CategoryName, "Unique databases queried", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientUniqueDatabases);
				this.GetServerForDatabaseClientUniqueServers = new ExPerformanceCounter(base.CategoryName, "Unique servers queried", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientUniqueServers);
				this.GetServerForDatabaseClientLocationCacheEntries = new ExPerformanceCounter(base.CategoryName, "Location cache entries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientLocationCacheEntries);
				this.CacheUpdateTimeInSec = new ExPerformanceCounter(base.CategoryName, "Location cache update time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheUpdateTimeInSec);
				this.GetServerForDatabaseClientServerInformationCacheHits = new ExPerformanceCounter(base.CategoryName, "Server-Information Cache Hits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientServerInformationCacheHits);
				this.GetServerForDatabaseClientServerInformationCacheMisses = new ExPerformanceCounter(base.CategoryName, "Server-Information Cache Misses", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientServerInformationCacheMisses);
				this.GetServerForDatabaseClientServerInformationCacheEntries = new ExPerformanceCounter(base.CategoryName, "Server-Information Cache Entries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseClientServerInformationCacheEntries);
				this.CalculatePreferredHomeServerCalls = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerCalls);
				this.CalculatePreferredHomeServerCallsPerSec = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Calls/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerCallsPerSec);
				this.CalculatePreferredHomeServerRedirects = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Redirects", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerRedirects);
				this.CalculatePreferredHomeServerRedirectsPerSec = new ExPerformanceCounter(base.CategoryName, "CalculatePreferredHomeServer Redirects/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CalculatePreferredHomeServerRedirectsPerSec);
				this.GetServerForDatabaseWCFLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLatency);
				this.GetServerForDatabaseWCFLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLatencyTimeBase);
				this.GetServerForDatabaseWCFLocalLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to the local server latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalLatency);
				this.GetServerForDatabaseWCFLocalLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to the local server time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalLatencyTimeBase);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the local site latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteLatency);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the local site latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteLatencyTimeBase);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the remote site latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatency);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in the remote site latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteLatencyTimeBase);
				this.GetServerForDatabaseWCFRemoteDomainLatency = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in a remote domain latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainLatency);
				this.GetServerForDatabaseWCFRemoteDomainLatencyTimeBase = new ExPerformanceCounter(base.CategoryName, "Average WCF calls to a remote server in a remote domain latency time base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainLatencyTimeBase);
				this.GetServerForDatabaseWCFCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFCalls);
				this.GetServerForDatabaseWCFCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFCallsPerSec);
				this.GetServerForDatabaseWCFLocalCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to local server", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalCalls);
				this.GetServerForDatabaseWCFLocalCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to local server", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalCallsPerSec);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to a remote server in local domain and local site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteCalls);
				this.GetServerForDatabaseWCFLocalDomainLocalSiteCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to a remote server in local domain and local site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainLocalSiteCallsPerSec);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to a remote server in remote site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteCalls);
				this.GetServerForDatabaseWCFLocalDomainRemoteSiteCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to a remote server in remote site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFLocalDomainRemoteSiteCallsPerSec);
				this.GetServerForDatabaseWCFRemoteDomainCalls = new ExPerformanceCounter(base.CategoryName, "WCF calls to a remote server in a remote domain", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainCalls);
				this.GetServerForDatabaseWCFRemoteDomainCallsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec to a remote server in a remote domain", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFRemoteDomainCallsPerSec);
				this.GetServerForDatabaseWCFErrors = new ExPerformanceCounter(base.CategoryName, "WCF calls returning an error", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFErrors);
				this.GetServerForDatabaseWCFErrorsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec returning an error", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFErrorsPerSec);
				this.GetServerForDatabaseWCFTimeouts = new ExPerformanceCounter(base.CategoryName, "WCF calls timing out", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFTimeouts);
				this.GetServerForDatabaseWCFTimeoutsPerSec = new ExPerformanceCounter(base.CategoryName, "WCF Calls/Sec timing out", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GetServerForDatabaseWCFTimeoutsPerSec);
				long num = this.GetServerForDatabaseClientCalls.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00071F38 File Offset: 0x00070138
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000B28 RID: 2856
		public readonly ExPerformanceCounter GetServerForDatabaseClientCalls;

		// Token: 0x04000B29 RID: 2857
		public readonly ExPerformanceCounter GetServerForDatabaseClientCallsPerSec;

		// Token: 0x04000B2A RID: 2858
		public readonly ExPerformanceCounter GetServerForDatabaseClientCacheHits;

		// Token: 0x04000B2B RID: 2859
		public readonly ExPerformanceCounter GetServerForDatabaseClientCacheMisses;

		// Token: 0x04000B2C RID: 2860
		public readonly ExPerformanceCounter GetServerForDatabaseClientCallsWithReadThrough;

		// Token: 0x04000B2D RID: 2861
		public readonly ExPerformanceCounter GetServerForDatabaseClientRpcCalls;

		// Token: 0x04000B2E RID: 2862
		public readonly ExPerformanceCounter GetServerForDatabaseClientUniqueDatabases;

		// Token: 0x04000B2F RID: 2863
		public readonly ExPerformanceCounter GetServerForDatabaseClientUniqueServers;

		// Token: 0x04000B30 RID: 2864
		public readonly ExPerformanceCounter GetServerForDatabaseClientLocationCacheEntries;

		// Token: 0x04000B31 RID: 2865
		public readonly ExPerformanceCounter CacheUpdateTimeInSec;

		// Token: 0x04000B32 RID: 2866
		public readonly ExPerformanceCounter GetServerForDatabaseClientServerInformationCacheHits;

		// Token: 0x04000B33 RID: 2867
		public readonly ExPerformanceCounter GetServerForDatabaseClientServerInformationCacheMisses;

		// Token: 0x04000B34 RID: 2868
		public readonly ExPerformanceCounter GetServerForDatabaseClientServerInformationCacheEntries;

		// Token: 0x04000B35 RID: 2869
		public readonly ExPerformanceCounter CalculatePreferredHomeServerCalls;

		// Token: 0x04000B36 RID: 2870
		public readonly ExPerformanceCounter CalculatePreferredHomeServerCallsPerSec;

		// Token: 0x04000B37 RID: 2871
		public readonly ExPerformanceCounter CalculatePreferredHomeServerRedirects;

		// Token: 0x04000B38 RID: 2872
		public readonly ExPerformanceCounter CalculatePreferredHomeServerRedirectsPerSec;

		// Token: 0x04000B39 RID: 2873
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLatency;

		// Token: 0x04000B3A RID: 2874
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLatencyTimeBase;

		// Token: 0x04000B3B RID: 2875
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalLatency;

		// Token: 0x04000B3C RID: 2876
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalLatencyTimeBase;

		// Token: 0x04000B3D RID: 2877
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainLocalSiteLatency;

		// Token: 0x04000B3E RID: 2878
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainLocalSiteLatencyTimeBase;

		// Token: 0x04000B3F RID: 2879
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainRemoteSiteLatency;

		// Token: 0x04000B40 RID: 2880
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainRemoteSiteLatencyTimeBase;

		// Token: 0x04000B41 RID: 2881
		public readonly ExPerformanceCounter GetServerForDatabaseWCFRemoteDomainLatency;

		// Token: 0x04000B42 RID: 2882
		public readonly ExPerformanceCounter GetServerForDatabaseWCFRemoteDomainLatencyTimeBase;

		// Token: 0x04000B43 RID: 2883
		public readonly ExPerformanceCounter GetServerForDatabaseWCFCalls;

		// Token: 0x04000B44 RID: 2884
		public readonly ExPerformanceCounter GetServerForDatabaseWCFCallsPerSec;

		// Token: 0x04000B45 RID: 2885
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalCalls;

		// Token: 0x04000B46 RID: 2886
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalCallsPerSec;

		// Token: 0x04000B47 RID: 2887
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainLocalSiteCalls;

		// Token: 0x04000B48 RID: 2888
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainLocalSiteCallsPerSec;

		// Token: 0x04000B49 RID: 2889
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainRemoteSiteCalls;

		// Token: 0x04000B4A RID: 2890
		public readonly ExPerformanceCounter GetServerForDatabaseWCFLocalDomainRemoteSiteCallsPerSec;

		// Token: 0x04000B4B RID: 2891
		public readonly ExPerformanceCounter GetServerForDatabaseWCFRemoteDomainCalls;

		// Token: 0x04000B4C RID: 2892
		public readonly ExPerformanceCounter GetServerForDatabaseWCFRemoteDomainCallsPerSec;

		// Token: 0x04000B4D RID: 2893
		public readonly ExPerformanceCounter GetServerForDatabaseWCFErrors;

		// Token: 0x04000B4E RID: 2894
		public readonly ExPerformanceCounter GetServerForDatabaseWCFErrorsPerSec;

		// Token: 0x04000B4F RID: 2895
		public readonly ExPerformanceCounter GetServerForDatabaseWCFTimeouts;

		// Token: 0x04000B50 RID: 2896
		public readonly ExPerformanceCounter GetServerForDatabaseWCFTimeoutsPerSec;
	}
}
