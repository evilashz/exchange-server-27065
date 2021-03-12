using System;
using System.ServiceModel;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D2 RID: 978
	public class LitigationHoldChangeDetailProperties : AuditLogChangeDetailProperties
	{
		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x0009C5A3 File Offset: 0x0009A7A3
		protected override string DetailsPaneId
		{
			get
			{
				return "litigationHoldChangeDetailsPane";
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x0009C5AA File Offset: 0x0009A7AA
		protected override string HeaderLabelId
		{
			get
			{
				return "lblMailboxChanged";
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0009C5B4 File Offset: 0x0009A7B4
		protected override string GetDetailsPaneHeader()
		{
			PowerShellResults<AdminAuditLogDetailRow> powerShellResults = base.Results as PowerShellResults<AdminAuditLogDetailRow>;
			if (powerShellResults != null && powerShellResults.Output != null && powerShellResults.Output.Length > 0 && powerShellResults.Succeeded)
			{
				return powerShellResults.Output[0].UserFriendlyObjectSelected;
			}
			return string.Empty;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0009C600 File Offset: 0x0009A800
		protected override void RenderChanges()
		{
			PowerShellResults<AdminAuditLogDetailRow> powerShellResults = base.Results as PowerShellResults<AdminAuditLogDetailRow>;
			if (powerShellResults != null && powerShellResults.Output != null && powerShellResults.Succeeded && powerShellResults.Output.Length > 0)
			{
				Table table = new Table();
				int num = 0;
				while (num < powerShellResults.Output.Length && num < 500)
				{
					AdminAuditLogDetailRow adminAuditLogDetailRow = powerShellResults.Output[num];
					foreach (AdminAuditLogCmdletParameter adminAuditLogCmdletParameter in adminAuditLogDetailRow.AdminAuditLogEvent.CmdletParameters)
					{
						if (adminAuditLogCmdletParameter.Name.Equals("LitigationHoldEnabled", StringComparison.InvariantCultureIgnoreCase))
						{
							table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.AdminAuditLogEvent.RunDate.Value.ToUniversalTime().UtcToUserDateTimeString()));
							table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.UserFriendlyCaller));
							table.Rows.Add(base.GetDetailRowForTable(this.GetLocalizedState(adminAuditLogCmdletParameter.Value)));
							table.Rows.Add(base.GetEmptyRowForTable());
							break;
						}
					}
					num++;
				}
				if (table.Rows.Count > 0)
				{
					this.detailsPane.Controls.Add(table);
					return;
				}
				table.Dispose();
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x0009C778 File Offset: 0x0009A978
		private string GetLocalizedState(string value)
		{
			if (string.Equals(value, "true", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.LitigationHoldEnabled;
			}
			if (string.Equals(value, "false", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.LitigationHoldDisabled;
			}
			throw new FaultException(new ArgumentException("LitigationHoldValue").Message);
		}
	}
}
