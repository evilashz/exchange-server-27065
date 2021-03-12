using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F2 RID: 754
	public sealed class SoftDeletedAccounts : Accounts, ISoftDeletedAccounts, IAccounts, IDataSourceService<MailboxFilter, MailboxRecipientRow, Account, SetAccount, NewAccount, RemoveAccount>, IEditListService<MailboxFilter, MailboxRecipientRow, Account, NewAccount, RemoveAccount>, IGetListService<MailboxFilter, MailboxRecipientRow>, INewObjectService<MailboxRecipientRow, NewAccount>, IRemoveObjectsService<RemoveAccount>, IEditObjectForListService<Account, SetAccount, MailboxRecipientRow>, IGetObjectService<Account>, IGetObjectForListService<MailboxRecipientRow>
	{
		// Token: 0x06002D37 RID: 11575 RVA: 0x0008A8A8 File Offset: 0x00088AA8
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-Mailbox?Identity@R:Organization")]
		public override PowerShellResults<RecoverAccount> GetObjectForNew(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Get-Mailbox");
			pscommand.AddParameter("SoftDeletedmailbox");
			if (!(identity == null))
			{
				return base.GetObject<RecoverAccount>(pscommand, identity);
			}
			return new PowerShellResults<RecoverAccount>();
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0008A8E8 File Offset: 0x00088AE8
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-Mailbox?DisplayName&Password&Name&MicrosoftOnlineServicesID@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Undo-SoftDeletedMailbox@W:Organization")]
		public override PowerShellResults<MailboxRecipientRow> NewObject(NewAccount properties)
		{
			PowerShellResults<MailboxRecipientRow> powerShellResults = base.NewObject<MailboxRecipientRow, NewAccount>("Undo-SoftDeletedMailbox", properties);
			if (powerShellResults.SucceededWithValue)
			{
				if (powerShellResults.HasWarnings)
				{
					LocalizedString warningUnlicensedMailbox = Strings.WarningUnlicensedMailbox;
					if (powerShellResults.Warnings.Contains(warningUnlicensedMailbox))
					{
						List<string> list = powerShellResults.Warnings.ToList<string>();
						list.Remove(warningUnlicensedMailbox);
						powerShellResults.Warnings = list.ToArray();
					}
				}
				PowerShellResults<MailboxRecipientRow> objectForList = base.GetObjectForList(powerShellResults.Value.Identity);
				if (objectForList != null)
				{
					powerShellResults.Output = objectForList.Output;
				}
			}
			return powerShellResults;
		}

		// Token: 0x04002246 RID: 8774
		private const string GetSoftDeletedObjectForNewRole_MultiTenant = "MultiTenant+Get-Mailbox?Identity@R:Organization";

		// Token: 0x04002247 RID: 8775
		private const string UndoSoftDeletedMailboxRole_WLID = "MultiTenant+Undo-SoftDeletedMailbox@W:Organization";
	}
}
