using System;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200008E RID: 142
	internal sealed class ElcEwsCallingContext : BaseServiceCallingContext<ExchangeServiceBinding>
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x000294DE File Offset: 0x000276DE
		public ElcEwsCallingContext(ADUser user, bool isCrossPremise)
		{
			this.user = user;
			this.isCrossPremise = isCrossPremise;
			this.userAgent = ElcEwsClientHelper.GetOAuthUserAgent("ElcClient");
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00029528 File Offset: 0x00027728
		public override ExchangeServiceBinding CreateServiceBinding(Uri serviceUrl)
		{
			Tracer.TraceInformation("ElcEwsCallingContext.CreateServiceBinding", new object[0]);
			ExchangeServiceBinding exchangeServiceBinding = base.CreateServiceBinding(serviceUrl);
			if (this.isCrossPremise)
			{
				OAuthCredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(this.user.OrganizationId, this.user, this.user.ArchiveDomain.Domain);
				exchangeServiceBinding.UseDefaultCredentials = false;
				exchangeServiceBinding.Credentials = oauthCredentialsForAppActAsToken;
				exchangeServiceBinding.UserAgent = this.userAgent;
				exchangeServiceBinding.ManagementRole = new ManagementRoleType
				{
					ApplicationRoles = new string[]
					{
						"UserApplication"
					}
				};
			}
			else
			{
				exchangeServiceBinding.UseDefaultCredentials = true;
				exchangeServiceBinding.Disposed += delegate(object param0, EventArgs param1)
				{
					ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ElcEwsCallingContext.CertificateErrorHandler));
				};
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ElcEwsCallingContext.CertificateErrorHandler));
			}
			return exchangeServiceBinding;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00029608 File Offset: 0x00027808
		public override void SetServiceApiContext(ExchangeServiceBinding binding, string mailboxEmailAddress)
		{
			base.SetServiceApiContext(binding, mailboxEmailAddress);
			OAuthCredentials oauthCredentials = binding.Credentials as OAuthCredentials;
			if (oauthCredentials != null)
			{
				oauthCredentials.ClientRequestId = new Guid?(binding.HttpContext.ClientRequestId);
			}
			if (!this.isCrossPremise)
			{
				binding.OpenAsAdminOrSystemService = new OpenAsAdminOrSystemServiceType
				{
					LogonType = SpecialLogonType.SystemService,
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

		// Token: 0x06000566 RID: 1382 RVA: 0x00029680 File Offset: 0x00027880
		private static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
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
			if (ElcEwsCallingContext.AllowInternalUntrustedCerts())
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

		// Token: 0x06000567 RID: 1383 RVA: 0x000296E0 File Offset: 0x000278E0
		private static bool AllowInternalUntrustedCerts()
		{
			if (ElcEwsCallingContext.allowInternalUntrustedCerts == null || ElcEwsCallingContext.allowInternalUntrustedCerts == null)
			{
				ElcEwsCallingContext.allowInternalUntrustedCerts = new bool?(true);
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
							ElcEwsCallingContext.allowInternalUntrustedCerts = new bool?((int)value != 0);
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
			return ElcEwsCallingContext.allowInternalUntrustedCerts.Value;
		}

		// Token: 0x04000406 RID: 1030
		private static bool? allowInternalUntrustedCerts;

		// Token: 0x04000407 RID: 1031
		private readonly string userAgent;

		// Token: 0x04000408 RID: 1032
		private readonly ADUser user;

		// Token: 0x04000409 RID: 1033
		private readonly bool isCrossPremise;
	}
}
