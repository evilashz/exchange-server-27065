using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
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
	// Token: 0x02000020 RID: 32
	public sealed class RecipientFilterAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000076AA File Offset: 0x000058AA
		public RecipientFilterAgentFactory()
		{
			CommonUtils.RegisterConfigurationChangeHandlers("Recipient Filtering", new ADOperation(this.RegisterConfigurationChangeHandlers), ExTraceGlobals.RecipientFilterAgentTracer, this);
			this.Configure(true);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000076D8 File Offset: 0x000058D8
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			AddressBook addressBook = this.recipientFilterConfig.RecipientValidationEnabled ? server.AddressBook : null;
			return new RecipientFilterAgent(this.recipientFilterConfig, this.blockedRecipients, addressBook, server.AcceptedDomains);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007722 File Offset: 0x00005922
		public override void Close()
		{
			this.UnregisterConfigurationChangeHandlers();
			Util.PerformanceCounters.RecipientFilter.RemoveCounters();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007730 File Offset: 0x00005930
		private void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 117, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ProtocolFilter\\Agent\\RecipientFilterAgent.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
			ADObjectId childId2 = childId.GetChildId("Message Hygiene");
			TransportFacades.ConfigChanged += this.ConfigUpdate;
			this.notificationRequestCookie = ADNotificationAdapter.RegisterChangeNotification<RecipientFilterConfig>(childId2, new ADNotificationCallback(this.Configure));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000077A2 File Offset: 0x000059A2
		private void UnregisterConfigurationChangeHandlers()
		{
			TransportFacades.ConfigChanged -= this.ConfigUpdate;
			if (this.notificationRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.notificationRequestCookie);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000077C8 File Offset: 0x000059C8
		private void ConfigUpdate(object source, EventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000077D1 File Offset: 0x000059D1
		private void Configure(ADNotificationEventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007800 File Offset: 0x00005A00
		private void Configure(bool onStartup)
		{
			RecipientFilterConfig recipientFilterConfig;
			ADOperationResult adoperationResult;
			if (ADNotificationAdapter.TryReadConfiguration<RecipientFilterConfig>(() => DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 181, "Configure", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ProtocolFilter\\Agent\\RecipientFilterAgent.cs").FindSingletonConfigurationObject<RecipientFilterConfig>(), out recipientFilterConfig, out adoperationResult))
			{
				Dictionary<RoutingAddress, bool> dictionary = new Dictionary<RoutingAddress, bool>();
				foreach (SmtpAddress smtpAddress in recipientFilterConfig.BlockedRecipients)
				{
					dictionary.Add(new RoutingAddress(smtpAddress.ToString()), true);
				}
				this.recipientFilterConfig = recipientFilterConfig;
				this.blockedRecipients = dictionary;
				return;
			}
			CommonUtils.FailedToReadConfiguration("Recipient Filtering", onStartup, adoperationResult.Exception, ExTraceGlobals.RecipientFilterAgentTracer, RecipientFilterAgentFactory.eventLogger, this);
		}

		// Token: 0x040000D7 RID: 215
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.RecipientFilterAgentTracer.Category, "MSExchange Antispam");

		// Token: 0x040000D8 RID: 216
		private RecipientFilterConfig recipientFilterConfig;

		// Token: 0x040000D9 RID: 217
		private Dictionary<RoutingAddress, bool> blockedRecipients;

		// Token: 0x040000DA RID: 218
		private ADNotificationRequestCookie notificationRequestCookie;
	}
}
