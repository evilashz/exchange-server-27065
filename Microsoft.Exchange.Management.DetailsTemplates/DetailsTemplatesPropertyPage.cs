using System;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200000F RID: 15
	internal class DetailsTemplatesPropertyPage : ExchangePropertyPageControl
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002DEE File Offset: 0x00000FEE
		internal DetailsTemplatesPropertyPage()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DF8 File Offset: 0x00000FF8
		internal DetailsTemplatesPropertyPage(IServiceProvider services)
		{
			this.designSurface = new DetailsTemplatesSurface(services);
			Control control = this.designSurface.View as Control;
			control.Dock = DockStyle.Fill;
			base.SuspendLayout();
			base.Controls.Add(control);
			base.ResumeLayout();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002E47 File Offset: 0x00001047
		internal DetailsTemplatesSurface TemplateSurface
		{
			get
			{
				return this.designSurface;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E50 File Offset: 0x00001050
		protected override void OnSetActive(EventArgs e)
		{
			base.OnSetActive(e);
			DetailsTemplate detailsTemplate = base.BindingSource.DataSource as DetailsTemplate;
			if (detailsTemplate != null && detailsTemplate.IsValid)
			{
				this.designSurface.LoadTemplate(detailsTemplate);
				this.designSurface.DataContext = base.Context;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E9D File Offset: 0x0000109D
		protected override void OnEnabledChanged(EventArgs e)
		{
			if (base.Enabled)
			{
				base.OnEnabledChanged(e);
			}
		}

		// Token: 0x0400000D RID: 13
		private DetailsTemplatesSurface designSurface;
	}
}
