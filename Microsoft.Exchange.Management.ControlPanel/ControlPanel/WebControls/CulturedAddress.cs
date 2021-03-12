using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200057D RID: 1405
	public class CulturedAddress : WebControl, INamingContainer
	{
		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x000C719B File Offset: 0x000C539B
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x000C71A3 File Offset: 0x000C53A3
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate StreetTemplate { get; set; }

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000C71AC File Offset: 0x000C53AC
		// (set) Token: 0x06004152 RID: 16722 RVA: 0x000C71B4 File Offset: 0x000C53B4
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate CityTemplate { get; set; }

		// Token: 0x17002553 RID: 9555
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x000C71BD File Offset: 0x000C53BD
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x000C71C5 File Offset: 0x000C53C5
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate StateProvinceTemplate { get; set; }

		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x000C71CE File Offset: 0x000C53CE
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x000C71D6 File Offset: 0x000C53D6
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate ZipPostalTemplate { get; set; }

		// Token: 0x17002555 RID: 9557
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000C71DF File Offset: 0x000C53DF
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x000C71E7 File Offset: 0x000C53E7
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate CountryTemplate { get; set; }

		// Token: 0x06004159 RID: 16729 RVA: 0x000C71F0 File Offset: 0x000C53F0
		public CulturedAddress()
		{
			this.CssClass = "divEncapsulation";
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000C7204 File Offset: 0x000C5404
		protected override void CreateChildControls()
		{
			CulturedHelper.CreateChildControls(this, "Street,City,StateProvince,ZipPostal,Country", 1, new Dictionary<string, ITemplate>
			{
				{
					"Street",
					this.StreetTemplate
				},
				{
					"ZipPostal",
					this.ZipPostalTemplate
				},
				{
					"City",
					this.CityTemplate
				},
				{
					"StateProvince",
					this.StateProvinceTemplate
				},
				{
					"Country",
					this.CountryTemplate
				}
			});
		}

		// Token: 0x04002B3E RID: 11070
		private const string DefaultPattern = "Street,City,StateProvince,ZipPostal,Country";
	}
}
