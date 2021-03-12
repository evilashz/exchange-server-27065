using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C8 RID: 1480
	public class EcpPortRangeValidator : EcpRangeValidator
	{
		// Token: 0x06004316 RID: 17174 RVA: 0x000CB735 File Offset: 0x000C9935
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.MinimumValue = "1";
			base.MaximumValue = "65535";
			base.Type = ValidationDataType.Integer;
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x000CB75B File Offset: 0x000C995B
		public override string TypeId
		{
			get
			{
				return "PortRange";
			}
		}
	}
}
