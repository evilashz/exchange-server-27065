using System;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x020004A1 RID: 1185
	internal abstract class UserSpecificResourceInjectorBase : IHttpHandler
	{
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x00095BA7 File Offset: 0x00093DA7
		public HttpResponse Response
		{
			get
			{
				return this.Context.Response;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x00095BB4 File Offset: 0x00093DB4
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x00095BBC File Offset: 0x00093DBC
		public HttpContext Context { get; private set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x00095BC5 File Offset: 0x00093DC5
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x00095BCD File Offset: 0x00093DCD
		public bool SessionDataEnabled { get; private set; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x00095BD6 File Offset: 0x00093DD6
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00095BDC File Offset: 0x00093DDC
		public void ProcessRequest(HttpContext context)
		{
			this.Context = context;
			UserContext userContext = UserContextManager.GetUserContext(context);
			string currentOwaVersion = userContext.CurrentOwaVersion;
			this.pageContext = this.GetPageContext(context, currentOwaVersion);
			this.SessionDataEnabled = false;
			context.Response.ContentType = "application/x-javascript";
			this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			this.Response.CacheControl = "No-Cache";
			this.Response.Cache.SetNoServerCaching();
			ResourceBase[] userDataEmbeddedLinks = UserResourcesFinder.GetUserDataEmbeddedLinks(this.pageContext.ManifestType, this.pageContext.UserAgent.Layout, currentOwaVersion);
			UserSpecificResourceInjectorBase.WriteScriptBlock(new Action<string>(this.Context.Response.Write), this.pageContext, userContext, userDataEmbeddedLinks, currentOwaVersion);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00095C9C File Offset: 0x00093E9C
		public static void WriteScriptBlock(Action<string> responseWriter, IPageContext pageContext, IUserContext userContext, ResourceBase[] userDataEmbeddedLinks, string owaVersion)
		{
			string text = null;
			string name = Thread.CurrentThread.CurrentCulture.Name;
			responseWriter("var blockToAdd;");
			foreach (ResourceBase resourceBase in userDataEmbeddedLinks)
			{
				ScriptResource scriptResource = resourceBase as ScriptResource;
				LocalizedStringsScriptResource localizedStringsScriptResource = scriptResource as LocalizedStringsScriptResource;
				string text2 = resourceBase.GetResourcePath(pageContext, true);
				if (scriptResource is LocalizedStringsScriptResource)
				{
					text = localizedStringsScriptResource.GetLocalizedCultureName();
				}
				else
				{
					string obj;
					if (scriptResource != null)
					{
						obj = "userScriptResources";
					}
					else
					{
						obj = "styleResources";
					}
					string text3 = "LT_ANY";
					LayoutType layoutType = LayoutType.Mouse;
					if (ResourceTarget.MouseOnly.Equals(resourceBase.TargetFilter))
					{
						layoutType = LayoutType.Mouse;
						text3 = "LT_MOUSE";
					}
					else if (ResourceTarget.WideOnly.Equals(resourceBase.TargetFilter) || ResourceTarget.WideHighResolution.Equals(resourceBase.TargetFilter))
					{
						layoutType = LayoutType.TouchWide;
						text3 = "LT_TWIDE";
					}
					else if (ResourceTarget.NarrowOnly.Equals(resourceBase.TargetFilter) || ResourceTarget.NarrowHighResolution.Equals(resourceBase.TargetFilter))
					{
						layoutType = LayoutType.TouchNarrow;
						text3 = "LT_TNARROW";
					}
					if (!(text3 != "LT_ANY") || layoutType == pageContext.UserAgent.Layout)
					{
						responseWriter(obj);
						responseWriter("[");
						responseWriter(obj);
						responseWriter(".length] = {");
						responseWriter(" \"fileName\": ");
						string text4 = "prem/" + owaVersion;
						if (pageContext.IsAppCacheEnabledClient && text2.StartsWith(text4))
						{
							text2 = text2.Substring(text4.Length);
							string obj2 = string.Format("\"prem/\" + {0} + ", "sver");
							responseWriter(obj2);
						}
						responseWriter("\"" + text2);
						responseWriter("\", \"layout\": ");
						responseWriter(text3);
						if (ResourceTarget.WideHighResolution.Equals(resourceBase.TargetFilter) || ResourceTarget.NarrowHighResolution.Equals(resourceBase.TargetFilter))
						{
							responseWriter(", \"highResolution\": true");
						}
						responseWriter("};\r\n");
					}
				}
			}
			string text5;
			string text6;
			if (userContext != null && userContext.FeaturesManager.ClientServerSettings.OwaVersioning.Enabled)
			{
				text5 = string.Format("'{0}'", ExchangeVersionType.V2_6.ToString());
				text6 = string.Format("'{0}'", ExchangeVersion.Latest.Version);
			}
			else
			{
				text5 = "null";
				text6 = "null";
			}
			string text7 = "[]";
			if (userContext != null)
			{
				text7 = UserResourcesFinder.GetEnabledFlightedFeaturesJsonArray(pageContext.ManifestType, userContext, FlightedFeatureScope.ClientServer);
			}
			responseWriter(string.Format("setupUserSpecificResources('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, '{7}', '{8}', {9}, {10});\r\n", new object[]
			{
				owaVersion,
				pageContext.Theme,
				name,
				text,
				DefaultPageBase.IsRtl ? "true" : "false",
				text5,
				text6,
				DefaultPageBase.ThemeStyleCultureDirectory,
				UserResourcesFinder.GetResourcesHash(userDataEmbeddedLinks, pageContext, true, owaVersion),
				text7,
				"3"
			}));
			responseWriter("userSpecificsLoaded = true;");
		}

		// Token: 0x0600287D RID: 10365
		protected abstract IPageContext GetPageContext(HttpContext context, string owaVersion);

		// Token: 0x04001767 RID: 5991
		private IPageContext pageContext;
	}
}
