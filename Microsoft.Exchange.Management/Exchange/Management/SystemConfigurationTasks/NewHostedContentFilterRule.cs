using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A4F RID: 2639
	[Cmdlet("New", "HostedContentFilterRule", SupportsShouldProcess = true)]
	public sealed class NewHostedContentFilterRule : NewHygieneFilterRuleTaskBase
	{
		// Token: 0x06005E7F RID: 24191 RVA: 0x0018BEFC File Offset: 0x0018A0FC
		public NewHostedContentFilterRule() : base("HostedContentFilterVersioned")
		{
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06005E80 RID: 24192 RVA: 0x0018BF09 File Offset: 0x0018A109
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewHostedContentFilterRule(base.Name);
			}
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x0018BF16 File Offset: 0x0018A116
		// (set) Token: 0x06005E82 RID: 24194 RVA: 0x0018BF2D File Offset: 0x0018A12D
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

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x0018BF40 File Offset: 0x0018A140
		// (set) Token: 0x06005E84 RID: 24196 RVA: 0x0018BF57 File Offset: 0x0018A157
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentToMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SentToMemberOf"];
			}
			set
			{
				base.Fields["SentToMemberOf"] = value;
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x0018BF6A File Offset: 0x0018A16A
		// (set) Token: 0x06005E86 RID: 24198 RVA: 0x0018BF81 File Offset: 0x0018A181
		[Parameter(Mandatory = false)]
		public Word[] RecipientDomainIs
		{
			get
			{
				return (Word[])base.Fields["RecipientDomainIs"];
			}
			set
			{
				base.Fields["RecipientDomainIs"] = value;
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06005E87 RID: 24199 RVA: 0x0018BF94 File Offset: 0x0018A194
		// (set) Token: 0x06005E88 RID: 24200 RVA: 0x0018BFAB File Offset: 0x0018A1AB
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfSentTo"];
			}
			set
			{
				base.Fields["ExceptIfSentTo"] = value;
			}
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06005E89 RID: 24201 RVA: 0x0018BFBE File Offset: 0x0018A1BE
		// (set) Token: 0x06005E8A RID: 24202 RVA: 0x0018BFD5 File Offset: 0x0018A1D5
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentToMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfSentToMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfSentToMemberOf"] = value;
			}
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06005E8B RID: 24203 RVA: 0x0018BFE8 File Offset: 0x0018A1E8
		// (set) Token: 0x06005E8C RID: 24204 RVA: 0x0018BFFF File Offset: 0x0018A1FF
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientDomainIs
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientDomainIs"];
			}
			set
			{
				base.Fields["ExceptIfRecipientDomainIs"] = value;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x06005E8D RID: 24205 RVA: 0x0018C012 File Offset: 0x0018A212
		// (set) Token: 0x06005E8E RID: 24206 RVA: 0x0018C029 File Offset: 0x0018A229
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public HostedContentFilterPolicyIdParameter HostedContentFilterPolicy
		{
			get
			{
				return (HostedContentFilterPolicyIdParameter)base.Fields["HostedContentFilterPolicy"];
			}
			set
			{
				base.Fields["HostedContentFilterPolicy"] = value;
			}
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x0018C03C File Offset: 0x0018A23C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.policyObject = HygieneUtils.ResolvePolicyObject<HostedContentFilterPolicy>(this, this.ConfigurationSession, this.HostedContentFilterPolicy);
			TransportRule transportRule = HygieneUtils.ResolvePolicyRuleObject<HostedContentFilterPolicy>(this.policyObject, this.ConfigurationSession, this.ruleCollectionName);
			if (transportRule != null)
			{
				base.WriteError(new OperationNotAllowedException(Strings.ErrorPolicyRuleExists(this.policyObject.Name, transportRule.Name)), ErrorCategory.InvalidOperation, null);
			}
			if (this.policyObject != null && this.policyObject.IsDefault)
			{
				base.WriteError(new OperationNotAllowedException(Strings.ErrorDefaultPolicyCannotHaveRule(this.policyObject.Name)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x0018C0D8 File Offset: 0x0018A2D8
		protected override void InternalProcessRecord()
		{
			HostedContentFilterRule hostedContentFilterRule = new HostedContentFilterRule(null, base.Name, base.Priority, base.Enabled ? RuleState.Enabled : RuleState.Disabled, base.Comments, base.Conditions, base.Exceptions, new HostedContentFilterPolicyIdParameter(this.policyObject.Name));
			if (this.policyObject.EnableEndUserSpamNotifications && !hostedContentFilterRule.IsEsnCompatible)
			{
				base.WriteError(new OperationNotAllowedException(Strings.ErrorCannotScopeEsnPolicy(this.policyObject.Name)), ErrorCategory.InvalidOperation, null);
			}
			int priority = base.Fields.IsModified("Priority") ? hostedContentFilterRule.Priority : -1;
			TransportRule transportRule = null;
			try
			{
				TransportRule rule = hostedContentFilterRule.ToInternalRule();
				ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, base.DataSession);
				adruleStorageManager.LoadRuleCollection();
				adruleStorageManager.NewRule(rule, this.ResolveCurrentOrganization(), ref priority, out transportRule);
				FfoDualWriter.SaveToFfo<TransportRule>(this, transportRule, TenantSettingSyncLogType.DUALSYNCTR, null);
			}
			catch (RulesValidationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, base.Name);
			}
			catch (InvalidPriorityException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (ParserException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidData, null);
			}
			hostedContentFilterRule.Priority = priority;
			hostedContentFilterRule.SetTransportRule(transportRule);
			base.WriteObject(hostedContentFilterRule);
		}

		// Token: 0x040034EA RID: 13546
		private HostedContentFilterPolicy policyObject;
	}
}
