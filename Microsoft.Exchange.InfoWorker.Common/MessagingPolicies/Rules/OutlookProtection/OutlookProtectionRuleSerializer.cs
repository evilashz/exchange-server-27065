using System;
using System.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x02000183 RID: 387
	internal sealed class OutlookProtectionRuleSerializer : RuleSerializer
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0002C364 File Offset: 0x0002A564
		protected override void SaveRuleSubElements(XmlWriter writer, Rule rule)
		{
			base.SaveRuleSubElements(writer, rule);
			OutlookProtectionRule outlookProtectionRule = (OutlookProtectionRule)rule;
			if (outlookProtectionRule.UserOverridable)
			{
				writer.WriteStartElement("userOverridable");
				writer.WriteEndElement();
			}
		}
	}
}
