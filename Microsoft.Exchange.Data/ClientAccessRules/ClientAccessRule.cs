using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	internal class ClientAccessRule : Rule
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x0001D2B1 File Offset: 0x0001B4B1
		public ClientAccessRule(string name) : base(name, null)
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001D2BB File Offset: 0x0001B4BB
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0001D2C3 File Offset: 0x0001B4C3
		public ObjectId Identity { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(RulesTasksStrings.NegativePriority, "Priority");
				}
				this.priority = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001D2F6 File Offset: 0x0001B4F6
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0001D2FE File Offset: 0x0001B4FE
		public bool DatacenterAdminsOnly { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001D307 File Offset: 0x0001B507
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0001D30F File Offset: 0x0001B50F
		public IPRange[] AnyOfClientIPAddressesOrRanges { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0001D318 File Offset: 0x0001B518
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0001D320 File Offset: 0x0001B520
		public IPRange[] ExceptAnyOfClientIPAddressesOrRanges { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001D329 File Offset: 0x0001B529
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0001D331 File Offset: 0x0001B531
		public IntRange[] AnyOfSourceTcpPortNumbers { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001D33A File Offset: 0x0001B53A
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0001D342 File Offset: 0x0001B542
		public IntRange[] ExceptAnyOfSourceTcpPortNumbers { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001D34B File Offset: 0x0001B54B
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001D353 File Offset: 0x0001B553
		public string[] UsernameMatchesAnyOfPatterns { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001D35C File Offset: 0x0001B55C
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001D364 File Offset: 0x0001B564
		public string[] ExceptUsernameMatchesAnyOfPatterns { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001D36D File Offset: 0x0001B56D
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0001D375 File Offset: 0x0001B575
		public string[] UserIsMemberOf { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001D37E File Offset: 0x0001B57E
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0001D386 File Offset: 0x0001B586
		public string[] ExceptUserIsMemberOf { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001D38F File Offset: 0x0001B58F
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0001D397 File Offset: 0x0001B597
		public ClientAccessAuthenticationMethod[] AnyOfAuthenticationTypes { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public ClientAccessAuthenticationMethod[] ExceptAnyOfAuthenticationTypes { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001D3B1 File Offset: 0x0001B5B1
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0001D3B9 File Offset: 0x0001B5B9
		public ClientAccessProtocol[] AnyOfProtocols { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001D3C2 File Offset: 0x0001B5C2
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0001D3CA File Offset: 0x0001B5CA
		public ClientAccessProtocol[] ExceptAnyOfProtocols { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001D3D3 File Offset: 0x0001B5D3
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0001D3DB File Offset: 0x0001B5DB
		public ClientAccessRulesAction Action { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		public string UserRecipientFilter { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001D3F5 File Offset: 0x0001B5F5
		public string Xml
		{
			get
			{
				this.PopulateRuleConditionAndAction();
				return ClientAccessRuleSerializer.Instance.SaveRuleToString(this);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001D408 File Offset: 0x0001B608
		public override Version MinimumVersion
		{
			get
			{
				Version version = ClientAccessRule.ClientAccessRuleBaseVersion;
				foreach (Microsoft.Exchange.MessagingPolicies.Rules.Action action in base.Actions)
				{
					version = ((version > action.MinimumVersion) ? version : action.MinimumVersion);
				}
				if (base.Condition.MinimumVersion > version)
				{
					return base.Condition.MinimumVersion;
				}
				return version;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001D494 File Offset: 0x0001B694
		public override int GetEstimatedSize()
		{
			int num = 5;
			return num + base.GetEstimatedSize();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		public static ClientAccessRule FromADProperties(string xml, ObjectId identity, string name, int priority, bool enabled, bool datacenterAdminsOnly, bool populatePresentationProperties)
		{
			ClientAccessRule clientAccessRule = (ClientAccessRule)ClientAccessRuleParser.Instance.GetRule(xml);
			clientAccessRule.Identity = identity;
			clientAccessRule.Name = name;
			clientAccessRule.Priority = priority;
			clientAccessRule.Enabled = (enabled ? RuleState.Enabled : RuleState.Disabled);
			clientAccessRule.DatacenterAdminsOnly = datacenterAdminsOnly;
			if (populatePresentationProperties)
			{
				if (clientAccessRule.Actions.Count > 0)
				{
					clientAccessRule.Action = (ClientAccessRulesAction)Enum.Parse(typeof(ClientAccessRulesAction), clientAccessRule.Actions[0].ExternalName);
				}
				ClientAccessRule.FillParametersFromCondition(clientAccessRule, clientAccessRule.Condition, false);
			}
			return clientAccessRule;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001D550 File Offset: 0x0001B750
		private void AddPredicateToCondition<T>(List<Condition> subConditions, string conditionTag, string propertyName, bool negateCondition, T[] values)
		{
			if (values == null || values.Length == 0)
			{
				return;
			}
			PredicateCondition predicateCondition = ClientAccessRuleParser.Instance.CreatePredicate(conditionTag, ClientAccessRuleParser.Instance.CreateProperty(propertyName), from value in values
			select value.ToString());
			if (negateCondition)
			{
				subConditions.Add(new NotCondition(predicateCondition));
				return;
			}
			subConditions.Add(predicateCondition);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		private void PopulateRuleConditionAndAction()
		{
			AndCondition andCondition = new AndCondition();
			andCondition.SubConditions.Add(Condition.True);
			this.AddPredicateToCondition<IPRange>(andCondition.SubConditions, "anyOfClientIPAddressesOrRangesPredicate", "ClientIpProperty", false, this.AnyOfClientIPAddressesOrRanges);
			this.AddPredicateToCondition<IntRange>(andCondition.SubConditions, "anyOfSourceTcpPortNumbersPredicate", "SourceTcpPortNumberProperty", false, this.AnyOfSourceTcpPortNumbers);
			this.AddPredicateToCondition<ClientAccessProtocol>(andCondition.SubConditions, "anyOfProtocolsPredicate", "ProtocolProperty", false, this.AnyOfProtocols);
			this.AddPredicateToCondition<string>(andCondition.SubConditions, "usernameMatchesAnyOfPatternsPredicate", "UsernamePatternProperty", false, this.UsernameMatchesAnyOfPatterns);
			this.AddPredicateToCondition<ClientAccessAuthenticationMethod>(andCondition.SubConditions, "anyOfAuthenticationTypesPredicate", "AuthenticationTypeProperty", false, this.AnyOfAuthenticationTypes);
			if (!string.IsNullOrEmpty(this.UserRecipientFilter))
			{
				this.AddPredicateToCondition<string>(andCondition.SubConditions, "UserRecipientFilterPredicate", "UserRecipientFilterProperty", false, new string[]
				{
					this.UserRecipientFilter
				});
			}
			this.AddPredicateToCondition<IPRange>(andCondition.SubConditions, "anyOfClientIPAddressesOrRangesPredicate", "ClientIpProperty", true, this.ExceptAnyOfClientIPAddressesOrRanges);
			this.AddPredicateToCondition<IntRange>(andCondition.SubConditions, "anyOfSourceTcpPortNumbersPredicate", "SourceTcpPortNumberProperty", true, this.ExceptAnyOfSourceTcpPortNumbers);
			this.AddPredicateToCondition<ClientAccessProtocol>(andCondition.SubConditions, "anyOfProtocolsPredicate", "ProtocolProperty", true, this.ExceptAnyOfProtocols);
			this.AddPredicateToCondition<string>(andCondition.SubConditions, "usernameMatchesAnyOfPatternsPredicate", "UsernamePatternProperty", true, this.ExceptUsernameMatchesAnyOfPatterns);
			this.AddPredicateToCondition<ClientAccessAuthenticationMethod>(andCondition.SubConditions, "anyOfAuthenticationTypesPredicate", "AuthenticationTypeProperty", true, this.ExceptAnyOfAuthenticationTypes);
			base.Condition = andCondition;
			base.Actions.Clear();
			switch (this.Action)
			{
			case ClientAccessRulesAction.AllowAccess:
				base.Actions.Add(ClientAccessRuleParser.Instance.CreateAction("AllowAccess", new ShortList<Argument>(), this.Action.ToString()));
				return;
			case ClientAccessRulesAction.DenyAccess:
				base.Actions.Add(ClientAccessRuleParser.Instance.CreateAction("DenyAccess", new ShortList<Argument>(), this.Action.ToString()));
				return;
			default:
				throw new ClientAccessRuleActionNotSupportedException(ClientAccessRulesAction.DenyAccess.ToString());
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		private static void FillParametersFromCondition(ClientAccessRule rule, Condition condition, bool wasNegated)
		{
			if (condition is AndCondition)
			{
				AndCondition andCondition = condition as AndCondition;
				foreach (Condition condition2 in andCondition.SubConditions)
				{
					ClientAccessRule.FillParametersFromCondition(rule, condition2, wasNegated);
				}
			}
			if (condition is NotCondition)
			{
				ClientAccessRule.FillParametersFromCondition(rule, ((NotCondition)condition).SubCondition, !wasNegated);
			}
			if (condition is AnyOfClientIPAddressesOrRangesPredicate)
			{
				AnyOfClientIPAddressesOrRangesPredicate anyOfClientIPAddressesOrRangesPredicate = condition as AnyOfClientIPAddressesOrRangesPredicate;
				if (wasNegated)
				{
					rule.ExceptAnyOfClientIPAddressesOrRanges = anyOfClientIPAddressesOrRangesPredicate.TargetIpRanges.ToArray<IPRange>();
				}
				else
				{
					rule.AnyOfClientIPAddressesOrRanges = anyOfClientIPAddressesOrRangesPredicate.TargetIpRanges.ToArray<IPRange>();
				}
			}
			if (condition is AnyOfSourceTcpPortNumbersPredicate)
			{
				AnyOfSourceTcpPortNumbersPredicate anyOfSourceTcpPortNumbersPredicate = condition as AnyOfSourceTcpPortNumbersPredicate;
				if (wasNegated)
				{
					rule.ExceptAnyOfSourceTcpPortNumbers = anyOfSourceTcpPortNumbersPredicate.TargetPortRanges.ToArray<IntRange>();
				}
				else
				{
					rule.AnyOfSourceTcpPortNumbers = anyOfSourceTcpPortNumbersPredicate.TargetPortRanges.ToArray<IntRange>();
				}
			}
			if (condition is AnyOfProtocolsPredicate)
			{
				AnyOfProtocolsPredicate anyOfProtocolsPredicate = condition as AnyOfProtocolsPredicate;
				if (wasNegated)
				{
					rule.ExceptAnyOfProtocols = anyOfProtocolsPredicate.ProtocolList.ToArray<ClientAccessProtocol>();
				}
				else
				{
					rule.AnyOfProtocols = anyOfProtocolsPredicate.ProtocolList.ToArray<ClientAccessProtocol>();
				}
			}
			if (condition is UsernameMatchesAnyOfPatternsPredicate)
			{
				UsernameMatchesAnyOfPatternsPredicate usernameMatchesAnyOfPatternsPredicate = condition as UsernameMatchesAnyOfPatternsPredicate;
				if (wasNegated)
				{
					rule.ExceptUsernameMatchesAnyOfPatterns = usernameMatchesAnyOfPatternsPredicate.RegexPatterns.Select(new Func<Regex, string>(ClientAccessRulesUsernamePatternProperty.GetDisplayValue)).ToArray<string>();
				}
				else
				{
					rule.UsernameMatchesAnyOfPatterns = usernameMatchesAnyOfPatternsPredicate.RegexPatterns.Select(new Func<Regex, string>(ClientAccessRulesUsernamePatternProperty.GetDisplayValue)).ToArray<string>();
				}
			}
			if (condition is AnyOfAuthenticationTypesPredicate)
			{
				AnyOfAuthenticationTypesPredicate anyOfAuthenticationTypesPredicate = condition as AnyOfAuthenticationTypesPredicate;
				if (wasNegated)
				{
					rule.ExceptAnyOfAuthenticationTypes = anyOfAuthenticationTypesPredicate.AuthenticationTypeList.ToArray<ClientAccessAuthenticationMethod>();
				}
				else
				{
					rule.AnyOfAuthenticationTypes = anyOfAuthenticationTypesPredicate.AuthenticationTypeList.ToArray<ClientAccessAuthenticationMethod>();
				}
			}
			if (condition is UserRecipientFilterPredicate)
			{
				UserRecipientFilterPredicate userRecipientFilterPredicate = condition as UserRecipientFilterPredicate;
				if (!wasNegated)
				{
					rule.UserRecipientFilter = userRecipientFilterPredicate.UserRecipientFilter;
				}
			}
		}

		// Token: 0x040005D7 RID: 1495
		private static readonly Version ClientAccessRuleBaseVersion = new Version("15.00.0008.00");

		// Token: 0x040005D8 RID: 1496
		private int priority;
	}
}
