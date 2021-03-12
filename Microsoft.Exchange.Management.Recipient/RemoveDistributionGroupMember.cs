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
	// Token: 0x0200002C RID: 44
	[Cmdlet("Remove", "DistributionGroupMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDistributionGroupMember : DistributionGroupMemberTaskBase<GeneralRecipientIdParameter>
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A812 File Offset: 0x00008A12
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDistributionGroupMember(this.Identity.ToString(), base.Member.ToString());
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A82F File Offset: 0x00008A2F
		protected override string ApprovalAction
		{
			get
			{
				return "Remove-DistributionGroupMember";
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A838 File Offset: 0x00008A38
		protected override void PerformGroupMemberAction()
		{
			try
			{
				ADRecipient memberRecipient = (ADRecipient)base.GetDataObject<ADRecipient>(base.Member, base.TenantGlobalCatalogSession, this.DataObject.OrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound((string)base.Member)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)base.Member)));
				MailboxTaskHelper.ValidateAndRemoveMember(this.DataObject, memberRecipient);
			}
			catch (ManagementObjectNotFoundException)
			{
				ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(base.Member, base.PartitionOrRootOrgGlobalCatalogSession, this.DataObject.OrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound((string)base.Member)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)base.Member)), ExchangeErrorCategory.Client);
				ADObjectId x;
				if (adrecipient == null || !base.TryGetExecutingUserId(out x) || !ADObjectId.Equals(x, adrecipient.Id) || !adrecipient.HiddenFromAddressListsEnabled)
				{
					throw;
				}
				MailboxTaskHelper.ValidateAndRemoveMember(this.DataObject, adrecipient);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A948 File Offset: 0x00008B48
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (ADNotAMemberException)
			{
				MailboxTaskHelper.WriteMemberNotFoundError(this.DataObject, base.Member, this.Identity.RawIdentity, base.IsSelfMemberAction, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		protected override void GroupMemberCheck(ADRecipient requester)
		{
			if (!this.DataObject.Members.Contains(requester.Id))
			{
				base.WriteError(new SelfMemberNotFoundException(this.Identity.ToString()), ErrorCategory.InvalidData, new RecipientIdParameter(requester.Id));
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A9DC File Offset: 0x00008BDC
		protected override MemberUpdateType MemberUpdateRestriction
		{
			get
			{
				return this.DataObject.MemberDepartRestriction;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A9E9 File Offset: 0x00008BE9
		protected override GeneralRecipientIdParameter IdentityFactory(ADObjectId id)
		{
			return new GeneralRecipientIdParameter(id);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A9F1 File Offset: 0x00008BF1
		protected override void WriteApprovalRequiredWarning(string messageId)
		{
			this.WriteWarning(Strings.WarningDistributionListDepartApprovalRequired(this.requester.DisplayName, this.DataObject.DisplayName, messageId));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000AA15 File Offset: 0x00008C15
		protected override void WriteClosedUpdateError()
		{
			base.WriteError(new RecipientTaskException(Strings.ErrorDistributionListDepartClosed(this.DataObject.DisplayName)), ErrorCategory.PermissionDenied, this.Identity);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AA3A File Offset: 0x00008C3A
		protected override LocalizedString ApprovalMessageSubject()
		{
			return Strings.AutoGroupDepartMessageSubject(this.requester.DisplayName, this.DataObject.DisplayName);
		}
	}
}
