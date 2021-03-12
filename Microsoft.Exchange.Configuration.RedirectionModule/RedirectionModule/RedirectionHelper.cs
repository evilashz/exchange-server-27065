using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.RedirectionModule.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x02000009 RID: 9
	public sealed class RedirectionHelper
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003470 File Offset: 0x00001670
		internal static Uri GetRedirectUrlForTenantForest(string domain, string podRedirectTemplate, Uri originalUrl, int podSiteStartRange, int podSiteEndRange)
		{
			int partnerId = LocalSiteCache.LocalSite.PartnerId;
			Guid orgId;
			string redirectServer;
			if (GuidHelper.TryParseGuid(domain, out orgId))
			{
				redirectServer = EdgeSyncMservConnector.GetRedirectServer(podRedirectTemplate, orgId, partnerId, podSiteStartRange, podSiteEndRange);
			}
			else
			{
				redirectServer = EdgeSyncMservConnector.GetRedirectServer(podRedirectTemplate, string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", domain), partnerId, podSiteStartRange, podSiteEndRange);
			}
			if (string.IsNullOrEmpty(redirectServer))
			{
				return null;
			}
			UriBuilder uriBuilder = new UriBuilder(originalUrl);
			uriBuilder.Host = redirectServer;
			if (uriBuilder.Port == 444)
			{
				uriBuilder.Port = 443;
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000034EC File Offset: 0x000016EC
		internal static Uri GetRedirectUrlForTenantSite(string domainName, string redirectTemplate, Uri originalUrl)
		{
			return RedirectionHelper.GetRedirectUrlForTenantSite(domainName, redirectTemplate, originalUrl, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000034F8 File Offset: 0x000016F8
		internal static Uri GetRedirectUrlForTenantSite(string organization, string redirectTemplate, Uri originalUrl, ExEventLog eventLogger)
		{
			if (organization == null)
			{
				return null;
			}
			ADSessionSettings sessionSettings;
			try
			{
				Guid externalDirectoryOrganizationId;
				sessionSettings = (Guid.TryParse(organization, out externalDirectoryOrganizationId) ? ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId) : ADSessionSettings.FromTenantCUName(organization));
			}
			catch (CannotResolveTenantNameException)
			{
				return null;
			}
			catch (CannotResolveExternalDirectoryOrganizationIdException)
			{
				return null;
			}
			ITenantConfigurationSession session = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 180, "GetRedirectUrlForTenantSite", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\RedirectionModule\\RedirectionHelper.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnit = RedirectionHelper.ResolveConfigurationUnitByName(organization, session);
			if (exchangeConfigurationUnit == null || exchangeConfigurationUnit.ManagementSiteLink == null)
			{
				return null;
			}
			ADSite localSite = LocalSiteCache.LocalSite;
			ADObjectId adobjectId = ADObjectIdResolutionHelper.ResolveDN(exchangeConfigurationUnit.ManagementSiteLink);
			Logger.LogEvent(eventLogger, TaskEventLogConstants.Tuple_LiveIdRedirection_UsingManagementSiteLink, organization, new object[]
			{
				organization,
				exchangeConfigurationUnit.AdminDisplayVersion,
				exchangeConfigurationUnit.ManagementSiteLink
			});
			if (adobjectId.Equals(localSite.Id))
			{
				return null;
			}
			foreach (ADObjectId adobjectId2 in localSite.ResponsibleForSites)
			{
				if (adobjectId2.Equals(adobjectId))
				{
					Logger.LogEvent(eventLogger, TaskEventLogConstants.Tuple_LiveIdRedirection_TargetSitePresentOnResponsibleForSite, organization, new object[]
					{
						organization,
						adobjectId,
						adobjectId2
					});
					return null;
				}
			}
			return RedirectionHelper.GetRedirectUrlForTenantSite(adobjectId, redirectTemplate, originalUrl);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000365C File Offset: 0x0000185C
		internal static bool ShouldProcessLiveIdRedirection(HttpContext context)
		{
			return !(context.User is DelegatedPrincipal) && string.IsNullOrEmpty(RedirectionHelper.ResolveOrganizationName(context));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003678 File Offset: 0x00001878
		internal static string ResolveOrganizationName(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			HttpRequest request = httpContext.Request;
			NameValueCollection urlProperties = RedirectionHelper.GetUrlProperties(request.Url);
			string text = null;
			foreach (string name in RedirectionHelper.tenantRedirectionPropertyNames)
			{
				if (!string.IsNullOrEmpty(urlProperties[name]))
				{
					text = urlProperties[name];
				}
			}
			if (text == null && httpContext.Items["Cert-MemberOrg"] != null)
			{
				text = (string)httpContext.Items["Cert-MemberOrg"];
			}
			return text;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000370C File Offset: 0x0000190C
		internal static NameValueCollection GetUrlProperties(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			return HttpUtility.ParseQueryString(uriBuilder.Query.Replace(';', '&'));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003748 File Offset: 0x00001948
		internal static void InitTenantsOnCurrentSiteCache()
		{
			if (RedirectionConfig.CurrentSiteTenantsCacheExpirationInHours <= 0)
			{
				return;
			}
			lock (RedirectionHelper.syncObject)
			{
				if (RedirectionHelper.currentSiteTenants == null)
				{
					RedirectionHelper.ExpireCurrentSiteTenantsCache();
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003798 File Offset: 0x00001998
		internal static bool IsTenantOnCurrentSiteCache(string tenantName)
		{
			if (RedirectionHelper.currentSiteTenants == null)
			{
				return false;
			}
			bool result;
			lock (RedirectionHelper.syncObject)
			{
				if (RedirectionHelper.IsCurrentSiteTenantsCacheExpired())
				{
					RedirectionHelper.ExpireCurrentSiteTenantsCache();
				}
				result = RedirectionHelper.currentSiteTenants.Contains(tenantName);
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000037F4 File Offset: 0x000019F4
		internal static bool IsUserTenantOnCurrentSiteCache(string userName)
		{
			if (RedirectionHelper.currentSiteTenants == null)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(userName) && SmtpAddress.IsValidSmtpAddress(userName))
			{
				string domain = SmtpAddress.Parse(userName).Domain;
				return RedirectionHelper.IsTenantOnCurrentSiteCache(domain);
			}
			return false;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003834 File Offset: 0x00001A34
		internal static void AddTenantToCurrentSiteCache(string tenantName)
		{
			if (RedirectionHelper.currentSiteTenants == null)
			{
				return;
			}
			lock (RedirectionHelper.syncObject)
			{
				if (RedirectionHelper.IsCurrentSiteTenantsCacheExpired())
				{
					RedirectionHelper.ExpireCurrentSiteTenantsCache();
				}
				if (RedirectionHelper.currentSiteTenants.Count < 10000)
				{
					RedirectionHelper.currentSiteTenants.Add(tenantName);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000038A0 File Offset: 0x00001AA0
		internal static Uri RemovePropertiesFromOriginalUri(Uri originalUrl, string[] redirectionUriFilterProperties)
		{
			UriBuilder uriBuilder = new UriBuilder(originalUrl);
			NameValueCollection urlProperties = RedirectionHelper.GetUrlProperties(originalUrl);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in urlProperties.AllKeys)
			{
				bool flag = false;
				foreach (string text2 in redirectionUriFilterProperties)
				{
					if (text2.Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.Append(text);
					stringBuilder.Append("=");
					stringBuilder.Append(urlProperties[text]);
				}
			}
			uriBuilder.Query = stringBuilder.ToString();
			return uriBuilder.Uri;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003964 File Offset: 0x00001B64
		internal static ExchangeConfigurationUnit ResolveConfigurationUnitByName(string domainName)
		{
			ITenantConfigurationSession session = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(domainName), 454, "ResolveConfigurationUnitByName", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\RedirectionModule\\RedirectionHelper.cs");
			return RedirectionHelper.ResolveConfigurationUnitByName(domainName, session);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003999 File Offset: 0x00001B99
		private static ExchangeConfigurationUnit ResolveConfigurationUnitByName(string domainName, ITenantConfigurationSession session)
		{
			return session.GetExchangeConfigurationUnitByName(domainName);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000039A4 File Offset: 0x00001BA4
		private static Uri GetRedirectUrlForTenantSite(ADObjectId siteId, string redirectTemplate, Uri originalUrl)
		{
			ADObjectId adobjectId = siteId;
			if (adobjectId == null)
			{
				adobjectId = TenantOrganizationPresentationObject.DefaultManagementSiteId;
			}
			return RedirectionHelper.ConvertSiteNameToUri(adobjectId.Rdn.UnescapedName, originalUrl, redirectTemplate);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000039D0 File Offset: 0x00001BD0
		private static void ExpireCurrentSiteTenantsCache()
		{
			RedirectionHelper.currentSiteTenants = new HashSet<string>();
			RedirectionHelper.currentSiteTenantsCacheExpiration = DateTime.UtcNow.AddHours((double)RedirectionConfig.CurrentSiteTenantsCacheExpirationInHours);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000039FF File Offset: 0x00001BFF
		private static bool IsCurrentSiteTenantsCacheExpired()
		{
			return DateTime.UtcNow > RedirectionHelper.currentSiteTenantsCacheExpiration;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003A10 File Offset: 0x00001C10
		private static Uri ConvertSiteNameToUri(string siteName, Uri originalUrl, string redirectTemplate)
		{
			return new UriBuilder(originalUrl)
			{
				Host = string.Format(redirectTemplate, siteName)
			}.Uri;
		}

		// Token: 0x0400002A RID: 42
		public const string SecurityTokenUriPropertyName = "SecurityToken";

		// Token: 0x0400002B RID: 43
		private static readonly string[] tenantRedirectionPropertyNames = new string[]
		{
			"Organization",
			"OrganizationContext"
		};

		// Token: 0x0400002C RID: 44
		private static readonly object syncObject = new object();

		// Token: 0x0400002D RID: 45
		private static HashSet<string> currentSiteTenants = null;

		// Token: 0x0400002E RID: 46
		private static DateTime currentSiteTenantsCacheExpiration;
	}
}
