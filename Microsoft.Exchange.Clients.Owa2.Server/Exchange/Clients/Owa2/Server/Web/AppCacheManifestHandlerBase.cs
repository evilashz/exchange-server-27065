using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Clients.Security;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200046E RID: 1134
	internal abstract class AppCacheManifestHandlerBase : IHttpHandler, IPageContext
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x00089D59 File Offset: 0x00087F59
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x00089D5C File Offset: 0x00087F5C
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x00089D64 File Offset: 0x00087F64
		public virtual UserAgent UserAgent { get; private set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x00089D6D File Offset: 0x00087F6D
		public CultureInfo UserCultureInfo
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00089D79 File Offset: 0x00087F79
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x00089D81 File Offset: 0x00087F81
		public HttpContext Context { get; private set; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00089D8A File Offset: 0x00087F8A
		public HttpResponse Response
		{
			get
			{
				return this.Context.Response;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x00089D97 File Offset: 0x00087F97
		public string Theme
		{
			get
			{
				return this.GetThemeFolder();
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00089D9F File Offset: 0x00087F9F
		public bool SessionDataEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00089DA2 File Offset: 0x00087FA2
		public bool IsAppCacheEnabledClient
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600262E RID: 9774
		public abstract HostNameController HostNameController { get; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x00089DA8 File Offset: 0x00087FA8
		protected virtual string ManifestTemplate
		{
			get
			{
				switch (this.UserAgent.Layout)
				{
				case LayoutType.TouchNarrow:
					return AppCacheManifestHandlerBase.NarrowManifestTemplate.Template;
				case LayoutType.TouchWide:
					return AppCacheManifestHandlerBase.WideManifestTemplate.Template;
				case LayoutType.Mouse:
					return AppCacheManifestHandlerBase.MouseManifestTemplate.Template;
				default:
					throw new InvalidOperationException("Unexpected layout " + this.UserAgent.Layout);
				}
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00089E18 File Offset: 0x00088018
		public virtual SlabManifestType ManifestType
		{
			get
			{
				if (!DefaultPageBase.IsPalEnabled(this.Context, this.UserAgent.RawString))
				{
					return SlabManifestType.Standard;
				}
				return SlabManifestType.Pal;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002631 RID: 9777
		protected abstract bool IsDatacenterNonDedicated { get; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002632 RID: 9778
		protected abstract string ResourceDirectory { get; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002633 RID: 9779
		protected abstract bool UseRootDirForAppCacheVdir { get; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00089E4A File Offset: 0x0008804A
		protected virtual bool? IsRequestForOfflineManifest
		{
			get
			{
				return OfflineClientRequestUtilities.IsRequestForOfflineAppcacheManifest(this.Context.Request);
			}
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00089E5C File Offset: 0x0008805C
		public static byte[] CalculateHashOnHashes(List<byte[]> hashes)
		{
			int num = 0;
			for (int i = 0; i < hashes.Count; i++)
			{
				num += hashes[i].Length;
			}
			byte[] array = new byte[num];
			num = 0;
			for (int j = 0; j < hashes.Count; j++)
			{
				hashes[j].CopyTo(array, num);
				num += hashes[j].Length;
			}
			byte[] result;
			using (SHA1Cng sha1Cng = new SHA1Cng())
			{
				sha1Cng.Initialize();
				result = sha1Cng.ComputeHash(array);
			}
			return result;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00089EF4 File Offset: 0x000880F4
		public static bool DoesBrowserSupportAppCache(UserAgent userAgent)
		{
			return userAgent.IsBrowserChrome() || userAgent.IsBrowserSafari() || (userAgent.IsBrowserIE() && (userAgent.BrowserVersion.Build >= 10 || userAgent.GetTridentVersion() >= 6.0)) || (userAgent.IsBrowserFirefox() && userAgent.BrowserVersion.Build >= 23);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00089F57 File Offset: 0x00088157
		public string FormatURIForCDN(string relativeUri, bool isBootResourceUri)
		{
			return relativeUri;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00089F5A File Offset: 0x0008815A
		public string GetCdnEndpointForResources(bool bootResources)
		{
			return string.Empty;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00089F64 File Offset: 0x00088164
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.Context = context;
			this.UserAgent = OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context);
			ExTraceGlobals.AppcacheManifestHandlerTracer.TraceDebug<string, LayoutType>((long)this.GetHashCode(), "User {0} requested for {1} manifest ", AppCacheManifestHandler.GetUserContextId(context), this.UserAgent.Layout);
			bool flag = this.IsManifestRequest();
			this.SetResponseHeaders(flag);
			this.AddAppCacheVersionCookie();
			UserAgent userAgent = OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context, null, true);
			UserAgent userAgent2 = OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context);
			if ((!this.IsManifestLinkerRequest() && !flag) || (this.ShouldUnInstallAppCache() && flag) || (userAgent2.Layout == LayoutType.TouchNarrow && userAgent.Layout == LayoutType.TouchWide))
			{
				this.RemoveManifest();
				return;
			}
			if (flag && this.IsRealmRewrittenFromPathToQuery(this.Context))
			{
				this.RemoveManifest();
				return;
			}
			try
			{
				if (flag)
				{
					bool? isRequestForOfflineManifest = this.IsRequestForOfflineManifest;
					if (isRequestForOfflineManifest == null)
					{
						this.Response.StatusCode = 440;
					}
					else
					{
						this.AddIsClientAppCacheEnabledCookie();
						if (isRequestForOfflineManifest.Value)
						{
							this.WriteManifest(true);
						}
						else
						{
							this.WriteManifest(false);
						}
					}
				}
				else
				{
					this.WriteManifestLinker();
				}
			}
			catch (IOException)
			{
				this.Response.StatusCode = 500;
			}
		}

		// Token: 0x0600263A RID: 9786
		protected abstract bool IsRealmRewrittenFromPathToQuery(HttpContext context);

		// Token: 0x0600263B RID: 9787
		protected abstract string GetThemeFolder();

		// Token: 0x0600263C RID: 9788
		protected abstract bool ShouldSkipThemeFolder();

		// Token: 0x0600263D RID: 9789
		protected abstract string GetUserUPN();

		// Token: 0x0600263E RID: 9790
		protected abstract bool ShouldAddDefaultMasterPageEntiresWithFlightDisabled();

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x0008A088 File Offset: 0x00088288
		protected virtual bool IsMowaClient
		{
			get
			{
				return OfflineClientRequestUtilities.IsRequestFromMOWAClient(this.Context.Request, this.UserAgent.RawString);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002640 RID: 9792
		protected abstract bool IsHostNameSwitchFlightEnabled { get; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002641 RID: 9793
		// (set) Token: 0x06002642 RID: 9794
		public abstract string VersionString { get; set; }

		// Token: 0x06002643 RID: 9795 RVA: 0x0008A0A5 File Offset: 0x000882A5
		protected virtual string GetLocalizedCultureNameForThemesResource(CultureInfo userCultureInfo)
		{
			return LocalizedThemeStyleResource.GetCultureDirectory(userCultureInfo);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0008A0AD File Offset: 0x000882AD
		protected virtual string GetLocalizedCultureNameForImagesResource(CultureInfo userCultureInfo)
		{
			return ThemeStyleResource.GetSpriteDirectory(userCultureInfo);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0008A0B5 File Offset: 0x000882B5
		protected virtual string AddedQueryParameters()
		{
			return string.Empty;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0008A0BC File Offset: 0x000882BC
		protected virtual bool IsManifestRequest()
		{
			return this.Context.Request.QueryString["owamanifest"] == "1";
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0008A0E2 File Offset: 0x000882E2
		protected virtual bool IsManifestLinkerRequest()
		{
			return this.Context.Request.QueryString["manifest"] == "0";
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0008A108 File Offset: 0x00088308
		protected bool ShouldUnInstallAppCache()
		{
			if (this.IsHostNameSwitchFlightEnabled && !this.HostNameController.IsUserAgentExcludedFromHostNameSwitchFlight(this.Context.Request))
			{
				Uri requestUrlEvenIfProxied = this.Context.Request.GetRequestUrlEvenIfProxied();
				string text;
				if (this.HostNameController.IsDeprecatedHostName(requestUrlEvenIfProxied.Host, out text))
				{
					return true;
				}
			}
			HttpCookie httpCookie = this.Context.Request.Cookies.Get("UnInstallAppcache");
			if (httpCookie == null || httpCookie.Value == null)
			{
				return false;
			}
			string value = httpCookie.Value;
			return value.ToLower() == true.ToString().ToLower();
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x0008A1AB File Offset: 0x000883AB
		protected virtual SlabManifestCollection SlabManifestCollection
		{
			get
			{
				return SlabManifestCollectionFactory.GetInstance(this.VersionString);
			}
		}

		// Token: 0x0600264A RID: 9802
		protected abstract List<CultureInfo> GetSupportedCultures();

		// Token: 0x0600264B RID: 9803 RVA: 0x0008A228 File Offset: 0x00088428
		protected virtual IEnumerable<string> GetScripts(bool generateBootResourcesAppcache)
		{
			bool isMowaClient = this.IsMowaClient;
			SlabManifestCollection slabManifestCollection = this.SlabManifestCollection;
			SlabManifestType slabManifestType = isMowaClient ? SlabManifestType.Pal : SlabManifestType.Standard;
			string[] enabledFeatures = this.GetEnabledFeatures();
			IEnumerable<string> enumerable = from source in slabManifestCollection.GetCodeScriptResources(SlabManifestType.PreBoot, this.UserAgent.Layout, enabledFeatures, false).Union(slabManifestCollection.GetCodeScriptResources(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache))
			select string.Format("../prem/{0}/scripts/{1}", this.VersionString, source);
			IEnumerable<string> source3 = from source in slabManifestCollection.GetLocalizedStringsScriptResources(SlabManifestType.PreBoot, this.UserAgent.Layout, enabledFeatures, false).Union(slabManifestCollection.GetLocalizedStringsScriptResources(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache))
			select source;
			if (source3.Any<string>())
			{
				LocalizedStringsScriptResource localizedStringsScriptResource = new LocalizedStringsScriptResource(source3.First<string>(), ResourceTarget.Any, this.VersionString);
				string cultureName = localizedStringsScriptResource.GetLocalizedCultureName();
				IEnumerable<string> second = from source in source3
				select string.Format("../prem/{0}/scripts/{1}/{2}", this.VersionString, cultureName.ToLowerInvariant(), source);
				enumerable = enumerable.Union(second);
			}
			IEnumerable<string> source2 = from source in slabManifestCollection.GetLocalizedExtStringsScriptResources(SlabManifestType.PreBoot, this.UserAgent.Layout, enabledFeatures, false).Union(slabManifestCollection.GetLocalizedExtStringsScriptResources(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache))
			select source;
			if (source2.Any<string>())
			{
				LocalizedExtensibilityStringsScriptResource localizedExtensibilityStringsScriptResource = new LocalizedExtensibilityStringsScriptResource(source2.First<string>(), ResourceTarget.Any, this.VersionString);
				string cultureName = localizedExtensibilityStringsScriptResource.GetLocalizedCultureName();
				IEnumerable<string> second2 = from source in source2
				select string.Format("../prem/{0}/scripts/ext/{1}/{2}", this.VersionString, cultureName.ToLowerInvariant(), source);
				enumerable = enumerable.Union(second2);
			}
			return enumerable;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0008A53C File Offset: 0x0008873C
		protected virtual IEnumerable<string> GetThemedResources(CultureInfo userCultureInfo, bool generateBootResourcesAppcache)
		{
			bool isMowaClient = this.IsMowaClient;
			SlabManifestCollection slabManifestCollection = this.SlabManifestCollection;
			SlabManifestType slabManifestType = isMowaClient ? SlabManifestType.Pal : SlabManifestType.Standard;
			string[] enabledFeatures = this.GetEnabledFeatures();
			string theme = this.GetThemeFolder();
			string locale = this.GetLocalizedCultureNameForThemesResource(userCultureInfo);
			IEnumerable<string> first = this.ShouldSkipThemeFolder() ? (from style in slabManifestCollection.GetThemedStyles(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache)
			select string.Format("../prem/{0}/resources/styles/{1}/{2}", this.VersionString, locale, style)) : slabManifestCollection.GetThemedStyles(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache).Select((string style) => string.Format("../prem/{0}/resources/themes/{1}/{2}/{3}", new object[]
			{
				this.VersionString,
				theme,
				locale,
				style
			}));
			IEnumerable<string> second = from image in slabManifestCollection.GetThemedImages(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache)
			select string.Format("../prem/{0}/resources/themes/{1}/images/{2}/{3}", new object[]
			{
				this.VersionString,
				theme,
				DefaultPageBase.IsRtl ? "rtl" : "0",
				image
			});
			IEnumerable<string> second2 = this.ShouldSkipThemeFolder() ? (from image in slabManifestCollection.GetThemedSpriteStyles(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache)
			select string.Format("../prem/{0}/resources/images/{1}/{2}", this.VersionString, DefaultPageBase.IsRtl ? "rtl" : "0", image)) : slabManifestCollection.GetThemedSpriteStyles(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache).Select((string image) => string.Format("../prem/{0}/resources/themes/{1}/images/{2}/{3}", new object[]
			{
				this.VersionString,
				theme,
				DefaultPageBase.IsRtl ? "rtl" : "0",
				image
			}));
			return first.Union(second).Union(second2);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0008A6CC File Offset: 0x000888CC
		protected virtual IEnumerable<string> GetNonThemedResources(CultureInfo userCultureInfo, bool generateBootResourcesAppcache)
		{
			bool isMowaClient = this.IsMowaClient;
			SlabManifestCollection slabManifestCollection = this.SlabManifestCollection;
			SlabManifestType slabManifestType = isMowaClient ? SlabManifestType.Pal : SlabManifestType.Standard;
			string[] enabledFeatures = this.GetEnabledFeatures();
			this.GetLocalizedCultureNameForThemesResource(userCultureInfo);
			IEnumerable<string> first = from image in slabManifestCollection.GetNonThemedImages(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache)
			select string.Format("../prem/{0}/resources/images/{1}/{2}", this.VersionString, DefaultPageBase.IsRtl ? "rtl" : "0", image);
			IEnumerable<string> second = from font in slabManifestCollection.GetFonts(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache)
			select string.Format("../prem/{0}/resources/styles/{1}", this.VersionString, font);
			IEnumerable<string> second2 = from style in slabManifestCollection.GetNonThemedStyles(slabManifestType, this.UserAgent.Layout, enabledFeatures, generateBootResourcesAppcache)
			select string.Format("../prem/{0}/resources/styles/{1}", this.VersionString, style);
			return first.Union(second).Union(second2);
		}

		// Token: 0x0600264E RID: 9806
		protected abstract string[] GetEnabledFeatures();

		// Token: 0x0600264F RID: 9807 RVA: 0x0008A78C File Offset: 0x0008898C
		private static byte[] GetHash(string fileName, string fileVersion)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"] + fileName);
				Tuple<string, string> key = Tuple.Create<string, string>(fileName, fileVersion);
				Tuple<DateTime, byte[]> tuple;
				if (!AppCacheManifestHandlerBase.hashMap.TryGetValue(key, out tuple))
				{
					tuple = Tuple.Create<DateTime, byte[]>(fileInfo.LastWriteTimeUtc, AppCacheManifestHandlerBase.CalculateFileHash(fileInfo));
				}
				else
				{
					if (!(tuple.Item1 != fileInfo.LastWriteTimeUtc))
					{
						return tuple.Item2;
					}
					tuple = Tuple.Create<DateTime, byte[]>(fileInfo.LastWriteTimeUtc, AppCacheManifestHandlerBase.CalculateFileHash(fileInfo));
				}
				Dictionary<Tuple<string, string>, Tuple<DateTime, byte[]>> dictionary = new Dictionary<Tuple<string, string>, Tuple<DateTime, byte[]>>(AppCacheManifestHandlerBase.hashMap);
				dictionary[key] = tuple;
				AppCacheManifestHandlerBase.hashMap = dictionary;
				return tuple.Item2;
			}
			catch (FileNotFoundException)
			{
			}
			return null;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0008A854 File Offset: 0x00088A54
		private static byte[] CalculateFileHash(FileInfo fileInfo)
		{
			if (fileInfo.Exists)
			{
				using (SHA1Cng sha1Cng = new SHA1Cng())
				{
					using (FileStream fileStream = fileInfo.OpenRead())
					{
						sha1Cng.Initialize();
						return sha1Cng.ComputeHash(fileStream);
					}
				}
			}
			return new byte[0];
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0008A8C0 File Offset: 0x00088AC0
		private void SetResponseHeaders(bool isManifestRequest)
		{
			if (isManifestRequest)
			{
				this.Response.ContentType = "text/cache-manifest";
			}
			else
			{
				this.Response.ContentType = "text/html";
			}
			this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			this.Response.CacheControl = "No-Cache";
			this.Response.Cache.SetNoServerCaching();
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0008A924 File Offset: 0x00088B24
		private void AddIsClientAppCacheEnabledCookie()
		{
			HttpCookie httpCookie = new HttpCookie("IsClientAppCacheEnabled");
			httpCookie.Value = true.ToString();
			this.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0008A95C File Offset: 0x00088B5C
		private void AddAppCacheVersionCookie()
		{
			HttpCookie httpCookie = new HttpCookie("AppcacheVer");
			httpCookie.Value = this.VersionString + ":" + this.UserCultureInfo.Name.ToLowerInvariant() + this.Theme.ToLowerInvariant();
			this.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0008A9C8 File Offset: 0x00088BC8
		private void WriteManifest(bool isOffline)
		{
			StringBuilder stringBuilder = new StringBuilder(this.ManifestTemplate);
			if (string.IsNullOrWhiteSpace(stringBuilder.ToString()))
			{
				string text = string.Format("User {0} request for {1} manifest fetched null or empty manifest", AppCacheManifestHandler.GetUserContextId(this.Context), this.UserAgent.Layout);
				ExTraceGlobals.AppcacheManifestHandlerTracer.TraceError((long)this.GetHashCode(), text);
				throw new ArgumentNullException(text);
			}
			bool generateBootResourcesAppcache = !isOffline;
			stringBuilder = this.AddTemplatedParameters(stringBuilder, generateBootResourcesAppcache);
			string text2 = stringBuilder.ToString();
			this.Response.Write(text2);
			string[] array = text2.Split(new char[]
			{
				'\r',
				'\n',
				' ',
				'\t'
			}, StringSplitOptions.RemoveEmptyEntries);
			string resourceDirectory = this.ResourceDirectory;
			List<byte[]> list = new List<byte[]>();
			foreach (string text3 in array)
			{
				if (text3.StartsWith(resourceDirectory))
				{
					list.Add(AppCacheManifestHandlerBase.GetHash(text3.Substring(3), this.VersionString));
				}
			}
			IOrderedEnumerable<string> orderedEnumerable = from id in this.GetEnabledFeatures()
			orderby id
			select id;
			foreach (string s in orderedEnumerable)
			{
				list.Add(Encoding.ASCII.GetBytes(s));
			}
			this.Response.Write("# ComputedHash: ");
			this.Response.Write(Convert.ToBase64String(AppCacheManifestHandlerBase.CalculateHashOnHashes(list)));
			this.Response.Write("\r\n");
			this.Response.Write("# Offline: ");
			this.Response.Write(isOffline.ToString().ToLower());
			this.Response.Write("\r\n");
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0008ABA0 File Offset: 0x00088DA0
		private StringBuilder AddTemplatedParameters(StringBuilder manifest, bool generateBootResourcesAppcache)
		{
			CultureInfo userCultureInfo = this.UserCultureInfo;
			manifest = this.AddVersion(manifest);
			manifest = this.AddDefaultMasterPageUrls(manifest);
			manifest = this.AddUserCulture(manifest, userCultureInfo);
			manifest = this.AddUserRealm(manifest);
			manifest = this.AddUserResources(manifest, userCultureInfo, generateBootResourcesAppcache);
			manifest = this.AddVirtualDirectoryRoot(manifest);
			return manifest;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0008ABEE File Offset: 0x00088DEE
		private StringBuilder AddVersion(StringBuilder manifest)
		{
			manifest = manifest.Replace("$OWA2_VER", this.VersionString);
			return manifest;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0008AC04 File Offset: 0x00088E04
		private StringBuilder AddDefaultMasterPageUrls(StringBuilder manifest)
		{
			string[] array = AppCacheManifestHandlerBase.ManifestFile.MasterPageManifestEntries;
			if (this.IsDatacenterNonDedicated)
			{
				array = array.Concat(AppCacheManifestHandlerBase.ManifestFile.DatacenterManifestEntries).ToArray<string>();
				if (!this.IsMowaClient)
				{
					array = array.Concat(AppCacheManifestHandlerBase.ManifestFile.DatacenterManifestEntriesForNonMowaClients).ToArray<string>();
				}
			}
			if (this.ShouldAddDefaultMasterPageEntiresWithFlightDisabled())
			{
				array = AppCacheManifestHandlerBase.ManifestFile.MasterPageManifestEntiresFlightsDisabled;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in array)
			{
				stringBuilder.AppendLine(value);
			}
			manifest.Replace("$Default_MasterPageUrls", stringBuilder.ToString());
			return manifest;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0008AC91 File Offset: 0x00088E91
		private StringBuilder AddUserCulture(StringBuilder manifest, CultureInfo userCultureInfo)
		{
			manifest = manifest.Replace("$USER_CULTURE", userCultureInfo.Name.ToLowerInvariant());
			return manifest;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0008ACAC File Offset: 0x00088EAC
		private StringBuilder AddResourceList(StringBuilder manifest, IEnumerable<string> resources)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in resources)
			{
				stringBuilder.AppendLine(value);
			}
			manifest.Replace("$USER_RESOURCES", stringBuilder.ToString());
			return manifest;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0008AD10 File Offset: 0x00088F10
		private StringBuilder AddUserSpecificResourceInjector(StringBuilder manifest)
		{
			manifest = manifest.Replace("\r\nCACHE:\r\n", "\r\nCACHE:\r\n../" + string.Format("userspecificresourceinjector.ashx?ver={0}", this.VersionString).ToLowerInvariant() + "\r\n");
			return manifest;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0008AD44 File Offset: 0x00088F44
		private StringBuilder AddUserRealm(StringBuilder manifest)
		{
			manifest = manifest.Replace("$USER_REALM", HttpUtility.UrlEncode(this.GetUserRealm(this.GetUserUPN())));
			return manifest;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0008AD68 File Offset: 0x00088F68
		private StringBuilder AddUserResources(StringBuilder manifest, CultureInfo cultureInfo, bool generateBootResourcesAppcache)
		{
			IEnumerable<string> scripts = this.GetScripts(generateBootResourcesAppcache);
			IEnumerable<string> nonThemedResources = this.GetNonThemedResources(cultureInfo, generateBootResourcesAppcache);
			IEnumerable<string> themedResources = this.GetThemedResources(cultureInfo, generateBootResourcesAppcache);
			return this.AddResourceList(manifest, scripts.Union(themedResources).Union(nonThemedResources));
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0008ADA3 File Offset: 0x00088FA3
		private void WriteManifestLinker()
		{
			this.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			this.Response.Write("<html manifest=\"appCacheManifestHandler.ashx?owamanifest=1");
			this.Response.Write("\"><head></head><body></body></html>");
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0008ADD5 File Offset: 0x00088FD5
		private void RemoveManifest()
		{
			this.Response.StatusCode = 404;
			this.Response.TrySkipIisCustomErrors = true;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0008ADF4 File Offset: 0x00088FF4
		private StringBuilder AddVirtualDirectoryRoot(StringBuilder manifest)
		{
			string newValue = string.Empty;
			if (this.UseRootDirForAppCacheVdir)
			{
				newValue = "/";
			}
			else
			{
				string text = this.Context.Request.ApplicationPath ?? string.Empty;
				string text2 = text.TrimEnd(new char[]
				{
					'/'
				});
				newValue = text2;
			}
			if (Globals.IsPreCheckinApp)
			{
				string name = "X-DFPOWA-Vdir";
				if (this.Context.Request.Cookies[name] != null)
				{
					string value = this.Context.Request.Cookies[name].Value;
					string str = HttpUtility.UrlEncode(this.GetUserRealm(this.GetUserUPN()));
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("/");
					stringBuilder.AppendLine("../");
					stringBuilder.AppendLine("../microsoft.com");
					stringBuilder.AppendLine("../microsoft.com/");
					stringBuilder.AppendLine("../?vdir=" + value);
					stringBuilder.AppendLine("../?wa=wsignin1.0&vdir=" + value);
					stringBuilder.AppendLine("../?realm=" + str + "&vdir=" + value);
					stringBuilder.AppendLine("../?wa=wsignin1.0&?realm=" + str + "&vdir=" + value);
					stringBuilder.AppendLine("../?flights=none&vdir=" + value);
					stringBuilder.AppendLine("../?vdir=" + value + "&flights=none");
					return manifest.Replace("$ROOT", stringBuilder.ToString());
				}
			}
			return manifest.Replace("$ROOT", newValue);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x0008AF8C File Offset: 0x0008918C
		private string GetUserRealm(string smtpAddress)
		{
			string text = string.Empty;
			int num = smtpAddress.IndexOf('@');
			if (num >= 0 && num < smtpAddress.Length - 1)
			{
				text = smtpAddress.Substring(num + 1);
			}
			return text.ToLowerInvariant();
		}

		// Token: 0x0400165F RID: 5727
		public const string AppcacheHandlerName = "appcachemanifesthandler.ashx";

		// Token: 0x04001660 RID: 5728
		public const string AppcacheVer = "AppcacheVer";

		// Token: 0x04001661 RID: 5729
		public const string UnInstallAppcache = "UnInstallAppcache";

		// Token: 0x04001662 RID: 5730
		public const string ManifestLinkerParameterName = "manifest";

		// Token: 0x04001663 RID: 5731
		private const string VersionStr = "$OWA2_VER";

		// Token: 0x04001664 RID: 5732
		private const string UserCultureStr = "$USER_CULTURE";

		// Token: 0x04001665 RID: 5733
		private const string DefaultMasterPageUrlStr = "$Default_MasterPageUrls";

		// Token: 0x04001666 RID: 5734
		private const string ResourceStr = "$USER_RESOURCES";

		// Token: 0x04001667 RID: 5735
		private const string RootStr = "$ROOT";

		// Token: 0x04001668 RID: 5736
		private const string UserRealmStr = "$USER_REALM";

		// Token: 0x04001669 RID: 5737
		private const string OwaManifestParameterName = "owamanifest";

		// Token: 0x0400166A RID: 5738
		private static AppCacheManifestHandlerBase.ManifestFile MouseManifestTemplate = new AppCacheManifestHandlerBase.ManifestFile(LayoutType.Mouse);

		// Token: 0x0400166B RID: 5739
		private static AppCacheManifestHandlerBase.ManifestFile NarrowManifestTemplate = new AppCacheManifestHandlerBase.ManifestFile(LayoutType.TouchNarrow);

		// Token: 0x0400166C RID: 5740
		private static AppCacheManifestHandlerBase.ManifestFile WideManifestTemplate = new AppCacheManifestHandlerBase.ManifestFile(LayoutType.TouchWide);

		// Token: 0x0400166D RID: 5741
		private static Dictionary<Tuple<string, string>, Tuple<DateTime, byte[]>> hashMap = new Dictionary<Tuple<string, string>, Tuple<DateTime, byte[]>>();

		// Token: 0x0200046F RID: 1135
		private class ManifestFile
		{
			// Token: 0x0600266B RID: 9835 RVA: 0x0008AFFC File Offset: 0x000891FC
			public ManifestFile(LayoutType layout)
			{
				this.layout = layout;
			}

			// Token: 0x17000A41 RID: 2625
			// (get) Token: 0x0600266C RID: 9836 RVA: 0x0008B050 File Offset: 0x00089250
			public string Template
			{
				get
				{
					if (this.template == null)
					{
						StringBuilder builder = new StringBuilder();
						Array.ForEach<string>(AppCacheManifestHandlerBase.ManifestFile.AnyManifestEntries, delegate(string s)
						{
							builder.AppendLine(s);
						});
						switch (this.layout)
						{
						case LayoutType.TouchNarrow:
							Array.ForEach<string>(AppCacheManifestHandlerBase.ManifestFile.NarrowManifestEntries, delegate(string s)
							{
								builder.AppendLine(s);
							});
							break;
						case LayoutType.TouchWide:
							Array.ForEach<string>(AppCacheManifestHandlerBase.ManifestFile.WideManifestEntries, delegate(string s)
							{
								builder.AppendLine(s);
							});
							break;
						case LayoutType.Mouse:
							Array.ForEach<string>(AppCacheManifestHandlerBase.ManifestFile.MouseManifestEntries, delegate(string s)
							{
								builder.AppendLine(s);
							});
							break;
						}
						this.template = string.Format(AppCacheManifestHandlerBase.ManifestFile.ManifestTemplateFormat, new object[]
						{
							"$OWA2_VER",
							"$Default_MasterPageUrls",
							builder.ToString(),
							"$USER_RESOURCES"
						});
					}
					return this.template;
				}
			}

			// Token: 0x04001673 RID: 5747
			public static readonly string[] MasterPageManifestEntries = new string[]
			{
				"$ROOT",
				"../",
				string.Format("../{0}", "$USER_REALM"),
				string.Format("../{0}/", "$USER_REALM"),
				string.Format("../?realm={0}", "$USER_REALM")
			};

			// Token: 0x04001674 RID: 5748
			public static readonly string[] DatacenterManifestEntries = new string[]
			{
				"../?wa=wsignin1.0",
				string.Format("../?realm={0}&wa=wsignin1.0", "$USER_REALM")
			};

			// Token: 0x04001675 RID: 5749
			public static readonly string[] DatacenterManifestEntriesForNonMowaClients = new string[]
			{
				string.Format("../?wa=wsignin1.0&realm={0}", "$USER_REALM")
			};

			// Token: 0x04001676 RID: 5750
			public static readonly string[] MasterPageManifestEntiresFlightsDisabled = new string[]
			{
				"../?flights=none"
			};

			// Token: 0x04001677 RID: 5751
			private static readonly string ManifestTemplateFormat = "CACHE MANIFEST\r\n# v. {0}\r\n\r\nCACHE:\r\n\r\n{1}{2}{3}\r\nNETWORK:\r\n*\r\n\r\n";

			// Token: 0x04001678 RID: 5752
			private static readonly string[] AnyManifestEntries = new string[]
			{
				"../1x1.gif",
				string.Format("../prem/$OWA2_VER/scripts/globalize/globalize.culture.{0}.js", "$USER_CULTURE")
			};

			// Token: 0x04001679 RID: 5753
			private static readonly string[] MouseManifestEntries = new string[]
			{
				"../?layout=mouse",
				"../popout.aspx",
				"../projection.aspx"
			};

			// Token: 0x0400167A RID: 5754
			private static readonly string[] NarrowManifestEntries = new string[]
			{
				"../?layout=tnarrow"
			};

			// Token: 0x0400167B RID: 5755
			private static readonly string[] WideManifestEntries = new string[]
			{
				"../?layout=twide"
			};

			// Token: 0x0400167C RID: 5756
			private string template;

			// Token: 0x0400167D RID: 5757
			private readonly LayoutType layout;
		}
	}
}
