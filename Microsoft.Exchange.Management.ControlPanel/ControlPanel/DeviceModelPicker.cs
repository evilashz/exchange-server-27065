using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000327 RID: 807
	public class DeviceModelPicker : PickerForm
	{
		// Token: 0x06002EE1 RID: 12001 RVA: 0x0008F148 File Offset: 0x0008D348
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!string.IsNullOrEmpty(this.Context.Request.QueryString["dt"]))
			{
				this.lblDeviceType.Text = this.Context.Request.QueryString["dt"];
			}
		}

		// Token: 0x040022E2 RID: 8930
		protected Label lblDeviceType;
	}
}
