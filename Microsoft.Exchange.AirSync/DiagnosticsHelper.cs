using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007C RID: 124
	internal class DiagnosticsHelper
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x00025B7C File Offset: 0x00023D7C
		internal static CallType GetCallType(string argument, out string newArg)
		{
			if (string.IsNullOrEmpty(argument))
			{
				newArg = null;
				return CallType.Metadata;
			}
			argument = argument.Trim();
			if (argument.ToLower() == "dumpcache")
			{
				newArg = null;
				return CallType.DumpCache;
			}
			if (argument.ToLower().StartsWith("emailaddress="))
			{
				newArg = argument.Substring("emailaddress=".Length);
				return CallType.EmailAddress;
			}
			if (argument.ToLower().StartsWith("deviceid="))
			{
				newArg = argument.Substring("deviceid=".Length);
				return CallType.DeviceId;
			}
			throw new ArgumentException("Unknown argument: " + argument);
		}

		// Token: 0x040004AD RID: 1197
		internal const string DumpCacheArgument = "dumpcache";

		// Token: 0x040004AE RID: 1198
		internal const string EmailAddressArgument = "emailaddress=";

		// Token: 0x040004AF RID: 1199
		internal const string DeviceIdArgument = "deviceid=";
	}
}
