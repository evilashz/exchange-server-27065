using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.EventLog;
using Microsoft.Exchange.Configuration.RedirectionModule;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DelegatedAuthentication;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.Compliance;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000002 RID: 2
	public class DelegatedAuthenticationModule : IHttpModule
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static DelegatedAuthenticationModule()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				DelegatedAuthenticationModule.processName = currentProcess.MainModule.ModuleName;
				DelegatedAuthenticationModule.processId = currentProcess.Id;
			}
			DelegatedAuthenticationModule.podRedirectTemplate = ConfigurationManager.AppSettings["PodRedirectTemplate"];
			DelegatedAuthenticationModule.siteRedirectTemplate = ConfigurationManager.AppSettings["SiteRedirectTemplate"];
			int.TryParse(ConfigurationManager.AppSettings["PodSiteStartRange"], out DelegatedAuthenticationModule.podSiteStartRange);
			int.TryParse(ConfigurationManager.AppSettings["PodSiteEndRange"], out DelegatedAuthenticationModule.podSiteEndRange);
			Enum.TryParse<DelegatedAuthenticationModule.Protocol>(ConfigurationManager.AppSettings["DelegatedAutentication.Protocol"], out DelegatedAuthenticationModule.protocol);
			TimeSpan tokenLifetime;
			if (TimeSpan.TryParse(ConfigurationManager.AppSettings["DelegatedAutentication.TokenLifetime"], out tokenLifetime))
			{
				DelegatedSecurityToken.TokenLifetime = tokenLifetime;
			}
			DelegatedAuthenticationModule.appDomainAppVirtualPath = (HttpRuntime.AppDomainAppVirtualPath ?? string.Empty);
			DelegatedAuthenticationModule.isRedirectToLocalServerEnabled = StringComparer.OrdinalIgnoreCase.Equals("True", ConfigurationManager.AppSettings["EnableRedirectToLocalServer"]);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000223C File Offset: 0x0000043C
		void IHttpModule.Init(HttpApplication application)
		{
			application.AuthenticateRequest += DelegatedAuthenticationModule.OnAuthenticateRequestHandler;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002249 File Offset: 0x00000449
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000224C File Offset: 0x0000044C
		internal static byte[] EncryptSecurityToken(string userId, string securityToken)
		{
			byte[] currentSecretKey = GccUtils.DatacenterServerAuthentication.CurrentSecretKey;
			if (currentSecretKey == null)
			{
				throw new CannotResolveCurrentKeyException(true);
			}
			byte[] result;
			using (HMACSHA256Cng hmacsha256Cng = new HMACSHA256Cng(currentSecretKey))
			{
				byte[] key = hmacsha256Cng.ComputeHash(Encoding.UTF8.GetBytes(userId));
				SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
				symmetricAlgorithm.Padding = PaddingMode.ISO10126;
				symmetricAlgorithm.Key = key;
				symmetricAlgorithm.IV = GccUtils.DatacenterServerAuthentication.CurrentIVKey;
				MemoryStream memoryStream = new MemoryStream();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
				{
					byte[] bytes = Encoding.UTF8.GetBytes(securityToken);
					cryptoStream.Write(bytes, 0, bytes.Length);
					cryptoStream.Flush();
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002328 File Offset: 0x00000528
		internal static string DecryptSecurityToken(string userId, byte[] encryptedToken, byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (iv == null)
			{
				throw new ArgumentNullException("iv");
			}
			string @string;
			using (HMACSHA256Cng hmacsha256Cng = new HMACSHA256Cng(key))
			{
				byte[] key2 = hmacsha256Cng.ComputeHash(Encoding.UTF8.GetBytes(userId));
				SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
				symmetricAlgorithm.Padding = PaddingMode.ISO10126;
				symmetricAlgorithm.Key = key2;
				symmetricAlgorithm.IV = iv;
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
				cryptoStream.Write(encryptedToken, 0, encryptedToken.Length);
				cryptoStream.Close();
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023E0 File Offset: 0x000005E0
		internal static bool IsSecurityTokenPresented(HttpRequest request)
		{
			return DelegatedAuthenticationModule.GetSecurityTokenProperty(request) != null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023F0 File Offset: 0x000005F0
		private static void InternalOnAuthenticate(HttpContext context)
		{
			HttpRequest request = context.Request;
			if (DelegatedAuthenticationModule.IsOAuthLinkedAccount(context))
			{
				DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. OAuth Linked account.");
				return;
			}
			string userId = DelegatedAuthenticationModule.GetUserId(context);
			string targetTenant = DelegatedAuthenticationModule.GetTargetTenant(request);
			DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. uesrId = {0}; targetOrg = {1}", new object[]
			{
				userId,
				targetTenant
			});
			if (string.IsNullOrWhiteSpace(targetTenant))
			{
				if (!DelegatedAuthenticationModule.IsCurrentStackECP())
				{
					DelegatedAuthenticationModule.LogDebug("Target Organization not present for user {0}", new object[]
					{
						userId
					});
				}
				DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. TargetOrg is empty");
				return;
			}
			if (string.IsNullOrWhiteSpace(userId))
			{
				DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_FailedToResolveCurrentUser, targetTenant, new object[0]);
				DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "UserId is empty");
				return;
			}
			string securityTokenProperty = DelegatedAuthenticationModule.GetSecurityTokenProperty(context.Request);
			bool flag = request.Headers["msExchCafeForceRouteToLogonAccount"] == "1";
			if (!flag && DelegatedAuthenticationModule.ResolvePreAuthenticatedUserFromCache(context, targetTenant, userId, securityTokenProperty))
			{
				DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. Succeeded to ResolvePreAuthenticatedUserFromCache");
				return;
			}
			byte[] array = null;
			try
			{
				if (!string.IsNullOrEmpty(securityTokenProperty))
				{
					array = Convert.FromBase64String(securityTokenProperty);
				}
			}
			catch (FormatException ex)
			{
				DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_FailedToDecodeBase64SecurityToken, userId, new object[]
				{
					userId,
					request.Url,
					ex
				});
				DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "Failed to decode base64 security token");
				return;
			}
			IPrincipal user = context.User;
			Uri uri;
			if (request.Headers[WellKnownHeader.XIsFromCafe] == "1")
			{
				uri = new Uri(request.Url, request.Headers[WellKnownHeader.MsExchProxyUri]);
			}
			else if (request.Headers["msExchOriginalUrl"] != null)
			{
				uri = new Uri(request.Url, request.Headers["msExchOriginalUrl"]);
			}
			else
			{
				uri = request.Url;
			}
			DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. originalUrl = {0}", new object[]
			{
				uri
			});
			if (array != null)
			{
				DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Security Token Present on the Uri");
				ExchangeConfigurationUnit exchangeConfigurationUnit;
				if (!DelegatedAuthenticationModule.TryResolveConfigurationUnit(context.Response, userId, targetTenant, out exchangeConfigurationUnit))
				{
					return;
				}
				if (exchangeConfigurationUnit == null)
				{
					DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_FailedToResolveTargetOrganization, userId, new object[]
					{
						targetTenant,
						userId
					});
					DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "TargetOrgCU is empty");
					return;
				}
				string securityToken;
				if (!DelegatedAuthenticationModule.TryDecryptBase64EncryptedSecurityToken(context.Response, userId, targetTenant, array, out securityToken))
				{
					return;
				}
				DelegatedSecurityToken delegatedSecurityToken = DelegatedSecurityToken.Parse(securityToken);
				if (delegatedSecurityToken.IsExpired())
				{
					DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. The security token is expired");
					DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_ExpiredSecurityToken, null, new object[]
					{
						userId,
						delegatedSecurityToken.UTCCreationTime,
						DelegatedAuthenticationModule.IsCurrentStackECP()
					});
					if (DelegatedAuthenticationModule.IsCurrentStackECP() && DelegatedAuthenticationModule.TryRedirectEcpForSecurityTokenRenewal(context, targetTenant, uri))
					{
						return;
					}
					DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "The security token is expired");
					return;
				}
				else
				{
					if (delegatedSecurityToken.PartnerGroupIds == null || delegatedSecurityToken.PartnerGroupIds.Length == 0)
					{
						DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_NoGroupMembershipOnSecurityToken, userId, new object[]
						{
							userId
						});
						DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "No group membership on security token");
						return;
					}
					foreach (string text in delegatedSecurityToken.PartnerGroupIds)
					{
						DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Member of partner group {0}", new object[]
						{
							text
						});
					}
					DelegatedAuthenticationModule.AddCookie(context, targetTenant, DelegatedAuthenticationModule.securityTokenUriPropertyName, securityTokenProperty);
					DelegatedPrincipal delegatedPrincipal = DelegatedAuthenticationModule.CreateDelegatedPrincipal(user, userId, exchangeConfigurationUnit.ToString(), delegatedSecurityToken);
					context.User = delegatedPrincipal;
					DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. Authentication succeeded. userid = {0}; targetOrgCU = {1}", new object[]
					{
						userId,
						exchangeConfigurationUnit.DistinguishedName
					});
					if (!DelegatedPrincipalCache.TrySetEntry(targetTenant, userId, securityTokenProperty, new DelegatedPrincipalCacheData(delegatedPrincipal, delegatedSecurityToken.UTCExpirationTime)))
					{
						DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Delegated Principal Cache Is Full");
						DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_DelegatedPrincipalCacheIsFull, userId, new object[]
						{
							DateTime.UtcNow,
							DelegatedPrincipalCache.NextScheduleCacheCleanUp()
						});
						return;
					}
				}
			}
			else
			{
				DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Scurity Token Not Present on the Uri");
				if (!(context.Items["Item-CommonAccessToken"] is CommonAccessToken) && !DelegatedAuthenticationModule.IsUserPresentOnForest(context.User))
				{
					DelegatedAuthenticationModule.LogDebug("Authenticated User {0} is not present on the current forest, skipping the DelegatedAuthentication", new object[]
					{
						userId
					});
					return;
				}
				ExchangeConfigurationUnit exchangeConfigurationUnit2 = null;
				if (!DelegatedAuthenticationModule.TryResolveConfigurationUnit(context.Response, userId, targetTenant, out exchangeConfigurationUnit2))
				{
					return;
				}
				Uri uri2 = null;
				if (exchangeConfigurationUnit2 != null && DelegatedAuthenticationModule.ShouldRedirectToLocalServer(context, targetTenant, true))
				{
					uri2 = new Uri(context.Request.Url, uri);
				}
				else if (exchangeConfigurationUnit2 == null || flag)
				{
					if (exchangeConfigurationUnit2 != null)
					{
						uri2 = uri;
						DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Use original url as forestRedirectUrl = {0}", new object[]
						{
							uri2
						});
					}
					else
					{
						uri2 = RedirectionHelper.GetRedirectUrlForTenantForest(targetTenant, DelegatedAuthenticationModule.podRedirectTemplate, uri, DelegatedAuthenticationModule.podSiteStartRange, DelegatedAuthenticationModule.podSiteEndRange);
						if (uri2 == null)
						{
							DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_CannotResolveForestRedirection, userId, new object[]
							{
								userId,
								targetTenant,
								uri
							});
							DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "forestRedirectionUrl is empty");
							return;
						}
						DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. forestRedirectUrl = {0}", new object[]
						{
							uri2
						});
					}
				}
				ADGroup[] array2 = null;
				ADRawEntry adrawEntry = null;
				ExchangeConfigurationUnit exchangeConfigurationUnit3 = null;
				Exception ex2 = null;
				try
				{
					adrawEntry = DelegatedAuthenticationModule.ResolveADUserFromPrincipal(user);
					if (exchangeConfigurationUnit2 != null && exchangeConfigurationUnit2.OrganizationId.Equals((OrganizationId)adrawEntry[ADObjectSchema.OrganizationId]))
					{
						DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. the user is requesting a delegated connection to its own organization {0}", new object[]
						{
							exchangeConfigurationUnit2.DistinguishedName
						});
						return;
					}
					array2 = DelegatedAuthenticationModule.ResolveDelegatedGroupForUser(adrawEntry);
					if (OrganizationId.ForestWideOrgId.Equals(adrawEntry[ADObjectSchema.OrganizationId]))
					{
						DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "The connectingUser is in the first org");
						return;
					}
					exchangeConfigurationUnit3 = DelegatedAuthenticationModule.GetADOrganizationById((OrganizationId)adrawEntry[ADObjectSchema.OrganizationId]);
					if (exchangeConfigurationUnit3 == null)
					{
						DelegatedAuthenticationModule.SendServerError(context.Response, userId, new CannotResolveUserTenantException(adrawEntry[ADObjectSchema.OrganizationId].ToString()));
						return;
					}
				}
				catch (DataSourceOperationException ex3)
				{
					ex2 = ex3;
				}
				catch (TransientException ex4)
				{
					ex2 = ex4;
				}
				catch (DataValidationException ex5)
				{
					ex2 = ex5;
				}
				if (ex2 != null)
				{
					DelegatedAuthenticationModule.SendServerError(context.Response, userId, ex2);
					return;
				}
				if (array2 == null || array2.Length == 0)
				{
					DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "the user doesn't belong to any groups.");
					return;
				}
				foreach (ADGroup adgroup in array2)
				{
					DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. The user is a member of group {0}", new object[]
					{
						adgroup
					});
				}
				string[] array4 = DelegatedAuthenticationModule.ExtractDirectoryObjectId(array2);
				if (array4 == null || array4.Length == 0)
				{
					DelegatedAuthenticationModule.SendAccessDenied(context.Response, userId, "cannot find any linked groups");
					return;
				}
				foreach (string text2 in array4)
				{
					DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Found linked group {0}", new object[]
					{
						text2
					});
				}
				DelegatedSecurityToken delegatedSecurityToken2 = new DelegatedSecurityToken(((string)adrawEntry[ADRecipientSchema.DisplayName]) ?? string.Empty, exchangeConfigurationUnit3.ExternalDirectoryOrganizationId, array4);
				if (uri2 == null)
				{
					DelegatedPrincipal delegatedPrincipal2 = DelegatedAuthenticationModule.CreateDelegatedPrincipal(user, userId, exchangeConfigurationUnit2.ToString(), delegatedSecurityToken2);
					context.User = delegatedPrincipal2;
					DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. Authentication succeeded. userid = {0}; targetOrgCU = {1}", new object[]
					{
						userId,
						exchangeConfigurationUnit2.DistinguishedName
					});
					if (!DelegatedPrincipalCache.TrySetEntry(targetTenant, userId, securityTokenProperty, new DelegatedPrincipalCacheData(delegatedPrincipal2, delegatedSecurityToken2.UTCExpirationTime)))
					{
						DelegatedAuthenticationModule.LogDebug("In OnAuthenticateRequest. Delegated Principal Cache Is Full");
						DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_DelegatedPrincipalCacheIsFull, userId, new object[]
						{
							DateTime.UtcNow,
							DelegatedPrincipalCache.NextScheduleCacheCleanUp()
						});
						return;
					}
				}
				else
				{
					string encryptedSecurityToken = null;
					if (!DelegatedAuthenticationModule.TryGenerateBase64EncryptedSecurityToken(context.Response, userId, delegatedSecurityToken2, out encryptedSecurityToken))
					{
						return;
					}
					DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. Forest redirection happends. Redirect to {0}.", new object[]
					{
						uri2
					});
					context.Response.Redirect(DelegatedAuthenticationModule.AppendSecurityTokenToRedirectionUri(uri2, encryptedSecurityToken));
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002C00 File Offset: 0x00000E00
		private static void OnAuthenticateRequest(object source, EventArgs args)
		{
			DelegatedAuthenticationModule.LogDebug("Enter OnAuthenticateRequest");
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!context.Request.IsAuthenticated)
			{
				if (!DelegatedAuthenticationModule.IsCurrentStackECP())
				{
					DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_RequestNotAuthenticated, context.Request.Path, new object[0]);
				}
				DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest. The original incomming request isn't authenticated");
				return;
			}
			DelegatedAuthenticationModule.InternalOnAuthenticate(context);
			DelegatedAuthenticationModule.LogDebug("Exit OnAuthenticateRequest");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002C70 File Offset: 0x00000E70
		private static bool TryRedirectEcpForSecurityTokenRenewal(HttpContext context, string targetOrg, Uri originalUrl)
		{
			bool result = false;
			DelegatedAuthenticationModule.DeleteCookie(context, targetOrg, DelegatedAuthenticationModule.securityTokenUriPropertyName);
			if (context.Request.RequestType == "GET")
			{
				originalUrl = new Uri(DelegatedAuthenticationModule.RemoveParameterFromUrlForEcp(originalUrl.ToString(), DelegatedAuthenticationModule.securityTokenUriPropertyName));
				Uri uri;
				if (DelegatedAuthenticationModule.ShouldRedirectToLocalServer(context, targetOrg, false))
				{
					string text = originalUrl.ToString();
					if (!text.Contains("redirtolocal="))
					{
						text = DelegatedAuthenticationModule.AppendParameterToUrl(text, "&", "redirtolocal", "1");
					}
					uri = new Uri(text);
				}
				else
				{
					uri = RedirectionHelper.GetRedirectUrlForTenantForest(DelegatedAuthenticationModule.GetUserDomainName(context), DelegatedAuthenticationModule.podRedirectTemplate, originalUrl, DelegatedAuthenticationModule.podSiteStartRange, DelegatedAuthenticationModule.podSiteEndRange);
				}
				if (uri != null)
				{
					DelegatedAuthenticationModule.LogDebug("DelegatedAuthenticationModule::TryRedirectEcpForSecurityTokenRenewal. Redirect to {0}.", new object[]
					{
						uri
					});
					context.Response.Redirect(uri.AbsoluteUri);
					result = true;
				}
				return result;
			}
			throw new DelegatedSecurityTokenExpiredException();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002D50 File Offset: 0x00000F50
		private static bool ShouldRedirectToLocalServer(HttpContext context, string targetOrg, bool addCookieIfNotPresent)
		{
			bool flag = DelegatedAuthenticationModule.isRedirectToLocalServerEnabled;
			if (flag && context.Request.Cookies["redirtolocal"] == null)
			{
				if (context.Request.QueryString["redirtolocal"] != null)
				{
					if (addCookieIfNotPresent)
					{
						DelegatedAuthenticationModule.AddCookie(context, targetOrg, "redirtolocal", "1");
					}
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002DAD File Offset: 0x00000FAD
		private static void AddCookie(HttpContext context, string targetOrg, string key, string value)
		{
			DelegatedAuthenticationModule.SetCookie(context, targetOrg, key, value, false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002DB9 File Offset: 0x00000FB9
		private static void DeleteCookie(HttpContext context, string targetOrg, string key)
		{
			DelegatedAuthenticationModule.SetCookie(context, targetOrg, key, string.Empty, true);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002DCC File Offset: 0x00000FCC
		private static void SetCookie(HttpContext context, string targetOrg, string key, string value, bool remove)
		{
			HttpCookie httpCookie = new HttpCookie(key, value);
			if (string.IsNullOrEmpty(targetOrg))
			{
				httpCookie.Path = DelegatedAuthenticationModule.appDomainAppVirtualPath;
			}
			else
			{
				httpCookie.Path = string.Format("{0}/@{1}/", DelegatedAuthenticationModule.appDomainAppVirtualPath, targetOrg);
			}
			if (remove)
			{
				httpCookie.Expires = (DateTime)ExDateTime.Now.AddYears(-1);
			}
			context.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002E3C File Offset: 0x0000103C
		private static void SendAccessDenied(HttpResponse response, string userId, string reason)
		{
			DelegatedAuthenticationModule.LogDebug("Sending 401 Access for user {0}. {1}", new object[]
			{
				userId,
				reason
			});
			DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_AccessDenied, userId, new object[]
			{
				userId
			});
			WinRMInfo.SetFailureCategoryInfo(response.Headers, FailureCategory.DelegatedAuth, "AccessDenied");
			response.Clear();
			response.StatusCode = 401;
			if (DelegatedAuthenticationModule.IsCurrentStackECP())
			{
				throw new DelegatedAccessDeniedException();
			}
			response.End();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002EB0 File Offset: 0x000010B0
		private static void SendServerError(HttpResponse response, string userId, Exception error)
		{
			DelegatedAuthenticationModule.LogError("There is a server error: {0}", error);
			DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_ServerError, userId, new object[]
			{
				error,
				userId
			});
			WinRMInfo.SetFailureCategoryInfo(response.Headers, FailureCategory.DelegatedAuth, error.GetType().Name);
			response.Clear();
			response.StatusCode = 500;
			if (DelegatedAuthenticationModule.IsCurrentStackECP())
			{
				throw new DelegatedServerErrorException(error);
			}
			response.End();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002F20 File Offset: 0x00001120
		private static bool IsUserPresentOnForest(IPrincipal principal)
		{
			WindowsIdentity windowsIdentity = principal.Identity as WindowsIdentity;
			if (windowsIdentity == null)
			{
				LiveIDIdentity liveIDIdentity = principal.Identity as LiveIDIdentity;
				if (liveIDIdentity != null)
				{
					return true;
				}
			}
			else if (!windowsIdentity.IsSystem)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002F5C File Offset: 0x0000115C
		private static string GetUserDomainName(HttpContext context)
		{
			string userId = DelegatedAuthenticationModule.GetUserId(context);
			if (userId != null && SmtpAddress.IsValidSmtpAddress(userId))
			{
				return SmtpAddress.Parse(userId).Domain;
			}
			return string.Empty;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002F90 File Offset: 0x00001190
		private static string GetUserId(HttpContext context)
		{
			SidOAuthIdentity sidOAuthIdentity = context.User.Identity as SidOAuthIdentity;
			if (sidOAuthIdentity != null)
			{
				return sidOAuthIdentity.Name;
			}
			object obj;
			if ((obj = context.Items["RPSMemberName"]) == null && (obj = context.Items["WLID-MemberName"]) == null)
			{
				obj = (context.Request.Headers["RPSMemberName"] ?? context.GetMemberName());
			}
			return (string)obj;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003004 File Offset: 0x00001204
		private static bool IsOAuthLinkedAccount(HttpContext context)
		{
			SidOAuthIdentity sidOAuthIdentity = context.User.Identity as SidOAuthIdentity;
			return sidOAuthIdentity != null && sidOAuthIdentity.OAuthAccountType == SidOAuthIdentity.AccountType.OAuthLinkedAccount;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003034 File Offset: 0x00001234
		private static bool ResolvePreAuthenticatedUserFromCache(HttpContext context, string targetOrg, string userId, string securityToken)
		{
			DelegatedPrincipalCache.Cleanup();
			DelegatedPrincipalCacheData entry = DelegatedPrincipalCache.GetEntry(targetOrg, userId, securityToken);
			if (entry == null)
			{
				return false;
			}
			if (entry.IsExpired())
			{
				DelegatedPrincipalCache.RemoveEntry(targetOrg, userId, securityToken);
				DelegatedAuthenticationModule.LogDebug("Principal for key {0}\\{1} is removed from the cache due to expiration.", new object[]
				{
					targetOrg,
					userId
				});
				return false;
			}
			context.User = entry.Principal;
			DelegatedAuthenticationModule.LogDebug("Principal for key {0}\\{1} is authenticated from the cache.", new object[]
			{
				targetOrg,
				userId
			});
			return true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000030A8 File Offset: 0x000012A8
		private static bool TryResolveConfigurationUnit(HttpResponse response, string userId, string targetOrganization, out ExchangeConfigurationUnit cu)
		{
			cu = null;
			Exception ex = null;
			try
			{
				Guid externalDirectoryId;
				if (GuidHelper.TryParseGuid(targetOrganization, out externalDirectoryId))
				{
					cu = DelegatedAuthenticationModule.GetADOrganizationByExternalDirectoryId(externalDirectoryId);
				}
				else
				{
					cu = DelegatedAuthenticationModule.GetADOrganizationByName(targetOrganization);
				}
			}
			catch (DataSourceOperationException ex2)
			{
				ex = ex2;
			}
			catch (TransientException ex3)
			{
				ex = ex3;
			}
			catch (DataValidationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				DelegatedAuthenticationModule.SendServerError(response, userId, ex);
				return false;
			}
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003120 File Offset: 0x00001320
		private static bool TryDecryptBase64EncryptedSecurityToken(HttpResponse response, string userId, string targetOrg, byte[] encryptedToken, out string decryptedToken)
		{
			decryptedToken = null;
			if (!DelegatedAuthenticationModule.ValidateSecretKey(true))
			{
				DelegatedAuthenticationModule.SendServerError(response, userId, new CannotResolveCurrentKeyException(true));
				return false;
			}
			try
			{
				decryptedToken = DelegatedAuthenticationModule.DecryptSecurityToken(userId, encryptedToken, GccUtils.DatacenterServerAuthentication.CurrentSecretKey, GccUtils.DatacenterServerAuthentication.CurrentIVKey);
			}
			catch (CryptographicException ex)
			{
				DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_FailedToDecryptSecurityToken, userId, new object[]
				{
					userId,
					targetOrg,
					ex,
					true
				});
			}
			try
			{
				if (decryptedToken == null && GccUtils.DatacenterServerAuthentication.PreviousSecretKey != null)
				{
					if (!DelegatedAuthenticationModule.ValidateSecretKey(false))
					{
						DelegatedAuthenticationModule.SendServerError(response, userId, new CannotResolveCurrentKeyException(false));
						return false;
					}
					decryptedToken = DelegatedAuthenticationModule.DecryptSecurityToken(userId, encryptedToken, GccUtils.DatacenterServerAuthentication.PreviousSecretKey, GccUtils.DatacenterServerAuthentication.PreviousIVKey);
				}
			}
			catch (CryptographicException ex2)
			{
				DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_FailedToDecryptSecurityToken, userId, new object[]
				{
					userId,
					targetOrg,
					ex2,
					false
				});
			}
			if (string.IsNullOrEmpty(decryptedToken))
			{
				DelegatedAuthenticationModule.SendAccessDenied(response, userId, "decryptedToken is empty");
				return false;
			}
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003248 File Offset: 0x00001448
		private static bool TryGenerateBase64EncryptedSecurityToken(HttpResponse response, string userId, DelegatedSecurityToken securityToken, out string encryptedSecurityToken)
		{
			if (!DelegatedAuthenticationModule.ValidateSecretKey(true))
			{
				encryptedSecurityToken = null;
				DelegatedAuthenticationModule.SendServerError(response, userId, new CannotResolveCurrentKeyException(true));
				return false;
			}
			byte[] array = null;
			Exception error = null;
			try
			{
				array = DelegatedAuthenticationModule.EncryptSecurityToken(userId, securityToken.ToString());
			}
			catch (CannotResolveCurrentKeyException ex)
			{
				error = ex;
			}
			catch (CryptographicException ex2)
			{
				error = ex2;
			}
			if (array == null)
			{
				encryptedSecurityToken = null;
				DelegatedAuthenticationModule.SendServerError(response, userId, error);
				return false;
			}
			encryptedSecurityToken = Convert.ToBase64String(array, 0, array.Length, Base64FormattingOptions.None);
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000032C8 File Offset: 0x000014C8
		private static bool ValidateSecretKey(bool currentKey)
		{
			if (currentKey)
			{
				try
				{
					GccUtils.RefreshProxySecretKeys();
				}
				catch (InvalidDatacenterProxyKeyException)
				{
					return false;
				}
				return GccUtils.DatacenterServerAuthentication.CurrentSecretKey != null && GccUtils.DatacenterServerAuthentication.CurrentIVKey != null;
			}
			return GccUtils.DatacenterServerAuthentication.PreviousSecretKey != null && GccUtils.DatacenterServerAuthentication.PreviousIVKey != null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003334 File Offset: 0x00001534
		private static DelegatedPrincipal CreateDelegatedPrincipal(IPrincipal user, string userId, string targetOrg, DelegatedSecurityToken token)
		{
			return new DelegatedPrincipal(DelegatedPrincipal.GetDelegatedIdentity(userId, token.PartnerOrgDirectoryId, targetOrg, token.DisplayName, token.PartnerGroupIds), token.PartnerGroupIds);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000335C File Offset: 0x0000155C
		private static string AppendSecurityTokenToRedirectionUri(Uri redirectionUri, string encryptedSecurityToken)
		{
			string url = redirectionUri.ToString();
			encryptedSecurityToken = Uri.EscapeDataString(encryptedSecurityToken);
			return DelegatedAuthenticationModule.AppendParameterToUrl(url, DelegatedAuthenticationModule.IsCurrentStackECP() ? "&" : ";", DelegatedAuthenticationModule.securityTokenUriPropertyName, encryptedSecurityToken);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003398 File Offset: 0x00001598
		private static string AppendParameterToUrl(string url, string ampStr, string key, string value)
		{
			int capacity = url.Length + key.Length + value.Length + 2;
			StringBuilder stringBuilder = new StringBuilder(url, capacity);
			if (!url.EndsWith("&"))
			{
				stringBuilder.Append(url.Contains("?") ? ampStr : "?");
			}
			stringBuilder.Append(key);
			stringBuilder.Append("=");
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003410 File Offset: 0x00001610
		private static string RemoveParameterFromUrlForEcp(string url, string name)
		{
			bool flag = false;
			int num = url.IndexOf('?');
			if (num > 0)
			{
				while (!flag && num < url.Length)
				{
					int num2 = url.IndexOf(name, num, StringComparison.OrdinalIgnoreCase);
					if (num2 <= 0)
					{
						break;
					}
					char c = url[num2 - 1];
					char c2 = (num2 + name.Length < url.Length) ? url[num2 + name.Length] : ' ';
					if ((c == '?' || c == '&') && c2 == '=')
					{
						flag = true;
						int num3 = url.IndexOf('&', num2 + name.Length + 1);
						if (num3 < 0)
						{
							num3 = url.IndexOf('#', num2 + name.Length + 1);
							if (num3 < 0)
							{
								num3 = url.Length;
							}
							num2--;
							num3--;
						}
						url = url.Remove(num2, num3 - num2 + 1);
					}
					else
					{
						num = num2 + name.Length;
					}
				}
			}
			return url;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000034F4 File Offset: 0x000016F4
		private static bool IsCurrentStackECP()
		{
			if (DelegatedAuthenticationModule.protocol == DelegatedAuthenticationModule.Protocol.RWS || DelegatedAuthenticationModule.protocol == DelegatedAuthenticationModule.Protocol.Psws)
			{
				return false;
			}
			if (DelegatedAuthenticationModule.isEcpStack == null)
			{
				Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.Path);
				string rawXml = configuration.GetSection("appSettings").SectionInformation.GetRawXml();
				string rawXml2 = configuration.GetSection("system.webServer").SectionInformation.GetRawXml();
				if (rawXml != null && rawXml.IndexOf("ProtocolType", StringComparison.OrdinalIgnoreCase) >= 0 && rawXml.IndexOf("RemotePS", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					DelegatedAuthenticationModule.isEcpStack = new bool?(false);
				}
				else if (rawXml2 != null && rawXml2.IndexOf("system.management.wsmanagement.config", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					DelegatedAuthenticationModule.isEcpStack = new bool?(false);
				}
				else
				{
					DelegatedAuthenticationModule.isEcpStack = new bool?(true);
				}
			}
			return DelegatedAuthenticationModule.isEcpStack.Value;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000035C8 File Offset: 0x000017C8
		private static string[] ExtractDirectoryObjectId(ADGroup[] groups)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < groups.Length; i++)
			{
				if (!string.IsNullOrEmpty(groups[i].ExternalDirectoryObjectId) && groups[i].RawCapabilities != null && groups[i].RawCapabilities.Contains(Capability.Partner_Managed))
				{
					list.Add(groups[i].ExternalDirectoryObjectId);
				}
				else
				{
					DelegatedAuthenticationModule.LogDebug("The following group: {0} is excluded from delegated partner permissions.", new object[]
					{
						groups[i].Id
					});
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003648 File Offset: 0x00001848
		private static ADRawEntry ResolveADUserFromPrincipal(IPrincipal user)
		{
			WindowsIdentity windowsIdentity = user.Identity as WindowsIdentity;
			SecurityIdentifier securityIdentifier = null;
			PartitionId partitionId = null;
			OrganizationId organizationId = null;
			if (windowsIdentity != null)
			{
				securityIdentifier = windowsIdentity.User;
			}
			else
			{
				LiveIDIdentity liveIDIdentity = user.Identity as LiveIDIdentity;
				if (liveIDIdentity != null)
				{
					securityIdentifier = liveIDIdentity.Sid;
					organizationId = liveIDIdentity.UserOrganizationId;
				}
				else
				{
					SidOAuthIdentity sidOAuthIdentity = user.Identity as SidOAuthIdentity;
					if (sidOAuthIdentity != null)
					{
						securityIdentifier = sidOAuthIdentity.OAuthIdentity.ActAsUser.Sid;
						organizationId = sidOAuthIdentity.OAuthIdentity.OrganizationId;
					}
					else
					{
						GenericSidIdentity genericSidIdentity = user.Identity as GenericSidIdentity;
						if (genericSidIdentity != null)
						{
							securityIdentifier = genericSidIdentity.Sid;
							if (!string.IsNullOrEmpty(genericSidIdentity.PartitionId))
							{
								PartitionId.TryParse(genericSidIdentity.PartitionId, out partitionId);
							}
						}
					}
				}
			}
			if (securityIdentifier == null)
			{
				throw new CannotResolveWindowsIdentityException();
			}
			ADRawEntry adrawEntry = UserTokenStaticHelper.GetADRawEntry(partitionId, organizationId, securityIdentifier);
			if (adrawEntry == null)
			{
				throw new CannotResolveSidToADAccountException(securityIdentifier.ToString());
			}
			return adrawEntry;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000372C File Offset: 0x0000192C
		private static ADGroup[] ResolveDelegatedGroupForUser(ADRawEntry userEntry)
		{
			if (userEntry == null)
			{
				throw new ArgumentNullException("userEntry");
			}
			OrganizationId organizationId = (OrganizationId)userEntry[ADObjectSchema.OrganizationId];
			ADSessionSettings sessionSettings = (organizationId == null) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(organizationId);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 1576, "ResolveDelegatedGroupForUser", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\DelegatedAuthentication\\DelegatedAuthenticationModule.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			List<string> tokenSids = tenantOrRootOrgRecipientSession.GetTokenSids(userEntry, AssignmentMethod.SecurityGroup);
			ADObjectId[] array = tenantOrRootOrgRecipientSession.ResolveSidsToADObjectIds(tokenSids.ToArray());
			Result<ADGroup>[] array2 = tenantOrRootOrgRecipientSession.ReadMultipleADGroups(array);
			StringBuilder stringBuilder = null;
			List<ADGroup> list = new List<ADGroup>(array2.Length);
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].Error != null)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(array[i]);
				}
				else
				{
					list.Add(array2[i].Data);
				}
			}
			if (stringBuilder != null)
			{
				DelegatedAuthenticationModule.LogEvent(TaskEventLogConstants.Tuple_DelegatedAuth_FailedToReadMultiple, (userEntry[IADSecurityPrincipalSchema.Sid] != null) ? userEntry[IADSecurityPrincipalSchema.Sid].ToString() : string.Empty, new object[]
				{
					stringBuilder.ToString(),
					(userEntry[ADObjectSchema.Id] != null) ? userEntry[ADObjectSchema.Id].ToString() : string.Empty
				});
			}
			return list.ToArray();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003898 File Offset: 0x00001A98
		private static ExchangeConfigurationUnit GetADOrganizationByName(string orgName)
		{
			ExchangeConfigurationUnit result = null;
			try
			{
				PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(orgName);
				ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionIdByAcceptedDomainName);
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 1637, "GetADOrganizationByName", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\DelegatedAuthentication\\DelegatedAuthenticationModule.cs");
				result = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(orgName);
			}
			catch (CannotResolveTenantNameException)
			{
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000038F0 File Offset: 0x00001AF0
		private static ExchangeConfigurationUnit GetADOrganizationByExternalDirectoryId(Guid externalDirectoryId)
		{
			try
			{
				PartitionId partitionIdByExternalDirectoryOrganizationId = ADAccountPartitionLocator.GetPartitionIdByExternalDirectoryOrganizationId(externalDirectoryId);
				ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionIdByExternalDirectoryOrganizationId);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 1664, "GetADOrganizationByExternalDirectoryId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\DelegatedAuthentication\\DelegatedAuthenticationModule.cs");
				ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId, externalDirectoryId.ToString());
				ExchangeConfigurationUnit[] array = tenantOrTopologyConfigurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 1);
				if (array.Length > 0)
				{
					return array[0];
				}
			}
			catch (CannotResolveExternalDirectoryOrganizationIdException)
			{
			}
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003978 File Offset: 0x00001B78
		private static ExchangeConfigurationUnit GetADOrganizationById(OrganizationId orgId)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(orgId), 1693, "GetADOrganizationById", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\DelegatedAuthentication\\DelegatedAuthenticationModule.cs");
			return tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(orgId.ConfigurationUnit);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000039B4 File Offset: 0x00001BB4
		private static string GetSecurityTokenProperty(HttpRequest request)
		{
			string text;
			if (DelegatedAuthenticationModule.IsCurrentStackECP())
			{
				text = request.QueryString[DelegatedAuthenticationModule.securityTokenUriPropertyName];
			}
			else
			{
				UriBuilder uriBuilder = new UriBuilder(request.Url);
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query.Replace(';', '&'));
				text = nameValueCollection[DelegatedAuthenticationModule.securityTokenUriPropertyName];
			}
			if (text != null)
			{
				text = Uri.UnescapeDataString(text);
			}
			else
			{
				HttpCookie httpCookie = request.Cookies[DelegatedAuthenticationModule.securityTokenUriPropertyName];
				if (httpCookie != null)
				{
					text = httpCookie.Value;
				}
			}
			return text;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003A32 File Offset: 0x00001C32
		private static void LogDebug(string message, params object[] args)
		{
			if (ExTraceGlobals.DelegatedAuthTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DelegatedAuthTracer.TraceDebug(0L, message, args);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003A4F File Offset: 0x00001C4F
		private static void LogDebug(string message)
		{
			if (ExTraceGlobals.DelegatedAuthTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				DelegatedAuthenticationModule.LogDebug(message, new object[0]);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003A6A File Offset: 0x00001C6A
		private static void LogError(string message, Exception exception)
		{
			if (ExTraceGlobals.DelegatedAuthTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.DelegatedAuthTracer.TraceError<string, Exception>(0L, "{0} - {1}", message, exception);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003A8C File Offset: 0x00001C8C
		private static void LogEvent(ExEventLog.EventTuple eventInfo, string periodicKey, params object[] messageArguments)
		{
			if (messageArguments == null)
			{
				throw new ArgumentNullException("messageArguments");
			}
			object[] array = new object[messageArguments.Length + 2];
			array[0] = DelegatedAuthenticationModule.processName;
			array[1] = DelegatedAuthenticationModule.processId;
			messageArguments.CopyTo(array, 2);
			DelegatedAuthenticationModule.eventLogger.LogEvent(eventInfo, periodicKey, array);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003AE0 File Offset: 0x00001CE0
		private static string GetTargetTenant(HttpRequest request)
		{
			string result;
			if (DelegatedAuthenticationModule.protocol == DelegatedAuthenticationModule.Protocol.PowerShell)
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(request.Url.Query.Replace(';', '&'));
				result = nameValueCollection["DelegatedOrg"];
			}
			else if (DelegatedAuthenticationModule.protocol == DelegatedAuthenticationModule.Protocol.RWS)
			{
				result = request.QueryString["DelegatedOrg"];
			}
			else if (DelegatedAuthenticationModule.protocol == DelegatedAuthenticationModule.Protocol.Psws)
			{
				result = request.Headers["msExchTargetTenant"];
			}
			else
			{
				result = request.Headers["msExchTargetTenant"];
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private const string CookiePathFormat = "{0}/@{1}/";

		// Token: 0x04000002 RID: 2
		private const string RedirToLocalKey = "redirtolocal";

		// Token: 0x04000003 RID: 3
		private const string RedirToLocalKeyWithTrailingEqual = "redirtolocal=";

		// Token: 0x04000004 RID: 4
		private const string CafeForceRouteToLogonAccountKey = "msExchCafeForceRouteToLogonAccount";

		// Token: 0x04000005 RID: 5
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.DelegatedAuthTracer.Category, "MSExchange Delegated Authentication Module");

		// Token: 0x04000006 RID: 6
		private static readonly EventHandler OnAuthenticateRequestHandler = new EventHandler(DelegatedAuthenticationModule.OnAuthenticateRequest);

		// Token: 0x04000007 RID: 7
		private static readonly string appDomainAppVirtualPath;

		// Token: 0x04000008 RID: 8
		private static readonly bool isRedirectToLocalServerEnabled;

		// Token: 0x04000009 RID: 9
		private static string securityTokenUriPropertyName = "SecurityToken";

		// Token: 0x0400000A RID: 10
		private static int podSiteStartRange = 0;

		// Token: 0x0400000B RID: 11
		private static int podSiteEndRange = 0;

		// Token: 0x0400000C RID: 12
		private static string podRedirectTemplate = null;

		// Token: 0x0400000D RID: 13
		private static string siteRedirectTemplate = null;

		// Token: 0x0400000E RID: 14
		private static string processName;

		// Token: 0x0400000F RID: 15
		private static int processId;

		// Token: 0x04000010 RID: 16
		private static bool? isEcpStack = null;

		// Token: 0x04000011 RID: 17
		private static DelegatedAuthenticationModule.Protocol protocol;

		// Token: 0x02000003 RID: 3
		private enum Protocol
		{
			// Token: 0x04000013 RID: 19
			Unknown,
			// Token: 0x04000014 RID: 20
			RWS,
			// Token: 0x04000015 RID: 21
			ECP,
			// Token: 0x04000016 RID: 22
			PowerShell,
			// Token: 0x04000017 RID: 23
			Psws
		}
	}
}
