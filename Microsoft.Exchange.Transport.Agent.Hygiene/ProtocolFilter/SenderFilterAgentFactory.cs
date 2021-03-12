using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProtocolFilter;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x02000023 RID: 35
	public sealed class SenderFilterAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00007C71 File Offset: 0x00005E71
		public SenderFilterAgentFactory()
		{
			CommonUtils.RegisterConfigurationChangeHandlers("Sender Filtering", new ADOperation(this.RegisterConfigurationChangeHandlers), ExTraceGlobals.SenderFilterAgentTracer, this);
			this.Configure(true);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007C9C File Offset: 0x00005E9C
		private void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 72, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ProtocolFilter\\Agent\\SenderFilterAgent.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
			ADObjectId childId2 = childId.GetChildId("Message Hygiene");
			TransportFacades.ConfigChanged += this.ConfigUpdate;
			this.notificationRequestCookie = ADNotificationAdapter.RegisterChangeNotification<SenderFilterConfig>(childId2, new ADNotificationCallback(this.Configure));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007D0E File Offset: 0x00005F0E
		private void UnregisterConfigurationChangeHandlers()
		{
			TransportFacades.ConfigChanged -= this.ConfigUpdate;
			if (this.notificationRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.notificationRequestCookie);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007D34 File Offset: 0x00005F34
		private void ConfigUpdate(object source, EventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007D3D File Offset: 0x00005F3D
		private void Configure(ADNotificationEventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007D6C File Offset: 0x00005F6C
		private void Configure(bool onStartup)
		{
			SenderFilterConfig senderFilterConfig;
			ADOperationResult adoperationResult;
			if (ADNotificationAdapter.TryReadConfiguration<SenderFilterConfig>(() => DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 136, "Configure", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ProtocolFilter\\Agent\\SenderFilterAgent.cs").FindSingletonConfigurationObject<SenderFilterConfig>(), out senderFilterConfig, out adoperationResult))
			{
				this.senderFilterConfig = senderFilterConfig;
				this.blockedSenders = new BlockedSenders(senderFilterConfig.BlockedSenders, senderFilterConfig.BlockedDomains, senderFilterConfig.BlockedDomainsAndSubdomains);
				return;
			}
			CommonUtils.FailedToReadConfiguration("Sender Filtering", onStartup, adoperationResult.Exception, ExTraceGlobals.SenderFilterAgentTracer, SenderFilterAgentFactory.eventLogger, this);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007DE2 File Offset: 0x00005FE2
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new SenderFilterAgent(this.senderFilterConfig, this.blockedSenders, server.AddressBook);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007DFB File Offset: 0x00005FFB
		public override void Close()
		{
			this.UnregisterConfigurationChangeHandlers();
			Util.PerformanceCounters.SenderFilter.RemoveCounters();
		}

		// Token: 0x040000E6 RID: 230
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.SenderFilterAgentTracer.Category, "MSExchange Antispam");

		// Token: 0x040000E7 RID: 231
		private SenderFilterConfig senderFilterConfig;

		// Token: 0x040000E8 RID: 232
		private BlockedSenders blockedSenders;

		// Token: 0x040000E9 RID: 233
		private ADNotificationRequestCookie notificationRequestCookie;
	}
}
