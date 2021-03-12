using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005AD RID: 1453
	[ClientScriptResource("DockPanel", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class DockPanel : ScriptControlBase
	{
		// Token: 0x0600427F RID: 17023 RVA: 0x000CA67F File Offset: 0x000C887F
		public DockPanel() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000CA689 File Offset: 0x000C8889
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x000CA691 File Offset: 0x000C8891
		[Browsable(false)]
		[TemplateInstance(TemplateInstance.Single)]
		[DefaultValue(null)]
		[Description("Property Pane Content")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(PropertiesContentPanel))]
		public virtual ITemplate Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000CA69C File Offset: 0x000C889C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.Content != null)
			{
				this.contentContainer = new PropertiesContentPanel();
				this.contentContainer.ID = "contentContainer";
				this.contentContainer.Height = Unit.Percentage(100.0);
				this.Controls.Add(this.contentContainer);
				this.Content.InstantiateIn(this.contentContainer);
			}
		}

		// Token: 0x04002BB8 RID: 11192
		internal const string ContentContainerID = "contentContainer";

		// Token: 0x04002BB9 RID: 11193
		public const string DockAttribute = "dock";

		// Token: 0x04002BBA RID: 11194
		public const string Top = "top";

		// Token: 0x04002BBB RID: 11195
		public const string Bottom = "bottom";

		// Token: 0x04002BBC RID: 11196
		public const string Fill = "fill";

		// Token: 0x04002BBD RID: 11197
		private PropertiesContentPanel contentContainer;

		// Token: 0x04002BBE RID: 11198
		private ITemplate content;
	}
}
