using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005AC RID: 1452
	[ClientScriptResource("DockElement", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class DockElement : ScriptControlBase
	{
		// Token: 0x06004270 RID: 17008 RVA: 0x000CA45C File Offset: 0x000C865C
		public DockElement() : base(HtmlTextWriterTag.Div)
		{
			this.Dock = DockStyle.Fill;
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000CA46D File Offset: 0x000C866D
		// (set) Token: 0x06004272 RID: 17010 RVA: 0x000CA475 File Offset: 0x000C8675
		[DefaultValue(DockStyle.Fill)]
		public DockStyle Dock { get; set; }

		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x000CA47E File Offset: 0x000C867E
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x000CA486 File Offset: 0x000C8686
		[DefaultValue(null)]
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate Content { get; set; }

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x000CA48F File Offset: 0x000C868F
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x000CA497 File Offset: 0x000C8697
		[DefaultValue(null)]
		public string Borders { get; set; }

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x000CA4A0 File Offset: 0x000C86A0
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x000CA4A8 File Offset: 0x000C86A8
		[DefaultValue(false)]
		public bool AutoSize { get; set; }

		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000CA4B1 File Offset: 0x000C86B1
		internal DockPanelBorderWidths BorderWidths
		{
			get
			{
				if (this.borderWidth == null)
				{
					this.borderWidth = new DockPanelBorderWidths(this.Borders);
				}
				return this.borderWidth;
			}
		}

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000CA4D2 File Offset: 0x000C86D2
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x000CA4DA File Offset: 0x000C86DA
		internal DockElement FillPanel { get; set; }

		// Token: 0x0600427C RID: 17020 RVA: 0x000CA4E4 File Offset: 0x000C86E4
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			DockElement dockElement = (this.Controls.Count > 0) ? (this.Controls[0] as DockElement) : null;
			if (dockElement != null)
			{
				descriptor.AddComponentProperty("ChildDockPanel", dockElement);
			}
			else if (this.FillPanel != null)
			{
				descriptor.AddComponentProperty("FillPanel", this.FillPanel);
			}
			if (this.Dock != DockStyle.Fill)
			{
				descriptor.AddProperty("Dock", this.Dock);
			}
			descriptor.AddProperty("AutoSize", this.AutoSize, true);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000CA578 File Offset: 0x000C8778
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.Content != null)
			{
				this.Content.InstantiateIn(this);
			}
			string text = "DockElement abs0 ";
			switch (this.Dock)
			{
			case DockStyle.Fill:
				this.Width = Unit.Empty;
				this.Height = Unit.Empty;
				break;
			case DockStyle.Top:
				text += "dpTop";
				this.Width = Unit.Empty;
				break;
			case DockStyle.Left:
				text += "dpLeft";
				this.Height = Unit.Empty;
				break;
			case DockStyle.Right:
				text += "dpRight";
				this.Height = Unit.Empty;
				break;
			case DockStyle.Bottom:
				text += "dpBottom";
				this.Width = Unit.Empty;
				break;
			}
			this.CssClass = this.CssClass + " " + text;
			base.Style.Add(HtmlTextWriterStyle.BorderWidth, this.BorderWidths.FormatCssString(RtlUtil.IsRtl));
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000CA675 File Offset: 0x000C8875
		public Control InternalFindControl(string id)
		{
			return this.FindControl(id, 0);
		}

		// Token: 0x04002BB2 RID: 11186
		private DockPanelBorderWidths borderWidth;
	}
}
