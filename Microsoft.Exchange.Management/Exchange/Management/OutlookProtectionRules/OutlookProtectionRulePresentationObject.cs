using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AFD RID: 2813
	[Serializable]
	public sealed class OutlookProtectionRulePresentationObject : RulePresentationObjectBase
	{
		// Token: 0x060063FC RID: 25596 RVA: 0x001A1DC8 File Offset: 0x0019FFC8
		public OutlookProtectionRulePresentationObject(TransportRule transportRule) : base(transportRule)
		{
			if (transportRule == null || string.IsNullOrEmpty(transportRule.Xml))
			{
				return;
			}
			OutlookProtectionRule outlookProtectionRule = (OutlookProtectionRule)OutlookProtectionRuleParser.Instance.GetRule(transportRule.Xml);
			this.enabled = (outlookProtectionRule.Enabled == RuleState.Enabled);
			this.userCanOverride = outlookProtectionRule.UserOverridable;
			PredicateCondition senderDepartmentPredicate = outlookProtectionRule.GetSenderDepartmentPredicate();
			if (senderDepartmentPredicate != null && senderDepartmentPredicate.Value != null)
			{
				this.fromDepartment = new List<string>(senderDepartmentPredicate.Value.RawValues);
			}
			PredicateCondition recipientIsPredicate = outlookProtectionRule.GetRecipientIsPredicate();
			if (recipientIsPredicate != null && recipientIsPredicate.Value != null)
			{
				this.sentTo = new List<SmtpAddress>(from s in recipientIsPredicate.Value.RawValues
				select SmtpAddress.Parse(s));
			}
			PredicateCondition allInternalPredicate = outlookProtectionRule.GetAllInternalPredicate();
			if (allInternalPredicate != null)
			{
				this.sentToScope = ToUserScope.InOrganization;
			}
			RightsProtectMessageAction rightsProtectMessageAction = outlookProtectionRule.GetRightsProtectMessageAction();
			if (rightsProtectMessageAction != null)
			{
				this.applyRightsProtectionTemplate = new RmsTemplateIdentity(new Guid(rightsProtectMessageAction.TemplateId), rightsProtectMessageAction.TemplateName);
			}
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x001A1ECB File Offset: 0x001A00CB
		public OutlookProtectionRulePresentationObject() : this(null)
		{
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x060063FE RID: 25598 RVA: 0x001A1ED4 File Offset: 0x001A00D4
		// (set) Token: 0x060063FF RID: 25599 RVA: 0x001A1EDC File Offset: 0x001A00DC
		public RmsTemplateIdentity ApplyRightsProtectionTemplate
		{
			get
			{
				return this.applyRightsProtectionTemplate;
			}
			internal set
			{
				this.applyRightsProtectionTemplate = value;
			}
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x06006400 RID: 25600 RVA: 0x001A1EE5 File Offset: 0x001A00E5
		// (set) Token: 0x06006401 RID: 25601 RVA: 0x001A1EED File Offset: 0x001A00ED
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			internal set
			{
				this.enabled = value;
			}
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x06006402 RID: 25602 RVA: 0x001A1EF6 File Offset: 0x001A00F6
		// (set) Token: 0x06006403 RID: 25603 RVA: 0x001A1F0D File Offset: 0x001A010D
		public string[] FromDepartment
		{
			get
			{
				if (this.fromDepartment == null)
				{
					return null;
				}
				return this.fromDepartment.ToArray();
			}
			internal set
			{
				this.fromDepartment = ((value != null) ? new List<string>(value) : null);
			}
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x06006404 RID: 25604 RVA: 0x001A1F21 File Offset: 0x001A0121
		// (set) Token: 0x06006405 RID: 25605 RVA: 0x001A1F29 File Offset: 0x001A0129
		public int Priority
		{
			get
			{
				return this.priority;
			}
			internal set
			{
				this.priority = value;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x06006406 RID: 25606 RVA: 0x001A1F32 File Offset: 0x001A0132
		// (set) Token: 0x06006407 RID: 25607 RVA: 0x001A1F49 File Offset: 0x001A0149
		public SmtpAddress[] SentTo
		{
			get
			{
				if (this.sentTo == null)
				{
					return null;
				}
				return this.sentTo.ToArray();
			}
			internal set
			{
				this.sentTo = ((value != null) ? new List<SmtpAddress>(value) : null);
			}
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x06006408 RID: 25608 RVA: 0x001A1F5D File Offset: 0x001A015D
		// (set) Token: 0x06006409 RID: 25609 RVA: 0x001A1F65 File Offset: 0x001A0165
		public ToUserScope SentToScope
		{
			get
			{
				return this.sentToScope;
			}
			internal set
			{
				this.sentToScope = value;
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x0600640A RID: 25610 RVA: 0x001A1F6E File Offset: 0x001A016E
		// (set) Token: 0x0600640B RID: 25611 RVA: 0x001A1F76 File Offset: 0x001A0176
		public bool UserCanOverride
		{
			get
			{
				return this.userCanOverride;
			}
			internal set
			{
				this.userCanOverride = value;
			}
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x001A1F80 File Offset: 0x001A0180
		public override ValidationError[] Validate()
		{
			if (this.applyRightsProtectionTemplate == null)
			{
				return new ValidationError[]
				{
					new ObjectValidationError(Strings.OutlookProtectionRuleRmsTemplateNotSet, base.Identity, null)
				};
			}
			return OutlookProtectionRulePresentationObject.EmptyValidationErrorArray;
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x001A1FB7 File Offset: 0x001A01B7
		internal bool IsEmptyCondition()
		{
			return (this.fromDepartment == null || this.fromDepartment.Count == 0) && (this.sentTo == null || this.sentTo.Count == 0) && this.sentToScope == ToUserScope.All;
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x001A1FF0 File Offset: 0x001A01F0
		internal string Serialize()
		{
			ValidationError[] array = this.Validate();
			if (array != null && array.Length > 0)
			{
				throw new DataValidationException(array[0]);
			}
			OutlookProtectionRule outlookProtectionRule = new OutlookProtectionRule(base.Name);
			outlookProtectionRule.Enabled = (this.Enabled ? RuleState.Enabled : RuleState.Disabled);
			outlookProtectionRule.UserOverridable = this.UserCanOverride;
			outlookProtectionRule.Condition = new AndCondition
			{
				SubConditions = 
				{
					this.CreateAllInternalCondition(),
					new AndCondition
					{
						SubConditions = 
						{
							this.CreateSenderDepartmentCondition(),
							this.CreateRecipientIsCondition()
						}
					}
				}
			};
			outlookProtectionRule.Actions.Add(this.CreateRightsProtectMessageAction());
			OutlookProtectionRuleSerializer outlookProtectionRuleSerializer = new OutlookProtectionRuleSerializer();
			return outlookProtectionRuleSerializer.SaveRuleToString(outlookProtectionRule);
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x001A20B4 File Offset: 0x001A02B4
		private Condition CreateSenderDepartmentCondition()
		{
			if (this.fromDepartment == null || this.fromDepartment.Count == 0)
			{
				return Condition.True;
			}
			ShortList<string> shortList = new ShortList<string>();
			foreach (string item in this.fromDepartment)
			{
				shortList.Add(item);
			}
			return new IsPredicate(new StringProperty("Message.Sender.Department"), shortList, new RulesCreationContext());
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x001A2140 File Offset: 0x001A0340
		private Condition CreateRecipientIsCondition()
		{
			if (this.sentTo == null || this.sentTo.Count == 0)
			{
				return Condition.True;
			}
			ShortList<string> shortList = new ShortList<string>();
			foreach (SmtpAddress smtpAddress in this.sentTo)
			{
				shortList.Add(smtpAddress.ToString());
			}
			return new RecipientIsPredicate(shortList, new RulesCreationContext());
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x001A21CC File Offset: 0x001A03CC
		private Condition CreateAllInternalCondition()
		{
			if (this.sentToScope != ToUserScope.InOrganization)
			{
				return Condition.True;
			}
			return new AllInternalPredicate();
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x001A21E2 File Offset: 0x001A03E2
		private Microsoft.Exchange.MessagingPolicies.Rules.Action CreateRightsProtectMessageAction()
		{
			return RightsProtectMessageAction.Create(this.applyRightsProtectionTemplate.TemplateId, this.applyRightsProtectionTemplate.TemplateName);
		}

		// Token: 0x04003611 RID: 13841
		private static readonly ValidationError[] EmptyValidationErrorArray = new ValidationError[0];

		// Token: 0x04003612 RID: 13842
		private RmsTemplateIdentity applyRightsProtectionTemplate;

		// Token: 0x04003613 RID: 13843
		private bool enabled;

		// Token: 0x04003614 RID: 13844
		private List<string> fromDepartment;

		// Token: 0x04003615 RID: 13845
		private int priority;

		// Token: 0x04003616 RID: 13846
		private List<SmtpAddress> sentTo;

		// Token: 0x04003617 RID: 13847
		private ToUserScope sentToScope;

		// Token: 0x04003618 RID: 13848
		private bool userCanOverride;
	}
}
