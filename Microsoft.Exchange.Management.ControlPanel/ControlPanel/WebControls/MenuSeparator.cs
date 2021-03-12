using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000610 RID: 1552
	public class MenuSeparator : MenuItem
	{
		// Token: 0x06004521 RID: 17697 RVA: 0x000D1150 File Offset: 0x000CF350
		public MenuSeparator()
		{
			this.CssClass = "MenuSeparator";
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000D1163 File Offset: 0x000CF363
		public override string ToJavaScript()
		{
			return "new MenuSeparator()";
		}
	}
}
