using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200021B RID: 539
	internal class PolicyTipRuleParser : RuleParser
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000498FD File Offset: 0x00047AFD
		public static PolicyTipRuleParser Instance
		{
			get
			{
				return PolicyTipRuleParser.instance;
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00049904 File Offset: 0x00047B04
		public override Rule CreateRule(string name)
		{
			return new PolicyTipRule(name);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0004990C File Offset: 0x00047B0C
		protected override void CreateRuleSubElements(Rule rule, XmlReader reader, RulesCreationContext creationContext)
		{
			PolicyTipRule policyTipRule = (PolicyTipRule)rule;
			List<Condition> list = new List<Condition>();
			while (base.IsTag(reader, "fork"))
			{
				list.Add(this.ParseFork(reader, creationContext));
				base.ReadNext(reader);
			}
			if (list.Count > 0)
			{
				policyTipRule.ForkConditions = list;
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0004995C File Offset: 0x00047B5C
		public override Microsoft.Exchange.MessagingPolicies.Rules.Action CreateAction(string actionName, ShortList<Argument> arguments, string externalName = null)
		{
			if (actionName != null && actionName == "SenderNotify")
			{
				return new SenderNotify(arguments);
			}
			return new NoopAction(arguments);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00049988 File Offset: 0x00047B88
		public override Property CreateProperty(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new RulesValidationException(RulesStrings.EmptyPropertyName);
			}
			return MessageProperty.Create(propertyName);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000499A3 File Offset: 0x00047BA3
		public override Property CreateProperty(string propertyName, string typeName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new RulesValidationException(RulesStrings.EmptyPropertyName);
			}
			return this.CreateProperty(propertyName);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000499BF File Offset: 0x00047BBF
		public override PredicateCondition CreatePredicate(string name, Property property, ShortList<ShortList<KeyValuePair<string, string>>> valueEntries, RulesCreationContext creationContext)
		{
			if (name == "containsDataClassification")
			{
				return new ContainsDataClassificationPredicate(property, valueEntries, creationContext);
			}
			return new UnconditionalTruePredicate();
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000499E0 File Offset: 0x00047BE0
		public override PredicateCondition CreatePredicate(string name, Property property, ShortList<string> valueEntries, RulesCreationContext creationContext)
		{
			if (name != null)
			{
				if (name == "isSameUser")
				{
					return new OwaIsSameUserPredicate(property, valueEntries, creationContext);
				}
				if (name == "isMemberOf")
				{
					return new IsMemberOfPredicate(property, valueEntries, creationContext);
				}
			}
			return new UnconditionalTruePredicate();
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00049A28 File Offset: 0x00047C28
		private Condition ParseFork(XmlReader reader, RulesCreationContext creationContext)
		{
			base.VerifyNotEmptyTag(reader);
			bool flag = false;
			string attribute = reader.GetAttribute("exception");
			if (attribute != null)
			{
				bool.TryParse(attribute, out flag);
			}
			ShortList<string> shortList = new ShortList<string>();
			ScopeType scopeType = ScopeType.None;
			base.ReadNext(reader);
			for (;;)
			{
				if (base.IsTag(reader, "recipient"))
				{
					string requiredAttribute = base.GetRequiredAttribute(reader, "address");
					shortList.Add(requiredAttribute);
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "recipient");
					}
					base.ReadNext(reader);
				}
				else if (base.IsTag(reader, "external"))
				{
					scopeType = ScopeType.External;
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "external");
					}
					base.ReadNext(reader);
				}
				else if (base.IsTag(reader, "internal"))
				{
					scopeType = ScopeType.Internal;
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "internal");
					}
					base.ReadNext(reader);
				}
				else if (base.IsTag(reader, "externalPartner"))
				{
					scopeType = ScopeType.ExternalPartner;
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "externalPartner");
					}
					base.ReadNext(reader);
				}
				else if (base.IsTag(reader, "externalNonPartner"))
				{
					scopeType = ScopeType.ExternalNonPartner;
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "externalNonPartner");
					}
					base.ReadNext(reader);
				}
				else
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name.Equals("fork"))
					{
						break;
					}
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (!reader.IsEmptyElement)
						{
							base.ReadNext(reader);
							base.VerifyEndTag(reader, reader.Name);
						}
						base.ReadNext(reader);
					}
				}
			}
			Condition result = new UnconditionalTruePredicate();
			if (shortList.Count > 0)
			{
				SentToPredicate sentToPredicate = new SentToPredicate(MessageProperty.Create("Message.To"), shortList, creationContext);
				result = (flag ? new NotCondition(sentToPredicate) : sentToPredicate);
			}
			else if (scopeType != ScopeType.None)
			{
				SentToScopePredicate sentToScopePredicate = new SentToScopePredicate(MessageProperty.Create("Message.To"), scopeType, creationContext);
				result = (flag ? new NotCondition(sentToScopePredicate) : sentToScopePredicate);
			}
			return result;
		}

		// Token: 0x04000B40 RID: 2880
		private static readonly HashSet<string> IgnroedForkTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000B41 RID: 2881
		private static PolicyTipRuleParser instance = new PolicyTipRuleParser();
	}
}
