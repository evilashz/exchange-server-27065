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
	// Token: 0x0200005F RID: 95
	[Cmdlet("Disable", "JournalArchiving", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableJournalArchiving : RemoveMailboxBase<MailboxIdParameter>
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001B076 File Offset: 0x00019276
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationDisableJournalArchiving;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001B080 File Offset: 0x00019280
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.UserMailbox)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			this.mailUser = (ADUser)MailboxTaskHelper.GetJournalArchiveMailUser(base.DataSession as IRecipientSession, (ADUser)adrecipient);
			return adrecipient;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001B114 File Offset: 0x00019314
		protected override void InternalValidate()
		{
			this.skipJournalArchivingCheck = true;
			base.InternalValidate();
			if (this.mailUser != null && this.mailUser.IsDirSyncEnabled && MailboxTaskHelper.IsOrgDirSyncEnabled(this.ConfigurationSession, this.mailUser.OrganizationId))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorRemoveMailboxWithJournalArchiveWithDirSync), ExchangeErrorCategory.Client, this.mailUser.Identity);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001B17B File Offset: 0x0001937B
		protected override bool ShouldSoftDeleteObject()
		{
			return DisableJournalArchiving.ShouldSoftDeleteUser(base.DataObject);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B188 File Offset: 0x00019388
		protected override void InternalProcessRecord()
		{
			this.RemoveMailUser();
			base.DataObject.JournalArchiveAddress = SmtpAddress.NullReversePath;
			base.InternalProcessRecord();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001B1A6 File Offset: 0x000193A6
		private static bool ShouldSoftDeleteUser(ADUser user)
		{
			return user != null && !(user.OrganizationId == null) && user.OrganizationId.ConfigurationUnit != null && Globals.IsMicrosoftHostedOnly && SoftDeletedTaskHelper.IsSoftDeleteSupportedRecipientTypeDetail(user.RecipientTypeDetails);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001B1E0 File Offset: 0x000193E0
		private void RemoveMailUser()
		{
			if (this.mailUser == null)
			{
				this.WriteWarning(Strings.WarningJournalArchiveMailboxHasNoMailUser);
				return;
			}
			if (DisableJournalArchiving.ShouldSoftDeleteUser(this.mailUser))
			{
				SoftDeletedTaskHelper.UpdateRecipientForSoftDelete(base.DataSession as IRecipientSession, this.mailUser, false);
				SoftDeletedTaskHelper.UpdateExchangeGuidForMailEnabledUser(this.mailUser);
				this.mailUser.JournalArchiveAddress = SmtpAddress.NullReversePath;
				base.WriteVerbose(TaskVerboseStringHelper.GetDeleteObjectVerboseString(this.mailUser.Identity, base.DataSession, typeof(ADUser)));
				try
				{
					try
					{
						RecipientTaskHelper.CreateSoftDeletedObjectsContainerIfNecessary(this.mailUser.Id.Parent, base.DomainController);
						base.DataSession.Save(this.mailUser);
					}
					catch (DataSourceTransientException exception)
					{
						base.WriteError(exception, ExchangeErrorCategory.ServerTransient, null);
					}
					return;
				}
				finally
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
				}
			}
			base.WriteError(new RecipientTaskException(Strings.ErrorDisableJournalArchiveMailuserNotSoftDeleted), ExchangeErrorCategory.Client, this.mailUser.Identity);
		}

		// Token: 0x04000188 RID: 392
		private ADUser mailUser;
	}
}
