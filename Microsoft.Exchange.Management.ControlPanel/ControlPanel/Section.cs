using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200064F RID: 1615
	[ToolboxData("<{0}:Section runat=server></{0}:Section>")]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class Section : WebControl, INamingContainer
	{
		// Token: 0x06004672 RID: 18034 RVA: 0x000D5388 File Offset: 0x000D3588
		public Section() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "section";
		}

		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x000D53A4 File Offset: 0x000D35A4
		// (set) Token: 0x06004674 RID: 18036 RVA: 0x000D53AC File Offset: 0x000D35AC
		public string Title { get; set; }

		// Token: 0x17002727 RID: 10023
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x000D53B5 File Offset: 0x000D35B5
		// (set) Token: 0x06004676 RID: 18038 RVA: 0x000D53BD File Offset: 0x000D35BD
		public string WorkflowName { get; set; }

		// Token: 0x17002728 RID: 10024
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x000D53C6 File Offset: 0x000D35C6
		// (set) Token: 0x06004678 RID: 18040 RVA: 0x000D53CE File Offset: 0x000D35CE
		public string ClientVisibilityBinding { get; set; }

		// Token: 0x17002729 RID: 10025
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x000D53D7 File Offset: 0x000D35D7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected SectionContentPanel ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this.contentContainer;
			}
		}

		// Token: 0x1700272A RID: 10026
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x000D53E5 File Offset: 0x000D35E5
		// (set) Token: 0x0600467B RID: 18043 RVA: 0x000D53ED File Offset: 0x000D35ED
		[Description("Section Pane Content")]
		[TemplateContainer(typeof(SectionContentPanel))]
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate Content { get; set; }

		// Token: 0x1700272B RID: 10027
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x000D53F6 File Offset: 0x000D35F6
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x1700272C RID: 10028
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x000D5404 File Offset: 0x000D3604
		public IEnumerable<FormView> FormViews
		{
			get
			{
				List<FormView> list = new List<FormView>();
				this.GetFormViews(this.ContentContainer, list);
				return list;
			}
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x000D5425 File Offset: 0x000D3625
		protected override void OnPreRender(EventArgs e)
		{
			base.Attributes.Add("data-control", "Section");
			base.Attributes.Add("Workflow", this.WorkflowName ?? string.Empty);
			base.OnPreRender(e);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x000D5464 File Offset: 0x000D3664
		private void GetFormViews(Control root, List<FormView> formViews)
		{
			foreach (object obj in root.Controls)
			{
				Control control = (Control)obj;
				FormView formView = control as FormView;
				if (formView != null)
				{
					formViews.Add(formView);
				}
				else
				{
					this.GetFormViews(control, formViews);
				}
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x000D54D4 File Offset: 0x000D36D4
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			HtmlAnchor htmlAnchor = new HtmlAnchor();
			htmlAnchor.Name = this.ID;
			htmlAnchor.Attributes["class"] = "dspBlock";
			htmlAnchor.Attributes["tabindex"] = "-1";
			this.Controls.Add(htmlAnchor);
			this.contentContainer = new SectionContentPanel();
			this.contentContainer.ID = "contentContainer";
			this.contentContainer.CssClass = "secCnt";
			this.Controls.Add(this.contentContainer);
			if (this.Content != null)
			{
				this.Content.InstantiateIn(this.contentContainer);
			}
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x000D5589 File Offset: 0x000D3789
		public override Control FindControl(string id)
		{
			this.EnsureChildControls();
			return base.FindControl(id) ?? this.contentContainer.FindControl(id);
		}

		// Token: 0x1700272D RID: 10029
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000D55A8 File Offset: 0x000D37A8
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x000D55B0 File Offset: 0x000D37B0
		public override bool Enabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
			}
		}

		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x000D55B9 File Offset: 0x000D37B9
		// (set) Token: 0x06004685 RID: 18053 RVA: 0x000D55CB File Offset: 0x000D37CB
		public string SetRoles
		{
			get
			{
				return base.Attributes["SetRoles"];
			}
			set
			{
				base.Attributes.Add("SetRoles", value);
			}
		}

		// Token: 0x04002FA2 RID: 12194
		private const string Workflow = "Workflow";

		// Token: 0x04002FA3 RID: 12195
		private SectionContentPanel contentContainer;

		// Token: 0x04002FA4 RID: 12196
		private bool isEnabled = true;
	}
}
