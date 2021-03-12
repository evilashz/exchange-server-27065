using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D7 RID: 471
	internal abstract class SipRoutingHelper
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x0003CCBC File Offset: 0x0003AEBC
		public static SipRoutingHelper Create(PlatformCallInfo callInfo)
		{
			string text = callInfo.RequestUri.FindParameter("ms-organization");
			string remoteMatchedFQDN = callInfo.RemoteMatchedFQDN;
			bool flag = SipPeerManager.Instance.IsLocalDiagnosticCall(callInfo.RemotePeer, callInfo.RemoteHeaders);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, 0, "Create: orgParameter:{0} remoteMatchedFqdn:{1} isLocalDiagnosticCall:{2}", new object[]
			{
				text,
				remoteMatchedFQDN,
				flag
			});
			SipRoutingHelper result;
			if (callInfo.IsInbound)
			{
				result = SipRoutingHelper.CreateForInbound(flag, remoteMatchedFQDN, text);
			}
			else
			{
				if (!callInfo.IsServiceRequest)
				{
					throw new InvalidOperationException();
				}
				result = SipRoutingHelper.CreateForServiceRequest(remoteMatchedFQDN, text);
			}
			return result;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003CD58 File Offset: 0x0003AF58
		public static SipRoutingHelper CreateForInbound(bool isLocalDiagnosticCall, string remotedMatchedFqdn, string orgParameter)
		{
			SipRoutingHelper sipRoutingHelper = (!isLocalDiagnosticCall && SipPeerManager.Instance.IsAccessProxyWithOrgTestHook(remotedMatchedFqdn, orgParameter)) ? SipRoutingHelper.MsOrganizationRoutingHelper.Create() : SipRoutingHelper.DefaultRoutingHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, 0, "CreateForInbound {0}", new object[]
			{
				sipRoutingHelper.GetType().Name
			});
			return sipRoutingHelper;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		public static SipRoutingHelper CreateForServiceRequest(string remoteMatchedFqdn, string orgParameter)
		{
			if (string.IsNullOrEmpty(remoteMatchedFqdn))
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.UnsupportedRequest, "TLS connection required.", new object[0]);
			}
			if (CommonConstants.UseDataCenterCallRouting && !SipPeerManager.Instance.IsAccessProxy(remoteMatchedFqdn))
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.UnsupportedRequest, null, new object[0]);
			}
			SipRoutingHelper sipRoutingHelper = SipRoutingHelper.MsOrganizationRoutingHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, 0, "CreateForServiceRequest {0}", new object[]
			{
				sipRoutingHelper.GetType().Name
			});
			return sipRoutingHelper;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003CE40 File Offset: 0x0003B040
		public static SipRoutingHelper CreateForOutbound(UMDialPlan dialPlan)
		{
			SipRoutingHelper sipRoutingHelper = (CommonConstants.UseDataCenterCallRouting && dialPlan.URIType == UMUriType.SipName) ? SipRoutingHelper.MsOrganizationRoutingHelper.Create() : SipRoutingHelper.DefaultRoutingHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, 0, "CreateForOutbound {0}", new object[]
			{
				sipRoutingHelper.GetType().Name
			});
			return sipRoutingHelper;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003CE96 File Offset: 0x0003B096
		public static bool UseGlobalAccessProxyForOutbound(UMDialPlan dialPlan)
		{
			return CommonConstants.UseDataCenterCallRouting && dialPlan.URIType == UMUriType.SipName;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003CEAA File Offset: 0x0003B0AA
		public static bool UseGlobalSBCSettingsForOutbound(UMIPGateway gw)
		{
			return CommonConstants.UseDataCenterCallRouting && gw.GlobalCallRoutingScheme != UMGlobalCallRoutingScheme.E164;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		public static string GetCrossSiteRedirectTargetFqdnAndPort(Server exchangeServer, bool useTls, out int port)
		{
			UMServer umserver = new UMServer(exchangeServer);
			string text;
			if (umserver.ExternalServiceFqdn != null)
			{
				text = umserver.ExternalServiceFqdn.ToString();
				port = Utils.GetRedirectPort(useTls);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "GetCrossSiteRedirectTargetFqdnAndPort -> load balancer {0}:{1}", new object[]
				{
					text,
					port
				});
			}
			else
			{
				text = Utils.TryGetRedirectTargetFqdnForServer(exchangeServer);
				port = Utils.GetRedirectPort(umserver.SipTcpListeningPort, umserver.SipTlsListeningPort, useTls);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "GetCrossSiteRedirectTargetFqdnAndPort -> direct server {0}:{1}", new object[]
				{
					text,
					port
				});
			}
			return text;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003CF6C File Offset: 0x0003B16C
		public static string[] ParseMsOrganizationParameter(string parameterValue, string requestUriUser, bool allowMultipleEntries)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "ParseMsOrganizationParameter: parameterValue:{0} requestUriUser:{1} allowMultipleEntries:{2}", new object[]
			{
				parameterValue,
				requestUriUser,
				allowMultipleEntries
			});
			string[] array = (parameterValue != null) ? parameterValue.Split(CommonConstants.MsOrganizationDomainSeparators, StringSplitOptions.RemoveEmptyEntries) : null;
			if (array == null || array.Length == 0)
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MsOrganizationRequired, null, new object[0]);
			}
			if (array.Length > 1 && !allowMultipleEntries)
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MsOrganizationCannotHaveMultipleEntries, "Value: {0}.", new object[]
				{
					parameterValue
				});
			}
			if (array.Length > 3)
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MsOrganizationHasTooManyEntries, "Value: {0}.", new object[]
				{
					parameterValue
				});
			}
			return array;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003D02C File Offset: 0x0003B22C
		public static OrganizationId GetOrganizationIdFromTenantDomainList(string[] domainList)
		{
			string text;
			return SipRoutingHelper.GetOrganizationIdFromTenantDomainList(domainList, out text);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003D044 File Offset: 0x0003B244
		public static OrganizationId GetOrganizationIdFromTenantDomainList(string[] domainList, out string acceptedDomain)
		{
			OrganizationId organizationId = null;
			bool flag = false;
			acceptedDomain = null;
			foreach (string text in domainList)
			{
				if (!SmtpAddress.IsValidDomain(text))
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MsOrganizationHasInvalidDomain, "Value: {0}.", new object[]
					{
						text
					});
				}
				try
				{
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromAcceptedDomain(text);
					organizationId = iadsystemConfigurationLookup.GetOrganizationIdFromDomainName(text, out flag);
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "DomainName:{0} -> OrganizationId:{1}", new object[]
					{
						text,
						organizationId
					});
					if (null != organizationId)
					{
						if (!flag)
						{
							UmGlobals.ExEvent.LogEvent(organizationId, UMEventLogConstants.Tuple_MsOrganizationNotAuthoritativeDomain, text, text);
							throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MsOrganizationDoesNotMatchAuthoritativeDomain, "Value: {0}.", new object[]
							{
								text
							});
						}
						acceptedDomain = text;
						break;
					}
				}
				catch (CannotResolveTenantNameException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "DomainName='{0}' could not be found: {1}", new object[]
					{
						text,
						ex
					});
				}
			}
			return organizationId;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DB6 RID: 3510
		public abstract int RedirectResponseCode { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DB7 RID: 3511
		public abstract bool SupportsMsOrganizationRouting { get; }

		// Token: 0x06000DB8 RID: 3512
		public abstract string GetContactHeaderHost(string targetHost, out string msfeParameter);

		// Token: 0x06000DB9 RID: 3513
		public abstract SipRoutingHelper.Context GetRoutingContext(string toUri, string fromUri, string diversionUri, PlatformSipUri requestUri);

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003D16C File Offset: 0x0003B36C
		public SipRoutingHelper.Context GetRoutingContext(string subscriberUri, PlatformSipUri requestUri)
		{
			return this.GetRoutingContext(null, null, subscriberUri, requestUri);
		}

		// Token: 0x020001D8 RID: 472
		public sealed class Context
		{
			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0003D180 File Offset: 0x0003B380
			// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0003D188 File Offset: 0x0003B388
			public ADObjectId DialPlanId { get; set; }

			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0003D191 File Offset: 0x0003B391
			// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0003D199 File Offset: 0x0003B399
			public UMAutoAttendant AutoAttendant { get; set; }

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0003D1A2 File Offset: 0x0003B3A2
			// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0003D1AA File Offset: 0x0003B3AA
			public string SubscriberUri { get; set; }

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0003D1B3 File Offset: 0x0003B3B3
			// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0003D1BB File Offset: 0x0003B3BB
			public ADRecipient Recipient { get; set; }

			// Token: 0x1700036D RID: 877
			// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
			// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0003D1CC File Offset: 0x0003B3CC
			public OrganizationId OrgId { get; set; }
		}

		// Token: 0x020001D9 RID: 473
		internal class MsOrganizationRoutingHelper : SipRoutingHelper
		{
			// Token: 0x1700036E RID: 878
			// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0003D1DD File Offset: 0x0003B3DD
			public override int RedirectResponseCode
			{
				get
				{
					return 303;
				}
			}

			// Token: 0x1700036F RID: 879
			// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
			public override bool SupportsMsOrganizationRouting
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x0003D1E7 File Offset: 0x0003B3E7
			public static SipRoutingHelper Create()
			{
				return new SipRoutingHelper.MsOrganizationRoutingHelper();
			}

			// Token: 0x06000DCA RID: 3530 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
			public override string GetContactHeaderHost(string targetHost, out string msfeParameter)
			{
				if (!CommonConstants.UseDataCenterCallRouting)
				{
					msfeParameter = null;
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "In enterprise scenario, returning the targethost {0}", new object[]
					{
						targetHost
					});
					return targetHost;
				}
				X509Certificate2 umcertificate = CertificateUtils.UMCertificate;
				ExAssert.RetailAssert(null != umcertificate, "CertificateUtils.UMCertificate not set");
				msfeParameter = targetHost;
				string subjectFqdn = CertificateUtils.GetSubjectFqdn(umcertificate);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetContactHeaderHost(targetHost:{0}) ->  HostFqdn:{1} MsFe:{2}", new object[]
				{
					targetHost,
					subjectFqdn,
					msfeParameter
				});
				return subjectFqdn;
			}

			// Token: 0x06000DCB RID: 3531 RVA: 0x0003D26C File Offset: 0x0003B46C
			public override SipRoutingHelper.Context GetRoutingContext(string toUri, string fromUri, string diversionUri, PlatformSipUri requestUri)
			{
				SipRoutingHelper.Context result = null;
				bool flag = !string.IsNullOrEmpty(diversionUri);
				string text = requestUri.FindParameter("ms-organization");
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "MsOrganizationRoutingHelper.GetRoutingContext. Ms-organization:{0} IsDivertedCall:{1} UserParameter:{2}", new object[]
				{
					text,
					flag,
					requestUri.UserParameter
				});
				bool flag2 = flag || requestUri.UserParameter != UserParameter.Phone;
				OrganizationId organizationId;
				if (!CommonConstants.UseDataCenterCallRouting)
				{
					organizationId = OrganizationId.ForestWideOrgId;
				}
				else
				{
					string[] domainList = SipRoutingHelper.ParseMsOrganizationParameter(text, requestUri.User, flag2);
					organizationId = SipRoutingHelper.GetOrganizationIdFromTenantDomainList(domainList);
					if (null == organizationId)
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MsOrganizationCannotFindTenant, "Value: {0}.", new object[]
						{
							text
						});
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "RequestUri:{0}", new object[]
				{
					requestUri
				});
				string uri = requestUri.ToString();
				try
				{
					if (flag2)
					{
						uri = (flag ? diversionUri : toUri);
						result = this.ResolveContextFromSubscriberCall(toUri, fromUri, diversionUri, organizationId);
					}
					else
					{
						if (requestUri.UserParameter != UserParameter.Phone || !Utils.IsUriValid(requestUri.User, UMUriType.E164))
						{
							throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidAccessProxyRequestUri, "Value: {0}.", new object[]
							{
								requestUri
							});
						}
						result = this.ResolveContextFromE164Call(requestUri, organizationId);
					}
				}
				catch (CallRejectedException error)
				{
					SipRoutingHelper.MsOrganizationRoutingHelper.LogUnableToResolveContextTenantEvent(organizationId, uri, flag2, error);
					throw;
				}
				return result;
			}

			// Token: 0x06000DCC RID: 3532 RVA: 0x0003D3E8 File Offset: 0x0003B5E8
			private static void LogUnableToResolveContextTenantEvent(OrganizationId organizationId, string uri, bool isSubscriberOrVoiceMailCall, CallRejectedException error)
			{
				if (isSubscriberOrVoiceMailCall)
				{
					UmGlobals.ExEvent.LogEvent(organizationId, UMEventLogConstants.Tuple_OCSUserNotProvisioned, uri, CommonUtil.ToEventLogString(CallId.Id), CommonUtil.ToEventLogString(uri));
					return;
				}
				UmGlobals.ExEvent.LogEvent(organizationId, UMEventLogConstants.Tuple_DialPlanOrAutoAttendantNotProvisioned, uri, CommonUtil.ToEventLogString(CallId.Id), CommonUtil.ToEventLogString(uri));
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x0003D440 File Offset: 0x0003B640
			private static ADRecipient GetRecipientByEumOrSipProxy(OrganizationId organizationId, string lookupUri)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "Looking up recipient by EUM prefix: {0}", new object[]
				{
					lookupUri
				});
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(organizationId, null);
				ADRecipient adrecipient = iadrecipientLookup.LookupByEumSipResourceIdentifierPrefix(lookupUri);
				if (adrecipient == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "Looking up recipient by SIP proxy: {0}", new object[]
					{
						lookupUri
					});
					adrecipient = iadrecipientLookup.LookupBySipExtension(lookupUri);
					if (adrecipient == null)
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.CouldNotFindUserBySipUri, "User: {0}.", new object[]
						{
							lookupUri
						});
					}
				}
				PIIMessage data = PIIMessage.Create(PIIType._User, adrecipient.Id);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, data, "Found Recipient:_User Type:{0}", new object[]
				{
					adrecipient.RecipientType
				});
				return adrecipient;
			}

			// Token: 0x06000DCE RID: 3534 RVA: 0x0003D514 File Offset: 0x0003B714
			private SipRoutingHelper.Context ResolveContextFromSubscriberCall(string toUri, string fromUri, string diversionUri, OrganizationId organizationId)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ResolveContextFromSubscriberCall({0})", new object[]
				{
					organizationId
				});
				string text;
				if (!string.IsNullOrEmpty(diversionUri))
				{
					text = diversionUri;
				}
				else
				{
					text = toUri;
					if (!string.Equals(text, fromUri, StringComparison.OrdinalIgnoreCase))
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.ToAndFromHeadersMustMatch, "To: {0}. From:{1}.", new object[]
						{
							toUri,
							fromUri
						});
					}
				}
				ADRecipient recipientByEumOrSipProxy = SipRoutingHelper.MsOrganizationRoutingHelper.GetRecipientByEumOrSipProxy(organizationId, text);
				if (recipientByEumOrSipProxy.RecipientType != RecipientType.UserMailbox && recipientByEumOrSipProxy.RecipientType != RecipientType.MailUser)
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.CouldNotFindUserBySipUri, "User: {0}.", new object[]
					{
						text
					});
				}
				ADUser aduser = recipientByEumOrSipProxy as ADUser;
				if (recipientByEumOrSipProxy.RecipientType == RecipientType.MailUser || aduser == null || aduser.UMRecipientDialPlanId == null)
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MailboxIsNotUMEnabled, "User: {0}. PrimarySmtpAddress: {1}.", new object[]
					{
						text,
						recipientByEumOrSipProxy.PrimarySmtpAddress
					});
				}
				return new SipRoutingHelper.Context
				{
					Recipient = recipientByEumOrSipProxy,
					SubscriberUri = text,
					DialPlanId = aduser.UMRecipientDialPlanId,
					OrgId = aduser.OrganizationId
				};
			}

			// Token: 0x06000DCF RID: 3535 RVA: 0x0003D644 File Offset: 0x0003B844
			private SipRoutingHelper.Context ResolveContextFromE164Call(PlatformSipUri requestUri, OrganizationId organizationId)
			{
				SipRoutingHelper.Context context = null;
				string user = requestUri.User;
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ResolveContextFromE164Call. Org:{0} E164Number:{1}", new object[]
				{
					organizationId,
					user
				});
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(organizationId);
				UMDialPlan dialPlanFromPilotIdentifier = iadsystemConfigurationLookup.GetDialPlanFromPilotIdentifier(user);
				if (dialPlanFromPilotIdentifier != null)
				{
					context = new SipRoutingHelper.Context();
					context.DialPlanId = dialPlanFromPilotIdentifier.Id;
					context.OrgId = dialPlanFromPilotIdentifier.OrganizationId;
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Found DP:{0}", new object[]
					{
						dialPlanFromPilotIdentifier.Id
					});
				}
				else
				{
					UMAutoAttendant autoAttendantWithNoDialplanInformation = iadsystemConfigurationLookup.GetAutoAttendantWithNoDialplanInformation(user);
					if (autoAttendantWithNoDialplanInformation != null)
					{
						context = new SipRoutingHelper.Context();
						context.DialPlanId = autoAttendantWithNoDialplanInformation.UMDialPlan;
						context.AutoAttendant = autoAttendantWithNoDialplanInformation;
						context.OrgId = autoAttendantWithNoDialplanInformation.OrganizationId;
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Found AA:{0}, DP:{1}", new object[]
						{
							autoAttendantWithNoDialplanInformation.Id,
							autoAttendantWithNoDialplanInformation.UMDialPlan
						});
					}
				}
				if (context == null)
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.NoObjectFoundForE164Number, "Value: {0}.", new object[]
					{
						user
					});
				}
				return context;
			}
		}

		// Token: 0x020001DA RID: 474
		internal class DefaultRoutingHelper : SipRoutingHelper
		{
			// Token: 0x17000370 RID: 880
			// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003D769 File Offset: 0x0003B969
			public override int RedirectResponseCode
			{
				get
				{
					return 302;
				}
			}

			// Token: 0x17000371 RID: 881
			// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003D770 File Offset: 0x0003B970
			public override bool SupportsMsOrganizationRouting
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000DD3 RID: 3539 RVA: 0x0003D773 File Offset: 0x0003B973
			public static SipRoutingHelper Create()
			{
				return new SipRoutingHelper.DefaultRoutingHelper();
			}

			// Token: 0x06000DD4 RID: 3540 RVA: 0x0003D77A File Offset: 0x0003B97A
			public override string GetContactHeaderHost(string targetHost, out string msfeParameter)
			{
				msfeParameter = null;
				return targetHost;
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x0003D780 File Offset: 0x0003B980
			public override SipRoutingHelper.Context GetRoutingContext(string toUri, string fromUri, string diversionUri, PlatformSipUri requestUri)
			{
				return null;
			}
		}
	}
}
