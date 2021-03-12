using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A4C RID: 2636
	public abstract class HygieneFilterRule : RulePresentationObjectBase
	{
		// Token: 0x06005E3F RID: 24127 RVA: 0x0018B30B File Offset: 0x0018950B
		protected HygieneFilterRule()
		{
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x0018B320 File Offset: 0x00189520
		protected HygieneFilterRule(TransportRule transportRule, string name, int priority, RuleState state, string comments, TransportRulePredicate[] conditions, TransportRulePredicate[] exceptions, ADIdParameter policyId) : base(transportRule)
		{
			if (transportRule == null)
			{
				base.Name = name;
			}
			this.Priority = priority;
			this.State = state;
			this.Comments = comments;
			this.conditions = conditions;
			this.exceptions = exceptions;
			this.PolicyId = policyId;
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x0018B378 File Offset: 0x00189578
		// (set) Token: 0x06005E42 RID: 24130 RVA: 0x0018B380 File Offset: 0x00189580
		public RuleState State { get; set; }

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0018B389 File Offset: 0x00189589
		// (set) Token: 0x06005E44 RID: 24132 RVA: 0x0018B391 File Offset: 0x00189591
		public int Priority { get; set; }

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x0018B39A File Offset: 0x0018959A
		// (set) Token: 0x06005E46 RID: 24134 RVA: 0x0018B3A2 File Offset: 0x001895A2
		public string Comments { get; set; }

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x0018B3AB File Offset: 0x001895AB
		public RuleDescription Description
		{
			get
			{
				return HygieneFilterRule.BuildRuleDescription(this);
			}
		}

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x0018B3B4 File Offset: 0x001895B4
		public Version RuleVersion
		{
			get
			{
				if (string.IsNullOrEmpty(base.TransportRuleXml))
				{
					return null;
				}
				Version result;
				try
				{
					TransportRule transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(base.TransportRuleXml);
					result = (transportRule.IsTooAdvancedToParse ? null : transportRule.MinimumVersion);
				}
				catch (ParserException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x0018B410 File Offset: 0x00189610
		// (set) Token: 0x06005E4A RID: 24138 RVA: 0x0018B418 File Offset: 0x00189618
		public RecipientIdParameter[] SentTo
		{
			get
			{
				return this.sentTo;
			}
			set
			{
				this.sentTo = value;
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x0018B421 File Offset: 0x00189621
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0018B429 File Offset: 0x00189629
		public RecipientIdParameter[] SentToMemberOf
		{
			get
			{
				return this.sentToMemberOf;
			}
			set
			{
				this.sentToMemberOf = value;
			}
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x0018B432 File Offset: 0x00189632
		// (set) Token: 0x06005E4E RID: 24142 RVA: 0x0018B43A File Offset: 0x0018963A
		public Word[] RecipientDomainIs
		{
			get
			{
				return this.recipientDomainIs;
			}
			set
			{
				this.recipientDomainIs = value;
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x0018B443 File Offset: 0x00189643
		// (set) Token: 0x06005E50 RID: 24144 RVA: 0x0018B44B File Offset: 0x0018964B
		public RecipientIdParameter[] ExceptIfSentTo
		{
			get
			{
				return this.exceptIfSentTo;
			}
			set
			{
				this.exceptIfSentTo = value;
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x0018B454 File Offset: 0x00189654
		// (set) Token: 0x06005E52 RID: 24146 RVA: 0x0018B45C File Offset: 0x0018965C
		public RecipientIdParameter[] ExceptIfSentToMemberOf
		{
			get
			{
				return this.exceptIfSentToMemberOf;
			}
			set
			{
				this.exceptIfSentToMemberOf = value;
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x06005E53 RID: 24147 RVA: 0x0018B465 File Offset: 0x00189665
		// (set) Token: 0x06005E54 RID: 24148 RVA: 0x0018B46D File Offset: 0x0018966D
		public Word[] ExceptIfRecipientDomainIs
		{
			get
			{
				return this.exceptIfRecipientDomainIs;
			}
			set
			{
				this.exceptIfRecipientDomainIs = value;
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x0018B476 File Offset: 0x00189676
		// (set) Token: 0x06005E56 RID: 24150 RVA: 0x0018B47E File Offset: 0x0018967E
		public TransportRulePredicate[] Conditions
		{
			get
			{
				return this.conditions;
			}
			set
			{
				this.conditions = value;
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x0018B487 File Offset: 0x00189687
		// (set) Token: 0x06005E58 RID: 24152 RVA: 0x0018B48F File Offset: 0x0018968F
		public TransportRulePredicate[] Exceptions
		{
			get
			{
				return this.exceptions;
			}
			set
			{
				this.exceptions = value;
			}
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x0018B498 File Offset: 0x00189698
		public override ValidationError[] Validate()
		{
			if (!this.isValid)
			{
				return new ValidationError[]
				{
					new ObjectValidationError(this.errorText, base.Identity, null)
				};
			}
			return ValidationError.None;
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x0018B4D0 File Offset: 0x001896D0
		// (set) Token: 0x06005E5B RID: 24155 RVA: 0x0018B4D8 File Offset: 0x001896D8
		protected ADIdParameter PolicyId { get; set; }

		// Token: 0x06005E5C RID: 24156 RVA: 0x0018B4E4 File Offset: 0x001896E4
		protected void SetParametersFromPredicate(TransportRulePredicate predicate, bool isException)
		{
			if (predicate is SentToPredicate)
			{
				if (isException)
				{
					this.exceptIfSentTo = Utils.BuildRecipientIdArray(((SentToPredicate)predicate).Addresses);
					return;
				}
				this.sentTo = Utils.BuildRecipientIdArray(((SentToPredicate)predicate).Addresses);
				return;
			}
			else
			{
				if (!(predicate is SentToMemberOfPredicate))
				{
					if (predicate is RecipientDomainIsPredicate)
					{
						if (isException)
						{
							this.exceptIfRecipientDomainIs = ((RecipientDomainIsPredicate)predicate).Words;
							return;
						}
						this.recipientDomainIs = ((RecipientDomainIsPredicate)predicate).Words;
					}
					return;
				}
				if (isException)
				{
					this.exceptIfSentToMemberOf = Utils.BuildRecipientIdArray(((SentToMemberOfPredicate)predicate).Addresses);
					return;
				}
				this.sentToMemberOf = Utils.BuildRecipientIdArray(((SentToMemberOfPredicate)predicate).Addresses);
				return;
			}
		}

		// Token: 0x06005E5D RID: 24157
		internal abstract IEnumerable<Microsoft.Exchange.MessagingPolicies.Rules.Action> CreateActions();

		// Token: 0x06005E5E RID: 24158
		internal abstract string BuildActionDescription();

		// Token: 0x06005E5F RID: 24159 RVA: 0x0018B591 File Offset: 0x00189791
		internal void SetPolicyId(ADIdParameter policyId)
		{
			this.PolicyId = policyId;
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x0018B59C File Offset: 0x0018979C
		internal TransportRule ToInternalRule()
		{
			AndCondition andCondition = new AndCondition();
			List<RuleBifurcationInfo> list = new List<RuleBifurcationInfo>();
			andCondition.SubConditions.Add(Microsoft.Exchange.MessagingPolicies.Rules.Condition.True);
			int num = -1;
			if (this.conditions != null)
			{
				foreach (TransportRulePredicate transportRulePredicate in this.conditions)
				{
					if (transportRulePredicate.Rank <= num)
					{
						throw new ArgumentException(Strings.InvalidPredicateSequence, "Conditions");
					}
					num = transportRulePredicate.Rank;
					if (transportRulePredicate is BifurcationInfoPredicate)
					{
						BifurcationInfoPredicate bifurcationInfoPredicate = (BifurcationInfoPredicate)transportRulePredicate;
						RuleBifurcationInfo ruleBifurcationInfo;
						RuleBifurcationInfo item = bifurcationInfoPredicate.ToRuleBifurcationInfo(out ruleBifurcationInfo);
						list.Add(item);
						if (ruleBifurcationInfo != null)
						{
							list.Add(ruleBifurcationInfo);
						}
					}
					else
					{
						Microsoft.Exchange.MessagingPolicies.Rules.Condition item2 = transportRulePredicate.ToInternalCondition();
						andCondition.SubConditions.Add(item2);
					}
				}
			}
			if (this.exceptions != null && this.exceptions.Length > 0)
			{
				OrCondition orCondition = new OrCondition();
				andCondition.SubConditions.Add(new NotCondition(orCondition));
				num = -1;
				foreach (TransportRulePredicate transportRulePredicate2 in this.exceptions)
				{
					if (transportRulePredicate2.Rank <= num)
					{
						throw new ArgumentException(Strings.InvalidPredicateSequence, "Exceptions");
					}
					num = transportRulePredicate2.Rank;
					if (transportRulePredicate2 is BifurcationInfoPredicate)
					{
						BifurcationInfoPredicate bifurcationInfoPredicate2 = (BifurcationInfoPredicate)transportRulePredicate2;
						RuleBifurcationInfo ruleBifurcationInfo3;
						RuleBifurcationInfo ruleBifurcationInfo2 = bifurcationInfoPredicate2.ToRuleBifurcationInfo(out ruleBifurcationInfo3);
						ruleBifurcationInfo2.Exception = true;
						list.Add(ruleBifurcationInfo2);
						if (ruleBifurcationInfo3 != null)
						{
							ruleBifurcationInfo3.Exception = true;
							list.Add(ruleBifurcationInfo3);
						}
					}
					else
					{
						Microsoft.Exchange.MessagingPolicies.Rules.Condition item3 = transportRulePredicate2.ToInternalCondition();
						orCondition.SubConditions.Add(item3);
					}
				}
				if (orCondition.SubConditions.Count == 0)
				{
					orCondition.SubConditions.Add(Microsoft.Exchange.MessagingPolicies.Rules.Condition.False);
				}
			}
			TransportRule transportRule = new TransportRule(base.Name, andCondition);
			transportRule.Enabled = this.State;
			transportRule.Comments = this.Comments;
			if (list.Count > 0)
			{
				transportRule.Fork = list;
			}
			foreach (Microsoft.Exchange.MessagingPolicies.Rules.Action item4 in this.CreateActions())
			{
				transportRule.Actions.Add(item4);
			}
			return transportRule;
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x0018B7E4 File Offset: 0x001899E4
		internal override void SuppressPiiData(PiiMap piiMap)
		{
			base.SuppressPiiData(piiMap);
			if (this.conditions != null)
			{
				foreach (TransportRulePredicate transportRulePredicate in this.conditions)
				{
					transportRulePredicate.SuppressPiiData();
				}
			}
			if (this.exceptions != null)
			{
				foreach (TransportRulePredicate transportRulePredicate2 in this.exceptions)
				{
					transportRulePredicate2.SuppressPiiData();
				}
			}
			this.sentTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(HygieneFilterRuleSchema.SentTo, this.sentTo);
			this.sentToMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(HygieneFilterRuleSchema.SentToMemberOf, this.sentToMemberOf);
			this.recipientDomainIs = SuppressingPiiProperty.TryRedactValue<Word[]>(HygieneFilterRuleSchema.RecipientDomainIs, this.recipientDomainIs);
			this.exceptIfSentTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(HygieneFilterRuleSchema.ExceptIfSentTo, this.exceptIfSentTo);
			this.exceptIfSentToMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(HygieneFilterRuleSchema.ExceptIfSentToMemberOf, this.exceptIfSentToMemberOf);
			this.exceptIfRecipientDomainIs = SuppressingPiiProperty.TryRedactValue<Word[]>(HygieneFilterRuleSchema.ExceptIfRecipientDomainIs, this.exceptIfRecipientDomainIs);
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0018B8D4 File Offset: 0x00189AD4
		private static RuleDescription BuildRuleDescription(HygieneFilterRule rule)
		{
			RuleDescription ruleDescription = new RuleDescription();
			if (rule.Conditions != null && rule.Conditions.Length > 0)
			{
				foreach (TransportRulePredicate transportRulePredicate in rule.Conditions)
				{
					ruleDescription.ConditionDescriptions.Add(transportRulePredicate.Description);
				}
			}
			ruleDescription.ActionDescriptions.Add(rule.BuildActionDescription());
			if (rule.Exceptions != null && rule.Exceptions.Length > 0)
			{
				foreach (TransportRulePredicate transportRulePredicate2 in rule.Exceptions)
				{
					ruleDescription.ExceptionDescriptions.Add(transportRulePredicate2.Description);
				}
			}
			return ruleDescription;
		}

		// Token: 0x040034D4 RID: 13524
		internal const string MalwareFilterVersioned = "MalwareFilterVersioned";

		// Token: 0x040034D5 RID: 13525
		internal const string HostedContentFilterVersioned = "HostedContentFilterVersioned";

		// Token: 0x040034D6 RID: 13526
		internal const string XMSExchangeMalwareFilterPolicyHeader = "X-MS-Exchange-Organization-MalwareFilterPolicy";

		// Token: 0x040034D7 RID: 13527
		internal const string XMSExchangeHostedContentFilterPolicyHeader = "X-MS-Exchange-Organization-HostedContentFilterPolicy";

		// Token: 0x040034D8 RID: 13528
		private RecipientIdParameter[] sentTo;

		// Token: 0x040034D9 RID: 13529
		private RecipientIdParameter[] sentToMemberOf;

		// Token: 0x040034DA RID: 13530
		private Word[] recipientDomainIs;

		// Token: 0x040034DB RID: 13531
		private RecipientIdParameter[] exceptIfSentTo;

		// Token: 0x040034DC RID: 13532
		private RecipientIdParameter[] exceptIfSentToMemberOf;

		// Token: 0x040034DD RID: 13533
		private Word[] exceptIfRecipientDomainIs;

		// Token: 0x040034DE RID: 13534
		private TransportRulePredicate[] conditions;

		// Token: 0x040034DF RID: 13535
		private TransportRulePredicate[] exceptions;

		// Token: 0x040034E0 RID: 13536
		protected LocalizedString errorText = LocalizedString.Empty;

		// Token: 0x040034E1 RID: 13537
		internal static TypeMapping[] SupportedPredicates = new TypeMapping[]
		{
			new TypeMapping("SentTo", typeof(SentToPredicate), RulesTasksStrings.LinkedPredicateSentTo, RulesTasksStrings.LinkedPredicateSentToException),
			new TypeMapping("SentToMemberOf", typeof(SentToMemberOfPredicate), RulesTasksStrings.LinkedPredicateSentToMemberOf, RulesTasksStrings.LinkedPredicateSentToMemberOfException),
			new TypeMapping("RecipientDomainIs", typeof(RecipientDomainIsPredicate), RulesTasksStrings.LinkedPredicateRecipientDomainIs, RulesTasksStrings.LinkedPredicateRecipientDomainIsException)
		};

		// Token: 0x040034E2 RID: 13538
		internal static TypeMapping[] SupportedActions = new TypeMapping[]
		{
			new TypeMapping("SetHeader", typeof(SetHeaderAction), RulesTasksStrings.LinkedActionSetHeader),
			new TypeMapping("StopRuleProcessing", typeof(StopRuleProcessingAction), RulesTasksStrings.LinkedActionStopRuleProcessing)
		};
	}
}
