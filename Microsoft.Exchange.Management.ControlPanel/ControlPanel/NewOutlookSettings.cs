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
	// Token: 0x020001C7 RID: 455
	public class NewOutlookSettings : BaseForm
	{
		// Token: 0x060024E7 RID: 9447 RVA: 0x000714A8 File Offset: 0x0006F6A8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.IsPostBack)
			{
				LanguageList langList = new LanguageList();
				PropertiesContentPanel propertiesContentPanel = (PropertiesContentPanel)this.outlooksettings.Controls[0];
				DropDownList dropDownList = (DropDownList)propertiesContentPanel.FindControl("ddLocale");
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

		// Token: 0x04001EAD RID: 7853
		protected PropertyPageSheet outlooksettings;
	}
}
