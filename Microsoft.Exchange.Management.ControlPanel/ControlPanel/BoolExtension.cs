using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B1 RID: 1713
	public static class BoolExtension
	{
		// Token: 0x06004911 RID: 18705 RVA: 0x000DF66B File Offset: 0x000DD86B
		public static string ToJavaScript(this bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}
	}
}
