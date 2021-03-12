using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B4 RID: 1716
	public static class NullableGuidExtension
	{
		// Token: 0x06004914 RID: 18708 RVA: 0x000DF6DC File Offset: 0x000DD8DC
		public static string FormatForLog(this Guid? guid)
		{
			if (guid == null)
			{
				return string.Empty;
			}
			return guid.Value.ToString();
		}
	}
}
