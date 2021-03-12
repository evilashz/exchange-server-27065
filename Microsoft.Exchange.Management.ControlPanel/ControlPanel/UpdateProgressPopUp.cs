using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D2 RID: 1746
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class UpdateProgressPopUp : UpdateProgress, IScriptControl, INamingContainer
	{
		// Token: 0x06004A16 RID: 18966 RVA: 0x000E2D10 File Offset: 0x000E0F10
		internal static UpdateProgressPopUp GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			UpdateProgressPopUp updateProgressPopUp = page.Items[typeof(UpdateProgressPopUp)] as UpdateProgressPopUp;
			if (updateProgressPopUp == null)
			{
				updateProgressPopUp = new UpdateProgressPopUp();
				page.Form.Controls.Add(updateProgressPopUp);
				page.Items[typeof(UpdateProgressPopUp)] = updateProgressPopUp;
			}
			return updateProgressPopUp;
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x000E2D78 File Offset: 0x000E0F78
		private UpdateProgressPopUp()
		{
			this.imgProgressIndicator = new Image();
			this.imgProgressIndicator.ID = "imgProgressIndicator";
			this.imgProgressIndicator.AlternateText = Strings.Processing;
			this.imgProgressIndicator.ToolTip = Strings.Processing;
			this.imgProgressIndicator.CssClass = "progressImg";
			this.lblProgressDescription = new EncodingLabel();
			this.lblProgressDescription.ID = "lblProgressDescription";
			this.lblProgressDescription.CssClass = "progressInfo";
			this.hiddenPanel = new Panel();
			this.hiddenPanel.ID = "hiddenPanel";
			this.hiddenPanel.Attributes.Add("style", "display:none;");
			this.modalPopupExtender = new ModalPopupExtender();
			this.modalPopupExtender.ID = "updateprogressmodalpopupextender";
			this.modalPopupExtender.BackgroundCssClass = "ModalDlgBackground";
			this.modalPopupExtender.TargetControlID = this.hiddenPanel.UniqueID;
			base.DisplayAfter = 0;
			base.ProgressTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(this.BuildProgressPanelContent));
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x000E2EA0 File Offset: 0x000E10A0
		private void BuildProgressPanelContent(Control target)
		{
			Table table = new Table();
			table.CssClass = "progress";
			table.CellSpacing = 0;
			table.CellPadding = 0;
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(this.imgProgressIndicator);
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Controls.Add(this.lblProgressDescription);
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			target.Controls.Add(table);
			target.Controls.Add(this.hiddenPanel);
			this.modalPopupExtender.PopupControlID = this.ClientID;
			target.Controls.Add(this.modalPopupExtender);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x000E2F68 File Offset: 0x000E1168
		private Control FindTargetControl(string controlID)
		{
			Control control = null;
			Control control2 = this;
			while (control == null && control2 != this.Page)
			{
				control2 = control2.NamingContainer;
				if (control2 == null)
				{
					break;
				}
				control = control2.FindControl(controlID);
			}
			return control;
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x000E2F9A File Offset: 0x000E119A
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.modalPopupExtender.BehaviorID = this.modalPopupExtender.ClientID + "_behavior";
			this.imgProgressIndicator.ImageUrl = ThemeResource.GetThemeResource(this, "progress.gif");
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x000E2FDC File Offset: 0x000E11DC
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("UpdateProgressPopUp", this.ClientID);
			scriptControlDescriptor.AddProperty("DisplayAfter", base.DisplayAfter);
			scriptControlDescriptor.AddElementProperty("ProcessingTextLabel", this.lblProgressDescription.ClientID);
			scriptControlDescriptor.AddProperty("PopupBehaviorID", this.modalPopupExtender.BehaviorID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x000E3048 File Offset: 0x000E1248
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(UpdateProgressPopUp));
		}

		// Token: 0x0400318D RID: 12685
		internal const int DefaultDisplayProgressAfter = 0;

		// Token: 0x0400318E RID: 12686
		private Image imgProgressIndicator;

		// Token: 0x0400318F RID: 12687
		private EncodingLabel lblProgressDescription;

		// Token: 0x04003190 RID: 12688
		private Panel hiddenPanel;

		// Token: 0x04003191 RID: 12689
		private ModalPopupExtender modalPopupExtender;
	}
}
