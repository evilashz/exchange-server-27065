using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Providers;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B5 RID: 1973
	[Cmdlet("New", "MailMessage", SupportsShouldProcess = true)]
	public sealed class NewMailMessage : NewTenantADTaskBase<MailMessage>
	{
		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x0011D1BC File Offset: 0x0011B3BC
		// (set) Token: 0x0600456A RID: 17770 RVA: 0x0011D1C9 File Offset: 0x0011B3C9
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Subject
		{
			get
			{
				return this.DataObject.Subject;
			}
			set
			{
				this.DataObject.Subject = value;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x0011D1D7 File Offset: 0x0011B3D7
		// (set) Token: 0x0600456C RID: 17772 RVA: 0x0011D1E4 File Offset: 0x0011B3E4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Body
		{
			get
			{
				return this.DataObject.Body;
			}
			set
			{
				this.DataObject.Body = value;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x0011D1F2 File Offset: 0x0011B3F2
		// (set) Token: 0x0600456E RID: 17774 RVA: 0x0011D1FF File Offset: 0x0011B3FF
		[Parameter(Mandatory = false)]
		public MailBodyFormat BodyFormat
		{
			get
			{
				return this.DataObject.BodyFormat;
			}
			set
			{
				this.DataObject.BodyFormat = value;
			}
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x0011D20D File Offset: 0x0011B40D
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is AdUserNotFoundException;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x0011D224 File Offset: 0x0011B424
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId identity;
			if (!base.TryGetExecutingUserId(out identity))
			{
				throw new ExecutingUserPropertyNotFoundException("executingUserid");
			}
			this.mailboxUser = (ADUser)base.TenantGlobalCatalogSession.Read<ADUser>(identity);
			return new MailMessageConfigDataProvider(base.TenantGlobalCatalogSession, this.mailboxUser);
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x0011D270 File Offset: 0x0011B470
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			if (this.mailboxUser != null && this.mailboxUser.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new TaskException(Strings.ErrorCannotSendMailToPublicFolderMailbox(this.mailboxUser.Name)), ErrorCategory.InvalidArgument, this);
			}
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x0011D2C0 File Offset: 0x0011B4C0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (!base.HasErrors)
				{
					if (string.IsNullOrEmpty(this.Subject) && string.IsNullOrEmpty(this.Body))
					{
						this.WriteWarning(Strings.EmptyMesssageWillBeCreated);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x0011D320 File Offset: 0x0011B520
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailMessage(this.mailboxUser.Name);
			}
		}

		// Token: 0x04002ADB RID: 10971
		private ADUser mailboxUser;
	}
}
