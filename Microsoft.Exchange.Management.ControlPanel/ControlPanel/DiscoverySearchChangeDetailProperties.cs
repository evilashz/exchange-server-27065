using System;
using System.ServiceModel;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C0 RID: 960
	public class DiscoverySearchChangeDetailProperties : AuditLogChangeDetailProperties
	{
		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x0009B62A File Offset: 0x0009982A
		protected override string DetailsPaneId
		{
			get
			{
				return "discoverySearchChangeDetailsPane";
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x0009B631 File Offset: 0x00099831
		protected override string HeaderLabelId
		{
			get
			{
				return "lblMailboxChanged";
			}
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x0009B638 File Offset: 0x00099838
		protected override string GetDetailsPaneHeader()
		{
			PowerShellResults<AdminAuditLogDetailRow> powerShellResults = base.Results as PowerShellResults<AdminAuditLogDetailRow>;
			if (powerShellResults != null && powerShellResults.Output != null && powerShellResults.Output.Length > 0 && powerShellResults.Succeeded)
			{
				return powerShellResults.Output[0].SearchObjectName;
			}
			return string.Empty;
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x0009B684 File Offset: 0x00099884
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
					string cmdletName = adminAuditLogDetailRow.AdminAuditLogEvent.CmdletName;
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.AdminAuditLogEvent.RunDate.Value.ToUniversalTime().UtcToUserDateTimeString()));
					table.Rows.Add(base.GetDetailRowForTable(adminAuditLogDetailRow.UserFriendlyCaller));
					table.Rows.Add(base.GetDetailRowForTable(this.GetLocalizedState(cmdletName, adminAuditLogDetailRow.AdminAuditLogEvent.CmdletParameters)));
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

		// Token: 0x0600320C RID: 12812 RVA: 0x0009B7B0 File Offset: 0x000999B0
		private string GetLocalizedState(string cmdlet, MultiValuedProperty<AdminAuditLogCmdletParameter> parameters)
		{
			if (cmdlet.Equals("New-MailboxSearch", StringComparison.InvariantCultureIgnoreCase))
			{
				string text = Strings.DiscoverySearchCreated;
				foreach (AdminAuditLogCmdletParameter adminAuditLogCmdletParameter in parameters)
				{
					if (adminAuditLogCmdletParameter.Name.Equals("InPlaceHoldEnabled", StringComparison.InvariantCultureIgnoreCase) && string.Equals(adminAuditLogCmdletParameter.Value, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						text = text + " " + Strings.DiscoverySearchWithHold;
						break;
					}
				}
				return text;
			}
			if (cmdlet.Equals("Get-MailboxSearch", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.DiscoverySearchRetrieved;
			}
			if (cmdlet.Equals("Remove-MailboxSearch", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.DiscoverySearchDeleted;
			}
			if (cmdlet.Equals("Start-MailboxSearch", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.DiscoverySearchStarted;
			}
			if (cmdlet.Equals("Stop-MailboxSearch", StringComparison.InvariantCultureIgnoreCase))
			{
				return Strings.DiscoverySearchStopped;
			}
			if (cmdlet.Equals("Set-MailboxSearch", StringComparison.InvariantCultureIgnoreCase))
			{
				foreach (AdminAuditLogCmdletParameter adminAuditLogCmdletParameter2 in parameters)
				{
					if (adminAuditLogCmdletParameter2.Name.Equals("InPlaceHoldEnabled", StringComparison.InvariantCultureIgnoreCase))
					{
						if (string.Equals(adminAuditLogCmdletParameter2.Value, "true", StringComparison.InvariantCultureIgnoreCase))
						{
							return Strings.DiscoverySearchHoldEnabled;
						}
						return Strings.DiscoverySearchHoldDisabled;
					}
				}
				return Strings.DiscoverySearchModified;
			}
			throw new FaultException(new ArgumentException("CmdletRun").Message);
		}
	}
}
