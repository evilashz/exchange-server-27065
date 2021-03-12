using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E5 RID: 997
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class NonOwnerAccess : DataSourceService, INonOwnerAccess, IGetListService<NonOwnerAccessFilter, NonOwnerAccessResultRow>
	{
		// Token: 0x06003344 RID: 13124 RVA: 0x0009F18C File Offset: 0x0009D38C
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-MailboxAuditLog?ResultSize&StartDate&EndDate&Mailboxes&LogonTypes&ExternalAccess@R:Organization")]
		public PowerShellResults<NonOwnerAccessResultRow> GetList(NonOwnerAccessFilter filter, SortOptions sort)
		{
			string logonTypes = filter.LogonTypes;
			string logonTypes2;
			if ((logonTypes2 = filter.LogonTypes) != null)
			{
				if (!(logonTypes2 == "AllNonOwners"))
				{
					if (!(logonTypes2 == "OutsideUsers"))
					{
						if (!(logonTypes2 == "InternalUsers"))
						{
							if (logonTypes2 == "NonDelegateUsers")
							{
								filter.LogonTypes = "Admin";
								filter.ExternalAccess = new bool?(false);
							}
						}
						else
						{
							filter.LogonTypes = "Admin,Delegate";
							filter.ExternalAccess = new bool?(false);
						}
					}
					else
					{
						filter.LogonTypes = null;
						filter.ExternalAccess = new bool?(true);
					}
				}
				else
				{
					filter.LogonTypes = "Admin,Delegate";
				}
			}
			PowerShellResults<NonOwnerAccessResultRow> list = base.GetList<NonOwnerAccessResultRow, NonOwnerAccessFilter>("Search-MailboxAuditLog", filter, sort);
			if (list.Succeeded)
			{
				PowerShellResults<NonOwnerAccessResultRow> powerShellResults = new PowerShellResults<NonOwnerAccessResultRow>();
				int num = list.Output.Length;
				NonOwnerAccessResultRow[] array = new NonOwnerAccessResultRow[num];
				for (int i = 0; i < num; i++)
				{
					Identity id = AuditHelper.CreateNonOwnerAccessIdentity(list.Output[i].Mailbox, filter.StartDate, filter.EndDate, logonTypes);
					array[i] = new NonOwnerAccessResultRow(id, list.Output[i].NonOwnerAccessResult);
				}
				powerShellResults.Output = array;
				return powerShellResults;
			}
			return list;
		}

		// Token: 0x040024DC RID: 9436
		internal const string NoStart = "NoStart";

		// Token: 0x040024DD RID: 9437
		internal const string NoEnd = "NoEnd";

		// Token: 0x040024DE RID: 9438
		internal const string GetCmdlet = "Search-MailboxAuditLog";

		// Token: 0x040024DF RID: 9439
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040024E0 RID: 9440
		private const string GetListRole = "Search-MailboxAuditLog?ResultSize&StartDate&EndDate&Mailboxes&LogonTypes&ExternalAccess@R:Organization";
	}
}
