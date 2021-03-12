using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001B5 RID: 437
	public class NewDataClassificationLanguage : BaseForm
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x0006DD1D File Offset: 0x0006BF1D
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.FillLanguageDropdown();
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x0006DD78 File Offset: 0x0006BF78
		private void FillLanguageDropdown()
		{
			if (!base.IsPostBack)
			{
				LanguageList langList = new LanguageList();
				PropertiesContentPanel propertiesContentPanel = (PropertiesContentPanel)this.DataClassificationLanguage.Controls[0];
				DropDownList dropDownList = (DropDownList)propertiesContentPanel.FindControl("ddLanguage");
				HashSet<int> expectedCultureLcids = LanguagePackInfo.expectedCultureLcids;
				IEnumerable<ListItem> source = expectedCultureLcids.Select(delegate(int lcid)
				{
					CultureInfo cultureInfo = new CultureInfo(lcid);
					return new ListItem(RtlUtil.ConvertToDecodedBidiString(langList.GetDisplayValue(cultureInfo.Name), RtlUtil.IsRtl), cultureInfo.Name);
				});
				dropDownList.Items.AddRange((from i in source
				orderby i.Text
				select i).ToArray<ListItem>());
			}
		}

		// Token: 0x04001E31 RID: 7729
		protected PropertyPageSheet DataClassificationLanguage;
	}
}
