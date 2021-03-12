using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000479 RID: 1145
	public class CALWarningLabel : WebControl
	{
		// Token: 0x0600398F RID: 14735 RVA: 0x000AF09E File Offset: 0x000AD29E
		public CALWarningLabel() : base(HtmlTextWriterTag.Table)
		{
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000AF0A8 File Offset: 0x000AD2A8
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x000AF0B0 File Offset: 0x000AD2B0
		public string EnterpriseText { get; set; }

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000AF0B9 File Offset: 0x000AD2B9
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x000AF0C1 File Offset: 0x000AD2C1
		public string DatacenterText { get; set; }

		// Token: 0x06003994 RID: 14740 RVA: 0x000AF0CC File Offset: 0x000AD2CC
		protected override void CreateChildControls()
		{
			string text = null;
			string helpId = EACHelpId.LearnMoreCAL.ToString();
			if (RbacPrincipal.Current.IsInRole("Enterprise"))
			{
				text = this.EnterpriseText;
			}
			else if (RbacPrincipal.Current.IsInRole("LiveID"))
			{
				text = this.DatacenterText;
			}
			if (text == null)
			{
				this.Visible = false;
				base.CreateChildControls();
				return;
			}
			TableRow tableRow = new TableRow();
			tableRow.VerticalAlign = VerticalAlign.Top;
			this.Controls.Add(tableRow);
			TableCell tableCell = new TableCell();
			tableRow.Controls.Add(tableCell);
			CommonSprite commonSprite = new CommonSprite();
			commonSprite.ImageId = CommonSprite.SpriteId.Information;
			tableCell.Controls.Add(commonSprite);
			TableCell tableCell2 = new TableCell();
			tableRow.Controls.Add(tableCell2);
			HelpLink helpLink = new HelpLink();
			helpLink.Text = text;
			helpLink.TextIsFormatString = true;
			helpLink.HelpId = helpId;
			tableCell2.Controls.Add(helpLink);
			base.CreateChildControls();
		}
	}
}
