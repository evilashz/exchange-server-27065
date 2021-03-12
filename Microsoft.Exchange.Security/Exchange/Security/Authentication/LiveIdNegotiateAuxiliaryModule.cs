﻿using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000076 RID: 118
	public class LiveIdNegotiateAuxiliaryModule : IHttpModule
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00020390 File Offset: 0x0001E590
		void IHttpModule.Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
			application.AuthenticateRequest += this.OnAuthenticate;
			application.AuthorizeRequest += this.OnAuthorize;
			bool flag;
			if (bool.TryParse(WebConfigurationManager.AppSettings["LiveIdNegotiateAuxiliaryModule.AllowPowerShellLiveIdCookie"], out flag) && flag)
			{
				application.PreSendRequestContent += this.OnSendRequestContent;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00020400 File Offset: 0x0001E600
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00020404 File Offset: 0x0001E604
		internal void OnBeginRequest(object sender, EventArgs args)
		{
			this.TraceEnterFunction("OnBeginRequest");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			string authType = AuthServiceHelper.GetAuthType(context.Request.Headers["Authorization"]);
			context.Items["AuthType"] = authType;
			context.Response.AppendToLog("&AuthType=" + authType);
			string text = context.Request.Headers["X-Nego-Capability"];
			if (!string.IsNullOrEmpty(text))
			{
				context.Items["NegoCap"] = text;
				context.Response.AppendToLog("&NegoCap=" + text);
			}
			string text2 = context.Request.Headers["X-User-Identity"];
			if (!string.IsNullOrEmpty(text2))
			{
				context.Response.AppendToLog("&XUserId=" + text2);
			}
			this.TraceExitFunction("OnBeginRequest");
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000204F4 File Offset: 0x0001E6F4
		internal void OnAuthenticate(object sender, EventArgs args)
		{
			this.TraceEnterFunction("OnAuthenticate");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			this.InternalOnAuthenticate(context);
			this.TraceExitFunction("OnAuthenticate");
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0002052C File Offset: 0x0001E72C
		private void InternalOnAuthenticate(HttpContext context)
		{
			HttpApplication applicationInstance = context.ApplicationInstance;
			bool flag = false;
			bool flag2 = false;
			string text = context.Request.Headers["X-User-Identity"];
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			SmtpAddress smtpAddress = new SmtpAddress(text);
			if (smtpAddress.IsValidAddress)
			{
				AcceptedDomain acceptedDomain = null;
				try
				{
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(smtpAddress.Domain), 204, "InternalOnAuthenticate", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\FederatedAuthService\\LiveIdNegotiateAuxiliaryModule.cs");
					acceptedDomain = tenantConfigurationSession.GetAcceptedDomainByDomainName(smtpAddress.Domain);
				}
				catch (CannotResolveTenantNameException)
				{
					ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Could not resolve recipient session for domain '{0}'", smtpAddress.Domain);
				}
				catch (CannotResolvePartitionException)
				{
					ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Could not resolve recipient session for domain '{0}'", smtpAddress.Domain);
				}
				if (acceptedDomain != null)
				{
					ExTraceGlobals.AuthenticationTracer.Information<string, string, bool>(0L, "Accepted domain found: {0} for user {1} Nego2Enabled: {2}", acceptedDomain.Name, text, acceptedDomain.EnableNego2Authentication);
					flag = acceptedDomain.EnableNego2Authentication;
					flag2 = true;
				}
				else
				{
					ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "Accepted domain not found - querying MSERV for user {0}", text);
					LiveIdBasicAuthentication liveIdBasicAuthentication = new LiveIdBasicAuthentication();
					if (liveIdBasicAuthentication.IsNego2AuthEnabledForDomain(smtpAddress.Domain, out flag))
					{
						flag2 = true;
						ExTraceGlobals.AuthenticationTracer.Information<string, string, bool>(0L, "MSERV service found: {0} for user {1} Nego2Enabled: {2}", smtpAddress.Domain, text, flag);
					}
					else
					{
						ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "MSERV service returned error for user {0}", text);
					}
				}
			}
			context.Items["WLID-TenantNegoEnabled"] = flag.ToString();
			if (!context.Request.IsAuthenticated)
			{
				if (flag2)
				{
					context.Response.AddHeader("X-EnableNego2Challenge", flag.ToString());
				}
				return;
			}
			string text2 = context.User.Identity.GetSafeName(true);
			if (!LiveIdNegotiateAuxiliaryModule.IsNegotiatedIdentity(context.User.Identity))
			{
				ExTraceGlobals.AuthenticationTracer.Information<string, string>(0L, "Authentication type not negotiated ({0}), stopped processing user {1}", context.User.Identity.AuthenticationType, text2);
				return;
			}
			WindowsIdentity windowsIdentity = context.User.Identity as WindowsIdentity;
			if (windowsIdentity == null)
			{
				ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "Not a valid WindowsIdentity ({0}), stopped processing", (context.User.Identity == null) ? "null" : context.User.Identity.GetType().ToString());
				return;
			}
			if (!LiveIdNegotiateAuxiliaryModule.ShouldProcessTicket())
			{
				ExTraceGlobals.AuthenticationTracer.Information(0L, "Live ID ticket processing is not enabled in web.config");
				return;
			}
			string text3;
			if (!LiveIdNegotiateAuxiliaryModule.TryGetLiveIdTicket(windowsIdentity, applicationInstance.Request.ServerVariables, out text3) || text3 == null)
			{
				if (windowsIdentity.IsSystem && !string.IsNullOrEmpty((string)context.Items["WLID-MemberName"]))
				{
					ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "context is LocalSystem and WLID-Membername is already set for user {0}, stopped processing", text2);
					return;
				}
				ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "Unable to retrieve LiveID ticket for user {0}", text2);
				LiveIdNegotiateAuxiliaryModule.CompleteWithAccessDenied(applicationInstance, "Unable to retrieve LiveID Ticket");
				return;
			}
			else
			{
				string text4;
				string puid;
				long num;
				if (!LiveIdNegotiateAuxiliaryModule.TryParseTicketInformation(windowsIdentity.GetSafeName(true), text3, out text4, out puid, out num))
				{
					ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "Unable to parse LiveID ticket for user {0}", text2);
					LiveIdNegotiateAuxiliaryModule.CompleteWithAccessDenied(applicationInstance, "Unable to parse LiveID ticket");
					return;
				}
				if (!smtpAddress.IsValidAddress)
				{
					smtpAddress = new SmtpAddress(text4);
					if (smtpAddress.IsValidAddress)
					{
						AcceptedDomain acceptedDomain2 = null;
						try
						{
							ITenantConfigurationSession tenantConfigurationSession2 = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(smtpAddress.Domain), 345, "InternalOnAuthenticate", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\FederatedAuthService\\LiveIdNegotiateAuxiliaryModule.cs");
							acceptedDomain2 = tenantConfigurationSession2.GetAcceptedDomainByDomainName(smtpAddress.Domain);
						}
						catch (CannotResolveTenantNameException)
						{
							ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Could not resolve recipient session for domain '{0}'", smtpAddress.Domain);
						}
						catch (CannotResolvePartitionException)
						{
							ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.GetHashCode(), "Could not resolve recipient session for domain '{0}'", smtpAddress.Domain);
						}
						if (acceptedDomain2 != null)
						{
							ExTraceGlobals.AuthenticationTracer.Information<string, string, bool>(0L, "Authenticated user - accepted domain found: {0} for user {1} Nego2Enabled: {2}", acceptedDomain2.Name, text4, acceptedDomain2.EnableNego2Authentication);
							flag = acceptedDomain2.EnableNego2Authentication;
						}
						else
						{
							ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "Authenticated users - accepted domain not found - querying MSERV for user {0}", text4);
							LiveIdBasicAuthentication liveIdBasicAuthentication2 = new LiveIdBasicAuthentication();
							if (liveIdBasicAuthentication2.IsNego2AuthEnabledForDomain(smtpAddress.Domain, out flag))
							{
								ExTraceGlobals.AuthenticationTracer.Information<string, string, bool>(0L, "MSERV service found: {0} for user {1} Nego2Enabled: {2}", smtpAddress.Domain, text4, flag);
							}
							else
							{
								ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "MSERV service error could not determine nego2 status for user {0}", text4);
							}
						}
						context.Items["WLID-TenantNegoEnabled"] = flag.ToString();
						if (!flag)
						{
							context.Response.AddHeader("X-EnableNego2Challenge", flag.ToString());
							LiveIdNegotiateAuxiliaryModule.CompleteWithAccessDenied(applicationInstance, "Suppress Nego2 for authenticated client");
							return;
						}
					}
				}
				context.Items["WLID-MemberName"] = text4;
				context.Items["WLIDAux-ProfileFlags"] = num;
				context.Items["LiveIdNegotiateMemberName"] = text4;
				context.Response.AppendToLog("&LiveIdNegotiateMemberName=" + text4);
				text2 = text4;
				string text5 = string.Empty;
				if (!LiveIdBasicAuthModule.TryParseOrganizationContext(context.Request.Url, out text5))
				{
					ExTraceGlobals.AuthenticationTracer.TraceWarning<string>(0L, "Cannot parse organization context from Url: {0}", context.Request.Url.ToString());
				}
				if (string.IsNullOrEmpty(text5))
				{
					text5 = smtpAddress.Domain;
				}
				string sUserPrincipalName;
				bool flag3;
				bool flag4;
				if (!LiveIdNegotiateAuxiliaryModule.TryLookupUser(text2, puid, text5, out sUserPrincipalName, out flag3) && (!bool.TryParse(WebConfigurationManager.AppSettings["LiveIdNegotiateAuxiliaryModule.AllowLiveIDOnlyAuth"], out flag4) || !flag4))
				{
					ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "LiveId Only Auth not allowed for user {0}", text2);
					LiveIdNegotiateAuxiliaryModule.CompleteWithAccessDenied(applicationInstance, "LiveID Only Auth not allowed");
					return;
				}
				try
				{
					using (WindowsIdentity windowsIdentity2 = new WindowsIdentity(sUserPrincipalName))
					{
						WindowsIdentity identity = new WindowsIdentity(windowsIdentity2.Token, "RPS", WindowsAccountType.Normal, true);
						context.User = new GenericPrincipal(identity, null);
					}
				}
				catch (SystemException arg)
				{
					ExTraceGlobals.AuthenticationTracer.TraceError<string, SystemException>(0L, "Remapping failed for user {0}, exception {1}", text2, arg);
					LiveIdNegotiateAuxiliaryModule.CompleteWithAccessDenied(applicationInstance, "Failed during user remapping");
					return;
				}
				if (LiveIdNegotiateAuxiliaryModule.ShouldCheckTermsOfUse())
				{
					context.Items["WLIDAux-RequireTermsCheck"] = flag3;
				}
				return;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00020B48 File Offset: 0x0001ED48
		internal void OnAuthorize(object sender, EventArgs args)
		{
			this.TraceEnterFunction("OnAuthorize");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			if (!context.Request.IsAuthenticated)
			{
				ExTraceGlobals.AuthenticationTracer.Information(0L, "Not authenticated, no processing necessary");
				this.TraceExitFunction("OnAuthorize");
				return;
			}
			string text = context.User.Identity.GetSafeName(true);
			if (context.Items.Contains("WLID-MemberName"))
			{
				text = (string)context.Items["WLID-MemberName"];
			}
			if (context.Items.Contains("WLIDAux-RequireTermsCheck"))
			{
				bool flag = (bool)context.Items["WLIDAux-RequireTermsCheck"];
				if (flag)
				{
					if (!context.Items.Contains("WLIDAux-ProfileFlags"))
					{
						ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "Unable to find profile flags for user {0}. Failing Authorization.", text);
						LiveIdNegotiateAuxiliaryModule.CompleteWithAccessDenied(httpApplication, "Missing profile info");
						this.TraceExitFunction("OnAuthorize");
						return;
					}
					int flags = (int)((long)context.Items["WLIDAux-ProfileFlags"]);
					if (!RPSCommon.HasUserSignedTOU(flags, text))
					{
						ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "User {0} has not accepted terms of use. Failing Authorization.", text);
						string text2 = context.Request.Url.GetLeftPart(UriPartial.Authority) + "/owa";
						if (SmtpAddress.IsValidSmtpAddress(text))
						{
							text2 = text2 + "/" + new SmtpAddress(text).Domain;
						}
						LiveIdNegotiateAuxiliaryModule.CompleteWithTermsNotAccepted(httpApplication, text2);
						this.TraceExitFunction("OnAuthorize");
						return;
					}
				}
			}
			this.TraceExitFunction("OnAuthorize");
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00020CDC File Offset: 0x0001EEDC
		internal void OnSendRequestContent(object source, EventArgs args)
		{
			this.TraceEnterFunction("OnSendRequestContent");
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (LiveIdNegotiateAuxiliaryModule.ShouldAddCookie(context))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "Stamping cookie {0} in HTTPContext.Response for Nego2 Authentication for user {1}", LiveIdNegotiateAuxiliaryModule.wsManCookie.ToString(), context.User.Identity.GetSafeName(true));
				context.Response.Cookies.Add(LiveIdNegotiateAuxiliaryModule.wsManCookie);
			}
			this.TraceExitFunction("OnSendRequestContent");
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00020D56 File Offset: 0x0001EF56
		private void TraceEnterFunction(string functionName)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction<string>((long)this.GetHashCode(), "Enter Function: LiveIdNegotiateAuxiliaryModule.{0}.", functionName);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00020D6F File Offset: 0x0001EF6F
		private void TraceExitFunction(string functionName)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction<string>((long)this.GetHashCode(), "Exit Function: LiveIdNegotiateAuxiliaryModule.{0}.", functionName);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00020D88 File Offset: 0x0001EF88
		private static bool ShouldProcessTicket()
		{
			string strA = WebConfigurationManager.AppSettings["LiveIdNegotiateAuxiliaryModule.LiveIdProcessing"];
			return string.Compare(strA, "all", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(strA, "ticket", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00020DC4 File Offset: 0x0001EFC4
		private static bool ShouldCheckTermsOfUse()
		{
			string strA = WebConfigurationManager.AppSettings["LiveIdNegotiateAuxiliaryModule.LiveIdProcessing"];
			return string.Compare(strA, "all", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		private static bool TryGetLiveIdTicket(WindowsIdentity windowsIdentity, NameValueCollection serverVariables, out string ticket)
		{
			string text;
			if (string.Compare(windowsIdentity.AuthenticationType, "RPS", StringComparison.OrdinalIgnoreCase) == 0)
			{
				ticket = serverVariables["LIVEID_TICKET_INFO"];
				if (ticket == null)
				{
					ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "Unable to find serialized live identity, stopped processing user {0}", windowsIdentity.GetSafeName(true));
					return false;
				}
			}
			else if (!LiveIdNegotiateAuxiliaryModule.TryQueryLiveUserProperties(windowsIdentity, out text, out ticket))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "Unable to query Live user properties from LiveAP for user {0}", windowsIdentity.GetSafeName(true));
				return false;
			}
			return true;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00020E64 File Offset: 0x0001F064
		private static bool TryParseTicketInformation(string userName, string ticket, out string memberName, out string puid, out long profileFlags)
		{
			puid = string.Empty;
			profileFlags = 0L;
			LiveIdTicketDictionary liveIdTicketDictionary = new LiveIdTicketDictionary(ticket);
			if (!liveIdTicketDictionary.TryGetValue<string>("membername", out memberName))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "Did not find a membername field for user {0}, ticket: {1}", userName, ticket);
				return false;
			}
			if (!liveIdTicketDictionary.TryGetValue<string>("hexpuid", out puid))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "Did not find a hexpuid field for user {0}, ticket: {1}", userName, ticket);
				return false;
			}
			string text;
			if (!liveIdTicketDictionary.TryGetValue<string>("profilebag", out text))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "Did not find a profilebag field for user {0}, ticket: {1}", userName, ticket);
				return false;
			}
			LiveIdTicketDictionary liveIdTicketDictionary2 = new LiveIdTicketDictionary(text);
			if (!liveIdTicketDictionary2.TryGetValue<long>("flags", out profileFlags))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "Did not find a flags field for user {0}, profilebag: {1}", userName, text);
				return false;
			}
			return true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00021008 File Offset: 0x0001F208
		private static bool TryLookupUser(string userName, string puid, string organizationContext, out string userPrincipalName, out bool requireTermsOfUseCheck)
		{
			userPrincipalName = string.Empty;
			requireTermsOfUseCheck = false;
			string domainFQDN = null;
			ADRawEntry adRawEntry = null;
			ITenantRecipientSession recipientSession = null;
			try
			{
				recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromTenantAcceptedDomain(organizationContext), 745, "TryLookupUser", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\FederatedAuthService\\LiveIdNegotiateAuxiliaryModule.cs");
			}
			catch (CannotResolveTenantNameException)
			{
				ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "Could not resolve recipient session for domain '{0}'", organizationContext);
				return false;
			}
			catch (CannotResolvePartitionException)
			{
				ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "Could not resolve recipient session for domain '{0}'", organizationContext);
			}
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADRawEntry[] array = recipientSession.FindByNetID(puid, organizationContext ?? string.Empty, LiveIdNegotiateAuxiliaryModule.UserLookupProperties);
				adRawEntry = recipientSession.ChooseBetweenAmbiguousUsers(array);
				if (adRawEntry != null && array.Length > 1)
				{
					ITenantRecipientSession ads = DirectorySessionFactory.Default.CreateTenantRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, false, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromTenantAcceptedDomain(organizationContext), 784, "TryLookupUser", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\FederatedAuthService\\LiveIdNegotiateAuxiliaryModule.cs");
					AuthServiceHelper.InvalidateDuplicateUPNs(ads, adRawEntry, array, delegate(string format, object[] args)
					{
						ExTraceGlobals.AuthenticationTracer.TraceWarning(0L, format, args);
					});
				}
				if (adRawEntry != null)
				{
					domainFQDN = recipientSession.SessionSettings.PartitionId.ForestFQDN;
				}
			});
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string, Exception>(0L, "Failed to lookup user {0} ({1}) in the AD, organization id: {2}", userName, puid, adoperationResult.Exception);
				return false;
			}
			if (adRawEntry == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string>(0L, "Unable to find AD object for user {0} ({1})", userName, puid);
				return false;
			}
			userPrincipalName = string.Format("{0}@{1}", (string)adRawEntry[ADMailboxRecipientSchema.SamAccountName], domainFQDN);
			OrganizationId organizationId = (OrganizationId)adRawEntry[ADObjectSchema.OrganizationId];
			OrganizationProperties organizationProperties;
			if (!OrganizationPropertyCache.TryGetOrganizationProperties(organizationId, out organizationProperties))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string, OrganizationId>(0L, "Unable to determine organization settings for user {0} ({1}), organization id: {2}", userName, puid, organizationId);
				return false;
			}
			requireTermsOfUseCheck = !organizationProperties.SkipToUAndParentalControlCheck;
			return true;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000211A8 File Offset: 0x0001F3A8
		private static void CompleteWithTermsNotAccepted(HttpApplication application, string url)
		{
			application.Response.StatusCode = 456;
			application.Response.StatusDescription = url;
			application.CompleteRequest();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000211CC File Offset: 0x0001F3CC
		private static void CompleteWithAccessDenied(HttpApplication application, string reason)
		{
			application.Response.StatusCode = 401;
			application.CompleteRequest();
			application.Context.Response.AppendToLog("&LiveIdNegotiateError=" + reason);
			application.Context.Items["LiveIdNegotiateError"] = reason;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00021220 File Offset: 0x0001F420
		private static bool IsNegotiatedIdentity(IIdentity identity)
		{
			return string.Compare(identity.AuthenticationType, "Negotiate", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(identity.AuthenticationType, "RPS", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0002124C File Offset: 0x0001F44C
		private static bool TryQueryLiveUserProperties(WindowsIdentity windowsIdentity, out string userName, out string ticket)
		{
			userName = string.Empty;
			ticket = string.Empty;
			WindowsImpersonationContext windowsImpersonationContext = null;
			try
			{
				try
				{
					windowsImpersonationContext = windowsIdentity.Impersonate();
					using (SafeLsaUntrustedHandle safeLsaUntrustedHandle = SafeLsaUntrustedHandle.Create())
					{
						int packageId;
						try
						{
							packageId = safeLsaUntrustedHandle.LookupPackage("liveap");
						}
						catch (Win32Exception ex)
						{
							LiveIdNegotiateAuxiliaryModule.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_NativeApiFailed, "NativeApiFailedliveap", new object[]
							{
								"LsaLookupAuthenticationPackage",
								"liveap",
								ex.ErrorCode
							});
							ExTraceGlobals.AuthenticationTracer.Information(0L, "Unable to lookup LiveAP package");
							return false;
						}
						try
						{
							ulong num;
							string text;
							safeLsaUntrustedHandle.LiveQueryUserInfo(packageId, out num, out userName, out ticket, out text);
						}
						catch (Win32Exception ex2)
						{
							ExTraceGlobals.AuthenticationTracer.Information<string, int>(0L, "Unable to query live information for user {0}, error {1}", windowsIdentity.GetSafeName(true), ex2.ErrorCode);
							return false;
						}
					}
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
						windowsImpersonationContext.Dispose();
					}
				}
			}
			catch
			{
				throw;
			}
			return true;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00021378 File Offset: 0x0001F578
		private static bool ShouldAddCookie(HttpContext context)
		{
			return context.Request.ApplicationPath.Contains("/PowerShell-LiveID") && LiveIdNegotiateAuxiliaryModule.wsManCookie != null && context.Request.IsAuthenticated && context.User != null && LiveIdNegotiateAuxiliaryModule.IsNegotiatedIdentity(context.User.Identity);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000213CC File Offset: 0x0001F5CC
		private static HttpCookie CreateCookieEntry()
		{
			HttpCookie result;
			try
			{
				string hostName = Dns.GetHostName();
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				string text = string.Empty;
				foreach (IPAddress ipaddress in hostEntry.AddressList)
				{
					if (ipaddress.AddressFamily.ToString() == AddressFamily.InterNetwork.ToString())
					{
						text = ipaddress.ToString();
						break;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					string[] array = text.Split(new char[]
					{
						'.'
					});
					Array.Reverse(array);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string value in array)
					{
						long num = Convert.ToInt64(value);
						stringBuilder.Append(string.Format("{0:x2}", num));
					}
					HttpCookie httpCookie = new HttpCookie("MS-WSMAN", Convert.ToInt64(stringBuilder.ToString(), 16).ToString() + ".47873.0000");
					result = httpCookie;
				}
				else
				{
					ExTraceGlobals.AuthenticationTracer.TraceWarning(0L, "Failed to create WSMan cookie entry, cannot find IPV4 address from host name");
					result = null;
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string>(0L, "Failed to create WSMan cookie entry for Nego2 Authentication in the current server. Exception: {0}", ex.Message);
				LiveIdNegotiateAuxiliaryModule.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_WSManCookieCreationException, ex.ToString(), new object[]
				{
					ex.ToString()
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0400044E RID: 1102
		public const string DelegatedTargetTenantKey = "msExchTargetTenant";

		// Token: 0x0400044F RID: 1103
		public const string OriginalUrlKey = "msExchOriginalUrl";

		// Token: 0x04000450 RID: 1104
		public const string TargetOrganizationPropertyName = "DelegatedOrg";

		// Token: 0x04000451 RID: 1105
		public const string AuthorizationHeaderKey = "Authorization";

		// Token: 0x04000452 RID: 1106
		private const string LiveIdComponent = "MSExchange LiveIdNegotiateAuxiliaryModule";

		// Token: 0x04000453 RID: 1107
		private const string LiveApPackageName = "liveap";

		// Token: 0x04000454 RID: 1108
		private const string EnableNego2ChallengeKey = "X-EnableNego2Challenge";

		// Token: 0x04000455 RID: 1109
		private const string XUserIdentity = "X-User-Identity";

		// Token: 0x04000456 RID: 1110
		private const string TenantNego2Key = "WLID-TenantNegoEnabled";

		// Token: 0x04000457 RID: 1111
		private const string MemberNameContextKey = "WLID-MemberName";

		// Token: 0x04000458 RID: 1112
		private const string ProfileFlagsContextKey = "WLIDAux-ProfileFlags";

		// Token: 0x04000459 RID: 1113
		private const string LiveIdOnlyUserContextKey = "WLIDAux-LiveIdOnlyUser";

		// Token: 0x0400045A RID: 1114
		private const string RequireTermsCheckContextKey = "WLIDAux-RequireTermsCheck";

		// Token: 0x0400045B RID: 1115
		private const string LiveIdProcessingConfigKey = "LiveIdNegotiateAuxiliaryModule.LiveIdProcessing";

		// Token: 0x0400045C RID: 1116
		private const string AllowLiveIdOnlyAuthConfigKey = "LiveIdNegotiateAuxiliaryModule.AllowLiveIDOnlyAuth";

		// Token: 0x0400045D RID: 1117
		private const string AllowPowerShellLiveIdCookieConfigKey = "LiveIdNegotiateAuxiliaryModule.AllowPowerShellLiveIdCookie";

		// Token: 0x0400045E RID: 1118
		private static readonly PropertyDefinition[] UserLookupProperties = new PropertyDefinition[]
		{
			ADUserSchema.UserPrincipalName,
			ADMailboxRecipientSchema.SamAccountName,
			ADObjectSchema.OrganizationId
		};

		// Token: 0x0400045F RID: 1119
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.AuthenticationTracer.Category, "LiveIdNegotiateAuxiliaryModule");

		// Token: 0x04000460 RID: 1120
		private static HttpCookie wsManCookie = LiveIdNegotiateAuxiliaryModule.CreateCookieEntry();
	}
}
