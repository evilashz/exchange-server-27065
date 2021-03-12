using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000578 RID: 1400
	public abstract class MenuItem : WebControl
	{
		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000C67BB File Offset: 0x000C49BB
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000C67BF File Offset: 0x000C49BF
		// (set) Token: 0x06004127 RID: 16679 RVA: 0x000C67C7 File Offset: 0x000C49C7
		public ContextMenu Owner { get; set; }

		// Token: 0x06004128 RID: 16680
		public abstract string ToJavaScript();
	}
}
