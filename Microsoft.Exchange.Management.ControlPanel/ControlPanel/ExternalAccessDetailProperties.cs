using System;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D0 RID: 976
	public class ExternalAccessDetailProperties : AuditLogChangeDetailProperties
	{
		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x0009C198 File Offset: 0x0009A398
		protected override string DetailsPaneId
		{
			get
			{
				return "ExternalAccessDetailsPane";
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x0009C19F File Offset: 0x0009A39F
		protected override string HeaderLabelId
		{
			get
			{
				return "lblExternalAccess";
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x0009C1A6 File Offset: 0x0009A3A6
		protected override string GetDetailsPaneHeader()
		{
			return string.Empty;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x0009C1B0 File Offset: 0x0009A3B0
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
					table.Rows.Add(base.GetDetailRowForTable(string.Format("<b>{0}</b>", Strings.AuditLogDateSDO)));
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.AdminAuditLogEvent.RunDate.Value.ToUniversalTime().UtcToUserDateTimeString()));
					table.Rows.Add(base.GetEmptyRowForTable());
					table.Rows.Add(base.GetDetailRowForTable(string.Format("<b>{0}</b>", Strings.AuditLogUserSDO)));
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.UserFriendlyCaller));
					table.Rows.Add(base.GetEmptyRowForTable());
					table.Rows.Add(base.GetDetailRowForTable(string.Format("<b>{0}</b>", Strings.AuditLogCmdletSDO)));
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.AdminAuditLogEvent.CmdletName));
					table.Rows.Add(base.GetEmptyRowForTable());
					table.Rows.Add(base.GetDetailRowForTable(string.Format("<b>{0}</b>", Strings.AuditLogCmdletParameters)));
					table.Rows.Add(base.GetDetailRowForTable(this.FormatParameters(adminAuditLogDetailRow.AdminAuditLogEvent.CmdletParameters)));
					table.Rows.Add(base.GetEmptyRowForTable());
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

		// Token: 0x06003249 RID: 12873 RVA: 0x0009C3B8 File Offset: 0x0009A5B8
		private string FormatParameters(MultiValuedProperty<AdminAuditLogCmdletParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AdminAuditLogCmdletParameter adminAuditLogCmdletParameter in parameters)
			{
				stringBuilder.AppendFormat("{0} : {1}", adminAuditLogCmdletParameter.Name, adminAuditLogCmdletParameter.Value);
				stringBuilder.Append("<br>");
			}
			return stringBuilder.ToString();
		}
	}
}
