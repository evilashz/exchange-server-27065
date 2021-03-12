using System;
using System.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x02000182 RID: 386
	internal sealed class OutlookProtectionRuleParser : RuleParser
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0002C1C1 File Offset: 0x0002A3C1
		private OutlookProtectionRuleParser()
		{
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002C1C9 File Offset: 0x0002A3C9
		public static OutlookProtectionRuleParser Instance
		{
			get
			{
				return OutlookProtectionRuleParser.instance;
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002C1D0 File Offset: 0x0002A3D0
		public override Action CreateAction(string actionName, ShortList<Argument> arguments, string externalName = null)
		{
			if (actionName != null && actionName == "RightsProtectMessage")
			{
				return new RightsProtectMessageAction(arguments);
			}
			throw new ParserException(RulesStrings.InvalidActionName(actionName));
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002C201 File Offset: 0x0002A401
		public override Property CreateProperty(string propertyName, string typeName)
		{
			return new StringProperty(propertyName);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002C209 File Offset: 0x0002A409
		public override Property CreateProperty(string propertyName)
		{
			return this.CreateProperty(propertyName, null);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002C214 File Offset: 0x0002A414
		protected override void CreateRuleSubElements(Rule rule, XmlReader reader, RulesCreationContext creationContext)
		{
			OutlookProtectionRule outlookProtectionRule = (OutlookProtectionRule)rule;
			if (base.IsTag(reader, "userOverridable"))
			{
				outlookProtectionRule.UserOverridable = true;
				base.ReadNext(reader);
				return;
			}
			outlookProtectionRule.UserOverridable = false;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002C24C File Offset: 0x0002A44C
		public override PredicateCondition CreatePredicate(string name, Property property, ShortList<string> valueEntries, RulesCreationContext creationContext)
		{
			if (name != null)
			{
				if (!(name == "recipientIs"))
				{
					if (!(name == "allInternal"))
					{
						if (name == "is")
						{
							if (property == null || !string.Equals(property.Name, "Message.Sender.Department", StringComparison.OrdinalIgnoreCase))
							{
								throw new ParserException(RulesStrings.InvalidPropertyName((property != null) ? property.Name : string.Empty));
							}
							return base.CreatePredicate(name, property, valueEntries, creationContext);
						}
					}
					else
					{
						if (property == null || !string.Equals(property.Name, "Message.ToCcBcc", StringComparison.OrdinalIgnoreCase))
						{
							throw new ParserException(RulesStrings.InvalidPropertyName((property != null) ? property.Name : string.Empty));
						}
						return new AllInternalPredicate();
					}
				}
				else
				{
					if (property == null || !string.Equals(property.Name, "Message.ToCcBcc", StringComparison.OrdinalIgnoreCase))
					{
						throw new ParserException(RulesStrings.InvalidPropertyName((property != null) ? property.Name : string.Empty));
					}
					return new RecipientIsPredicate(valueEntries, creationContext);
				}
			}
			return base.CreatePredicate(name, property, valueEntries, creationContext);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002C346 File Offset: 0x0002A546
		public override Rule CreateRule(string ruleName)
		{
			return new OutlookProtectionRule(ruleName);
		}

		// Token: 0x040007FE RID: 2046
		private static readonly OutlookProtectionRuleParser instance = new OutlookProtectionRuleParser();
	}
}
