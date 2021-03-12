using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000673 RID: 1651
	public class ToolBarMoveButton : ToolBarButton
	{
		// Token: 0x06004789 RID: 18313 RVA: 0x000D95C4 File Offset: 0x000D77C4
		public ToolBarMoveButton(InlineSearchBarCommand command) : base(command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this.command = command;
			this.CssClass += " ToolBarButton";
			this.CssClass += " InlineSearchBarCommand";
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x000D961C File Offset: 0x000D781C
		public override string ToJavaScript()
		{
			return string.Format("new ToolBarMoveButton(\"{0}\",{1},\"{2}\",\"{3}\",{4},\"{5}\",{6},\"{7}\",{8},{9},{10},\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\")", new object[]
			{
				base.Command.Name,
				base.Command.DefaultCommand.ToJavaScript(),
				base.Command.ShortCut,
				base.Command.SelectionMode,
				string.IsNullOrEmpty(base.Command.OnClientClick) ? "null" : ("function($_){ return " + base.Command.OnClientClick + "($_)}"),
				base.Command.RefreshAction,
				string.IsNullOrEmpty(base.Command.Condition) ? "null" : ("function($_){ return " + base.Command.Condition + "}"),
				base.Command.GroupId,
				base.Command.HideOnDisable.ToJavaScript(),
				base.Command.Visible.ToJavaScript(),
				true.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(base.Command.Text),
				CommandSprite.GetCssClass(base.Command.ImageId),
				HttpUtility.JavaScriptStringEncode(base.Command.ImageAltText),
				HttpUtility.JavaScriptStringEncode(base.Command.Description),
				this.command.ControlIdToMove,
				this.command.MovedControlCss
			});
		}

		// Token: 0x04003016 RID: 12310
		private InlineSearchBarCommand command;
	}
}
