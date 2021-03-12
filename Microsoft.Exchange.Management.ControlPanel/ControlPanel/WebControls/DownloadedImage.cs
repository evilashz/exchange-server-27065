using System;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B0 RID: 1456
	[ClientScriptResource("DownloadedImage", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class DownloadedImage : Image
	{
		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x000CAC07 File Offset: 0x000C8E07
		// (set) Token: 0x06004298 RID: 17048 RVA: 0x000CAC0F File Offset: 0x000C8E0F
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x04002BC4 RID: 11204
		private bool readOnly;
	}
}
