using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x02000006 RID: 6
	public sealed class Factory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public Factory()
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.CompliancePolicy.RuleConfigurationAdChangeNotifications.Enabled)
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(Configuration.RegisterConfigurationChangeHandlers));
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceError(0L, "Unable to register for AD Change notification");
					throw new ExchangeConfigurationException(TransportRulesStrings.FailedToRegisterForConfigChangeNotification(Factory.AgentName), adoperationResult.Exception);
				}
			}
			Configuration.Configure(null);
			if (Configuration.Current == null)
			{
				throw new ExchangeConfigurationException(TransportRulesStrings.FailedToLoadAttachmentFilteringConfigOnStartup);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002D45 File Offset: 0x00000F45
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new Agent(server);
		}

		// Token: 0x0400001A RID: 26
		private static readonly string AgentName = "Attachment Filtering";
	}
}
