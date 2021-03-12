using System;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200032B RID: 811
	[ClientScriptResource("EsoPickerForm", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class EsoPickerForm : PickerForm
	{
		// Token: 0x06002EE6 RID: 12006 RVA: 0x0008F1DC File Offset: 0x0008D3DC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			StaticBinding staticBinding = new StaticBinding();
			staticBinding.Name = "IsCrossPremise";
			if (string.IsNullOrEmpty(base.Request.QueryString["xprs"]))
			{
				foreach (ColumnHeader columnHeader in this.pickerContent.Columns)
				{
					if (columnHeader.Name == "RecipientTypeName")
					{
						this.pickerContent.Columns.Remove(columnHeader);
						break;
					}
				}
				staticBinding.Value = "false";
			}
			else
			{
				staticBinding.Value = "true";
			}
			this.pickerContent.FilterParameters.Add(staticBinding);
			if (DDIHelper.IsFFO())
			{
				base.Title = Strings.MasterAccountPickerTitle;
			}
		}

		// Token: 0x040022E3 RID: 8931
		protected PickerContent pickerContent;
	}
}
