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
	// Token: 0x02000762 RID: 1890
	[Cmdlet("Disable", "ServiceEmailChannel", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableServiceEmailChannel : RecipientObjectActionTask<MailboxIdParameter, ADRecipient>
	{
		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x00113DE8 File Offset: 0x00111FE8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableServiceEmailChannel(this.Identity.ToString());
			}
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x00113E04 File Offset: 0x00112004
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

		// Token: 0x06004333 RID: 17203 RVA: 0x00113E64 File Offset: 0x00112064
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
					using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=Disable-ServiceEmailChannel"))
					{
						using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.Root))
						{
							StoreObjectId receiveFolder = mailboxSession.GetReceiveFolder("IPM.Note.Custom.ServiceEmail");
							mailboxSession.ClearReceiveFolder("IPM.Note.Custom.ServiceEmail");
							StoreObjectId receiveFolder2 = mailboxSession.GetReceiveFolder("IPM.Note.Custom.ServiceEmail");
							if (receiveFolder != null && (receiveFolder2 == null || !receiveFolder2.Equals(receiveFolder)))
							{
								using (Folder folder2 = Folder.Bind(folder.Session, receiveFolder))
								{
									if ((string)folder2[StoreObjectSchema.DisplayName] != null && (string)folder2[StoreObjectSchema.DisplayName] == "Service E-Mail")
									{
										folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
										{
											receiveFolder
										});
									}
								}
							}
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
