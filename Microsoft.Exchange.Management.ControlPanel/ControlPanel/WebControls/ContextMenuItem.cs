using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000579 RID: 1401
	public class ContextMenuItem : MenuItem
	{
		// Token: 0x0600412A RID: 16682 RVA: 0x000C67D8 File Offset: 0x000C49D8
		public ContextMenuItem(Command command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this.Command = command;
			this.ImageId = command.ImageId;
			this.CssClass = "MenuItem";
			base.Attributes.Add("role", "menuitem");
			if (command.SelectionMode != SelectionMode.SelectionIndependent)
			{
				this.CssClass += " DisabledMenuItem";
			}
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000C684C File Offset: 0x000C4A4C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			HyperLink hyperLink = new HyperLink();
			hyperLink.NavigateUrl = this.NavigateUrl;
			hyperLink.Attributes.Add("onclick", this.OnClientClick);
			WebControl webControl = this.CreateImageControl();
			if (webControl != null)
			{
				hyperLink.Controls.Add(webControl);
			}
			else
			{
				this.CssClass += " MenuItemNoImage";
			}
			WebControl webControl2 = this.CreateContentControl();
			if (webControl2 != null)
			{
				hyperLink.Controls.Add(webControl2);
			}
			this.Controls.Add(hyperLink);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000C68D8 File Offset: 0x000C4AD8
		private WebControl CreateImageControl()
		{
			CommandSprite commandSprite = new CommandSprite();
			commandSprite.CssClass = "MenuItemImage";
			commandSprite.ImageId = this.ImageId;
			if (!string.IsNullOrEmpty(this.Command.ImageAltText))
			{
				commandSprite.AlternateText = this.Command.ImageAltText;
			}
			if (commandSprite.IsRenderable)
			{
				return commandSprite;
			}
			commandSprite.Dispose();
			return null;
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000C6938 File Offset: 0x000C4B38
		private WebControl CreateContentControl()
		{
			if (!string.IsNullOrEmpty(this.Command.Text))
			{
				return new EncodingLabel
				{
					CssClass = "MenuItemContent",
					Text = this.Command.Text
				};
			}
			return null;
		}

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x000C697C File Offset: 0x000C4B7C
		// (set) Token: 0x0600412F RID: 16687 RVA: 0x000C698D File Offset: 0x000C4B8D
		internal string OnClientClick
		{
			get
			{
				return this.onClientClick ?? "javascript:return false;";
			}
			set
			{
				this.onClientClick = value;
			}
		}

		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x000C6996 File Offset: 0x000C4B96
		// (set) Token: 0x06004131 RID: 16689 RVA: 0x000C69A7 File Offset: 0x000C4BA7
		internal string NavigateUrl
		{
			get
			{
				return this.navigateUrl ?? "#";
			}
			set
			{
				this.navigateUrl = value;
			}
		}

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000C69B0 File Offset: 0x000C4BB0
		// (set) Token: 0x06004133 RID: 16691 RVA: 0x000C69B8 File Offset: 0x000C4BB8
		public Command Command { get; set; }

		// Token: 0x06004134 RID: 16692 RVA: 0x000C69C1 File Offset: 0x000C4BC1
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.Command.PreRender(this);
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x000C69D6 File Offset: 0x000C4BD6
		// (set) Token: 0x06004136 RID: 16694 RVA: 0x000C69DE File Offset: 0x000C4BDE
		[Themeable(true)]
		public CommandSprite.SpriteId ImageId { get; set; }

		// Token: 0x06004137 RID: 16695 RVA: 0x000C69E8 File Offset: 0x000C4BE8
		public override string ToJavaScript()
		{
			StringBuilder stringBuilder = new StringBuilder("new ContextMenuItem(");
			stringBuilder.Append(string.Format("\"{0}\",{1},\"{2}\",\"{3}\",{4},\"{5}\",{6},\"{7}\",{8},{9},\"{10}\",\"{11}\",\"{12}\",\"{13}\",{14}", new object[]
			{
				this.Command.Name,
				this.Command.DefaultCommand.ToJavaScript(),
				this.Command.ShortCut,
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
				HttpUtility.JavaScriptStringEncode(this.Command.MultiSelectionConfirmMessage),
				this.Command.UseCustomConfirmDialog.ToJavaScript()
			}));
			PopupCommand popupCommand = this.Command as PopupCommand;
			if (popupCommand != null)
			{
				stringBuilder.Append(string.Format(",\"{0}\",{1},{2},{3},{4},{5},{6},{7},\"{8}\",\"{9}\",{10},{11},\"{12}\",\"{13}\"", new object[]
				{
					popupCommand.SelectionParameterName,
					popupCommand.UseDefaultWindow.ToJavaScript(),
					popupCommand.ShowAddressBar.ToJavaScript(),
					popupCommand.ShowMenuBar.ToJavaScript(),
					popupCommand.ShowStatusBar.ToJavaScript(),
					popupCommand.ShowToolBar.ToJavaScript(),
					popupCommand.Resizable.ToJavaScript(),
					popupCommand.SingleInstance.ToJavaScript(),
					HttpUtility.JavaScriptStringEncode(base.ResolveClientUrl(popupCommand.NavigateUrl)),
					popupCommand.TargetFrame,
					popupCommand.Position.X,
					popupCommand.Position.Y,
					popupCommand.DialogSize.Height,
					popupCommand.DialogSize.Width
				}));
			}
			else
			{
				stringBuilder.Append(",\"\",false,false,false,false,false,false,false,\"\",\"\",-1,-1,\"\",\"\"");
			}
			TaskCommand taskCommand = this.Command as TaskCommand;
			if (taskCommand != null)
			{
				stringBuilder.Append(string.Format(",\"{0}\",{1},\"{2}\",\"{3}\",\"{4}\"", new object[]
				{
					taskCommand.ActionName,
					taskCommand.IsLongRunning.ToJavaScript(),
					HttpUtility.JavaScriptStringEncode(taskCommand.InProgressDescription),
					HttpUtility.JavaScriptStringEncode(taskCommand.StoppedDescription),
					HttpUtility.JavaScriptStringEncode(taskCommand.CompletedDescription)
				}));
			}
			else
			{
				stringBuilder.Append(",\"\",false,\"\",\"\",\"\"");
			}
			stringBuilder.Append(string.Format(",{0},\"{1}\",\"{2}\",\"{3}\",\"{4}\"", new object[]
			{
				true.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(this.Command.Text),
				CommandSprite.GetCssClass(this.Command.ImageId),
				HttpUtility.JavaScriptStringEncode(this.Command.ImageAltText),
				HttpUtility.JavaScriptStringEncode(this.Command.Description)
			}));
			if (!string.IsNullOrEmpty(this.Command.ClientCommandHandler))
			{
				stringBuilder.Append(string.Format(", new {0}()", this.Command.ClientCommandHandler));
			}
			else
			{
				stringBuilder.Append(",\"\"");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04002B33 RID: 11059
		private string onClientClick;

		// Token: 0x04002B34 RID: 11060
		private string navigateUrl;
	}
}
