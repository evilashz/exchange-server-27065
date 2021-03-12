using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B1 RID: 1457
	[ClientScriptResource("DropDownButton", "Microsoft.Exchange.Management.ControlPanel.Client.Navigation.js")]
	[ToolboxData("<{0}:DropDownButton runat=\"server\" />")]
	public class DropDownButton : ScriptControlBase
	{
		// Token: 0x0600429A RID: 17050 RVA: 0x000CAC20 File Offset: 0x000C8E20
		public DropDownButton(HtmlTextWriterTag tag = HtmlTextWriterTag.Span) : base(tag)
		{
			this.DropDownCommand = new DropDownCommand();
		}

		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x000CAC34 File Offset: 0x000C8E34
		// (set) Token: 0x0600429C RID: 17052 RVA: 0x000CAC3C File Offset: 0x000C8E3C
		public DropDownCommand DropDownCommand { get; private set; }

		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x000CAC45 File Offset: 0x000C8E45
		// (set) Token: 0x0600429E RID: 17054 RVA: 0x000CAC4D File Offset: 0x000C8E4D
		public bool AlignToRight { get; set; }

		// Token: 0x0600429F RID: 17055 RVA: 0x000CAC56 File Offset: 0x000C8E56
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ToolBar", this.toolBar.ClientID, true);
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000CAC78 File Offset: 0x000C8E78
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.toolBar = new ToolBar();
			this.toolBar.ID = "toolbar";
			this.toolBar.Commands.Add(this.GetFinalCommand());
			this.Controls.Add(this.toolBar);
			if (this.AlignToRight)
			{
				if (RtlUtil.IsRtl)
				{
					ToolBar toolBar = this.toolBar;
					toolBar.CssClass += " ToolBarRightAlign";
				}
				ToolBar toolBar2 = this.toolBar;
				toolBar2.CssClass += " floatRight";
				return;
			}
			ToolBar toolBar3 = this.toolBar;
			toolBar3.CssClass += " floatLeft";
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000CAD30 File Offset: 0x000C8F30
		private Command GetFinalCommand()
		{
			Command result = this.DropDownCommand;
			if (this.DropDownCommand.Commands.Count == 0)
			{
				result = new Command
				{
					Name = this.DropDownCommand.Name,
					Text = this.DropDownCommand.Text,
					ImageId = this.DropDownCommand.ImageId,
					ImageAltText = this.DropDownCommand.ImageAltText,
					OnClientClick = ";"
				};
			}
			return result;
		}

		// Token: 0x04002BC5 RID: 11205
		private ToolBar toolBar;
	}
}
