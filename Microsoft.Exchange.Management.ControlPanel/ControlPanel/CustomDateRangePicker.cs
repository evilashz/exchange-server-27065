using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200031E RID: 798
	public class CustomDateRangePicker : BaseForm
	{
		// Token: 0x06002EB3 RID: 11955 RVA: 0x0008EAF8 File Offset: 0x0008CCF8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				string text = RtlUtil.ConvertToDecodedBidiString(exTimeZone.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture), RtlUtil.IsRtl);
				this.ddlTimeZone.Items.Add(new ListItem(text, exTimeZone.Id));
			}
		}

		// Token: 0x040022D3 RID: 8915
		protected DropDownList ddlTimeZone;
	}
}
