using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.CertificateAuthentication.EventLog;
using Microsoft.Exchange.Configuration.CertificateAuthentication.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.RedirectionModule;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.CertificateAuthentication;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication
{
	// Token: 0x02000003 RID: 3
	public class CertificateAuthenticationModule : IHttpModule
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000221C File Offset: 0x0000041C
		static CertificateAuthenticationModule()
		{
			if (!int.TryParse(ConfigurationManager.AppSettings["CertificateAuthentication.MaxRetryForADTransient"], out CertificateAuthenticationModule.maxRetryForADTransient))
			{
				CertificateAuthenticationModule.maxRetryForADTransient = 2;
			}
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["CertificateAuthentication.UserCacheMappingMaximumSize"], out num) || num < 0)
			{
				num = 300;
			}
			if (num != 0)
			{
				int num2;
				if (!int.TryParse(ConfigurationManager.AppSettings["CertificateAuthentication.UserCacheMappingExpirationInHours"], out num2) || num2 < 0)
				{
					num2 = 4;
				}
				CertificateAuthenticationModule.certCache = new CertificateADUserCache(num2, num);
			}
			bool.TryParse(ConfigurationManager.AppSettings["CertificateAuthenticationModule.CafeProxy"], out CertificateAuthenticationModule.cafeProxy);
			if (DatacenterRegistry.IsForefrontForOffice())
			{
				AppDomain.CurrentDomain.AssemblyResolve += CertificateAuthenticationModule.CurrentDomain_AssemblyResolve;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000231A File Offset: 0x0000051A
		void IHttpModule.Init(HttpApplication application)
		{
			application.AuthenticateRequest += CertificateAuthenticationModule.OnAuthenticateRequestHandler;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002327 File Offset: 0x00000527
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000232C File Offset: 0x0000052C
		internal static ADUser ResolveCertificate(X509Identifier certificate, string orgName)
		{
			Logger.EnterFunction(ExTraceGlobals.CertAuthTracer, "ResolveCertificate");
			ADSessionSettings sessionSettings = null;
			if (string.IsNullOrEmpty(orgName))
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			else
			{
				try
				{
					sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(orgName);
				}
				catch (CannotResolveTenantNameException ex)
				{
					Logger.LogError(string.Format("Can not resolve the organization based on org name {0} provided in requesting url.", orgName), ex);
					Logger.LogEvent(CertificateAuthenticationModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_OrganizationNotFound, null, new object[]
					{
						ex,
						orgName
					});
					Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "ResolveCertificate");
					HttpLogger.SafeAppendGenericError("ResolveCertificate", ex.ToString(), false);
					return null;
				}
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 193, "ResolveCertificate", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\CertificateAuthentication\\CertificateAuthenticationModule.cs");
			ADUser result = (ADUser)tenantOrRootOrgRecipientSession.FindByCertificate(certificate);
			Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "ResolveCertificate");
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000240C File Offset: 0x0000060C
		private static void OnAuthenticateRequest(object source, EventArgs args)
		{
			Logger.EnterFunction(ExTraceGlobals.CertAuthTracer, "OnAuthenticateRequest");
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Request.IsAuthenticated)
			{
				Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "OnAuthenticateRequest");
				return;
			}
			if (context.Request.ClientCertificate == null || !context.Request.ClientCertificate.IsPresent)
			{
				Logger.LogVerbose("No client certificate is found in authenticated request");
				Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "OnAuthenticateRequest");
				return;
			}
			CertificateAuthenticationModule.InternalOnAuthenticate(context);
			Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "OnAuthenticateRequest");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024A4 File Offset: 0x000006A4
		private static void InternalOnAuthenticate(HttpContext context)
		{
			Logger.EnterFunction(ExTraceGlobals.CertAuthTracer, "InternalOnAuthenticate");
			HttpRequest request = context.Request;
			Logger.LogVerbose("Request of Authentication for certificate {0}.", new object[]
			{
				request.ClientCertificate.Subject
			});
			int i = 0;
			ADUser aduser = null;
			NameValueCollection urlProperties = RedirectionHelper.GetUrlProperties(request.Url);
			string orgName = urlProperties[CertificateAuthenticationModule.OrganizationDomain];
			while (i < CertificateAuthenticationModule.maxRetryForADTransient)
			{
				try
				{
					aduser = CertificateAuthenticationModule.ResolveCertificateFromCacheOrAD(request.ClientCertificate, orgName);
					if (aduser != null && !string.IsNullOrEmpty(aduser.UserPrincipalName))
					{
						CertificateAuthenticationFaultInjection.FaultInjectionTracer.TraceTest<string>(3745918269U, aduser.UserPrincipalName);
					}
					break;
				}
				catch (ADTransientException ex)
				{
					if (i == 0)
					{
						Logger.LogEvent(CertificateAuthenticationModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_TransientError, null, new object[]
						{
							ex,
							request.ClientCertificate.Subject
						});
						HttpLogger.SafeAppendGenericInfo("ResolveCertificateFromCacheOrAD-TransientException", ex.Message);
					}
					if (i == CertificateAuthenticationModule.maxRetryForADTransient - 1)
					{
						HttpLogger.SafeAppendGenericError("ResolveCertificateFromCacheOrAD", ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
					}
					Logger.LogError(string.Format("AD Transient Error when processing certificate authentication {0}.", request.ClientCertificate.Subject), ex);
				}
				catch (Exception ex2)
				{
					HttpLogger.SafeAppendGenericError("CertAuthNModule.InternalOnAuthenticate", ex2, new Func<Exception, bool>(KnownException.IsUnhandledException));
					Logger.LogEvent(CertificateAuthenticationModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_ServerError, null, new object[]
					{
						ex2,
						request.ClientCertificate.Subject
					});
					Logger.LogError(string.Format("Unknown error when processing certificate authentication {0}.", request.ClientCertificate.Subject), ex2);
					if (GrayException.IsGrayException(ex2))
					{
						ExWatson.SendReport(ex2, ReportOptions.DoNotCollectDumps, null);
					}
					CertificateAuthenticationModule.ReportCustomError(context.Response, HttpStatusCode.InternalServerError, 200, Strings.UnknownInternalError(request.ClientCertificate.Subject));
				}
				i++;
			}
			if (i > 0)
			{
				HttpLogger.SafeAppendGenericInfo("ResolveCertificateFromCacheOrAD-Retry", i.ToString());
			}
			if (i >= CertificateAuthenticationModule.maxRetryForADTransient)
			{
				CertificateAuthenticationModule.ReportCustomError(context.Response, HttpStatusCode.InternalServerError, 201, Strings.ADTransientError(request.ClientCertificate.Subject));
			}
			else if (aduser == null)
			{
				HttpLogger.SafeAppendGenericError("ResolveCertificateFromCacheOrAD-ResolvedUser", "null", false);
				if (request.Headers["Authorization"] == "http://schemas.dmtf.org/wbem/wsman/1/wsman/secprofile/https/mutual")
				{
					CertificateAuthenticationModule.ReportCustomError(context.Response, HttpStatusCode.BadRequest, 102, Strings.UserNotFound(request.ClientCertificate.Subject));
				}
			}
			else
			{
				Logger.LogVerbose("User correctly authenticated and linked to Certificate {0}.", new object[]
				{
					request.ClientCertificate.Subject
				});
				if (i > 0)
				{
					Logger.LogEvent(CertificateAuthenticationModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_TransientRecovery, null, new object[]
					{
						request.ClientCertificate.Subject,
						i
					});
				}
				X509Identifier certId = CertificateAuthenticationModule.CreateCertificateIdentity(request.ClientCertificate);
				CertificateAuthenticationModule.SetAuthenticatedInfo(context, aduser, certId);
			}
			Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "InternalOnAuthenticate");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000027B4 File Offset: 0x000009B4
		private static ADUser ResolveCertificateFromCacheOrAD(HttpClientCertificate certificate, string orgName)
		{
			Logger.EnterFunction(ExTraceGlobals.CertAuthTracer, "ResolveCertificateFromCacheOrAD");
			X509Identifier x509Identifier = CertificateAuthenticationModule.CreateCertificateIdentity(certificate);
			ADUser aduser = CertificateAuthenticationModule.GetUserFromCache(x509Identifier);
			if (aduser == null)
			{
				aduser = CertificateAuthenticationModule.ResolveCertificate(x509Identifier, orgName);
				if (aduser != null)
				{
					CertificateAuthenticationModule.AddUserToCache(x509Identifier, aduser);
				}
			}
			else
			{
				HttpLogger.SafeAppendGenericInfo("ResolveCertificateFromCacheOrAD", "Cache");
			}
			if (aduser == null)
			{
				Logger.LogEvent(CertificateAuthenticationModule.eventLogger, TaskEventLogConstants.Tuple_CertAuth_UserNotFound, certificate.Subject, new object[]
				{
					certificate.Subject
				});
				Logger.LogVerbose("Found no user by certificate {0}", new object[]
				{
					certificate.Subject
				});
			}
			else
			{
				Logger.LogVerbose("Found user {0} by certificate {1}", new object[]
				{
					aduser.Name,
					certificate.Subject
				});
			}
			Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "ResolveCertificateFromCacheOrAD");
			return aduser;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002884 File Offset: 0x00000A84
		private static void ReportCustomError(HttpResponse response, HttpStatusCode status, int subStatus, string errorMessage)
		{
			Logger.LogVerbose("Reporting HTTP error: Status - {0}, SubStatus - {1}, Message - {2}", new object[]
			{
				status,
				subStatus,
				errorMessage
			});
			string fcSubInfo = "Unknown";
			if (subStatus != 102)
			{
				switch (subStatus)
				{
				case 200:
					fcSubInfo = "UnknownInternalError";
					break;
				case 201:
					fcSubInfo = "TransientServerError";
					break;
				}
			}
			else
			{
				fcSubInfo = "UserNotFound";
			}
			WinRMInfo.SetFailureCategoryInfo(response.Headers, FailureCategory.Certificate, fcSubInfo);
			response.Clear();
			response.StatusCode = (int)status;
			response.SubStatusCode = subStatus;
			response.ContentType = "application/soap+xml;charset=UTF-8";
			response.TrySkipIisCustomErrors = true;
			if (!string.IsNullOrEmpty(errorMessage))
			{
				response.Write(errorMessage);
			}
			response.End();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000293C File Offset: 0x00000B3C
		private static void SetAuthenticatedInfo(HttpContext context, ADUser authenticatedUser, X509Identifier certId)
		{
			Logger.EnterFunction(ExTraceGlobals.CertAuthTracer, "SetAuthenticatedInfo");
			IIdentity identity;
			if (CertificateAuthenticationModule.cafeProxy)
			{
				CertificateSidTokenAccessor certificateSidTokenAccessor = CertificateSidTokenAccessor.Create(authenticatedUser, certId);
				context.Items["Item-CommonAccessToken"] = certificateSidTokenAccessor.GetToken();
				identity = new GenericSidIdentity(authenticatedUser.Sid.ToString(), "Certificate", authenticatedUser.Sid, certificateSidTokenAccessor.PartitionId);
			}
			else
			{
				context.Items["AuthType"] = AccessTokenType.CertificateSid;
				if (authenticatedUser.RecipientTypeDetails == RecipientTypeDetails.LinkedUser)
				{
					identity = new GenericIdentity(authenticatedUser.Sid.ToString());
				}
				else
				{
					string sUserPrincipalName = string.Format("{0}@{1}", authenticatedUser.SamAccountName, authenticatedUser.Id.GetPartitionId().ForestFQDN);
					identity = new WindowsIdentity(sUserPrincipalName);
				}
			}
			string name = authenticatedUser.Name;
			if (!string.IsNullOrEmpty(name))
			{
				context.Items["AuthenticatedUser"] = name;
			}
			AuthCommon.SetHttpContextADRawEntry(context, authenticatedUser);
			if (!OrganizationId.ForestWideOrgId.Equals(authenticatedUser.OrganizationId))
			{
				context.Items[CertificateAuthenticationModule.TenantCertificateOrganizaitonItemName] = authenticatedUser.OrganizationId.OrganizationalUnit.Name;
			}
			context.User = new GenericPrincipal(identity, new string[0]);
			Logger.ExitFunction(ExTraceGlobals.CertAuthTracer, "SetAuthenticatedInfo");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002A7C File Offset: 0x00000C7C
		private static ADUser GetUserFromCache(X509Identifier certificateId)
		{
			if (!CertificateAuthenticationModule.IsUserCacheEnabled())
			{
				return null;
			}
			return CertificateAuthenticationModule.certCache.GetUser(certificateId);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002A92 File Offset: 0x00000C92
		private static void AddUserToCache(X509Identifier certificateId, ADUser user)
		{
			if (!CertificateAuthenticationModule.IsUserCacheEnabled())
			{
				return;
			}
			CertificateAuthenticationModule.certCache.AddUser(certificateId, user);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002AA8 File Offset: 0x00000CA8
		private static bool IsUserCacheEnabled()
		{
			return CertificateAuthenticationModule.certCache != null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002AB5 File Offset: 0x00000CB5
		private static X509Identifier CreateCertificateIdentity(HttpClientCertificate certificate)
		{
			return new X509Identifier(CertificateAuthenticationModule.InvertDn(certificate.Issuer), CertificateAuthenticationModule.InvertDn(certificate.Subject));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private static string InvertDn(string dn)
		{
			if (string.IsNullOrEmpty(dn))
			{
				return dn;
			}
			StringBuilder stringBuilder = new StringBuilder(dn.Length);
			int i = 0;
			int num = 0;
			bool flag = false;
			while (i < dn.Length)
			{
				char c = dn[i];
				if (flag)
				{
					if (c == '"')
					{
						flag = false;
					}
				}
				else if (c == '"')
				{
					flag = true;
				}
				else if (c == ',')
				{
					stringBuilder.Insert(0, ", " + dn.Substring(num, i - num).Trim());
					num = i + 1;
				}
				i++;
			}
			stringBuilder.Insert(0, dn.Substring(num).Trim());
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B74 File Offset: 0x00000D74
		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\Setup", "v15")))
				{
					if (registryKey == null)
					{
						Logger.LogVerbose("Cannot find Install path");
						return null;
					}
					string text = (string)registryKey.GetValue("MsiInstallPath");
					if (string.IsNullOrWhiteSpace(text))
					{
						Logger.LogVerbose("MsiIntallPath is not set");
						return null;
					}
					if (args.Name.IndexOf(',') < 0)
					{
						string path = args.Name + ".dll";
						string path2 = Path.Combine(text, "Bin");
						string text2 = Path.Combine(path2, path);
						if (File.Exists(text2))
						{
							Assembly result = Assembly.LoadFrom(text2);
							Logger.LogVerbose("Assembly {0} is loaded", new object[]
							{
								text2
							});
							return result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogVerbose("Got exception in CurrentDomain_AssemblyResolve(). " + ex.ToString());
			}
			return null;
		}

		// Token: 0x04000008 RID: 8
		private const string WSManCertAuthorizationHeader = "http://schemas.dmtf.org/wbem/wsman/1/wsman/secprofile/https/mutual";

		// Token: 0x04000009 RID: 9
		private const int UnknownInternalError = 200;

		// Token: 0x0400000A RID: 10
		private const int TransientServerError = 201;

		// Token: 0x0400000B RID: 11
		private const int UserNotFound = 102;

		// Token: 0x0400000C RID: 12
		internal static readonly string TenantCertificateOrganizaitonItemName = "Cert-MemberOrg";

		// Token: 0x0400000D RID: 13
		internal static readonly string OrganizationDomain = "OrganizationDomain";

		// Token: 0x0400000E RID: 14
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.CertAuthTracer.Category, "MSExchange Certificate Authentication Module");

		// Token: 0x0400000F RID: 15
		private static readonly EventHandler OnAuthenticateRequestHandler = new EventHandler(CertificateAuthenticationModule.OnAuthenticateRequest);

		// Token: 0x04000010 RID: 16
		private static bool cafeProxy = false;

		// Token: 0x04000011 RID: 17
		private static CertificateADUserCache certCache = null;

		// Token: 0x04000012 RID: 18
		private static int maxRetryForADTransient;
	}
}
