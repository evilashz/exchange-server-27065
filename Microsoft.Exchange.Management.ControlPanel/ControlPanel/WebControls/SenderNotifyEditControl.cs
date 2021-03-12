using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000655 RID: 1621
	[ClientScriptResource("SenderNotifyEditControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:SenderNotifyEditControl runat=server></{0}:SenderNotifyEditControl>")]
	public class SenderNotifyEditControl : ScriptControlBase
	{
		// Token: 0x060046AB RID: 18091 RVA: 0x000D5B27 File Offset: 0x000D3D27
		public SenderNotifyEditControl() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x000D5B31 File Offset: 0x000D3D31
		public string NotifySenderDropDownID
		{
			get
			{
				this.EnsureChildControls();
				return this.ddNotfySender.ClientID;
			}
		}

		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x060046AD RID: 18093 RVA: 0x000D5B44 File Offset: 0x000D3D44
		public string RejectMessageTextboxID
		{
			get
			{
				this.EnsureChildControls();
				return this.txtRejectMessage.ClientID;
			}
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000D5B57 File Offset: 0x000D3D57
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("SenderNotifyDropdownListID", this.ddNotfySender.ClientID);
			descriptor.AddProperty("RejectMessageTextboxID", this.txtRejectMessage.ClientID);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000D5B8C File Offset: 0x000D3D8C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.ddNotfySender = new EnumDropDownList();
			this.ddNotfySender.ID = "notifySenderdropdown";
			this.ddNotfySender.EnumType = typeof(NotifySenderType).AssemblyQualifiedName;
			this.ddNotfySender.Width = Unit.Percentage(100.0);
			this.txtRejectMessage = new TextBox();
			this.txtRejectMessage.ID = "rejectmessagetextbox";
			this.txtRejectMessage.TextMode = TextBoxMode.SingleLine;
			this.txtRejectMessage.Width = Unit.Percentage(100.0);
			this.txtRejectMessage.MaxLength = 128;
			Table table = new Table();
			table.Width = Unit.Pixel(375);
			table.CellPadding = 2;
			table.CellSpacing = 2;
			this.AddRow(table, new Label
			{
				Text = Strings.SenderNotfyTypeLabel,
				ID = string.Format("{0}_label", this.ddNotfySender.ID)
			});
			this.AddRow(table, this.ddNotfySender);
			this.AddRow(table, new Label
			{
				Text = Strings.SenderNotfyRejectLabel,
				ID = string.Format("{0}_label", this.txtRejectMessage.ID)
			});
			this.AddRow(table, this.txtRejectMessage);
			this.Controls.Add(table);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000D5CFC File Offset: 0x000D3EFC
		private void AddRow(Table table, Control control)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(control);
			tableRow.Controls.Add(tableCell);
			table.Rows.Add(tableRow);
		}

		// Token: 0x04002FB3 RID: 12211
		private EnumDropDownList ddNotfySender;

		// Token: 0x04002FB4 RID: 12212
		private TextBox txtRejectMessage;
	}
}
