using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200005D RID: 93
	internal static class AutoDiscoverDnsReader
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000A104 File Offset: 0x00008304
		internal static void Initialize()
		{
			IPHostEntry iphostEntry = null;
			try
			{
				if (!string.IsNullOrEmpty(Configuration.DnsServerAddress.Value))
				{
					IPAddress address;
					bool flag = IPAddress.TryParse(Configuration.DnsServerAddress.Value, out address);
					if (flag)
					{
						iphostEntry = Dns.GetHostEntry(address);
					}
					else
					{
						AutoDiscoverDnsReader.DnsReaderTracer.TraceError(0L, "Failed to get a valid ipAddress from web.config.");
					}
				}
				if (iphostEntry != null)
				{
					AutoDiscoverDnsReader.dns.ServerList.Initialize(iphostEntry.AddressList);
				}
				else
				{
					AutoDiscoverDnsReader.dns.InitializeFromMachineServerList();
				}
			}
			catch (SocketException e)
			{
				ConfigurationReader.HandleException(e);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A194 File Offset: 0x00008394
		public static Uri Query(string domain)
		{
			IAsyncResult asyncResult = AutoDiscoverDnsReader.BeginQuery(domain);
			if (!asyncResult.AsyncWaitHandle.WaitOne(Configuration.AutodiscoverSrvRecordLookupTimeoutInSeconds.Value, false))
			{
				AutoDiscoverDnsReader.DnsReaderTracer.TraceError<string>(0L, "DNS query for SRV records for domain {0} did not complete in time.", domain);
				return null;
			}
			string text = AutoDiscoverDnsReader.FetchResults(asyncResult);
			if (string.IsNullOrEmpty(text))
			{
				AutoDiscoverDnsReader.DnsReaderTracer.TraceError<string>(0L, "DNS query for SRV record for domain {0} found an null SRV target", domain);
				return null;
			}
			AutoDiscoverDnsReader.DnsReaderTracer.TraceDebug<string, string>(0L, "DNS query for SRV record for domain {0} found target '{1}'", domain, text);
			string text2 = string.Format("https://{0}/autodiscover/autodiscover.xml", text);
			if (string.IsNullOrEmpty(text2) || !Uri.IsWellFormedUriString(text2, UriKind.Absolute))
			{
				AutoDiscoverDnsReader.DnsReaderTracer.TraceError<string, string>(0L, "DNS query for SRV record for domain {0} found an non-valid autodiscover URL {1}", domain, text2);
				return null;
			}
			AutoDiscoverDnsReader.DnsReaderTracer.TraceDebug<string, string>(0L, "DNS query for SRV record for domain {0] found {1}", domain, text2);
			return new Uri(text2);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A258 File Offset: 0x00008458
		internal static IAsyncResult BeginQuery(string domain)
		{
			string text = "_autodiscover._tcp." + domain;
			AutoDiscoverDnsReader.DnsReaderTracer.TraceDebug<string>(0L, "Issuing a DNS query for {0}.", text);
			DnsQueryOptions options = DnsQueryOptions.None;
			if (Configuration.BypassDnsCache.Value)
			{
				options = DnsQueryOptions.BypassCache;
			}
			return AutoDiscoverDnsReader.dns.BeginRetrieveSrvRecords(text, options, null, null);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A2D8 File Offset: 0x000084D8
		internal static string FetchResults(IAsyncResult ar)
		{
			int priority = -1;
			int weight = -1;
			SrvRecord[] array;
			DnsStatus dnsStatus = Dns.EndResolveToSrvRecords(ar, out array);
			if (dnsStatus != DnsStatus.Success)
			{
				AutoDiscoverDnsReader.DnsReaderTracer.TraceError(0L, "Failed to get results from DNS.");
				return null;
			}
			int length = array.GetLength(0);
			if (length == 0)
			{
				AutoDiscoverDnsReader.DnsReaderTracer.TraceError(0L, "No SRV records were returned.");
				return null;
			}
			AutoDiscoverDnsReader.DnsReaderTracer.TraceDebug<int>(0L, "{0} SRV records were returned.", length);
			bool flag = false;
			for (int i = 0; i < length; i++)
			{
				if (array[i].Port == 443)
				{
					priority = array[i].Priority;
					weight = array[i].Weight;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				AutoDiscoverDnsReader.DnsReaderTracer.TraceError(0L, "No SRV records were returned with SSL Port specified.");
				return null;
			}
			SrvRecord[] array2 = Array.FindAll<SrvRecord>(array, (SrvRecord srvRecord) => srvRecord.Port == 443 && priority == srvRecord.Priority && weight == srvRecord.Weight);
			int num = 0;
			length = array2.GetLength(0);
			AutoDiscoverDnsReader.DnsReaderTracer.TraceDebug<int, int>(0L, "Found {0} SRV records with priority = {1}.", length, array2[0].Priority);
			if (length > 1)
			{
				num = AutoDiscoverDnsReader.randomIndexSelector.Next(length);
			}
			return array2[num].TargetHost;
		}

		// Token: 0x0400014C RID: 332
		private const string AutodiscoverLegacyPath = "/autodiscover/autodiscover.xml";

		// Token: 0x0400014D RID: 333
		private const string AutodiscoverLegacyHttpsUrl = "https://{0}/autodiscover/autodiscover.xml";

		// Token: 0x0400014E RID: 334
		private const string AutoDiscoverSRV = "_autodiscover._tcp.";

		// Token: 0x0400014F RID: 335
		private const int SSLPort = 443;

		// Token: 0x04000150 RID: 336
		private static readonly Trace DnsReaderTracer = ExTraceGlobals.DnsReaderTracer;

		// Token: 0x04000151 RID: 337
		private static Random randomIndexSelector = new Random();

		// Token: 0x04000152 RID: 338
		private static Dns dns = new Dns();
	}
}
