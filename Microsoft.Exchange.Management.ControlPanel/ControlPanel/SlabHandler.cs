using System;
using System.Configuration;
using System.Web;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200020D RID: 525
	public class SlabHandler : EcpContentPage
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x0007950D File Offset: 0x0007770D
		public SlabHandler()
		{
			base.AppRelativeVirtualPath = "~/SlabHandler.aspx";
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00079520 File Offset: 0x00077720
		public override void ProcessRequest(HttpContext context)
		{
			string virtualPath = context.Request.Path.Replace(".slab", ".ascx");
			this.slab = (BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(SlabControl)) as SlabControl);
			base.ProcessRequest(context);
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0007956C File Offset: 0x0007776C
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			this.MasterPageFile = "~/CommonMaster.Master";
			this.EnableViewState = SlabHandler.pagesSection.EnableViewState;
			base.EnableViewStateMac = SlabHandler.pagesSection.EnableViewStateMac;
			this.EnableEventValidation = SlabHandler.pagesSection.EnableEventValidation;
			this.Theme = SlabHandler.pagesSection.Theme;
			this.InitializeCulture();
			base.AddContentTemplate("ResultPanePlaceHolder", new CompiledTemplateBuilder(new BuildTemplateMethod(this.BuildControl)));
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000795EC File Offset: 0x000777EC
		private void BuildControl(Control ctrl)
		{
			if (this.slab != null)
			{
				this.slab.InitializeAsUserControl(this);
				if (this.slab.LayoutOnly)
				{
					throw new NotSupportedException("LayoutOnly slab control cannot be initialized as an independent slab.");
				}
				base.Title = this.slab.Title;
				SlabTable slabTable = new SlabTable();
				slabTable.HelpId = this.slab.HelpId;
				slabTable.IsSingleSlabPage = true;
				SlabColumn slabColumn = new SlabColumn();
				slabColumn.Slabs.Add(this.slab);
				slabTable.Components.Add(slabColumn);
				base.FeatureSet = this.slab.FeatureSet;
				ctrl.Controls.Add(slabTable);
			}
		}

		// Token: 0x04001FA7 RID: 8103
		private static readonly PagesSection pagesSection = (PagesSection)ConfigurationManager.GetSection("system.web/pages");

		// Token: 0x04001FA8 RID: 8104
		private SlabControl slab;
	}
}
