using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B47 RID: 2887
	[Cmdlet("Remove", "OutboundConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveOutboundConnector : RemoveSystemConfigurationObjectTask<OutboundConnectorIdParameter, TenantOutboundConnector>
	{
		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x060068B6 RID: 26806 RVA: 0x001AF777 File Offset: 0x001AD977
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOutboundConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x001AF78C File Offset: 0x001AD98C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			try
			{
				ManageTenantOutboundConnectors.ValidateIfAcceptedDomainsCanBeRoutedWithConnectors(base.DataObject, base.DataSession, this, true);
				IEnumerable<TransportRule> source;
				if (Utils.TryGetTransportRules(base.DataSession, new Utils.TransportRuleSelectionDelegate(Utils.RuleHasOutboundConnectorReference), out source, base.DataObject.Name) && source.Any<TransportRule>())
				{
					base.WriteError(new ConnectorIncorrectUsageConnectorStillReferencedException(), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
		}
	}
}
