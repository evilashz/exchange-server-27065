using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Win32;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000E RID: 14
	internal sealed class ServerToServerEwsCallingContext : BaseServiceCallingContext<ExchangeServiceBinding>
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000638C File Offset: 0x0000458C
		public ServerToServerEwsCallingContext(IDictionary<Uri, string> remoteUrls)
		{
			this.remoteUrls = (remoteUrls ?? new Dictionary<Uri, string>());
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000063C8 File Offset: 0x000045C8
		public override ExchangeServiceBinding CreateServiceBinding(Uri serviceUrl)
		{
			Tracer.TraceInformation("ServerToServerEwsCallingContext.CreateServiceBinding", new object[0]);
			ExchangeServiceBinding exchangeServiceBinding = base.CreateServiceBinding(serviceUrl);
			if (this.remoteUrls.ContainsKey(serviceUrl))
			{
				OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.ForestWideOrgId, this.remoteUrls[serviceUrl]);
				exchangeServiceBinding.UseDefaultCredentials = false;
				exchangeServiceBinding.Credentials = oauthCredentialsForAppToken;
				exchangeServiceBinding.ManagementRole = new ManagementRoleType
				{
					ApplicationRoles = new string[]
					{
						"MailboxSearchApplication"
					}
				};
			}
			else
			{
				exchangeServiceBinding.UseDefaultCredentials = true;
				exchangeServiceBinding.Disposed += delegate(object param0, EventArgs param1)
				{
					ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ServerToServerEwsCallingContext.CertificateErrorHandler));
				};
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ServerToServerEwsCallingContext.CertificateErrorHandler));
			}
			return exchangeServiceBinding;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006490 File Offset: 0x00004690
		public override void SetServiceApiContext(ExchangeServiceBinding binding, string mailboxEmailAddress)
		{
			base.SetServiceApiContext(binding, mailboxEmailAddress);
			OAuthCredentials oauthCredentials = binding.Credentials as OAuthCredentials;
			if (oauthCredentials != null)
			{
				oauthCredentials.ClientRequestId = new Guid?(binding.HttpContext.ClientRequestId);
			}
			if (!ServerToServerEwsCallingContext.IsCrossPremiseServiceBinding(binding))
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

		// Token: 0x060000C6 RID: 198 RVA: 0x00006508 File Offset: 0x00004708
		internal static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				Tracer.TraceInformation("Accepting SSL certificate because the only error is invalid hostname", new object[0]);
				return true;
			}
			if (ServerToServerEwsCallingContext.AllowInternalUntrustedCerts())
			{
				Tracer.TraceInformation("Accepting SSL certificate because registry config AllowInternalUntrustedCerts tells to ignore errors", new object[0]);
				return true;
			}
			Tracer.TraceInformation("Failed because SSL certificate contains the following errors: {0}", new object[]
			{
				sslPolicyErrors
			});
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006568 File Offset: 0x00004768
		private static bool AllowInternalUntrustedCerts()
		{
			if (ServerToServerEwsCallingContext.allowInternalUntrustedCerts == null || ServerToServerEwsCallingContext.allowInternalUntrustedCerts == null)
			{
				ServerToServerEwsCallingContext.allowInternalUntrustedCerts = new bool?(true);
				RegistryKey registryKey = null;
				try
				{
					registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA");
					if (registryKey != null)
					{
						object value = registryKey.GetValue("AllowInternalUntrustedCerts");
						if (value != null && value is int)
						{
							Tracer.TraceInformation("Registry value: {0} was found. Its value is: {1}", new object[]
							{
								"AllowInternalUntrustedCerts",
								value
							});
							ServerToServerEwsCallingContext.allowInternalUntrustedCerts = new bool?((int)value != 0);
						}
						else
						{
							Tracer.TraceInformation("Registry value: {0} was not found or invalid. Use default value: {1}.", new object[]
							{
								"AllowInternalUntrustedCerts",
								true
							});
						}
					}
				}
				catch (SecurityException)
				{
					Tracer.TraceInformation("Failed reading registry key. Use default value: {0}", new object[]
					{
						true
					});
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			}
			return ServerToServerEwsCallingContext.allowInternalUntrustedCerts.Value;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006678 File Offset: 0x00004878
		private static bool IsCrossPremiseServiceBinding(ExchangeServiceBinding binding)
		{
			return !binding.UseDefaultCredentials;
		}

		// Token: 0x04000063 RID: 99
		private static bool? allowInternalUntrustedCerts;

		// Token: 0x04000064 RID: 100
		private readonly IDictionary<Uri, string> remoteUrls;
	}
}
