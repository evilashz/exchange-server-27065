using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.ProtocolFilter;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x02000026 RID: 38
	internal sealed class BlockedSenders
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00008B08 File Offset: 0x00006D08
		public BlockedSenders(ICollection<SmtpAddress> blockedSenders, ICollection<SmtpDomain> blockedDomains, ICollection<SmtpDomain> blockedDomainsAndSubdomains)
		{
			if (blockedSenders.Count > 0)
			{
				this.blockedSenders = new Dictionary<RoutingAddress, object>(blockedSenders.Count);
				foreach (SmtpAddress smtpAddress in blockedSenders)
				{
					this.blockedSenders.Add((RoutingAddress)smtpAddress.ToString(), null);
				}
			}
			List<SenderDomainEntry> list = new List<SenderDomainEntry>();
			foreach (SmtpDomain domain in blockedDomains)
			{
				list.Add(new SenderDomainEntry(domain, false));
			}
			foreach (SmtpDomain domain2 in blockedDomainsAndSubdomains)
			{
				list.Add(new SenderDomainEntry(domain2, true));
			}
			if (list.Count > 0)
			{
				this.domainMatchMap = new DomainMatchMap<SenderDomainEntry>(list);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008C2C File Offset: 0x00006E2C
		public bool IsBlocked(RoutingAddress address, out LogEntry logEntry)
		{
			logEntry = null;
			if (this.blockedSenders != null && this.blockedSenders.ContainsKey(address))
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Sender {0} is an exact match", address);
				logEntry = SenderFilterAgent.RejectContext.ExactMatch(address.ToString());
				return true;
			}
			if (this.domainMatchMap != null)
			{
				SmtpDomain domainPart = SmtpDomain.GetDomainPart(address);
				SenderDomainEntry bestMatch = this.domainMatchMap.GetBestMatch(domainPart);
				if (bestMatch != null)
				{
					ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Sender {0} was matched because of a blocked domain or parent domain", address);
					logEntry = ((!bestMatch.DomainName.IncludeSubDomains) ? SenderFilterAgent.RejectContext.DomainMatch(address.DomainPart) : SenderFilterAgent.RejectContext.SubdomainMatch(address.DomainPart));
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000F5 RID: 245
		private Dictionary<RoutingAddress, object> blockedSenders;

		// Token: 0x040000F6 RID: 246
		private DomainMatchMap<SenderDomainEntry> domainMatchMap;
	}
}
