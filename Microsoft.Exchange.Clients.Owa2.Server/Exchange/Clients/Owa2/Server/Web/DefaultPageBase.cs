using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200046C RID: 1132
	public abstract class DefaultPageBase : Page, IPageContext
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x00088927 File Offset: 0x00086B27
		public virtual ResourceBase[] UserDataEmbeddedLinks
		{
			get
			{
				if (this.userDataEmbededLinks == null)
				{
					this.userDataEmbededLinks = UserResourcesFinder.GetUserDataEmbeddedLinks(this.GetBootSlab(), this.VersionString);
				}
				return this.userDataEmbededLinks;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x0008894E File Offset: 0x00086B4E
		public static bool IsRtl
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x00088964 File Offset: 0x00086B64
		public new static string UICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.Name;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x00088978 File Offset: 0x00086B78
		public string UserLanguage
		{
			get
			{
				foreach (ResourceBase resourceBase in this.UserDataEmbeddedLinks)
				{
					LocalizedStringsScriptResource localizedStringsScriptResource = resourceBase as LocalizedStringsScriptResource;
					if (localizedStringsScriptResource != null)
					{
						return localizedStringsScriptResource.GetLocalizedCultureName();
					}
				}
				return null;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000889B9 File Offset: 0x00086BB9
		public static string PayloadClassName
		{
			get
			{
				return "PageDataPayload";
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000889C0 File Offset: 0x00086BC0
		public static string ThemeStyleCultureDirectory
		{
			get
			{
				return LocalizedThemeStyleResource.GetCultureDirectory(Thread.CurrentThread.CurrentUICulture);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000889D1 File Offset: 0x00086BD1
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x000889D9 File Offset: 0x00086BD9
		public virtual UserAgent UserAgent { get; private set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060025C6 RID: 9670
		public abstract string SlabsManifest { get; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060025C7 RID: 9671
		public abstract string TenantId { get; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060025C8 RID: 9672
		public abstract string MdbGuid { get; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000889E2 File Offset: 0x00086BE2
		public bool IsMowaAlt2Session
		{
			get
			{
				return OfflineClientRequestUtilities.IsRequestForAppCachedVersion(this.Context);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000889F0 File Offset: 0x00086BF0
		public bool IsClientInOfflineMode
		{
			get
			{
				bool? flag = OfflineClientRequestUtilities.IsRequestForOfflineAppcacheManifest(base.Request);
				return OfflineClientRequestUtilities.IsRequestFromOfflineClient(base.Request) || (flag != null && flag.Value);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x00088A2C File Offset: 0x00086C2C
		public virtual string ServerSettings
		{
			get
			{
				string text;
				if (Globals.IsPreCheckinApp)
				{
					text = "/owa";
				}
				else
				{
					text = HttpRuntime.AppDomainAppVirtualPath;
				}
				return string.Concat(new string[]
				{
					"'version': '",
					this.VersionString,
					"','startTime': st,'bootedFromAppcache': appCachedPage,'cdnEndpoint': '",
					this.GetCdnEndpointForResources(false),
					"','mapControlUrl': '",
					Default.MapControlUrl,
					"','appDomainAppVirtualPath': '",
					text,
					"','layout': layout,'uiCulture': userCultureVar,'uiLang': userLanguageVar,'userCultureRtl': userCultureRtl,'uiTheme': clientTheme,'osfStringPath': '",
					this.GetLocalizedOsfStringResourcePath(false),
					"','scriptsFolder': '",
					this.GetScriptsFolder(false),
					"','resourcesFolder': '",
					this.GetResourcesFolder(false),
					"','featuresSupported': ",
					this.GetFeaturesSupportedJsonArray(FlightedFeatureScope.Client | FlightedFeatureScope.ClientServer),
					",'themedImagesFolderFormat': '",
					this.GetThemedImagesFolderFormat(false),
					"','stylesFolderFormat': '",
					this.GetStylesFolderFormat(false),
					"','stylesLocale': stylesLocale"
				});
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x00088B1B File Offset: 0x00086D1B
		public virtual bool SessionDataEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x00088B1E File Offset: 0x00086D1E
		public new string Theme
		{
			get
			{
				if (string.IsNullOrEmpty(this.theme))
				{
					this.theme = this.GetThemeFolder();
				}
				return this.theme;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00088B3F File Offset: 0x00086D3F
		public virtual string IsChangeLayoutFeatureEnabled
		{
			get
			{
				return "true";
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x00088B46 File Offset: 0x00086D46
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x00088B4E File Offset: 0x00086D4E
		public virtual bool CompositeSessionData { get; protected set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00088B57 File Offset: 0x00086D57
		public virtual bool RecoveryBoot
		{
			get
			{
				return DefaultPageBase.IsRecoveryBoot(this.Context);
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00088B64 File Offset: 0x00086D64
		// (set) Token: 0x060025D3 RID: 9683 RVA: 0x00088B6C File Offset: 0x00086D6C
		public bool IsAppCacheEnabledClient { get; protected set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x00088B75 File Offset: 0x00086D75
		public string AppCacheClientQueryParamWithValue
		{
			get
			{
				return string.Format("{0}={1}", "appcacheclient", this.IsAppCacheEnabledClient ? "1" : "0");
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00088B9A File Offset: 0x00086D9A
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x00088BA2 File Offset: 0x00086DA2
		public bool IsOfflineAppCacheEnabledClient { get; protected set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x00088BAB File Offset: 0x00086DAB
		public string EncodingErrorMessage
		{
			get
			{
				return Strings.ErrorWrongEncoding;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x00088BB2 File Offset: 0x00086DB2
		public string LogOffOwaUrl
		{
			get
			{
				return OwaUrl.LogoffOwa.GetExplicitUrl(this.Context.Request);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00088BCC File Offset: 0x00086DCC
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x00088C13 File Offset: 0x00086E13
		public virtual SlabManifestType ManifestType
		{
			get
			{
				if (this.slabManifestType == null)
				{
					this.slabManifestType = (DefaultPageBase.IsPalEnabled(this.Context, this.UserAgent.RawString) ? SlabManifestType.Pal : SlabManifestType.Standard);
				}
				return this.slabManifestType;
			}
			protected set
			{
				this.slabManifestType = value;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00088C1C File Offset: 0x00086E1C
		public string UserSpecificResourcesHash
		{
			get
			{
				return UserResourcesFinder.GetResourcesHash(this.UserDataEmbeddedLinks, this, true, this.VersionString);
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00088C31 File Offset: 0x00086E31
		protected string BootScriptsFolder
		{
			get
			{
				return this.GetScriptsFolder(true);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x00088C3A File Offset: 0x00086E3A
		protected string BootResourcesFolder
		{
			get
			{
				return this.GetResourcesFolder(true);
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x00088C43 File Offset: 0x00086E43
		protected string BootStylesFolder
		{
			get
			{
				return this.GetStylesFolder(true);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x00088C4C File Offset: 0x00086E4C
		protected string BootImagesFolder
		{
			get
			{
				return this.GetImagesFolder(true);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00088C55 File Offset: 0x00086E55
		protected string BootThemedImagesFolderFormat
		{
			get
			{
				return this.GetThemedImagesFolderFormat(true);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x00088C5E File Offset: 0x00086E5E
		protected string BootThemedStylesFolder
		{
			get
			{
				return string.Format(this.BootThemedImagesFolderFormat, this.Theme);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00088C71 File Offset: 0x00086E71
		protected string BootThemedImagesFolder
		{
			get
			{
				return string.Format(this.FormatURIForCDN(ResourcePathBuilderUtilities.GetThemedLocaleImageResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString), DefaultPageBase.IsRtl), true), this.Theme);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x00088C9A File Offset: 0x00086E9A
		protected string BootStylesFolderFormat
		{
			get
			{
				return this.GetStylesFolderFormat(true);
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00088CA3 File Offset: 0x00086EA3
		protected string CdnVersionCheckUrl
		{
			get
			{
				return this.GetScriptsFolder(false) + "/cdnversioncheck.js";
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00088CB8 File Offset: 0x00086EB8
		protected string LogoImageFileName
		{
			get
			{
				switch (this.UserAgent.Layout)
				{
				case LayoutType.TouchNarrow:
					return "owa_logo.narrow.png";
				case LayoutType.TouchWide:
					return "owa_logo.wide.png";
				case LayoutType.Mouse:
					return "owa_logo.mouse.png";
				default:
					return null;
				}
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x00088CFC File Offset: 0x00086EFC
		protected string BootTraceUrl
		{
			get
			{
				string text = ConfigurationManager.AppSettings["BootTraceUrl"];
				return text ?? string.Empty;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x00088D23 File Offset: 0x00086F23
		protected bool ShouldLoadSegoeFonts
		{
			get
			{
				return DefaultPageBase.GetFontLocale() == "0" && !this.UserAgent.IsOsWindowsNtVersionOrLater(6, 1);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x00088D48 File Offset: 0x00086F48
		protected bool ShouldLoadSegoeSemilight
		{
			get
			{
				return DefaultPageBase.GetFontLocale() == "0" && !this.UserAgent.IsOsWindows8OrLater();
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x00088D6B File Offset: 0x00086F6B
		internal LocalizedExtensibilityStringsScriptResource LocalizedOsfStringResource
		{
			get
			{
				if (this.localizedOsfStringResource == null)
				{
					this.localizedOsfStringResource = new LocalizedExtensibilityStringsScriptResource("osfruntime_strings.js", ResourceTarget.Any, this.VersionString);
				}
				return this.localizedOsfStringResource;
			}
		}

		// Token: 0x060025EA RID: 9706
		public abstract string FormatURIForCDN(string relativeUri, bool isBootResourceUri);

		// Token: 0x060025EB RID: 9707
		public abstract string GetCdnEndpointForResources(bool bootResources);

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060025EC RID: 9708
		// (set) Token: 0x060025ED RID: 9709
		public abstract string VersionString { get; set; }

		// Token: 0x060025EE RID: 9710 RVA: 0x00088D98 File Offset: 0x00086F98
		public static bool IsRecoveryBoot(HttpContext context)
		{
			bool result = false;
			if (context != null)
			{
				result = context.Request.QueryString.AllKeys.Contains("bO");
			}
			return result;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00088DC6 File Offset: 0x00086FC6
		public static bool IsPalEnabled(HttpContext context)
		{
			return DefaultPageBase.IsPalEnabled(context, context.Request.UserAgent);
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00088DD9 File Offset: 0x00086FD9
		public static bool IsPalEnabled(HttpContext context, string userAgent)
		{
			return OfflineClientRequestUtilities.IsRequestFromMOWAClient(context.Request, userAgent);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00088DE7 File Offset: 0x00086FE7
		protected void WriteUserSpecificScripts()
		{
			UserSpecificResourceInjectorBase.WriteScriptBlock(new Action<string>(base.Response.Write), this, UserContextManager.GetUserContext(HttpContext.Current), this.UserDataEmbeddedLinks, this.VersionString);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00088E18 File Offset: 0x00087018
		protected override void OnPreInit(EventArgs e)
		{
			this.UserAgent = OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(this.Context);
			this.IsAppCacheEnabledClient = this.GetIsClientAppCacheEnabled(this.Context);
			this.IsOfflineAppCacheEnabledClient = (this.IsAppCacheEnabledClient && this.IsClientInOfflineMode);
			this.CompositeSessionData = this.CalculateCompositeSessionDataEnabled();
			base.Response.AddHeader("pragma", "no-cache");
			base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			base.Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
			if (UrlUtilities.IsAuthRedirectRequest(this.Context.Request) || AppCacheManifestHandlerBase.DoesBrowserSupportAppCache(this.UserAgent))
			{
				string url = null;
				if (this.ShouldRedirectWithoutUnnecessaryParams(out url))
				{
					base.Response.Redirect(url);
				}
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00088EE6 File Offset: 0x000870E6
		protected virtual string GetThemeFolder()
		{
			return "base";
		}

		// Token: 0x060025F4 RID: 9716
		protected abstract bool ShouldSkipThemeFolder();

		// Token: 0x060025F5 RID: 9717 RVA: 0x00088EF0 File Offset: 0x000870F0
		protected virtual bool ShouldRedirectWithoutUnnecessaryParams(out string destinationUrl)
		{
			destinationUrl = null;
			string uriString = this.Context.Request.Url.OriginalString;
			string text = this.Context.Request.Headers["msExchProxyUri"];
			if (!string.IsNullOrEmpty(text))
			{
				uriString = text;
			}
			Uri uri = new Uri(uriString);
			NameValueCollection coll = HttpUtility.ParseQueryString(uri.Query);
			string str = null;
			if (DefaultPageBase.QueryHasUnnecessaryParameters(coll, out str))
			{
				string text2 = uri.AbsolutePath;
				if (text2.ToLowerInvariant().EndsWith("default.aspx"))
				{
					text2 = text2.Substring(0, text2.Length - "default.aspx".Length);
				}
				text2 += str;
				destinationUrl = text2;
				return true;
			}
			return false;
		}

		// Token: 0x060025F6 RID: 9718
		protected abstract string GetFeaturesSupportedJsonArray(FlightedFeatureScope scope);

		// Token: 0x060025F7 RID: 9719 RVA: 0x00088FA4 File Offset: 0x000871A4
		private static bool QueryHasUnnecessaryParameters(NameValueCollection coll, out string query)
		{
			query = null;
			bool result = false;
			if (coll.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = null;
				string text2 = null;
				for (int i = 0; i < coll.Keys.Count; i++)
				{
					string text3 = coll.Keys[i];
					string value = coll.Keys[i].ToLowerInvariant();
					if (Array.IndexOf<string>(DefaultPageBase.ParametersToRemove, value) >= 0)
					{
						result = true;
					}
					else if ("path".Equals(value))
					{
						text2 = coll[text3];
						result = true;
					}
					else if ("modurl".Equals(value))
					{
						text = coll[text3];
						result = true;
					}
					else
					{
						if (stringBuilder.Length == 0)
						{
							stringBuilder.Append('?');
						}
						else
						{
							stringBuilder.Append('&');
						}
						stringBuilder.Append(Uri.EscapeDataString(text3));
						if (!string.IsNullOrEmpty(coll[text3]))
						{
							stringBuilder.Append('=');
							stringBuilder.Append(Uri.EscapeDataString(coll[text3]));
						}
					}
				}
				if (!string.IsNullOrWhiteSpace(text2))
				{
					stringBuilder.Append(string.Format("#{0}={1}", "path", Uri.EscapeDataString(text2)));
					result = true;
				}
				else if (!string.IsNullOrWhiteSpace(text))
				{
					stringBuilder.Append(string.Format("#{0}={1}", "modurl", Uri.EscapeDataString(text)));
					result = true;
				}
				if (stringBuilder.Length > 0)
				{
					query = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00089114 File Offset: 0x00087314
		protected virtual bool GetIsClientAppCacheEnabled(HttpContext context)
		{
			bool flag = false;
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(context.Request.Url.Query);
			foreach (string text in nameValueCollection.AllKeys)
			{
				string text2;
				if (text == null || !DefaultPageBase.ParamsInAppCache.TryGetValue(text.ToLowerInvariant(), out text2) || (text2 != null && text2 != context.Request.Params[text]))
				{
					flag = true;
					break;
				}
			}
			return (!flag && OfflineClientRequestUtilities.IsRequestForAppCachedVersion(context)) || DefaultPageBase.IsPalEnabled(context, this.UserAgent.RawString);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000891B0 File Offset: 0x000873B0
		protected string InlineJavascript(string fileName)
		{
			string text = Path.Combine(FolderConfiguration.Instance.RootPath, ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString)), fileName);
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(text);
			Tuple<string, DateTime> tuple;
			lock (DefaultPageBase.inlineScripts)
			{
				if (!DefaultPageBase.inlineScripts.TryGetValue(text, out tuple) || tuple.Item2 < lastWriteTimeUtc)
				{
					tuple = Tuple.Create<string, DateTime>(File.ReadAllText(text), lastWriteTimeUtc);
					DefaultPageBase.inlineScripts[text] = tuple;
				}
			}
			return tuple.Item1;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00089250 File Offset: 0x00087450
		protected string InlineImage(string fileName)
		{
			string text = Path.Combine(Path.Combine(FolderConfiguration.Instance.RootPath, ResourcePathBuilderUtilities.GetBootImageResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString), DefaultPageBase.IsRtl)), fileName);
			DateTime lastWriteTimeUtc;
			try
			{
				lastWriteTimeUtc = File.GetLastWriteTimeUtc(text);
			}
			catch
			{
				return this.BootImagesFolder + "/" + fileName;
			}
			Tuple<string, DateTime> tuple;
			lock (DefaultPageBase.inlineImages)
			{
				if (!DefaultPageBase.inlineImages.TryGetValue(text, out tuple) || tuple.Item2 < lastWriteTimeUtc)
				{
					tuple = Tuple.Create<string, DateTime>("data:image/" + Path.GetExtension(fileName).Substring(1) + ";base64," + Convert.ToBase64String(File.ReadAllBytes(text)), lastWriteTimeUtc);
					DefaultPageBase.inlineImages[text] = tuple;
				}
			}
			return tuple.Item1;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00089340 File Offset: 0x00087540
		protected string InlineFontCss()
		{
			string fontLocale = DefaultPageBase.GetFontLocale();
			return this.InlineStyles(string.Format("fabric.font.{0}.css", fontLocale));
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x00089364 File Offset: 0x00087564
		protected string InlineFabricCss()
		{
			return this.InlineStyles(string.Format("fabric.color.theme.{0}.css", this.Theme));
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0008937C File Offset: 0x0008757C
		protected string InlineStyles(string filename)
		{
			string text = Path.Combine(FolderConfiguration.Instance.RootPath, ResourcePathBuilderUtilities.GetStyleResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString)), filename);
			Tuple<string, DateTime> tuple;
			lock (DefaultPageBase.inlineStyles)
			{
				DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(text);
				if (!DefaultPageBase.inlineStyles.TryGetValue(text, out tuple) || tuple.Item2 < lastWriteTimeUtc)
				{
					tuple = Tuple.Create<string, DateTime>("<style>" + File.ReadAllText(text) + "</style>", lastWriteTimeUtc);
					DefaultPageBase.inlineStyles[text] = tuple;
				}
			}
			return tuple.Item1;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00089434 File Offset: 0x00087634
		protected virtual IEnumerable<string> GetBootScripts()
		{
			Slab slab = this.GetBootSlab();
			return from s in slab.PackagedSources
			select s.Name;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00089470 File Offset: 0x00087670
		protected Slab GetBootSlab()
		{
			if (this.bootSlab == null)
			{
				this.bootSlab = UserResourcesFinder.CreateUserBootSlab(this.ManifestType, this.UserAgent.Layout, this.VersionString);
			}
			return this.bootSlab;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000894A4 File Offset: 0x000876A4
		private static string GetFontLocale()
		{
			string cultureDirectory = LocalizedThemeStyleResource.GetCultureDirectory(Thread.CurrentThread.CurrentUICulture);
			if (!(cultureDirectory == "rtl"))
			{
				return cultureDirectory;
			}
			return "0";
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000894D8 File Offset: 0x000876D8
		private string GetLayoutSuffix()
		{
			string result;
			switch (this.UserAgent.Layout)
			{
			case LayoutType.TouchNarrow:
				result = "narrow";
				break;
			case LayoutType.TouchWide:
				result = "wide";
				break;
			case LayoutType.Mouse:
				result = "mouse";
				break;
			default:
				result = "mouse";
				break;
			}
			return result;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00089526 File Offset: 0x00087726
		protected string GetScriptsFolder(bool bootScriptsFolder)
		{
			return this.FormatURIForCDN(ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString)), bootScriptsFolder);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0008953F File Offset: 0x0008773F
		protected string GetResourcesFolder(bool bootResourcesFolder)
		{
			return this.FormatURIForCDN(ResourcePathBuilderUtilities.GetBootResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString)), bootResourcesFolder);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00089558 File Offset: 0x00087758
		protected string GetStylesFolder(bool bootStylesFolder)
		{
			return this.FormatURIForCDN(ResourcePathBuilderUtilities.GetStyleResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString)), bootStylesFolder);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00089571 File Offset: 0x00087771
		protected string GetImagesFolder(bool bootImagesfolder)
		{
			return this.FormatURIForCDN(ResourcePathBuilderUtilities.GetBootImageResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString), DefaultPageBase.IsRtl), bootImagesfolder);
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0008958F File Offset: 0x0008778F
		protected string GetThemedImagesFolderFormat(bool bootThemedImagesFolder)
		{
			return this.FormatURIForCDN(ResourcePathBuilderUtilities.GetBootThemeImageResourcesRelativeFolderPath(this.VersionString, ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString), DefaultPageBase.IsRtl, this.ShouldSkipThemeFolder()), bootThemedImagesFolder);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000895B9 File Offset: 0x000877B9
		protected string GetStylesFolderFormat(bool bootStylesFolderFormat)
		{
			return this.FormatURIForCDN(ResourcePathBuilderUtilities.GetBootStyleResourcesRelativeFolderPath(this.VersionString, ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.VersionString), "#LCL", this.ShouldSkipThemeFolder()), bootStylesFolderFormat);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000895E3 File Offset: 0x000877E3
		protected string FormatResourceString(string link)
		{
			return link.ToLowerInvariant();
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000895EB File Offset: 0x000877EB
		protected virtual string GetLocalizedOsfStringResourcePath(bool isBootResourcePath)
		{
			return this.LocalizedOsfStringResource.GetResourcePath(this, false);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000895FC File Offset: 0x000877FC
		protected virtual bool CalculateCompositeSessionDataEnabled()
		{
			bool result = false;
			if (this.IsAppCacheEnabledClient && !this.IsClientInOfflineMode && !DefaultPageBase.IsRecoveryBoot(this.Context))
			{
				UserContext userContext = UserContextManager.GetUserContext(this.Context);
				result = (userContext != null && userContext.FeaturesManager != null && userContext.FeaturesManager.ServerSettings.OwaCompositeSessionData.Enabled);
			}
			return result;
		}

		// Token: 0x04001623 RID: 5667
		public const string CompositeResourceRequestQueryParam = "crr";

		// Token: 0x04001624 RID: 5668
		public const string CompositeResourceRequestRetryQueryParam = "crrretry";

		// Token: 0x04001625 RID: 5669
		public const string RetryCompositeResourceRequest = "RetryCrrRequest";

		// Token: 0x04001626 RID: 5670
		public const string AppCacheClientQueryParam = "appcacheclient";

		// Token: 0x04001627 RID: 5671
		public const string AppCacheEnabledClientValue = "1";

		// Token: 0x04001628 RID: 5672
		public const string AppCacheDisabledClientValue = "0";

		// Token: 0x04001629 RID: 5673
		public const string ExternalDropScriptResources = "extDropScriptResources";

		// Token: 0x0400162A RID: 5674
		public const string ChangeLayoutFeatureEnabledVar = "changeLayoutEnabled";

		// Token: 0x0400162B RID: 5675
		public const string LayoutVar = "layout";

		// Token: 0x0400162C RID: 5676
		public const string StylesLocaleVar = "stylesLocale";

		// Token: 0x0400162D RID: 5677
		public const string AppCachedPageVar = "appCachedPage";

		// Token: 0x0400162E RID: 5678
		public const string ClientServerFeaturesVar = "featuresVar";

		// Token: 0x0400162F RID: 5679
		public const string UserSpecificResourcesHashVar = "userSpecificResourcesHashVar";

		// Token: 0x04001630 RID: 5680
		public const string UserLanguageVar = "userLanguageVar";

		// Token: 0x04001631 RID: 5681
		public const string UserCultureVar = "userCultureVar";

		// Token: 0x04001632 RID: 5682
		public const string UserCultureRtlVar = "userCultureRtl";

		// Token: 0x04001633 RID: 5683
		public const string ScriptResourcesVar = "scriptResources";

		// Token: 0x04001634 RID: 5684
		public const string UserScriptResourcesVar = "userScriptResources";

		// Token: 0x04001635 RID: 5685
		public const string StyleSheetResourcesVar = "styleResources";

		// Token: 0x04001636 RID: 5686
		public const string ServerVersionVar = "sver";

		// Token: 0x04001637 RID: 5687
		public const string LastClientVersionVar = "lcver";

		// Token: 0x04001638 RID: 5688
		public const string ThemeVar = "clientTheme";

		// Token: 0x04001639 RID: 5689
		public const string StylesFolderCulturePlaceHolder = "#LCL";

		// Token: 0x0400163A RID: 5690
		public const string LocalizedExtStringResourceName = "osfruntime_strings.js";

		// Token: 0x0400163B RID: 5691
		public const string UserSpecificResourceInjectorJs = "userspecificresourceinjector.ashx?ver={0}";

		// Token: 0x0400163C RID: 5692
		public const string LanguageReplacementMarker = "##LANG##";

		// Token: 0x0400163D RID: 5693
		public const string CultureReplacementMarker = "##CULTURE##";

		// Token: 0x0400163E RID: 5694
		public const string AuthParamName = "authRedirect";

		// Token: 0x0400163F RID: 5695
		public const string ClientExistingVersionParam = "cver";

		// Token: 0x04001640 RID: 5696
		public const string VersionParam = "ver";

		// Token: 0x04001641 RID: 5697
		public const string FeaturesParam = "cf";

		// Token: 0x04001642 RID: 5698
		public const string VersionChangeParam = "vC";

		// Token: 0x04001643 RID: 5699
		public const string AppCacheParam = "appcache";

		// Token: 0x04001644 RID: 5700
		public const string BootOnlineParam = "bO";

		// Token: 0x04001645 RID: 5701
		public const string StartLoadTimeVarName = "st";

		// Token: 0x04001646 RID: 5702
		public const string LayoutParameterName = "layout";

		// Token: 0x04001647 RID: 5703
		public const string DefaultPageVersion = "3";

		// Token: 0x04001648 RID: 5704
		private const string PathParameter = "path";

		// Token: 0x04001649 RID: 5705
		private const string ModurlParameter = "modurl";

		// Token: 0x0400164A RID: 5706
		internal static readonly Dictionary<string, string> ParamsInAppCache = new Dictionary<string, string>
		{
			{
				"appcache",
				"true"
			},
			{
				"realm",
				null
			},
			{
				"layout",
				null
			},
			{
				"wa",
				"wsignin1.0"
			},
			{
				"palenabled",
				"1"
			}
		};

		// Token: 0x0400164B RID: 5707
		private static readonly string[] ParametersToRemove = new string[]
		{
			"authredirect",
			"ll-cc",
			"exsvurl",
			"source",
			"cbcxt",
			"lc",
			"exch",
			"delegatedorg",
			"ae",
			"slusng",
			"id",
			"src",
			"to",
			"type",
			"vd"
		};

		// Token: 0x0400164C RID: 5708
		private static Dictionary<string, Tuple<string, DateTime>> inlineScripts = new Dictionary<string, Tuple<string, DateTime>>();

		// Token: 0x0400164D RID: 5709
		private static Dictionary<string, Tuple<string, DateTime>> inlineImages = new Dictionary<string, Tuple<string, DateTime>>();

		// Token: 0x0400164E RID: 5710
		private static Dictionary<string, Tuple<string, DateTime>> inlineStyles = new Dictionary<string, Tuple<string, DateTime>>();

		// Token: 0x0400164F RID: 5711
		private ResourceBase[] userDataEmbededLinks;

		// Token: 0x04001650 RID: 5712
		private string theme;

		// Token: 0x04001651 RID: 5713
		private Slab bootSlab;

		// Token: 0x04001652 RID: 5714
		private SlabManifestType slabManifestType;

		// Token: 0x04001653 RID: 5715
		private LocalizedExtensibilityStringsScriptResource localizedOsfStringResource;
	}
}
