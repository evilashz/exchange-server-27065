using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Hygiene.Deployment.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Security
{
	// Token: 0x0200020B RID: 523
	public class IpsecConnectionSecurityRulesProbe : ProbeWorkItem
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x0002B430 File Offset: 0x00029630
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.logger = new ProbeWorkItemLogger(this, false, true);
			this.logger.LogMessage("IpsecConnectionSecurityRulesProbe started");
			List<Dictionary<string, string>> firewallConnectionSecurityRules = NetHelpers.GetFirewallConnectionSecurityRules();
			this.logger.LogMessage("ruleCount:" + firewallConnectionSecurityRules.Count);
			bool flag = bool.Parse(ProbeHelper.GetExtensionAttribute(this.logger, this, "VerifyRequestInRequestOut"));
			if (!flag)
			{
				this.logger.LogMessage("Skipping RequestInRequestOut");
				return;
			}
			this.logger.LogMessage("Verifying RequestInRequestOut.");
			bool flag2 = false;
			foreach (Dictionary<string, string> dictionary in firewallConnectionSecurityRules)
			{
				this.logger.LogMessage("ruleName:" + (dictionary.ContainsKey("Rule Name") ? dictionary["Rule Name"] : "(null)"));
				if (this.AssertRuleProperty(dictionary, "Endpoint1", "Any") && this.AssertRuleProperty(dictionary, "Endpoint2", "Any") && this.AssertRuleProperty(dictionary, "Action", "RequestInRequestOut") && this.AssertRuleProperty(dictionary, "Auth1", "ComputerKerb") && this.AssertRuleProperty(dictionary, "Auth2", "UserKerb") && this.AssertRuleProperty(dictionary, "MainModeSecMethods", "ECDHP384-AES256-SHA384"))
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				this.logger.LogMessage("requestInRequestOutSucceeded is true");
				return;
			}
			throw new Exception("Did not find a rule that matched for RequestInRequestOut");
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002B5C8 File Offset: 0x000297C8
		private bool AssertRuleProperty(Dictionary<string, string> rule, string propertyName, string expectedValue)
		{
			return rule.ContainsKey(propertyName) && string.Equals(rule[propertyName], expectedValue);
		}

		// Token: 0x040007B1 RID: 1969
		private IHygieneLogger logger = new NullHygieneLogger();
	}
}
