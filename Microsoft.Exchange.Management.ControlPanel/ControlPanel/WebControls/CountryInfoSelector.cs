using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200057B RID: 1403
	[ToolboxData("<{0}:CountryInfoSelector runat=server></{0}:CountryInfoSelector>")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class CountryInfoSelector : DropDownList
	{
		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000C6FC9 File Offset: 0x000C51C9
		// (set) Token: 0x06004149 RID: 16713 RVA: 0x000C6FD1 File Offset: 0x000C51D1
		public bool SkipEmptyCountry
		{
			get
			{
				return this.skipEmptyCountry;
			}
			set
			{
				this.skipEmptyCountry = value;
			}
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000C6FDC File Offset: 0x000C51DC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			List<CountryInfo> list = new List<CountryInfo>(CountryInfo.AllCountryInfos);
			list.Sort();
			if (!this.SkipEmptyCountry)
			{
				this.Items.Add(new ListItem(string.Empty, string.Empty));
			}
			bool isRtl = RtlUtil.IsRtl;
			foreach (CountryInfo countryInfo in list)
			{
				this.Items.Add(new ListItem(RtlUtil.ConvertToDecodedBidiString(countryInfo.LocalizedDisplayName, isRtl), countryInfo.Name));
			}
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000C708C File Offset: 0x000C528C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("role", "combobox");
			base.AddAttributesToRender(writer);
		}

		// Token: 0x04002B3D RID: 11069
		private bool skipEmptyCountry;
	}
}
