using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AFF RID: 2815
	[Cmdlet("Set", "OutlookProtectionRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOutlookProtectionRule : SetSystemConfigurationObjectTask<RuleIdParameter, OutlookProtectionRulePresentationObject, TransportRule>
	{
		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x06006418 RID: 25624 RVA: 0x001A22B3 File Offset: 0x001A04B3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOutlookProtectionRule(this.Identity.ToString());
			}
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x0600641A RID: 25626 RVA: 0x001A22CD File Offset: 0x001A04CD
		// (set) Token: 0x0600641B RID: 25627 RVA: 0x001A22E4 File Offset: 0x001A04E4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public RmsTemplateIdParameter ApplyRightsProtectionTemplate
		{
			get
			{
				return (RmsTemplateIdParameter)base.Fields["ApplyRightsProtectionTemplate"];
			}
			set
			{
				base.Fields["ApplyRightsProtectionTemplate"] = value;
			}
		}

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x0600641C RID: 25628 RVA: 0x001A22F7 File Offset: 0x001A04F7
		// (set) Token: 0x0600641D RID: 25629 RVA: 0x001A22FF File Offset: 0x001A04FF
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return this.force;
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x0600641E RID: 25630 RVA: 0x001A2308 File Offset: 0x001A0508
		// (set) Token: 0x0600641F RID: 25631 RVA: 0x001A231F File Offset: 0x001A051F
		[Parameter(Mandatory = false)]
		public string[] FromDepartment
		{
			get
			{
				return (string[])base.Fields["FromDepartment"];
			}
			set
			{
				base.Fields["FromDepartment"] = value;
			}
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x06006420 RID: 25632 RVA: 0x001A2332 File Offset: 0x001A0532
		// (set) Token: 0x06006421 RID: 25633 RVA: 0x001A2349 File Offset: 0x001A0549
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

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x001A235C File Offset: 0x001A055C
		// (set) Token: 0x06006423 RID: 25635 RVA: 0x001A2373 File Offset: 0x001A0573
		[ValidateRange(0, 2147483647)]
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)base.Fields["Priority"];
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x001A238B File Offset: 0x001A058B
		// (set) Token: 0x06006425 RID: 25637 RVA: 0x001A23A2 File Offset: 0x001A05A2
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientIdParameter> SentTo
		{
			get
			{
				return (MultiValuedProperty<RecipientIdParameter>)base.Fields["SentTo"];
			}
			set
			{
				base.Fields["SentTo"] = value;
			}
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x06006426 RID: 25638 RVA: 0x001A23B5 File Offset: 0x001A05B5
		// (set) Token: 0x06006427 RID: 25639 RVA: 0x001A23CC File Offset: 0x001A05CC
		[Parameter(Mandatory = false)]
		public ToUserScope SentToScope
		{
			get
			{
				return (ToUserScope)base.Fields["SentToScope"];
			}
			set
			{
				base.Fields["SentToScope"] = value;
			}
		}

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x06006428 RID: 25640 RVA: 0x001A23E4 File Offset: 0x001A05E4
		// (set) Token: 0x06006429 RID: 25641 RVA: 0x001A23FB File Offset: 0x001A05FB
		[Parameter(Mandatory = false)]
		public bool UserCanOverride
		{
			get
			{
				return (bool)base.Fields["UserCanOverride"];
			}
			set
			{
				base.Fields["UserCanOverride"] = value;
			}
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x001A2414 File Offset: 0x001A0614
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = base.CreateSession();
			this.priorityHelper = new PriorityHelper(configDataProvider);
			return configDataProvider;
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x001A2438 File Offset: 0x001A0638
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn("OutlookProtectionRules");
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.RuleNameAlreadyInUse())
			{
				base.WriteError(new OutlookProtectionRuleNameIsNotUniqueException(this.DataObject.Name), (ErrorCategory)1000, this.DataObject);
			}
			if (this.IsParameterSpecified("Priority") && !this.IsPriorityValid(this.Priority))
			{
				base.WriteError(new OutlookProtectionRuleInvalidPriorityException(), (ErrorCategory)1000, this.DataObject);
			}
			if (this.IsParameterSpecified("ApplyRightsProtectionTemplate"))
			{
				this.ResolveTemplate(this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x001A24F0 File Offset: 0x001A06F0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)dataObject;
			if (!Utils.IsChildOfOutlookProtectionRuleContainer(this.Identity))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
			OutlookProtectionRulePresentationObject outlookProtectionRulePresentationObject = new OutlookProtectionRulePresentationObject(transportRule);
			if (this.IsParameterSpecified("ApplyRightsProtectionTemplate"))
			{
				outlookProtectionRulePresentationObject.ApplyRightsProtectionTemplate = this.ResolveTemplate(transportRule);
			}
			if (this.IsParameterSpecified("FromDepartment"))
			{
				outlookProtectionRulePresentationObject.FromDepartment = this.FromDepartment;
			}
			if (this.IsParameterSpecified("Name"))
			{
				outlookProtectionRulePresentationObject.Name = this.Name;
			}
			if (this.IsParameterSpecified("Priority"))
			{
				outlookProtectionRulePresentationObject.Priority = this.Priority;
				transportRule.Priority = this.GetSequenceNumberForPriority(transportRule, outlookProtectionRulePresentationObject.Priority);
			}
			if (this.IsParameterSpecified("SentTo"))
			{
				if (this.SentTo == null)
				{
					outlookProtectionRulePresentationObject.SentTo = null;
				}
				else if (!this.SentTo.IsChangesOnlyCopy)
				{
					outlookProtectionRulePresentationObject.SentTo = this.ResolveRecipients(this.SentTo).ToArray<SmtpAddress>();
				}
				else
				{
					HashSet<SmtpAddress> first = new HashSet<SmtpAddress>(outlookProtectionRulePresentationObject.SentTo);
					IEnumerable<SmtpAddress> second = this.ResolveRecipients(this.SentTo.Added.Cast<RecipientIdParameter>());
					IEnumerable<SmtpAddress> second2 = this.ResolveRecipients(this.SentTo.Removed.Cast<RecipientIdParameter>());
					outlookProtectionRulePresentationObject.SentTo = first.Union(second).Except(second2).ToArray<SmtpAddress>();
				}
			}
			if (this.IsParameterSpecified("SentToScope"))
			{
				outlookProtectionRulePresentationObject.SentToScope = this.SentToScope;
			}
			if (this.IsParameterSpecified("UserCanOverride"))
			{
				outlookProtectionRulePresentationObject.UserCanOverride = this.UserCanOverride;
			}
			transportRule.Name = outlookProtectionRulePresentationObject.Name;
			transportRule.Xml = outlookProtectionRulePresentationObject.Serialize();
			TaskLogger.LogExit();
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x001A26C8 File Offset: 0x001A08C8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (Utils.IsEmptyCondition(this.DataObject) && !this.Force && !base.ShouldContinue(Strings.ConfirmationMessageOutlookProtectionRuleWithEmptyCondition(this.DataObject.Name)))
			{
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x001A2718 File Offset: 0x001A0918
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ParserException).IsInstanceOfType(exception) || RmsUtil.IsKnownException(exception);
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x001A273D File Offset: 0x001A093D
		private bool IsParameterSpecified(string parameterName)
		{
			return base.Fields.IsModified(parameterName);
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x001A274B File Offset: 0x001A094B
		private int GetSequenceNumberForPriority(TransportRule rule, int newPriority)
		{
			return this.priorityHelper.GetSequenceNumberToUpdatePriority(rule, newPriority);
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x001A275C File Offset: 0x001A095C
		private bool RuleNameAlreadyInUse()
		{
			IConfigurable configurable = base.DataSession.Read<TransportRule>(Utils.GetRuleId(base.DataSession, this.DataObject.Name));
			return configurable != null && !configurable.Identity.Equals(this.DataObject.Identity);
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x001A27A9 File Offset: 0x001A09A9
		private bool IsPriorityValid(int priority)
		{
			return this.priorityHelper.IsPriorityValidForUpdate(priority);
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x001A27B8 File Offset: 0x001A09B8
		private RmsTemplateIdentity ResolveTemplate(ADObject dataObject)
		{
			string name = (this.ApplyRightsProtectionTemplate != null) ? this.ApplyRightsProtectionTemplate.ToString() : string.Empty;
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, dataObject))
			{
				base.UnderscopeDataSession(dataObject.OrganizationId);
			}
			RmsTemplateDataProvider session = new RmsTemplateDataProvider((IConfigurationSession)base.DataSession, RmsTemplateType.Distributed, true);
			RmsTemplatePresentation rmsTemplatePresentation = (RmsTemplatePresentation)base.GetDataObject<RmsTemplatePresentation>(this.ApplyRightsProtectionTemplate, session, null, new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotFound(name)), new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotUnique(name)));
			return (RmsTemplateIdentity)rmsTemplatePresentation.Identity;
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x001A2848 File Offset: 0x001A0A48
		private IEnumerable<SmtpAddress> ResolveRecipients(IEnumerable<RecipientIdParameter> recipients)
		{
			LocalizedException exception;
			IEnumerable<SmtpAddress> enumerable = Utils.ResolveRecipientIdParameters(base.TenantGlobalCatalogSession, recipients, out exception);
			if (enumerable == null)
			{
				base.WriteError(exception, (ErrorCategory)1000, this.DataObject);
			}
			return enumerable;
		}

		// Token: 0x0400361A RID: 13850
		private PriorityHelper priorityHelper;

		// Token: 0x0400361B RID: 13851
		private SwitchParameter force;
	}
}
