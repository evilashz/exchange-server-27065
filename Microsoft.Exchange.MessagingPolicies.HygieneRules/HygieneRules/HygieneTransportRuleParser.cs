using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000009 RID: 9
	internal class HygieneTransportRuleParser : RuleParser
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026DC File Offset: 0x000008DC
		public static HygieneTransportRuleParser Instance
		{
			get
			{
				return HygieneTransportRuleParser.instance;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026E3 File Offset: 0x000008E3
		public override RuleCollection CreateRuleCollection(string name)
		{
			return new RuleCollection(name);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026EB File Offset: 0x000008EB
		public override Rule CreateRule(string name)
		{
			return new HygieneTransportRule(name);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026F4 File Offset: 0x000008F4
		public override Microsoft.Exchange.MessagingPolicies.Rules.Action CreateAction(string actionName, ShortList<Argument> arguments, string externalName = null)
		{
			if (actionName != null)
			{
				Microsoft.Exchange.MessagingPolicies.Rules.Action action;
				if (!(actionName == "Halt"))
				{
					if (!(actionName == "SetHeader"))
					{
						goto IL_35;
					}
					action = new SetHeaderAction(arguments);
				}
				else
				{
					action = new HaltAction(arguments);
				}
				action.ExternalName = externalName;
				return action;
			}
			IL_35:
			throw new RulesValidationException(RulesStrings.InvalidActionName(actionName));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000274C File Offset: 0x0000094C
		public override Property CreateProperty(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new RulesValidationException(RulesStrings.EmptyPropertyName);
			}
			if (!propertyName.StartsWith("Message.Headers"))
			{
				return MessageProperty.Create(propertyName);
			}
			if (propertyName["Message.Headers".Length] != ':')
			{
				throw new RulesValidationException(RulesStrings.DelimeterNotFound("Message.Headers"));
			}
			string str = propertyName.Substring("Message.Headers".Length + 1);
			return new HeaderProperty(string.Intern(str));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027C2 File Offset: 0x000009C2
		public override Property CreateProperty(string propertyName, string typeName)
		{
			return this.CreateProperty(propertyName);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027CC File Offset: 0x000009CC
		protected override void CreateRuleSubElements(Rule rule, XmlReader reader, RulesCreationContext creationContext)
		{
			HygieneTransportRule hygieneTransportRule = (HygieneTransportRule)rule;
			List<BifurcationInfo> list = new List<BifurcationInfo>();
			while (base.IsTag(reader, "fork"))
			{
				list.Add(this.ParseFork(reader));
				base.ReadNext(reader);
			}
			if (list.Count > 0)
			{
				hygieneTransportRule.Fork = list;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000281C File Offset: 0x00000A1C
		private BifurcationInfo ParseFork(XmlReader reader)
		{
			base.VerifyNotEmptyTag(reader);
			BifurcationInfo bifurcationInfo = new BifurcationInfo();
			string attribute = reader.GetAttribute("exception");
			bool exception;
			if (attribute != null && bool.TryParse(attribute, out exception))
			{
				bifurcationInfo.Exception = exception;
			}
			base.ReadNext(reader);
			for (;;)
			{
				if (base.IsTag(reader, "recipientDomainIs"))
				{
					string requiredAttribute = base.GetRequiredAttribute(reader, "value");
					bifurcationInfo.RecipientDomainIs.Add(requiredAttribute);
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "recipientDomainIs");
					}
					base.ReadNext(reader);
				}
				else if (base.IsTag(reader, "recipient"))
				{
					string requiredSmtpAddressAttribute = this.GetRequiredSmtpAddressAttribute(reader, "address");
					bifurcationInfo.Recipients.Add(requiredSmtpAddressAttribute);
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "recipient");
					}
					base.ReadNext(reader);
				}
				else
				{
					if (!base.IsTag(reader, "list"))
					{
						break;
					}
					string requiredSmtpAddressAttribute2 = this.GetRequiredSmtpAddressAttribute(reader, "name");
					bifurcationInfo.Lists.Add(requiredSmtpAddressAttribute2);
					if (!reader.IsEmptyElement)
					{
						base.ReadNext(reader);
						base.VerifyEndTag(reader, "list");
					}
					base.ReadNext(reader);
				}
			}
			base.VerifyEndTag(reader, "fork");
			return bifurcationInfo;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002958 File Offset: 0x00000B58
		private string GetRequiredSmtpAddressAttribute(XmlReader reader, string attributeName)
		{
			string requiredAttribute = base.GetRequiredAttribute(reader, attributeName);
			RoutingAddress routingAddress = new RoutingAddress(requiredAttribute);
			if (!routingAddress.IsValid)
			{
				throw new ParserException(HygieneRulesStrings.InvalidAddress(requiredAttribute), reader);
			}
			return requiredAttribute;
		}

		// Token: 0x04000015 RID: 21
		private static HygieneTransportRuleParser instance = new HygieneTransportRuleParser();
	}
}
