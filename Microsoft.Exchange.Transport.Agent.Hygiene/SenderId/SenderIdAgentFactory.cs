using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SenderId;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.SenderId
{
	// Token: 0x02000032 RID: 50
	public sealed class SenderIdAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public SenderIdAgentFactory()
		{
			CommonUtils.RegisterConfigurationChangeHandlers("Sender ID", new ADOperation(this.RegisterConfigurationChangeHandlers), ExTraceGlobals.OtherTracer, this);
			this.Configure(true);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009C20 File Offset: 0x00007E20
		private void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 69, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\SenderId\\Agent\\SenderIdAgentFactory.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
			ADObjectId childId2 = childId.GetChildId("Message Hygiene");
			TransportFacades.ConfigChanged += this.ConfigUpdate;
			this.notificationRequestCookie = ADNotificationAdapter.RegisterChangeNotification<SenderIdConfig>(childId2, new ADNotificationCallback(this.Configure));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009C92 File Offset: 0x00007E92
		private void UnregisterConfigurationChangeHandlers()
		{
			TransportFacades.ConfigChanged -= this.ConfigUpdate;
			if (this.notificationRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.notificationRequestCookie);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009CB8 File Offset: 0x00007EB8
		private void ConfigUpdate(object source, EventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009CC1 File Offset: 0x00007EC1
		private void Configure(ADNotificationEventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009CF0 File Offset: 0x00007EF0
		private void Configure(bool onStartup)
		{
			SenderIdConfig senderIdConfig;
			ADOperationResult adoperationResult;
			if (ADNotificationAdapter.TryReadConfiguration<SenderIdConfig>(() => DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 133, "Configure", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\SenderId\\Agent\\SenderIdAgentFactory.cs").FindSingletonConfigurationObject<SenderIdConfig>(), out senderIdConfig, out adoperationResult))
			{
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (SmtpDomain smtpDomain in senderIdConfig.BypassedSenderDomains)
				{
					hashSet.Add(smtpDomain.Domain);
				}
				this.senderIdConfig = senderIdConfig;
				this.bypassedSenderDomains = hashSet;
				return;
			}
			CommonUtils.FailedToReadConfiguration("Sender ID", onStartup, adoperationResult.Exception, ExTraceGlobals.OtherTracer, SenderIdAgentFactory.eventLogger, this);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			BypassedRecipients bypassedRecipients = new BypassedRecipients(this.senderIdConfig.BypassedRecipients, (server != null) ? server.AddressBook : null);
			return new SenderIdAgent(this.senderIdConfig, bypassedRecipients, this.bypassedSenderDomains, server);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009DE5 File Offset: 0x00007FE5
		public override void Close()
		{
			this.UnregisterConfigurationChangeHandlers();
			Util.PerformanceCounters.RemoveCounters();
		}

		// Token: 0x0400011E RID: 286
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.OtherTracer.Category, "MSExchange Antispam");

		// Token: 0x0400011F RID: 287
		private SenderIdConfig senderIdConfig;

		// Token: 0x04000120 RID: 288
		private HashSet<string> bypassedSenderDomains;

		// Token: 0x04000121 RID: 289
		private ADNotificationRequestCookie notificationRequestCookie;
	}
}
