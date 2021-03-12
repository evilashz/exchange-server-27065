using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x0200007F RID: 127
	internal abstract class DnsConfiguration
	{
		// Token: 0x06000336 RID: 822 RVA: 0x000134DB File Offset: 0x000116DB
		protected DnsConfiguration(XmlElement configNode, TracingContext traceContext, bool isLocal)
		{
			this.IsLocal = isLocal;
			this.TraceContext = traceContext;
			this.LoadConfig(configNode);
			this.Validate();
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00013509 File Offset: 0x00011709
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00013511 File Offset: 0x00011711
		public List<IPAddress> DnsServerIps { get; protected set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001351A File Offset: 0x0001171A
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00013522 File Offset: 0x00011722
		public string IpV6Prefix { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0001352B File Offset: 0x0001172B
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00013532 File Offset: 0x00011732
		protected static List<string> Zones { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001353A File Offset: 0x0001173A
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00013542 File Offset: 0x00011742
		protected TracingContext TraceContext { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001354B File Offset: 0x0001174B
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00013553 File Offset: 0x00011753
		protected bool IsLocal { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0001355C File Offset: 0x0001175C
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00013564 File Offset: 0x00011764
		protected List<string> AllZones { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0001356D File Offset: 0x0001176D
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00013575 File Offset: 0x00011775
		protected List<string> ZonesWithFallback { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0001357E File Offset: 0x0001177E
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00013586 File Offset: 0x00011786
		protected List<string> ZonesWithoutFallback { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0001358F File Offset: 0x0001178F
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00013597 File Offset: 0x00011797
		protected List<Tuple<string, string>> IpV6TargetServices { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000135A0 File Offset: 0x000117A0
		// (set) Token: 0x0600034A RID: 842 RVA: 0x000135A8 File Offset: 0x000117A8
		protected List<Tuple<string, string>> IpV4TargetServices { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000135B1 File Offset: 0x000117B1
		// (set) Token: 0x0600034C RID: 844 RVA: 0x000135B9 File Offset: 0x000117B9
		protected string MonitorDomain { get; set; }

		// Token: 0x0600034D RID: 845 RVA: 0x000135C4 File Offset: 0x000117C4
		public static DnsConfiguration CreateInstance(XmlElement configNode, TracingContext traceContext, bool isLocal)
		{
			if (isLocal)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, traceContext, "DnsConfiguration: Found IsLocal element creating DnsLocalConfiguration", null, "CreateInstance", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 113);
				Type type = Assembly.GetExecutingAssembly().GetType("Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus.DnsLocalConfiguration");
				if (type == null)
				{
					throw new InvalidOperationException("Could not find Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus.DnsLocalConfiguration");
				}
				return Activator.CreateInstance(type, new object[]
				{
					configNode,
					traceContext
				}) as DnsConfiguration;
			}
			else
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, traceContext, "DnsConfiguration: Did not find IsLocal element creating DnsRemoteConfiguration", null, "CreateInstance", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 124);
				Type type2 = Assembly.GetExecutingAssembly().GetType("Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus.DnsRemoteConfiguration");
				if (type2 == null)
				{
					throw new InvalidOperationException("Could not find Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus.DnsRemoteConfiguration");
				}
				return Activator.CreateInstance(type2, new object[]
				{
					configNode,
					traceContext
				}) as DnsConfiguration;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000136C0 File Offset: 0x000118C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ZonesWithFallback:{0} ", string.Join(",", this.ZonesWithFallback));
			stringBuilder.AppendFormat("ZonesWithoutFallback:{0} ", string.Join(",", this.ZonesWithoutFallback));
			stringBuilder.AppendFormat("IpV4TargetServices:{0} ", string.Join(",", from t in this.IpV4TargetServices
			select t.Item1 + "-" + t.Item2));
			stringBuilder.AppendFormat("IpV6TargetServices:{0} ", string.Join(",", from t in this.IpV6TargetServices
			select t.Item1 + "-" + t.Item2));
			stringBuilder.AppendFormat("DnsServerIps:{0} ", string.Join<IPAddress>(",", this.DnsServerIps));
			stringBuilder.AppendFormat("MonitorDomain:{0}", this.MonitorDomain);
			return stringBuilder.ToString();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000137BB File Offset: 0x000119BB
		public bool IsSupportedZone(string zoneName)
		{
			return this.AllZones.Contains(zoneName, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000137D0 File Offset: 0x000119D0
		public List<string> GetDomainsToLookup(DomainSelection selectionFilter)
		{
			WTFDiagnostics.TraceInformation<DomainSelection>(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Trying to get Domains for filter={0}", selectionFilter, null, "GetDomainsToLookup", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 169);
			List<string> list = new List<string>();
			switch (selectionFilter)
			{
			case DomainSelection.AnyTargetServiceAnyZone:
			{
				Tuple<string, string> randomItem = this.GetRandomItem<Tuple<string, string>>(this.IpV4TargetServices);
				list.Add(string.Format("{0}--{1}--{2}.{3}", new object[]
				{
					this.MonitorDomain,
					randomItem.Item1,
					randomItem.Item2,
					this.GetRandomItem<string>(this.AllZones)
				}));
				goto IL_3C6;
			}
			case DomainSelection.AllTargetServicesAnyZone:
				using (List<Tuple<string, string>>.Enumerator enumerator = this.IpV4TargetServices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Tuple<string, string> tuple = enumerator.Current;
						list.Add(string.Format("{0}--{1}--{2}.{3}", new object[]
						{
							this.MonitorDomain,
							tuple.Item1,
							tuple.Item2,
							this.GetRandomItem<string>(this.AllZones)
						}));
					}
					goto IL_3C6;
				}
				break;
			case DomainSelection.AnyTargetServiceAllZones:
				break;
			case DomainSelection.Ipv6AnyTargetServiceAnyZone:
				goto IL_1BB;
			case DomainSelection.Ipv6AllTargetServicesAnyZone:
				using (List<Tuple<string, string>>.Enumerator enumerator2 = this.IpV6TargetServices.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Tuple<string, string> tuple2 = enumerator2.Current;
						list.Add(string.Format("{0}--{1}--{2}.{3}", new object[]
						{
							this.IpV6Prefix + this.MonitorDomain,
							tuple2.Item1,
							tuple2.Item2,
							this.GetRandomItem<string>(this.AllZones)
						}));
					}
					goto IL_3C6;
				}
				goto IL_2AC;
			case DomainSelection.Ipv6AnyTargetServiceAllZones:
				goto IL_2AC;
			case DomainSelection.AnyZone:
				goto IL_339;
			case DomainSelection.AllZones:
				list.AddRange(this.AllZones);
				goto IL_3C6;
			case DomainSelection.InvalidDomainWithFallback:
				if (this.ZonesWithFallback.Count > 0)
				{
					list.Add(string.Format("InvalidDomainWithFallback.{0}", this.GetRandomItem<string>(this.ZonesWithFallback)));
					goto IL_3C6;
				}
				goto IL_3C6;
			case DomainSelection.InvalidDomainWithoutFallback:
				if (this.ZonesWithoutFallback.Count > 0)
				{
					list.Add(string.Format("InvalidDomainWithoutFallback.{0}", this.GetRandomItem<string>(this.ZonesWithoutFallback)));
					goto IL_3C6;
				}
				goto IL_3C6;
			case DomainSelection.InvalidZone:
				list.Add("InvalidZone.com");
				goto IL_3C6;
			default:
				throw new NotImplementedException();
			}
			Tuple<string, string> randomItem2 = this.GetRandomItem<Tuple<string, string>>(this.IpV4TargetServices);
			using (List<string>.Enumerator enumerator3 = this.AllZones.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					string text = enumerator3.Current;
					list.Add(string.Format("{0}--{1}--{2}.{3}", new object[]
					{
						this.MonitorDomain,
						randomItem2.Item1,
						randomItem2.Item2,
						text
					}));
				}
				goto IL_3C6;
			}
			IL_1BB:
			Tuple<string, string> randomItem3 = this.GetRandomItem<Tuple<string, string>>(this.IpV6TargetServices);
			list.Add(string.Format("{0}--{1}--{2}.{3}", new object[]
			{
				this.IpV6Prefix + this.MonitorDomain,
				randomItem3.Item1,
				randomItem3.Item2,
				this.GetRandomItem<string>(this.AllZones)
			}));
			goto IL_3C6;
			IL_2AC:
			Tuple<string, string> randomItem4 = this.GetRandomItem<Tuple<string, string>>(this.IpV6TargetServices);
			using (List<string>.Enumerator enumerator4 = this.AllZones.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					string text2 = enumerator4.Current;
					list.Add(string.Format("{0}--{1}--{2}.{3}", new object[]
					{
						this.IpV6Prefix + this.MonitorDomain,
						randomItem4.Item1,
						randomItem4.Item2,
						text2
					}));
				}
				goto IL_3C6;
			}
			IL_339:
			list.Add(this.GetRandomItem<string>(this.AllZones));
			IL_3C6:
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Domains identified={0}", string.Join(",", list), null, "GetDomainsToLookup", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 278);
			return list;
		}

		// Token: 0x06000351 RID: 849
		protected abstract void InitDnsServerIps(bool useSingleDnsServer);

		// Token: 0x06000352 RID: 850
		protected abstract void InitSupportedZones();

		// Token: 0x06000353 RID: 851
		protected abstract void InitSupportedTargetServices();

		// Token: 0x06000354 RID: 852
		protected abstract void InitMonitorDomain();

		// Token: 0x06000355 RID: 853
		protected abstract void InitIpV6Prefix();

		// Token: 0x06000356 RID: 854 RVA: 0x00013C08 File Offset: 0x00011E08
		private T GetRandomItem<T>(List<T> items)
		{
			int index = this.randomGenerator.Next(items.Count);
			return items[index];
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00013C2E File Offset: 0x00011E2E
		private void LoadConfig(XmlElement configNode)
		{
			this.LoadSupportedZones(configNode);
			this.LoadSupportedTargetServices(configNode);
			this.LoadMonitorDomain(configNode);
			this.LoadIpV6Prefix(configNode);
			this.LoadDnsServerIps(configNode);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00013C54 File Offset: 0x00011E54
		private void LoadDnsServerIps(XmlElement configNode)
		{
			bool flag = false;
			XmlElement xmlElement = null;
			if (configNode != null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Parsing /DnsServerIps", null, "LoadDnsServerIps", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 345);
				flag = Utils.GetBoolean(configNode.GetAttribute("UseSingleDnsServer"), "DnsConfiguration.UseSingleDnsServer");
				xmlElement = (configNode.SelectSingleNode("DnsServerIps") as XmlElement);
			}
			WTFDiagnostics.TraceInformation<bool>(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: DnsConfiguration.UseSingleDnsServer={0}", flag, null, "LoadDnsServerIps", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 350);
			if (xmlElement == null || Utils.GetBoolean(xmlElement.GetAttribute("AutoDetect"), "DnsServerIps.AutoDetect"))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: detecting DnsServerIps", null, "LoadDnsServerIps", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 356);
				this.InitDnsServerIps(flag);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: reading DnsServerIps", null, "LoadDnsServerIps", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 363);
			this.DnsServerIps = new List<IPAddress>();
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement2 = xmlNode as XmlElement;
				if (xmlElement2 != null)
				{
					string ipString = Utils.CheckNullOrWhiteSpace(xmlElement2.GetAttribute("Address"), "DnsServerIps/Ip.Address");
					this.DnsServerIps.Add(IPAddress.Parse(ipString));
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00013DD0 File Offset: 0x00011FD0
		private void LoadSupportedZones(XmlElement configNode)
		{
			XmlElement xmlElement = null;
			if (configNode != null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Parsing /SupportedZones", null, "LoadSupportedZones", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 391);
				xmlElement = (configNode.SelectSingleNode("SupportedZones") as XmlElement);
			}
			if (xmlElement == null || Utils.GetBoolean(xmlElement.GetAttribute("AutoDetect"), "SupportedZones.AutoDetect"))
			{
				this.InitSupportedZones();
				return;
			}
			this.ZonesWithFallback = new List<string>();
			this.ZonesWithoutFallback = new List<string>();
			this.AllZones = new List<string>();
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement2 = xmlNode as XmlElement;
				if (xmlElement2 != null)
				{
					string item = Utils.CheckNullOrWhiteSpace(xmlElement2.GetAttribute("Name"), "SupportedZones/Zone.Name");
					bool boolean = Utils.GetBoolean(xmlElement2.GetAttribute("HasFallback"), "SupportedZones/Zone.HasFallback");
					if (boolean)
					{
						this.ZonesWithFallback.Add(item);
					}
					else
					{
						this.ZonesWithoutFallback.Add(item);
					}
					this.AllZones.Add(item);
				}
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00013F08 File Offset: 0x00012108
		private void LoadSupportedTargetServices(XmlElement configNode)
		{
			XmlElement xmlElement = null;
			XmlElement xmlElement2 = null;
			if (configNode != null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Parsing /SupportedTargetServices", null, "LoadSupportedTargetServices", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 446);
				xmlElement = (configNode.SelectSingleNode("SupportedTargetServices") as XmlElement);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Parsing /IpV4OnlyVersions", null, "LoadSupportedTargetServices", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 449);
				xmlElement2 = (configNode.SelectSingleNode("IpV4OnlyVersions") as XmlElement);
			}
			if (xmlElement == null || Utils.GetBoolean(xmlElement.GetAttribute("AutoDetect"), "SupportedTargetServices.AutoDetect"))
			{
				this.InitSupportedTargetServices();
				return;
			}
			HashSet<string> hashSet = new HashSet<string>();
			if (xmlElement2 != null)
			{
				foreach (object obj in xmlElement2.SelectNodes("Version"))
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement3 = xmlNode as XmlElement;
					if (xmlElement3 != null)
					{
						hashSet.Add(Utils.CheckNullOrWhiteSpace(xmlElement3.GetAttribute("Value"), "IpV4OnlyVersions/Version.Value"));
					}
				}
			}
			this.IpV6TargetServices = new List<Tuple<string, string>>();
			this.IpV4TargetServices = new List<Tuple<string, string>>();
			foreach (object obj2 in xmlElement.SelectNodes("TargetService"))
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				XmlElement xmlElement4 = xmlNode2 as XmlElement;
				if (xmlElement4 != null)
				{
					Tuple<string, string> tuple = new Tuple<string, string>(Utils.CheckNullOrWhiteSpace(xmlElement4.GetAttribute("Region"), "SupportedTargetServices/TargetService.Region"), Utils.CheckNullOrWhiteSpace(xmlElement4.GetAttribute("Version"), "SupportedTargetServices/TargetService.Version"));
					this.IpV4TargetServices.Add(tuple);
					if (!hashSet.Contains(tuple.Item2))
					{
						this.IpV6TargetServices.Add(tuple);
					}
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000140FC File Offset: 0x000122FC
		private void LoadMonitorDomain(XmlElement configNode)
		{
			XmlElement xmlElement = null;
			if (configNode != null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Parsing /MonitorDomain", null, "LoadMonitorDomain", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 517);
				xmlElement = (configNode.SelectSingleNode("MonitorDomain") as XmlElement);
			}
			if (xmlElement == null || Utils.GetBoolean(xmlElement.GetAttribute("AutoDetect"), "MonitorDomain.AutoDetect"))
			{
				this.InitMonitorDomain();
				return;
			}
			this.MonitorDomain = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Name"), "MonitorDomain.Name");
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00014180 File Offset: 0x00012380
		private void LoadIpV6Prefix(XmlElement configNode)
		{
			XmlElement xmlElement = null;
			if (configNode != null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.TraceContext, "DnsConfiguration: Parsing /IpV6Prefix", null, "LoadIpV6Prefix", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsConfiguration.cs", 546);
				xmlElement = (configNode.SelectSingleNode("IpV6Prefix") as XmlElement);
			}
			if (xmlElement == null || Utils.GetBoolean(xmlElement.GetAttribute("AutoDetect"), "IpV6Prefix.AutoDetect"))
			{
				this.InitIpV6Prefix();
				return;
			}
			this.IpV6Prefix = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Value"), "IpV6Prefix.Value");
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014204 File Offset: 0x00012404
		private void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.MonitorDomain))
			{
				throw new DnsMonitorException("DnsConfiguration: Monitor domain was not found", null);
			}
			if (this.IpV4TargetServices.Count == 0)
			{
				throw new DnsMonitorException("DnsConfiguration: IPV4 Target services were not found", null);
			}
			if (this.IpV6TargetServices.Count == 0)
			{
				throw new DnsMonitorException("DnsConfiguration: IPV6 Target services were not found", null);
			}
			if (this.AllZones.Count == 0)
			{
				throw new DnsMonitorException("DnsConfiguration: Supported zones were not found", null);
			}
			if (this.DnsServerIps.Count == 0)
			{
				throw new DnsMonitorException("DnsConfiguration: DNS++ server IPs were not found", null);
			}
		}

		// Token: 0x040001D8 RID: 472
		private const string DomainKeyFormat = "{0}--{1}--{2}.{3}";

		// Token: 0x040001D9 RID: 473
		private Random randomGenerator = new Random();
	}
}
