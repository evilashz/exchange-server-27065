using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000399 RID: 921
	internal sealed class OutboundProxySession : RbacSession
	{
		// Token: 0x060030EC RID: 12524 RVA: 0x00095250 File Offset: 0x00093450
		private OutboundProxySession(RbacContext context, IEnumerable<Uri> serviceUrls) : base(context, OutboundProxySession.sessionPerfCounters, OutboundProxySession.esoSessionPerfCounters)
		{
			this.ServiceUrls = serviceUrls.ToArray<Uri>();
			this.serviceUrlProxyQueue = new ProxyQueue<Uri>(this.ServiceUrls);
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x0009528B File Offset: 0x0009348B
		// (set) Token: 0x060030EE RID: 12526 RVA: 0x00095293 File Offset: 0x00093493
		public IEnumerable<Uri> ServiceUrls { get; private set; }

		// Token: 0x060030EF RID: 12527 RVA: 0x000952A4 File Offset: 0x000934A4
		protected override void WriteInitializationLog()
		{
			string[] value = (from url in this.ServiceUrls
			select url.Host).ToArray<string>();
			string text = string.Join("', '", value);
			ExTraceGlobals.RBACTracer.TraceInformation<OutboundProxySession, string>(0, 0L, "{0} created to proxy calls to '{1}'.", this, text);
			EcpEventLogConstants.Tuple_OutboundProxySessionInitialize.LogEvent(new object[]
			{
				base.NameForEventLog,
				text
			});
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0009531E File Offset: 0x0009351E
		public override void RequestReceived()
		{
			base.RequestReceived();
			HttpContext.Current.RemapHandler(new ProxyHandler(this));
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000954E8 File Offset: 0x000936E8
		private IEnumerable<ProxyConnection> GetProxyConnections()
		{
			foreach (Uri uri in this.serviceUrlProxyQueue)
			{
				ProxyConnection connection = this.GetProxyConnection(uri);
				if (connection.IsCompatible)
				{
					yield return connection;
				}
			}
			yield break;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x00095520 File Offset: 0x00093720
		private ProxyConnection GetProxyConnection(Uri serviceUrl)
		{
			return this.proxyConnections.AddIfNotExists(serviceUrl, delegate(Uri url)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation<Uri>(0, 0L, "Creating ProxyConnection for {0}", url);
				return new ProxyConnection(url);
			});
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0009554B File Offset: 0x0009374B
		public IAsyncResult BeginSendOutboundProxyRequest(HttpContext context, AsyncCallback requestCompletedCallback, object requestCompletedData)
		{
			return new OutboundProxyRequest(this.GetProxyConnections(), context, requestCompletedCallback, requestCompletedData);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0009555C File Offset: 0x0009375C
		public void EndSendOutboundProxyRequest(IAsyncResult result)
		{
			OutboundProxyRequest outboundProxyRequest = (OutboundProxyRequest)result;
			outboundProxyRequest.AsyncWaitHandle.WaitOne();
			if (outboundProxyRequest.Exception != null || outboundProxyRequest.AllServersFailed)
			{
				throw new ProxyFailureException(outboundProxyRequest.Context.Request.Url, base.ExecutingUserId, outboundProxyRequest.Exception);
			}
		}

		// Token: 0x040023A5 RID: 9125
		private static PerfCounterGroup sessionsCounters = new PerfCounterGroup(EcpPerfCounters.OutboundProxySessions, EcpPerfCounters.OutboundProxySessionsPeak, EcpPerfCounters.OutboundProxySessionsTotal);

		// Token: 0x040023A6 RID: 9126
		private static PerfCounterGroup requestsCounters = new PerfCounterGroup(EcpPerfCounters.OutboundProxyRequests, EcpPerfCounters.OutboundProxyRequestsPeak, EcpPerfCounters.OutboundProxyRequestsTotal);

		// Token: 0x040023A7 RID: 9127
		private static PerfCounterGroup esoSessionsCounters = new PerfCounterGroup(EcpPerfCounters.EsoOutboundProxySessions, EcpPerfCounters.EsoOutboundProxySessionsPeak, EcpPerfCounters.EsoOutboundProxySessionsTotal);

		// Token: 0x040023A8 RID: 9128
		private static PerfCounterGroup esoRequestsCounters = new PerfCounterGroup(EcpPerfCounters.EsoOutboundProxyRequests, EcpPerfCounters.EsoOutboundProxyRequestsPeak, EcpPerfCounters.EsoOutboundProxyRequestsTotal);

		// Token: 0x040023A9 RID: 9129
		private static SessionPerformanceCounters sessionPerfCounters = new SessionPerformanceCounters(OutboundProxySession.sessionsCounters, OutboundProxySession.requestsCounters);

		// Token: 0x040023AA RID: 9130
		private static EsoSessionPerformanceCounters esoSessionPerfCounters = new EsoSessionPerformanceCounters(OutboundProxySession.sessionsCounters, OutboundProxySession.requestsCounters, OutboundProxySession.esoSessionsCounters, OutboundProxySession.esoRequestsCounters);

		// Token: 0x040023AB RID: 9131
		private ProxyQueue<Uri> serviceUrlProxyQueue;

		// Token: 0x040023AC RID: 9132
		private SynchronizedDictionary<Uri, ProxyConnection> proxyConnections = new SynchronizedDictionary<Uri, ProxyConnection>();

		// Token: 0x0200039A RID: 922
		public new sealed class Factory : RbacSession.Factory
		{
			// Token: 0x060030F8 RID: 12536 RVA: 0x00095794 File Offset: 0x00093994
			public Factory(RbacContext context) : base(context)
			{
				if (OutboundProxySession.Factory.ProxyToLocalHost)
				{
					ExTraceGlobals.ProxyTracer.TraceInformation(0, 0L, "ProxyToLocalHost is True, so we will always proxy back to ourselves.");
					if (OutboundProxySession.Factory.ProxyToLocalHostUris == null)
					{
						OutboundProxySession.Factory.ProxyToLocalHostUris = new Uri[]
						{
							new Uri(HttpContext.Current.Request.Url, HttpRuntime.AppDomainAppVirtualPath)
						};
					}
					this.ProxyTargets = OutboundProxySession.Factory.ProxyToLocalHostUris;
					return;
				}
				OutboundProxySession.Factory.<>c__DisplayClassf CS$<>8__locals1 = new OutboundProxySession.Factory.<>c__DisplayClassf();
				CS$<>8__locals1.allowProxyingWithoutSsl = Registry.AllowProxyingWithoutSsl;
				CS$<>8__locals1.mailboxMajorVersion = base.Context.MailboxServerVersion.Major;
				using (StringWriter decisionLogWriter = new StringWriter())
				{
					IList<EcpService> servicesInMailboxSite = base.Context.GetServicesInMailboxSite(ClientAccessType.Internal, delegate(EcpService service)
					{
						decisionLogWriter.WriteLine(service.Url);
						Version version = new ServerVersion(service.ServerVersionNumber);
						DecisionLogger decisionLogger = new DecisionLogger(decisionLogWriter)
						{
							{
								(service.AuthenticationMethod & AuthenticationMethod.WindowsIntegrated) == AuthenticationMethod.WindowsIntegrated,
								Strings.ProxyServiceConditionWindowsAuth
							},
							{
								version.Major == CS$<>8__locals1.mailboxMajorVersion,
								Strings.ProxyServiceConditionMajorVersion(version.Major, CS$<>8__locals1.mailboxMajorVersion)
							},
							{
								service.Url.IsAbsoluteUri && !service.Url.IsLoopback && (service.Url.Scheme == Uri.UriSchemeHttps || service.Url.Scheme == Uri.UriSchemeHttp),
								Strings.ProxyServiceConditionInvalidUri
							},
							{
								CS$<>8__locals1.allowProxyingWithoutSsl || service.Url.Scheme == "https",
								Strings.ProxyServiceConditionSchemeUri(Environment.MachineName, service.ServerFullyQualifiedDomainName)
							}
						};
						decisionLogWriter.WriteLine();
						return decisionLogger.Decision;
					});
					this.ProxyTargets = (from service in servicesInMailboxSite
					select service.Url).ToArray<Uri>();
					this.decisionLog = decisionLogWriter.GetStringBuilder().ToString();
				}
			}

			// Token: 0x17001F52 RID: 8018
			// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000958CC File Offset: 0x00093ACC
			// (set) Token: 0x060030FA RID: 12538 RVA: 0x000958D4 File Offset: 0x00093AD4
			public IList<Uri> ProxyTargets { get; private set; }

			// Token: 0x060030FB RID: 12539 RVA: 0x000958E0 File Offset: 0x00093AE0
			protected override bool CanCreateSession()
			{
				if (this.ProxyTargets.Count == 0)
				{
					ExTraceGlobals.ProxyTracer.TraceInformation<string, string>(0, 0L, "No internal CAS was found in the mailbox site {0} that could serve as proxy target. Services considered:\r\n{1}", "[mailbox site]", this.decisionLog);
					EcpEventLogConstants.Tuple_EcpProxyCantFindCasServer.LogPeriodicEvent("[mailbox site]", new object[]
					{
						base.Settings.UserName,
						"[current site]",
						"[mailbox site]",
						this.decisionLog
					});
					throw new ProxyCantFindCasServerException(base.Settings.UserName, "[current site]", "[mailbox site]", this.decisionLog);
				}
				return true;
			}

			// Token: 0x060030FC RID: 12540 RVA: 0x00095977 File Offset: 0x00093B77
			protected override RbacSession CreateNewSession()
			{
				return new OutboundProxySession(base.Context, this.ProxyTargets);
			}

			// Token: 0x040023B0 RID: 9136
			public static bool ProxyToLocalHost = StringComparer.OrdinalIgnoreCase.Equals("true", ConfigurationManager.AppSettings["ProxyToLocalHost"]);

			// Token: 0x040023B1 RID: 9137
			public static Uri[] ProxyToLocalHostUris;

			// Token: 0x040023B2 RID: 9138
			private string decisionLog;
		}
	}
}
