using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A51 RID: 2641
	public abstract class SetHygieneFilterRuleTaskBase<TPublicObject> : SetSystemConfigurationObjectTask<RuleIdParameter, TPublicObject, TransportRule> where TPublicObject : IConfigurable, new()
	{
		// Token: 0x06005E94 RID: 24212 RVA: 0x0018C254 File Offset: 0x0018A454
		protected SetHygieneFilterRuleTaskBase(string ruleCollectionName)
		{
			this.ruleCollectionName = ruleCollectionName;
			this.Priority = 0;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x0018C275 File Offset: 0x0018A475
		// (set) Token: 0x06005E96 RID: 24214 RVA: 0x0018C28C File Offset: 0x0018A48C
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x06005E97 RID: 24215 RVA: 0x0018C29F File Offset: 0x0018A49F
		// (set) Token: 0x06005E98 RID: 24216 RVA: 0x0018C2B6 File Offset: 0x0018A4B6
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)base.Fields["Priority"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Strings.NegativePriority);
				}
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x0018C2E2 File Offset: 0x0018A4E2
		// (set) Token: 0x06005E9A RID: 24218 RVA: 0x0018C2F9 File Offset: 0x0018A4F9
		[Parameter(Mandatory = false)]
		public string Comments
		{
			get
			{
				return (string)base.Fields["Comments"];
			}
			set
			{
				base.Fields["Comments"] = value;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x0018C30C File Offset: 0x0018A50C
		// (set) Token: 0x06005E9C RID: 24220 RVA: 0x0018C323 File Offset: 0x0018A523
		public TransportRulePredicate[] Conditions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Conditions"];
			}
			set
			{
				base.Fields["Conditions"] = value;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06005E9D RID: 24221 RVA: 0x0018C336 File Offset: 0x0018A536
		// (set) Token: 0x06005E9E RID: 24222 RVA: 0x0018C34D File Offset: 0x0018A54D
		public TransportRulePredicate[] Exceptions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Exceptions"];
			}
			set
			{
				base.Fields["Exceptions"] = value;
			}
		}

		// Token: 0x06005E9F RID: 24223
		internal abstract HygieneFilterRule CreateTaskRuleFromInternalRule(TransportRule internalRule, int priority);

		// Token: 0x06005EA0 RID: 24224
		internal abstract ADIdParameter GetPolicyIdentity();

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0018C360 File Offset: 0x0018A560
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn(this.ruleCollectionName);
			}
			this.DataObject = (TransportRule)this.ResolveDataObject();
			if (this.DataObject != null)
			{
				base.CurrentOrganizationId = this.DataObject.OrganizationId;
			}
			if (!this.DataObject.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && !((IDirectorySession)base.DataSession).SessionSettings.CurrentOrganizationId.Equals(this.DataObject.OrganizationId))
			{
				base.UnderscopeDataSession(this.DataObject.OrganizationId);
			}
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsChildOfRuleContainer(this.Identity, this.ruleCollectionName))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
			Exception exception;
			string target;
			if (!Utils.ValidateRecipientIdParameters(base.Fields, base.TenantGlobalCatalogSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), out exception, out target))
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, target);
			}
			try
			{
				Utils.BuildConditionsAndExceptionsFromParameters(base.Fields, base.TenantGlobalCatalogSession, base.DataSession, false, out this.conditionTypesToUpdate, out this.exceptionTypesToUpdate, out this.conditionsSetByParameters, out this.exceptionsSetByParameters);
			}
			catch (TransientException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, this.Name);
			}
			catch (DataValidationException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, this.Name);
			}
			catch (ArgumentException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, this.Name);
			}
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0018C524 File Offset: 0x0018A724
		protected override void InternalProcessRecord()
		{
			try
			{
				ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, base.DataSession);
				adruleStorageManager.LoadRuleCollection();
				if (base.Fields.IsModified("Priority"))
				{
					this.SetRuleWithPriorityChange(adruleStorageManager);
				}
				else
				{
					this.SetRuleWithoutPriorityChange(adruleStorageManager);
				}
			}
			catch (RuleCollectionNotInAdException)
			{
				base.WriteError(new ArgumentException(Strings.RuleNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, this.Identity);
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			catch (ArgumentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (InvalidPriorityException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x0018C5F4 File Offset: 0x0018A7F4
		private void SetRuleWithPriorityChange(ADRuleStorageManager storedRules)
		{
			TransportRule transportRule;
			int priority;
			storedRules.GetRule(this.DataObject.Identity, out transportRule, out priority);
			if (transportRule == null)
			{
				base.WriteError(new ArgumentException(Strings.RuleNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, this.Identity);
			}
			HygieneFilterRule hygieneFilterRule = this.CreateTaskRuleFromInternalRule(transportRule, priority);
			this.UpdateRuleFromParameters(hygieneFilterRule);
			this.ValidateRuleEsnCompatibility(hygieneFilterRule);
			transportRule = hygieneFilterRule.ToInternalRule();
			try
			{
				storedRules.UpdateRule(transportRule, hygieneFilterRule.Identity, hygieneFilterRule.Priority);
			}
			catch (RulesValidationException)
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExist, "Name"), ErrorCategory.InvalidArgument, this.Name);
			}
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0018C6AC File Offset: 0x0018A8AC
		private void SetRuleWithoutPriorityChange(ADRuleStorageManager storedRules)
		{
			TransportRule transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(this.DataObject.Xml);
			HygieneFilterRule hygieneFilterRule = this.CreateTaskRuleFromInternalRule(transportRule, -1);
			this.UpdateRuleFromParameters(hygieneFilterRule);
			this.ValidateRuleEsnCompatibility(hygieneFilterRule);
			transportRule = hygieneFilterRule.ToInternalRule();
			this.DataObject.Xml = TransportRuleSerializer.Instance.SaveRuleToString(transportRule);
			if (base.Fields.IsModified("Name") && !storedRules.CanRename((ADObjectId)this.DataObject.Identity, ((ADObjectId)this.DataObject.Identity).Name, transportRule.Name))
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExist, "Name"), ErrorCategory.InvalidArgument, this.Name);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x0018C774 File Offset: 0x0018A974
		private void UpdateRuleFromParameters(HygieneFilterRule rule)
		{
			if (this.Name != null)
			{
				rule.Name = this.Name;
			}
			if (this.Comments != null)
			{
				rule.Comments = this.Comments;
			}
			if (base.Fields.IsModified("Priority"))
			{
				rule.Priority = this.Priority;
			}
			if (this.Conditions != null)
			{
				rule.Conditions = this.Conditions;
			}
			else
			{
				List<TransportRulePredicate> list = new List<TransportRulePredicate>();
				if (rule.Conditions != null)
				{
					foreach (TransportRulePredicate transportRulePredicate in rule.Conditions)
					{
						if (!this.conditionTypesToUpdate.Contains(transportRulePredicate.GetType()))
						{
							Utils.InsertPredicateSorted(transportRulePredicate, list);
						}
					}
				}
				foreach (TransportRulePredicate predicate in this.conditionsSetByParameters)
				{
					Utils.InsertPredicateSorted(predicate, list);
				}
				rule.Conditions = ((list.Count > 0) ? list.ToArray() : null);
				if (rule.Conditions == null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorCannotCreateRuleWithoutCondition), ErrorCategory.InvalidArgument, this.Name);
				}
			}
			if (this.Exceptions != null)
			{
				rule.Exceptions = this.Exceptions;
			}
			else
			{
				List<TransportRulePredicate> list2 = new List<TransportRulePredicate>();
				if (rule.Exceptions != null)
				{
					foreach (TransportRulePredicate transportRulePredicate2 in rule.Exceptions)
					{
						if (!this.exceptionTypesToUpdate.Contains(transportRulePredicate2.GetType()))
						{
							Utils.InsertPredicateSorted(transportRulePredicate2, list2);
						}
					}
				}
				foreach (TransportRulePredicate predicate2 in this.exceptionsSetByParameters)
				{
					Utils.InsertPredicateSorted(predicate2, list2);
				}
				if (list2.Count > 0)
				{
					rule.Exceptions = list2.ToArray();
				}
				else
				{
					rule.Exceptions = null;
				}
			}
			if (this.policyObject != null)
			{
				rule.SetPolicyId(this.GetPolicyIdentity());
			}
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x0018C950 File Offset: 0x0018AB50
		private void ValidateRuleEsnCompatibility(HygieneFilterRule rule)
		{
			if (rule is HostedContentFilterRule && ((HostedContentFilterPolicy)this.effectivePolicyObject).EnableEndUserSpamNotifications && !((HostedContentFilterRule)rule).IsEsnCompatible)
			{
				base.WriteError(new OperationNotAllowedException(Strings.ErrorCannotScopeEsnPolicy(this.effectivePolicyObject.Name)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x040034EB RID: 13547
		private TransportRulePredicate[] conditionsSetByParameters;

		// Token: 0x040034EC RID: 13548
		private TransportRulePredicate[] exceptionsSetByParameters;

		// Token: 0x040034ED RID: 13549
		private List<Type> conditionTypesToUpdate;

		// Token: 0x040034EE RID: 13550
		private List<Type> exceptionTypesToUpdate;

		// Token: 0x040034EF RID: 13551
		protected readonly string ruleCollectionName;

		// Token: 0x040034F0 RID: 13552
		protected ADConfigurationObject policyObject;

		// Token: 0x040034F1 RID: 13553
		protected ADConfigurationObject effectivePolicyObject;
	}
}
