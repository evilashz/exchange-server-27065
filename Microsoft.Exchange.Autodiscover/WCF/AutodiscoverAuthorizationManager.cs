using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Net.Claim;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.PartnerToken;
using Microsoft.Exchange.Security.X509CertAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000055 RID: 85
	public class AutodiscoverAuthorizationManager : ServiceAuthorizationManager
	{
		// Token: 0x06000273 RID: 627 RVA: 0x000108A0 File Offset: 0x0000EAA0
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			RequestDetailsLogger logger = RequestDetailsLoggerBase<RequestDetailsLogger>.Current;
			bool requestAllowed = false;
			logger.UpdateLatency(ServiceLatencyMetadata.HttpPipelineLatency, RequestDetailsLoggerBase<RequestDetailsLogger>.Current.ActivityScope.TotalMilliseconds);
			Common.SendWatsonReportOnUnhandledException(delegate
			{
				requestAllowed = logger.TrackLatency<bool>(ServiceLatencyMetadata.CheckAccessCoreLatency, () => this.InternalCheckAccessCore(operationContext));
			});
			return requestAllowed;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001090A File Offset: 0x0000EB0A
		private static bool IsDelegationToken(ReadOnlyCollection<ClaimSet> claimSets)
		{
			return claimSets.PossessClaimType("http://schemas.microsoft.com/ws/2006/04/identity/claims/ThirdPartyRequested");
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010918 File Offset: 0x0000EB18
		private static bool Return401UnauthorizedResponse(OperationContext operationContext, string reason)
		{
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "Returning a 401 Unauthorized response for reason: {0}", reason);
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendAuthError("AuthFailureReason", reason);
			HttpResponseMessageProperty httpResponseMessageProperty = AutodiscoverAuthorizationManager.GetHttpResponseMessageProperty(operationContext);
			httpResponseMessageProperty.StatusCode = HttpStatusCode.Unauthorized;
			httpResponseMessageProperty.SuppressEntityBody = true;
			return false;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010964 File Offset: 0x0000EB64
		private static bool Return403UnauthorizedResponse(OperationContext operationContext, string reason)
		{
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "Returning a 403 Unauthorized response for reason: {0}", reason);
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendAuthError("AuthFailureReason", reason);
			HttpResponseMessageProperty httpResponseMessageProperty = AutodiscoverAuthorizationManager.GetHttpResponseMessageProperty(operationContext);
			httpResponseMessageProperty.StatusCode = HttpStatusCode.Forbidden;
			httpResponseMessageProperty.SuppressEntityBody = true;
			return false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000109B0 File Offset: 0x0000EBB0
		private static bool Return302RedirectionResponse(OperationContext operationContext, string redirectUrl)
		{
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "Returning a 302 Redirection response to Location: {0}", redirectUrl);
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.SetRedirectionType(RedirectionType.HttpRedirect);
			HttpResponseMessageProperty httpResponseMessageProperty = AutodiscoverAuthorizationManager.GetHttpResponseMessageProperty(operationContext);
			httpResponseMessageProperty.StatusCode = HttpStatusCode.Found;
			httpResponseMessageProperty.SuppressEntityBody = true;
			httpResponseMessageProperty.Headers.Add(HttpResponseHeader.Location, redirectUrl);
			return false;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00010A04 File Offset: 0x0000EC04
		private static HttpResponseMessageProperty GetHttpResponseMessageProperty(OperationContext operationContext)
		{
			object obj = null;
			if (!operationContext.OutgoingMessageProperties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError(0L, "OutgoingMessageProperties.TryGetValue(HttpResponseMessageProperty) returned false.");
			}
			HttpResponseMessageProperty httpResponseMessageProperty;
			if (obj == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError(0L, "OutgoingMessageProperties.TryGetValue(HttpResponseMessageProperty) returned null property value.");
				httpResponseMessageProperty = new HttpResponseMessageProperty();
				operationContext.OutgoingMessageProperties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
			}
			else
			{
				httpResponseMessageProperty = (HttpResponseMessageProperty)obj;
			}
			return httpResponseMessageProperty;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00010A6F File Offset: 0x0000EC6F
		private static bool DoesClaimMatch(Claim claim, string claimTypeToTest, string rightToTest)
		{
			return claim.ClaimType == claimTypeToTest && claim.Right == rightToTest;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010A90 File Offset: 0x0000EC90
		private static bool DoesClaimHaveProperResource<T>(Claim claim, out T resource) where T : class
		{
			resource = default(T);
			if (claim.Resource == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>(0L, "{0}/{1} claim had a null Resource", claim.ClaimType, claim.Right);
				return false;
			}
			resource = (claim.Resource as T);
			if (resource == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug(0L, "{0}/{1} claim had a claim of type {2}, not of type {3}", new object[]
				{
					claim.ClaimType,
					claim.Right,
					claim.Resource.GetType().FullName,
					typeof(T).FullName
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00010B44 File Offset: 0x0000ED44
		private static bool? ProcessTrueFalseClaim(Claim claim)
		{
			string text;
			if (!AutodiscoverAuthorizationManager.DoesClaimHaveProperResource<string>(claim, out text))
			{
				return null;
			}
			bool value;
			if (string.Equals(text, "TRUE"))
			{
				value = true;
			}
			else
			{
				if (!string.Equals(text, "FALSE"))
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string, string>(0L, "{0}/{1} claim resource was not a known value: {2}", claim.ClaimType, claim.Right, text);
					return null;
				}
				value = false;
			}
			return new bool?(value);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		private static AutodiscoverAuthorizationManager.ConsentLevel? ProcessConsentLevelClaim(Claim claim)
		{
			string text;
			if (!AutodiscoverAuthorizationManager.DoesClaimHaveProperResource<string>(claim, out text))
			{
				return null;
			}
			AutodiscoverAuthorizationManager.ConsentLevel value;
			if (string.Equals(text, "NONE"))
			{
				value = AutodiscoverAuthorizationManager.ConsentLevel.None;
			}
			else if (string.Equals(text, "PARTIAL"))
			{
				value = AutodiscoverAuthorizationManager.ConsentLevel.Partial;
			}
			else
			{
				if (!string.Equals(text, "FULL"))
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string, string>(0L, "{0}/{1} claim resource was not a known value: {2}", claim.ClaimType, claim.Right, text);
					return null;
				}
				value = AutodiscoverAuthorizationManager.ConsentLevel.Full;
			}
			return new AutodiscoverAuthorizationManager.ConsentLevel?(value);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010D88 File Offset: 0x0000EF88
		private static bool CheckClaimSets(OperationContext operationContext, ReadOnlyCollection<ClaimSet> claimSets)
		{
			HttpContext.Current.Items["AuthType"] = "LiveIdToken";
			claimSets.TraceClaimSets();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			string text = null;
			string text2 = null;
			foreach (ClaimSet claimSet in claimSets)
			{
				foreach (Claim claim in claimSet)
				{
					if (AutodiscoverAuthorizationManager.DoesClaimMatch(claim, "http://schemas.xmlsoap.org/claims/PUID", Rights.PossessProperty))
					{
						flag = AutodiscoverAuthorizationManager.DoesClaimHaveProperResource<string>(claim, out text);
					}
					else if (AutodiscoverAuthorizationManager.DoesClaimMatch(claim, "http://schemas.xmlsoap.org/claims/ConsumerPUID", Rights.PossessProperty))
					{
						flag2 = AutodiscoverAuthorizationManager.DoesClaimHaveProperResource<string>(claim, out text2);
					}
					else if (AutodiscoverAuthorizationManager.DoesClaimMatch(claim, ClaimTypes.Authentication, Rights.PossessProperty))
					{
						flag3 = true;
					}
					if (flag && flag2 && flag3)
					{
						break;
					}
				}
				if (flag && flag2 && flag3)
				{
					break;
				}
			}
			if (!flag3 || (text == null && text2 == null))
			{
				string reason = string.Format("Did not find all necessary claims. PUID: {0}; ConsumerPUID: {1}; Auth/Possess: {2}", flag, flag2, flag3);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, reason);
			}
			string userId = (text2 == null) ? text : text2;
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("UserPUID", userId);
			SmtpAddress smtpAddress;
			if (!AutodiscoverAuthorizationManager.TryGetEmailAddressInClaimSets(claimSets, out smtpAddress))
			{
				string reason2 = string.Format("Did not find EmailAddress claim for PUID: {0}; ", userId);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, reason2);
			}
			PropertyDefinition[] propertyDefinitionArrayUPN = new PropertyDefinition[]
			{
				ADUserSchema.UserPrincipalName,
				ADMailboxRecipientSchema.SamAccountName,
				ADObjectSchema.OrganizationId
			};
			ADRawEntry adRawEntry = null;
			try
			{
				bool isRootOrgLookup = false;
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency(ServiceLatencyMetadata.CallerADLatency, delegate()
				{
					DateTime utcNow = DateTime.UtcNow;
					ADSessionSettings adsessionSettings = Common.SessionSettingsFromAddress(smtpAddress.ToString());
					RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("CheckClaimSets_SmtpAddress", smtpAddress.ToString());
					RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("AD_ChkClaim_SessionSettingsFromAddress", (DateTime.UtcNow - utcNow).TotalMilliseconds);
					utcNow = DateTime.UtcNow;
					isRootOrgLookup = OrganizationId.ForestWideOrgId.Equals(adsessionSettings.CurrentOrganizationId);
					if (!isRootOrgLookup)
					{
						ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, adsessionSettings, 596, "CheckClaimSets", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\WCF\\AutodiscoverAuthorizationManager.cs");
						RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("AD_TenantRecipientSession", (DateTime.UtcNow - utcNow).TotalMilliseconds);
						utcNow = DateTime.UtcNow;
						adRawEntry = tenantRecipientSession.FindUniqueEntryByNetID(userId, propertyDefinitionArrayUPN);
						RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("AD_FindUniqueEntryByNetID", (DateTime.UtcNow - utcNow).TotalMilliseconds);
					}
				});
				if (isRootOrgLookup)
				{
					return AutodiscoverAuthorizationManager.Return403UnauthorizedResponse(operationContext, "NetID lookup for root org user is not allowed");
				}
			}
			catch (NonUniqueRecipientException arg)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<NonUniqueRecipientException>(0L, "FindUniqueEntryByNetId threw exception: {0}", arg);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Found more than 1 user by NetID in AD");
			}
			if (adRawEntry == null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericError("Redirect as we are unable to find user:.", userId);
				return AutodiscoverAuthorizationManager.RedirectCaller(operationContext, smtpAddress.ToString());
			}
			string arg2 = (string)adRawEntry[ADMailboxRecipientSchema.SamAccountName];
			string text3 = string.Format("{0}@{1}", arg2, adRawEntry.Id.GetPartitionId().ForestFQDN);
			string text4 = (string)adRawEntry[ADUserSchema.UserPrincipalName];
			OrganizationId organizationId = (OrganizationId)adRawEntry[ADObjectSchema.OrganizationId];
			HttpContext.Current.Items["UserOrganizationId"] = organizationId;
			OrganizationProperties organizationProperties;
			if (!OrganizationPropertyCache.TryGetOrganizationProperties(organizationId, out organizationProperties))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<OrganizationId, string>(0L, "[AutodiscoverAuthorizationManager::CheckClaimSets] Logon failed: could not locate org info for organization {0} even though user from this org was found {1}", organizationId, text4);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Could not find organization info via OrganizationPropertyCache");
			}
			if (!organizationProperties.SkipToUAndParentalControlCheck && !AutodiscoverAuthorizationManager.CheckClaimSetsForTOUClaims(operationContext, claimSets, true) && !AutodiscoverAuthorizationManager.CheckClaimSetsForTOUClaims(operationContext, claimSets, false))
			{
				return false;
			}
			WindowsIdentity windowsIdentity = null;
			try
			{
				windowsIdentity = new WindowsIdentity(text3);
			}
			catch (UnauthorizedAccessException ex)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendAuthError("WindowsIdentity_UnauthorizedAccessException", ex.ToString());
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string, object>(0L, "[AutodiscoverAuthorizationManager::CheckClaimSets] UnauthorizedAccessException encountered. UPN: {0}, Exception message: {1}, Identity: {2}", text3, ex.Message, (windowsIdentity == null) ? "<NULL>" : windowsIdentity.User);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Creating WindowsIdentity from UPN failed with a UnauthorizedAccessException");
			}
			catch (SecurityException ex2)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendAuthError("WindowsIdentity_SecurityException", ex2.ToString());
				ExTraceGlobals.AuthenticationTracer.TraceError<string, string, object>(0L, "[AutodiscoverAuthorizationManager::CheckClaimSets] SecurityException encountered. UPN: {0}, Exception message: {1}, Identity: {2}", text3, ex2.Message, (windowsIdentity == null) ? "<NULL>" : windowsIdentity.User);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Creating WindowsIdentity from UPN failed with a SecurityException");
			}
			string org = null;
			if (organizationId != null && organizationId.OrganizationalUnit != null)
			{
				org = organizationId.OrganizationalUnit.Name;
			}
			AutodiscoverAuthorizationManager.PushUserAndOrgInfoToContext(text4, org);
			HttpContext.Current.User = new WindowsPrincipal(windowsIdentity);
			return true;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000111F8 File Offset: 0x0000F3F8
		private static void PushUserAndOrgInfoToContext(string userName, string org)
		{
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Items["AuthenticatedUser"] = userName;
				HttpContext.Current.Items["AuthenticatedUserOrganization"] = org;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001122C File Offset: 0x0000F42C
		private static bool CheckClaimSetsForTOUClaims(OperationContext operationContext, ReadOnlyCollection<ClaimSet> claimSets, bool checkConsumerClaims)
		{
			string claimTypeToTest = checkConsumerClaims ? "http://schemas.xmlsoap.org/claims/ConsumerChild" : "http://schemas.xmlsoap.org/claims/Child";
			string claimTypeToTest2 = checkConsumerClaims ? "http://schemas.xmlsoap.org/claims/ConsumerTOUAccepted" : "http://schemas.xmlsoap.org/claims/TOUAccepted";
			string claimTypeToTest3 = checkConsumerClaims ? "http://schemas.xmlsoap.org/claims/ConsumerConsentLevel" : "http://schemas.xmlsoap.org/claims/ConsentLevel";
			AutodiscoverAuthorizationManager.ConsentLevel? consentLevel = null;
			bool? flag = null;
			bool? flag2 = null;
			foreach (ClaimSet claimSet in claimSets)
			{
				foreach (Claim claim in claimSet)
				{
					if (AutodiscoverAuthorizationManager.DoesClaimMatch(claim, claimTypeToTest, Rights.PossessProperty))
					{
						flag = AutodiscoverAuthorizationManager.ProcessTrueFalseClaim(claim);
					}
					else if (AutodiscoverAuthorizationManager.DoesClaimMatch(claim, claimTypeToTest2, Rights.PossessProperty))
					{
						flag2 = AutodiscoverAuthorizationManager.ProcessTrueFalseClaim(claim);
					}
					else if (AutodiscoverAuthorizationManager.DoesClaimMatch(claim, claimTypeToTest3, Rights.PossessProperty))
					{
						consentLevel = AutodiscoverAuthorizationManager.ProcessConsentLevelClaim(claim);
					}
					if (flag != null && flag2 != null && (!flag.Value || consentLevel != null))
					{
						break;
					}
				}
				if (flag != null && flag2 != null && (!flag.Value || consentLevel != null))
				{
					break;
				}
			}
			if (checkConsumerClaims && flag == null && flag2 == null && consentLevel == null)
			{
				return false;
			}
			if (flag == null)
			{
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Didn't find child claim");
			}
			if (flag2 == null)
			{
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Didn't find TOU claim");
			}
			if (flag.Value && consentLevel == null)
			{
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Didn't find consent level claim for child");
			}
			if (!flag2.Value)
			{
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "TOU was not accepted");
			}
			return !flag.Value || consentLevel.Value != AutodiscoverAuthorizationManager.ConsentLevel.None || AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Child with no consent");
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001142C File Offset: 0x0000F62C
		private static bool RedirectCaller(OperationContext operationContext, string smtpAddress)
		{
			string redirectServer = MServe.GetRedirectServer(smtpAddress);
			if (!string.IsNullOrEmpty(redirectServer))
			{
				return AutodiscoverAuthorizationManager.BuildRedirectUrlAndRedirectCaller(operationContext, redirectServer);
			}
			string reason = string.Format("No redirection server for Identity: {0}; ", smtpAddress.ToString());
			return AutodiscoverAuthorizationManager.Return403UnauthorizedResponse(operationContext, reason);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00011468 File Offset: 0x0000F668
		internal static bool BuildRedirectUrlAndRedirectCaller(OperationContext operationContext, string redirectServer)
		{
			UriBuilder uriBuilder = new UriBuilder(HttpContext.Current.Request.Headers[WellKnownHeader.MsExchProxyUri]);
			uriBuilder.Host = redirectServer;
			string redirectUrl = string.Empty;
			try
			{
				redirectUrl = uriBuilder.Uri.ToString();
			}
			catch (UriFormatException ex)
			{
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrCoreInvalidRedirectionUrl, Common.PeriodicKey, new object[0]);
				ex.Data["FilterExceptionFromWatson"] = true;
				throw ex;
			}
			return AutodiscoverAuthorizationManager.Return302RedirectionResponse(operationContext, redirectUrl);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000114FC File Offset: 0x0000F6FC
		private static bool TryGetEmailAddressInClaimSets(ReadOnlyCollection<ClaimSet> claimSets, out SmtpAddress smtpAddress)
		{
			smtpAddress = SmtpAddress.Empty;
			foreach (ClaimSet claimSet in claimSets)
			{
				foreach (Claim claim in claimSet.FindClaims("http://schemas.xmlsoap.org/claims/EmailAddress", Rights.PossessProperty))
				{
					string text = claim.Resource as string;
					if (!string.IsNullOrEmpty(text) && SmtpAddress.IsValidSmtpAddress(text))
					{
						smtpAddress = new SmtpAddress(text);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000115C0 File Offset: 0x0000F7C0
		private static bool IsAnonymousMethod(OperationContext operationContext)
		{
			return operationContext.IncomingMessageHeaders != null && operationContext.IncomingMessageHeaders.Action != null && StringComparer.InvariantCultureIgnoreCase.Equals(operationContext.IncomingMessageHeaders.Action, "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetFederationInformation");
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000115F4 File Offset: 0x0000F7F4
		private static bool CheckClaimSetsForExternalUser(AuthorizationContext authorizationContext, OperationContext operationContext)
		{
			HttpContext.Current.Items["AuthType"] = "External";
			SamlSecurityToken samlSecurityToken = null;
			foreach (SupportingTokenSpecification supportingTokenSpecification in operationContext.SupportingTokens)
			{
				samlSecurityToken = (supportingTokenSpecification.SecurityToken as SamlSecurityToken);
				if (samlSecurityToken != null)
				{
					break;
				}
			}
			if (samlSecurityToken == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError(0L, "Found no security token in authorization context");
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Cannot find security token in authorization context");
			}
			ExternalAuthentication current = ExternalAuthentication.GetCurrent();
			if (!current.Enabled)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError(0L, "Federation is not enabled");
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Federation is not enabled");
			}
			TokenValidationResults tokenValidationResults = current.TokenValidator.ValidateToken(samlSecurityToken, Offer.Autodiscover);
			if (tokenValidationResults.Result != TokenValidationResult.Valid || !SmtpAddress.IsValidSmtpAddress(tokenValidationResults.EmailAddress))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<TokenValidationResults>(0L, "Validation of security token in WS-Security header failed: {0}", tokenValidationResults);
				return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Validation of the delegation failed");
			}
			SmtpAddress smtpAddress = SmtpAddress.Empty;
			int num = -1;
			try
			{
				num = operationContext.IncomingMessageHeaders.FindHeader("SharingSecurity", "http://schemas.microsoft.com/exchange/services/2006/types");
			}
			catch (MessageHeaderException ex)
			{
				AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Exception when looking for SharingSecurity header in request: " + ex.ToString());
				return false;
			}
			if (num < 0)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug(0L, "Request has no SharingSecurity header");
			}
			else
			{
				XmlElement header = operationContext.IncomingMessageHeaders.GetHeader<XmlElement>(num);
				smtpAddress = SharingKeyHandler.Decrypt(header, tokenValidationResults.ProofToken);
				if (smtpAddress == SmtpAddress.Empty)
				{
					ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "SharingSecurity is present but invalid: {0}", header.OuterXml);
					AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Validation of the SharingSecurity failed");
					return false;
				}
				ExTraceGlobals.AuthenticationTracer.TraceDebug<SmtpAddress>(0L, "SharingSecurity header contains external identity: {0}", smtpAddress);
			}
			AutodiscoverAuthorizationManager.PushUserAndOrgInfoToContext(tokenValidationResults.EmailAddress, null);
			HttpContext.Current.User = new GenericPrincipal(new ExternalIdentity(new SmtpAddress(tokenValidationResults.EmailAddress), smtpAddress), null);
			return true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000117FC File Offset: 0x0000F9FC
		private static bool CheckClaimSetsForPartnerUser(AuthorizationContext authorizationContext, OperationContext operationContext)
		{
			HttpContext.Current.Items["AuthType"] = "Partner";
			PerformanceCounters.UpdateRequestsReceivedWithPartnerToken();
			ReadOnlyCollection<ClaimSet> claimSets = authorizationContext.ClaimSets;
			claimSets.TraceClaimSets();
			DelegatedPrincipal delegatedPrincipal = null;
			OrganizationId delegatedOrganizationId = null;
			string text = null;
			if (!PartnerToken.TryGetDelegatedPrincipalAndOrganizationId(claimSets, out delegatedPrincipal, out delegatedOrganizationId, out text))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "[AutodiscoverAuthorizationManager.CheckClaimSetsForPartnerUser] unable to create partner identity, error message: {0}", text);
				PerformanceCounters.UpdateUnauthorizedRequestsReceivedWithPartnerToken();
				AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, text);
				return false;
			}
			ExTraceGlobals.AuthenticationTracer.TraceDebug<DelegatedPrincipal>(0L, "[AutodiscoverAuthorizationManager.CheckClaimSetsForPartnerUser] ws-security header contains the partner identity: {0}", delegatedPrincipal);
			string text2 = delegatedPrincipal.ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				AutodiscoverAuthorizationManager.PushUserAndOrgInfoToContext(text2, text2.Split(new char[]
				{
					'\\'
				})[0]);
			}
			HttpContext.Current.User = new WindowsPrincipal(PartnerIdentity.Create(delegatedPrincipal, delegatedOrganizationId));
			return true;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000118C4 File Offset: 0x0000FAC4
		private static bool CheckClaimSetsForX509CertUser(AuthorizationContext authorizationContext, OperationContext operationContext)
		{
			HttpContext.Current.Items["AuthType"] = "X509Cert";
			ReadOnlyCollection<ClaimSet> claimSets = authorizationContext.ClaimSets;
			claimSets.TraceClaimSets();
			X509CertUser x509CertUser = null;
			if (!X509CertUser.TryCreateX509CertUser(claimSets, out x509CertUser))
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug(0L, "[AutodiscoverAuthorizationManager.CheckClaimSetsForX509CertUser] unable to create the x509certuser");
				AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "unable to create the X509CertUser based on the given claim sets.");
				return false;
			}
			OrganizationId value;
			WindowsIdentity windowsIdentity;
			string arg;
			if (!x509CertUser.TryGetWindowsIdentity(out value, out windowsIdentity, out arg))
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<X509CertUser>(0L, "[AutodiscoverAuthorizationManager.CheckClaimSetsForX509CertUser] unable to find the windows identity for cert user: {0}", x509CertUser);
				string reason = string.Format("unable to find the windows identity for the given cert {0}, reason: {1}", x509CertUser, arg);
				AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, reason);
				return false;
			}
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>(0L, "[AutodiscoverAuthorizationManager.CheckClaimSetsForX509CertUser] ws-security header contains the x509 cert user identity: {0}, upn: {1}", Common.GetIdentityNameForTrace(windowsIdentity), x509CertUser.UserPrincipalName);
			AutodiscoverAuthorizationManager.PushUserAndOrgInfoToContext(x509CertUser.UserPrincipalName, null);
			HttpContext.Current.User = new WindowsPrincipal(windowsIdentity);
			HttpContext.Current.Items["UserOrganizationId"] = value;
			return true;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000119AC File Offset: 0x0000FBAC
		private bool InternalCheckAccessCore(OperationContext operationContext)
		{
			string text = operationContext.RequestContext.RequestMessage.Headers.Action;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Substring(text.LastIndexOf('/') + 1);
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.ActivityScope.Action = text;
			}
			if (AutodiscoverAuthorizationManager.IsAnonymousMethod(operationContext))
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "Allowing request to go anonymous: {0}", operationContext.IncomingMessageHeaders.Action);
				return true;
			}
			HttpContext httpContext = HttpContext.Current;
			HttpApplication applicationInstance = httpContext.ApplicationInstance;
			if (!httpContext.Request.IsAuthenticated)
			{
				if (ServiceSecurityContext.Current == null)
				{
					return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "ServiceSecurityContext.Current was null");
				}
				AuthorizationContext authorizationContext = ServiceSecurityContext.Current.AuthorizationContext;
				if (authorizationContext == null)
				{
					return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "authContext was null");
				}
				if (authorizationContext.ClaimSets == null)
				{
					return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "authContext.ClaimSets was null");
				}
				if (authorizationContext.ClaimSets.Count == 0)
				{
					return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "authContext.ClaimSets.Count was 0");
				}
				if (AutodiscoverAuthorizationManager.IsDelegationToken(authorizationContext.ClaimSets))
				{
					if (!AutodiscoverAuthorizationManager.CheckClaimSetsForExternalUser(authorizationContext, operationContext))
					{
						return false;
					}
				}
				else
				{
					if (!VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.LogonViaStandardTokens.Enabled)
					{
						return AutodiscoverAuthorizationManager.Return401UnauthorizedResponse(operationContext, "No login via standard token on-premises");
					}
					Uri uri = operationContext.Channel.LocalAddress.Uri;
					if (Common.IsWsSecuritySymmetricKeyAddress(uri))
					{
						if (!AutodiscoverAuthorizationManager.CheckClaimSetsForPartnerUser(authorizationContext, operationContext))
						{
							return false;
						}
					}
					else if (Common.IsWsSecurityX509CertAddress(uri))
					{
						if (!AutodiscoverAuthorizationManager.CheckClaimSetsForX509CertUser(authorizationContext, operationContext))
						{
							return false;
						}
					}
					else
					{
						if (!Common.IsWsSecurityAddress(uri))
						{
							return false;
						}
						if (!AutodiscoverAuthorizationManager.CheckClaimSets(operationContext, authorizationContext.ClaimSets))
						{
							return false;
						}
					}
				}
			}
			Common.ResolveCaller();
			return base.CheckAccessCore(operationContext);
		}

		// Token: 0x0400028A RID: 650
		private const string X509CertAuth = "X509Cert";

		// Token: 0x0400028B RID: 651
		private const string ExternalAuth = "External";

		// Token: 0x0400028C RID: 652
		private const string PartnerAuth = "Partner";

		// Token: 0x0400028D RID: 653
		private const string LiveIdTokenAuth = "LiveIdToken";

		// Token: 0x0400028E RID: 654
		internal const string SamlEmailAddress = "http://schemas.xmlsoap.org/claims/EmailAddress";

		// Token: 0x0400028F RID: 655
		private const string PUIDClaimType = "http://schemas.xmlsoap.org/claims/PUID";

		// Token: 0x04000290 RID: 656
		private const string ConsumerPUIDClaimType = "http://schemas.xmlsoap.org/claims/ConsumerPUID";

		// Token: 0x04000291 RID: 657
		private const string ChildClaimType = "http://schemas.xmlsoap.org/claims/Child";

		// Token: 0x04000292 RID: 658
		private const string ConsumerChildClaimType = "http://schemas.xmlsoap.org/claims/ConsumerChild";

		// Token: 0x04000293 RID: 659
		private const string ConsentLevelClaimType = "http://schemas.xmlsoap.org/claims/ConsentLevel";

		// Token: 0x04000294 RID: 660
		private const string ConsumerConsentLevelClaimType = "http://schemas.xmlsoap.org/claims/ConsumerConsentLevel";

		// Token: 0x04000295 RID: 661
		private const string TOUAcceptedClaimType = "http://schemas.xmlsoap.org/claims/TOUAccepted";

		// Token: 0x04000296 RID: 662
		private const string ConsumerTOUAcceptedClaimType = "http://schemas.xmlsoap.org/claims/ConsumerTOUAccepted";

		// Token: 0x04000297 RID: 663
		private const string ConsentLevelNone = "NONE";

		// Token: 0x04000298 RID: 664
		private const string ConsentLevelPartial = "PARTIAL";

		// Token: 0x04000299 RID: 665
		private const string ConsentLevelFull = "FULL";

		// Token: 0x0400029A RID: 666
		private const string TrueAllCapitals = "TRUE";

		// Token: 0x0400029B RID: 667
		private const string FalseAllCapitals = "FALSE";

		// Token: 0x02000056 RID: 86
		private enum ConsentLevel
		{
			// Token: 0x0400029D RID: 669
			None,
			// Token: 0x0400029E RID: 670
			Partial,
			// Token: 0x0400029F RID: 671
			Full
		}
	}
}
