using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000432 RID: 1074
	internal class OutboundProxyBySourceTracker
	{
		// Token: 0x06003161 RID: 12641 RVA: 0x000C4FB8 File Offset: 0x000C31B8
		public OutboundProxyBySourceTracker(string resourceForestMatchingDomains)
		{
			ArgumentValidator.ThrowIfNull("resourceForestMatchingDomains", resourceForestMatchingDomains);
			if (!string.IsNullOrEmpty(resourceForestMatchingDomains))
			{
				OutboundProxyBySourceTracker.O365Domains = resourceForestMatchingDomains.Split(new char[]
				{
					'|'
				});
			}
			this.smtpProxyTracker = new ConnectionsTracker(new ConnectionsTracker.GetExPerfCounterDelegate(OutboundProxyBySourceTracker.GetConnectionsCurrentCounter), new ConnectionsTracker.GetExPerfCounterDelegate(OutboundProxyBySourceTracker.GetConnectionsTotalCounter));
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000C501C File Offset: 0x000C321C
		public static ExPerformanceCounter GetConnectionsCurrentCounter(string ehloDomain)
		{
			string office365DomainFromEhlo = OutboundProxyBySourceTracker.GetOffice365DomainFromEhlo(ehloDomain);
			return OutboundProxyBySourcePerfCounters.GetInstance(office365DomainFromEhlo).ConnectionsCurrent;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000C503C File Offset: 0x000C323C
		public static ExPerformanceCounter GetConnectionsTotalCounter(string ehloDomain)
		{
			string office365DomainFromEhlo = OutboundProxyBySourceTracker.GetOffice365DomainFromEhlo(ehloDomain);
			return OutboundProxyBySourcePerfCounters.GetInstance(office365DomainFromEhlo).ConnectionsTotal;
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000C505B File Offset: 0x000C325B
		public void IncrementProxyCount(string forest)
		{
			ArgumentValidator.ThrowIfNull("forest", forest);
			this.smtpProxyTracker.IncrementProxyCount(forest);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000C5074 File Offset: 0x000C3274
		public void DecrementProxyCount(string forest)
		{
			ArgumentValidator.ThrowIfNull("forest", forest);
			this.smtpProxyTracker.DecrementProxyCount(forest);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000C508D File Offset: 0x000C328D
		public bool TryGetDiagnosticInfo(DiagnosableParameters parameters, out XElement diagnosticInfo)
		{
			diagnosticInfo = null;
			return false;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000C5094 File Offset: 0x000C3294
		internal static string GetOffice365DomainFromEhlo(string ehloDomain)
		{
			string result = "Forest-UNKNOWN";
			if (!string.IsNullOrEmpty(ehloDomain) && OutboundProxyBySourceTracker.O365Domains != null)
			{
				int num = ehloDomain.IndexOf('.');
				if (num != -1 && num < ehloDomain.Length - 1)
				{
					string text = ehloDomain.Substring(num + 1);
					foreach (string value in OutboundProxyBySourceTracker.O365Domains)
					{
						if (!string.IsNullOrEmpty(value) && text.EndsWith(value, StringComparison.OrdinalIgnoreCase))
						{
							result = text;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04001818 RID: 6168
		private const string UnknownResourceForest = "Forest-UNKNOWN";

		// Token: 0x04001819 RID: 6169
		private static string[] O365Domains;

		// Token: 0x0400181A RID: 6170
		private ConnectionsTracker smtpProxyTracker;
	}
}
