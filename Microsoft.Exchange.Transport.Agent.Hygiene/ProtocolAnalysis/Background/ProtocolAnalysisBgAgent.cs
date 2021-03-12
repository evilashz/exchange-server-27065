using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProtocolAnalysisBg;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Agent.Hygiene;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000050 RID: 80
	internal class ProtocolAnalysisBgAgent
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		public ProtocolAnalysisBgAgent(SmtpServer server)
		{
			this.server = server;
			this.hostIPAddress = new IPAddress[0];
			this.hostAddressIndex = 0;
			this.proxyIPAddress = new IPAddress[0];
			this.proxyAddressIndex = 0;
			this.pendingProxyIPQuery = false;
			ProtocolAnalysisBgAgent.LoadConfiguration();
			ConfigurationAccess.HandleConfigChangeEvent += this.OnConfigChanged;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000FE28 File Offset: 0x0000E028
		private static string RemoveWildCards(string domain)
		{
			if (string.IsNullOrEmpty(domain) || (domain.Length == 1 && domain[0] == '*'))
			{
				return null;
			}
			if (domain.StartsWith("*.", StringComparison.OrdinalIgnoreCase))
			{
				return domain.Substring("*.".Length);
			}
			return domain;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000FE68 File Offset: 0x0000E068
		private static void LoadConfiguration()
		{
			ProtocolAnalysisBgAgent.Settings = ConfigurationAccess.ConfigSettings;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000FE74 File Offset: 0x0000E074
		public void Shutdown()
		{
			this.OnShutdownHandler();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public void Startup()
		{
			this.OnStartupHandler();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000FE84 File Offset: 0x0000E084
		private void OnConfigChanged(object o, ConfigChangedEventArgs e)
		{
			if (e != null && e.Fields != null)
			{
				return;
			}
			ProtocolAnalysisBgAgent.Settings = ConfigurationAccess.ConfigSettings;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000FE9C File Offset: 0x0000E09C
		private void OnStartupHandler()
		{
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "OnStartupHandler");
			ProtocolAnalysisBgAgent.proxyDetectors = new ProxyType[7];
			ProtocolAnalysisBgAgent.proxyDetectors[0] = ProxyType.Wingate;
			ProtocolAnalysisBgAgent.proxyDetectors[1] = ProxyType.Socks4;
			ProtocolAnalysisBgAgent.proxyDetectors[2] = ProxyType.Socks5;
			ProtocolAnalysisBgAgent.proxyDetectors[3] = ProxyType.HttpConnect;
			ProtocolAnalysisBgAgent.proxyDetectors[4] = ProxyType.HttpPost;
			ProtocolAnalysisBgAgent.proxyDetectors[5] = ProxyType.Cisco;
			ProtocolAnalysisBgAgent.proxyDetectors[6] = ProxyType.Telnet;
			this.hostDnsResolutionTimer = new Timer(new TimerCallback(this.RetrieveServerIPs), null, TimeSpan.Zero, new TimeSpan(1, 0, 0));
			this.proxyDnsResolutionTimer = new Timer(new TimerCallback(this.RetrieveProxyIPs), null, TimeSpan.Zero, new TimeSpan(1, 0, 0));
			this.tablePurgeTimer = new Timer(new TimerCallback(this.PurgeTableTimerHandler), null, new TimeSpan(0, 0, 10), new TimeSpan(1, 0, 0));
			this.poller = new Thread(new ThreadStart(this.PollerThreadProc));
			this.poller.Start();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000FF98 File Offset: 0x0000E198
		private void OnShutdownHandler()
		{
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "OnShutdownHandler");
			try
			{
				ProtocolAnalysisBgAgent.ShutDown = true;
				if (this.proxyDnsCompletionEvent != null)
				{
					this.proxyDnsCompletionEvent.Set();
				}
				if (this.hostDnsCompletionEvent != null)
				{
					this.hostDnsCompletionEvent.Set();
				}
				if (this.poller != null)
				{
					this.poller.Join();
				}
				this.WaitForAllDetections(5U);
				ConfigurationAccess.Unsubscribe();
				Database.Detach();
			}
			finally
			{
				if (this.hostDnsResolutionTimer != null)
				{
					this.hostDnsResolutionTimer.Dispose();
				}
				this.hostDnsResolutionTimer = null;
				if (this.proxyDnsResolutionTimer != null)
				{
					this.proxyDnsResolutionTimer.Dispose();
				}
				this.proxyDnsResolutionTimer = null;
				if (this.tablePurgeTimer != null)
				{
					this.tablePurgeTimer.Dispose();
				}
				this.tablePurgeTimer = null;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00010070 File Offset: 0x0000E270
		private void RetrieveServerIPs(object state)
		{
			if (ProtocolAnalysisAgentFactory.SrlCalculationDisabled)
			{
				return;
			}
			try
			{
				List<string> list = new List<string>();
				foreach (Microsoft.Exchange.Data.Transport.AcceptedDomain acceptedDomain in this.server.AcceptedDomains)
				{
					if (acceptedDomain.IsInCorporation)
					{
						string text = ProtocolAnalysisBgAgent.RemoveWildCards(acceptedDomain.NameSpecification);
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(text);
						}
					}
				}
				PaBgSmtpMxDns paBgSmtpMxDns = new PaBgSmtpMxDns(ExTraceGlobals.OnDnsQueryTracer, new PaBgSmtpMxDns.EndMxDnsResolutionCallback(this.EndHostIPResolution));
				paBgSmtpMxDns.BeginSmtpMxQueries(list);
			}
			catch (ArgumentException ex)
			{
				ProtocolAnalysisAgentFactory.LogSrlCalculationDisabled();
				ExTraceGlobals.OnDnsQueryTracer.TraceError<string>((long)this.GetHashCode(), "Failed to retrieve sender IPs. Error: {0}", ex.Message);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00010144 File Offset: 0x0000E344
		private void RetrieveProxyIPs(object state)
		{
			if (this.pendingProxyIPQuery || ProtocolAnalysisBgAgent.Settings.ProxyServerType == ProxyType.None)
			{
				return;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(ProtocolAnalysisBgAgent.Settings.ProxyServerName, out ipaddress))
			{
				lock (this.syncObject)
				{
					this.proxyIPAddress = new IPAddress[1];
					this.proxyIPAddress[0] = ipaddress;
					this.proxyAddressIndex = 0;
					if (this.proxyDnsResolutionTimer != null)
					{
						this.proxyDnsResolutionTimer.Change(-1, -1);
					}
				}
				return;
			}
			try
			{
				if (this.proxyDnsResolutionTimer != null)
				{
					this.proxyDnsResolutionTimer.Change(new TimeSpan(0, 5, 0), new TimeSpan(0, 5, 0));
				}
				this.pendingProxyIPQuery = true;
				ExTraceGlobals.OnDnsQueryTracer.TraceDebug<string>((long)this.GetHashCode(), "Call Dns.BeginResolveToAddresses:{0}", ProtocolAnalysisBgAgent.Settings.ProxyServerName);
				TransportFacades.Dns.BeginResolveToAddresses(ProtocolAnalysisBgAgent.Settings.ProxyServerName, AddressFamily.InterNetwork, new AsyncCallback(this.EndProxyIPResolution), null);
				this.proxyDnsCompletionEvent.Reset();
			}
			catch
			{
				this.pendingProxyIPQuery = false;
				throw;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001026C File Offset: 0x0000E46C
		private void EndHostIPResolution(DnsStatus status, TargetHost[] targetHosts)
		{
			if (this.hostDnsCompletionEvent != null)
			{
				this.hostDnsCompletionEvent.Set();
			}
			if (targetHosts == null || status != DnsStatus.Success)
			{
				return;
			}
			int num = 0;
			lock (this.syncObject)
			{
				for (int i = 0; i < targetHosts.Length; i++)
				{
					num += targetHosts[i].IPAddresses.Count;
				}
				this.hostIPAddress = new IPAddress[num];
				num = 0;
				for (int i = 0; i < targetHosts.Length; i++)
				{
					for (int j = 0; j < targetHosts[i].IPAddresses.Count; j++)
					{
						this.hostIPAddress[num++] = targetHosts[i].IPAddresses[j];
					}
				}
				this.hostAddressIndex = 0;
			}
			ExTraceGlobals.OnDnsQueryTracer.TraceDebug<int>((long)this.GetHashCode(), "Query sender IPs returned {0} IP addresses", num);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00010350 File Offset: 0x0000E550
		private void EndProxyIPResolution(IAsyncResult ar)
		{
			IPAddress[] array;
			DnsStatus dnsStatus = Dns.EndResolveToAddresses(ar, out array);
			this.pendingProxyIPQuery = false;
			if (dnsStatus == DnsStatus.Success)
			{
				lock (this.syncObject)
				{
					this.proxyIPAddress = array;
					this.proxyAddressIndex = 0;
					this.proxyDnsResolutionTimer.Change(new TimeSpan(1, 0, 0), new TimeSpan(1, 0, 0));
				}
				ExTraceGlobals.OnDnsQueryTracer.TraceDebug<int>((long)this.GetHashCode(), "Query proxy IPs returned {0} IP addresses", this.proxyIPAddress.Length);
			}
			else
			{
				ExTraceGlobals.OnDnsQueryTracer.TraceDebug((long)this.GetHashCode(), "Query proxy IPs failed.");
			}
			if (this.proxyDnsCompletionEvent != null)
			{
				this.proxyDnsCompletionEvent.Set();
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00010414 File Offset: 0x0000E614
		public static IEnumerator GetProxyEnumerator()
		{
			return ProtocolAnalysisBgAgent.proxyDetectors.GetEnumerator();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010420 File Offset: 0x0000E620
		public static ushort[] GetProxyPortList(ProxyType proxyType)
		{
			MultiValuedProperty<int> multiValuedProperty;
			switch (proxyType)
			{
			case ProxyType.Socks4:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.Socks4Ports;
				break;
			case ProxyType.Socks5:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.Socks5Ports;
				break;
			case ProxyType.HttpConnect:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.HttpConnectPorts;
				break;
			case ProxyType.HttpPost:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.HttpPostPorts;
				break;
			case ProxyType.Telnet:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.TelnetPorts;
				break;
			case ProxyType.Cisco:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.CiscoPorts;
				break;
			case ProxyType.Wingate:
				multiValuedProperty = ProtocolAnalysisBgAgent.Settings.WingatePorts;
				break;
			default:
				throw new LocalizedException(AgentStrings.InvalidProxyType);
			}
			int[] array = (int[])Array.CreateInstance(typeof(int), multiValuedProperty.Count);
			ushort[] array2 = (ushort[])Array.CreateInstance(typeof(ushort), multiValuedProperty.Count);
			int num = 0;
			multiValuedProperty.CopyTo(array, 0);
			foreach (int num2 in array)
			{
				array2[num++] = (ushort)num2;
			}
			return array2;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00010528 File Offset: 0x0000E728
		public void PollerThreadProc()
		{
			this.hostDnsCompletionEvent.WaitOne();
			this.hostDnsCompletionEvent.Reset();
			this.proxyDnsCompletionEvent.WaitOne();
			this.proxyDnsCompletionEvent.Reset();
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Agent's worker thread started");
			StsWorkItem stsWorkItem = new StsWorkItem();
			while (!ProtocolAnalysisBgAgent.ShutDown)
			{
				if ((long)ProtocolAnalysisBgAgent.NumDetections + ProtocolAnalysisBgAgent.NumQueries < (long)ProtocolAnalysisBgAgent.Settings.MaxPendingOperations && stsWorkItem.Poll())
				{
					ExTraceGlobals.AgentTracer.TraceDebug<IPAddress, WorkItemType>((long)this.GetHashCode(), "Load work item sender: {0}, type: {1}", stsWorkItem.SenderAddress, stsWorkItem.WorkType);
					switch (stsWorkItem.WorkType)
					{
					case WorkItemType.OpenProxyDetection:
						if (ProtocolAnalysisBgAgent.Settings.ProxyServerType != ProxyType.None)
						{
							stsWorkItem.StartOpenProxyTest(this.GetRandomHostEndpoint(), this.GetRandomProxyIPAddress(), (int)((ushort)ProtocolAnalysisBgAgent.Settings.ProxyServerPort), ProtocolAnalysisBgAgent.Settings.ProxyServerType, new NetworkCredential());
						}
						else
						{
							stsWorkItem.StartOpenProxyTest(this.GetRandomHostEndpoint());
						}
						break;
					case WorkItemType.ReverseDnsQuery:
						stsWorkItem.StartReverseDnsQuery();
						break;
					case WorkItemType.BlockSender:
						stsWorkItem.BlockSender(this.server);
						break;
					default:
						ExTraceGlobals.AgentTracer.TraceError<WorkItemType>((long)this.GetHashCode(), "Invalid work type: {0}", stsWorkItem.WorkType);
						break;
					}
				}
				else
				{
					Thread.Sleep(2000);
				}
			}
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Agent's worker thread started");
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00010694 File Offset: 0x0000E894
		private void PurgeTableTimerHandler(object state)
		{
			Database.PurgeTable(new TimeSpan(ProtocolAnalysisBgAgent.Settings.TablePurgeInterval, 0, 0), new TimeSpan(ProtocolAnalysisBgAgent.Settings.TablePurgeInterval, 0, 0), ExTraceGlobals.DatabaseTracer);
			ExTraceGlobals.DatabaseTracer.TraceDebug((long)this.GetHashCode(), "Purge database records for PA and OP");
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000106E3 File Offset: 0x0000E8E3
		private void WaitForAllDetections(uint maxWaitSeconds)
		{
			while (ProtocolAnalysisBgAgent.NumDetections > 0 || ProtocolAnalysisBgAgent.NumQueries > 0L || this.pendingProxyIPQuery)
			{
				if (maxWaitSeconds == 0U)
				{
					return;
				}
				maxWaitSeconds -= 1U;
				Thread.Sleep(1000);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010714 File Offset: 0x0000E914
		private IPEndPoint GetRandomHostEndpoint()
		{
			IPAddress address = IPAddress.Any;
			lock (this.syncObject)
			{
				if (this.hostIPAddress.Length > 0)
				{
					if (this.hostAddressIndex >= this.hostIPAddress.Length)
					{
						this.hostAddressIndex = 0;
					}
					address = this.hostIPAddress[this.hostAddressIndex++];
				}
			}
			if (this.hostIPAddress.Length <= 0)
			{
				return null;
			}
			return new IPEndPoint(address, 25);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000107A4 File Offset: 0x0000E9A4
		private IPAddress GetRandomProxyIPAddress()
		{
			IPAddress result = IPAddress.Any;
			lock (this.syncObject)
			{
				if (this.proxyIPAddress.Length > 0)
				{
					if (this.proxyAddressIndex >= this.proxyIPAddress.Length)
					{
						this.proxyAddressIndex = 0;
					}
					result = this.proxyIPAddress[this.proxyAddressIndex++];
				}
			}
			return result;
		}

		// Token: 0x040001B7 RID: 439
		internal const int DetectionTimeoutInSeconds = 10;

		// Token: 0x040001B8 RID: 440
		internal const string SmtpGreeting = "220 ";

		// Token: 0x040001B9 RID: 441
		private const string WildCardPrefix = "*.";

		// Token: 0x040001BA RID: 442
		private const char WildCardChar = '*';

		// Token: 0x040001BB RID: 443
		private const int SmtpPort = 25;

		// Token: 0x040001BC RID: 444
		private const int DnsResolutionIntervalInHours = 1;

		// Token: 0x040001BD RID: 445
		private const int PurgeTableIntervalInHours = 1;

		// Token: 0x040001BE RID: 446
		private const int DnsRetryIntervalInMinutes = 5;

		// Token: 0x040001BF RID: 447
		private const int PollIntervalInSeconds = 2;

		// Token: 0x040001C0 RID: 448
		internal static ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.FactoryTracer.Category, "MSExchange Antispam");

		// Token: 0x040001C1 RID: 449
		internal static SenderReputationConfig Settings;

		// Token: 0x040001C2 RID: 450
		internal static bool ShutDown;

		// Token: 0x040001C3 RID: 451
		private static ProxyType[] proxyDetectors;

		// Token: 0x040001C4 RID: 452
		private readonly object syncObject = new object();

		// Token: 0x040001C5 RID: 453
		private IPAddress[] hostIPAddress;

		// Token: 0x040001C6 RID: 454
		private int hostAddressIndex;

		// Token: 0x040001C7 RID: 455
		private Timer hostDnsResolutionTimer;

		// Token: 0x040001C8 RID: 456
		private ManualResetEvent hostDnsCompletionEvent = new ManualResetEvent(true);

		// Token: 0x040001C9 RID: 457
		private IPAddress[] proxyIPAddress;

		// Token: 0x040001CA RID: 458
		private int proxyAddressIndex;

		// Token: 0x040001CB RID: 459
		private Timer proxyDnsResolutionTimer;

		// Token: 0x040001CC RID: 460
		private bool pendingProxyIPQuery;

		// Token: 0x040001CD RID: 461
		private ManualResetEvent proxyDnsCompletionEvent = new ManualResetEvent(true);

		// Token: 0x040001CE RID: 462
		private Timer tablePurgeTimer;

		// Token: 0x040001CF RID: 463
		private Thread poller;

		// Token: 0x040001D0 RID: 464
		private SmtpServer server;

		// Token: 0x040001D1 RID: 465
		public static int NumDetections;

		// Token: 0x040001D2 RID: 466
		public static long NumQueries;

		// Token: 0x040001D3 RID: 467
		public static long NumSockets;

		// Token: 0x02000051 RID: 81
		internal sealed class PerformanceCounters
		{
			// Token: 0x06000270 RID: 624 RVA: 0x0001083C File Offset: 0x0000EA3C
			public static void PositiveOpenProxy(ProxyType type)
			{
				switch (type)
				{
				case ProxyType.Socks4:
					ProtocolAnalysisBgPerfCounters.Socks4Proxy.Increment();
					return;
				case ProxyType.Socks5:
					ProtocolAnalysisBgPerfCounters.Socks5Proxy.Increment();
					return;
				case ProxyType.HttpConnect:
					ProtocolAnalysisBgPerfCounters.HttpConnectProxy.Increment();
					return;
				case ProxyType.HttpPost:
					ProtocolAnalysisBgPerfCounters.HttpPostProxy.Increment();
					return;
				case ProxyType.Telnet:
					ProtocolAnalysisBgPerfCounters.TelnetProxy.Increment();
					return;
				case ProxyType.Cisco:
					ProtocolAnalysisBgPerfCounters.CiscoProxy.Increment();
					return;
				case ProxyType.Wingate:
					ProtocolAnalysisBgPerfCounters.WingateProxy.Increment();
					return;
				default:
					return;
				}
			}

			// Token: 0x06000271 RID: 625 RVA: 0x000108C3 File Offset: 0x0000EAC3
			public static void NegativeOpenProxy()
			{
				ProtocolAnalysisBgPerfCounters.NegativeOpenProxy.Increment();
			}

			// Token: 0x06000272 RID: 626 RVA: 0x000108D0 File Offset: 0x0000EAD0
			public static void UnknownOpenProxy()
			{
				ProtocolAnalysisBgPerfCounters.UnknownOpenProxy.Increment();
			}

			// Token: 0x06000273 RID: 627 RVA: 0x000108DD File Offset: 0x0000EADD
			public static void TotalOpenProxy()
			{
				ProtocolAnalysisBgPerfCounters.TotalOpenProxy.Increment();
			}

			// Token: 0x06000274 RID: 628 RVA: 0x000108EA File Offset: 0x0000EAEA
			public static void ReverseDnsSucc()
			{
				ProtocolAnalysisBgPerfCounters.ReverseDnsSucc.Increment();
			}

			// Token: 0x06000275 RID: 629 RVA: 0x000108F7 File Offset: 0x0000EAF7
			public static void ReverseDnsFail()
			{
				ProtocolAnalysisBgPerfCounters.ReverseDnsFail.Increment();
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00010904 File Offset: 0x0000EB04
			public static void BlockSender()
			{
				ProtocolAnalysisBgPerfCounters.BlockSender.Increment();
			}

			// Token: 0x06000277 RID: 631 RVA: 0x00010914 File Offset: 0x0000EB14
			public static void RemoveCounters()
			{
				ProtocolAnalysisBgPerfCounters.Socks4Proxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.Socks5Proxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.HttpConnectProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.HttpPostProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.TelnetProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.CiscoProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.WingateProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.NegativeOpenProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.UnknownOpenProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.TotalOpenProxy.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.ReverseDnsSucc.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.ReverseDnsFail.RawValue = 0L;
				ProtocolAnalysisBgPerfCounters.BlockSender.RawValue = 0L;
			}
		}
	}
}
