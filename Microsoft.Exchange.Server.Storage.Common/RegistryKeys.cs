using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200007C RID: 124
	public static class RegistryKeys
	{
		// Token: 0x04000650 RID: 1616
		public const string ParametersSystemKey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem";

		// Token: 0x04000651 RID: 1617
		public const string PerSessionLimitsKey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession";

		// Token: 0x04000652 RID: 1618
		public const string ReplayParametersKey = "Software\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";

		// Token: 0x04000653 RID: 1619
		public const string DatabaseSubKey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\{1}-{2}";
	}
}
