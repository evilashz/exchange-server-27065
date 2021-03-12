using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B53 RID: 2899
	[Cmdlet("Disable", "TransportRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableTransportRule : DisableRuleTaskBase
	{
		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x06006934 RID: 26932 RVA: 0x001B1D81 File Offset: 0x001AFF81
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableTransportRule(this.Identity.ToString());
			}
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x001B1D93 File Offset: 0x001AFF93
		public DisableTransportRule() : base(Utils.RuleCollectionNameFromRole())
		{
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x001B1DA0 File Offset: 0x001AFFA0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			try
			{
				transportRule.Xml = RuleParser.GetDisabledRuleXml(transportRule.Xml);
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
				return null;
			}
			if (Utils.Exchange12HubServersExist(this))
			{
				this.WriteWarning(Strings.SetRuleSyncAcrossDifferentVersionsNeeded);
			}
			TaskLogger.LogExit();
			return transportRule;
		}
	}
}
