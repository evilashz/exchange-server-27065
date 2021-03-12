using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PswsClient;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C1F RID: 3103
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocalPowerShellClient
	{
		// Token: 0x06004408 RID: 17416 RVA: 0x000B68FA File Offset: 0x000B4AFA
		public LocalPowerShellClient(IAuthenticator authenticator)
		{
			ArgumentValidator.ThrowIfNull("authenticator", authenticator);
			this.authenticator = authenticator;
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000B6914 File Offset: 0x000B4B14
		public void ApplyTo(PswsCmdlet cmdlet)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			cmdlet.Authenticator = this.authenticator;
			cmdlet.HostServerName = "localhost";
			cmdlet.Port = 444;
			CertificateValidationManager.SetComponentId(cmdlet.AdditionalHeaders, "LocalPowerShellClient");
			CertificateValidationManager.RegisterCallback("LocalPowerShellClient", new RemoteCertificateValidationCallback(LocalPowerShellClient.CertificateHandler));
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000B6974 File Offset: 0x000B4B74
		private static bool CertificateHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return sslPolicyErrors == SslPolicyErrors.None || sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch;
		}

		// Token: 0x040039C7 RID: 14791
		private const string ComponentId = "LocalPowerShellClient";

		// Token: 0x040039C8 RID: 14792
		private readonly IAuthenticator authenticator;
	}
}
