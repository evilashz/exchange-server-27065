using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.CertificateAuthentication.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.CertificateAuthentication;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication
{
	// Token: 0x02000005 RID: 5
	public class CertificateHeaderAuthModule : IHttpModule
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002D80 File Offset: 0x00000F80
		static CertificateHeaderAuthModule()
		{
			if (!int.TryParse(ConfigurationManager.AppSettings["CertificateHeaderAuthentication.MaxRetryForADTransient"], out CertificateHeaderAuthModule.maxRetryForADTransient))
			{
				CertificateHeaderAuthModule.maxRetryForADTransient = 2;
			}
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["CertificateHeaderAuthentication.UserCacheMappingMaximumSize"], out num) || num < 0)
			{
				num = 300;
			}
			if (num != 0)
			{
				int num2;
				if (!int.TryParse(ConfigurationManager.AppSettings["CertificateHeaderAuthentication.UserCacheMappingExpirationInHours"], out num2) || num2 < 0)
				{
					num2 = 4;
				}
				CertificateHeaderAuthModule.certCache = new CertificateADUserCache(num2, num);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E2D File Offset: 0x0000102D
		void IHttpModule.Init(HttpApplication application)
		{
			application.AuthenticateRequest += CertificateHeaderAuthModule.OnAuthenticateRequestHandler;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E3A File Offset: 0x0000103A
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E3C File Offset: 0x0000103C
		private static void OnAuthenticateRequest(object source, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Request.IsAuthenticated)
			{
				return;
			}
			HttpRequest request = context.Request;
			if (!CertificateHeaderAuthModule.IsValidCertificateHeaderRequest(request))
			{
				return;
			}
			Logger.LogVerbose("Request of Authentication for certificate {0}.", new object[]
			{
				request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]
			});
			int i = 0;
			while (i < CertificateHeaderAuthModule.maxRetryForADTransient)
			{
				try
				{
					X509Identifier x509Identifier = CertificateHeaderAuthModule.CreateCertificateIdentity(request);
					ADUser aduser = CertificateHeaderAuthModule.GetUserFromCache(x509Identifier);
					if (aduser == null)
					{
						aduser = CertificateAuthenticationModule.ResolveCertificate(x509Identifier, null);
						if (aduser != null)
						{
							CertificateHeaderAuthModule.AddUserToCache(x509Identifier, aduser);
						}
					}
					if (aduser == null)
					{
						Logger.LogEvent(CertificateHeaderAuthModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_UserNotFound, request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"], new object[]
						{
							request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"],
							"CertificateHeader"
						});
						Logger.LogVerbose("Certificate authentication succeeded but certificate {0} cannot be mapped to an AD account.", new object[]
						{
							request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]
						});
						break;
					}
					IIdentity identity;
					if (aduser.RecipientTypeDetails == RecipientTypeDetails.LinkedUser)
					{
						identity = new GenericIdentity(aduser.Sid.ToString(), "CertificateLinkedUser");
					}
					else
					{
						identity = new WindowsIdentity(aduser.UserPrincipalName);
					}
					if (!OrganizationId.ForestWideOrgId.Equals(aduser.OrganizationId))
					{
						HttpContext.Current.Items[CertificateAuthenticationModule.TenantCertificateOrganizaitonItemName] = aduser.OrganizationId.OrganizationalUnit.Name;
					}
					context.User = new GenericPrincipal(identity, new string[0]);
					Logger.LogVerbose("User correctly authenticated and linked to Certificate {0}.", new object[]
					{
						request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]
					});
					if (i > 0)
					{
						Logger.LogEvent(CertificateHeaderAuthModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_TransientRecovery, null, new object[]
						{
							request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"],
							i,
							"CertificateHeader"
						});
					}
					break;
				}
				catch (ADTransientException ex)
				{
					i++;
					if (i == 1)
					{
						Logger.LogEvent(CertificateHeaderAuthModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_TransientError, null, new object[]
						{
							ex,
							request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"],
							"CertificateHeader"
						});
					}
					Logger.LogError(string.Format("AD Transient Error when processing certificate authentication {0}.", request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]), ex);
					if (i > CertificateHeaderAuthModule.maxRetryForADTransient)
					{
						throw;
					}
				}
				catch (Exception ex2)
				{
					Logger.LogEvent(CertificateHeaderAuthModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_ServerError, null, new object[]
					{
						ex2,
						request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"],
						"CertificateHeader"
					});
					Logger.LogError(string.Format("AD Transient Error when processing certificate authentication {0}.", request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]), ex2);
					throw;
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003154 File Offset: 0x00001354
		private static ADUser GetUserFromCache(X509Identifier certificateId)
		{
			if (!CertificateHeaderAuthModule.IsUserCacheEnabled())
			{
				return null;
			}
			return CertificateHeaderAuthModule.certCache.GetUser(certificateId);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000316A File Offset: 0x0000136A
		private static void AddUserToCache(X509Identifier certificateId, ADUser user)
		{
			if (!CertificateHeaderAuthModule.IsUserCacheEnabled())
			{
				return;
			}
			CertificateHeaderAuthModule.certCache.AddUser(certificateId, user);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003180 File Offset: 0x00001380
		private static bool IsUserCacheEnabled()
		{
			return CertificateHeaderAuthModule.certCache != null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000318D File Offset: 0x0000138D
		private static X509Identifier CreateCertificateIdentity(HttpRequest request)
		{
			return new X509Identifier(CertificateHeaderAuthModule.FixCertificateDN(request.Headers["X-Exchange-PowerShell-Client-Cert-Issuer"]), CertificateHeaderAuthModule.FixCertificateDN(request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000031C0 File Offset: 0x000013C0
		private static string FixCertificateDN(string originalDn)
		{
			if (!string.IsNullOrEmpty(originalDn))
			{
				StringBuilder stringBuilder = new StringBuilder(originalDn);
				if (originalDn.Contains(",ST="))
				{
					stringBuilder.Replace(",ST=", ",S=");
				}
				if (originalDn.Contains("emailAddress="))
				{
					stringBuilder.Replace("emailAddress=", "E=");
				}
				if (!originalDn.Contains(", "))
				{
					stringBuilder.Replace(",", ", ");
				}
				return stringBuilder.ToString();
			}
			return originalDn;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003240 File Offset: 0x00001440
		private static bool IsValidCertificateHeaderRequest(HttpRequest request)
		{
			return !string.IsNullOrEmpty(request.Headers["Authorization"]) && request.Headers["Authorization"].Equals("http://schemas.dmtf.org/wbem/wsman/1/wsman/secprofile/https/mutual") && !string.IsNullOrEmpty(request.Headers["X-Exchange-PowerShell-Client-Cert-Issuer"]) && !string.IsNullOrEmpty(request.Headers["X-Exchange-PowerShell-Client-Cert-Subject"]);
		}

		// Token: 0x04000015 RID: 21
		private const string WSManCertAuthorizationHeader = "http://schemas.dmtf.org/wbem/wsman/1/wsman/secprofile/https/mutual";

		// Token: 0x04000016 RID: 22
		private const string CertificateIssuerHeaderName = "X-Exchange-PowerShell-Client-Cert-Issuer";

		// Token: 0x04000017 RID: 23
		private const string CertificateSubjectHeaderName = "X-Exchange-PowerShell-Client-Cert-Subject";

		// Token: 0x04000018 RID: 24
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.CertAuthTracer.Category, "MSExchange Certificate Authentication Module");

		// Token: 0x04000019 RID: 25
		private static readonly EventHandler OnAuthenticateRequestHandler = new EventHandler(CertificateHeaderAuthModule.OnAuthenticateRequest);

		// Token: 0x0400001A RID: 26
		private static CertificateADUserCache certCache = null;

		// Token: 0x0400001B RID: 27
		private static int maxRetryForADTransient;
	}
}
