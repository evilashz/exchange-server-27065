using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200004C RID: 76
	internal class MobileRedirectOptimization
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public static bool TryGetEasServerFromConfig(ADRecipient user, string userAgent, out string easServerName)
		{
			OrganizationRelationship organizationRelationship = null;
			easServerName = null;
			if (user == null)
			{
				ExTraceGlobals.FrameworkTracer.TraceError(0L, "[MobileRedirectOptimization] User object is null. Proceeding with <Redirect>.");
				return false;
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>(0L, "[MobileRedirectOptimization] Attempting to retrieve EAS settings with OrganizationRelationship for user {0}, user agent {1}.", MobileRedirectOptimization.SafeGetEmailAddressStringFromADUser(user), userAgent ?? string.Empty);
			string text = FaultInjection.TraceTest<string>((FaultInjection.LIDs)3866504509U);
			if (text == null)
			{
				if (MobileRedirectOptimization.settings.Member.Enabled)
				{
					if (MobileRedirectOptimization.settings.Member.UserAgentEnabled(userAgent))
					{
						organizationRelationship = MobileRedirectOptimization.GetOrganizationRelationship(user, null);
					}
					else
					{
						ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "[MobileRedirectOptimization] Redirect bypass is disabled for user agent {0}. Proceeding with <Redirect>.", userAgent ?? string.Empty);
					}
				}
				else
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug(0L, "[MobileRedirectOptimization] Redirect bypass is disabled globally. Proceeding with <Redirect>.");
				}
			}
			else
			{
				organizationRelationship = MobileRedirectOptimization.GetOrganizationRelationship(user, text);
			}
			if (organizationRelationship != null)
			{
				if (organizationRelationship.Enabled)
				{
					easServerName = MobileRedirectOptimization.GetEasServerFromOrgRelationship(user, organizationRelationship);
				}
				else
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "[MobileRedirectOptimization] OrganizationRelationship is disabled for user {0}. Proceeding with <Redirect>.", MobileRedirectOptimization.SafeGetEmailAddressStringFromADUser(user));
				}
			}
			else
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string>(0L, "[MobileRedirectOptimization] OrganizationRelationship retrieval failed for user {0}. Proceeding with <Redirect>.", MobileRedirectOptimization.SafeGetEmailAddressStringFromADUser(user));
			}
			return easServerName != null;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		private static OrganizationRelationship GetOrganizationRelationship(ADRecipient user, string overrideOwaUrlString = null)
		{
			if (overrideOwaUrlString != null)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug(0L, "[MobileRedirectOptimization] Creating mock OrganizationRelationship for testing.");
				return new OrganizationRelationship
				{
					TargetOwaURL = new Uri(overrideOwaUrlString),
					Enabled = true
				};
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "[MobileRedirectOptimization] Attempting to retrieve OrganizationRelationship for user {0}.", MobileRedirectOptimization.SafeGetEmailAddressStringFromADUser(user));
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(user.OrganizationId);
			if (organizationIdCacheValue != null)
			{
				string text = MobileRedirectOptimization.SafeGetEmailDomainFromADUser(user);
				if (text != null)
				{
					return organizationIdCacheValue.GetOrganizationRelationship(text);
				}
			}
			return null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CC68 File Offset: 0x0000AE68
		private static string GetEasServerFromOrgRelationship(ADRecipient user, OrganizationRelationship organizationRelationship)
		{
			Uri targetOwaURL = organizationRelationship.TargetOwaURL;
			if (targetOwaURL != null && targetOwaURL.Host != null)
			{
				string text;
				if (targetOwaURL.Host.Contains(MobileRedirectOptimization.legacyO365OwaHost))
				{
					text = MobileRedirectOptimization.correctO365OwaUrl;
				}
				else
				{
					text = MobileRedirectOptimization.httpsPrefix + targetOwaURL.Host + MobileRedirectOptimization.activeSyncServerSuffix;
				}
				ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>(0L, "[MobileRedirectOptimization] Redirect bypass succeeded, writing config xml for user {0} using EAS server name {1}.", MobileRedirectOptimization.SafeGetEmailAddressStringFromADUser(user), text);
				return text;
			}
			ExTraceGlobals.FrameworkTracer.TraceError<string>(0L, "[MobileRedirectOptimization] TargetOwaUrl parsing failed for user {0}. Proceeding with <Redirect>.", MobileRedirectOptimization.SafeGetEmailAddressStringFromADUser(user));
			return null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CCEF File Offset: 0x0000AEEF
		private static string SafeGetEmailAddressStringFromADUser(ADRecipient user)
		{
			if (!(user.ExternalEmailAddress != null))
			{
				return string.Empty;
			}
			return user.ExternalEmailAddress.AddressString ?? string.Empty;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		private static string SafeGetEmailDomainFromADUser(ADRecipient user)
		{
			SmtpProxyAddress smtpProxyAddress = user.ExternalEmailAddress as SmtpProxyAddress;
			if (!(smtpProxyAddress != null))
			{
				return null;
			}
			return ((SmtpAddress)smtpProxyAddress).Domain;
		}

		// Token: 0x04000245 RID: 581
		private static string legacyO365OwaHost = "outlook.com";

		// Token: 0x04000246 RID: 582
		private static string correctO365OwaUrl = "https://outlook.office365.com/Microsoft-Server-ActiveSync";

		// Token: 0x04000247 RID: 583
		private static string activeSyncServerSuffix = "/Microsoft-Server-ActiveSync";

		// Token: 0x04000248 RID: 584
		private static string httpsPrefix = "https://";

		// Token: 0x04000249 RID: 585
		private static LazyMember<MobileRedirectOptimizationSettings> settings = new LazyMember<MobileRedirectOptimizationSettings>(() => new MobileRedirectOptimizationSettings());
	}
}
