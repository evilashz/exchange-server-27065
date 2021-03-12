using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
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

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x0200008C RID: 140
	[Cmdlet("Set", "SupervisionPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "ClosedCampusInbound")]
	public sealed class SetSupervisionPolicy : SetMultitenancySingletonSystemConfigurationObjectTask<SupervisionTransportRule>
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00012F19 File Offset: 0x00011119
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSupervisionPolicy;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00012F20 File Offset: 0x00011120
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00012F37 File Offset: 0x00011137
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public new OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00012F67 File Offset: 0x00011167
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00012F4A File Offset: 0x0001114A
		[Parameter(ParameterSetName = "ClosedCampusInbound")]
		public bool ClosedCampusInboundPolicyEnabled
		{
			get
			{
				return (bool)base.Fields[SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled.Name] = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012F9B File Offset: 0x0001119B
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00012F83 File Offset: 0x00011183
		[Parameter(ParameterSetName = "ClosedCampusInbound")]
		public MultiValuedProperty<SmtpDomain> ClosedCampusInboundDomainExceptions
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)base.Fields[SupervisionPolicySchema.ClosedCampusInboundDomainExceptions.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.ClosedCampusInboundDomainExceptions.Name] = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00012FCF File Offset: 0x000111CF
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00012FB7 File Offset: 0x000111B7
		[Parameter(ParameterSetName = "ClosedCampusInbound")]
		public MultiValuedProperty<RecipientIdParameter> ClosedCampusInboundGroupExceptions
		{
			get
			{
				return (MultiValuedProperty<RecipientIdParameter>)base.Fields[SupervisionPolicySchema.ClosedCampusInboundGroupExceptions.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.ClosedCampusInboundGroupExceptions.Name] = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00013008 File Offset: 0x00011208
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00012FEB File Offset: 0x000111EB
		[Parameter(ParameterSetName = "ClosedCampusOutbound")]
		public bool ClosedCampusOutboundPolicyEnabled
		{
			get
			{
				return (bool)base.Fields[SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled.Name] = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001303C File Offset: 0x0001123C
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00013024 File Offset: 0x00011224
		[Parameter(ParameterSetName = "ClosedCampusOutbound")]
		public MultiValuedProperty<SmtpDomain> ClosedCampusOutboundDomainExceptions
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)base.Fields[SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions.Name] = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00013070 File Offset: 0x00011270
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00013058 File Offset: 0x00011258
		[Parameter(ParameterSetName = "ClosedCampusOutbound")]
		public MultiValuedProperty<RecipientIdParameter> ClosedCampusOutboundGroupExceptions
		{
			get
			{
				return (MultiValuedProperty<RecipientIdParameter>)base.Fields[SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions.Name] = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000130A9 File Offset: 0x000112A9
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0001308C File Offset: 0x0001128C
		[Parameter(ParameterSetName = "BadWords")]
		public bool BadWordsPolicyEnabled
		{
			get
			{
				return (bool)base.Fields[SupervisionPolicySchema.BadWordsPolicyEnabled.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.BadWordsPolicyEnabled.Name] = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000130FC File Offset: 0x000112FC
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000130C8 File Offset: 0x000112C8
		[Parameter(ParameterSetName = "BadWords")]
		public string BadWordsList
		{
			get
			{
				return (string)base.Fields[SupervisionPolicySchema.BadWordsList.Name];
			}
			set
			{
				string value2 = value;
				if (!string.IsNullOrEmpty(value))
				{
					value2 = value.Trim();
				}
				base.Fields[SupervisionPolicySchema.BadWordsList.Name] = value2;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00013135 File Offset: 0x00011335
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00013118 File Offset: 0x00011318
		[Parameter(ParameterSetName = "AntiBullying")]
		public bool AntiBullyingPolicyEnabled
		{
			get
			{
				return (bool)base.Fields[SupervisionPolicySchema.AntiBullyingPolicyEnabled.Name];
			}
			set
			{
				base.Fields[SupervisionPolicySchema.AntiBullyingPolicyEnabled.Name] = value;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00013151 File Offset: 0x00011351
		protected override void InternalValidate()
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00013154 File Offset: 0x00011354
		protected override void InternalProcessRecord()
		{
			string ruleName = this.GetRuleName();
			if (ruleName == null)
			{
				this.WriteWarning(Strings.WarningForceMessage);
				return;
			}
			ADObjectId supervisionTransportRuleCollectionId = this.GetSupervisionTransportRuleCollectionId();
			this.DataObject = this.LoadRule(ruleName, supervisionTransportRuleCollectionId);
			this.UpdateDataObject();
			base.InternalProcessRecord();
			this.UpdateBadWordRules(supervisionTransportRuleCollectionId);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000131A0 File Offset: 0x000113A0
		private void UpdateBadWordRules(ADObjectId supervisionTransportRuleCollectionId)
		{
			if (!base.Fields.IsModified(SupervisionPolicySchema.BadWordsPolicyEnabled.Name))
			{
				return;
			}
			QueryFilter ruleFilter = new TextFilter(ADObjectSchema.Name, GetSupervisionPolicy.BadWordsRuleName, MatchOptions.Prefix, MatchFlags.Default);
			List<SupervisionTransportRule> supervisionRulesWithFilter = this.GetSupervisionRulesWithFilter(supervisionTransportRuleCollectionId, ruleFilter);
			bool flag = true;
			foreach (SupervisionTransportRule supervisionTransportRule in supervisionRulesWithFilter)
			{
				if (SetSupervisionPolicy.standardBadWordsRegex.Match(supervisionTransportRule.Name).Success)
				{
					this.UpdateRuleState(supervisionTransportRule);
					try
					{
						base.DataSession.Save(supervisionTransportRule);
					}
					catch (DataSourceTransientException ex)
					{
						flag = false;
						this.WriteWarning(Strings.ErrorWhileUpdatingBadWordsRules(supervisionTransportRule.Name, ex.Message));
					}
				}
			}
			if (!flag)
			{
				this.WriteWarning(Strings.ErrorChangingBadwordsTransportRule);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00013284 File Offset: 0x00011484
		private void UpdateRuleState(SupervisionTransportRule supervisionTransportRule)
		{
			Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule = SetSupervisionPolicy.ConvertFromRuleADObjectToPresentationObject(supervisionTransportRule);
			rule.State = (this.BadWordsPolicyEnabled ? RuleState.Enabled : RuleState.Disabled);
			SetSupervisionPolicy.UpdateRuleADObjectFromPresentationObject(supervisionTransportRule, rule);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000132B4 File Offset: 0x000114B4
		private string GetRuleName()
		{
			if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled.Name) || base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundDomainExceptions.Name) || base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundGroupExceptions.Name))
			{
				return GetSupervisionPolicy.ClosedCampusInboundRuleName;
			}
			if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled.Name) || base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions.Name) || base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions.Name))
			{
				return GetSupervisionPolicy.ClosedCampusOutboundRuleName;
			}
			if (base.Fields.IsModified(SupervisionPolicySchema.BadWordsPolicyEnabled.Name) || base.Fields.IsModified(SupervisionPolicySchema.BadWordsList.Name))
			{
				return GetSupervisionPolicy.BadWordsRuleName;
			}
			if (base.Fields.IsModified(SupervisionPolicySchema.AntiBullyingPolicyEnabled.Name))
			{
				return GetSupervisionPolicy.AntiBullyingRuleName;
			}
			return null;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000133AC File Offset: 0x000115AC
		private void UpdateDataObject()
		{
			Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule = SetSupervisionPolicy.ConvertFromRuleADObjectToPresentationObject(this.DataObject);
			this.UpdatePresentationObject(ref rule);
			SetSupervisionPolicy.UpdateRuleADObjectFromPresentationObject(this.DataObject, rule);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000133DC File Offset: 0x000115DC
		private static void UpdateRuleADObjectFromPresentationObject(SupervisionTransportRule adRuleObject, Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule)
		{
			TransportRule rule2 = rule.ToInternalRule();
			string xml = TransportRuleSerializer.Instance.SaveRuleToString(rule2);
			adRuleObject.Xml = xml;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013404 File Offset: 0x00011604
		private static Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule ConvertFromRuleADObjectToPresentationObject(SupervisionTransportRule adRuleObject)
		{
			TransportRule rule = (TransportRule)TransportRuleParser.Instance.GetRule(adRuleObject.Xml);
			return Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule.CreateFromInternalRule(TransportRulePredicate.BridgeheadMappings, TransportRuleAction.BridgeheadMappings, rule, -1, adRuleObject);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001343C File Offset: 0x0001163C
		private void UpdatePresentationObject(ref Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule)
		{
			try
			{
				this.UpdateRuleFromParameters(rule);
				this.BuildConditionsFromParameters(rule);
				this.BuildExceptionsFromParameters(rule);
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
				if (list.Count > 0)
				{
					rule.Conditions = list.ToArray();
				}
				else
				{
					rule.Conditions = null;
				}
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
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (RulesValidationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (TransientException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
			}
			catch (DataValidationException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001362C File Offset: 0x0001182C
		private void UpdateRuleFromParameters(Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule)
		{
			if (rule.Name.Equals(GetSupervisionPolicy.ClosedCampusInboundRuleName))
			{
				if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled.Name))
				{
					rule.State = (this.ClosedCampusInboundPolicyEnabled ? RuleState.Enabled : RuleState.Disabled);
				}
				if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundGroupExceptions.Name))
				{
					this.VerifyDistributionGroups(this.ClosedCampusInboundGroupExceptions, SupervisionPolicySchema.ClosedCampusInboundGroupExceptions.Name);
					rule.ExceptIfSentToMemberOf = ((this.ClosedCampusInboundGroupExceptions == null) ? null : this.ClosedCampusInboundGroupExceptions.ToArray());
				}
				if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundDomainExceptions.Name))
				{
					rule.ExceptIfFromAddressMatchesPatterns = this.ConvertToPatterns(this.ClosedCampusInboundDomainExceptions);
					return;
				}
			}
			else if (rule.Name.Equals(GetSupervisionPolicy.ClosedCampusOutboundRuleName))
			{
				if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled.Name))
				{
					rule.State = (this.ClosedCampusOutboundPolicyEnabled ? RuleState.Enabled : RuleState.Disabled);
				}
				if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions.Name))
				{
					this.VerifyDistributionGroups(this.ClosedCampusOutboundGroupExceptions, SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions.Name);
					rule.ExceptIfFromMemberOf = ((this.ClosedCampusOutboundGroupExceptions == null) ? null : this.ClosedCampusOutboundGroupExceptions.ToArray());
				}
				if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions.Name))
				{
					rule.ExceptIfRecipientAddressMatchesPatterns = this.ConvertToPatterns(this.ClosedCampusOutboundDomainExceptions);
					return;
				}
			}
			else if (rule.Name.Equals(GetSupervisionPolicy.BadWordsRuleName))
			{
				if (base.Fields.IsModified(SupervisionPolicySchema.BadWordsPolicyEnabled.Name))
				{
					if (this.BadWordsPolicyEnabled && ((base.Fields.IsModified(SupervisionPolicySchema.BadWordsList.Name) && string.IsNullOrEmpty(this.BadWordsList)) || (!base.Fields.IsModified(SupervisionPolicySchema.BadWordsList.Name) && rule.SubjectOrBodyContainsWords == null)))
					{
						base.WriteError(new SupervisionPolicyTaskException(Strings.BadWordsPolicyNotEnabledIfBadWordsListNull), (ErrorCategory)1003, null);
					}
					rule.State = (this.BadWordsPolicyEnabled ? RuleState.Enabled : RuleState.Disabled);
				}
				if (base.Fields.IsModified(SupervisionPolicySchema.BadWordsList.Name))
				{
					Word[] array = this.ConvertFromCommaSeparatedStringToWords(this.BadWordsList);
					if ((string.IsNullOrEmpty(this.BadWordsList) || array.Length == 0) && ((base.Fields.IsModified(SupervisionPolicySchema.BadWordsPolicyEnabled.Name) && this.BadWordsPolicyEnabled) || (!base.Fields.IsModified(SupervisionPolicySchema.BadWordsPolicyEnabled.Name) && rule.State == RuleState.Enabled)))
					{
						base.WriteError(new SupervisionPolicyTaskException(Strings.BadWordsPolicyNotEnabledIfBadWordsListNull), (ErrorCategory)1003, null);
					}
					rule.SubjectOrBodyContainsWords = array;
					return;
				}
			}
			else if (rule.Name.Equals(GetSupervisionPolicy.AntiBullyingRuleName) && base.Fields.IsModified(SupervisionPolicySchema.AntiBullyingPolicyEnabled.Name))
			{
				rule.State = (this.AntiBullyingPolicyEnabled ? RuleState.Enabled : RuleState.Disabled);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013910 File Offset: 0x00011B10
		private void VerifyDistributionGroups(MultiValuedProperty<RecipientIdParameter> identities, string parameterName)
		{
			if (identities == null)
			{
				return;
			}
			ADRecipient adrecipient = null;
			HashSet<ADObjectId> hashSet = new HashSet<ADObjectId>();
			foreach (RecipientIdParameter recipientIdParameter in identities)
			{
				try
				{
					adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, this.DataObject.OrganizationId.OrganizationalUnit, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())));
				}
				catch (ManagementObjectNotFoundException exception)
				{
					base.WriteError(exception, (ErrorCategory)1003, null);
				}
				OrganizationId organizationId = this.DataObject.OrganizationId;
				OrganizationId organizationId2 = adrecipient.OrganizationId;
				if (!organizationId.Equals(organizationId2))
				{
					base.WriteError(new SupervisionPolicyTaskException(Strings.RecipientNotFoundInOrganization(recipientIdParameter.ToString())), (ErrorCategory)1003, null);
				}
				if (!ADRecipient.IsAllowedDeliveryRestrictionGroup(adrecipient.RecipientType))
				{
					base.WriteError(new SupervisionPolicyTaskException(Strings.SpecifiedRecipientNotDistributionGroup(recipientIdParameter.ToString())), (ErrorCategory)1003, null);
				}
				if (!hashSet.Add(adrecipient.Id))
				{
					base.WriteError(new SupervisionPolicyTaskException(Strings.ErrorRecipientIdParamElementsNotUnique(parameterName, recipientIdParameter.ToString())), (ErrorCategory)1003, null);
				}
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013A74 File Offset: 0x00011C74
		private Pattern[] ConvertToPatterns(IList<SmtpDomain> domains)
		{
			if (domains == null)
			{
				return null;
			}
			Pattern[] array = new Pattern[domains.Count];
			for (int i = 0; i < domains.Count; i++)
			{
				array[i] = new Pattern("@" + domains[i].ToString(), true, false);
			}
			return array;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013AD0 File Offset: 0x00011CD0
		private void BuildConditionsFromParameters(Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule)
		{
			TypeMapping[] bridgeheadMappings = TransportRulePredicate.BridgeheadMappings;
			List<TransportRulePredicate> list = new List<TransportRulePredicate>();
			this.conditionTypesToUpdate = new List<Type>();
			if (base.Fields.IsModified(SupervisionPolicySchema.BadWordsList.Name))
			{
				Word[] subjectOrBodyContainsWords = rule.SubjectOrBodyContainsWords;
				if (subjectOrBodyContainsWords != null && subjectOrBodyContainsWords.Length > 0)
				{
					SubjectOrBodyContainsPredicate subjectOrBodyContainsPredicate = new SubjectOrBodyContainsPredicate();
					subjectOrBodyContainsPredicate.Initialize(bridgeheadMappings);
					subjectOrBodyContainsPredicate.Words = subjectOrBodyContainsWords;
					Utils.InsertPredicateSorted(subjectOrBodyContainsPredicate, list);
				}
				this.conditionTypesToUpdate.Add(typeof(SubjectOrBodyContainsPredicate));
			}
			this.conditionsSetByParameters = list.ToArray();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013B58 File Offset: 0x00011D58
		private void BuildExceptionsFromParameters(Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule)
		{
			TypeMapping[] bridgeheadMappings = TransportRulePredicate.BridgeheadMappings;
			List<TransportRulePredicate> list = new List<TransportRulePredicate>();
			this.exceptionTypesToUpdate = new List<Type>();
			if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions.Name))
			{
				RecipientIdParameter[] exceptIfFromMemberOf = rule.ExceptIfFromMemberOf;
				if (exceptIfFromMemberOf != null && exceptIfFromMemberOf.Length > 0)
				{
					FromMemberOfPredicate fromMemberOfPredicate = new FromMemberOfPredicate();
					fromMemberOfPredicate.Initialize(bridgeheadMappings);
					fromMemberOfPredicate.Addresses = Utils.BuildSmtpAddressArray(exceptIfFromMemberOf, base.TenantGlobalCatalogSession);
					Utils.InsertPredicateSorted(fromMemberOfPredicate, list);
				}
				this.exceptionTypesToUpdate.Add(typeof(FromMemberOfPredicate));
			}
			if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundGroupExceptions.Name))
			{
				RecipientIdParameter[] exceptIfSentToMemberOf = rule.ExceptIfSentToMemberOf;
				if (exceptIfSentToMemberOf != null && exceptIfSentToMemberOf.Length > 0)
				{
					SentToMemberOfPredicate sentToMemberOfPredicate = new SentToMemberOfPredicate();
					sentToMemberOfPredicate.Initialize(bridgeheadMappings);
					sentToMemberOfPredicate.Addresses = Utils.BuildSmtpAddressArray(exceptIfSentToMemberOf, base.TenantGlobalCatalogSession);
					Utils.InsertPredicateSorted(sentToMemberOfPredicate, list);
				}
				this.exceptionTypesToUpdate.Add(typeof(SentToMemberOfPredicate));
			}
			if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusInboundDomainExceptions.Name))
			{
				Pattern[] exceptIfFromAddressMatchesPatterns = rule.ExceptIfFromAddressMatchesPatterns;
				if (exceptIfFromAddressMatchesPatterns != null && exceptIfFromAddressMatchesPatterns.Length > 0)
				{
					FromAddressMatchesPredicate fromAddressMatchesPredicate = new FromAddressMatchesPredicate();
					fromAddressMatchesPredicate.Initialize(bridgeheadMappings);
					fromAddressMatchesPredicate.Patterns = exceptIfFromAddressMatchesPatterns;
					Utils.InsertPredicateSorted(fromAddressMatchesPredicate, list);
				}
				this.exceptionTypesToUpdate.Add(typeof(FromAddressMatchesPredicate));
			}
			if (base.Fields.IsModified(SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions.Name))
			{
				Pattern[] exceptIfRecipientAddressMatchesPatterns = rule.ExceptIfRecipientAddressMatchesPatterns;
				if (exceptIfRecipientAddressMatchesPatterns != null && exceptIfRecipientAddressMatchesPatterns.Length > 0)
				{
					RecipientAddressMatchesPatternsPredicate recipientAddressMatchesPatternsPredicate = new RecipientAddressMatchesPatternsPredicate();
					recipientAddressMatchesPatternsPredicate.Initialize(bridgeheadMappings);
					recipientAddressMatchesPatternsPredicate.Patterns = exceptIfRecipientAddressMatchesPatterns;
					Utils.InsertPredicateSorted(recipientAddressMatchesPatternsPredicate, list);
				}
				this.exceptionTypesToUpdate.Add(typeof(RecipientAddressMatchesPatternsPredicate));
			}
			this.exceptionsSetByParameters = list.ToArray();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013D14 File Offset: 0x00011F14
		private SupervisionTransportRule LoadRule(string ruleName, ADObjectId supervisionTransportRuleCollectionId)
		{
			QueryFilter ruleFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, ruleName);
			List<SupervisionTransportRule> supervisionRulesWithFilter = this.GetSupervisionRulesWithFilter(supervisionTransportRuleCollectionId, ruleFilter);
			if (supervisionRulesWithFilter.Count == 0)
			{
				Dictionary<string, string> ruleNames = GetSupervisionPolicy.GetRuleNames();
				base.WriteError(new SupervisionPoliciesNotFoundException(Strings.SupervisionPoliciesNotFound(ruleNames[ruleName])), (ErrorCategory)1003, ruleName);
			}
			else if (supervisionRulesWithFilter.Count > 1)
			{
				base.WriteError(new ManagementObjectAmbiguousException(Strings.SupervisionPolicyAmbiguous), (ErrorCategory)1003, null);
			}
			return supervisionRulesWithFilter[0];
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00013D8C File Offset: 0x00011F8C
		private List<SupervisionTransportRule> GetSupervisionRulesWithFilter(ADObjectId supervisionTransportRuleCollectionId, QueryFilter ruleFilter)
		{
			IEnumerable<SupervisionTransportRule> enumerable = base.DataSession.FindPaged<SupervisionTransportRule>(ruleFilter, supervisionTransportRuleCollectionId, false, null, 0);
			List<SupervisionTransportRule> list = new List<SupervisionTransportRule>();
			foreach (SupervisionTransportRule item in enumerable)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013DEC File Offset: 0x00011FEC
		private ADObjectId GetSupervisionTransportRuleCollectionId()
		{
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, "TransportVersioned", MatchOptions.FullString, MatchFlags.Default);
			IEnumerable<TransportRuleCollection> enumerable = base.DataSession.FindPaged<TransportRuleCollection>(filter, null, true, null, 2);
			TransportRuleCollection transportRuleCollection = null;
			if (enumerable != null)
			{
				foreach (TransportRuleCollection transportRuleCollection2 in enumerable)
				{
					if (transportRuleCollection != null)
					{
						base.WriteError(new ManagementObjectAmbiguousException(Strings.SupervisionPolicyCollectionAmbiguous), (ErrorCategory)1003, null);
					}
					transportRuleCollection = transportRuleCollection2;
				}
			}
			if (transportRuleCollection == null)
			{
				base.WriteError(new RuleCollectionNotInAdException("TransportVersioned"), (ErrorCategory)1003, this.Identity);
			}
			return transportRuleCollection.Id;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013E9C File Offset: 0x0001209C
		private Word[] ConvertFromCommaSeparatedStringToWords(string commaSeparatedString)
		{
			if (string.IsNullOrEmpty(commaSeparatedString))
			{
				return null;
			}
			string[] array = commaSeparatedString.Replace(Environment.NewLine, SupervisionPolicy.BadWordsSeparator).Trim().Split(SupervisionPolicy.BadWordsSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			List<Word> list = new List<Word>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!string.IsNullOrEmpty(text))
				{
					if (text.Length > 128)
					{
						base.WriteError(new BadwordsLengthTooLongException(text.Substring(0, (text.Length >= 32) ? 32 : text.Length), 128), ErrorCategory.InvalidArgument, text);
					}
					list.Add(new Word(text));
				}
			}
			return list.ToArray();
		}

		// Token: 0x040001A7 RID: 423
		private const string ClosedCampusInboundParameterSetName = "ClosedCampusInbound";

		// Token: 0x040001A8 RID: 424
		private const string ClosedCampusOutboundParameterSetName = "ClosedCampusOutbound";

		// Token: 0x040001A9 RID: 425
		private const string BadWordsParameterSetName = "BadWords";

		// Token: 0x040001AA RID: 426
		private const string AntiBullyingParameterSetName = "AntiBullying";

		// Token: 0x040001AB RID: 427
		private const int MaxPrefixLengthToDisplay = 32;

		// Token: 0x040001AC RID: 428
		private static Regex standardBadWordsRegex = new Regex("^" + GetSupervisionPolicy.BadWordsRuleName + "__\\d+$");

		// Token: 0x040001AD RID: 429
		private TransportRulePredicate[] conditionsSetByParameters;

		// Token: 0x040001AE RID: 430
		private TransportRulePredicate[] exceptionsSetByParameters;

		// Token: 0x040001AF RID: 431
		private List<Type> conditionTypesToUpdate;

		// Token: 0x040001B0 RID: 432
		private List<Type> exceptionTypesToUpdate;
	}
}
