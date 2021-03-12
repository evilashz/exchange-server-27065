using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.ConnectionFiltering;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ConnectionFiltering
{
	// Token: 0x02000003 RID: 3
	public sealed class ConnectionFilteringAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ConnectionFilteringAgentFactory()
		{
			CommonUtils.RegisterConfigurationChangeHandlers("Connection Filtering", new ADOperation(this.RegisterConfigurationChangeHandlers), ExTraceGlobals.FactoryTracer, this);
			this.Configure(true);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			BypassedRecipients blockListProviderBypassedRecipients = new BypassedRecipients(this.config.BlockListProviderConfig.BypassedRecipients, (server != null) ? server.AddressBook : null);
			return new ConnectionFilteringAgent(this.config, server, blockListProviderBypassedRecipients);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002138 File Offset: 0x00000338
		public override void Close()
		{
			this.UnregisterConfigurationChangeHandlers();
			ConnectionFilteringAgent.PerformanceCounters.RemoveCounters();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002148 File Offset: 0x00000348
		private void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 150, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ConnectionFiltering\\Agent\\ConnectionFilteringAgent.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
			ADObjectId childId2 = childId.GetChildId("Message Hygiene");
			ADObjectId childId3 = childId2.GetChildId("IPAllowListProviderConfig");
			ADObjectId childId4 = childId2.GetChildId("IPBlockListProviderConfig");
			TransportFacades.ConfigChanged += this.ConfigUpdate;
			this.ipAllowListProviderRequestCookie = ADNotificationAdapter.RegisterChangeNotification<IPAllowListProvider>(childId3, new ADNotificationCallback(this.Configure));
			this.ipBlockListProviderRequestCookie = ADNotificationAdapter.RegisterChangeNotification<IPBlockListProvider>(childId4, new ADNotificationCallback(this.Configure));
			this.ipAllowListConfigRequestCookie = ADNotificationAdapter.RegisterChangeNotification<IPAllowListConfig>(childId2, new ADNotificationCallback(this.Configure));
			this.ipBlockListConfigRequestCookie = ADNotificationAdapter.RegisterChangeNotification<IPBlockListConfig>(childId2, new ADNotificationCallback(this.Configure));
			this.ipAllowListProviderConfigRequestCookie = ADNotificationAdapter.RegisterChangeNotification<IPAllowListProviderConfig>(childId2, new ADNotificationCallback(this.Configure));
			this.ipBlockListProviderConfigRequestCookie = ADNotificationAdapter.RegisterChangeNotification<IPBlockListProviderConfig>(childId2, new ADNotificationCallback(this.Configure));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002254 File Offset: 0x00000454
		private void UnregisterConfigurationChangeHandlers()
		{
			TransportFacades.ConfigChanged -= this.ConfigUpdate;
			if (this.ipAllowListProviderRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.ipAllowListProviderRequestCookie);
			}
			if (this.ipBlockListProviderRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.ipBlockListProviderRequestCookie);
			}
			if (this.ipAllowListConfigRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.ipAllowListConfigRequestCookie);
			}
			if (this.ipBlockListConfigRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.ipBlockListConfigRequestCookie);
			}
			if (this.ipAllowListProviderConfigRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.ipAllowListProviderConfigRequestCookie);
			}
			if (this.ipBlockListProviderConfigRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.ipBlockListProviderConfigRequestCookie);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022E4 File Offset: 0x000004E4
		private void ConfigUpdate(object source, EventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022ED File Offset: 0x000004ED
		private void Configure(ADNotificationEventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000231C File Offset: 0x0000051C
		private void Configure(bool onStartup)
		{
			ConnectionFilterConfig connectionFilterConfig;
			ADOperationResult adoperationResult;
			if (ADNotificationAdapter.TryReadConfiguration<ConnectionFilterConfig>(() => new ConnectionFilterConfig(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 266, "Configure", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ConnectionFiltering\\Agent\\ConnectionFilteringAgent.cs")), out connectionFilterConfig, out adoperationResult))
			{
				this.config = connectionFilterConfig;
				return;
			}
			CommonUtils.FailedToReadConfiguration("Connection Filtering", onStartup, adoperationResult.Exception, ExTraceGlobals.FactoryTracer, ConnectionFilteringAgent.EventLogger, this);
		}

		// Token: 0x04000005 RID: 5
		private ConnectionFilterConfig config;

		// Token: 0x04000006 RID: 6
		private ADNotificationRequestCookie ipAllowListProviderRequestCookie;

		// Token: 0x04000007 RID: 7
		private ADNotificationRequestCookie ipBlockListProviderRequestCookie;

		// Token: 0x04000008 RID: 8
		private ADNotificationRequestCookie ipAllowListConfigRequestCookie;

		// Token: 0x04000009 RID: 9
		private ADNotificationRequestCookie ipBlockListConfigRequestCookie;

		// Token: 0x0400000A RID: 10
		private ADNotificationRequestCookie ipAllowListProviderConfigRequestCookie;

		// Token: 0x0400000B RID: 11
		private ADNotificationRequestCookie ipBlockListProviderConfigRequestCookie;
	}
}
