using System;
using System.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000074 RID: 116
	internal class JournalingRuleSerializer : TransportRuleSerializer
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000143FB File Offset: 0x000125FB
		public new static JournalingRuleSerializer Instance
		{
			get
			{
				return JournalingRuleSerializer.instance;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00014404 File Offset: 0x00012604
		protected override void SaveRuleAttributes(XmlWriter xmlWriter, Rule rule)
		{
			base.SaveRuleAttributes(xmlWriter, rule);
			JournalingRule journalingRule = rule as JournalingRule;
			if (journalingRule.GccRuleType != GccType.None)
			{
				xmlWriter.WriteAttributeString("gccType", JournalingRuleConstants.StringFromGccType(journalingRule.GccRuleType));
			}
		}

		// Token: 0x04000255 RID: 597
		private static readonly JournalingRuleSerializer instance = new JournalingRuleSerializer();
	}
}
