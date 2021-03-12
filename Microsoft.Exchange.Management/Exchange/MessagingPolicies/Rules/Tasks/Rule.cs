using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B6A RID: 2922
	[Serializable]
	public class Rule : RulePresentationObjectBase
	{
		// Token: 0x06006B30 RID: 27440 RVA: 0x001B9C5D File Offset: 0x001B7E5D
		public Rule()
		{
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x001B9C70 File Offset: 0x001B7E70
		internal Rule(TransportRule transportRule, string name, int priority, string dlpPolicy, Guid dlpPolicyId, string comments, bool manuallyModified, DateTime? activationDate, DateTime? expiryDate, TransportRulePredicate[] conditions, TransportRulePredicate[] exceptions, TransportRuleAction[] actions, RuleState state, RuleMode mode, RuleSubType subType, RuleErrorAction ruleErrorAction, SenderAddressLocation senderAddressLocation = SenderAddressLocation.Header) : base(transportRule)
		{
			if (transportRule == null)
			{
				base.Name = name;
			}
			this.priority = priority;
			this.dlpPolicy = dlpPolicy;
			this.dlpPolicyId = dlpPolicyId;
			this.comments = comments;
			this.manuallyModified = manuallyModified;
			this.activationDate = activationDate;
			this.expiryDate = expiryDate;
			this.conditions = conditions;
			this.exceptions = exceptions;
			this.actions = actions;
			this.State = state;
			this.Mode = mode;
			this.RuleErrorAction = ruleErrorAction;
			this.SenderAddressLocation = senderAddressLocation;
			this.RuleSubType = subType;
			OrganizationId orgId = null;
			if (transportRule != null)
			{
				orgId = transportRule.OrganizationId;
			}
			if (this.conditions != null)
			{
				foreach (TransportRulePredicate predicate in this.conditions)
				{
					this.SetParametersFromPredicate(predicate, false, orgId);
				}
			}
			if (this.exceptions != null)
			{
				foreach (TransportRulePredicate predicate2 in this.exceptions)
				{
					this.SetParametersFromPredicate(predicate2, true, orgId);
				}
			}
			if (this.actions != null)
			{
				foreach (TransportRuleAction parametersFromAction in this.actions)
				{
					this.SetParametersFromAction(parametersFromAction);
				}
			}
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x06006B32 RID: 27442 RVA: 0x001B9DB0 File Offset: 0x001B7FB0
		internal ObjectSchema Schema
		{
			get
			{
				return Rule.schema;
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x06006B33 RID: 27443 RVA: 0x001B9DB7 File Offset: 0x001B7FB7
		// (set) Token: 0x06006B34 RID: 27444 RVA: 0x001B9DBF File Offset: 0x001B7FBF
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

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x06006B35 RID: 27445 RVA: 0x001B9DE1 File Offset: 0x001B7FE1
		// (set) Token: 0x06006B36 RID: 27446 RVA: 0x001B9DE9 File Offset: 0x001B7FE9
		public string DlpPolicy
		{
			get
			{
				return this.dlpPolicy;
			}
			set
			{
				this.dlpPolicy = value;
			}
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x06006B37 RID: 27447 RVA: 0x001B9DF2 File Offset: 0x001B7FF2
		// (set) Token: 0x06006B38 RID: 27448 RVA: 0x001B9DFA File Offset: 0x001B7FFA
		public Guid DlpPolicyId
		{
			get
			{
				return this.dlpPolicyId;
			}
			set
			{
				this.dlpPolicyId = value;
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x06006B39 RID: 27449 RVA: 0x001B9E03 File Offset: 0x001B8003
		// (set) Token: 0x06006B3A RID: 27450 RVA: 0x001B9E0C File Offset: 0x001B800C
		public string Comments
		{
			get
			{
				return this.comments;
			}
			set
			{
				ArgumentException ex;
				if (!Utils.ValidateRuleComments(value, out ex))
				{
					throw ex;
				}
				this.comments = value;
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x06006B3B RID: 27451 RVA: 0x001B9E2C File Offset: 0x001B802C
		public bool ManuallyModified
		{
			get
			{
				return this.manuallyModified;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x06006B3C RID: 27452 RVA: 0x001B9E34 File Offset: 0x001B8034
		// (set) Token: 0x06006B3D RID: 27453 RVA: 0x001B9E3C File Offset: 0x001B803C
		public DateTime? ActivationDate
		{
			get
			{
				return this.activationDate;
			}
			set
			{
				this.activationDate = value;
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x06006B3E RID: 27454 RVA: 0x001B9E45 File Offset: 0x001B8045
		// (set) Token: 0x06006B3F RID: 27455 RVA: 0x001B9E4D File Offset: 0x001B804D
		public DateTime? ExpiryDate
		{
			get
			{
				return this.expiryDate;
			}
			set
			{
				this.expiryDate = value;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06006B40 RID: 27456 RVA: 0x001B9E56 File Offset: 0x001B8056
		public RuleDescription Description
		{
			get
			{
				return Utils.BuildRuleDescription(this, 200);
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x06006B41 RID: 27457 RVA: 0x001B9E64 File Offset: 0x001B8064
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
					if (transportRule.IsTooAdvancedToParse)
					{
						result = null;
					}
					else
					{
						result = transportRule.MinimumVersion;
					}
				}
				catch (ParserException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x06006B42 RID: 27458 RVA: 0x001B9EC4 File Offset: 0x001B80C4
		// (set) Token: 0x06006B43 RID: 27459 RVA: 0x001B9ECC File Offset: 0x001B80CC
		public TransportRulePredicate[] Conditions
		{
			get
			{
				return this.conditions;
			}
			set
			{
				if (value != null)
				{
					Rule.ValidatePhraseArray(value, true, true);
				}
				this.conditions = value;
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x06006B44 RID: 27460 RVA: 0x001B9EE1 File Offset: 0x001B80E1
		// (set) Token: 0x06006B45 RID: 27461 RVA: 0x001B9EE9 File Offset: 0x001B80E9
		public TransportRulePredicate[] Exceptions
		{
			get
			{
				return this.exceptions;
			}
			set
			{
				if (value != null)
				{
					Rule.ValidatePhraseArray(value, true, true);
				}
				this.exceptions = value;
			}
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x06006B46 RID: 27462 RVA: 0x001B9EFE File Offset: 0x001B80FE
		// (set) Token: 0x06006B47 RID: 27463 RVA: 0x001B9F06 File Offset: 0x001B8106
		public TransportRuleAction[] Actions
		{
			get
			{
				return this.actions;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException(RulesTasksStrings.NoAction);
				}
				Rule.ValidatePhraseArray(value, false, true);
				this.actions = value;
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x06006B48 RID: 27464 RVA: 0x001B9F30 File Offset: 0x001B8130
		// (set) Token: 0x06006B49 RID: 27465 RVA: 0x001B9F38 File Offset: 0x001B8138
		public RuleState State { get; set; }

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x06006B4A RID: 27466 RVA: 0x001B9F41 File Offset: 0x001B8141
		// (set) Token: 0x06006B4B RID: 27467 RVA: 0x001B9F49 File Offset: 0x001B8149
		public RuleMode Mode { get; set; }

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x06006B4C RID: 27468 RVA: 0x001B9F52 File Offset: 0x001B8152
		// (set) Token: 0x06006B4D RID: 27469 RVA: 0x001B9F5A File Offset: 0x001B815A
		public RuleErrorAction RuleErrorAction { get; set; }

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x06006B4E RID: 27470 RVA: 0x001B9F63 File Offset: 0x001B8163
		// (set) Token: 0x06006B4F RID: 27471 RVA: 0x001B9F6B File Offset: 0x001B816B
		public SenderAddressLocation SenderAddressLocation { get; set; }

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x06006B50 RID: 27472 RVA: 0x001B9F74 File Offset: 0x001B8174
		// (set) Token: 0x06006B51 RID: 27473 RVA: 0x001B9F7C File Offset: 0x001B817C
		public RuleSubType RuleSubType { get; set; }

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x06006B52 RID: 27474 RVA: 0x001B9F98 File Offset: 0x001B8198
		public bool UseLegacyRegex
		{
			get
			{
				if (this.conditions != null)
				{
					if (this.Conditions.Any((TransportRulePredicate predicate) => Utils.IsLegacyRegexPredicate(predicate)))
					{
						return true;
					}
				}
				if (this.exceptions != null)
				{
					if (this.Exceptions.Any((TransportRulePredicate predicate) => Utils.IsLegacyRegexPredicate(predicate)))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x06006B53 RID: 27475 RVA: 0x001BA00E File Offset: 0x001B820E
		// (set) Token: 0x06006B54 RID: 27476 RVA: 0x001BA016 File Offset: 0x001B8216
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FromAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FromAddressesDescription)]
		public RecipientIdParameter[] From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06006B55 RID: 27477 RVA: 0x001BA01F File Offset: 0x001B821F
		// (set) Token: 0x06006B56 RID: 27478 RVA: 0x001BA027 File Offset: 0x001B8227
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FromDLAddressDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FromDLAddressDisplayName)]
		public RecipientIdParameter[] FromMemberOf
		{
			get
			{
				return this.fromMemberOf;
			}
			set
			{
				this.fromMemberOf = value;
			}
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06006B57 RID: 27479 RVA: 0x001BA030 File Offset: 0x001B8230
		// (set) Token: 0x06006B58 RID: 27480 RVA: 0x001BA038 File Offset: 0x001B8238
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FromScopeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FromScopeDisplayName)]
		public FromUserScope? FromScope
		{
			get
			{
				return this.fromScope;
			}
			set
			{
				this.fromScope = value;
			}
		}

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06006B59 RID: 27481 RVA: 0x001BA041 File Offset: 0x001B8241
		// (set) Token: 0x06006B5A RID: 27482 RVA: 0x001BA049 File Offset: 0x001B8249
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
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

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06006B5B RID: 27483 RVA: 0x001BA052 File Offset: 0x001B8252
		// (set) Token: 0x06006B5C RID: 27484 RVA: 0x001BA05A File Offset: 0x001B825A
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
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

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06006B5D RID: 27485 RVA: 0x001BA063 File Offset: 0x001B8263
		// (set) Token: 0x06006B5E RID: 27486 RVA: 0x001BA06B File Offset: 0x001B826B
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToScopeDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToScopeDescription)]
		public ToUserScope? SentToScope
		{
			get
			{
				return this.sentToScope;
			}
			set
			{
				this.sentToScope = value;
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06006B5F RID: 27487 RVA: 0x001BA074 File Offset: 0x001B8274
		// (set) Token: 0x06006B60 RID: 27488 RVA: 0x001BA07C File Offset: 0x001B827C
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		public RecipientIdParameter[] BetweenMemberOf1
		{
			get
			{
				return this.betweenMemberOf1;
			}
			set
			{
				this.betweenMemberOf1 = value;
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06006B61 RID: 27489 RVA: 0x001BA085 File Offset: 0x001B8285
		// (set) Token: 0x06006B62 RID: 27490 RVA: 0x001BA08D File Offset: 0x001B828D
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		public RecipientIdParameter[] BetweenMemberOf2
		{
			get
			{
				return this.betweenMemberOf2;
			}
			set
			{
				this.betweenMemberOf2 = value;
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x06006B63 RID: 27491 RVA: 0x001BA096 File Offset: 0x001B8296
		// (set) Token: 0x06006B64 RID: 27492 RVA: 0x001BA09E File Offset: 0x001B829E
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public RecipientIdParameter[] ManagerAddresses
		{
			get
			{
				return this.managerAddresses;
			}
			set
			{
				this.managerAddresses = value;
			}
		}

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06006B65 RID: 27493 RVA: 0x001BA0A7 File Offset: 0x001B82A7
		// (set) Token: 0x06006B66 RID: 27494 RVA: 0x001BA0AF File Offset: 0x001B82AF
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.EvaluatedUserDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.EvaluatedUserDescription)]
		public EvaluatedUser? ManagerForEvaluatedUser
		{
			get
			{
				return this.managerForEvaluatedUser;
			}
			set
			{
				this.managerForEvaluatedUser = value;
			}
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06006B67 RID: 27495 RVA: 0x001BA0B8 File Offset: 0x001B82B8
		// (set) Token: 0x06006B68 RID: 27496 RVA: 0x001BA0C0 File Offset: 0x001B82C0
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ManagementRelationshipDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ManagementRelationshipDisplayName)]
		public ManagementRelationship? SenderManagementRelationship
		{
			get
			{
				return this.senderManagementRelationship;
			}
			set
			{
				this.senderManagementRelationship = value;
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x06006B69 RID: 27497 RVA: 0x001BA0C9 File Offset: 0x001B82C9
		// (set) Token: 0x06006B6A RID: 27498 RVA: 0x001BA0D1 File Offset: 0x001B82D1
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ADAttributeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ADAttributeDisplayName)]
		public ADAttribute? ADComparisonAttribute
		{
			get
			{
				return this.adComparisonAttribute;
			}
			set
			{
				this.adComparisonAttribute = value;
			}
		}

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x06006B6B RID: 27499 RVA: 0x001BA0DA File Offset: 0x001B82DA
		// (set) Token: 0x06006B6C RID: 27500 RVA: 0x001BA0E2 File Offset: 0x001B82E2
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.EvaluationDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.EvaluationDescription)]
		public Evaluation? ADComparisonOperator
		{
			get
			{
				return this.adComparisonOperator;
			}
			set
			{
				this.adComparisonOperator = value;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06006B6D RID: 27501 RVA: 0x001BA0EB File Offset: 0x001B82EB
		// (set) Token: 0x06006B6E RID: 27502 RVA: 0x001BA0F3 File Offset: 0x001B82F3
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] SenderADAttributeContainsWords
		{
			get
			{
				return this.senderADAttributeContainsWords;
			}
			set
			{
				this.senderADAttributeContainsWords = value;
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x06006B6F RID: 27503 RVA: 0x001BA0FC File Offset: 0x001B82FC
		// (set) Token: 0x06006B70 RID: 27504 RVA: 0x001BA104 File Offset: 0x001B8304
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] SenderADAttributeMatchesPatterns
		{
			get
			{
				return this.senderADAttributeMatchesPatterns;
			}
			set
			{
				this.senderADAttributeMatchesPatterns = value;
			}
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x06006B71 RID: 27505 RVA: 0x001BA10D File Offset: 0x001B830D
		// (set) Token: 0x06006B72 RID: 27506 RVA: 0x001BA115 File Offset: 0x001B8315
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] RecipientADAttributeContainsWords
		{
			get
			{
				return this.recipientADAttributeContainsWords;
			}
			set
			{
				this.recipientADAttributeContainsWords = value;
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x06006B73 RID: 27507 RVA: 0x001BA11E File Offset: 0x001B831E
		// (set) Token: 0x06006B74 RID: 27508 RVA: 0x001BA126 File Offset: 0x001B8326
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] RecipientADAttributeMatchesPatterns
		{
			get
			{
				return this.recipientADAttributeMatchesPatterns;
			}
			set
			{
				this.recipientADAttributeMatchesPatterns = value;
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06006B75 RID: 27509 RVA: 0x001BA12F File Offset: 0x001B832F
		// (set) Token: 0x06006B76 RID: 27510 RVA: 0x001BA137 File Offset: 0x001B8337
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] AnyOfToHeader
		{
			get
			{
				return this.anyOfToHeader;
			}
			set
			{
				this.anyOfToHeader = value;
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x06006B77 RID: 27511 RVA: 0x001BA140 File Offset: 0x001B8340
		// (set) Token: 0x06006B78 RID: 27512 RVA: 0x001BA148 File Offset: 0x001B8348
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		public RecipientIdParameter[] AnyOfToHeaderMemberOf
		{
			get
			{
				return this.anyOfToHeaderMemberOf;
			}
			set
			{
				this.anyOfToHeaderMemberOf = value;
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06006B79 RID: 27513 RVA: 0x001BA151 File Offset: 0x001B8351
		// (set) Token: 0x06006B7A RID: 27514 RVA: 0x001BA159 File Offset: 0x001B8359
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] AnyOfCcHeader
		{
			get
			{
				return this.anyOfCcHeader;
			}
			set
			{
				this.anyOfCcHeader = value;
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06006B7B RID: 27515 RVA: 0x001BA162 File Offset: 0x001B8362
		// (set) Token: 0x06006B7C RID: 27516 RVA: 0x001BA16A File Offset: 0x001B836A
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		public RecipientIdParameter[] AnyOfCcHeaderMemberOf
		{
			get
			{
				return this.anyOfCcHeaderMemberOf;
			}
			set
			{
				this.anyOfCcHeaderMemberOf = value;
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06006B7D RID: 27517 RVA: 0x001BA173 File Offset: 0x001B8373
		// (set) Token: 0x06006B7E RID: 27518 RVA: 0x001BA17B File Offset: 0x001B837B
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] AnyOfToCcHeader
		{
			get
			{
				return this.anyOfToCcHeader;
			}
			set
			{
				this.anyOfToCcHeader = value;
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06006B7F RID: 27519 RVA: 0x001BA184 File Offset: 0x001B8384
		// (set) Token: 0x06006B80 RID: 27520 RVA: 0x001BA18C File Offset: 0x001B838C
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		public RecipientIdParameter[] AnyOfToCcHeaderMemberOf
		{
			get
			{
				return this.anyOfToCcHeaderMemberOf;
			}
			set
			{
				this.anyOfToCcHeaderMemberOf = value;
			}
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06006B81 RID: 27521 RVA: 0x001BA195 File Offset: 0x001B8395
		// (set) Token: 0x06006B82 RID: 27522 RVA: 0x001BA19D File Offset: 0x001B839D
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ClassificationDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ClassificationDisplayName)]
		public ADObjectId HasClassification
		{
			get
			{
				return this.hasClassification;
			}
			set
			{
				this.hasClassification = value;
			}
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06006B83 RID: 27523 RVA: 0x001BA1A6 File Offset: 0x001B83A6
		// (set) Token: 0x06006B84 RID: 27524 RVA: 0x001BA1AE File Offset: 0x001B83AE
		public bool HasNoClassification
		{
			get
			{
				return this.hasNoClassification;
			}
			set
			{
				this.hasNoClassification = value;
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06006B85 RID: 27525 RVA: 0x001BA1B7 File Offset: 0x001B83B7
		// (set) Token: 0x06006B86 RID: 27526 RVA: 0x001BA1BF File Offset: 0x001B83BF
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] SubjectContainsWords
		{
			get
			{
				return this.subjectContainsWords;
			}
			set
			{
				this.subjectContainsWords = value;
			}
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06006B87 RID: 27527 RVA: 0x001BA1C8 File Offset: 0x001B83C8
		// (set) Token: 0x06006B88 RID: 27528 RVA: 0x001BA1D0 File Offset: 0x001B83D0
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] SubjectOrBodyContainsWords
		{
			get
			{
				return this.subjectOrBodyContainsWords;
			}
			set
			{
				this.subjectOrBodyContainsWords = value;
			}
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06006B89 RID: 27529 RVA: 0x001BA1D9 File Offset: 0x001B83D9
		// (set) Token: 0x06006B8A RID: 27530 RVA: 0x001BA1E1 File Offset: 0x001B83E1
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		public HeaderName? HeaderContainsMessageHeader
		{
			get
			{
				return this.headerContainsMessageHeader;
			}
			set
			{
				this.headerContainsMessageHeader = value;
			}
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x06006B8B RID: 27531 RVA: 0x001BA1EA File Offset: 0x001B83EA
		// (set) Token: 0x06006B8C RID: 27532 RVA: 0x001BA1F2 File Offset: 0x001B83F2
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] HeaderContainsWords
		{
			get
			{
				return this.headerContainsWords;
			}
			set
			{
				this.headerContainsWords = value;
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06006B8D RID: 27533 RVA: 0x001BA1FB File Offset: 0x001B83FB
		// (set) Token: 0x06006B8E RID: 27534 RVA: 0x001BA203 File Offset: 0x001B8403
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] FromAddressContainsWords
		{
			get
			{
				return this.fromAddressContainsWords;
			}
			set
			{
				this.fromAddressContainsWords = value;
			}
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06006B8F RID: 27535 RVA: 0x001BA20C File Offset: 0x001B840C
		// (set) Token: 0x06006B90 RID: 27536 RVA: 0x001BA214 File Offset: 0x001B8414
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] SenderDomainIs
		{
			get
			{
				return this.senderDomainIs;
			}
			set
			{
				this.senderDomainIs = value;
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06006B91 RID: 27537 RVA: 0x001BA21D File Offset: 0x001B841D
		// (set) Token: 0x06006B92 RID: 27538 RVA: 0x001BA225 File Offset: 0x001B8425
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
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

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x06006B93 RID: 27539 RVA: 0x001BA22E File Offset: 0x001B842E
		// (set) Token: 0x06006B94 RID: 27540 RVA: 0x001BA236 File Offset: 0x001B8436
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] SubjectMatchesPatterns
		{
			get
			{
				return this.subjectMatchesPatterns;
			}
			set
			{
				this.subjectMatchesPatterns = value;
			}
		}

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06006B95 RID: 27541 RVA: 0x001BA23F File Offset: 0x001B843F
		// (set) Token: 0x06006B96 RID: 27542 RVA: 0x001BA247 File Offset: 0x001B8447
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] SubjectOrBodyMatchesPatterns
		{
			get
			{
				return this.subjectOrBodyMatchesPatterns;
			}
			set
			{
				this.subjectOrBodyMatchesPatterns = value;
			}
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06006B97 RID: 27543 RVA: 0x001BA250 File Offset: 0x001B8450
		// (set) Token: 0x06006B98 RID: 27544 RVA: 0x001BA258 File Offset: 0x001B8458
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		public HeaderName? HeaderMatchesMessageHeader
		{
			get
			{
				return this.headerMatchesMessageHeader;
			}
			set
			{
				this.headerMatchesMessageHeader = value;
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06006B99 RID: 27545 RVA: 0x001BA261 File Offset: 0x001B8461
		// (set) Token: 0x06006B9A RID: 27546 RVA: 0x001BA269 File Offset: 0x001B8469
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] HeaderMatchesPatterns
		{
			get
			{
				return this.headerMatchesPatterns;
			}
			set
			{
				this.headerMatchesPatterns = value;
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06006B9B RID: 27547 RVA: 0x001BA272 File Offset: 0x001B8472
		// (set) Token: 0x06006B9C RID: 27548 RVA: 0x001BA27A File Offset: 0x001B847A
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] FromAddressMatchesPatterns
		{
			get
			{
				return this.fromAddressMatchesPatterns;
			}
			set
			{
				this.fromAddressMatchesPatterns = value;
			}
		}

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x06006B9D RID: 27549 RVA: 0x001BA283 File Offset: 0x001B8483
		// (set) Token: 0x06006B9E RID: 27550 RVA: 0x001BA28B File Offset: 0x001B848B
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] AttachmentNameMatchesPatterns
		{
			get
			{
				return this.attachmentNameMatchesPatterns;
			}
			set
			{
				this.attachmentNameMatchesPatterns = value;
			}
		}

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06006B9F RID: 27551 RVA: 0x001BA294 File Offset: 0x001B8494
		// (set) Token: 0x06006BA0 RID: 27552 RVA: 0x001BA29C File Offset: 0x001B849C
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] AttachmentExtensionMatchesWords
		{
			get
			{
				return this.attachmentExtensionMatchesWords;
			}
			set
			{
				this.attachmentExtensionMatchesWords = value;
			}
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06006BA1 RID: 27553 RVA: 0x001BA2A5 File Offset: 0x001B84A5
		// (set) Token: 0x06006BA2 RID: 27554 RVA: 0x001BA2AD File Offset: 0x001B84AD
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] AttachmentPropertyContainsWords
		{
			get
			{
				return this.attachmentPropertyContainsWords;
			}
			set
			{
				this.attachmentPropertyContainsWords = value;
			}
		}

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x06006BA3 RID: 27555 RVA: 0x001BA2B6 File Offset: 0x001B84B6
		// (set) Token: 0x06006BA4 RID: 27556 RVA: 0x001BA2BE File Offset: 0x001B84BE
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ContentCharacterSetContainsWords
		{
			get
			{
				return this.contentCharacterSetContainsWords;
			}
			set
			{
				this.contentCharacterSetContainsWords = value;
			}
		}

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x06006BA5 RID: 27557 RVA: 0x001BA2C7 File Offset: 0x001B84C7
		// (set) Token: 0x06006BA6 RID: 27558 RVA: 0x001BA2CF File Offset: 0x001B84CF
		public bool HasSenderOverride
		{
			get
			{
				return this.hasSenderOverride;
			}
			set
			{
				this.hasSenderOverride = value;
			}
		}

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06006BA7 RID: 27559 RVA: 0x001BA2D8 File Offset: 0x001B84D8
		// (set) Token: 0x06006BA8 RID: 27560 RVA: 0x001BA2E0 File Offset: 0x001B84E0
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageDataClassificationDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageDataClassificationDescription)]
		public string[] MessageContainsDataClassifications
		{
			get
			{
				return this.messageContainsDataClassifications;
			}
			set
			{
				this.messageContainsDataClassifications = value;
			}
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06006BA9 RID: 27561 RVA: 0x001BA2E9 File Offset: 0x001B84E9
		// (set) Token: 0x06006BAA RID: 27562 RVA: 0x001BA2F1 File Offset: 0x001B84F1
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SenderIpRangesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SenderIpRangesDescription)]
		public MultiValuedProperty<IPRange> SenderIpRanges
		{
			get
			{
				return this.senderIpRanges;
			}
			set
			{
				this.senderIpRanges = value;
			}
		}

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06006BAB RID: 27563 RVA: 0x001BA2FA File Offset: 0x001B84FA
		// (set) Token: 0x06006BAC RID: 27564 RVA: 0x001BA302 File Offset: 0x001B8502
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SclValueDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SclValueDescription)]
		public SclValue? SCLOver
		{
			get
			{
				return this.sclOver;
			}
			set
			{
				this.sclOver = value;
			}
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06006BAD RID: 27565 RVA: 0x001BA30B File Offset: 0x001B850B
		// (set) Token: 0x06006BAE RID: 27566 RVA: 0x001BA313 File Offset: 0x001B8513
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.AttachmentSizeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.AttachmentSizeDisplayName)]
		public ByteQuantifiedSize? AttachmentSizeOver
		{
			get
			{
				return this.attachmentSizeOver;
			}
			set
			{
				this.attachmentSizeOver = value;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06006BAF RID: 27567 RVA: 0x001BA31C File Offset: 0x001B851C
		// (set) Token: 0x06006BB0 RID: 27568 RVA: 0x001BA324 File Offset: 0x001B8524
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageSizeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageSizeDisplayName)]
		public ByteQuantifiedSize? MessageSizeOver
		{
			get
			{
				return this.messageSizeOver;
			}
			set
			{
				this.messageSizeOver = value;
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06006BB1 RID: 27569 RVA: 0x001BA32D File Offset: 0x001B852D
		// (set) Token: 0x06006BB2 RID: 27570 RVA: 0x001BA335 File Offset: 0x001B8535
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ImportanceDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ImportanceDisplayName)]
		public Importance? WithImportance
		{
			get
			{
				return this.withImportance;
			}
			set
			{
				this.withImportance = value;
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06006BB3 RID: 27571 RVA: 0x001BA33E File Offset: 0x001B853E
		// (set) Token: 0x06006BB4 RID: 27572 RVA: 0x001BA346 File Offset: 0x001B8546
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageTypeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageTypeDisplayName)]
		public MessageType? MessageTypeMatches
		{
			get
			{
				return this.messageTypeMatches;
			}
			set
			{
				this.messageTypeMatches = value;
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x06006BB5 RID: 27573 RVA: 0x001BA34F File Offset: 0x001B854F
		// (set) Token: 0x06006BB6 RID: 27574 RVA: 0x001BA357 File Offset: 0x001B8557
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] RecipientAddressContainsWords
		{
			get
			{
				return this.recipientAddressContainsWords;
			}
			set
			{
				this.recipientAddressContainsWords = value;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x001BA360 File Offset: 0x001B8560
		// (set) Token: 0x06006BB8 RID: 27576 RVA: 0x001BA368 File Offset: 0x001B8568
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] RecipientAddressMatchesPatterns
		{
			get
			{
				return this.recipientAddressMatchesPatterns;
			}
			set
			{
				this.recipientAddressMatchesPatterns = value;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x001BA371 File Offset: 0x001B8571
		// (set) Token: 0x06006BBA RID: 27578 RVA: 0x001BA379 File Offset: 0x001B8579
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ListsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ListsDescription)]
		public Word[] SenderInRecipientList
		{
			get
			{
				return this.senderInRecipientList;
			}
			set
			{
				this.senderInRecipientList = value;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06006BBB RID: 27579 RVA: 0x001BA382 File Offset: 0x001B8582
		// (set) Token: 0x06006BBC RID: 27580 RVA: 0x001BA38A File Offset: 0x001B858A
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ListsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ListsDisplayName)]
		public Word[] RecipientInSenderList
		{
			get
			{
				return this.recipientInSenderList;
			}
			set
			{
				this.recipientInSenderList = value;
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06006BBD RID: 27581 RVA: 0x001BA393 File Offset: 0x001B8593
		// (set) Token: 0x06006BBE RID: 27582 RVA: 0x001BA39B File Offset: 0x001B859B
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] AttachmentContainsWords
		{
			get
			{
				return this.attachmentContainsWords;
			}
			set
			{
				this.attachmentContainsWords = value;
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06006BBF RID: 27583 RVA: 0x001BA3A4 File Offset: 0x001B85A4
		// (set) Token: 0x06006BC0 RID: 27584 RVA: 0x001BA3AC File Offset: 0x001B85AC
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] AttachmentMatchesPatterns
		{
			get
			{
				return this.attachmentMatchesPatterns;
			}
			set
			{
				this.attachmentMatchesPatterns = value;
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06006BC1 RID: 27585 RVA: 0x001BA3B5 File Offset: 0x001B85B5
		// (set) Token: 0x06006BC2 RID: 27586 RVA: 0x001BA3BD File Offset: 0x001B85BD
		public bool AttachmentIsUnsupported
		{
			get
			{
				return this.attachmentIsUnsupported;
			}
			set
			{
				this.attachmentIsUnsupported = value;
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06006BC3 RID: 27587 RVA: 0x001BA3C6 File Offset: 0x001B85C6
		// (set) Token: 0x06006BC4 RID: 27588 RVA: 0x001BA3CE File Offset: 0x001B85CE
		public bool AttachmentProcessingLimitExceeded
		{
			get
			{
				return this.attachmentProcessingLimitExceeded;
			}
			set
			{
				this.attachmentProcessingLimitExceeded = value;
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x001BA3D7 File Offset: 0x001B85D7
		// (set) Token: 0x06006BC6 RID: 27590 RVA: 0x001BA3DF File Offset: 0x001B85DF
		public bool AttachmentHasExecutableContent
		{
			get
			{
				return this.attachmentHasExecutableContent;
			}
			set
			{
				this.attachmentHasExecutableContent = value;
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06006BC7 RID: 27591 RVA: 0x001BA3E8 File Offset: 0x001B85E8
		// (set) Token: 0x06006BC8 RID: 27592 RVA: 0x001BA3F0 File Offset: 0x001B85F0
		public bool AttachmentIsPasswordProtected
		{
			get
			{
				return this.attachmentIsPasswordProtected;
			}
			set
			{
				this.attachmentIsPasswordProtected = value;
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06006BC9 RID: 27593 RVA: 0x001BA3F9 File Offset: 0x001B85F9
		// (set) Token: 0x06006BCA RID: 27594 RVA: 0x001BA401 File Offset: 0x001B8601
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] AnyOfRecipientAddressContainsWords
		{
			get
			{
				return this.anyOfRecipientAddressContainsWords;
			}
			set
			{
				this.anyOfRecipientAddressContainsWords = value;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06006BCB RID: 27595 RVA: 0x001BA40A File Offset: 0x001B860A
		// (set) Token: 0x06006BCC RID: 27596 RVA: 0x001BA412 File Offset: 0x001B8612
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] AnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return this.anyOfRecipientAddressMatchesPatterns;
			}
			set
			{
				this.anyOfRecipientAddressMatchesPatterns = value;
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x06006BCD RID: 27597 RVA: 0x001BA41B File Offset: 0x001B861B
		// (set) Token: 0x06006BCE RID: 27598 RVA: 0x001BA423 File Offset: 0x001B8623
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FromAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FromAddressesDescription)]
		public RecipientIdParameter[] ExceptIfFrom
		{
			get
			{
				return this.exceptIfFrom;
			}
			set
			{
				this.exceptIfFrom = value;
			}
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x001BA42C File Offset: 0x001B862C
		// (set) Token: 0x06006BD0 RID: 27600 RVA: 0x001BA434 File Offset: 0x001B8634
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FromDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FromDLAddressDescription)]
		public RecipientIdParameter[] ExceptIfFromMemberOf
		{
			get
			{
				return this.exceptIfFromMemberOf;
			}
			set
			{
				this.exceptIfFromMemberOf = value;
			}
		}

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x001BA43D File Offset: 0x001B863D
		// (set) Token: 0x06006BD2 RID: 27602 RVA: 0x001BA445 File Offset: 0x001B8645
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FromScopeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FromScopeDisplayName)]
		public FromUserScope? ExceptIfFromScope
		{
			get
			{
				return this.exceptIfFromScope;
			}
			set
			{
				this.exceptIfFromScope = value;
			}
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06006BD3 RID: 27603 RVA: 0x001BA44E File Offset: 0x001B864E
		// (set) Token: 0x06006BD4 RID: 27604 RVA: 0x001BA456 File Offset: 0x001B8656
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
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

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06006BD5 RID: 27605 RVA: 0x001BA45F File Offset: 0x001B865F
		// (set) Token: 0x06006BD6 RID: 27606 RVA: 0x001BA467 File Offset: 0x001B8667
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
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

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06006BD7 RID: 27607 RVA: 0x001BA470 File Offset: 0x001B8670
		// (set) Token: 0x06006BD8 RID: 27608 RVA: 0x001BA478 File Offset: 0x001B8678
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToScopeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToScopeDisplayName)]
		public ToUserScope? ExceptIfSentToScope
		{
			get
			{
				return this.exceptIfSentToScope;
			}
			set
			{
				this.exceptIfSentToScope = value;
			}
		}

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06006BD9 RID: 27609 RVA: 0x001BA481 File Offset: 0x001B8681
		// (set) Token: 0x06006BDA RID: 27610 RVA: 0x001BA489 File Offset: 0x001B8689
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		public RecipientIdParameter[] ExceptIfBetweenMemberOf1
		{
			get
			{
				return this.exceptIfBetweenMemberOf1;
			}
			set
			{
				this.exceptIfBetweenMemberOf1 = value;
			}
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06006BDB RID: 27611 RVA: 0x001BA492 File Offset: 0x001B8692
		// (set) Token: 0x06006BDC RID: 27612 RVA: 0x001BA49A File Offset: 0x001B869A
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		public RecipientIdParameter[] ExceptIfBetweenMemberOf2
		{
			get
			{
				return this.exceptIfBetweenMemberOf2;
			}
			set
			{
				this.exceptIfBetweenMemberOf2 = value;
			}
		}

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06006BDD RID: 27613 RVA: 0x001BA4A3 File Offset: 0x001B86A3
		// (set) Token: 0x06006BDE RID: 27614 RVA: 0x001BA4AB File Offset: 0x001B86AB
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] ExceptIfManagerAddresses
		{
			get
			{
				return this.exceptIfManagerAddresses;
			}
			set
			{
				this.exceptIfManagerAddresses = value;
			}
		}

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06006BDF RID: 27615 RVA: 0x001BA4B4 File Offset: 0x001B86B4
		// (set) Token: 0x06006BE0 RID: 27616 RVA: 0x001BA4BC File Offset: 0x001B86BC
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.EvaluatedUserDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.EvaluatedUserDisplayName)]
		public EvaluatedUser? ExceptIfManagerForEvaluatedUser
		{
			get
			{
				return this.exceptIfManagerForEvaluatedUser;
			}
			set
			{
				this.exceptIfManagerForEvaluatedUser = value;
			}
		}

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x06006BE1 RID: 27617 RVA: 0x001BA4C5 File Offset: 0x001B86C5
		// (set) Token: 0x06006BE2 RID: 27618 RVA: 0x001BA4CD File Offset: 0x001B86CD
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ManagementRelationshipDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ManagementRelationshipDescription)]
		public ManagementRelationship? ExceptIfSenderManagementRelationship
		{
			get
			{
				return this.exceptIfSenderManagementRelationship;
			}
			set
			{
				this.exceptIfSenderManagementRelationship = value;
			}
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06006BE3 RID: 27619 RVA: 0x001BA4D6 File Offset: 0x001B86D6
		// (set) Token: 0x06006BE4 RID: 27620 RVA: 0x001BA4DE File Offset: 0x001B86DE
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ADAttributeDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ADAttributeDescription)]
		public ADAttribute? ExceptIfADComparisonAttribute
		{
			get
			{
				return this.exceptIfADComparisonAttribute;
			}
			set
			{
				this.exceptIfADComparisonAttribute = value;
			}
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x001BA4E7 File Offset: 0x001B86E7
		// (set) Token: 0x06006BE6 RID: 27622 RVA: 0x001BA4EF File Offset: 0x001B86EF
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.EvaluationDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.EvaluationDescription)]
		public Evaluation? ExceptIfADComparisonOperator
		{
			get
			{
				return this.exceptIfADComparisonOperator;
			}
			set
			{
				this.exceptIfADComparisonOperator = value;
			}
		}

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06006BE7 RID: 27623 RVA: 0x001BA4F8 File Offset: 0x001B86F8
		// (set) Token: 0x06006BE8 RID: 27624 RVA: 0x001BA500 File Offset: 0x001B8700
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] ExceptIfSenderADAttributeContainsWords
		{
			get
			{
				return this.exceptIfSenderADAttributeContainsWords;
			}
			set
			{
				this.exceptIfSenderADAttributeContainsWords = value;
			}
		}

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x001BA509 File Offset: 0x001B8709
		// (set) Token: 0x06006BEA RID: 27626 RVA: 0x001BA511 File Offset: 0x001B8711
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] ExceptIfSenderADAttributeMatchesPatterns
		{
			get
			{
				return this.exceptIfSenderADAttributeMatchesPatterns;
			}
			set
			{
				this.exceptIfSenderADAttributeMatchesPatterns = value;
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06006BEB RID: 27627 RVA: 0x001BA51A File Offset: 0x001B871A
		// (set) Token: 0x06006BEC RID: 27628 RVA: 0x001BA522 File Offset: 0x001B8722
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] ExceptIfRecipientADAttributeContainsWords
		{
			get
			{
				return this.exceptIfRecipientADAttributeContainsWords;
			}
			set
			{
				this.exceptIfRecipientADAttributeContainsWords = value;
			}
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06006BED RID: 27629 RVA: 0x001BA52B File Offset: 0x001B872B
		// (set) Token: 0x06006BEE RID: 27630 RVA: 0x001BA533 File Offset: 0x001B8733
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] ExceptIfRecipientADAttributeMatchesPatterns
		{
			get
			{
				return this.exceptIfRecipientADAttributeMatchesPatterns;
			}
			set
			{
				this.exceptIfRecipientADAttributeMatchesPatterns = value;
			}
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06006BEF RID: 27631 RVA: 0x001BA53C File Offset: 0x001B873C
		// (set) Token: 0x06006BF0 RID: 27632 RVA: 0x001BA544 File Offset: 0x001B8744
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public RecipientIdParameter[] ExceptIfAnyOfToHeader
		{
			get
			{
				return this.exceptIfAnyOfToHeader;
			}
			set
			{
				this.exceptIfAnyOfToHeader = value;
			}
		}

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06006BF1 RID: 27633 RVA: 0x001BA54D File Offset: 0x001B874D
		// (set) Token: 0x06006BF2 RID: 27634 RVA: 0x001BA555 File Offset: 0x001B8755
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		public RecipientIdParameter[] ExceptIfAnyOfToHeaderMemberOf
		{
			get
			{
				return this.exceptIfAnyOfToHeaderMemberOf;
			}
			set
			{
				this.exceptIfAnyOfToHeaderMemberOf = value;
			}
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06006BF3 RID: 27635 RVA: 0x001BA55E File Offset: 0x001B875E
		// (set) Token: 0x06006BF4 RID: 27636 RVA: 0x001BA566 File Offset: 0x001B8766
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public RecipientIdParameter[] ExceptIfAnyOfCcHeader
		{
			get
			{
				return this.exceptIfAnyOfCcHeader;
			}
			set
			{
				this.exceptIfAnyOfCcHeader = value;
			}
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06006BF5 RID: 27637 RVA: 0x001BA56F File Offset: 0x001B876F
		// (set) Token: 0x06006BF6 RID: 27638 RVA: 0x001BA577 File Offset: 0x001B8777
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		public RecipientIdParameter[] ExceptIfAnyOfCcHeaderMemberOf
		{
			get
			{
				return this.exceptIfAnyOfCcHeaderMemberOf;
			}
			set
			{
				this.exceptIfAnyOfCcHeaderMemberOf = value;
			}
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06006BF7 RID: 27639 RVA: 0x001BA580 File Offset: 0x001B8780
		// (set) Token: 0x06006BF8 RID: 27640 RVA: 0x001BA588 File Offset: 0x001B8788
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] ExceptIfAnyOfToCcHeader
		{
			get
			{
				return this.exceptIfAnyOfToCcHeader;
			}
			set
			{
				this.exceptIfAnyOfToCcHeader = value;
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06006BF9 RID: 27641 RVA: 0x001BA591 File Offset: 0x001B8791
		// (set) Token: 0x06006BFA RID: 27642 RVA: 0x001BA599 File Offset: 0x001B8799
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToDLAddressDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToDLAddressDescription)]
		public RecipientIdParameter[] ExceptIfAnyOfToCcHeaderMemberOf
		{
			get
			{
				return this.exceptIfAnyOfToCcHeaderMemberOf;
			}
			set
			{
				this.exceptIfAnyOfToCcHeaderMemberOf = value;
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06006BFB RID: 27643 RVA: 0x001BA5A2 File Offset: 0x001B87A2
		// (set) Token: 0x06006BFC RID: 27644 RVA: 0x001BA5AA File Offset: 0x001B87AA
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ClassificationDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ClassificationDescription)]
		public ADObjectId ExceptIfHasClassification
		{
			get
			{
				return this.exceptIfHasClassification;
			}
			set
			{
				this.exceptIfHasClassification = value;
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x06006BFD RID: 27645 RVA: 0x001BA5B3 File Offset: 0x001B87B3
		// (set) Token: 0x06006BFE RID: 27646 RVA: 0x001BA5BB File Offset: 0x001B87BB
		public bool ExceptIfHasNoClassification
		{
			get
			{
				return this.exceptIfHasNoClassification;
			}
			set
			{
				this.exceptIfHasNoClassification = value;
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06006BFF RID: 27647 RVA: 0x001BA5C4 File Offset: 0x001B87C4
		// (set) Token: 0x06006C00 RID: 27648 RVA: 0x001BA5CC File Offset: 0x001B87CC
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ExceptIfSubjectContainsWords
		{
			get
			{
				return this.exceptIfSubjectContainsWords;
			}
			set
			{
				this.exceptIfSubjectContainsWords = value;
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x06006C01 RID: 27649 RVA: 0x001BA5D5 File Offset: 0x001B87D5
		// (set) Token: 0x06006C02 RID: 27650 RVA: 0x001BA5DD File Offset: 0x001B87DD
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return this.exceptIfSubjectOrBodyContainsWords;
			}
			set
			{
				this.exceptIfSubjectOrBodyContainsWords = value;
			}
		}

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x06006C03 RID: 27651 RVA: 0x001BA5E6 File Offset: 0x001B87E6
		// (set) Token: 0x06006C04 RID: 27652 RVA: 0x001BA5EE File Offset: 0x001B87EE
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		public HeaderName? ExceptIfHeaderContainsMessageHeader
		{
			get
			{
				return this.exceptIfHeaderContainsMessageHeader;
			}
			set
			{
				this.exceptIfHeaderContainsMessageHeader = value;
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06006C05 RID: 27653 RVA: 0x001BA5F7 File Offset: 0x001B87F7
		// (set) Token: 0x06006C06 RID: 27654 RVA: 0x001BA5FF File Offset: 0x001B87FF
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] ExceptIfHeaderContainsWords
		{
			get
			{
				return this.exceptIfHeaderContainsWords;
			}
			set
			{
				this.exceptIfHeaderContainsWords = value;
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06006C07 RID: 27655 RVA: 0x001BA608 File Offset: 0x001B8808
		// (set) Token: 0x06006C08 RID: 27656 RVA: 0x001BA610 File Offset: 0x001B8810
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return this.exceptIfFromAddressContainsWords;
			}
			set
			{
				this.exceptIfFromAddressContainsWords = value;
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x06006C09 RID: 27657 RVA: 0x001BA619 File Offset: 0x001B8819
		// (set) Token: 0x06006C0A RID: 27658 RVA: 0x001BA621 File Offset: 0x001B8821
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] ExceptIfSenderDomainIs
		{
			get
			{
				return this.exceptIfSenderDomainIs;
			}
			set
			{
				this.exceptIfSenderDomainIs = value;
			}
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06006C0B RID: 27659 RVA: 0x001BA62A File Offset: 0x001B882A
		// (set) Token: 0x06006C0C RID: 27660 RVA: 0x001BA632 File Offset: 0x001B8832
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
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

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x06006C0D RID: 27661 RVA: 0x001BA63B File Offset: 0x001B883B
		// (set) Token: 0x06006C0E RID: 27662 RVA: 0x001BA643 File Offset: 0x001B8843
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] ExceptIfSubjectMatchesPatterns
		{
			get
			{
				return this.exceptIfSubjectMatchesPatterns;
			}
			set
			{
				this.exceptIfSubjectMatchesPatterns = value;
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06006C0F RID: 27663 RVA: 0x001BA64C File Offset: 0x001B884C
		// (set) Token: 0x06006C10 RID: 27664 RVA: 0x001BA654 File Offset: 0x001B8854
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] ExceptIfSubjectOrBodyMatchesPatterns
		{
			get
			{
				return this.exceptIfSubjectOrBodyMatchesPatterns;
			}
			set
			{
				this.exceptIfSubjectOrBodyMatchesPatterns = value;
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06006C11 RID: 27665 RVA: 0x001BA65D File Offset: 0x001B885D
		// (set) Token: 0x06006C12 RID: 27666 RVA: 0x001BA665 File Offset: 0x001B8865
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		public HeaderName? ExceptIfHeaderMatchesMessageHeader
		{
			get
			{
				return this.exceptIfHeaderMatchesMessageHeader;
			}
			set
			{
				this.exceptIfHeaderMatchesMessageHeader = value;
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x06006C13 RID: 27667 RVA: 0x001BA66E File Offset: 0x001B886E
		// (set) Token: 0x06006C14 RID: 27668 RVA: 0x001BA676 File Offset: 0x001B8876
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] ExceptIfHeaderMatchesPatterns
		{
			get
			{
				return this.exceptIfHeaderMatchesPatterns;
			}
			set
			{
				this.exceptIfHeaderMatchesPatterns = value;
			}
		}

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06006C15 RID: 27669 RVA: 0x001BA67F File Offset: 0x001B887F
		// (set) Token: 0x06006C16 RID: 27670 RVA: 0x001BA687 File Offset: 0x001B8887
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] ExceptIfFromAddressMatchesPatterns
		{
			get
			{
				return this.exceptIfFromAddressMatchesPatterns;
			}
			set
			{
				this.exceptIfFromAddressMatchesPatterns = value;
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06006C17 RID: 27671 RVA: 0x001BA690 File Offset: 0x001B8890
		// (set) Token: 0x06006C18 RID: 27672 RVA: 0x001BA698 File Offset: 0x001B8898
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] ExceptIfAttachmentNameMatchesPatterns
		{
			get
			{
				return this.exceptIfAttachmentNameMatchesPatterns;
			}
			set
			{
				this.exceptIfAttachmentNameMatchesPatterns = value;
			}
		}

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06006C19 RID: 27673 RVA: 0x001BA6A1 File Offset: 0x001B88A1
		// (set) Token: 0x06006C1A RID: 27674 RVA: 0x001BA6A9 File Offset: 0x001B88A9
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Word[] ExceptIfAttachmentExtensionMatchesWords
		{
			get
			{
				return this.exceptIfAttachmentExtensionMatchesWords;
			}
			set
			{
				this.exceptIfAttachmentExtensionMatchesWords = value;
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06006C1B RID: 27675 RVA: 0x001BA6B2 File Offset: 0x001B88B2
		// (set) Token: 0x06006C1C RID: 27676 RVA: 0x001BA6BA File Offset: 0x001B88BA
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ExceptIfAttachmentPropertyContainsWords
		{
			get
			{
				return this.exceptIfAttachmentPropertyContainsWords;
			}
			set
			{
				this.exceptIfAttachmentPropertyContainsWords = value;
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06006C1D RID: 27677 RVA: 0x001BA6C3 File Offset: 0x001B88C3
		// (set) Token: 0x06006C1E RID: 27678 RVA: 0x001BA6CB File Offset: 0x001B88CB
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ExceptIfContentCharacterSetContainsWords
		{
			get
			{
				return this.exceptIfContentCharacterSetContainsWords;
			}
			set
			{
				this.exceptIfContentCharacterSetContainsWords = value;
			}
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x06006C1F RID: 27679 RVA: 0x001BA6D4 File Offset: 0x001B88D4
		// (set) Token: 0x06006C20 RID: 27680 RVA: 0x001BA6DC File Offset: 0x001B88DC
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SclValueDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SclValueDisplayName)]
		public SclValue? ExceptIfSCLOver
		{
			get
			{
				return this.exceptIfSCLOver;
			}
			set
			{
				this.exceptIfSCLOver = value;
			}
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x06006C21 RID: 27681 RVA: 0x001BA6E5 File Offset: 0x001B88E5
		// (set) Token: 0x06006C22 RID: 27682 RVA: 0x001BA6ED File Offset: 0x001B88ED
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.AttachmentSizeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.AttachmentSizeDisplayName)]
		public ByteQuantifiedSize? ExceptIfAttachmentSizeOver
		{
			get
			{
				return this.exceptIfAttachmentSizeOver;
			}
			set
			{
				this.exceptIfAttachmentSizeOver = value;
			}
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x06006C23 RID: 27683 RVA: 0x001BA6F6 File Offset: 0x001B88F6
		// (set) Token: 0x06006C24 RID: 27684 RVA: 0x001BA6FE File Offset: 0x001B88FE
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageSizeDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageSizeDescription)]
		public ByteQuantifiedSize? ExceptIfMessageSizeOver
		{
			get
			{
				return this.exceptIfMessageSizeOver;
			}
			set
			{
				this.exceptIfMessageSizeOver = value;
			}
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06006C25 RID: 27685 RVA: 0x001BA707 File Offset: 0x001B8907
		// (set) Token: 0x06006C26 RID: 27686 RVA: 0x001BA70F File Offset: 0x001B890F
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ImportanceDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ImportanceDisplayName)]
		public Importance? ExceptIfWithImportance
		{
			get
			{
				return this.exceptIfWithImportance;
			}
			set
			{
				this.exceptIfWithImportance = value;
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06006C27 RID: 27687 RVA: 0x001BA718 File Offset: 0x001B8918
		// (set) Token: 0x06006C28 RID: 27688 RVA: 0x001BA720 File Offset: 0x001B8920
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageTypeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageTypeDisplayName)]
		public MessageType? ExceptIfMessageTypeMatches
		{
			get
			{
				return this.exceptIfMessageTypeMatches;
			}
			set
			{
				this.exceptIfMessageTypeMatches = value;
			}
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06006C29 RID: 27689 RVA: 0x001BA729 File Offset: 0x001B8929
		// (set) Token: 0x06006C2A RID: 27690 RVA: 0x001BA731 File Offset: 0x001B8931
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		public Word[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return this.exceptIfRecipientAddressContainsWords;
			}
			set
			{
				this.exceptIfRecipientAddressContainsWords = value;
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06006C2B RID: 27691 RVA: 0x001BA73A File Offset: 0x001B893A
		// (set) Token: 0x06006C2C RID: 27692 RVA: 0x001BA742 File Offset: 0x001B8942
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		public Pattern[] ExceptIfRecipientAddressMatchesPatterns
		{
			get
			{
				return this.exceptIfRecipientAddressMatchesPatterns;
			}
			set
			{
				this.exceptIfRecipientAddressMatchesPatterns = value;
			}
		}

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06006C2D RID: 27693 RVA: 0x001BA74B File Offset: 0x001B894B
		// (set) Token: 0x06006C2E RID: 27694 RVA: 0x001BA753 File Offset: 0x001B8953
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ListsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ListsDescription)]
		public Word[] ExceptIfSenderInRecipientList
		{
			get
			{
				return this.exceptIfSenderInRecipientList;
			}
			set
			{
				this.exceptIfSenderInRecipientList = value;
			}
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06006C2F RID: 27695 RVA: 0x001BA75C File Offset: 0x001B895C
		// (set) Token: 0x06006C30 RID: 27696 RVA: 0x001BA764 File Offset: 0x001B8964
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ListsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ListsDisplayName)]
		public Word[] ExceptIfRecipientInSenderList
		{
			get
			{
				return this.exceptIfRecipientInSenderList;
			}
			set
			{
				this.exceptIfRecipientInSenderList = value;
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x06006C31 RID: 27697 RVA: 0x001BA76D File Offset: 0x001B896D
		// (set) Token: 0x06006C32 RID: 27698 RVA: 0x001BA775 File Offset: 0x001B8975
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ExceptIfAttachmentContainsWords
		{
			get
			{
				return this.exceptIfAttachmentContainsWords;
			}
			set
			{
				this.exceptIfAttachmentContainsWords = value;
			}
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x06006C33 RID: 27699 RVA: 0x001BA77E File Offset: 0x001B897E
		// (set) Token: 0x06006C34 RID: 27700 RVA: 0x001BA786 File Offset: 0x001B8986
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] ExceptIfAttachmentMatchesPatterns
		{
			get
			{
				return this.exceptIfAttachmentMatchesPatterns;
			}
			set
			{
				this.exceptIfAttachmentMatchesPatterns = value;
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x06006C35 RID: 27701 RVA: 0x001BA78F File Offset: 0x001B898F
		// (set) Token: 0x06006C36 RID: 27702 RVA: 0x001BA797 File Offset: 0x001B8997
		public bool ExceptIfAttachmentIsUnsupported
		{
			get
			{
				return this.exceptIfAttachmentIsUnsupported;
			}
			set
			{
				this.exceptIfAttachmentIsUnsupported = value;
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x06006C37 RID: 27703 RVA: 0x001BA7A0 File Offset: 0x001B89A0
		// (set) Token: 0x06006C38 RID: 27704 RVA: 0x001BA7A8 File Offset: 0x001B89A8
		public bool ExceptIfAttachmentProcessingLimitExceeded
		{
			get
			{
				return this.exceptIfAttachmentProcessingLimitExceeded;
			}
			set
			{
				this.exceptIfAttachmentProcessingLimitExceeded = value;
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x06006C39 RID: 27705 RVA: 0x001BA7B1 File Offset: 0x001B89B1
		// (set) Token: 0x06006C3A RID: 27706 RVA: 0x001BA7B9 File Offset: 0x001B89B9
		public bool ExceptIfAttachmentHasExecutableContent
		{
			get
			{
				return this.exceptIfAttachmentHasExecutableContent;
			}
			set
			{
				this.exceptIfAttachmentHasExecutableContent = value;
			}
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x06006C3B RID: 27707 RVA: 0x001BA7C2 File Offset: 0x001B89C2
		// (set) Token: 0x06006C3C RID: 27708 RVA: 0x001BA7CA File Offset: 0x001B89CA
		public bool ExceptIfAttachmentIsPasswordProtected
		{
			get
			{
				return this.exceptIfAttachmentIsPasswordProtected;
			}
			set
			{
				this.exceptIfAttachmentIsPasswordProtected = value;
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x06006C3D RID: 27709 RVA: 0x001BA7D3 File Offset: 0x001B89D3
		// (set) Token: 0x06006C3E RID: 27710 RVA: 0x001BA7DB File Offset: 0x001B89DB
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.WordsDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.WordsDisplayName)]
		public Word[] ExceptIfAnyOfRecipientAddressContainsWords
		{
			get
			{
				return this.exceptIfAnyOfRecipientAddressContainsWords;
			}
			set
			{
				this.exceptIfAnyOfRecipientAddressContainsWords = value;
			}
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x06006C3F RID: 27711 RVA: 0x001BA7E4 File Offset: 0x001B89E4
		// (set) Token: 0x06006C40 RID: 27712 RVA: 0x001BA7EC File Offset: 0x001B89EC
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.TextPatternsDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.TextPatternsDescription)]
		public Pattern[] ExceptIfAnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return this.exceptIfAnyOfRecipientAddressMatchesPatterns;
			}
			set
			{
				this.exceptIfAnyOfRecipientAddressMatchesPatterns = value;
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x06006C41 RID: 27713 RVA: 0x001BA7F5 File Offset: 0x001B89F5
		// (set) Token: 0x06006C42 RID: 27714 RVA: 0x001BA7FD File Offset: 0x001B89FD
		public bool ExceptIfHasSenderOverride
		{
			get
			{
				return this.exceptIfHasSenderOverride;
			}
			set
			{
				this.exceptIfHasSenderOverride = value;
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x06006C43 RID: 27715 RVA: 0x001BA806 File Offset: 0x001B8A06
		// (set) Token: 0x06006C44 RID: 27716 RVA: 0x001BA80E File Offset: 0x001B8A0E
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageDataClassificationDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageDataClassificationDescription)]
		public string[] ExceptIfMessageContainsDataClassifications
		{
			get
			{
				return this.exceptIfMessageContainsDataClassifications;
			}
			set
			{
				this.exceptIfMessageContainsDataClassifications = value;
			}
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x06006C45 RID: 27717 RVA: 0x001BA817 File Offset: 0x001B8A17
		// (set) Token: 0x06006C46 RID: 27718 RVA: 0x001BA81F File Offset: 0x001B8A1F
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SenderIpRangesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SenderIpRangesDisplayName)]
		public MultiValuedProperty<IPRange> ExceptIfSenderIpRanges
		{
			get
			{
				return this.exceptIfSenderIpanges;
			}
			set
			{
				this.exceptIfSenderIpanges = value;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x06006C47 RID: 27719 RVA: 0x001BA828 File Offset: 0x001B8A28
		// (set) Token: 0x06006C48 RID: 27720 RVA: 0x001BA830 File Offset: 0x001B8A30
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.PrefixDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.PrefixDescription)]
		public string PrependSubject
		{
			get
			{
				return this.prependSubject;
			}
			set
			{
				this.prependSubject = value;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06006C49 RID: 27721 RVA: 0x001BA839 File Offset: 0x001B8A39
		// (set) Token: 0x06006C4A RID: 27722 RVA: 0x001BA841 File Offset: 0x001B8A41
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SetAuditSeverityDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SetAuditSeverityDisplayName)]
		public string SetAuditSeverity
		{
			get
			{
				return this.setAuditSeverity;
			}
			set
			{
				this.setAuditSeverity = value;
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x06006C4B RID: 27723 RVA: 0x001BA84A File Offset: 0x001B8A4A
		// (set) Token: 0x06006C4C RID: 27724 RVA: 0x001BA852 File Offset: 0x001B8A52
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ClassificationDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ClassificationDisplayName)]
		public ADObjectId ApplyClassification
		{
			get
			{
				return this.applyClassification;
			}
			set
			{
				this.applyClassification = value;
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x06006C4D RID: 27725 RVA: 0x001BA85B File Offset: 0x001B8A5B
		// (set) Token: 0x06006C4E RID: 27726 RVA: 0x001BA863 File Offset: 0x001B8A63
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.DisclaimerLocationDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.DisclaimerLocationDescription)]
		public DisclaimerLocation? ApplyHtmlDisclaimerLocation
		{
			get
			{
				return this.applyHtmlDisclaimerLocation;
			}
			set
			{
				this.applyHtmlDisclaimerLocation = value;
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x06006C4F RID: 27727 RVA: 0x001BA86C File Offset: 0x001B8A6C
		// (set) Token: 0x06006C50 RID: 27728 RVA: 0x001BA874 File Offset: 0x001B8A74
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.DisclaimerTextDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.DisclaimerTextDisplayName)]
		public DisclaimerText? ApplyHtmlDisclaimerText
		{
			get
			{
				return this.applyHtmlDisclaimerText;
			}
			set
			{
				this.applyHtmlDisclaimerText = value;
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x06006C51 RID: 27729 RVA: 0x001BA87D File Offset: 0x001B8A7D
		// (set) Token: 0x06006C52 RID: 27730 RVA: 0x001BA885 File Offset: 0x001B8A85
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.FallbackActionDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.FallbackActionDisplayName)]
		public DisclaimerFallbackAction? ApplyHtmlDisclaimerFallbackAction
		{
			get
			{
				return this.applyHtmlDisclaimerFallbackAction;
			}
			set
			{
				this.applyHtmlDisclaimerFallbackAction = value;
			}
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x06006C53 RID: 27731 RVA: 0x001BA88E File Offset: 0x001B8A8E
		// (set) Token: 0x06006C54 RID: 27732 RVA: 0x001BA896 File Offset: 0x001B8A96
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.RmsTemplateDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.RmsTemplateDisplayName)]
		[ActionParameterName("ApplyRightsProtectionTemplate")]
		public RmsTemplateIdentity ApplyRightsProtectionTemplate
		{
			get
			{
				return this.applyRightsProtectionTemplate;
			}
			set
			{
				this.applyRightsProtectionTemplate = value;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06006C55 RID: 27733 RVA: 0x001BA89F File Offset: 0x001B8A9F
		// (set) Token: 0x06006C56 RID: 27734 RVA: 0x001BA8A7 File Offset: 0x001B8AA7
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SclValueDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SclValueDescription)]
		public SclValue? SetSCL
		{
			get
			{
				return this.setSCL;
			}
			set
			{
				this.setSCL = value;
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x001BA8B0 File Offset: 0x001B8AB0
		// (set) Token: 0x06006C58 RID: 27736 RVA: 0x001BA8B8 File Offset: 0x001B8AB8
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		public HeaderName? SetHeaderName
		{
			get
			{
				return this.setHeaderName;
			}
			set
			{
				this.setHeaderName = value;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06006C59 RID: 27737 RVA: 0x001BA8C1 File Offset: 0x001B8AC1
		// (set) Token: 0x06006C5A RID: 27738 RVA: 0x001BA8C9 File Offset: 0x001B8AC9
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.HeaderValueDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.HeaderValueDescription)]
		public HeaderValue? SetHeaderValue
		{
			get
			{
				return this.setHeaderValue;
			}
			set
			{
				this.setHeaderValue = value;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x06006C5B RID: 27739 RVA: 0x001BA8D2 File Offset: 0x001B8AD2
		// (set) Token: 0x06006C5C RID: 27740 RVA: 0x001BA8DA File Offset: 0x001B8ADA
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		public HeaderName? RemoveHeader
		{
			get
			{
				return this.removeHeader;
			}
			set
			{
				this.removeHeader = value;
			}
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x06006C5D RID: 27741 RVA: 0x001BA8E3 File Offset: 0x001B8AE3
		// (set) Token: 0x06006C5E RID: 27742 RVA: 0x001BA8EB File Offset: 0x001B8AEB
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public RecipientIdParameter[] AddToRecipients
		{
			get
			{
				return this.addToRecipients;
			}
			set
			{
				this.addToRecipients = value;
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x06006C5F RID: 27743 RVA: 0x001BA8F4 File Offset: 0x001B8AF4
		// (set) Token: 0x06006C60 RID: 27744 RVA: 0x001BA8FC File Offset: 0x001B8AFC
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public RecipientIdParameter[] CopyTo
		{
			get
			{
				return this.copyTo;
			}
			set
			{
				this.copyTo = value;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06006C61 RID: 27745 RVA: 0x001BA905 File Offset: 0x001B8B05
		// (set) Token: 0x06006C62 RID: 27746 RVA: 0x001BA90D File Offset: 0x001B8B0D
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] BlindCopyTo
		{
			get
			{
				return this.blindCopyTo;
			}
			set
			{
				this.blindCopyTo = value;
			}
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06006C63 RID: 27747 RVA: 0x001BA916 File Offset: 0x001B8B16
		// (set) Token: 0x06006C64 RID: 27748 RVA: 0x001BA91E File Offset: 0x001B8B1E
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.RecipientTypeDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.RecipientTypeDescription)]
		public AddedRecipientType? AddManagerAsRecipientType
		{
			get
			{
				return this.addManagerAsRecipientType;
			}
			set
			{
				this.addManagerAsRecipientType = value;
			}
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06006C65 RID: 27749 RVA: 0x001BA927 File Offset: 0x001B8B27
		// (set) Token: 0x06006C66 RID: 27750 RVA: 0x001BA92F File Offset: 0x001B8B2F
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public RecipientIdParameter[] ModerateMessageByUser
		{
			get
			{
				return this.moderateMessageByUser;
			}
			set
			{
				this.moderateMessageByUser = value;
			}
		}

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x06006C67 RID: 27751 RVA: 0x001BA938 File Offset: 0x001B8B38
		// (set) Token: 0x06006C68 RID: 27752 RVA: 0x001BA940 File Offset: 0x001B8B40
		public bool ModerateMessageByManager
		{
			get
			{
				return this.moderateMessageByManager;
			}
			set
			{
				this.moderateMessageByManager = value;
			}
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x06006C69 RID: 27753 RVA: 0x001BA949 File Offset: 0x001B8B49
		// (set) Token: 0x06006C6A RID: 27754 RVA: 0x001BA951 File Offset: 0x001B8B51
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public RecipientIdParameter[] RedirectMessageTo
		{
			get
			{
				return this.redirectMessageTo;
			}
			set
			{
				this.redirectMessageTo = value;
			}
		}

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x06006C6B RID: 27755 RVA: 0x001BA95A File Offset: 0x001B8B5A
		// (set) Token: 0x06006C6C RID: 27756 RVA: 0x001BA962 File Offset: 0x001B8B62
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.EnhancedStatusCodeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.EnhancedStatusCodeDisplayName)]
		public RejectEnhancedStatus? RejectMessageEnhancedStatusCode
		{
			get
			{
				return this.rejectMessageEnhancedStatusCode;
			}
			set
			{
				this.rejectMessageEnhancedStatusCode = value;
			}
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x06006C6D RID: 27757 RVA: 0x001BA96B File Offset: 0x001B8B6B
		// (set) Token: 0x06006C6E RID: 27758 RVA: 0x001BA973 File Offset: 0x001B8B73
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.RejectReasonDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.RejectReasonDisplayName)]
		public DsnText? RejectMessageReasonText
		{
			get
			{
				return this.rejectMessageReasonText;
			}
			set
			{
				this.rejectMessageReasonText = value;
			}
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x06006C6F RID: 27759 RVA: 0x001BA97C File Offset: 0x001B8B7C
		// (set) Token: 0x06006C70 RID: 27760 RVA: 0x001BA984 File Offset: 0x001B8B84
		public bool DeleteMessage
		{
			get
			{
				return this.deleteMessage;
			}
			set
			{
				this.deleteMessage = value;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x06006C71 RID: 27761 RVA: 0x001BA98D File Offset: 0x001B8B8D
		// (set) Token: 0x06006C72 RID: 27762 RVA: 0x001BA995 File Offset: 0x001B8B95
		public bool Disconnect
		{
			get
			{
				return this.disconnect;
			}
			set
			{
				this.disconnect = value;
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06006C73 RID: 27763 RVA: 0x001BA99E File Offset: 0x001B8B9E
		// (set) Token: 0x06006C74 RID: 27764 RVA: 0x001BA9A6 File Offset: 0x001B8BA6
		public bool Quarantine
		{
			get
			{
				return this.quarantine;
			}
			set
			{
				this.quarantine = value;
			}
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06006C75 RID: 27765 RVA: 0x001BA9AF File Offset: 0x001B8BAF
		// (set) Token: 0x06006C76 RID: 27766 RVA: 0x001BA9B7 File Offset: 0x001B8BB7
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.RejectReasonDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.RejectReasonDescription)]
		public RejectText? SmtpRejectMessageRejectText
		{
			get
			{
				return this.smtpRejectMessageRejectText;
			}
			set
			{
				this.smtpRejectMessageRejectText = value;
			}
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06006C77 RID: 27767 RVA: 0x001BA9C0 File Offset: 0x001B8BC0
		// (set) Token: 0x06006C78 RID: 27768 RVA: 0x001BA9C8 File Offset: 0x001B8BC8
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.StatusCodeDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.StatusCodeDisplayName)]
		public RejectStatusCode? SmtpRejectMessageRejectStatusCode
		{
			get
			{
				return this.smtpRejectMessageRejectStatusCode;
			}
			set
			{
				this.smtpRejectMessageRejectStatusCode = value;
			}
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06006C79 RID: 27769 RVA: 0x001BA9D1 File Offset: 0x001B8BD1
		// (set) Token: 0x06006C7A RID: 27770 RVA: 0x001BA9D9 File Offset: 0x001B8BD9
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.EventMessageDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.EventMessageDescription)]
		public EventLogText? LogEventText
		{
			get
			{
				return this.logEventText;
			}
			set
			{
				this.logEventText = value;
			}
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06006C7B RID: 27771 RVA: 0x001BA9E2 File Offset: 0x001B8BE2
		// (set) Token: 0x06006C7C RID: 27772 RVA: 0x001BA9EA File Offset: 0x001B8BEA
		public bool StopRuleProcessing
		{
			get
			{
				return this.stopRuleProcessing;
			}
			set
			{
				this.stopRuleProcessing = value;
			}
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x06006C7D RID: 27773 RVA: 0x001BA9F3 File Offset: 0x001B8BF3
		// (set) Token: 0x06006C7E RID: 27774 RVA: 0x001BA9FB File Offset: 0x001B8BFB
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.SenderNotificationTypeDisplayName)]
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.SenderNotificationTypeDescription)]
		public NotifySenderType? SenderNotificationType
		{
			get
			{
				return this.senderNotificationType;
			}
			set
			{
				this.senderNotificationType = value;
			}
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06006C7F RID: 27775 RVA: 0x001BAA04 File Offset: 0x001B8C04
		// (set) Token: 0x06006C80 RID: 27776 RVA: 0x001BAA0C File Offset: 0x001B8C0C
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.ReportDestinationDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.ReportDestinationDisplayName)]
		public RecipientIdParameter GenerateIncidentReport
		{
			get
			{
				return this.generateIncidentReport;
			}
			set
			{
				this.generateIncidentReport = value;
			}
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x06006C81 RID: 27777 RVA: 0x001BAA15 File Offset: 0x001B8C15
		// (set) Token: 0x06006C82 RID: 27778 RVA: 0x001BAA1D File Offset: 0x001B8C1D
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.IncidentReportOriginalMailDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.IncidentReportOriginalMailnDisplayName)]
		public IncidentReportOriginalMail? IncidentReportOriginalMail
		{
			get
			{
				return this.incidentReportOriginalMail;
			}
			set
			{
				this.incidentReportOriginalMail = value;
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x06006C83 RID: 27779 RVA: 0x001BAA26 File Offset: 0x001B8C26
		// (set) Token: 0x06006C84 RID: 27780 RVA: 0x001BAA2E File Offset: 0x001B8C2E
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.IncidentReportContentDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.IncidentReportContentDisplayName)]
		public IncidentReportContent[] IncidentReportContent
		{
			get
			{
				return this.incidentReportContent;
			}
			set
			{
				this.incidentReportContent = value;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x06006C85 RID: 27781 RVA: 0x001BAA37 File Offset: 0x001B8C37
		// (set) Token: 0x06006C86 RID: 27782 RVA: 0x001BAA3F File Offset: 0x001B8C3F
		public string RouteMessageOutboundConnector
		{
			get
			{
				return this.connectorName;
			}
			set
			{
				this.connectorName = value;
			}
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x06006C87 RID: 27783 RVA: 0x001BAA48 File Offset: 0x001B8C48
		// (set) Token: 0x06006C88 RID: 27784 RVA: 0x001BAA50 File Offset: 0x001B8C50
		public bool RouteMessageOutboundRequireTls { get; set; }

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x06006C89 RID: 27785 RVA: 0x001BAA59 File Offset: 0x001B8C59
		// (set) Token: 0x06006C8A RID: 27786 RVA: 0x001BAA61 File Offset: 0x001B8C61
		public bool ApplyOME { get; set; }

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x06006C8B RID: 27787 RVA: 0x001BAA6A File Offset: 0x001B8C6A
		// (set) Token: 0x06006C8C RID: 27788 RVA: 0x001BAA72 File Offset: 0x001B8C72
		public bool RemoveOME { get; set; }

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x06006C8D RID: 27789 RVA: 0x001BAA7B File Offset: 0x001B8C7B
		// (set) Token: 0x06006C8E RID: 27790 RVA: 0x001BAA83 File Offset: 0x001B8C83
		[Microsoft.Exchange.Core.RuleTasks.LocDescription(RulesTasksStrings.IDs.GenerateNotificationDescription)]
		[Microsoft.Exchange.Core.RuleTasks.LocDisplayName(RulesTasksStrings.IDs.GenerateNotificationDisplayName)]
		public DisclaimerText? GenerateNotification
		{
			get
			{
				return this.generateNotification;
			}
			set
			{
				this.generateNotification = value;
			}
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x001BAA8C File Offset: 0x001B8C8C
		public override ValidationError[] Validate()
		{
			if (!this.isValid)
			{
				return new ValidationError[]
				{
					new ObjectValidationError(this.errorText, base.Identity, null)
				};
			}
			List<ValidationError> list = new List<ValidationError>();
			ValidationError[] collection = Rule.ValidatePhraseArray(this.Conditions, true, false);
			list.AddRange(collection);
			collection = Rule.ValidatePhraseArray(this.Actions, true, false);
			list.AddRange(collection);
			collection = Rule.ValidatePhraseArray(this.Exceptions, true, false);
			list.AddRange(collection);
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return ValidationError.None;
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x001BAB18 File Offset: 0x001B8D18
		internal static Rule CreateCorruptRule(int priority, TransportRule transportRule, LocalizedString errorText)
		{
			return new Rule(transportRule, transportRule.Name, priority, null, Guid.Empty, null, true, null, null, null, null, null, RuleState.Disabled, RuleMode.Enforce, RuleSubType.None, RuleErrorAction.Ignore, SenderAddressLocation.Header)
			{
				isValid = false,
				errorText = errorText
			};
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x001BAB64 File Offset: 0x001B8D64
		internal static Rule CreateAdvancedRule(int priority, TransportRule transportRule, RuleState state)
		{
			return new Rule(transportRule, transportRule.Name, priority, null, Guid.Empty, null, true, null, null, null, null, null, state, RuleMode.Enforce, RuleSubType.None, RuleErrorAction.Ignore, SenderAddressLocation.Header);
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x001BABA4 File Offset: 0x001B8DA4
		internal static Rule CreateFromInternalRule(TypeMapping[] supportedPredicates, TypeMapping[] supportedActions, TransportRule rule, int priority, TransportRule transportRule)
		{
			IConfigDataProvider configDataProvider = null;
			if (transportRule != null)
			{
				configDataProvider = transportRule.Session;
			}
			TransportRulePredicate[] array;
			TransportRulePredicate[] array2;
			TransportRuleAction[] array3;
			bool flag = Rule.TryConvert(supportedPredicates, supportedActions, rule, out array, out array2, out array3, configDataProvider);
			string text = null;
			Guid guid;
			if (rule.TryGetDlpPolicyId(out guid) && configDataProvider != null)
			{
				ADComplianceProgram adcomplianceProgram = DlpUtils.GetInstalledTenantDlpPolicies(configDataProvider, guid.ToString()).FirstOrDefault<ADComplianceProgram>();
				if (adcomplianceProgram != null)
				{
					text = adcomplianceProgram.Name;
				}
			}
			return new Rule(transportRule, rule.Name, priority, text, guid, rule.Comments, !flag, rule.ActivationDate, rule.ExpiryDate, array, array2, array3, rule.Enabled, rule.Mode, rule.SubType, rule.ErrorAction, rule.SenderAddressLocation);
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x001BAC54 File Offset: 0x001B8E54
		internal TransportRule ToInternalRule()
		{
			if (this.ManuallyModified)
			{
				throw new InvalidOperationException();
			}
			AndCondition andCondition = new AndCondition();
			List<RuleBifurcationInfo> list = new List<RuleBifurcationInfo>();
			if (this.actions == null || this.actions.Length == 0)
			{
				throw new ArgumentException(RulesTasksStrings.NoAction, "Actions");
			}
			andCondition.SubConditions.Add(Condition.True);
			int num = -1;
			if (this.conditions != null)
			{
				Rule.ValidatePhraseArray(this.conditions, true, true);
				foreach (TransportRulePredicate transportRulePredicate in this.conditions)
				{
					if (transportRulePredicate.Rank <= num)
					{
						throw new PredicateOrActionSequenceException(string.Format("Either multiple predicates of the same type have been added to this rule, or the order in which the predicates have been added is incorrect - detected when processing predicate type {0}", transportRulePredicate.GetType()), null);
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
						Condition item2 = transportRulePredicate.ToInternalCondition();
						andCondition.SubConditions.Add(item2);
					}
				}
			}
			if (this.exceptions != null && this.exceptions.Length > 0)
			{
				OrCondition orCondition = new OrCondition();
				andCondition.SubConditions.Add(new NotCondition(orCondition));
				Rule.ValidatePhraseArray(this.exceptions, true, true);
				num = -1;
				foreach (TransportRulePredicate transportRulePredicate2 in this.exceptions)
				{
					if (transportRulePredicate2.Rank <= num)
					{
						throw new PredicateOrActionSequenceException(string.Format("Either multiple exceptions of the same type have been added to this rule, or the order in which the exceptions have been added is incorrect - detected when processing exception type {0}", transportRulePredicate2.GetType()), null);
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
						Condition item3 = transportRulePredicate2.ToInternalCondition();
						orCondition.SubConditions.Add(item3);
					}
				}
				if (orCondition.SubConditions.Count == 0)
				{
					orCondition.SubConditions.Add(Condition.False);
				}
			}
			TransportRule transportRule = new TransportRule(base.Name, andCondition);
			transportRule.Enabled = this.State;
			transportRule.Mode = this.Mode;
			transportRule.ErrorAction = this.RuleErrorAction;
			transportRule.SenderAddressLocation = this.SenderAddressLocation;
			transportRule.SubType = this.RuleSubType;
			if (!Guid.Empty.Equals(this.dlpPolicyId))
			{
				transportRule.SetDlpPolicy(this.dlpPolicyId, this.dlpPolicy);
			}
			if (this.comments != null)
			{
				transportRule.Comments = this.comments;
			}
			if (list.Count > 0)
			{
				transportRule.Fork = list;
			}
			if (this.activationDate != null)
			{
				transportRule.ActivationDate = this.activationDate;
			}
			if (this.expiryDate != null)
			{
				transportRule.ExpiryDate = this.expiryDate;
			}
			Rule.ValidatePhraseArray(this.actions, false, true);
			num = -1;
			foreach (TransportRuleAction transportRuleAction in this.actions)
			{
				if (transportRuleAction.Rank <= num)
				{
					throw new PredicateOrActionSequenceException(string.Format("Either multiple actions of the same type have been added to this rule, or the order in which the actions have been added is incorrect - detected when processing action type {0}", transportRuleAction.GetType()), null);
				}
				num = transportRuleAction.Rank;
				foreach (Action item4 in transportRuleAction.ToInternalActions())
				{
					transportRule.Actions.Add(item4);
				}
			}
			return transportRule;
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x001BAFCC File Offset: 0x001B91CC
		internal static bool TryConvert(TypeMapping[] supportedPredicates, TypeMapping[] supportedActions, TransportRule rule, out TransportRulePredicate[] conditions, out TransportRulePredicate[] exceptions, out TransportRuleAction[] actions, IConfigDataProvider session)
		{
			conditions = null;
			exceptions = null;
			List<TransportRulePredicate> list = new List<TransportRulePredicate>();
			List<TransportRulePredicate> list2 = new List<TransportRulePredicate>();
			if (!Rule.TryConvertConditionAndException(supportedPredicates, rule, list, list2, session))
			{
				actions = null;
				return false;
			}
			if (list.Count > 0)
			{
				conditions = list.ToArray();
			}
			if (list2.Count > 0)
			{
				exceptions = list2.ToArray();
			}
			if (!Rule.TryConvertActions(supportedActions, rule, out actions, session))
			{
				conditions = null;
				exceptions = null;
				return false;
			}
			return true;
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x001BB03C File Offset: 0x001B923C
		private static bool TryConvertConditionAndException(TypeMapping[] supportedPredicates, TransportRule rule, List<TransportRulePredicate> conditions, List<TransportRulePredicate> exceptions, IConfigDataProvider session)
		{
			if (rule.Condition.ConditionType != ConditionType.And)
			{
				return false;
			}
			AndCondition andCondition = (AndCondition)rule.Condition;
			if (andCondition.SubConditions.Count < 1 || andCondition.SubConditions[0].ConditionType != ConditionType.True)
			{
				return false;
			}
			foreach (Condition condition in andCondition.SubConditions)
			{
				if (condition.ConditionType != ConditionType.True)
				{
					TransportRulePredicate item;
					if (TransportRulePredicate.TryCreatePredicateFromCondition(supportedPredicates, condition, out item, session))
					{
						conditions.Add(item);
					}
					else
					{
						if (condition != andCondition.SubConditions[andCondition.SubConditions.Count - 1] || condition.ConditionType != ConditionType.Not)
						{
							return false;
						}
						if (!Rule.TryConvertException(supportedPredicates, ((NotCondition)condition).SubCondition, exceptions, session))
						{
							return false;
						}
					}
				}
			}
			if (rule.Fork != null)
			{
				for (int i = 0; i < rule.Fork.Count; i++)
				{
					RuleBifurcationInfo ruleBifurcationInfo = rule.Fork[i];
					RuleBifurcationInfo bifInfo = (i + 1 < rule.Fork.Count) ? rule.Fork[i + 1] : null;
					TransportRulePredicate predicate;
					bool flag;
					if (!BifurcationInfoPredicate.TryCreatePredicateFromBifInfo(supportedPredicates, ruleBifurcationInfo, bifInfo, out predicate, out flag))
					{
						return false;
					}
					bool flag2;
					if (ruleBifurcationInfo.Exception)
					{
						flag2 = Rule.TryInsertPredicateSorted(predicate, exceptions);
					}
					else
					{
						flag2 = Rule.TryInsertPredicateSorted(predicate, conditions);
					}
					if (!flag2)
					{
						return false;
					}
					if (flag)
					{
						i++;
					}
				}
			}
			return true;
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x001BB1CC File Offset: 0x001B93CC
		private static bool TryInsertPredicateSorted(TransportRulePredicate predicate, List<TransportRulePredicate> predicateList)
		{
			int rank = predicate.Rank;
			int num = 0;
			while (num < predicateList.Count && rank >= predicateList[num].Rank)
			{
				if (rank == predicateList[num].Rank)
				{
					return false;
				}
				num++;
			}
			predicateList.Insert(num, predicate);
			return true;
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x001BB21C File Offset: 0x001B941C
		private static bool TryConvertException(TypeMapping[] supportedPredicates, Condition conditionTree, List<TransportRulePredicate> exceptions, IConfigDataProvider session)
		{
			if (conditionTree.ConditionType != ConditionType.Or)
			{
				return false;
			}
			OrCondition orCondition = (OrCondition)conditionTree;
			foreach (Condition condition in orCondition.SubConditions)
			{
				if (condition.ConditionType != ConditionType.False)
				{
					TransportRulePredicate item;
					if (!TransportRulePredicate.TryCreatePredicateFromCondition(supportedPredicates, condition, out item, session))
					{
						return false;
					}
					exceptions.Add(item);
				}
			}
			return true;
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x001BB2A0 File Offset: 0x001B94A0
		private static bool TryConvertActions(TypeMapping[] supportedActions, TransportRule rule, out TransportRuleAction[] actions, IConfigDataProvider session)
		{
			List<TransportRuleAction> list = new List<TransportRuleAction>();
			actions = null;
			if (rule.Actions.Count == 0)
			{
				return false;
			}
			int num = -1;
			int i = 0;
			while (i < rule.Actions.Count)
			{
				TransportRuleAction transportRuleAction;
				if (!TransportRuleAction.TryCreateFromInternalAction(supportedActions, rule.Actions, ref i, out transportRuleAction, session))
				{
					return false;
				}
				if (transportRuleAction.Rank <= num)
				{
					return false;
				}
				num = transportRuleAction.Rank;
				list.Add(transportRuleAction);
			}
			actions = list.ToArray();
			return true;
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x001BB314 File Offset: 0x001B9514
		private static ValidationError[] ValidatePhraseArray(RulePhrase[] phrases, bool isPredicate, bool throwException)
		{
			List<ValidationError> list = new List<ValidationError>();
			if (phrases != null)
			{
				foreach (RulePhrase rulePhrase in phrases)
				{
					if (rulePhrase == null)
					{
						if (isPredicate)
						{
							if (throwException)
							{
								throw new ArgumentException(RulesTasksStrings.InvalidPredicate, "Predicates");
							}
							list.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidPredicate, "Predicates"));
						}
						else
						{
							if (throwException)
							{
								throw new ArgumentException(RulesTasksStrings.InvalidAction, "Actions");
							}
							list.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidAction, "Actions"));
						}
					}
					else
					{
						ValidationError[] array = rulePhrase.Validate();
						if (array != null && array.Length != 0)
						{
							list.AddRange(array);
						}
					}
				}
			}
			if (throwException)
			{
				List<RulePhrase.RulePhraseValidationError> list2 = new List<RulePhrase.RulePhraseValidationError>();
				foreach (ValidationError validationError in list)
				{
					RulePhrase.RulePhraseValidationError rulePhraseValidationError = validationError as RulePhrase.RulePhraseValidationError;
					if (rulePhraseValidationError != null)
					{
						list2.Add(rulePhraseValidationError);
					}
				}
				if (list2.Any<RulePhrase.RulePhraseValidationError>())
				{
					List<ValidationError> errors = (from ruleError in list2
					select ruleError).ToList<ValidationError>();
					LocalizedString value = ValidationError.CombineErrorDescriptions(errors);
					string name = list2[0].Name;
					throw new ArgumentException(value, name);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x001BB484 File Offset: 0x001B9684
		private void SetParametersFromPredicate(TransportRulePredicate predicate, bool isException, OrganizationId orgId)
		{
			if (predicate is FromPredicate)
			{
				if (isException)
				{
					this.exceptIfFrom = Utils.BuildRecipientIdArray(((FromPredicate)predicate).Addresses);
					return;
				}
				this.from = Utils.BuildRecipientIdArray(((FromPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is FromMemberOfPredicate)
			{
				if (isException)
				{
					this.exceptIfFromMemberOf = Utils.BuildRecipientIdArray(((FromMemberOfPredicate)predicate).Addresses);
					return;
				}
				this.fromMemberOf = Utils.BuildRecipientIdArray(((FromMemberOfPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is FromScopePredicate)
			{
				if (isException)
				{
					this.exceptIfFromScope = new FromUserScope?(((FromScopePredicate)predicate).Scope);
					return;
				}
				this.fromScope = new FromUserScope?(((FromScopePredicate)predicate).Scope);
				return;
			}
			else if (predicate is SentToPredicate)
			{
				if (isException)
				{
					this.exceptIfSentTo = Utils.BuildRecipientIdArray(((SentToPredicate)predicate).Addresses);
					return;
				}
				this.sentTo = Utils.BuildRecipientIdArray(((SentToPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is SentToMemberOfPredicate)
			{
				if (isException)
				{
					this.exceptIfSentToMemberOf = Utils.BuildRecipientIdArray(((SentToMemberOfPredicate)predicate).Addresses);
					return;
				}
				this.sentToMemberOf = Utils.BuildRecipientIdArray(((SentToMemberOfPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is SentToScopePredicate)
			{
				if (isException)
				{
					this.exceptIfSentToScope = new ToUserScope?(((SentToScopePredicate)predicate).Scope);
					return;
				}
				this.sentToScope = new ToUserScope?(((SentToScopePredicate)predicate).Scope);
				return;
			}
			else if (predicate is BetweenMemberOfPredicate)
			{
				if (isException)
				{
					this.exceptIfBetweenMemberOf1 = Utils.BuildRecipientIdArray(((BetweenMemberOfPredicate)predicate).Addresses);
					this.exceptIfBetweenMemberOf2 = Utils.BuildRecipientIdArray(((BetweenMemberOfPredicate)predicate).Addresses2);
					return;
				}
				this.betweenMemberOf1 = Utils.BuildRecipientIdArray(((BetweenMemberOfPredicate)predicate).Addresses);
				this.betweenMemberOf2 = Utils.BuildRecipientIdArray(((BetweenMemberOfPredicate)predicate).Addresses2);
				return;
			}
			else if (predicate is ManagerIsPredicate)
			{
				if (isException)
				{
					this.exceptIfManagerAddresses = Utils.BuildRecipientIdArray(((ManagerIsPredicate)predicate).Addresses);
					this.exceptIfManagerForEvaluatedUser = new EvaluatedUser?(((ManagerIsPredicate)predicate).EvaluatedUser);
					return;
				}
				this.managerAddresses = Utils.BuildRecipientIdArray(((ManagerIsPredicate)predicate).Addresses);
				this.managerForEvaluatedUser = new EvaluatedUser?(((ManagerIsPredicate)predicate).EvaluatedUser);
				return;
			}
			else if (predicate is ManagementRelationshipPredicate)
			{
				if (isException)
				{
					this.exceptIfSenderManagementRelationship = new ManagementRelationship?(((ManagementRelationshipPredicate)predicate).ManagementRelationship);
					return;
				}
				this.senderManagementRelationship = new ManagementRelationship?(((ManagementRelationshipPredicate)predicate).ManagementRelationship);
				return;
			}
			else if (predicate is ADAttributeComparisonPredicate)
			{
				if (isException)
				{
					this.exceptIfADComparisonAttribute = new ADAttribute?(((ADAttributeComparisonPredicate)predicate).ADAttribute);
					this.exceptIfADComparisonOperator = new Evaluation?(((ADAttributeComparisonPredicate)predicate).Evaluation);
					return;
				}
				this.adComparisonAttribute = new ADAttribute?(((ADAttributeComparisonPredicate)predicate).ADAttribute);
				this.adComparisonOperator = new Evaluation?(((ADAttributeComparisonPredicate)predicate).Evaluation);
				return;
			}
			else if (predicate is AnyOfToHeaderPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfToHeader = Utils.BuildRecipientIdArray(((AnyOfToHeaderPredicate)predicate).Addresses);
					return;
				}
				this.anyOfToHeader = Utils.BuildRecipientIdArray(((AnyOfToHeaderPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is AnyOfToHeaderMemberOfPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfToHeaderMemberOf = Utils.BuildRecipientIdArray(((AnyOfToHeaderMemberOfPredicate)predicate).Addresses);
					return;
				}
				this.anyOfToHeaderMemberOf = Utils.BuildRecipientIdArray(((AnyOfToHeaderMemberOfPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is AnyOfCcHeaderPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfCcHeader = Utils.BuildRecipientIdArray(((AnyOfCcHeaderPredicate)predicate).Addresses);
					return;
				}
				this.anyOfCcHeader = Utils.BuildRecipientIdArray(((AnyOfCcHeaderPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is AnyOfCcHeaderMemberOfPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfCcHeaderMemberOf = Utils.BuildRecipientIdArray(((AnyOfCcHeaderMemberOfPredicate)predicate).Addresses);
					return;
				}
				this.anyOfCcHeaderMemberOf = Utils.BuildRecipientIdArray(((AnyOfCcHeaderMemberOfPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is AnyOfToCcHeaderPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfToCcHeader = Utils.BuildRecipientIdArray(((AnyOfToCcHeaderPredicate)predicate).Addresses);
					return;
				}
				this.anyOfToCcHeader = Utils.BuildRecipientIdArray(((AnyOfToCcHeaderPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is AnyOfToCcHeaderMemberOfPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfToCcHeaderMemberOf = Utils.BuildRecipientIdArray(((AnyOfToCcHeaderMemberOfPredicate)predicate).Addresses);
					return;
				}
				this.anyOfToCcHeaderMemberOf = Utils.BuildRecipientIdArray(((AnyOfToCcHeaderMemberOfPredicate)predicate).Addresses);
				return;
			}
			else if (predicate is HasClassificationPredicate)
			{
				if (isException)
				{
					this.exceptIfHasClassification = ((HasClassificationPredicate)predicate).Classification;
					return;
				}
				this.hasClassification = ((HasClassificationPredicate)predicate).Classification;
				return;
			}
			else if (predicate is HasNoClassificationPredicate)
			{
				if (isException)
				{
					this.exceptIfHasNoClassification = true;
					return;
				}
				this.hasNoClassification = true;
				return;
			}
			else if (predicate is SubjectContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfSubjectContainsWords = ((SubjectContainsPredicate)predicate).Words;
					return;
				}
				this.subjectContainsWords = ((SubjectContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is SubjectOrBodyContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfSubjectOrBodyContainsWords = ((SubjectOrBodyContainsPredicate)predicate).Words;
					return;
				}
				this.subjectOrBodyContainsWords = ((SubjectOrBodyContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is HeaderContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfHeaderContainsMessageHeader = new HeaderName?(((HeaderContainsPredicate)predicate).MessageHeader);
					this.exceptIfHeaderContainsWords = ((HeaderContainsPredicate)predicate).Words;
					return;
				}
				this.headerContainsMessageHeader = new HeaderName?(((HeaderContainsPredicate)predicate).MessageHeader);
				this.headerContainsWords = ((HeaderContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is FromAddressContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfFromAddressContainsWords = ((FromAddressContainsPredicate)predicate).Words;
					return;
				}
				this.fromAddressContainsWords = ((FromAddressContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is SenderDomainIsPredicate)
			{
				if (isException)
				{
					this.exceptIfSenderDomainIs = (predicate as SenderDomainIsPredicate).Words;
					return;
				}
				this.SenderDomainIs = (predicate as SenderDomainIsPredicate).Words;
				return;
			}
			else if (predicate is RecipientDomainIsPredicate)
			{
				if (isException)
				{
					this.exceptIfRecipientDomainIs = (predicate as RecipientDomainIsPredicate).Words;
					return;
				}
				this.recipientDomainIs = (predicate as RecipientDomainIsPredicate).Words;
				return;
			}
			else if (predicate is RecipientAddressContainsWordsPredicate)
			{
				if (isException)
				{
					this.exceptIfRecipientAddressContainsWords = ((RecipientAddressContainsWordsPredicate)predicate).Words;
					return;
				}
				this.recipientAddressContainsWords = ((RecipientAddressContainsWordsPredicate)predicate).Words;
				return;
			}
			else if (predicate is RecipientAddressMatchesPatternsPredicate)
			{
				if (isException)
				{
					this.exceptIfRecipientAddressMatchesPatterns = ((RecipientAddressMatchesPatternsPredicate)predicate).Patterns;
					return;
				}
				this.recipientAddressMatchesPatterns = ((RecipientAddressMatchesPatternsPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is SubjectMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfSubjectMatchesPatterns = ((SubjectMatchesPredicate)predicate).Patterns;
					return;
				}
				this.subjectMatchesPatterns = ((SubjectMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is SubjectOrBodyMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfSubjectOrBodyMatchesPatterns = ((SubjectOrBodyMatchesPredicate)predicate).Patterns;
					return;
				}
				this.subjectOrBodyMatchesPatterns = ((SubjectOrBodyMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is HeaderMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfHeaderMatchesMessageHeader = new HeaderName?(((HeaderMatchesPredicate)predicate).MessageHeader);
					this.exceptIfHeaderMatchesPatterns = ((HeaderMatchesPredicate)predicate).Patterns;
					return;
				}
				this.headerMatchesMessageHeader = new HeaderName?(((HeaderMatchesPredicate)predicate).MessageHeader);
				this.headerMatchesPatterns = ((HeaderMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is FromAddressMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfFromAddressMatchesPatterns = ((FromAddressMatchesPredicate)predicate).Patterns;
					return;
				}
				this.fromAddressMatchesPatterns = ((FromAddressMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is AttachmentNameMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentNameMatchesPatterns = ((AttachmentNameMatchesPredicate)predicate).Patterns;
					return;
				}
				this.attachmentNameMatchesPatterns = ((AttachmentNameMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is AttachmentExtensionMatchesWordsPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentExtensionMatchesWords = ((AttachmentExtensionMatchesWordsPredicate)predicate).Words;
					return;
				}
				this.attachmentExtensionMatchesWords = ((AttachmentExtensionMatchesWordsPredicate)predicate).Words;
				return;
			}
			else if (predicate is AttachmentPropertyContainsWordsPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentPropertyContainsWords = ((AttachmentPropertyContainsWordsPredicate)predicate).Words;
					return;
				}
				this.attachmentPropertyContainsWords = ((AttachmentPropertyContainsWordsPredicate)predicate).Words;
				return;
			}
			else if (predicate is ContentCharacterSetContainsWordsPredicate)
			{
				if (isException)
				{
					this.exceptIfContentCharacterSetContainsWords = ((ContentCharacterSetContainsWordsPredicate)predicate).Words;
					return;
				}
				this.contentCharacterSetContainsWords = ((ContentCharacterSetContainsWordsPredicate)predicate).Words;
				return;
			}
			else if (predicate is SclOverPredicate)
			{
				if (isException)
				{
					this.exceptIfSCLOver = new SclValue?(((SclOverPredicate)predicate).SclValue);
					return;
				}
				this.sclOver = new SclValue?(((SclOverPredicate)predicate).SclValue);
				return;
			}
			else if (predicate is AttachmentSizeOverPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentSizeOver = new ByteQuantifiedSize?(((AttachmentSizeOverPredicate)predicate).Size);
					return;
				}
				this.attachmentSizeOver = new ByteQuantifiedSize?(((AttachmentSizeOverPredicate)predicate).Size);
				return;
			}
			else if (predicate is MessageSizeOverPredicate)
			{
				if (isException)
				{
					this.exceptIfMessageSizeOver = new ByteQuantifiedSize?(((MessageSizeOverPredicate)predicate).Size);
					return;
				}
				this.messageSizeOver = new ByteQuantifiedSize?(((MessageSizeOverPredicate)predicate).Size);
				return;
			}
			else if (predicate is WithImportancePredicate)
			{
				if (isException)
				{
					this.exceptIfWithImportance = new Importance?(((WithImportancePredicate)predicate).Importance);
					return;
				}
				this.withImportance = new Importance?(((WithImportancePredicate)predicate).Importance);
				return;
			}
			else if (predicate is MessageTypeMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfMessageTypeMatches = new MessageType?(((MessageTypeMatchesPredicate)predicate).MessageType);
					return;
				}
				this.messageTypeMatches = new MessageType?(((MessageTypeMatchesPredicate)predicate).MessageType);
				return;
			}
			else if (predicate is SenderAttributeContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfSenderADAttributeContainsWords = ((SenderAttributeContainsPredicate)predicate).Words;
					return;
				}
				this.senderADAttributeContainsWords = ((SenderAttributeContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is RecipientAttributeContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfRecipientADAttributeContainsWords = ((RecipientAttributeContainsPredicate)predicate).Words;
					return;
				}
				this.recipientADAttributeContainsWords = ((RecipientAttributeContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is SenderAttributeMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfSenderADAttributeMatchesPatterns = ((SenderAttributeMatchesPredicate)predicate).Patterns;
					return;
				}
				this.senderADAttributeMatchesPatterns = ((SenderAttributeMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is RecipientAttributeMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfRecipientADAttributeMatchesPatterns = ((RecipientAttributeMatchesPredicate)predicate).Patterns;
					return;
				}
				this.recipientADAttributeMatchesPatterns = ((RecipientAttributeMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is RecipientInSenderListPredicate)
			{
				if (isException)
				{
					this.exceptIfRecipientInSenderList = ((RecipientInSenderListPredicate)predicate).Lists;
					return;
				}
				this.recipientInSenderList = ((RecipientInSenderListPredicate)predicate).Lists;
				return;
			}
			else if (predicate is SenderInRecipientListPredicate)
			{
				if (isException)
				{
					this.exceptIfSenderInRecipientList = ((SenderInRecipientListPredicate)predicate).Lists;
					return;
				}
				this.senderInRecipientList = ((SenderInRecipientListPredicate)predicate).Lists;
				return;
			}
			else if (predicate is AttachmentContainsWordsPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentContainsWords = ((AttachmentContainsWordsPredicate)predicate).Words;
					return;
				}
				this.attachmentContainsWords = ((AttachmentContainsWordsPredicate)predicate).Words;
				return;
			}
			else if (predicate is AttachmentMatchesPatternsPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentMatchesPatterns = ((AttachmentMatchesPatternsPredicate)predicate).Patterns;
					return;
				}
				this.attachmentMatchesPatterns = ((AttachmentMatchesPatternsPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is AttachmentIsUnsupportedPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentIsUnsupported = true;
					return;
				}
				this.attachmentIsUnsupported = true;
				return;
			}
			else if (predicate is AttachmentProcessingLimitExceededPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentProcessingLimitExceeded = true;
					return;
				}
				this.attachmentProcessingLimitExceeded = true;
				return;
			}
			else if (predicate is AttachmentHasExecutableContentPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentHasExecutableContent = true;
					return;
				}
				this.attachmentHasExecutableContent = true;
				return;
			}
			else if (predicate is AttachmentIsPasswordProtectedPredicate)
			{
				if (isException)
				{
					this.exceptIfAttachmentIsPasswordProtected = true;
					return;
				}
				this.attachmentIsPasswordProtected = true;
				return;
			}
			else if (predicate is AnyOfRecipientAddressContainsPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfRecipientAddressContainsWords = ((AnyOfRecipientAddressContainsPredicate)predicate).Words;
					return;
				}
				this.anyOfRecipientAddressContainsWords = ((AnyOfRecipientAddressContainsPredicate)predicate).Words;
				return;
			}
			else if (predicate is AnyOfRecipientAddressMatchesPredicate)
			{
				if (isException)
				{
					this.exceptIfAnyOfRecipientAddressMatchesPatterns = ((AnyOfRecipientAddressMatchesPredicate)predicate).Patterns;
					return;
				}
				this.anyOfRecipientAddressMatchesPatterns = ((AnyOfRecipientAddressMatchesPredicate)predicate).Patterns;
				return;
			}
			else if (predicate is MessageContainsDataClassificationsPredicate)
			{
				((MessageContainsDataClassificationsPredicate)predicate).OrganizationId = orgId;
				if (isException)
				{
					this.exceptIfMessageContainsDataClassifications = MessageContainsDataClassificationsPredicate.HashtablesToStrings(((MessageContainsDataClassificationsPredicate)predicate).DataClassifications, orgId).ToArray<string>();
					return;
				}
				this.messageContainsDataClassifications = MessageContainsDataClassificationsPredicate.HashtablesToStrings(((MessageContainsDataClassificationsPredicate)predicate).DataClassifications, orgId).ToArray<string>();
				return;
			}
			else
			{
				if (!(predicate is SenderIpRangesPredicate))
				{
					if (predicate is HasSenderOverridePredicate)
					{
						if (isException)
						{
							this.exceptIfHasSenderOverride = true;
							return;
						}
						this.hasSenderOverride = true;
					}
					return;
				}
				if (isException)
				{
					this.ExceptIfSenderIpRanges = ((SenderIpRangesPredicate)predicate).IpRanges.ToArray();
					return;
				}
				this.SenderIpRanges = ((SenderIpRangesPredicate)predicate).IpRanges.ToArray();
				return;
			}
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x001BC044 File Offset: 0x001BA244
		private void SetParametersFromAction(TransportRuleAction action)
		{
			if (action is PrependSubjectAction)
			{
				this.prependSubject = ((PrependSubjectAction)action).Prefix.ToString();
				return;
			}
			if (action is SetAuditSeverityAction)
			{
				this.setAuditSeverity = ((SetAuditSeverityAction)action).SeverityLevel.ToString();
				return;
			}
			if (action is ApplyClassificationAction)
			{
				this.applyClassification = ((ApplyClassificationAction)action).Classification;
				return;
			}
			if (action is ApplyHtmlDisclaimerAction)
			{
				this.applyHtmlDisclaimerLocation = new DisclaimerLocation?(((ApplyHtmlDisclaimerAction)action).Location);
				this.applyHtmlDisclaimerText = new DisclaimerText?(((ApplyHtmlDisclaimerAction)action).Text);
				this.applyHtmlDisclaimerFallbackAction = new DisclaimerFallbackAction?(((ApplyHtmlDisclaimerAction)action).FallbackAction);
				return;
			}
			if (action is RightsProtectMessageAction)
			{
				this.applyRightsProtectionTemplate = ((RightsProtectMessageAction)action).Template;
				return;
			}
			if (action is SetSclAction)
			{
				this.setSCL = new SclValue?(((SetSclAction)action).SclValue);
				return;
			}
			if (action is SetHeaderAction)
			{
				this.setHeaderName = new HeaderName?(((SetHeaderAction)action).MessageHeader);
				this.setHeaderValue = new HeaderValue?(((SetHeaderAction)action).HeaderValue);
				return;
			}
			if (action is RemoveHeaderAction)
			{
				this.removeHeader = new HeaderName?(((RemoveHeaderAction)action).MessageHeader);
				return;
			}
			if (action is AddToRecipientAction)
			{
				this.addToRecipients = Utils.BuildRecipientIdArray(((AddToRecipientAction)action).Addresses);
				return;
			}
			if (action is CopyToAction)
			{
				this.copyTo = Utils.BuildRecipientIdArray(((CopyToAction)action).Addresses);
				return;
			}
			if (action is BlindCopyToAction)
			{
				this.blindCopyTo = Utils.BuildRecipientIdArray(((BlindCopyToAction)action).Addresses);
				return;
			}
			if (action is AddManagerAsRecipientTypeAction)
			{
				this.addManagerAsRecipientType = new AddedRecipientType?(((AddManagerAsRecipientTypeAction)action).RecipientType);
				return;
			}
			if (action is ModerateMessageByUserAction)
			{
				this.moderateMessageByUser = Utils.BuildRecipientIdArray(((ModerateMessageByUserAction)action).Addresses);
				return;
			}
			if (action is ModerateMessageByManagerAction)
			{
				this.moderateMessageByManager = true;
				return;
			}
			if (action is RedirectMessageAction)
			{
				this.redirectMessageTo = Utils.BuildRecipientIdArray(((RedirectMessageAction)action).Addresses);
				return;
			}
			if (action is RejectMessageAction)
			{
				this.rejectMessageReasonText = new DsnText?(((RejectMessageAction)action).RejectReason);
				this.rejectMessageEnhancedStatusCode = new RejectEnhancedStatus?(((RejectMessageAction)action).EnhancedStatusCode);
				return;
			}
			if (action is DeleteMessageAction)
			{
				this.deleteMessage = true;
				return;
			}
			if (action is DisconnectAction)
			{
				this.disconnect = true;
				return;
			}
			if (action is QuarantineAction)
			{
				this.quarantine = true;
				return;
			}
			if (action is LogEventAction)
			{
				this.logEventText = new EventLogText?(((LogEventAction)action).EventMessage);
				return;
			}
			if (action is SmtpRejectMessageAction)
			{
				this.smtpRejectMessageRejectStatusCode = new RejectStatusCode?(((SmtpRejectMessageAction)action).StatusCode);
				this.smtpRejectMessageRejectText = new RejectText?(((SmtpRejectMessageAction)action).RejectReason);
				return;
			}
			if (action is StopRuleProcessingAction)
			{
				this.stopRuleProcessing = true;
				return;
			}
			if (action is NotifySenderAction)
			{
				this.senderNotificationType = new NotifySenderType?(((NotifySenderAction)action).SenderNotificationType);
				this.rejectMessageReasonText = new DsnText?(((NotifySenderAction)action).RejectReason);
				this.rejectMessageEnhancedStatusCode = new RejectEnhancedStatus?(((NotifySenderAction)action).EnhancedStatusCode);
				return;
			}
			if (action is GenerateIncidentReportAction)
			{
				this.GenerateIncidentReport = new RecipientIdParameter(((GenerateIncidentReportAction)action).ReportDestination.ToString());
				this.IncidentReportOriginalMail = new IncidentReportOriginalMail?(((GenerateIncidentReportAction)action).IncidentReportOriginalMail);
				this.IncidentReportContent = ((GenerateIncidentReportAction)action).IncidentReportContent;
				return;
			}
			if (action is RouteMessageOutboundConnectorAction)
			{
				this.RouteMessageOutboundConnector = ((RouteMessageOutboundConnectorAction)action).ConnectorName;
				return;
			}
			if (action is RouteMessageOutboundRequireTlsAction)
			{
				this.RouteMessageOutboundRequireTls = true;
				return;
			}
			if (action is ApplyOMEAction)
			{
				this.ApplyOME = true;
				return;
			}
			if (action is RemoveOMEAction)
			{
				this.RemoveOME = true;
				return;
			}
			if (action is GenerateNotificationAction)
			{
				this.GenerateNotification = new DisclaimerText?(((GenerateNotificationAction)action).NotificationContent);
			}
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x001BC424 File Offset: 0x001BA624
		internal string ToCmdlet()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("New-TransportRule ");
			stringBuilder.Append(string.Format("-{0} {1} ", "Name", Utils.QuoteCmdletParameter(base.Name)));
			if (Guid.Empty != this.DlpPolicyId)
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "DlpPolicy", this.DlpPolicyId));
			}
			if (!string.IsNullOrEmpty(this.Comments))
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "Comments", Utils.QuoteCmdletParameter(this.Comments)));
			}
			if (this.ActivationDate != null)
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "ActivationDate", Utils.QuoteCmdletParameter(this.ActivationDate.Value.ToString())));
			}
			if (this.ExpiryDate != null)
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "ExpiryDate", Utils.QuoteCmdletParameter(this.ExpiryDate.Value.ToString())));
			}
			if (this.State != RuleState.Enabled)
			{
				stringBuilder.Append(string.Format("-{0} $False ", "Enabled"));
			}
			stringBuilder.Append(string.Format("-{0} {1} ", "Mode", Enum.GetName(typeof(RuleMode), this.Mode)));
			if (this.RuleSubType != RuleSubType.None)
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "RuleSubType", Enum.GetName(typeof(RuleSubType), this.RuleSubType)));
			}
			if (this.RuleErrorAction != RuleErrorAction.Ignore)
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "RuleErrorAction", Enum.GetName(typeof(RuleErrorAction), this.RuleErrorAction)));
			}
			if (this.SenderAddressLocation != SenderAddressLocation.Header)
			{
				stringBuilder.Append(string.Format("-{0} {1} ", "SenderAddressLocation", Enum.GetName(typeof(SenderAddressLocation), this.SenderAddressLocation)));
			}
			if (this.Conditions != null)
			{
				foreach (TransportRulePredicate transportRulePredicate in this.Conditions)
				{
					stringBuilder.Append(transportRulePredicate.ToCmdletParameter(false));
					stringBuilder.Append(' ');
				}
			}
			if (this.Exceptions != null)
			{
				foreach (TransportRulePredicate transportRulePredicate2 in this.Exceptions)
				{
					stringBuilder.Append(transportRulePredicate2.ToCmdletParameter(true));
					stringBuilder.Append(' ');
				}
			}
			if (this.Actions != null)
			{
				foreach (TransportRuleAction transportRuleAction in this.Actions)
				{
					stringBuilder.Append(transportRuleAction.ToCmdletParameter());
					stringBuilder.Append(' ');
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x001BC71C File Offset: 0x001BA91C
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
			if (this.actions != null)
			{
				foreach (TransportRuleAction transportRuleAction in this.actions)
				{
					transportRuleAction.SuppressPiiData();
				}
			}
			this.dlpPolicy = SuppressingPiiProperty.TryRedactValue<string>(RuleSchema.DlpPolicy, this.dlpPolicy);
			this.comments = SuppressingPiiProperty.TryRedactValue<string>(RuleSchema.Comments, this.comments);
			this.from = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.From, this.from);
			this.fromMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.FromMemberOf, this.fromMemberOf);
			this.sentTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.SentTo, this.sentTo);
			this.sentToMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.SentToMemberOf, this.sentToMemberOf);
			this.betweenMemberOf1 = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.BetweenMemberOf1, this.betweenMemberOf1);
			this.betweenMemberOf2 = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.BetweenMemberOf2, this.betweenMemberOf2);
			this.managerAddresses = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ManagerAddresses, this.managerAddresses);
			this.senderADAttributeContainsWords = Utils.RedactNameValuePairWords(this.senderADAttributeContainsWords);
			this.senderADAttributeMatchesPatterns = Utils.RedactNameValuePairPatterns(this.senderADAttributeMatchesPatterns);
			this.recipientADAttributeContainsWords = Utils.RedactNameValuePairWords(this.recipientADAttributeContainsWords);
			this.recipientADAttributeMatchesPatterns = Utils.RedactNameValuePairPatterns(this.recipientADAttributeMatchesPatterns);
			this.anyOfToHeader = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AnyOfToHeader, this.anyOfToHeader);
			this.anyOfToHeaderMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AnyOfToHeaderMemberOf, this.anyOfToHeaderMemberOf);
			this.anyOfCcHeader = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AnyOfCcHeader, this.anyOfCcHeader);
			this.anyOfCcHeaderMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AnyOfCcHeaderMemberOf, this.anyOfCcHeaderMemberOf);
			this.anyOfToCcHeader = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AnyOfToCcHeader, this.anyOfToCcHeader);
			this.anyOfToCcHeaderMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AnyOfToCcHeaderMemberOf, this.anyOfToCcHeaderMemberOf);
			this.subjectContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.SubjectContainsWords, this.subjectContainsWords);
			this.subjectOrBodyContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.SubjectOrBodyContainsWords, this.subjectOrBodyContainsWords);
			this.headerContainsMessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName?>(RuleSchema.HeaderContainsMessageHeader, this.headerContainsMessageHeader);
			this.headerContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.HeaderContainsWords, this.headerContainsWords);
			this.fromAddressContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.FromAddressContainsWords, this.fromAddressContainsWords);
			this.senderDomainIs = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.SenderDomainIs, this.senderDomainIs);
			this.recipientDomainIs = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.RecipientDomainIs, this.recipientDomainIs);
			this.subjectMatchesPatterns = Utils.RedactPatterns(this.subjectMatchesPatterns);
			this.subjectOrBodyMatchesPatterns = Utils.RedactPatterns(this.subjectOrBodyMatchesPatterns);
			this.headerMatchesMessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName?>(RuleSchema.HeaderMatchesMessageHeader, this.headerMatchesMessageHeader);
			this.headerMatchesPatterns = Utils.RedactPatterns(this.headerMatchesPatterns);
			this.fromAddressMatchesPatterns = Utils.RedactPatterns(this.fromAddressMatchesPatterns);
			this.attachmentNameMatchesPatterns = Utils.RedactPatterns(this.attachmentNameMatchesPatterns);
			this.attachmentExtensionMatchesWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.AttachmentExtensionMatchesWords, this.attachmentExtensionMatchesWords);
			this.attachmentPropertyContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.AttachmentPropertyContainsWords, this.AttachmentPropertyContainsWords);
			this.contentCharacterSetContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ContentCharacterSetContainsWords, this.contentCharacterSetContainsWords);
			this.recipientAddressContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.RecipientAddressContainsWords, this.recipientAddressContainsWords);
			this.recipientAddressMatchesPatterns = Utils.RedactPatterns(this.recipientAddressMatchesPatterns);
			this.senderInRecipientList = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.SenderInRecipientList, this.senderInRecipientList);
			this.recipientInSenderList = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.RecipientInSenderList, this.recipientInSenderList);
			this.attachmentContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.AttachmentContainsWords, this.attachmentContainsWords);
			this.attachmentMatchesPatterns = Utils.RedactPatterns(this.attachmentMatchesPatterns);
			this.anyOfRecipientAddressContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.AnyOfRecipientAddressContainsWords, this.anyOfRecipientAddressContainsWords);
			this.anyOfRecipientAddressMatchesPatterns = Utils.RedactPatterns(this.anyOfRecipientAddressMatchesPatterns);
			this.exceptIfFrom = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfFrom, this.exceptIfFrom);
			this.exceptIfFromMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfFromMemberOf, this.exceptIfFromMemberOf);
			this.exceptIfSentTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfSentTo, this.exceptIfSentTo);
			this.exceptIfSentToMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfSentToMemberOf, this.exceptIfSentToMemberOf);
			this.exceptIfBetweenMemberOf1 = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfBetweenMemberOf1, this.exceptIfBetweenMemberOf1);
			this.exceptIfBetweenMemberOf2 = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfBetweenMemberOf2, this.exceptIfBetweenMemberOf2);
			this.exceptIfManagerAddresses = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfManagerAddresses, this.exceptIfManagerAddresses);
			this.exceptIfSenderADAttributeContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfSenderADAttributeContainsWords, this.exceptIfSenderADAttributeContainsWords);
			this.exceptIfSenderADAttributeMatchesPatterns = Utils.RedactPatterns(this.exceptIfSenderADAttributeMatchesPatterns);
			this.exceptIfRecipientADAttributeContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfRecipientADAttributeContainsWords, this.exceptIfRecipientADAttributeContainsWords);
			this.exceptIfRecipientADAttributeMatchesPatterns = Utils.RedactPatterns(this.exceptIfRecipientADAttributeMatchesPatterns);
			this.exceptIfAnyOfToHeader = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfAnyOfToHeader, this.exceptIfAnyOfToHeader);
			this.exceptIfAnyOfToHeaderMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfAnyOfToHeaderMemberOf, this.exceptIfAnyOfToHeaderMemberOf);
			this.exceptIfAnyOfCcHeader = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfAnyOfCcHeader, this.exceptIfAnyOfCcHeader);
			this.exceptIfAnyOfCcHeaderMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfAnyOfCcHeaderMemberOf, this.exceptIfAnyOfCcHeaderMemberOf);
			this.exceptIfAnyOfToCcHeader = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfAnyOfToCcHeader, this.exceptIfAnyOfToCcHeader);
			this.exceptIfAnyOfToCcHeaderMemberOf = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ExceptIfAnyOfToCcHeaderMemberOf, this.exceptIfAnyOfToCcHeaderMemberOf);
			this.exceptIfSubjectContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfSubjectContainsWords, this.exceptIfSubjectContainsWords);
			this.exceptIfSubjectOrBodyContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfSubjectOrBodyContainsWords, this.exceptIfSubjectOrBodyContainsWords);
			this.exceptIfHeaderContainsMessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName?>(RuleSchema.ExceptIfHeaderContainsMessageHeader, this.exceptIfHeaderContainsMessageHeader);
			this.exceptIfHeaderContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfHeaderContainsWords, this.exceptIfHeaderContainsWords);
			this.exceptIfFromAddressContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfFromAddressContainsWords, this.exceptIfFromAddressContainsWords);
			this.exceptIfSenderDomainIs = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfSenderDomainIs, this.exceptIfSenderDomainIs);
			this.exceptIfRecipientDomainIs = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfRecipientDomainIs, this.exceptIfRecipientDomainIs);
			this.exceptIfSubjectMatchesPatterns = Utils.RedactPatterns(this.exceptIfSubjectMatchesPatterns);
			this.exceptIfSubjectOrBodyMatchesPatterns = Utils.RedactPatterns(this.exceptIfSubjectOrBodyMatchesPatterns);
			this.exceptIfHeaderMatchesMessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName?>(RuleSchema.ExceptIfHeaderMatchesMessageHeader, this.exceptIfHeaderMatchesMessageHeader);
			this.exceptIfHeaderMatchesPatterns = Utils.RedactPatterns(this.exceptIfHeaderMatchesPatterns);
			this.exceptIfFromAddressMatchesPatterns = Utils.RedactPatterns(this.exceptIfFromAddressMatchesPatterns);
			this.exceptIfAttachmentNameMatchesPatterns = Utils.RedactPatterns(this.exceptIfAttachmentNameMatchesPatterns);
			this.exceptIfAttachmentExtensionMatchesWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfAttachmentExtensionMatchesWords, this.exceptIfAttachmentExtensionMatchesWords);
			this.exceptIfAttachmentPropertyContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfAttachmentPropertyContainsWords, this.exceptIfAttachmentPropertyContainsWords);
			this.exceptIfContentCharacterSetContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfContentCharacterSetContainsWords, this.exceptIfContentCharacterSetContainsWords);
			this.exceptIfRecipientAddressContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfRecipientAddressContainsWords, this.exceptIfRecipientAddressContainsWords);
			this.exceptIfRecipientAddressMatchesPatterns = Utils.RedactPatterns(this.exceptIfRecipientAddressMatchesPatterns);
			this.exceptIfSenderInRecipientList = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfSenderInRecipientList, this.exceptIfSenderInRecipientList);
			this.exceptIfRecipientInSenderList = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfRecipientInSenderList, this.exceptIfRecipientInSenderList);
			this.exceptIfAttachmentContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfAttachmentContainsWords, this.exceptIfAttachmentContainsWords);
			this.exceptIfAttachmentMatchesPatterns = Utils.RedactPatterns(this.exceptIfAttachmentMatchesPatterns);
			this.exceptIfAnyOfRecipientAddressContainsWords = SuppressingPiiProperty.TryRedactValue<Word[]>(RuleSchema.ExceptIfAnyOfRecipientAddressContainsWords, this.exceptIfAnyOfRecipientAddressContainsWords);
			this.exceptIfAnyOfRecipientAddressMatchesPatterns = Utils.RedactPatterns(this.exceptIfAnyOfRecipientAddressMatchesPatterns);
			this.exceptIfMessageContainsDataClassifications = SuppressingPiiProperty.TryRedactValue<string[]>(RuleSchema.ExceptIfMessageContainsDataClassifications, this.exceptIfMessageContainsDataClassifications);
			this.prependSubject = SuppressingPiiProperty.TryRedactValue<string>(RuleSchema.PrependSubject, this.prependSubject);
			this.applyHtmlDisclaimerText = SuppressingPiiProperty.TryRedactValue<DisclaimerText?>(RuleSchema.ApplyHtmlDisclaimerText, this.applyHtmlDisclaimerText);
			this.applyRightsProtectionTemplate = SuppressingPiiProperty.TryRedactValue<RmsTemplateIdentity>(RuleSchema.ApplyRightsProtectionTemplate, this.applyRightsProtectionTemplate);
			this.setHeaderName = SuppressingPiiProperty.TryRedactValue<HeaderName?>(RuleSchema.SetHeaderName, this.setHeaderName);
			this.setHeaderValue = SuppressingPiiProperty.TryRedactValue<HeaderValue?>(RuleSchema.SetHeaderValue, this.setHeaderValue);
			this.removeHeader = SuppressingPiiProperty.TryRedactValue<HeaderName?>(RuleSchema.RemoveHeader, this.removeHeader);
			this.addToRecipients = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.AddToRecipients, this.addToRecipients);
			this.copyTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.CopyTo, this.copyTo);
			this.blindCopyTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.BlindCopyTo, this.blindCopyTo);
			this.moderateMessageByUser = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.ModerateMessageByUser, this.moderateMessageByUser);
			this.redirectMessageTo = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter[]>(RuleSchema.RedirectMessageTo, this.redirectMessageTo);
			this.rejectMessageReasonText = SuppressingPiiProperty.TryRedactValue<DsnText?>(RuleSchema.RejectMessageReasonText, this.rejectMessageReasonText);
			this.smtpRejectMessageRejectText = SuppressingPiiProperty.TryRedactValue<RejectText?>(RuleSchema.SmtpRejectMessageRejectText, this.smtpRejectMessageRejectText);
			this.logEventText = SuppressingPiiProperty.TryRedactValue<EventLogText?>(RuleSchema.LogEventText, this.logEventText);
			this.generateIncidentReport = SuppressingPiiProperty.TryRedactValue<RecipientIdParameter>(RuleSchema.GenerateIncidentReport, this.generateIncidentReport);
			this.connectorName = SuppressingPiiProperty.TryRedactValue<string>(RuleSchema.RouteMessageOutboundConnector, this.connectorName);
			this.generateNotification = SuppressingPiiProperty.TryRedactValue<DisclaimerText?>(RuleSchema.GenerateNotification, this.generateNotification);
		}

		// Token: 0x040037B5 RID: 14261
		public const int MaxCommentLength = 1024;

		// Token: 0x040037B6 RID: 14262
		public const int MaxDlpPolicyLength = 64;

		// Token: 0x040037B7 RID: 14263
		private static readonly RuleSchema schema = ObjectSchema.GetInstance<RuleSchema>();

		// Token: 0x040037B8 RID: 14264
		private int priority;

		// Token: 0x040037B9 RID: 14265
		private string dlpPolicy;

		// Token: 0x040037BA RID: 14266
		private Guid dlpPolicyId;

		// Token: 0x040037BB RID: 14267
		private string comments;

		// Token: 0x040037BC RID: 14268
		private readonly bool manuallyModified;

		// Token: 0x040037BD RID: 14269
		private TransportRulePredicate[] conditions;

		// Token: 0x040037BE RID: 14270
		private TransportRulePredicate[] exceptions;

		// Token: 0x040037BF RID: 14271
		private TransportRuleAction[] actions;

		// Token: 0x040037C0 RID: 14272
		private LocalizedString errorText = LocalizedString.Empty;

		// Token: 0x040037C1 RID: 14273
		private RecipientIdParameter[] from;

		// Token: 0x040037C2 RID: 14274
		private RecipientIdParameter[] fromMemberOf;

		// Token: 0x040037C3 RID: 14275
		private FromUserScope? fromScope;

		// Token: 0x040037C4 RID: 14276
		private RecipientIdParameter[] sentTo;

		// Token: 0x040037C5 RID: 14277
		private RecipientIdParameter[] sentToMemberOf;

		// Token: 0x040037C6 RID: 14278
		private ToUserScope? sentToScope;

		// Token: 0x040037C7 RID: 14279
		private RecipientIdParameter[] betweenMemberOf1;

		// Token: 0x040037C8 RID: 14280
		private RecipientIdParameter[] betweenMemberOf2;

		// Token: 0x040037C9 RID: 14281
		private RecipientIdParameter[] managerAddresses;

		// Token: 0x040037CA RID: 14282
		private EvaluatedUser? managerForEvaluatedUser;

		// Token: 0x040037CB RID: 14283
		private ManagementRelationship? senderManagementRelationship;

		// Token: 0x040037CC RID: 14284
		private ADAttribute? adComparisonAttribute;

		// Token: 0x040037CD RID: 14285
		private Evaluation? adComparisonOperator;

		// Token: 0x040037CE RID: 14286
		private Word[] senderADAttributeContainsWords;

		// Token: 0x040037CF RID: 14287
		private Pattern[] senderADAttributeMatchesPatterns;

		// Token: 0x040037D0 RID: 14288
		private Word[] recipientADAttributeContainsWords;

		// Token: 0x040037D1 RID: 14289
		private Pattern[] recipientADAttributeMatchesPatterns;

		// Token: 0x040037D2 RID: 14290
		private RecipientIdParameter[] anyOfToHeader;

		// Token: 0x040037D3 RID: 14291
		private RecipientIdParameter[] anyOfToHeaderMemberOf;

		// Token: 0x040037D4 RID: 14292
		private RecipientIdParameter[] anyOfCcHeader;

		// Token: 0x040037D5 RID: 14293
		private RecipientIdParameter[] anyOfCcHeaderMemberOf;

		// Token: 0x040037D6 RID: 14294
		private RecipientIdParameter[] anyOfToCcHeader;

		// Token: 0x040037D7 RID: 14295
		private RecipientIdParameter[] anyOfToCcHeaderMemberOf;

		// Token: 0x040037D8 RID: 14296
		private ADObjectId hasClassification;

		// Token: 0x040037D9 RID: 14297
		private bool hasNoClassification;

		// Token: 0x040037DA RID: 14298
		private Word[] subjectContainsWords;

		// Token: 0x040037DB RID: 14299
		private Word[] subjectOrBodyContainsWords;

		// Token: 0x040037DC RID: 14300
		private HeaderName? headerContainsMessageHeader;

		// Token: 0x040037DD RID: 14301
		private Word[] headerContainsWords;

		// Token: 0x040037DE RID: 14302
		private Word[] fromAddressContainsWords;

		// Token: 0x040037DF RID: 14303
		private Word[] senderDomainIs;

		// Token: 0x040037E0 RID: 14304
		private Word[] recipientDomainIs;

		// Token: 0x040037E1 RID: 14305
		private Pattern[] subjectMatchesPatterns;

		// Token: 0x040037E2 RID: 14306
		private Pattern[] subjectOrBodyMatchesPatterns;

		// Token: 0x040037E3 RID: 14307
		private HeaderName? headerMatchesMessageHeader;

		// Token: 0x040037E4 RID: 14308
		private Pattern[] headerMatchesPatterns;

		// Token: 0x040037E5 RID: 14309
		private Pattern[] fromAddressMatchesPatterns;

		// Token: 0x040037E6 RID: 14310
		private Pattern[] attachmentNameMatchesPatterns;

		// Token: 0x040037E7 RID: 14311
		private Word[] attachmentExtensionMatchesWords;

		// Token: 0x040037E8 RID: 14312
		private Word[] attachmentPropertyContainsWords;

		// Token: 0x040037E9 RID: 14313
		private Word[] contentCharacterSetContainsWords;

		// Token: 0x040037EA RID: 14314
		private bool hasSenderOverride;

		// Token: 0x040037EB RID: 14315
		private string[] messageContainsDataClassifications;

		// Token: 0x040037EC RID: 14316
		private MultiValuedProperty<IPRange> senderIpRanges;

		// Token: 0x040037ED RID: 14317
		private SclValue? sclOver;

		// Token: 0x040037EE RID: 14318
		private ByteQuantifiedSize? attachmentSizeOver;

		// Token: 0x040037EF RID: 14319
		private ByteQuantifiedSize? messageSizeOver;

		// Token: 0x040037F0 RID: 14320
		private Importance? withImportance;

		// Token: 0x040037F1 RID: 14321
		private MessageType? messageTypeMatches;

		// Token: 0x040037F2 RID: 14322
		private Word[] recipientAddressContainsWords;

		// Token: 0x040037F3 RID: 14323
		private Pattern[] recipientAddressMatchesPatterns;

		// Token: 0x040037F4 RID: 14324
		private Word[] senderInRecipientList;

		// Token: 0x040037F5 RID: 14325
		private Word[] recipientInSenderList;

		// Token: 0x040037F6 RID: 14326
		private Word[] attachmentContainsWords;

		// Token: 0x040037F7 RID: 14327
		private Pattern[] attachmentMatchesPatterns;

		// Token: 0x040037F8 RID: 14328
		private bool attachmentIsUnsupported;

		// Token: 0x040037F9 RID: 14329
		private bool attachmentProcessingLimitExceeded;

		// Token: 0x040037FA RID: 14330
		private bool attachmentHasExecutableContent;

		// Token: 0x040037FB RID: 14331
		private bool attachmentIsPasswordProtected;

		// Token: 0x040037FC RID: 14332
		private Word[] anyOfRecipientAddressContainsWords;

		// Token: 0x040037FD RID: 14333
		private Pattern[] anyOfRecipientAddressMatchesPatterns;

		// Token: 0x040037FE RID: 14334
		private RecipientIdParameter[] exceptIfFrom;

		// Token: 0x040037FF RID: 14335
		private RecipientIdParameter[] exceptIfFromMemberOf;

		// Token: 0x04003800 RID: 14336
		private FromUserScope? exceptIfFromScope;

		// Token: 0x04003801 RID: 14337
		private RecipientIdParameter[] exceptIfSentTo;

		// Token: 0x04003802 RID: 14338
		private RecipientIdParameter[] exceptIfSentToMemberOf;

		// Token: 0x04003803 RID: 14339
		private ToUserScope? exceptIfSentToScope;

		// Token: 0x04003804 RID: 14340
		private RecipientIdParameter[] exceptIfBetweenMemberOf1;

		// Token: 0x04003805 RID: 14341
		private RecipientIdParameter[] exceptIfBetweenMemberOf2;

		// Token: 0x04003806 RID: 14342
		private RecipientIdParameter[] exceptIfManagerAddresses;

		// Token: 0x04003807 RID: 14343
		private EvaluatedUser? exceptIfManagerForEvaluatedUser;

		// Token: 0x04003808 RID: 14344
		private ManagementRelationship? exceptIfSenderManagementRelationship;

		// Token: 0x04003809 RID: 14345
		private ADAttribute? exceptIfADComparisonAttribute;

		// Token: 0x0400380A RID: 14346
		private Evaluation? exceptIfADComparisonOperator;

		// Token: 0x0400380B RID: 14347
		private Word[] exceptIfSenderADAttributeContainsWords;

		// Token: 0x0400380C RID: 14348
		private Pattern[] exceptIfSenderADAttributeMatchesPatterns;

		// Token: 0x0400380D RID: 14349
		private Word[] exceptIfRecipientADAttributeContainsWords;

		// Token: 0x0400380E RID: 14350
		private Pattern[] exceptIfRecipientADAttributeMatchesPatterns;

		// Token: 0x0400380F RID: 14351
		private RecipientIdParameter[] exceptIfAnyOfToHeader;

		// Token: 0x04003810 RID: 14352
		private RecipientIdParameter[] exceptIfAnyOfToHeaderMemberOf;

		// Token: 0x04003811 RID: 14353
		private RecipientIdParameter[] exceptIfAnyOfCcHeader;

		// Token: 0x04003812 RID: 14354
		private RecipientIdParameter[] exceptIfAnyOfCcHeaderMemberOf;

		// Token: 0x04003813 RID: 14355
		private RecipientIdParameter[] exceptIfAnyOfToCcHeader;

		// Token: 0x04003814 RID: 14356
		private RecipientIdParameter[] exceptIfAnyOfToCcHeaderMemberOf;

		// Token: 0x04003815 RID: 14357
		private ADObjectId exceptIfHasClassification;

		// Token: 0x04003816 RID: 14358
		private bool exceptIfHasNoClassification;

		// Token: 0x04003817 RID: 14359
		private Word[] exceptIfSubjectContainsWords;

		// Token: 0x04003818 RID: 14360
		private Word[] exceptIfSubjectOrBodyContainsWords;

		// Token: 0x04003819 RID: 14361
		private HeaderName? exceptIfHeaderContainsMessageHeader;

		// Token: 0x0400381A RID: 14362
		private Word[] exceptIfHeaderContainsWords;

		// Token: 0x0400381B RID: 14363
		private Word[] exceptIfFromAddressContainsWords;

		// Token: 0x0400381C RID: 14364
		private Word[] exceptIfSenderDomainIs;

		// Token: 0x0400381D RID: 14365
		private Word[] exceptIfRecipientDomainIs;

		// Token: 0x0400381E RID: 14366
		private Pattern[] exceptIfSubjectMatchesPatterns;

		// Token: 0x0400381F RID: 14367
		private Pattern[] exceptIfSubjectOrBodyMatchesPatterns;

		// Token: 0x04003820 RID: 14368
		private HeaderName? exceptIfHeaderMatchesMessageHeader;

		// Token: 0x04003821 RID: 14369
		private Pattern[] exceptIfHeaderMatchesPatterns;

		// Token: 0x04003822 RID: 14370
		private Pattern[] exceptIfFromAddressMatchesPatterns;

		// Token: 0x04003823 RID: 14371
		private Pattern[] exceptIfAttachmentNameMatchesPatterns;

		// Token: 0x04003824 RID: 14372
		private Word[] exceptIfAttachmentExtensionMatchesWords;

		// Token: 0x04003825 RID: 14373
		private Word[] exceptIfAttachmentPropertyContainsWords;

		// Token: 0x04003826 RID: 14374
		private Word[] exceptIfContentCharacterSetContainsWords;

		// Token: 0x04003827 RID: 14375
		private bool exceptIfHasSenderOverride;

		// Token: 0x04003828 RID: 14376
		private string[] exceptIfMessageContainsDataClassifications;

		// Token: 0x04003829 RID: 14377
		private MultiValuedProperty<IPRange> exceptIfSenderIpanges;

		// Token: 0x0400382A RID: 14378
		private SclValue? exceptIfSCLOver;

		// Token: 0x0400382B RID: 14379
		private ByteQuantifiedSize? exceptIfAttachmentSizeOver;

		// Token: 0x0400382C RID: 14380
		private ByteQuantifiedSize? exceptIfMessageSizeOver;

		// Token: 0x0400382D RID: 14381
		private Importance? exceptIfWithImportance;

		// Token: 0x0400382E RID: 14382
		private MessageType? exceptIfMessageTypeMatches;

		// Token: 0x0400382F RID: 14383
		private Word[] exceptIfRecipientAddressContainsWords;

		// Token: 0x04003830 RID: 14384
		private Pattern[] exceptIfRecipientAddressMatchesPatterns;

		// Token: 0x04003831 RID: 14385
		private Word[] exceptIfSenderInRecipientList;

		// Token: 0x04003832 RID: 14386
		private Word[] exceptIfRecipientInSenderList;

		// Token: 0x04003833 RID: 14387
		private Word[] exceptIfAttachmentContainsWords;

		// Token: 0x04003834 RID: 14388
		private Pattern[] exceptIfAttachmentMatchesPatterns;

		// Token: 0x04003835 RID: 14389
		private bool exceptIfAttachmentIsUnsupported;

		// Token: 0x04003836 RID: 14390
		private bool exceptIfAttachmentProcessingLimitExceeded;

		// Token: 0x04003837 RID: 14391
		private bool exceptIfAttachmentHasExecutableContent;

		// Token: 0x04003838 RID: 14392
		private bool exceptIfAttachmentIsPasswordProtected;

		// Token: 0x04003839 RID: 14393
		private Word[] exceptIfAnyOfRecipientAddressContainsWords;

		// Token: 0x0400383A RID: 14394
		private Pattern[] exceptIfAnyOfRecipientAddressMatchesPatterns;

		// Token: 0x0400383B RID: 14395
		private string prependSubject;

		// Token: 0x0400383C RID: 14396
		private string setAuditSeverity;

		// Token: 0x0400383D RID: 14397
		private ADObjectId applyClassification;

		// Token: 0x0400383E RID: 14398
		private DisclaimerLocation? applyHtmlDisclaimerLocation;

		// Token: 0x0400383F RID: 14399
		private DisclaimerText? applyHtmlDisclaimerText;

		// Token: 0x04003840 RID: 14400
		private DisclaimerFallbackAction? applyHtmlDisclaimerFallbackAction;

		// Token: 0x04003841 RID: 14401
		private RmsTemplateIdentity applyRightsProtectionTemplate;

		// Token: 0x04003842 RID: 14402
		private SclValue? setSCL;

		// Token: 0x04003843 RID: 14403
		private HeaderName? setHeaderName;

		// Token: 0x04003844 RID: 14404
		private HeaderValue? setHeaderValue;

		// Token: 0x04003845 RID: 14405
		private HeaderName? removeHeader;

		// Token: 0x04003846 RID: 14406
		private RecipientIdParameter[] addToRecipients;

		// Token: 0x04003847 RID: 14407
		private RecipientIdParameter[] copyTo;

		// Token: 0x04003848 RID: 14408
		private RecipientIdParameter[] blindCopyTo;

		// Token: 0x04003849 RID: 14409
		private AddedRecipientType? addManagerAsRecipientType;

		// Token: 0x0400384A RID: 14410
		private RecipientIdParameter[] moderateMessageByUser;

		// Token: 0x0400384B RID: 14411
		private bool moderateMessageByManager;

		// Token: 0x0400384C RID: 14412
		private RecipientIdParameter[] redirectMessageTo;

		// Token: 0x0400384D RID: 14413
		private RejectEnhancedStatus? rejectMessageEnhancedStatusCode;

		// Token: 0x0400384E RID: 14414
		private DsnText? rejectMessageReasonText;

		// Token: 0x0400384F RID: 14415
		private bool deleteMessage;

		// Token: 0x04003850 RID: 14416
		private bool disconnect;

		// Token: 0x04003851 RID: 14417
		private bool quarantine;

		// Token: 0x04003852 RID: 14418
		private RejectText? smtpRejectMessageRejectText;

		// Token: 0x04003853 RID: 14419
		private RejectStatusCode? smtpRejectMessageRejectStatusCode;

		// Token: 0x04003854 RID: 14420
		private EventLogText? logEventText;

		// Token: 0x04003855 RID: 14421
		private DateTime? activationDate;

		// Token: 0x04003856 RID: 14422
		private DateTime? expiryDate;

		// Token: 0x04003857 RID: 14423
		private bool stopRuleProcessing;

		// Token: 0x04003858 RID: 14424
		private NotifySenderType? senderNotificationType;

		// Token: 0x04003859 RID: 14425
		private RecipientIdParameter generateIncidentReport;

		// Token: 0x0400385A RID: 14426
		private IncidentReportOriginalMail? incidentReportOriginalMail;

		// Token: 0x0400385B RID: 14427
		private IncidentReportContent[] incidentReportContent;

		// Token: 0x0400385C RID: 14428
		private DisclaimerText? generateNotification;

		// Token: 0x0400385D RID: 14429
		private string connectorName;
	}
}
