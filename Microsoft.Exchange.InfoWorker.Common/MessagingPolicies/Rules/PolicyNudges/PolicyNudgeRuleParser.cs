using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges
{
	// Token: 0x0200017D RID: 381
	internal sealed class PolicyNudgeRuleParser
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0002BEE1 File Offset: 0x0002A0E1
		public static PolicyNudgeRuleParser Instance
		{
			get
			{
				return PolicyNudgeRuleParser.instance;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002BEE8 File Offset: 0x0002A0E8
		public PolicyNudgeRule GetRule(string ruleString, string id, DateTime version)
		{
			ETRToPNRTranslator etrtoPNRTranslator = new ETRToPNRTranslator(ruleString, PolicyNudgeRuleParser.emptyMessageStrings, null, null);
			if (!etrtoPNRTranslator.IsValid)
			{
				return null;
			}
			return new PolicyNudgeRule(ruleString, id, version, etrtoPNRTranslator.Enabled, etrtoPNRTranslator.ActivationDate, etrtoPNRTranslator.ExpiryDate);
		}

		// Token: 0x040007EE RID: 2030
		private static readonly PolicyNudgeRuleParser instance = new PolicyNudgeRuleParser();

		// Token: 0x040007EF RID: 2031
		private static ETRToPNRTranslator.IMessageStrings emptyMessageStrings = new ETRToPNRTranslator.MessageStringCallbackImpl(string.Empty, (ETRToPNRTranslator.OutlookActionTypes type) => PolicyTipMessage.Empty, () => PolicyTipMessage.Empty);
	}
}
