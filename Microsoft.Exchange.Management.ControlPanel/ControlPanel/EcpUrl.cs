using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000557 RID: 1367
	public static class EcpUrl
	{
		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x000C10D9 File Offset: 0x000BF2D9
		public static string EcpVDir
		{
			get
			{
				return EcpUrl.ProcessUrl(EcpUrl.EcpVDirForStaticResource, true, EcpUrl.EcpVDirForStaticResource, false, false);
			}
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000C10F0 File Offset: 0x000BF2F0
		public static string GetEcpVDirForCanary()
		{
			string text = EcpUrl.ProcessUrl(EcpUrl.EcpVDirForStaticResource, true, EcpUrl.EcpVDirForStaticResource, false, true);
			if (text.EndsWith("/"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		// Token: 0x170024DB RID: 9435
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000C112E File Offset: 0x000BF32E
		public static string OwaVDir
		{
			get
			{
				return EcpUrl.ProcessUrl("/owa/", true, "/owa/", false, false);
			}
		}

		// Token: 0x170024DC RID: 9436
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x000C1142 File Offset: 0x000BF342
		public static string OwaCreateUnifiedGroup
		{
			get
			{
				return EcpUrl.ProcessUrl("/owa/?path=/mail/action/createmoderngroup", true, "/owa/", false, false);
			}
		}

		// Token: 0x170024DD RID: 9437
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000C1156 File Offset: 0x000BF356
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x000C115D File Offset: 0x000BF35D
		internal static bool IsEcpStandalone { get; private set; } = DatacenterRegistry.IsForefrontForOffice() && EcpUrl.GetLocalServer().IsFfoWebServiceRole;

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000C1168 File Offset: 0x000BF368
		public static string GetOwaNavigateBackUrl()
		{
			string text = EcpUrl.OwaVDir;
			string owaNavigationParameter = HttpContext.Current.GetOwaNavigationParameter();
			if (!string.IsNullOrWhiteSpace(owaNavigationParameter))
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(owaNavigationParameter);
				for (int i = 0; i < nameValueCollection.Count; i++)
				{
					text = EcpUrl.AppendFragmentParameter(text, nameValueCollection.Keys[i] ?? string.Empty, nameValueCollection[i]);
				}
			}
			return text;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000C11CA File Offset: 0x000BF3CA
		internal static string AppendLanguageOverriddenParameter(string url)
		{
			return EcpUrl.AppendQueryParameter(url, "mkt", CultureInfo.CurrentUICulture.Name);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000C11E1 File Offset: 0x000BF3E1
		internal static string AppendFragmentParameter(string url, string name, string value)
		{
			return EcpUrl.AppendParameter(url, '#', name, value);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000C11ED File Offset: 0x000BF3ED
		internal static string AppendQueryParameter(string url, string name, string value)
		{
			return EcpUrl.AppendParameter(url, '?', name, value);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000C11FC File Offset: 0x000BF3FC
		internal static string AppendParameter(string url, char identifier, string name, string value)
		{
			StringBuilder stringBuilder = new StringBuilder(url, url.Length + name.Length + value.Length + 2);
			if (url.IndexOf(identifier) >= 0)
			{
				stringBuilder.Append("&");
			}
			else
			{
				stringBuilder.Append(identifier.ToString());
			}
			stringBuilder.Append(HttpUtility.UrlEncode(name));
			if (!string.IsNullOrEmpty(name))
			{
				stringBuilder.Append('=');
			}
			stringBuilder.Append(HttpUtility.UrlEncode(value));
			return stringBuilder.ToString();
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000C1280 File Offset: 0x000BF480
		internal static string RemoveQueryParameter(string url, string name, bool throwIfNotFound)
		{
			bool flag = false;
			int num = url.IndexOf('?');
			if (num > 0)
			{
				while (!flag && num < url.Length)
				{
					int num2 = url.IndexOf(name, num, StringComparison.InvariantCultureIgnoreCase);
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
			if (throwIfNotFound && !flag)
			{
				throw new InvalidOperationException(string.Format("The url '{0}' doesn't contain parameter '{1}'.", url, name));
			}
			return url;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000C137C File Offset: 0x000BF57C
		internal static string GetLeftPart(string url, UriPartial part)
		{
			Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
			if (uri.IsAbsoluteUri)
			{
				return uri.GetLeftPart(part);
			}
			switch (part)
			{
			case UriPartial.Scheme:
			case UriPartial.Authority:
				throw new InvalidOperationException();
			case UriPartial.Path:
			{
				int num = url.IndexOf('?');
				if (num < 0)
				{
					return url;
				}
				return url.Substring(0, num);
			}
			default:
				return url;
			}
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000C13D6 File Offset: 0x000BF5D6
		public static string ProcessUrl(string url)
		{
			return EcpUrl.ProcessUrl(url, true);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000C13DF File Offset: 0x000BF5DF
		public static string ProcessUrl(string url, bool isFullPath)
		{
			return EcpUrl.ProcessUrl(url, isFullPath, EcpUrl.EcpVDirForStaticResource, false, false);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000C13F0 File Offset: 0x000BF5F0
		internal static string ProcessUrl(string url, bool isFullPath, string vdir, bool skipTargetTenant, bool skipEso = false)
		{
			HttpContext context = HttpContext.Current;
			StringBuilder stringBuilder = null;
			bool flag = false;
			if (!skipTargetTenant)
			{
				flag |= (EcpUrl.AppendDelegatedTenantIfAny(context, url, ref stringBuilder, isFullPath, vdir) || EcpUrl.AppendOrganizationContextIfAny(context, url, ref stringBuilder, isFullPath, vdir));
			}
			if (!skipEso)
			{
				flag |= EcpUrl.AppendEsoUserIfAny(context, url, ref stringBuilder, isFullPath, vdir);
			}
			if (flag)
			{
				int length = EcpUrl.EcpVDirForStaticResource.Length;
				stringBuilder.Append(url, length, url.Length - length);
				url = stringBuilder.ToString();
			}
			return url;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000C1464 File Offset: 0x000BF664
		private static bool AppendDelegatedTenantIfAny(HttpContext context, string url, ref StringBuilder sb, bool isFullPath, string vdir)
		{
			bool result = false;
			string targetTenant = context.GetTargetTenant();
			if (!string.IsNullOrEmpty(targetTenant) && !context.HasOrganizationContext())
			{
				EcpUrl.EnsureStringBuilderInitialized(ref sb, isFullPath, vdir);
				sb.Append('@');
				sb.Append(targetTenant);
				sb.Append('/');
				result = true;
			}
			return result;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000C14B4 File Offset: 0x000BF6B4
		private static bool AppendOrganizationContextIfAny(HttpContext context, string url, ref StringBuilder sb, bool isFullPath, string vdir)
		{
			bool result = false;
			string targetTenant = context.GetTargetTenant();
			if (!string.IsNullOrEmpty(targetTenant) && context.HasOrganizationContext())
			{
				EcpUrl.EnsureStringBuilderInitialized(ref sb, isFullPath, vdir);
				sb.Append('@');
				sb.Append('.');
				sb.Append(targetTenant);
				sb.Append('/');
				result = true;
			}
			return result;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000C1510 File Offset: 0x000BF710
		private static bool AppendEsoUserIfAny(HttpContext context, string url, ref StringBuilder sb, bool isFullPath, string vdir)
		{
			bool result = false;
			string explicitUser = context.GetExplicitUser();
			if (!string.IsNullOrEmpty(explicitUser))
			{
				EcpUrl.EnsureStringBuilderInitialized(ref sb, isFullPath, vdir);
				sb.Append(explicitUser);
				sb.Append('/');
				result = true;
			}
			return result;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000C154D File Offset: 0x000BF74D
		private static void EnsureStringBuilderInitialized(ref StringBuilder sb, bool isFullPath, string vdir)
		{
			if (sb == null)
			{
				sb = new StringBuilder();
				if (isFullPath)
				{
					sb.Append(vdir);
				}
			}
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000C1566 File Offset: 0x000BF766
		[Conditional("DEBUG")]
		private static void ThrowIfAlreadyContainsPrefix(string url, object prefix, params object[] morePrefixes)
		{
			if (url.Contains("/" + prefix + morePrefixes.StringArrayJoin(string.Empty)))
			{
				throw new InvalidOperationException("The url already contains the path prefix. Probably this method has been called twice. Please fix it.");
			}
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000C1591 File Offset: 0x000BF791
		[Conditional("DEBUG")]
		private static void ThrowIfFullURLNotStartWithECPVdir(string url, bool isFullPath, string vdir)
		{
			if (isFullPath && !url.StartsWith(vdir, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("The url isn't start with " + vdir + " as expected: " + url, "url");
			}
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000C15BC File Offset: 0x000BF7BC
		public static string ToEscapedString(this Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				return uri.ToString();
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000C15E7 File Offset: 0x000BF7E7
		public static string ResolveClientUrl(string url)
		{
			return EcpUrl.ResolveClientUrl(new Uri(url, UriKind.RelativeOrAbsolute)).ToString();
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000C15FC File Offset: 0x000BF7FC
		public static Uri ResolveClientUrl(Uri url)
		{
			UriBuilder uriBuilder = null;
			if (url != null && url.IsAbsoluteUri)
			{
				uriBuilder = new UriBuilder(url);
				if (uriBuilder.Scheme == "http" && EcpUrl.SslOffloaded)
				{
					uriBuilder.Scheme = "https";
				}
			}
			if (HttpContext.Current.IsExplicitSignOn() || HttpContext.Current.HasTargetTenant())
			{
				uriBuilder = (uriBuilder ?? new UriBuilder(url));
				uriBuilder.Path = EcpUrl.ProcessUrl(uriBuilder.Path, true);
			}
			if (uriBuilder != null)
			{
				url = uriBuilder.Uri;
			}
			return url;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000C168C File Offset: 0x000BF88C
		private static bool IsSslOffloaded()
		{
			bool result = false;
			if (Util.IsDataCenter)
			{
				bool.TryParse(ConfigurationManager.AppSettings["LiveIdAuthModuleSslOffloadedKey"], out result);
			}
			else
			{
				result = Registry.SslOffloaded;
			}
			return result;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000C16C4 File Offset: 0x000BF8C4
		private static Server GetLocalServer()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 634, "GetLocalServer", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Utilities\\EcpUrl.cs");
			topologyConfigurationSession.UseConfigNC = true;
			topologyConfigurationSession.UseGlobalCatalog = true;
			Server server = topologyConfigurationSession.FindLocalServer();
			if (server == null)
			{
				ExTraceGlobals.ProxyTracer.TraceInformation(0, 0L, "Could not find local server in directory.");
				throw new CmdletAccessDeniedException(Strings.FailedToGetLocalServerInfo);
			}
			return server;
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000C1728 File Offset: 0x000BF928
		public static bool HasScheme(string virtualPath)
		{
			int num = virtualPath.IndexOf(':');
			if (num == -1)
			{
				return false;
			}
			int num2 = virtualPath.IndexOf('/');
			return num2 == -1 || num < num2;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000C1757 File Offset: 0x000BF957
		public static bool IsRooted(string basepath)
		{
			return string.IsNullOrEmpty(basepath) || basepath[0] == '/' || basepath[0] == '\\';
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000C1779 File Offset: 0x000BF979
		public static bool IsRelativeUrl(string virtualPath)
		{
			return !EcpUrl.HasScheme(virtualPath) && !EcpUrl.IsRooted(virtualPath);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000C1790 File Offset: 0x000BF990
		public static bool IsAppRelativePath(string path)
		{
			if (path == null)
			{
				return false;
			}
			int length = path.Length;
			return length != 0 && path[0] == '~' && (length == 1 || path[1] == '\\' || path[1] == '/');
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000C17D8 File Offset: 0x000BF9D8
		public static string GetRelativePathToAppRoot(string url)
		{
			int num;
			int num2;
			if (!EcpUrl.GetRelativePathToAppRootRange(url, out num, out num2))
			{
				return null;
			}
			return url.Substring(num, num2 - num);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000C1800 File Offset: 0x000BFA00
		public static string ReplaceRelativePath(string url, string newRelativePath, bool throwIfFail)
		{
			int length;
			int num;
			if (EcpUrl.GetRelativePathToAppRootRange(url, out length, out num))
			{
				return url.Substring(0, length) + newRelativePath + url.Substring(num, url.Length - num);
			}
			if (throwIfFail)
			{
				throw new ArgumentException("Can not determine relative path", "url");
			}
			return null;
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000C184C File Offset: 0x000BFA4C
		private static bool GetRelativePathToAppRootRange(string url, out int start, out int end)
		{
			start = -1;
			end = -1;
			if (EcpUrl.HasScheme(url))
			{
				return false;
			}
			if (EcpUrl.IsAppRelativePath(url))
			{
				start = 2;
			}
			else if (EcpUrl.IsRelativeUrl(url))
			{
				start = 0;
			}
			else if (EcpUrl.IsRooted(url) && url.StartsWith(EcpUrl.EcpVDirForStaticResource))
			{
				start = EcpUrl.EcpVDirForStaticResource.Length;
			}
			if (start == -1)
			{
				return false;
			}
			end = url.IndexOf('?');
			if (end == -1)
			{
				end = url.Length;
			}
			return start < end;
		}

		// Token: 0x04002A73 RID: 10867
		public const string CopyrightLink = "http://go.microsoft.com/fwlink/p/?LinkId=256676";

		// Token: 0x04002A74 RID: 10868
		public const string OwaVDirForStaticResource = "/owa/";

		// Token: 0x04002A75 RID: 10869
		internal const string LogoffAndReturnPath = "logoff.aspx?src=exch&ru=";

		// Token: 0x04002A76 RID: 10870
		public const string DDIServicePath = "DDI/DDIService.svc?schema=";

		// Token: 0x04002A77 RID: 10871
		public static readonly string EcpVDirForStaticResource = HttpRuntime.AppDomainAppVirtualPath + '/';

		// Token: 0x04002A78 RID: 10872
		public static readonly bool SslOffloaded = EcpUrl.IsSslOffloaded();

		// Token: 0x02000558 RID: 1368
		public class QueryParameter
		{
			// Token: 0x04002A7A RID: 10874
			public const string ID = "id";

			// Token: 0x04002A7B RID: 10875
			public const string EWS_ID = "ewsid";

			// Token: 0x04002A7C RID: 10876
			public const string TplNames = "tplNames";

			// Token: 0x04002A7D RID: 10877
			public const string Recipient = "recip";

			// Token: 0x04002A7E RID: 10878
			public const string MessageId = "MsgId";

			// Token: 0x04002A7F RID: 10879
			public const string Mailbox = "Mbx";

			// Token: 0x04002A80 RID: 10880
			public const string Cause = "cause";

			// Token: 0x04002A81 RID: 10881
			public const string IsOwa = "isowa";

			// Token: 0x04002A82 RID: 10882
			public const string SubscriptionType = "st";

			// Token: 0x04002A83 RID: 10883
			public const string SubscriptionGuid = "su";

			// Token: 0x04002A84 RID: 10884
			public const string SharedSecretParam = "ss";

			// Token: 0x04002A85 RID: 10885
			public const string Command = "cmd";

			// Token: 0x04002A86 RID: 10886
			public const string Feature = "ftr";

			// Token: 0x04002A87 RID: 10887
			public const string RedirectionURL = "backUr";

			// Token: 0x04002A88 RID: 10888
			public const string SelectionMode = "mode";

			// Token: 0x04002A89 RID: 10889
			public const string MessageTypes = "types";

			// Token: 0x04002A8A RID: 10890
			public const string Cross = "cross";

			// Token: 0x04002A8B RID: 10891
			public const string XPremiseServer = "xprs";

			// Token: 0x04002A8C RID: 10892
			public const string XPremiseVersion = "xprv";

			// Token: 0x04002A8D RID: 10893
			public const string XPremiseFeatures = "xprf";

			// Token: 0x04002A8E RID: 10894
			public const string ShowHelp = "showhelp";

			// Token: 0x04002A8F RID: 10895
			public const string LanguageOverriddenParam = "mkt";

			// Token: 0x04002A90 RID: 10896
			public const string LanguageOverriddenCandidateParam = "mkt2";

			// Token: 0x04002A91 RID: 10897
			public const string AllowTypingParam = "allowtyping";

			// Token: 0x04002A92 RID: 10898
			public const string ViewParam = "vw";

			// Token: 0x04002A93 RID: 10899
			public const string ReferrerParam = "rfr";

			// Token: 0x04002A94 RID: 10900
			public const string TopNavParam = "topnav";

			// Token: 0x04002A95 RID: 10901
			public const string StartPageParam = "p";

			// Token: 0x04002A96 RID: 10902
			public const string OnPremiseStartPageParam = "op";

			// Token: 0x04002A97 RID: 10903
			public const string CloudStartPageParam = "cp";

			// Token: 0x04002A98 RID: 10904
			public const string OnPremiseVisibleParam = "ov";

			// Token: 0x04002A99 RID: 10905
			public const string QueryStringParam = "q";

			// Token: 0x04002A9A RID: 10906
			public const string BreadCrumbParam = "bc";

			// Token: 0x04002A9B RID: 10907
			public const string FolderIdParam = "fldID";

			// Token: 0x04002A9C RID: 10908
			public const string DialPlanID = "dialPlanId";

			// Token: 0x04002A9D RID: 10909
			public const string New = "new";

			// Token: 0x04002A9E RID: 10910
			public const string DeviceType = "dt";

			// Token: 0x04002A9F RID: 10911
			public const string HelpId = "helpid";

			// Token: 0x04002AA0 RID: 10912
			public const string IsNarrowPage = "isNarrow";

			// Token: 0x04002AA1 RID: 10913
			public const string AssetID = "AssetID";

			// Token: 0x04002AA2 RID: 10914
			public const string Etoken = "ClientToken=";

			// Token: 0x04002AA3 RID: 10915
			public const string QueryMarket = "LC";

			// Token: 0x04002AA4 RID: 10916
			public const string Scope = "Scope";

			// Token: 0x04002AA5 RID: 10917
			public const string DeploymentId = "DeployId";

			// Token: 0x04002AA6 RID: 10918
			public const string CallingApplication = "app";

			// Token: 0x04002AA7 RID: 10919
			public const string DataTransferMode = "dtm";

			// Token: 0x04002AA8 RID: 10920
			public const string Provider = "Provider";

			// Token: 0x04002AA9 RID: 10921
			public const string SchemaName = "sn";

			// Token: 0x04002AAA RID: 10922
			public const string RequestId = "reqId";

			// Token: 0x04002AAB RID: 10923
			public const string RetentionTagTypeGroup = "typeGroup";

			// Token: 0x04002AAC RID: 10924
			public const string RequestVersion = "ExchClientVer";

			// Token: 0x04002AAD RID: 10925
			public const string RequestTargetServer = "TargetServer";

			// Token: 0x04002AAE RID: 10926
			public const string SimplifiedChangePhotoExperience = "chgPhoto";
		}
	}
}
