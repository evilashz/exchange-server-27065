using System;
using System.ServiceModel;
using System.Text;
using System.Web.Security.AntiXss;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003EE RID: 1006
	public class RoleGroupChangeDetailProperties : AuditLogChangeDetailProperties
	{
		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x0009FED2 File Offset: 0x0009E0D2
		protected override string DetailsPaneId
		{
			get
			{
				return "roleGroupChangeDetailsPane";
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x0009FED9 File Offset: 0x0009E0D9
		protected override string HeaderLabelId
		{
			get
			{
				return "lblRoleGroupChanged";
			}
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x0009FEE0 File Offset: 0x0009E0E0
		protected override string GetDetailsPaneHeader()
		{
			PowerShellResults<AdminAuditLogDetailRow> powerShellResults = base.Results as PowerShellResults<AdminAuditLogDetailRow>;
			if (powerShellResults != null && powerShellResults.Output != null && powerShellResults.Output.Length > 0 && powerShellResults.Succeeded)
			{
				return powerShellResults.Output[0].UserFriendlyObjectSelected;
			}
			return string.Empty;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0009FF2C File Offset: 0x0009E12C
		protected override void RenderChanges()
		{
			PowerShellResults<AdminAuditLogDetailRow> powerShellResults = base.Results as PowerShellResults<AdminAuditLogDetailRow>;
			if (powerShellResults != null && powerShellResults.Output != null && powerShellResults.Output.Length > 0 && powerShellResults.Succeeded)
			{
				Table table = new Table();
				int num = 0;
				while (num < powerShellResults.Output.Length && num < 500)
				{
					AdminAuditLogDetailRow adminAuditLogDetailRow = powerShellResults.Output[num];
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.AdminAuditLogEvent.RunDate.Value.ToUniversalTime().UtcToUserDateTimeString()));
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.UserFriendlyCaller));
					string text = null;
					string text2 = null;
					foreach (AdminAuditLogCmdletParameter adminAuditLogCmdletParameter in adminAuditLogDetailRow.AdminAuditLogEvent.CmdletParameters)
					{
						if (adminAuditLogCmdletParameter.Name.Equals("Members", StringComparison.InvariantCultureIgnoreCase) || adminAuditLogCmdletParameter.Name.Equals("Member", StringComparison.InvariantCultureIgnoreCase))
						{
							text = AuditHelper.MakeUserFriendly(adminAuditLogCmdletParameter.Value);
						}
						if (adminAuditLogCmdletParameter.Name.Equals("Roles", StringComparison.InvariantCultureIgnoreCase) || adminAuditLogCmdletParameter.Name.Equals("Role", StringComparison.InvariantCultureIgnoreCase))
						{
							text2 = adminAuditLogCmdletParameter.Value;
						}
					}
					if (adminAuditLogDetailRow.AdminAuditLogEvent.CmdletName.Equals("New-RoleGroup", StringComparison.InvariantCultureIgnoreCase) || adminAuditLogDetailRow.AdminAuditLogEvent.CmdletName.Equals("Remove-RoleGroup", StringComparison.InvariantCultureIgnoreCase))
					{
						table.Rows.Add(base.GetDetailRowForTable(this.GetLocalizedAction(adminAuditLogDetailRow.AdminAuditLogEvent.CmdletName)));
					}
					if (text != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						if (adminAuditLogDetailRow.AdminAuditLogEvent.CmdletName.Equals("Update-RoleGroupMember", StringComparison.InvariantCultureIgnoreCase))
						{
							stringBuilder.Append(Strings.UpdatedMembers);
						}
						else
						{
							stringBuilder.Append(Strings.AddedMembers);
						}
						stringBuilder.Append(AntiXssEncoder.HtmlEncode(text, false));
						table.Rows.Add(base.GetDetailRowForTable(stringBuilder.ToString()));
					}
					if (text2 != null)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.Append(Strings.AddedRoles);
						stringBuilder2.Append(text2);
						table.Rows.Add(base.GetDetailRowForTable(stringBuilder2.ToString()));
					}
					table.Rows.Add(base.GetEmptyRowForTable());
					num++;
				}
				if (table.Rows.Count > 0)
				{
					this.detailsPane.Controls.Add(table);
				}
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000A01D4 File Offset: 0x0009E3D4
		private string GetLocalizedAction(string cmdlet)
		{
			if (string.Equals(cmdlet, "Add-RoleGroupMember", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.AddedMembers;
			}
			if (string.Equals(cmdlet, "Remove-RoleGroupMember", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.RemovedMembers;
			}
			if (string.Equals(cmdlet, "Update-RoleGroupMember", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.UpdatedMembers;
			}
			if (string.Equals(cmdlet, "New-RoleGroup", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.CreatedRoleGroup;
			}
			if (string.Equals(cmdlet, "Remove-RoleGroup", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.RemovedRoleGroup;
			}
			throw new FaultException(new ArgumentException("RoleGroupValue").Message);
		}

		// Token: 0x040024EB RID: 9451
		private const string RoleGroupParameterMembers = "Members";

		// Token: 0x040024EC RID: 9452
		private const string RoleGroupParameterMember = "Member";

		// Token: 0x040024ED RID: 9453
		private const string RoleGroupParameterRoles = "Roles";

		// Token: 0x040024EE RID: 9454
		private const string RoleGroupParameterRole = "Role";

		// Token: 0x040024EF RID: 9455
		private const string AddRoleGroupMember = "Add-RoleGroupMember";

		// Token: 0x040024F0 RID: 9456
		private const string RemoveRoleGroupMember = "Remove-RoleGroupMember";

		// Token: 0x040024F1 RID: 9457
		private const string UpdateRoleGroupMember = "Update-RoleGroupMember";

		// Token: 0x040024F2 RID: 9458
		private const string NewRoleGroup = "New-RoleGroup";

		// Token: 0x040024F3 RID: 9459
		private const string RemoveRoleGroup = "Remove-RoleGroup";
	}
}
