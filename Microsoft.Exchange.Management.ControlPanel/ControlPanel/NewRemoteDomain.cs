using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003AE RID: 942
	public class NewRemoteDomain : BaseForm
	{
		// Token: 0x0600318A RID: 12682 RVA: 0x00098EC4 File Offset: 0x000970C4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.IsPostBack)
			{
				new LanguageList();
				PropertiesContentPanel propertiesContentPanel = (PropertiesContentPanel)this.newEditRemoteDomainPropertySheet.Controls[0];
				DropDownList dropDownList = (DropDownList)propertiesContentPanel.FindControl("ddMIMECharacterSet");
				DropDownList dropDownList2 = (DropDownList)propertiesContentPanel.FindControl("ddNonMIMECharacterSet");
				DomainContentConfig.CharacterSetInfo[] characterSetList = DomainContentConfig.CharacterSetList;
				IEnumerable<ListItem> source = from aCharacterSet in characterSetList
				select new ListItem(aCharacterSet.CharsetDescription, aCharacterSet.CharacterSetName);
				dropDownList2.Items.AddRange(source.ToArray<ListItem>());
				dropDownList.Items.AddRange(source.ToArray<ListItem>());
			}
		}

		// Token: 0x0400240C RID: 9228
		protected PropertyPageSheet newEditRemoteDomainPropertySheet;
	}
}
