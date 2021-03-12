using System;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.ApplicationLogic.ServiceInfoParser;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Win32;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SharePointUrl
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00011464 File Offset: 0x0000F664
		public static Uri GetRootSiteUrl(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return null;
			}
			try
			{
				Uri uri = SharePointUrl.GetRootSiteUrlFromExchangeAD(organizationId);
				if (uri != null)
				{
					if (uri != SharePointUrl.NoSharePointSubscription)
					{
						SharePointUrl.Tracer.TraceDebug<OrganizationId, Uri>(0L, "Found SharePoint Root Site Url in Exchange AD for organization {0}: {1}", organizationId, uri);
						return uri;
					}
					SharePointUrl.Tracer.TraceDebug<OrganizationId>(0L, "Organization {0} has no SharePoint subscription", organizationId);
					return null;
				}
				else
				{
					uri = SharePointUrl.GetRootSiteUrlFromAAD(organizationId);
					if (uri == null)
					{
						uri = SharePointUrl.NoSharePointSubscription;
						SharePointUrl.Tracer.TraceDebug<OrganizationId>(0L, "Organization {0} has no SharePoint subscription", organizationId);
					}
					try
					{
						SharePointUrl.SetRootSiteUrlInExchangeAD(organizationId, uri);
					}
					catch (LocalizedException arg)
					{
						SharePointUrl.Tracer.TraceError<LocalizedException>(0L, "Failed to set SharePoint Root Site Url in Exchange AD: {0}", arg);
					}
					if (uri != SharePointUrl.NoSharePointSubscription)
					{
						return uri;
					}
				}
			}
			catch (LocalizedException arg2)
			{
				SharePointUrl.Tracer.TraceError<LocalizedException>(0L, "Exception while getting the SharePoint Root Site Url: {0}", arg2);
			}
			return null;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001155C File Offset: 0x0000F75C
		private static Uri GetRootSiteUrlFromAAD(OrganizationId organizationId)
		{
			AADClient aadclient = AADClientFactory.Create(organizationId, GraphProxyVersions.Version14);
			if (aadclient == null)
			{
				SharePointUrl.Tracer.TraceDebug<OrganizationId>(0L, "Failed to create AADClient for organization {0}", organizationId);
				return null;
			}
			Uri rootSiteUrlFromServiceInfo = SharePointUrl.GetRootSiteUrlFromServiceInfo(aadclient);
			if (rootSiteUrlFromServiceInfo != null)
			{
				SharePointUrl.Tracer.TraceDebug<OrganizationId, Uri>(0L, "Found SharePoint Root Site Url in ServiceInfo for organization {0}: {1}", organizationId, rootSiteUrlFromServiceInfo);
			}
			return rootSiteUrlFromServiceInfo;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000115AC File Offset: 0x0000F7AC
		private static Uri GetRootSiteUrlFromExchangeAD(OrganizationId organizationId)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 146, "GetRootSiteUrlFromExchangeAD", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\SharePointNotification\\SharePointUrl.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADOrganizationConfig adorganizationConfig = tenantOrTopologyConfigurationSession.Read<ADOrganizationConfig>(orgContainerId);
			if (adorganizationConfig == null)
			{
				SharePointUrl.Tracer.TraceError<OrganizationId>(0L, "ADOrganizationConfig not found for {0}", organizationId);
				return null;
			}
			return adorganizationConfig.SharePointUrl;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011608 File Offset: 0x0000F808
		private static void SetRootSiteUrlInExchangeAD(OrganizationId organizationId, Uri sharePointUrl)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 168, "SetRootSiteUrlInExchangeAD", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\SharePointNotification\\SharePointUrl.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADOrganizationConfig adorganizationConfig = tenantOrTopologyConfigurationSession.Read<ADOrganizationConfig>(orgContainerId);
			if (adorganizationConfig == null)
			{
				SharePointUrl.Tracer.TraceError<OrganizationId>(0L, "ADOrganizationConfig not found for {0}", organizationId);
				return;
			}
			adorganizationConfig.SharePointUrl = sharePointUrl;
			tenantOrTopologyConfigurationSession.Save(adorganizationConfig);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001166C File Offset: 0x0000F86C
		private static Uri GetRootSiteUrlFromServiceInfo(AADClient aadClient)
		{
			string[] array = null;
			try
			{
				array = aadClient.GetServiceInfo("SharePoint/");
			}
			catch (AADException arg)
			{
				SharePointUrl.Tracer.TraceError<AADException>(0L, "GetServiceInfo failed: {0}", arg);
			}
			if (array == null)
			{
				return null;
			}
			foreach (string s in array)
			{
				using (StringReader stringReader = new StringReader(s))
				{
					try
					{
						XmlDocument xmlDocument = new SafeXmlDocument();
						xmlDocument.Load(stringReader);
						Uri rootSiteUrlFromServiceInfo = ServiceInfoParser.GetRootSiteUrlFromServiceInfo(xmlDocument, SharePointUrl.Tracer);
						if (rootSiteUrlFromServiceInfo != null && !string.IsNullOrWhiteSpace(rootSiteUrlFromServiceInfo.ToString()))
						{
							return rootSiteUrlFromServiceInfo;
						}
					}
					catch (XmlException arg2)
					{
						SharePointUrl.Tracer.TraceError<XmlException>(0L, "Failed to get SPO_RootSiteUrl from ServiceInfo: {0}", arg2);
					}
				}
			}
			return null;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011754 File Offset: 0x0000F954
		private static string GetSiteDomainSubstring()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ExchangeServer\\v15\\FederatedDirectory"))
			{
				if (registryKey == null)
				{
					SharePointUrl.Tracer.TraceDebug(0L, "FederatedDiretory registry key not found");
					result = null;
				}
				else
				{
					string text = registryKey.GetValue("SharePointSiteDomainSubstring") as string;
					if (string.IsNullOrEmpty(text))
					{
						SharePointUrl.Tracer.TraceDebug(0L, "SharePointSiteDomainSubstring not found");
						result = null;
					}
					else
					{
						SharePointUrl.Tracer.TraceDebug<string>(0L, "SharePointSiteDomainSubstring: {0}", text);
						result = text;
					}
				}
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000117E8 File Offset: 0x0000F9E8
		private static Uri GetRootSiteUrlFromDefaultDomain(AADClient aadClient)
		{
			string siteDomainSubstring = SharePointUrl.GetSiteDomainSubstring();
			if (string.IsNullOrEmpty(siteDomainSubstring))
			{
				SharePointUrl.Tracer.TraceError(0L, "Failed to get site domain substring");
				return null;
			}
			string text = null;
			try
			{
				text = aadClient.GetDefaultDomain();
				if (string.IsNullOrEmpty(text))
				{
					SharePointUrl.Tracer.TraceError(0L, "Failed to get default domain");
					return null;
				}
			}
			catch (AADException arg)
			{
				SharePointUrl.Tracer.TraceError<AADException>(0L, "GetDefaultDomain failed: {0}", arg);
				return null;
			}
			int num = text.IndexOf('.');
			if (num > 0)
			{
				string str = text.Substring(0, num);
				string str2 = str + (siteDomainSubstring.StartsWith(".", StringComparison.Ordinal) ? siteDomainSubstring : ("." + siteDomainSubstring));
				try
				{
					return new Uri("https://" + str2);
				}
				catch (UriFormatException arg2)
				{
					SharePointUrl.Tracer.TraceError<string, UriFormatException>(0L, "Invalid Uri: {0}, {1}", "https://" + str2, arg2);
				}
			}
			return null;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000118F0 File Offset: 0x0000FAF0
		private static Guid GetTenantContextId(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return Guid.Empty;
			}
			return new Guid(organizationId.ToExternalDirectoryOrganizationId());
		}

		// Token: 0x0400019C RID: 412
		private const string FederatedDirectoryRegistryPath = "Software\\Microsoft\\ExchangeServer\\v15\\FederatedDirectory";

		// Token: 0x0400019D RID: 413
		private const string SharePointSiteDomainSubstring = "SharePointSiteDomainSubstring";

		// Token: 0x0400019E RID: 414
		public static readonly Uri NoSharePointSubscription = new Uri("http://NoSharePointSubscription");

		// Token: 0x0400019F RID: 415
		internal static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;
	}
}
