using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000234 RID: 564
	internal class FanOutPlanner
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x00065153 File Offset: 0x00063353
		public FanOutPlanner(RoutingTables routingTables)
		{
			this.siteRelayMap = routingTables.ServerMap.SiteRelayMap;
			this.timestamp = routingTables.WhenCreated;
			this.recipientMap = new Dictionary<Guid, FanOutPlanner.PathRecipients>();
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00065184 File Offset: 0x00063384
		public void AddRecipient(MailRecipient recipient)
		{
			Guid nextHopConnector = recipient.NextHop.NextHopConnector;
			ADSiteRelayMap.ADTopologyPath adtopologyPath;
			if (!this.siteRelayMap.TryGetPath(nextHopConnector, out adtopologyPath))
			{
				ExTraceGlobals.RoutingTracer.TraceError<DateTime, Guid, RoutingAddress>((long)this.GetHashCode(), "[{0}] [DFO] Cannot find route to AD site '{1}' for recipient '{2}'. E-DNS will handle it as a config change.", this.timestamp, nextHopConnector, recipient.Email);
				return;
			}
			Guid guid = adtopologyPath.FirstHopSiteGuid();
			FanOutPlanner.PathRecipients pathRecipients;
			if (this.recipientMap.TryGetValue(guid, out pathRecipients))
			{
				pathRecipients.AddRecipient(ADSiteRelayMap.ADTopologyPath.GetCommonPath(adtopologyPath, pathRecipients.CommonPath), recipient);
				ExTraceGlobals.RoutingTracer.TraceDebug<DateTime, RoutingAddress, Guid>((long)this.GetHashCode(), "[{0}] [DFO] Recipient '{1}' added to existing group with first hop id '{2}'.", this.timestamp, recipient.Email, guid);
				return;
			}
			if (TransportHelpers.AttemptAddToDictionary<Guid, FanOutPlanner.PathRecipients>(this.recipientMap, guid, new FanOutPlanner.PathRecipients(adtopologyPath, recipient), new TransportHelpers.DiagnosticsHandler<Guid, FanOutPlanner.PathRecipients>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, FanOutPlanner.PathRecipients>)))
			{
				ExTraceGlobals.RoutingTracer.TraceDebug<DateTime, RoutingAddress, Guid>((long)this.GetHashCode(), "[{0}] [DFO] Created a new group for recipient '{1}' and with first hop id '{2}'.", this.timestamp, recipient.Email, guid);
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00065268 File Offset: 0x00063468
		public void UpdateRecipientNextHops()
		{
			foreach (FanOutPlanner.PathRecipients pathRecipients in this.recipientMap.Values)
			{
				pathRecipients.UpdateRecipientNextHops(this.timestamp);
			}
		}

		// Token: 0x04000BFB RID: 3067
		private DateTime timestamp;

		// Token: 0x04000BFC RID: 3068
		private ADSiteRelayMap siteRelayMap;

		// Token: 0x04000BFD RID: 3069
		private Dictionary<Guid, FanOutPlanner.PathRecipients> recipientMap;

		// Token: 0x02000235 RID: 565
		private class PathRecipients
		{
			// Token: 0x060018E7 RID: 6375 RVA: 0x000652C8 File Offset: 0x000634C8
			public PathRecipients(ADSiteRelayMap.ADTopologyPath path, MailRecipient recipient)
			{
				this.recipients = new List<MailRecipient>();
				this.AddRecipient(path, recipient);
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000652E3 File Offset: 0x000634E3
			public ADSiteRelayMap.ADTopologyPath CommonPath
			{
				get
				{
					return this.commonPath;
				}
			}

			// Token: 0x060018E9 RID: 6377 RVA: 0x000652EB File Offset: 0x000634EB
			public void AddRecipient(ADSiteRelayMap.ADTopologyPath newCommonPath, MailRecipient recipient)
			{
				this.commonPath = newCommonPath;
				this.recipients.Add(recipient);
			}

			// Token: 0x060018EA RID: 6378 RVA: 0x00065300 File Offset: 0x00063500
			public void UpdateRecipientNextHops(DateTime timestamp)
			{
				if (this.recipients.Count > 1)
				{
					foreach (MailRecipient mailRecipient in this.recipients)
					{
						ExTraceGlobals.RoutingTracer.TraceDebug((long)this.GetHashCode(), "[{0}] [DFO] Changing next hop site for recipient '{1}' from '{2}:{3}' to '{4}:{5}'.", new object[]
						{
							timestamp,
							mailRecipient.Email,
							mailRecipient.NextHop.NextHopDomain,
							mailRecipient.NextHop.NextHopConnector,
							this.commonPath.NextHopSite.Name,
							this.commonPath.NextHopSite.Guid
						});
						ExTraceGlobals.RoutingTracer.TracePfd((long)this.GetHashCode(), "PFD CAT {0} Delayed Fan-out: Changing next hop site for recipient '{1}' from '{2}' to '{3}'.", new object[]
						{
							17570,
							mailRecipient.Email,
							mailRecipient.NextHop.NextHopDomain,
							this.commonPath.NextHopSite.Name
						});
						mailRecipient.NextHop = new NextHopSolutionKey(mailRecipient.NextHop.NextHopType, this.commonPath.NextHopSite.Name, this.commonPath.NextHopSite.Guid);
					}
				}
			}

			// Token: 0x04000BFE RID: 3070
			private ADSiteRelayMap.ADTopologyPath commonPath;

			// Token: 0x04000BFF RID: 3071
			private List<MailRecipient> recipients;
		}
	}
}
