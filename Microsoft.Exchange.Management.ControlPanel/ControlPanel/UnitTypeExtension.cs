using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B3 RID: 1715
	public static class UnitTypeExtension
	{
		// Token: 0x06004913 RID: 18707 RVA: 0x000DF6CA File Offset: 0x000DD8CA
		public static string ToJavaScript(this UnitType value)
		{
			if (value != UnitType.Pixel)
			{
				return "%";
			}
			return "px";
		}
	}
}
