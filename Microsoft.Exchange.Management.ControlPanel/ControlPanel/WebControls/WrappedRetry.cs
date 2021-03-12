using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200068A RID: 1674
	[ParseChildren(true)]
	[ToolboxData("<{0}:WrappedRetry  runat=server></{0}:WrappedRetry >")]
	[ClientScriptResource("WrappedRetry", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class WrappedRetry : SimpleRetry
	{
		// Token: 0x170027B4 RID: 10164
		// (get) Token: 0x0600484A RID: 18506 RVA: 0x000DBD19 File Offset: 0x000D9F19
		// (set) Token: 0x0600484B RID: 18507 RVA: 0x000DBD21 File Offset: 0x000D9F21
		[Description("WrappedRetry Content")]
		[TemplateContainer(typeof(PropertiesContentPanel))]
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate Content { get; set; }

		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x000DBD2A File Offset: 0x000D9F2A
		// (set) Token: 0x0600484D RID: 18509 RVA: 0x000DBD31 File Offset: 0x000D9F31
		public override string Description
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x000DBD38 File Offset: 0x000D9F38
		public override bool ApplyRbacRolesAndAddControls(WebControl parentControl, IPrincipal currentUser)
		{
			if (base.ApplyRbacRolesAndAddControls(parentControl, currentUser))
			{
				this.EnsureChildControls();
				parentControl.Controls.Add(this.contentContainer);
				Properties.ApplyRolesFilterRecursive(this.contentContainer, currentUser, null);
				return true;
			}
			return false;
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x000DBD6C File Offset: 0x000D9F6C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.Content != null)
			{
				this.contentContainer = new Panel();
				this.contentContainer.ID = "wrappedRetryContentContainer";
				this.contentContainer.Height = Unit.Percentage(100.0);
				this.contentContainer.Style.Add(HtmlTextWriterStyle.Display, "none");
				this.Controls.Add(this.contentContainer);
				this.Content.InstantiateIn(this.contentContainer);
			}
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x000DBDF4 File Offset: 0x000D9FF4
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("ContentElement", this.contentContainer.ClientID);
		}

		// Token: 0x04003080 RID: 12416
		private Panel contentContainer;
	}
}
