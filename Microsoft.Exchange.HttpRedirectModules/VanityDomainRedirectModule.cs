﻿using System;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpRedirect
{
	// Token: 0x02000006 RID: 6
	public class VanityDomainRedirectModule : IHttpModule
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002425 File Offset: 0x00000625
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002439 File Offset: 0x00000639
		public void Dispose()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002458 File Offset: 0x00000658
		internal static bool IsVanityDomain(Uri requestUri, string canonicalHostname)
		{
			IPAddress ipaddress;
			return !requestUri.Host.Equals(canonicalHostname, StringComparison.OrdinalIgnoreCase) && !(from s in VanityDomainRedirectModule.NonVanityDomainSuffixes
			where requestUri.Host.EndsWith(s)
			select s).Any<string>() && !IPAddress.TryParse(requestUri.Host, out ipaddress) && !requestUri.IsLoopback && !requestUri.Host.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E0 File Offset: 0x000006E0
		internal static bool TryGetRedirectUri(Uri uri, string canonicalHostname, out Uri redirectUri, out bool isVanityDomainRedirect)
		{
			redirectUri = null;
			isVanityDomainRedirect = false;
			if (!HttpRedirectCommon.UriIsHttps(uri))
			{
				UriBuilder uriBuilder = new UriBuilder(uri);
				uriBuilder.Scheme = "https";
				uriBuilder.Port = 443;
				if (string.IsNullOrEmpty(uriBuilder.Path) || uriBuilder.Path == "/")
				{
					uriBuilder.Path = "/owa/";
				}
				if (VanityDomainRedirectModule.IsMissingTrailingSlashRequired(uriBuilder.Path))
				{
					UriBuilder uriBuilder2 = uriBuilder;
					uriBuilder2.Path += "/";
				}
				if (VanityDomainRedirectModule.IsVanityDomain(uri, canonicalHostname))
				{
					isVanityDomainRedirect = true;
					uriBuilder.Host = canonicalHostname;
					string text = null;
					string text2 = null;
					string host = uri.Host;
					int num = host.IndexOf('.');
					if (num >= 0)
					{
						text2 = host.Substring(0, num);
						text = host.Substring(num + 1);
					}
					string text3 = null;
					if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(text2))
					{
						text3 = "realm=" + text + "&vd=" + text2;
					}
					if (!string.IsNullOrEmpty(uriBuilder.Query))
					{
						uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + text3;
					}
					else
					{
						uriBuilder.Query = text3;
					}
				}
				redirectUri = uriBuilder.Uri;
				return true;
			}
			if (VanityDomainRedirectModule.IsMissingTrailingSlashRequired(uri.LocalPath))
			{
				UriBuilder uriBuilder3 = new UriBuilder(uri);
				UriBuilder uriBuilder4 = uriBuilder3;
				uriBuilder4.Path += "/";
				redirectUri = uriBuilder3.Uri;
				return true;
			}
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002648 File Offset: 0x00000848
		private static bool IsMissingTrailingSlashRequired(string requestPath)
		{
			bool result = false;
			int startIndex;
			if ((!string.IsNullOrWhiteSpace(HttpRedirectCommon.VirtualDirectoryNameLeadingSlash) && !"/".Equals(HttpRedirectCommon.VirtualDirectoryNameLeadingSlash, StringComparison.OrdinalIgnoreCase) && requestPath.EndsWith(HttpRedirectCommon.VirtualDirectoryNameLeadingSlash, StringComparison.OrdinalIgnoreCase)) || ((startIndex = requestPath.IndexOf('@')) != -1 && requestPath.IndexOf("/", startIndex) == -1))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026C4 File Offset: 0x000008C4
		private void OnBeginRequest(object sender, EventArgs e)
		{
			if (VanityDomainRedirectModule.CanonicalHostNameSetting == null || string.IsNullOrWhiteSpace(VanityDomainRedirectModule.CanonicalHostNameSetting.Value))
			{
				throw new InvalidOperationException("CanonicalHostName not set in web.config.");
			}
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnBeginRequestInternal((HttpApplication)sender);
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000271C File Offset: 0x0000091C
		private void OnBeginRequestInternal(HttpApplication httpApplication)
		{
			HttpContext context = httpApplication.Context;
			Uri url = context.Request.Url;
			Uri redirectUri = null;
			bool flag = false;
			if (VanityDomainRedirectModule.TryGetRedirectUri(url, VanityDomainRedirectModule.CanonicalHostNameSetting.Value, out redirectUri, out flag))
			{
				HttpRedirectCommon.RedirectRequestToNewUri(httpApplication, HttpRedirectCommon.HttpRedirectType.Permanent, redirectUri, flag ? "VanityDomainRedirectUri=" : "HttpsRedirectUri=");
			}
		}

		// Token: 0x04000014 RID: 20
		private const string IISLogFieldPrefix = "VanityDomainRedirectUri=";

		// Token: 0x04000015 RID: 21
		private const string OwaEcpCanonicalHostNameKey = "OwaEcpCanonicalHostName";

		// Token: 0x04000016 RID: 22
		private const string VanityDomainQueryStringParameterName = "realm=";

		// Token: 0x04000017 RID: 23
		private static readonly string[] NonVanityDomainSuffixes = new string[]
		{
			".outlook.com",
			".office365.com"
		};

		// Token: 0x04000018 RID: 24
		private static readonly StringAppSettingsEntry CanonicalHostNameSetting = new StringAppSettingsEntry("OwaEcpCanonicalHostName", string.Empty, ExTraceGlobals.VerboseTracer);
	}
}
