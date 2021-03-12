using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A52 RID: 2642
	[Cmdlet("Set", "HostedContentFilterRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHostedContentFilterRule : SetHygieneFilterRuleTaskBase<HostedContentFilterRule>
	{
		// Token: 0x06005EA7 RID: 24231 RVA: 0x0018C9A1 File Offset: 0x0018ABA1
		public SetHostedContentFilterRule() : base("HostedContentFilterVersioned")
		{
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06005EA8 RID: 24232 RVA: 0x0018C9AE File Offset: 0x0018ABAE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetHostedContentFilterRule(this.Identity.ToString());
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x06005EA9 RID: 24233 RVA: 0x0018C9C0 File Offset: 0x0018ABC0
		// (set) Token: 0x06005EAA RID: 24234 RVA: 0x0018C9D7 File Offset: 0x0018ABD7
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

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06005EAB RID: 24235 RVA: 0x0018C9EA File Offset: 0x0018ABEA
		// (set) Token: 0x06005EAC RID: 24236 RVA: 0x0018CA01 File Offset: 0x0018AC01
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

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x0018CA14 File Offset: 0x0018AC14
		// (set) Token: 0x06005EAE RID: 24238 RVA: 0x0018CA2B File Offset: 0x0018AC2B
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

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x0018CA3E File Offset: 0x0018AC3E
		// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x0018CA55 File Offset: 0x0018AC55
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

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x0018CA68 File Offset: 0x0018AC68
		// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x0018CA7F File Offset: 0x0018AC7F
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

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x0018CA92 File Offset: 0x0018AC92
		// (set) Token: 0x06005EB4 RID: 24244 RVA: 0x0018CAA9 File Offset: 0x0018ACA9
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

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x0018CABC File Offset: 0x0018ACBC
		// (set) Token: 0x06005EB6 RID: 24246 RVA: 0x0018CAD3 File Offset: 0x0018ACD3
		[Parameter(Mandatory = false)]
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

		// Token: 0x06005EB7 RID: 24247 RVA: 0x0018CAE6 File Offset: 0x0018ACE6
		internal override HygieneFilterRule CreateTaskRuleFromInternalRule(TransportRule internalRule, int priority)
		{
			return HostedContentFilterRule.CreateFromInternalRule(internalRule, priority, this.DataObject);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x0018CAF5 File Offset: 0x0018ACF5
		internal override ADIdParameter GetPolicyIdentity()
		{
			if (this.HostedContentFilterPolicy != null && this.policyObject != null)
			{
				return new HostedContentFilterPolicyIdParameter(this.policyObject.Name);
			}
			return null;
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x0018CB1C File Offset: 0x0018AD1C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.HostedContentFilterPolicy != null)
			{
				this.policyObject = HygieneUtils.ResolvePolicyObject<HostedContentFilterPolicy>(this, base.DataSession, this.HostedContentFilterPolicy);
				this.effectivePolicyObject = this.policyObject;
				TransportRule transportRule = HygieneUtils.ResolvePolicyRuleObject<HostedContentFilterPolicy>((HostedContentFilterPolicy)this.policyObject, base.DataSession, this.ruleCollectionName);
				if (transportRule != null)
				{
					base.WriteError(new OperationNotAllowedException(Strings.ErrorPolicyRuleExists(this.policyObject.Name, transportRule.Name)), ErrorCategory.InvalidOperation, null);
				}
				if (this.policyObject != null && ((HostedContentFilterPolicy)this.policyObject).IsDefault)
				{
					base.WriteError(new OperationNotAllowedException(Strings.ErrorDefaultPolicyCannotHaveRule(this.policyObject.Name)), ErrorCategory.InvalidOperation, null);
					return;
				}
			}
			else
			{
				HostedContentFilterRule hostedContentFilterRule = HostedContentFilterRule.CreateFromInternalRule((TransportRule)TransportRuleParser.Instance.GetRule(this.DataObject.Xml), -1, this.DataObject);
				this.effectivePolicyObject = HygieneUtils.ResolvePolicyObject<HostedContentFilterPolicy>(this, base.DataSession, hostedContentFilterRule.HostedContentFilterPolicy);
			}
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x0018CC18 File Offset: 0x0018AE18
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<TransportRule>(this, this.DataObject, TenantSettingSyncLogType.DUALSYNCTR, null);
		}
	}
}
