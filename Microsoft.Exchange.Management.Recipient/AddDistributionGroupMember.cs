using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000019 RID: 25
	[Cmdlet("Add", "DistributionGroupMember", SupportsShouldProcess = true)]
	public sealed class AddDistributionGroupMember : DistributionGroupMemberTaskBase<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000082C2 File Offset: 0x000064C2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddDistributionGroupMember(this.Identity.ToString(), base.Member.ToString());
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000082DF File Offset: 0x000064DF
		protected override string ApprovalAction
		{
			get
			{
				return "Add-DistributionGroupMember";
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000082E8 File Offset: 0x000064E8
		protected override void PerformGroupMemberAction()
		{
			try
			{
				ADRecipient memberRecipient = (ADRecipient)base.GetDataObject<ADRecipient>(base.Member, base.GlobalCatalogRBACSession, this.DataObject.OrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound((string)base.Member)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)base.Member)), ExchangeErrorCategory.Client);
				MailboxTaskHelper.ValidateAndAddMember(this.DataObject, base.Member, memberRecipient, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			catch (ManagementObjectNotFoundException)
			{
				ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(base.Member, base.PartitionOrRootOrgGlobalCatalogSession, this.DataObject.OrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound((string)base.Member)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)base.Member)), ExchangeErrorCategory.Client);
				ADObjectId x;
				if (adrecipient == null || !base.TryGetExecutingUserId(out x) || !ADObjectId.Equals(x, adrecipient.Id) || !adrecipient.HiddenFromAddressListsEnabled)
				{
					throw;
				}
				MailboxTaskHelper.ValidateAndAddMember(this.DataObject, base.Member, adrecipient, new Task.ErrorLoggerDelegate(base.WriteError));
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008420 File Offset: 0x00006620
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (ADObjectEntryAlreadyExistsException)
			{
				MailboxTaskHelper.WriteMemberAlreadyExistsError(this.DataObject, base.Member, base.IsSelfMemberAction, new Task.ErrorLoggerDelegate(base.WriteError));
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000846C File Offset: 0x0000666C
		protected override void GroupMemberCheck(ADRecipient requester)
		{
			if (this.DataObject.Members.Contains(requester.Id))
			{
				base.WriteError(new SelfMemberAlreadyExistsException(this.Identity.ToString()), ErrorCategory.InvalidData, new RecipientWithAdUserGroupIdParameter<RecipientIdParameter>(requester.Id));
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000084A8 File Offset: 0x000066A8
		protected override MemberUpdateType MemberUpdateRestriction
		{
			get
			{
				return this.DataObject.MemberJoinRestriction;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000084B5 File Offset: 0x000066B5
		protected override RecipientWithAdUserGroupIdParameter<RecipientIdParameter> IdentityFactory(ADObjectId id)
		{
			return new RecipientWithAdUserGroupIdParameter<RecipientIdParameter>(id);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000084BD File Offset: 0x000066BD
		protected override void WriteApprovalRequiredWarning(string messageId)
		{
			this.WriteWarning(Strings.WarningDistributionListJoinApprovalRequired(this.requester.DisplayName, this.DataObject.DisplayName, messageId));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000084E1 File Offset: 0x000066E1
		protected override void WriteClosedUpdateError()
		{
			base.WriteError(new RecipientTaskException(Strings.ErrorDistributionListJoinClosed(this.DataObject.DisplayName)), ExchangeErrorCategory.Client, this.Identity);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008509 File Offset: 0x00006709
		protected override LocalizedString ApprovalMessageSubject()
		{
			return Strings.AutoGroupJoinMessageSubject;
		}
	}
}
