using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200065F RID: 1631
	internal class SlabFrame : WebControl, INamingContainer
	{
		// Token: 0x17002746 RID: 10054
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x000D64E6 File Offset: 0x000D46E6
		// (set) Token: 0x060046DB RID: 18139 RVA: 0x000D64EE File Offset: 0x000D46EE
		internal SlabControl Slab { get; set; }

		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x000D64F7 File Offset: 0x000D46F7
		// (set) Token: 0x060046DD RID: 18141 RVA: 0x000D64FF File Offset: 0x000D46FF
		private CaptionPanel CaptionPanel { get; set; }

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x000D6508 File Offset: 0x000D4708
		// (set) Token: 0x060046DF RID: 18143 RVA: 0x000D6515 File Offset: 0x000D4715
		internal bool ShowHelp
		{
			get
			{
				return this.CaptionPanel.ShowHelp;
			}
			set
			{
				this.CaptionPanel.ShowHelp = value;
			}
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x000D6523 File Offset: 0x000D4723
		// (set) Token: 0x060046E1 RID: 18145 RVA: 0x000D6530 File Offset: 0x000D4730
		internal bool PublishHelp
		{
			get
			{
				return this.CaptionPanel.AddHelpButton;
			}
			set
			{
				this.CaptionPanel.AddHelpButton = value;
			}
		}

		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x000D653E File Offset: 0x000D473E
		internal WebControl CaptionLabel
		{
			get
			{
				return this.CaptionPanel.TextLabel;
			}
		}

		// Token: 0x1700274B RID: 10059
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x000D654B File Offset: 0x000D474B
		internal string SaveButtonClientID
		{
			get
			{
				if (this.buttonsPanel == null)
				{
					return null;
				}
				return this.buttonsPanel.CommitButtonClientID;
			}
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000D6564 File Offset: 0x000D4764
		internal SlabFrame(SlabControl slab) : base(HtmlTextWriterTag.Div)
		{
			if (slab == null)
			{
				throw new ArgumentNullException("slab", "Slab cannot be null");
			}
			this.Slab = slab;
			this.CssClass = "slbFrm";
			if (this.Slab.HideSlabBorder)
			{
				this.CssClass = "slbFrm noBorderAlways";
			}
			else
			{
				this.CssClass = "slbFrm";
			}
			base.Style[HtmlTextWriterStyle.Visibility] = "hidden";
			this.CaptionPanel = new CaptionPanel();
			this.ShowHelp = false;
			this.PublishHelp = false;
			Unit height = slab.Height;
			if (!height.IsEmpty && height.Type == UnitType.Percentage)
			{
				base.Attributes.Add("fill", ((int)height.Value).ToString());
				return;
			}
			this.Height = slab.Height;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000D6638 File Offset: 0x000D4838
		protected override void OnInit(EventArgs e)
		{
			this.Controls.Clear();
			this.Controls.Add(this.CaptionPanel);
			Panel panel = new Panel();
			if (this.Slab.HideSlabBorder)
			{
				panel.CssClass = "cttPane noBorderAlways";
			}
			else
			{
				panel.CssClass = "cttPane";
			}
			panel.Controls.Add(this.Slab);
			this.Controls.Add(panel);
			this.CaptionPanel.HelpId = this.Slab.HelpId;
			this.CaptionPanel.Text = (string.IsNullOrWhiteSpace(this.Slab.Caption) ? this.Slab.Title : this.Slab.Caption);
			base.Attributes.Add("testid", this.Slab.HelpId);
			this.Page.PreRenderComplete += this.Page_PreRenderComplete;
			base.OnInit(e);
		}

		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x000D672D File Offset: 0x000D492D
		private bool HideCaption
		{
			get
			{
				return this.Slab.HideSlabBorder || (this.Slab.IsPrimarySlab && !((EcpContentPage)this.Page).ShowHelp);
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000D6760 File Offset: 0x000D4960
		private void Page_PreRenderComplete(object sender, EventArgs e)
		{
			if (this.HideCaption)
			{
				this.CaptionPanel.Style[HtmlTextWriterStyle.Display] = "none";
			}
			if (this.buttonsPanel != null)
			{
				this.buttonsPanel.Visible = false;
			}
		}

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x000D6795 File Offset: 0x000D4995
		internal IBaseFormContentControl PropertiesControl
		{
			get
			{
				return this.Slab.PropertiesControl;
			}
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x000D67A4 File Offset: 0x000D49A4
		internal void InitSaveButton()
		{
			if (this.PropertiesControl != null)
			{
				this.buttonsPanel = new ButtonsPanel();
				this.buttonsPanel.State = ButtonsPanelState.Save;
				this.Controls.Add(this.buttonsPanel);
				WebServiceMethod saveWebServiceMethod = this.PropertiesControl.SaveWebServiceMethod;
				if (saveWebServiceMethod != null)
				{
					this.buttonsPanel.SaveWebServiceMethods.Add(saveWebServiceMethod);
				}
			}
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x000D6801 File Offset: 0x000D4A01
		internal void ShowSaveButton()
		{
			if (this.buttonsPanel != null)
			{
				this.buttonsPanel.Visible = true;
				if (!this.Slab.UsePropertyPageStyle)
				{
					SlabFrame.SetFocusCssOnSaveButton(this.buttonsPanel);
				}
			}
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x000D682F File Offset: 0x000D4A2F
		internal static void SetFocusCssOnSaveButton(ButtonsPanel buttonsPanel)
		{
			((HtmlButton)buttonsPanel.FindControl("btnCommit")).Attributes.Add("class", "btnSave");
		}

		// Token: 0x04002FD2 RID: 12242
		private ButtonsPanel buttonsPanel;
	}
}
