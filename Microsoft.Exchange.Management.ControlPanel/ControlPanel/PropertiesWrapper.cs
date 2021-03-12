using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A3 RID: 675
	[ClientScriptResource("PropertiesWrapper", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class PropertiesWrapper : ScriptControlBase
	{
		// Token: 0x06002B87 RID: 11143 RVA: 0x00087E98 File Offset: 0x00086098
		public PropertiesWrapper()
		{
			this.properties = new Properties();
			this.Properties.HasSaveMethod = true;
			this.toolBar = new ToolBar();
			this.toolBar.RightAlign = this.IsToolbarRightAlign;
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x00087EE4 File Offset: 0x000860E4
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Style[HtmlTextWriterStyle.Visibility] = "hidden";
			base.EnsureID();
			this.EnsureChildControls();
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x00087F0B File Offset: 0x0008610B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00087F10 File Offset: 0x00086110
		protected override void CreateChildControls()
		{
			this.Height = Unit.Percentage(100.0);
			this.dockPanel = new DockPanel();
			this.dockPanel.Height = Unit.Percentage(100.0);
			this.Controls.Add(this.dockPanel);
			this.contentContainer = new PropertiesContentPanel();
			this.contentContainer.ID = "contentContainer";
			this.contentContainer.Height = Unit.Percentage(100.0);
			this.contentContainer.CssClass = " propertyWrpCttPane";
			this.dockPanel.Controls.Add(this.contentContainer);
			this.propertiesPanel = new Panel();
			this.propertiesPanel.Attributes.Add("dock", "fill");
			this.propertiesPanel.CssClass = (this.ReserveSpaceForFVA ? "reservedSpaceForFVA propertyWrapperProperties" : " propertyWrapperProperties");
			this.propertiesPanel.Controls.Add(this.Properties);
			this.contentContainer.Controls.Add(this.propertiesPanel);
			Panel panel = new Panel();
			panel.CssClass = "btnPane";
			panel.Attributes.Add("dock", "bottom");
			panel.Controls.Add(this.ToolBar);
			this.ToolBar.OwnerControlID = this.ClientID;
			this.contentContainer.Controls.Add(panel);
			base.CreateChildControls();
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0008808F File Offset: 0x0008628F
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("PropertiesID", this.Properties.ClientID);
			descriptor.AddComponentProperty("ToolBar", this.ToolBar.ClientID);
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000880C4 File Offset: 0x000862C4
		protected virtual bool IsToolbarRightAlign
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000880C7 File Offset: 0x000862C7
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x000880CF File Offset: 0x000862CF
		public bool ReserveSpaceForFVA { get; set; }

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000880D8 File Offset: 0x000862D8
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x000880E0 File Offset: 0x000862E0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Properties Properties
		{
			get
			{
				return this.properties;
			}
			private set
			{
				this.properties = value;
			}
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000880E9 File Offset: 0x000862E9
		// (set) Token: 0x06002B92 RID: 11154 RVA: 0x000880F1 File Offset: 0x000862F1
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ToolBar ToolBar
		{
			get
			{
				return this.toolBar;
			}
			private set
			{
				this.toolBar = value;
			}
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000880FA File Offset: 0x000862FA
		internal Panel ContentPanel
		{
			get
			{
				return this.propertiesPanel;
			}
		}

		// Token: 0x040021AC RID: 8620
		private DockPanel dockPanel;

		// Token: 0x040021AD RID: 8621
		private PropertiesContentPanel contentContainer;

		// Token: 0x040021AE RID: 8622
		private Panel propertiesPanel;

		// Token: 0x040021AF RID: 8623
		private Properties properties;

		// Token: 0x040021B0 RID: 8624
		private ToolBar toolBar;
	}
}
