using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000086 RID: 134
	internal class DnsRemoteConfiguration : DnsConfiguration
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0001577C File Offset: 0x0001397C
		public DnsRemoteConfiguration(XmlElement configNode, TracingContext traceContext) : base(configNode, traceContext, false)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00015788 File Offset: 0x00013988
		protected override void InitDnsServerIps(bool useSingleDnsServer)
		{
			if (base.AllZones.Count > 0)
			{
				string text = base.AllZones.First<string>();
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "Using zone={0} to the find DNS server Ips", text, null, "InitDnsServerIps", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsRemoteConfiguration.cs", 40);
				IEnumerable<IPAddress> nsipEndPointsForDomain = DnsUtils.GetNSIpEndPointsForDomain(text);
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "DNS server Ips ={0}", string.Join<IPAddress>(",", nsipEndPointsForDomain), null, "InitDnsServerIps", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsRemoteConfiguration.cs", 45);
				if (useSingleDnsServer)
				{
					base.DnsServerIps = nsipEndPointsForDomain.Take(1).ToList<IPAddress>();
					return;
				}
				base.DnsServerIps = nsipEndPointsForDomain.Distinct<IPAddress>().ToList<IPAddress>();
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00015830 File Offset: 0x00013A30
		protected override void InitSupportedZones()
		{
			throw new DnsMonitorException("Cannot AutoDetect from a remote box, specify a SupportedZones node in WorkContext with AutoDetect='false'", null);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001583D File Offset: 0x00013A3D
		protected override void InitSupportedTargetServices()
		{
			throw new DnsMonitorException("Cannot AutoDetect from a remote box, specify a SupportedTargetServices node in WorkContext with AutoDetect='false'", null);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001584A File Offset: 0x00013A4A
		protected override void InitMonitorDomain()
		{
			throw new DnsMonitorException("Cannot AutoDetect from a remote box, specify a MonitorDomain node in WorkContext with AutoDetect='false'", null);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015857 File Offset: 0x00013A57
		protected override void InitIpV6Prefix()
		{
			throw new DnsMonitorException("Cannot AutoDetect from a remote box, specify a IPV6 prefix node in WorkContext with AutoDetect='false'", null);
		}
	}
}
