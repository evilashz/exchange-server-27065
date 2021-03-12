using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000761 RID: 1889
	[Cmdlet("Enable", "ServiceEmailChannel", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class EnableServiceEmailChannel : RecipientObjectActionTask<MailboxIdParameter, ADRecipient>
	{
		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x0600432C RID: 17196 RVA: 0x00113C44 File Offset: 0x00111E44
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableServiceEmailChannel(this.Identity.ToString());
			}
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x00113C60 File Offset: 0x00111E60
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			ADUser aduser = this.DataObject as ADUser;
			if (aduser == null || (aduser.RecipientType != RecipientType.UserMailbox && aduser.RecipientType != RecipientType.MailUser))
			{
				base.WriteError(new RecipientTypeInvalidException(this.Identity.ToString()), ErrorCategory.InvalidData, aduser);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x00113CC0 File Offset: 0x00111EC0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalProcessRecord();
				if (!base.HasErrors)
				{
					ADUser user = this.DataObject as ADUser;
					ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(base.SessionSettings, user);
					using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=Enable-ServiceEmailChannel"))
					{
						using (Folder folder = Folder.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Root), StoreObjectType.Folder, "Service E-Mail", CreateMode.CreateNew))
						{
							folder.ClassName = "IPF.Note";
							folder.Save();
							folder.Load();
							mailboxSession.SetReceiveFolder("IPM.Note.Custom.ServiceEmail", folder.StoreObjectId);
						}
					}
				}
			}
			catch (StorageTransientException ex)
			{
				TaskLogger.LogError(ex);
				base.WriteError(ex, ErrorCategory.ReadError, this.DataObject);
			}
			catch (StoragePermanentException ex2)
			{
				TaskLogger.LogError(ex2);
				base.WriteError(ex2, ErrorCategory.InvalidOperation, this.DataObject);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}
	}
}
