using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000675 RID: 1653
	public class ToolBarSplitButton : ToolBarItem
	{
		// Token: 0x17002776 RID: 10102
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000D97C8 File Offset: 0x000D79C8
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x000D97D0 File Offset: 0x000D79D0
		public bool HideArrow { get; set; }

		// Token: 0x0600478E RID: 18318 RVA: 0x000D97DC File Offset: 0x000D79DC
		public ToolBarSplitButton(DropDownCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (command.Commands.Count == 0)
			{
				throw new ArgumentException("There must be at least one command in DropDownCommand.", "command");
			}
			this.command = command;
			this.CssClass += " SplitButton";
			this.contextMenu = new ContextMenu(this.command.Commands);
			this.contextMenu.CssClass = "hidden";
			if (!string.IsNullOrEmpty(this.command.Name))
			{
				this.ID = command.Name;
				this.contextMenu.ID = command.Name + "_DropDown";
			}
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x000D9898 File Offset: 0x000D7A98
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Table table = new Table();
			TableRow tableRow = new TableRow();
			Command defaultCommand = this.Command.GetDefaultCommand();
			int num = (defaultCommand == null) ? 1 : 2;
			bool flag = this.Command.Commands.Count >= num && this.Command.Commands.ContainsVisibleCommands();
			TableCell tableCell = new TableCell();
			bool flag2;
			if (defaultCommand == null)
			{
				flag2 = flag;
			}
			else
			{
				flag2 = (defaultCommand.SelectionMode == SelectionMode.SelectionIndependent);
			}
			tableCell.CssClass = (flag2 ? "EnabledButtonPart" : "DisabledButtonPart");
			this.toolBarButton = new ToolBarButton(this.Command);
			this.toolBarButton.CssClass = "SplitButtonButtonPart";
			tableCell.Controls.Add(this.toolBarButton);
			tableRow.Cells.Add(tableCell);
			if (!this.HideArrow)
			{
				TableCell tableCell2 = new TableCell();
				tableCell2.HorizontalAlign = HorizontalAlign.Center;
				CommandSprite commandSprite = new CommandSprite();
				commandSprite.AlternateText = Strings.More;
				if (!flag)
				{
					tableCell2.CssClass = "SplitButtonArrow DisabledSplitArrow";
					commandSprite.ImageId = CommandSprite.SpriteId.DisabledArrow;
				}
				else
				{
					tableCell2.CssClass = "SplitButtonArrow EnabledSplitArrow";
					commandSprite.ImageId = CommandSprite.SpriteId.EnabledArrow;
				}
				CommandSprite commandSprite2 = commandSprite;
				commandSprite2.CssClass += " SplitButtonImage";
				HyperLink hyperLink = new HyperLink();
				hyperLink.NavigateUrl = "#";
				hyperLink.CssClass = "ToolBarButtonLnk";
				hyperLink.Attributes.Add("role", "button");
				hyperLink.Attributes.Add("aria-haspopup", "true");
				hyperLink.ToolTip = Strings.More;
				hyperLink.Attributes.Add("onclick", "javascript:return false;");
				hyperLink.Controls.Add(commandSprite);
				EncodingLabel encodingLabel = new EncodingLabel();
				encodingLabel.Text = "▼";
				encodingLabel.CssClass = "SplitButtonImageAlter";
				hyperLink.Controls.Add(encodingLabel);
				tableCell2.Controls.Add(hyperLink);
				tableRow.Cells.Add(tableCell2);
			}
			table.Rows.Add(tableRow);
			table.Attributes.Add("role", "presentation");
			this.Controls.Add(table);
			this.Controls.Add(this.contextMenu);
		}

		// Token: 0x17002777 RID: 10103
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x000D9AEA File Offset: 0x000D7CEA
		public DropDownCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x000D9AF4 File Offset: 0x000D7CF4
		public override string ToJavaScript()
		{
			StringBuilder stringBuilder = new StringBuilder("new ToolBarSplitButton(");
			stringBuilder.Append(string.Format("\"{0}\"", this.Command.DefaultCommandName));
			if (this.contextMenu != null)
			{
				stringBuilder.Append(string.Format(",{0}", this.contextMenu.ToJavaScript()));
			}
			stringBuilder.Append(string.Format(",\"{0}\",{1},\"{2}\",{3},\"{4}\",{5},\"{6}\",{7},{8},\"{9}\",\"{10}\",\"{11}\",\"{12}\"", new object[]
			{
				this.Command.Name,
				this.Command.DefaultCommand.ToJavaScript(),
				this.Command.SelectionMode,
				string.IsNullOrEmpty(this.Command.OnClientClick) ? "null" : ("function($_){ return " + this.Command.OnClientClick + "($_)}"),
				this.Command.RefreshAction,
				string.IsNullOrEmpty(this.Command.Condition) ? "null" : ("function($_){ return " + this.Command.Condition + "}"),
				this.Command.GroupId,
				this.Command.HideOnDisable.ToJavaScript(),
				this.Command.Visible.ToJavaScript(),
				this.Command.ConfirmDialogType,
				HttpUtility.JavaScriptStringEncode(this.Command.SingleSelectionConfirmMessage),
				HttpUtility.JavaScriptStringEncode(this.Command.SelectionConfirmMessageDetail),
				HttpUtility.JavaScriptStringEncode(this.Command.MultiSelectionConfirmMessage)
			}));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04003017 RID: 12311
		private DropDownCommand command;

		// Token: 0x04003018 RID: 12312
		private ContextMenu contextMenu;

		// Token: 0x04003019 RID: 12313
		private ToolBarButton toolBarButton;
	}
}
