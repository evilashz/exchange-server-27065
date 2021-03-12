using System;
using System.Security.Permissions;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000DB RID: 219
	public class MailboxPermissions : DataSourceService, IGetListService<GetMailboxPermissionParameters, MailboxPermissionsRow>
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x0005A744 File Offset: 0x00058944
		public static bool CanAccessMailboxOf(string smtpAddress)
		{
			if (!RbacPrincipal.Current.IsInRole(string.Format("{0}?Identity&User", "Get-MailboxPermission")))
			{
				return false;
			}
			if (string.Compare(smtpAddress, LocalSession.Current.ExecutingUserPrimarySmtpAddress.ToString(), true) == 0)
			{
				return true;
			}
			MailboxPermissions mailboxPermissions = new MailboxPermissions();
			PowerShellResults<MailboxPermissionsRow> list = mailboxPermissions.GetList(new GetMailboxPermissionParameters
			{
				Identity = new Identity(smtpAddress),
				User = Identity.FromExecutingUserId()
			}, null);
			return list.Output.Length == 1 && (list.Output[0].HasReadAccess || list.Output[0].HasFullAccess);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0005A7E8 File Offset: 0x000589E8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxPermission@C:OrganizationConfig")]
		public PowerShellResults<MailboxPermissionsRow> GetList(GetMailboxPermissionParameters filter, SortOptions sort)
		{
			return base.GetList<MailboxPermissionsRow, GetMailboxPermissionParameters>(filter.AssociatedCmdlet, filter, sort);
		}

		// Token: 0x04001BE7 RID: 7143
		private const string Noun = "MailboxPermission";

		// Token: 0x04001BE8 RID: 7144
		private const string GetCmdlet = "Get-MailboxPermission";

		// Token: 0x04001BE9 RID: 7145
		private const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x04001BEA RID: 7146
		private const string GetListRole = "Get-MailboxPermission@C:OrganizationConfig";
	}
}
