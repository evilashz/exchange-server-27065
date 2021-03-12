using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Smtp
{
	// Token: 0x02000028 RID: 40
	internal class SmtpMailItemSender
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00007588 File Offset: 0x00005788
		private SmtpMailItemSender()
		{
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007590 File Offset: 0x00005790
		public static SmtpMailItemSender Instance
		{
			get
			{
				return SmtpMailItemSender.instance;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00007597 File Offset: 0x00005797
		private static string LocalFQDN
		{
			get
			{
				return SmtpMailItemSender.localFQDN;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000759E File Offset: 0x0000579E
		private static bool IsFrontendAndHubColocatedServer
		{
			get
			{
				return SmtpMailItemSender.isFrontendAndHubColocatedServer;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000075A5 File Offset: 0x000057A5
		public SmtpMailItemResult Send(IReadOnlyMailItem readOnlyMailItem)
		{
			return this.Send(readOnlyMailItem, false, TimeSpan.FromMinutes(15.0));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000075BD File Offset: 0x000057BD
		public SmtpMailItemResult Send(IReadOnlyMailItem readOnlyMailItem, bool useLocalHubOnly, TimeSpan waitTimeOut)
		{
			return this.Send(readOnlyMailItem, useLocalHubOnly, waitTimeOut, null);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000075CC File Offset: 0x000057CC
		public SmtpMailItemResult Send(IReadOnlyMailItem readOnlyMailItem, bool useLocalHubOnly, TimeSpan waitTimeOut, ISmtpMailItemSenderNotifications notificationHandler)
		{
			if (readOnlyMailItem == null)
			{
				throw new ArgumentNullException("readOnlyMailItem");
			}
			IEnumerable<INextHopServer> enumerable;
			if (useLocalHubOnly)
			{
				string text = SmtpMailItemSender.LocalFQDN;
				bool flag = SmtpMailItemSender.IsFrontendAndHubColocatedServer;
				if (string.IsNullOrEmpty(text))
				{
					throw new InvalidOperationException("Email is unable to be sent because the name of the local machine can not be detemined.");
				}
				enumerable = new List<INextHopServer>();
				((List<INextHopServer>)enumerable).Add(new NextHopFqdn(text, flag));
			}
			else
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionHubSelector, readOnlyMailItem.LatencyTracker);
				if (!Components.ProxyHubSelectorComponent.ProxyHubSelector.TrySelectHubServers(readOnlyMailItem, out enumerable))
				{
					throw new InvalidOperationException("Email is unable to be sent because Hub Selector didn't return any HUBs.");
				}
				LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionHubSelector, readOnlyMailItem.LatencyTracker);
			}
			NextHopSolutionKey key = new NextHopSolutionKey(NextHopType.Empty, "MailboxTransportSubmissionInternalProxy", Guid.Empty);
			SmtpMailItemResult smtpMailItemResult;
			using (SmtpMailItemNextHopConnection smtpMailItemNextHopConnection = new SmtpMailItemNextHopConnection(key, readOnlyMailItem, notificationHandler))
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtpOut, readOnlyMailItem.LatencyTracker);
				Components.SmtpOutConnectionHandler.HandleProxyConnection(smtpMailItemNextHopConnection, enumerable, true, null);
				smtpMailItemNextHopConnection.AckConnectionEvent.WaitOne(waitTimeOut);
				LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtpOut, readOnlyMailItem.LatencyTracker);
				smtpMailItemResult = smtpMailItemNextHopConnection.SmtpMailItemResult;
			}
			return smtpMailItemResult;
		}

		// Token: 0x04000085 RID: 133
		private const string NextHopDomain = "MailboxTransportSubmissionInternalProxy";

		// Token: 0x04000086 RID: 134
		private static readonly SmtpMailItemSender instance = new SmtpMailItemSender();

		// Token: 0x04000087 RID: 135
		private static string localFQDN = Components.Configuration.LocalServer.TransportServer.Fqdn;

		// Token: 0x04000088 RID: 136
		private static bool isFrontendAndHubColocatedServer = Components.Configuration.LocalServer.TransportServer.IsHubTransportServer && Components.Configuration.LocalServer.TransportServer.IsFrontendTransportServer;
	}
}
