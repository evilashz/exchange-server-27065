using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000264 RID: 612
	public sealed class EditRetentionPolicyTagProperties : PropertyPageSheet
	{
		// Token: 0x0600293D RID: 10557 RVA: 0x0008199C File Offset: 0x0007FB9C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.InitializeControls();
			PowerShellResults<JsonDictionary<object>> powerShellResults = (PowerShellResults<JsonDictionary<object>>)base["PreLoadResults"];
			if (powerShellResults != null && powerShellResults.SucceededWithValue)
			{
				Dictionary<string, object> dictionary = powerShellResults.Value;
				bool includeArchive = false;
				if ((string)dictionary["Type"] == ElcFolderType.All.ToString() || (string)dictionary["Type"] == ElcFolderType.Personal.ToString())
				{
					this.divType.Visible = false;
					includeArchive = true;
				}
				RetentionUtils.PopulateRetentionActions(this.rblRetentionAction, includeArchive);
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00081A40 File Offset: 0x0007FC40
		private void InitializeControls()
		{
			Section section = base.Sections["GroupInformationSection"];
			this.divType = (HtmlControl)section.FindControl("divType");
			this.rblRetentionAction = (RadioButtonList)section.FindControl("rblRetentionAction");
		}

		// Token: 0x040020BE RID: 8382
		private const string SectionID = "GroupInformationSection";

		// Token: 0x040020BF RID: 8383
		private const string TypePanelID = "divType";

		// Token: 0x040020C0 RID: 8384
		private const string RetentionActionID = "rblRetentionAction";

		// Token: 0x040020C1 RID: 8385
		private HtmlControl divType;

		// Token: 0x040020C2 RID: 8386
		private RadioButtonList rblRetentionAction;
	}
}
