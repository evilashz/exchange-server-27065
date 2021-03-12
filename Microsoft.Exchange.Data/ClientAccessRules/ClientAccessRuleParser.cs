using System;
using System.Collections.Generic;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x0200010B RID: 267
	internal class ClientAccessRuleParser : RuleParser
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001DB0E File Offset: 0x0001BD0E
		public static ClientAccessRuleParser Instance
		{
			get
			{
				return ClientAccessRuleParser.instance;
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001DB15 File Offset: 0x0001BD15
		public override RuleCollection CreateRuleCollection(string name)
		{
			return new ClientAccessRuleCollection(name);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001DB1D File Offset: 0x0001BD1D
		public override Rule CreateRule(string name)
		{
			return new ClientAccessRule(name);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001DB28 File Offset: 0x0001BD28
		public override Microsoft.Exchange.MessagingPolicies.Rules.Action CreateAction(string actionName, ShortList<Argument> arguments, string externalName = null)
		{
			ClientAccessRuleAction clientAccessRuleAction = ClientAccessRuleParser.CreateAction(actionName, arguments);
			clientAccessRuleAction.ExternalName = externalName;
			return clientAccessRuleAction;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001DB45 File Offset: 0x0001BD45
		private static ClientAccessRuleAction CreateAction(string actionName, ShortList<Argument> arguments)
		{
			if (string.Compare(actionName, "AllowAccess", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return new ClientAccessRuleAllowAccessAction(arguments);
			}
			if (string.Compare(actionName, "DenyAccess", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return new ClientAccessRuleDenyAccessAction(arguments);
			}
			throw new RulesValidationException(RulesStrings.InvalidActionName(actionName));
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		public override Property CreateProperty(string propertyName)
		{
			if (propertyName != null)
			{
				if (propertyName == "ClientIpProperty")
				{
					return new ClientAccessRulesClientIpProperty(propertyName, typeof(string));
				}
				if (propertyName == "SourceTcpPortNumberProperty")
				{
					return new ClientAccessRulesSourcePortNumberProperty(propertyName, typeof(string));
				}
				if (propertyName == "ProtocolProperty")
				{
					return new ClientAccessRulesProtocolProperty(propertyName, typeof(string));
				}
				if (propertyName == "UsernamePatternProperty")
				{
					return new ClientAccessRulesUsernamePatternProperty(propertyName, typeof(string));
				}
				if (propertyName == "AuthenticationTypeProperty")
				{
					return new ClientAccessRulesAuthenticationTypeProperty(propertyName, typeof(string));
				}
				if (propertyName == "UserRecipientFilterProperty")
				{
					return new ClientAccessRulesUserRecipientFilterProperty(propertyName, typeof(string));
				}
			}
			throw new RulesValidationException(RulesStrings.InvalidPropertyName(propertyName));
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public override Property CreateProperty(string propertyName, string typeName)
		{
			if (propertyName != null)
			{
				if (propertyName == "ClientIpProperty")
				{
					return new ClientAccessRulesClientIpProperty(propertyName, Argument.GetTypeForName(typeName));
				}
				if (propertyName == "SourceTcpPortNumberProperty")
				{
					return new ClientAccessRulesSourcePortNumberProperty(propertyName, Argument.GetTypeForName(typeName));
				}
				if (propertyName == "ProtocolProperty")
				{
					return new ClientAccessRulesProtocolProperty(propertyName, Argument.GetTypeForName(typeName));
				}
				if (propertyName == "UsernamePatternProperty")
				{
					return new ClientAccessRulesUsernamePatternProperty(propertyName, Argument.GetTypeForName(typeName));
				}
				if (propertyName == "AuthenticationTypeProperty")
				{
					return new ClientAccessRulesAuthenticationTypeProperty(propertyName, Argument.GetTypeForName(typeName));
				}
				if (propertyName == "UserRecipientFilterProperty")
				{
					return new ClientAccessRulesUserRecipientFilterProperty(propertyName, Argument.GetTypeForName(typeName));
				}
			}
			throw new RulesValidationException(RulesStrings.InvalidPropertyName(propertyName));
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001DD12 File Offset: 0x0001BF12
		public PredicateCondition CreatePredicate(string name, Property property, IEnumerable<string> valueEntries)
		{
			return base.CreatePredicate(name, property, new ShortList<string>(valueEntries));
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001DD24 File Offset: 0x0001BF24
		public override PredicateCondition CreatePredicate(string name, Property property, ShortList<string> valueEntries, RulesCreationContext creationContext)
		{
			if (name != null)
			{
				if (name == "anyOfClientIPAddressesOrRangesPredicate")
				{
					return new AnyOfClientIPAddressesOrRangesPredicate(property, valueEntries, creationContext);
				}
				if (name == "anyOfSourceTcpPortNumbersPredicate")
				{
					return new AnyOfSourceTcpPortNumbersPredicate(property, valueEntries, creationContext);
				}
				if (name == "anyOfProtocolsPredicate")
				{
					return new AnyOfProtocolsPredicate(property, valueEntries, creationContext);
				}
				if (name == "usernameMatchesAnyOfPatternsPredicate")
				{
					return new UsernameMatchesAnyOfPatternsPredicate(property, valueEntries, creationContext);
				}
				if (name == "anyOfAuthenticationTypesPredicate")
				{
					return new AnyOfAuthenticationTypesPredicate(property, valueEntries, creationContext);
				}
				if (name == "UserRecipientFilterPredicate")
				{
					return new UserRecipientFilterPredicate(property, valueEntries, creationContext);
				}
			}
			return base.CreatePredicate(name, property, valueEntries, creationContext);
		}

		// Token: 0x040005F2 RID: 1522
		private static ClientAccessRuleParser instance = new ClientAccessRuleParser();
	}
}
