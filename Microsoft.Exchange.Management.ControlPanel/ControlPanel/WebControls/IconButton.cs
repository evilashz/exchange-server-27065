using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005F2 RID: 1522
	[ToolboxData("<{0}:IconButton runat=\"server\" />")]
	public class IconButton : HtmlButton
	{
		// Token: 0x06004441 RID: 17473 RVA: 0x000CE4BC File Offset: 0x000CC6BC
		public IconButton()
		{
			this.cmdSprtIcon = new CommandSprite();
			this.lblText = new Label();
			this.CssClass = (Util.IsIE() ? "btn" : "btn btnFF");
			this.SetAttribute("type", "button");
			this.SetAttribute("data-TextTagName", "span");
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000CE51E File Offset: 0x000CC71E
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Add(this.cmdSprtIcon);
			this.Controls.Add(this.lblText);
		}

		// Token: 0x1700265F RID: 9823
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x000CE548 File Offset: 0x000CC748
		public CommandSprite CommandSprite
		{
			get
			{
				return this.cmdSprtIcon;
			}
		}

		// Token: 0x17002660 RID: 9824
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x000CE550 File Offset: 0x000CC750
		public Label Label
		{
			get
			{
				return this.lblText;
			}
		}

		// Token: 0x17002661 RID: 9825
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x000CE558 File Offset: 0x000CC758
		// (set) Token: 0x06004446 RID: 17478 RVA: 0x000CE565 File Offset: 0x000CC765
		public CommandSprite.SpriteId ImageId
		{
			get
			{
				return this.cmdSprtIcon.ImageId;
			}
			set
			{
				this.cmdSprtIcon.ImageId = value;
			}
		}

		// Token: 0x17002662 RID: 9826
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x000CE573 File Offset: 0x000CC773
		// (set) Token: 0x06004448 RID: 17480 RVA: 0x000CE580 File Offset: 0x000CC780
		public string Text
		{
			get
			{
				return this.lblText.Text;
			}
			set
			{
				this.lblText.Text = value;
			}
		}

		// Token: 0x17002663 RID: 9827
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x000CE58E File Offset: 0x000CC78E
		// (set) Token: 0x0600444A RID: 17482 RVA: 0x000CE5A0 File Offset: 0x000CC7A0
		public string CssClass
		{
			get
			{
				return base.Attributes["class"];
			}
			set
			{
				base.Attributes["class"] = value;
			}
		}

		// Token: 0x17002664 RID: 9828
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x000CE5B3 File Offset: 0x000CC7B3
		// (set) Token: 0x0600444C RID: 17484 RVA: 0x000CE5BB File Offset: 0x000CC7BB
		public override string InnerHtml
		{
			get
			{
				return base.InnerHtml;
			}
			set
			{
				throw new NotSupportedException("Setting InnerHtml is not supported for IconButton. Please use the Text property instead.");
			}
		}

		// Token: 0x17002665 RID: 9829
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x000CE5C7 File Offset: 0x000CC7C7
		// (set) Token: 0x0600444E RID: 17486 RVA: 0x000CE5CF File Offset: 0x000CC7CF
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				throw new NotSupportedException("Setting InnerText is not supported for IconButton. Please use the Text property instead.");
			}
		}

		// Token: 0x04002DE3 RID: 11747
		private CommandSprite cmdSprtIcon;

		// Token: 0x04002DE4 RID: 11748
		private Label lblText;
	}
}
