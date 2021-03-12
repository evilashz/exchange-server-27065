using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x0200079C RID: 1948
	internal static class CacheApplicationManager
	{
		// Token: 0x06006105 RID: 24837 RVA: 0x001499A0 File Offset: 0x00147BA0
		static CacheApplicationManager()
		{
			CacheApplicationManager.appDiagPipes.Add("Powershell", "Powershell-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("Powershell-LiveId", "Powershell-LiveId-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("Powershell-Proxy", "Powershell-Proxy-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("PowershellLiveId-Proxy", "PowershellLiveId-Proxy-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("Ecp", "Ecp-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("Psws", "Psws-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("Owa", "Owa-NamedPipe");
			CacheApplicationManager.appDiagPipes.Add("HxS", "HxS-NamedPipe");
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x00149A5C File Offset: 0x00147C5C
		public static string GetAppPipeName(string app)
		{
			if (!CacheApplicationManager.IsApplicationDefined(app))
			{
				throw new ArgumentException(string.Format("The application {0} is not defined in Exchange ProvisioningCache.", app.ToString()));
			}
			return CacheApplicationManager.appDiagPipes[app];
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x00149A87 File Offset: 0x00147C87
		public static bool IsApplicationDefined(string app)
		{
			return CacheApplicationManager.appDiagPipes.ContainsKey(app);
		}

		// Token: 0x04004103 RID: 16643
		internal const string RPSIdentification = "Powershell";

		// Token: 0x04004104 RID: 16644
		internal const string RPSLiveIdIdentification = "Powershell-LiveId";

		// Token: 0x04004105 RID: 16645
		internal const string RPSProxyIdentification = "Powershell-Proxy";

		// Token: 0x04004106 RID: 16646
		internal const string RPSLiveIdProxyIdentification = "PowershellLiveId-Proxy";

		// Token: 0x04004107 RID: 16647
		internal const string EcpIdentification = "Ecp";

		// Token: 0x04004108 RID: 16648
		internal const string PswsIdentification = "Psws";

		// Token: 0x04004109 RID: 16649
		internal const string OwaIdentification = "Owa";

		// Token: 0x0400410A RID: 16650
		internal const string HxsIdentification = "HxS";

		// Token: 0x0400410B RID: 16651
		private static readonly Dictionary<string, string> appDiagPipes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
