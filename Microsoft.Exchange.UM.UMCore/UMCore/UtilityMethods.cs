using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000235 RID: 565
	internal static class UtilityMethods
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x0004A874 File Offset: 0x00048A74
		internal static double GetTimeSpanFrom(ExDateTime time)
		{
			double result = 0.0;
			ExDateTime now = ExDateTime.GetNow(time.TimeZone);
			if (now > time)
			{
				result = (now - time).TotalSeconds;
			}
			return result;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004A8B2 File Offset: 0x00048AB2
		internal static bool IsAnonymousNumber(string number)
		{
			return string.IsNullOrEmpty(number) || string.Equals(number, "Anonymous", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0004A8CA File Offset: 0x00048ACA
		internal static bool IsAnonymousAddress(PlatformTelephonyAddress address)
		{
			return (!string.IsNullOrEmpty(address.Name) && string.Equals(address.Name, "Anonymous", StringComparison.OrdinalIgnoreCase)) || UtilityMethods.IsAnonymousNumber(address.Uri.User);
		}
	}
}
