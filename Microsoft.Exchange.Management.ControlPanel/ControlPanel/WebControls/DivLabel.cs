using System;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005D3 RID: 1491
	internal class DivLabel : EncodingLabel
	{
		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x000CC015 File Offset: 0x000CA215
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}
	}
}
