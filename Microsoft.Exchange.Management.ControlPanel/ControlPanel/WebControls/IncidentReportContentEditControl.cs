using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005F1 RID: 1521
	[ToolboxData("<{0}:IncidentReportContentEditControl runat=server></{0}:IncidentReportContentEditControl>")]
	[ClientScriptResource("IncidentReportContentEditControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class IncidentReportContentEditControl : ScriptControlBase
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x000CE3C5 File Offset: 0x000CC5C5
		public IncidentReportContentEditControl() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700265E RID: 9822
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x000CE3CF File Offset: 0x000CC5CF
		public string CheckboxListControlID
		{
			get
			{
				this.EnsureChildControls();
				return this.checkBoxList.ClientID;
			}
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000CE3E2 File Offset: 0x000CC5E2
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("CheckboxListID", this.CheckboxListControlID);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x000CE428 File Offset: 0x000CC628
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.checkBoxList = new EnumCheckBoxList();
			this.checkBoxList.ID = "checklist";
			this.checkBoxList.Items.AddRange((from e in Enum.GetNames(typeof(IncidentReportContent))
			select new ListItem(LocalizedDescriptionAttribute.FromEnum(typeof(IncidentReportContent), Enum.Parse(typeof(IncidentReportContent), e)), e.ToString())).ToArray<ListItem>());
			this.checkBoxList.CellSpacing = 2;
			this.Controls.Add(this.checkBoxList);
		}

		// Token: 0x04002DE1 RID: 11745
		private EnumCheckBoxList checkBoxList;
	}
}
