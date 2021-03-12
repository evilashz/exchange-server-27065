using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Autodiscover
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutodiscoverRpcHttpSettings
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001CE00 File Offset: 0x0001B000
		internal string RpcHttpServer { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001CE09 File Offset: 0x0001B009
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001CE11 File Offset: 0x0001B011
		internal bool SslRequired { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001CE1A File Offset: 0x0001B01A
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0001CE22 File Offset: 0x0001B022
		internal string AuthPackageString { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001CE2B File Offset: 0x0001B02B
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001CE33 File Offset: 0x0001B033
		internal string XropUrl { get; private set; }

		// Token: 0x0600075F RID: 1887 RVA: 0x0001CE3C File Offset: 0x0001B03C
		internal AutodiscoverRpcHttpSettings(string rpcHttpServer, bool sslRequired, string authPackageString, string xropUrl)
		{
			this.RpcHttpServer = rpcHttpServer;
			this.SslRequired = sslRequired;
			this.AuthPackageString = authPackageString;
			this.XropUrl = xropUrl;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001CE64 File Offset: 0x0001B064
		internal static AutodiscoverRpcHttpSettings GetRpcHttpAuthSettingsFromService(RpcHttpService service, ClientAccessType clientAccessType, AutodiscoverRpcHttpSettings.AuthMethodGetter authMethodPicker)
		{
			bool flag = clientAccessType == ClientAccessType.Internal;
			bool sslRequired = service.Url.Scheme == Uri.UriSchemeHttps;
			AuthenticationMethod authMethod = authMethodPicker(flag ? service.InternalClientAuthenticationMethod : service.ExternalClientAuthenticationMethod, service.IISAuthenticationMethods, sslRequired);
			string authPackageStringFromAuthMethod = AutodiscoverRpcHttpSettings.GetAuthPackageStringFromAuthMethod(authMethod);
			bool flag2 = service.XropUrl != null;
			return new AutodiscoverRpcHttpSettings(service.Url.DnsSafeHost, sslRequired, authPackageStringFromAuthMethod, flag2 ? service.XropUrl.AbsoluteUri : null);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001CEE4 File Offset: 0x0001B0E4
		internal static AuthenticationMethod UseProvidedAuthenticationMethod(AuthenticationMethod clientAuthenticationMethod, ICollection<AuthenticationMethod> unusedAuthMethods, bool unusedSslRequired)
		{
			return clientAuthenticationMethod;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		internal static string GetAuthPackageStringFromAuthMethod(AuthenticationMethod authMethod)
		{
			string result;
			AutodiscoverRpcHttpSettings.AuthMethodStrings.TryGetValue(authMethod, out result);
			return result;
		}

		// Token: 0x04000354 RID: 852
		internal const string Nego2AuthPackageString = "Nego2";

		// Token: 0x04000355 RID: 853
		internal const string NegotiateAuthPackageString = "Negotiate";

		// Token: 0x04000356 RID: 854
		internal const string NtlmAuthPackageString = "Ntlm";

		// Token: 0x04000357 RID: 855
		internal const string BasicAuthPackageString = "Basic";

		// Token: 0x04000358 RID: 856
		private static readonly Dictionary<AuthenticationMethod, string> AuthMethodStrings = new Dictionary<AuthenticationMethod, string>
		{
			{
				AuthenticationMethod.NegoEx,
				"Nego2"
			},
			{
				AuthenticationMethod.Negotiate,
				"Negotiate"
			},
			{
				AuthenticationMethod.Ntlm,
				"Ntlm"
			},
			{
				AuthenticationMethod.Basic,
				"Basic"
			}
		};

		// Token: 0x020000AE RID: 174
		// (Invoke) Token: 0x06000765 RID: 1893
		internal delegate AuthenticationMethod AuthMethodGetter(AuthenticationMethod clientAuthenticationMethod, ICollection<AuthenticationMethod> iisAuthenticationMethods, bool sslRequired);
	}
}
