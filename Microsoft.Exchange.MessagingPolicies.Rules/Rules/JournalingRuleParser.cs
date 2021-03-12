using System;
using System.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000073 RID: 115
	internal class JournalingRuleParser : TransportRuleParser
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00014314 File Offset: 0x00012514
		public new static JournalingRuleParser Instance
		{
			get
			{
				return JournalingRuleParser.instance;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001431B File Offset: 0x0001251B
		public override Rule CreateRule(string name)
		{
			return new JournalingRule(name);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014324 File Offset: 0x00012524
		public override Action CreateAction(string actionName, ShortList<Argument> arguments, string externalName = null)
		{
			JournalBase journalBase = JournalingRuleParser.CreateAction(actionName, arguments);
			journalBase.ExternalName = externalName;
			return journalBase;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00014344 File Offset: 0x00012544
		protected override Rule ParseRuleAttributes(XmlReader reader)
		{
			JournalingRule journalingRule = (JournalingRule)base.ParseRuleAttributes(reader);
			string attribute = reader.GetAttribute("gccType");
			GccType gccRuleType;
			if (string.IsNullOrEmpty(attribute))
			{
				gccRuleType = GccType.None;
			}
			else if (!JournalingRuleConstants.TryParseGccType(attribute, out gccRuleType))
			{
				throw new ParserException(RulesStrings.InvalidAttribute("gccType", "rule", attribute), reader);
			}
			journalingRule.GccRuleType = gccRuleType;
			return journalingRule;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000143A0 File Offset: 0x000125A0
		private static JournalBase CreateAction(string actionName, ShortList<Argument> arguments)
		{
			if (actionName != null)
			{
				if (actionName == "Journal")
				{
					return new Journal(arguments);
				}
				if (actionName == "JournalAndReconcile")
				{
					return new JournalAndReconcile(arguments);
				}
			}
			throw new RulesValidationException(RulesStrings.InvalidActionName(actionName));
		}

		// Token: 0x04000254 RID: 596
		private static readonly JournalingRuleParser instance = new JournalingRuleParser();
	}
}
