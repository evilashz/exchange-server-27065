using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AF9 RID: 2809
	[Cmdlet("New", "OutlookProtectionRule", SupportsShouldProcess = true)]
	public sealed class NewOutlookProtectionRule : NewMultitenancySystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x060063D7 RID: 25559 RVA: 0x001A16ED File Offset: 0x0019F8ED
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOutlookProtectionRule(base.Name);
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x060063D8 RID: 25560 RVA: 0x001A16FA File Offset: 0x0019F8FA
		// (set) Token: 0x060063D9 RID: 25561 RVA: 0x001A1711 File Offset: 0x0019F911
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x060063DA RID: 25562 RVA: 0x001A1724 File Offset: 0x0019F924
		// (set) Token: 0x060063DB RID: 25563 RVA: 0x001A174A File Offset: 0x0019F94A
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return !this.IsParameterSpecified("Enabled") || (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x060063DC RID: 25564 RVA: 0x001A1762 File Offset: 0x0019F962
		// (set) Token: 0x060063DD RID: 25565 RVA: 0x001A176A File Offset: 0x0019F96A
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

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x060063DE RID: 25566 RVA: 0x001A1773 File Offset: 0x0019F973
		// (set) Token: 0x060063DF RID: 25567 RVA: 0x001A178A File Offset: 0x0019F98A
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x060063E0 RID: 25568 RVA: 0x001A179D File Offset: 0x0019F99D
		// (set) Token: 0x060063E1 RID: 25569 RVA: 0x001A17C3 File Offset: 0x0019F9C3
		[Parameter(Mandatory = false)]
		[ValidateRange(0, 2147483647)]
		public int Priority
		{
			get
			{
				if (this.IsParameterSpecified("Priority"))
				{
					return (int)base.Fields["Priority"];
				}
				return 0;
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x060063E2 RID: 25570 RVA: 0x001A17DB File Offset: 0x0019F9DB
		// (set) Token: 0x060063E3 RID: 25571 RVA: 0x001A17F2 File Offset: 0x0019F9F2
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SentTo"];
			}
			set
			{
				base.Fields["SentTo"] = value;
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x060063E4 RID: 25572 RVA: 0x001A1805 File Offset: 0x0019FA05
		// (set) Token: 0x060063E5 RID: 25573 RVA: 0x001A182B File Offset: 0x0019FA2B
		[Parameter(Mandatory = false)]
		public ToUserScope SentToScope
		{
			get
			{
				if (this.IsParameterSpecified("SentToScope"))
				{
					return (ToUserScope)base.Fields["SentToScope"];
				}
				return ToUserScope.All;
			}
			set
			{
				base.Fields["SentToScope"] = value;
			}
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x060063E6 RID: 25574 RVA: 0x001A1843 File Offset: 0x0019FA43
		// (set) Token: 0x060063E7 RID: 25575 RVA: 0x001A1869 File Offset: 0x0019FA69
		[Parameter(Mandatory = false)]
		public bool UserCanOverride
		{
			get
			{
				return !this.IsParameterSpecified("UserCanOverride") || (bool)base.Fields["UserCanOverride"];
			}
			set
			{
				base.Fields["UserCanOverride"] = value;
			}
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x001A1884 File Offset: 0x0019FA84
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = base.CreateSession();
			this.rmsTemplateDataProvider = new RmsTemplateDataProvider((IConfigurationSession)configDataProvider, RmsTemplateType.Distributed, true);
			this.priorityHelper = new PriorityHelper(configDataProvider);
			return configDataProvider;
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x001A18B8 File Offset: 0x0019FAB8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)base.PrepareDataObject();
			transportRule.SetId(Utils.GetRuleId(base.DataSession, base.Name));
			transportRule.OrganizationId = this.ResolveCurrentOrganization();
			transportRule.Priority = this.GetSequenceNumberForPriority(this.Priority);
			transportRule.Xml = new OutlookProtectionRulePresentationObject(transportRule)
			{
				ApplyRightsProtectionTemplate = this.ResolveTemplate(),
				Enabled = this.Enabled,
				FromDepartment = this.FromDepartment,
				SentTo = this.ResolveSentToRecipients(),
				SentToScope = this.SentToScope,
				UserCanOverride = this.UserCanOverride
			}.Serialize();
			TaskLogger.LogExit();
			return transportRule;
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x001A196C File Offset: 0x0019FB6C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.RuleNameAlreadyInUse())
				{
					base.WriteError(new OutlookProtectionRuleNameIsNotUniqueException(base.Name), (ErrorCategory)1000, this.DataObject);
				}
				if (!this.IsPriorityValid(this.Priority))
				{
					base.WriteError(new OutlookProtectionRuleInvalidPriorityException(), (ErrorCategory)1000, this.DataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x001A19D9 File Offset: 0x0019FBD9
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (Utils.IsEmptyCondition(this.DataObject) && !this.Force && !base.ShouldContinue(Strings.ConfirmationMessageOutlookProtectionRuleWithEmptyCondition(base.Name)))
			{
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x001A1A1C File Offset: 0x0019FC1C
		protected override void WriteResult(IConfigurable result)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)result;
			base.WriteResult(new OutlookProtectionRulePresentationObject(transportRule)
			{
				Priority = this.Priority
			});
			TaskLogger.LogExit();
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x001A1A54 File Offset: 0x0019FC54
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ParserException).IsInstanceOfType(exception) || RmsUtil.IsKnownException(exception);
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x001A1A79 File Offset: 0x0019FC79
		private bool IsParameterSpecified(string parameterName)
		{
			return base.Fields.IsModified(parameterName);
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x001A1A87 File Offset: 0x0019FC87
		private int GetSequenceNumberForPriority(int priority)
		{
			return this.priorityHelper.GetSequenceNumberToInsertPriority(priority);
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x001A1A95 File Offset: 0x0019FC95
		private int GetPriorityFromSequenceNumber(int sequenceNumber)
		{
			return this.priorityHelper.GetPriorityFromSequenceNumber(sequenceNumber);
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x001A1AA3 File Offset: 0x0019FCA3
		private bool IsPriorityValid(int priority)
		{
			return this.priorityHelper.IsPriorityValidForInsertion(priority);
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x001A1AB1 File Offset: 0x0019FCB1
		private bool RuleNameAlreadyInUse()
		{
			return base.DataSession.Read<TransportRule>(Utils.GetRuleId(base.DataSession, base.Name)) != null;
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x001A1AD8 File Offset: 0x0019FCD8
		private RmsTemplateIdentity ResolveTemplate()
		{
			string name = (this.ApplyRightsProtectionTemplate != null) ? this.ApplyRightsProtectionTemplate.ToString() : string.Empty;
			RmsTemplatePresentation rmsTemplatePresentation = (RmsTemplatePresentation)base.GetDataObject<RmsTemplatePresentation>(this.ApplyRightsProtectionTemplate, this.rmsTemplateDataProvider, null, new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotFound(name)), new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotUnique(name)));
			return (RmsTemplateIdentity)rmsTemplatePresentation.Identity;
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x001A1B3C File Offset: 0x0019FD3C
		private SmtpAddress[] ResolveSentToRecipients()
		{
			LocalizedException exception;
			IEnumerable<SmtpAddress> enumerable = Utils.ResolveRecipientIdParameters(base.TenantGlobalCatalogSession, this.SentTo, out exception);
			if (enumerable == null)
			{
				base.WriteError(exception, (ErrorCategory)1000, this.DataObject);
			}
			return enumerable.ToArray<SmtpAddress>();
		}

		// Token: 0x040035FF RID: 13823
		private RmsTemplateDataProvider rmsTemplateDataProvider;

		// Token: 0x04003600 RID: 13824
		private PriorityHelper priorityHelper;

		// Token: 0x04003601 RID: 13825
		private SwitchParameter force;

		// Token: 0x02000AFA RID: 2810
		private static class DefaultParameterValues
		{
			// Token: 0x04003602 RID: 13826
			public const bool Enabled = true;

			// Token: 0x04003603 RID: 13827
			public const ToUserScope SentToScope = ToUserScope.All;

			// Token: 0x04003604 RID: 13828
			public const bool UserCanOverride = true;

			// Token: 0x04003605 RID: 13829
			public const int Priority = 0;
		}
	}
}
