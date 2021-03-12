using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200001B RID: 27
	internal static class OnlineMeetingHelper
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000031E0 File Offset: 0x000013E0
		internal static string GetSipDomain(string sipAddress)
		{
			if (string.IsNullOrWhiteSpace(sipAddress))
			{
				return string.Empty;
			}
			int num = sipAddress.LastIndexOf("@");
			if (num <= 0)
			{
				return string.Empty;
			}
			return sipAddress.Substring(num + 1);
		}
	}
}
