using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Owa
{
	// Token: 0x0200016E RID: 366
	internal class OwaConstants
	{
		// Token: 0x040007A7 RID: 1959
		public const string OwaVersionRegistryName = "OwaVersion";

		// Token: 0x040007A8 RID: 1960
		internal const string OwaLocalPath = "ClientAccess\\owa\\";

		// Token: 0x040007A9 RID: 1961
		internal const string DefaultExtensionLocalPathFormat = "\\prem\\{0}\\ext\\def\\";

		// Token: 0x040007AA RID: 1962
		internal const string KillBitLocalPathFormat = "\\prem\\{0}\\ext\\killbit\\";

		// Token: 0x040007AB RID: 1963
		internal const string KillBitFileName = "killbit.xml";

		// Token: 0x040007AC RID: 1964
		internal const string OwaDllBinPath = "ClientAccess\\\\owa\\\\bin\\\\Microsoft.Exchange.Clients.Owa.dll";

		// Token: 0x040007AD RID: 1965
		public static readonly string OwaSetupInstallKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";
	}
}
