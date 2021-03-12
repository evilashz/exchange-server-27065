using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000F RID: 15
	internal class ServerToServerAutoDiscoveryCallingContext : BaseServiceCallingContext<DefaultBinding_Autodiscover>
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00006683 File Offset: 0x00004883
		public ServerToServerAutoDiscoveryCallingContext(IDictionary<Uri, string> remoteUrls)
		{
			this.remoteUrls = (remoteUrls ?? new Dictionary<Uri, string>());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000066C0 File Offset: 0x000048C0
		public override DefaultBinding_Autodiscover CreateServiceBinding(Uri serviceUrl)
		{
			Tracer.TraceInformation("ServerToServerEwsCallingContext.CreateServiceBinding", new object[0]);
			DefaultBinding_Autodiscover defaultBinding_Autodiscover = base.CreateServiceBinding(serviceUrl);
			if (this.remoteUrls.ContainsKey(serviceUrl))
			{
				OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.ForestWideOrgId, this.remoteUrls[serviceUrl]);
				defaultBinding_Autodiscover.UseDefaultCredentials = false;
				defaultBinding_Autodiscover.Credentials = oauthCredentialsForAppToken;
			}
			else
			{
				defaultBinding_Autodiscover.UseDefaultCredentials = true;
				defaultBinding_Autodiscover.Disposed += delegate(object param0, EventArgs param1)
				{
					ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ServerToServerEwsCallingContext.CertificateErrorHandler));
				};
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ServerToServerEwsCallingContext.CertificateErrorHandler));
			}
			return defaultBinding_Autodiscover;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006768 File Offset: 0x00004968
		public override void SetServiceApiContext(DefaultBinding_Autodiscover binding, string mailboxEmailAddress)
		{
			base.SetServiceApiContext(binding, mailboxEmailAddress);
			ICredentials credentials = binding.Credentials;
			if (!ServerToServerAutoDiscoveryCallingContext.IsCrossPremiseServiceBinding(binding))
			{
				binding.OpenAsAdminOrSystemService = new OpenAsAdminOrSystemServiceType
				{
					LogonType = SpecialLogonType.Admin,
					ConnectingSID = new ConnectingSIDType
					{
						Item = new PrimarySmtpAddressType
						{
							Value = mailboxEmailAddress
						}
					}
				};
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000067C1 File Offset: 0x000049C1
		private static bool IsCrossPremiseServiceBinding(DefaultBinding_Autodiscover binding)
		{
			return !binding.UseDefaultCredentials;
		}

		// Token: 0x04000066 RID: 102
		private readonly IDictionary<Uri, string> remoteUrls;
	}
}
