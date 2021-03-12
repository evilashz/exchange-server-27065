using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Hygiene.Data.Domain;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000080 RID: 128
	internal class DnsLocalConfiguration : DnsConfiguration
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0001428E File Offset: 0x0001248E
		public DnsLocalConfiguration(XmlElement configNode, TracingContext traceContext) : base(configNode, traceContext, true)
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000142A4 File Offset: 0x000124A4
		protected override void InitDnsServerIps(bool useSingleDnsServer)
		{
			base.DnsServerIps = this.GetLocalIpAddress();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000142D4 File Offset: 0x000124D4
		protected override void InitSupportedZones()
		{
			DomainSession domainSession = new DomainSession();
			string internalZoneName = this.GetInternalZone();
			if (DnsConfiguration.Zones == null)
			{
				lock (this.lockObject)
				{
					if (DnsConfiguration.Zones == null)
					{
						DnsConfiguration.Zones = (from zone in domainSession.FindZoneAll()
						select zone.DomainName).ToList<string>();
					}
				}
			}
			base.AllZones = (from name in DnsConfiguration.Zones
			where !string.Equals(name, internalZoneName, StringComparison.OrdinalIgnoreCase)
			select name).ToList<string>();
			base.ZonesWithFallback = this.GetZonesWithFallback();
			base.ZonesWithoutFallback = base.AllZones.Except(base.ZonesWithFallback).ToList<string>();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000143B0 File Offset: 0x000125B0
		protected override void InitSupportedTargetServices()
		{
			this.InitDnsSupportedTargetServicesFromRegistry();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000143B8 File Offset: 0x000125B8
		protected override void InitMonitorDomain()
		{
			base.MonitorDomain = this.GetDnsMonitorDomain();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000143C6 File Offset: 0x000125C6
		protected override void InitIpV6Prefix()
		{
			base.IpV6Prefix = this.GetIpV6Prefix();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000143D4 File Offset: 0x000125D4
		private static Tuple<string, string, bool> GetTargetServiceTuple(string targetService)
		{
			string[] array = targetService.Split(new char[]
			{
				':'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 3 && (array[2] == "1" || array[2] == "0"))
			{
				bool item = array[2] == "1";
				return new Tuple<string, string, bool>(array[0], array[1], item);
			}
			throw new FormatException(string.Format("Invalid format for target service, expected='region:version:bool_SupportIpV6', actual='{0}'", targetService));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001444C File Offset: 0x0001264C
		private List<string> GetZonesWithFallback()
		{
			List<string> list = new List<string>();
			XmlElement elementFromDnsConfig = this.GetElementFromDnsConfig("/configuration/customSettings/plugins/add[@name='Database Plugin']/fallbackTargetServices");
			if (elementFromDnsConfig != null)
			{
				foreach (object obj in elementFromDnsConfig.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null)
					{
						list.Add(Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("zone"), "fallbackTargetServices.Zone"));
					}
				}
			}
			return list;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000144E0 File Offset: 0x000126E0
		private string GetDnsMonitorDomain()
		{
			XmlElement elementFromDnsConfig = this.GetElementFromDnsConfig("/configuration/customSettings/plugins/add[@name='Database Plugin']/MonitorDomain");
			if (elementFromDnsConfig == null)
			{
				throw new FormatException("Monitor Domain element missing");
			}
			return Utils.CheckNullOrWhiteSpace(elementFromDnsConfig.GetAttribute("Domain"), "MonitorDomain.Domain");
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001451C File Offset: 0x0001271C
		private string GetIpV6Prefix()
		{
			XmlElement elementFromDnsConfig = this.GetElementFromDnsConfig("/configuration/customSettings/plugins/add[@name='Database Plugin']/settings/add[@name='IpV6Prefix']");
			if (elementFromDnsConfig == null)
			{
				throw new FormatException("IpV6Prefix element missing");
			}
			return Utils.CheckNullOrWhiteSpace(elementFromDnsConfig.GetAttribute("value"), "IpV6Prefix.value");
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00014558 File Offset: 0x00012758
		private XmlElement GetElementFromDnsConfig(string xPath)
		{
			string text = Path.Combine(this.GetDnsInstallPath(), "Bin\\Microsoft.Exchange.Hygiene.ServiceLocator.FfoDnsServer.exe.config");
			if (!File.Exists(text))
			{
				throw new DnsMonitorException(string.Format("DNS configuration file missing, path={0}", text), null);
			}
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.DNSTracer, base.TraceContext, "Trying to read file, file={0}, xpath={1}", text, xPath, null, "GetElementFromDnsConfig", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsLocalConfiguration.cs", 223);
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(text);
			return safeXmlDocument.DocumentElement.SelectSingleNode(xPath) as XmlElement;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000145D8 File Offset: 0x000127D8
		private string GetDnsInstallPath()
		{
			string stringValueFromRegistry = Utils.GetStringValueFromRegistry("SOFTWARE\\Microsoft\\FfoDomainNameServer\\Setup", "MsiInstallPath");
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "DnsLocalConfiguration: Found install path={0}", stringValueFromRegistry, null, "GetDnsInstallPath", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsLocalConfiguration.cs", 238);
			return stringValueFromRegistry;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001461C File Offset: 0x0001281C
		private void InitDnsSupportedTargetServicesFromRegistry()
		{
			string stringValueFromRegistry = Utils.GetStringValueFromRegistry("SOFTWARE\\Microsoft\\FfoDomainNameServer\\Config", "SupportedTargetServices");
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "DnsLocalConfiguration: Found TargetServices={0}", stringValueFromRegistry, null, "InitDnsSupportedTargetServicesFromRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsLocalConfiguration.cs", 250);
			base.IpV4TargetServices = new List<Tuple<string, string>>();
			base.IpV6TargetServices = new List<Tuple<string, string>>();
			foreach (string targetService in stringValueFromRegistry.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				Tuple<string, string, bool> targetServiceTuple = DnsLocalConfiguration.GetTargetServiceTuple(targetService);
				Tuple<string, string> item = new Tuple<string, string>(targetServiceTuple.Item1, targetServiceTuple.Item2);
				base.IpV4TargetServices.Add(item);
				if (targetServiceTuple.Item3)
				{
					base.IpV6TargetServices.Add(item);
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000146E0 File Offset: 0x000128E0
		private string GetInternalZone()
		{
			string stringValueFromRegistry = Utils.GetStringValueFromRegistry("SOFTWARE\\Microsoft\\FfoDomainNameServer\\Config", "InternalZoneName");
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "DnsLocalConfiguration: Found internal zone={0}", stringValueFromRegistry, null, "GetInternalZone", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsLocalConfiguration.cs", 277);
			return stringValueFromRegistry;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00014724 File Offset: 0x00012924
		private List<IPAddress> GetLocalIpAddress()
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, base.TraceContext, "DnsLocalConfiguration: Trying to get local Ip Address", null, "GetLocalIpAddress", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsLocalConfiguration.cs", 288);
			List<IPAddress> list = new List<IPAddress>();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					foreach (UnicastIPAddressInformation unicastIPAddressInformation in ipproperties.UnicastAddresses)
					{
						if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							list.Add(unicastIPAddressInformation.Address);
							break;
						}
					}
				}
			}
			if (list.Count == 0)
			{
				throw new DnsMonitorException("DnsLocalConfiguration: Could not retrive the local IpAddress for DNS", null);
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "DnsLocalConfiguration: LocalIpAddress={0}", string.Join<IPAddress>(",", list), null, "GetLocalIpAddress", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsLocalConfiguration.cs", 313);
			return list;
		}

		// Token: 0x040001E7 RID: 487
		private const string DnsConfigRegistryKeyPath = "SOFTWARE\\Microsoft\\FfoDomainNameServer\\Config";

		// Token: 0x040001E8 RID: 488
		private const string DnsSetupRegistryKeyPath = "SOFTWARE\\Microsoft\\FfoDomainNameServer\\Setup";

		// Token: 0x040001E9 RID: 489
		private const string DnsConfigRelativePath = "Bin\\Microsoft.Exchange.Hygiene.ServiceLocator.FfoDnsServer.exe.config";

		// Token: 0x040001EA RID: 490
		private const string DnsInstallPathNameKey = "MsiInstallPath";

		// Token: 0x040001EB RID: 491
		private const string DnsInternalZoneNameKey = "InternalZoneName";

		// Token: 0x040001EC RID: 492
		private const string DnsSupportedTargetServices = "SupportedTargetServices";

		// Token: 0x040001ED RID: 493
		private object lockObject = new object();
	}
}
