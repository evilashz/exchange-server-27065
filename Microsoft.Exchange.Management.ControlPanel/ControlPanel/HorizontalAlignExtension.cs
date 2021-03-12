using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B2 RID: 1714
	public static class HorizontalAlignExtension
	{
		// Token: 0x06004912 RID: 18706 RVA: 0x000DF67C File Offset: 0x000DD87C
		public static string ToJavaScript(this HorizontalAlign alignment)
		{
			string result = string.Empty;
			switch (alignment)
			{
			case HorizontalAlign.Left:
				result = "left";
				break;
			case HorizontalAlign.Center:
				result = "center";
				break;
			case HorizontalAlign.Right:
				result = "right";
				break;
			case HorizontalAlign.Justify:
				result = "justify";
				break;
			}
			return result;
		}
	}
}
