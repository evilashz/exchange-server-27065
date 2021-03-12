using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.CommonHelpProvider
{
	// Token: 0x02000006 RID: 6
	public static class Utilities
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000326C File Offset: 0x0000146C
		internal static bool IsMicrosoftHostedOnly()
		{
			bool result = false;
			try
			{
				result = Datacenter.IsMicrosoftHostedOnly(true);
			}
			catch (CannotDetermineExchangeModeException ex)
			{
				ExTraceGlobals.CoBrandingTracer.TraceError<string>(0L, "HelpProvider::IsMicrosoftHostedOnly CannotDetermineExchangeModeException {0}.", ex.Message);
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000032B0 File Offset: 0x000014B0
		internal static string GetServicePlanName(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			if (rbacConfiguration == null || rbacConfiguration.OrganizationId == null)
			{
				return string.Empty;
			}
			OrganizationProperties organizationProperties;
			if (!OrganizationPropertyCache.TryGetOrganizationProperties(rbacConfiguration.OrganizationId, out organizationProperties))
			{
				return string.Empty;
			}
			return organizationProperties.ServicePlan;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000032F0 File Offset: 0x000014F0
		internal static Uri NormalizeUrl(Uri inputUri)
		{
			if (inputUri.ToString().Contains("{0}"))
			{
				return inputUri;
			}
			Regex regex = new Regex("\\(.*?\\)");
			Match match = regex.Match(inputUri.ToString());
			string value = match.Value;
			if (match.Success)
			{
				inputUri = new Uri(regex.Replace(inputUri.ToString(), string.Empty));
			}
			string leftPart = inputUri.GetLeftPart(UriPartial.Authority);
			string text = inputUri.PathAndQuery;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			string uriString = string.Concat(new string[]
			{
				leftPart,
				"/{0}",
				text,
				"{1}",
				value,
				".aspx"
			});
			return new Uri(uriString);
		}

		// Token: 0x04000049 RID: 73
		private const string ContentIDSuffix = ".aspx";
	}
}
