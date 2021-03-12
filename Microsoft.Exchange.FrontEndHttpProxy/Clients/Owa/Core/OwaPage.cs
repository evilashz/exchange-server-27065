using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.HttpProxy;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200004F RID: 79
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class OwaPage : Page
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000D760 File Offset: 0x0000B960
		public OwaPage()
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D776 File Offset: 0x0000B976
		public OwaPage(bool setNoCacheNoStore)
		{
			this.setNoCacheNoStore = setNoCacheNoStore;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000D794 File Offset: 0x0000B994
		public UserAgent UserAgent
		{
			get
			{
				if (this.userAgent == null)
				{
					UserAgent userAgent = new UserAgent(base.Request.UserAgent, false, base.Request.Cookies);
					if (base.Request.QueryString != null)
					{
						string text = base.Request.QueryString["layout"];
						if (text != null)
						{
							userAgent.SetLayoutFromString(text);
						}
						else
						{
							string text2 = base.Request.QueryString["url"];
							if (text2 != null)
							{
								int num = text2.IndexOf('?');
								if (num >= 0 && num < text2.Length - 1)
								{
									string query = text2.Substring(num + 1);
									NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
									text = nameValueCollection["layout"];
									if (text != null)
									{
										userAgent.SetLayoutFromString(text);
									}
								}
							}
						}
					}
					this.userAgent = userAgent;
				}
				return this.userAgent;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000D861 File Offset: 0x0000BA61
		public string Identity
		{
			get
			{
				return base.GetType().BaseType.Name;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000D873 File Offset: 0x0000BA73
		protected static bool IsRtl
		{
			get
			{
				return Microsoft.Exchange.Clients.Owa.Core.Culture.IsRtl;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000D87A File Offset: 0x0000BA7A
		protected static bool SMimeEnabledPerServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000D87D File Offset: 0x0000BA7D
		protected bool IsDownLevelClient
		{
			get
			{
				if (this.isDownLevelClient == -1)
				{
					this.isDownLevelClient = (base.Request.IsDownLevelClient() ? 1 : 0);
				}
				return this.isDownLevelClient == 1;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		protected virtual bool UseStrictMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000D8AB File Offset: 0x0000BAAB
		protected virtual bool HasFrameset
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		protected virtual bool IsTextHtml
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		public static bool IsPalEnabled(HttpContext context)
		{
			if (context.Request != null && context.Request.Cookies != null && context.Request.Cookies["PALEnabled"] != null)
			{
				return context.Request.Cookies["PALEnabled"].Value != "-1";
			}
			return context.Request.QueryString["palenabled"] == "1" || (context.Request.UserAgent != null && context.Request.UserAgent.Contains("MSAppHost"));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D95C File Offset: 0x0000BB5C
		public string GetDefaultCultureCssFontFileName()
		{
			CultureInfo userCulture = Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture();
			return Microsoft.Exchange.Clients.Owa.Core.Culture.GetCssFontFileNameFromCulture(userCulture);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D978 File Offset: 0x0000BB78
		protected override void InitializeCulture()
		{
			CultureInfo cultureInfo = Microsoft.Exchange.Clients.Owa.Core.Culture.GetBrowserDefaultCulture(base.Request);
			if (cultureInfo == null && OwaVdirConfiguration.Instance.LogonAndErrorLanguage > 0)
			{
				try
				{
					cultureInfo = CultureInfo.GetCultureInfo(OwaVdirConfiguration.Instance.LogonAndErrorLanguage);
				}
				catch (CultureNotFoundException)
				{
					cultureInfo = null;
				}
			}
			if (cultureInfo != null)
			{
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
				Thread.CurrentThread.CurrentCulture = cultureInfo;
			}
			base.InitializeCulture();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		protected void RenderIdentity()
		{
			base.Response.Output.Write("<input type=hidden name=\"");
			base.Response.Output.Write("hidpid");
			base.Response.Output.Write("\" value=\"");
			EncodingUtilities.HtmlEncode(this.Identity, base.Response.Output);
			base.Response.Output.Write("\">");
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DA60 File Offset: 0x0000BC60
		protected override void OnPreRender(EventArgs e)
		{
			if (this.HasFrameset)
			{
				base.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">");
				base.Response.Write("\n");
			}
			else if (this.UseStrictMode && this.IsTextHtml)
			{
				base.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">");
				base.Response.Write("\n");
			}
			else if (this.IsTextHtml)
			{
				base.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
				base.Response.Write("\n");
			}
			base.Response.Write("<!-- ");
			EncodingUtilities.HtmlEncode(OwaPage.CopyrightMessage, base.Response.Output);
			base.Response.Write(" -->");
			base.Response.Write("\n<!-- OwaPage = ");
			EncodingUtilities.HtmlEncode(base.GetType().ToString(), base.Response.Output);
			base.Response.Write(" -->\n");
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DB61 File Offset: 0x0000BD61
		protected override void OnInit(EventArgs e)
		{
			if (this.setNoCacheNoStore)
			{
				AspNetHelper.MakePageNoCacheNoStore(base.Response);
			}
			this.EnableViewState = false;
			base.OnInit(e);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DB84 File Offset: 0x0000BD84
		protected string GetNoScriptHtml()
		{
			string htmlEncoded = LocalizedStrings.GetHtmlEncoded(719849305);
			return string.Format(htmlEncoded, "<a href=\"http://www.microsoft.com/windows/ie/downloads/default.mspx\">", "</a>");
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000DBC3 File Offset: 0x0000BDC3
		protected string InlineJavascript(string fileName)
		{
			return this.InlineResource(fileName, "scripts\\premium\\", (string fullFilePath) => "<script>" + File.ReadAllText(fullFilePath) + "</script>", OwaPage.inlineScripts);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DC24 File Offset: 0x0000BE24
		protected string InlineImage(ThemeFileId themeFileId)
		{
			string fileName = ThemeFileList.GetNameFromId(themeFileId);
			return this.InlineResource(fileName, "themes\\resources", (string fullFilePath) => "data:" + MimeMapping.GetMimeMapping(fileName) + ";base64," + Convert.ToBase64String(File.ReadAllBytes(fullFilePath)), OwaPage.inlineImages);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DC68 File Offset: 0x0000BE68
		protected string InlineCss(ThemeFileId themeFileId)
		{
			string nameFromId = ThemeFileList.GetNameFromId(themeFileId);
			return this.InlineCss(nameFromId);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		protected string InlineCss(string fileName)
		{
			return this.InlineResource(fileName, "themes\\resources", (string fullFilePath) => "<style>" + File.ReadAllText(fullFilePath) + "</style>", OwaPage.inlineStyles);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000DCCC File Offset: 0x0000BECC
		private string InlineResource(string fileName, string partialFileLocation, OwaPage.ResoruceCreator createResource, Dictionary<string, Tuple<string, DateTime>> resourceDictionary)
		{
			string text = HttpRuntime.AppDomainAppPath.ToLower();
			if (text.EndsWith("ecp\\"))
			{
				text = text.Replace("ecp\\", "owa\\");
			}
			string text2 = Path.Combine(text, "auth\\" + ProxyApplication.ApplicationVersion, partialFileLocation, fileName);
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(text2);
			Tuple<string, DateTime> tuple;
			lock (resourceDictionary)
			{
				if (!resourceDictionary.TryGetValue(text2, out tuple) || tuple.Item2 < lastWriteTimeUtc)
				{
					tuple = Tuple.Create<string, DateTime>(createResource(text2), lastWriteTimeUtc);
					resourceDictionary[text2] = tuple;
				}
			}
			return tuple.Item1;
		}

		// Token: 0x04000139 RID: 313
		protected const string SilverlightXapName = "OwaSl";

		// Token: 0x0400013A RID: 314
		protected const string SilverlightRootNamespace = "Microsoft.Exchange.Clients.Owa.Silverlight";

		// Token: 0x0400013B RID: 315
		protected const string SilverlightPluginErrorHandler = "SL_OnPluginError";

		// Token: 0x0400013C RID: 316
		protected const string PALEnabledCookieName = "PALEnabled";

		// Token: 0x0400013D RID: 317
		protected const string LoadFailedCookieName = "loadFailed";

		// Token: 0x0400013E RID: 318
		private const string PageIdentityHiddenName = "hidpid";

		// Token: 0x0400013F RID: 319
		private const string LayoutParamName = "layout";

		// Token: 0x04000140 RID: 320
		private const string ScriptsPath = "scripts\\premium\\";

		// Token: 0x04000141 RID: 321
		private const string ResourcesPath = "themes\\resources";

		// Token: 0x04000142 RID: 322
		private const string OwaVDir = "owa\\";

		// Token: 0x04000143 RID: 323
		private const string EcpVDir = "ecp\\";

		// Token: 0x04000144 RID: 324
		public static readonly string CopyrightMessage = "Copyright (c) 2011 Microsoft Corporation.  All rights reserved.";

		// Token: 0x04000145 RID: 325
		public static readonly string SupportedBrowserHelpUrl = "http://office.com/redir/HA102824601.aspx";

		// Token: 0x04000146 RID: 326
		private static Dictionary<string, Tuple<string, DateTime>> inlineScripts = new Dictionary<string, Tuple<string, DateTime>>();

		// Token: 0x04000147 RID: 327
		private static Dictionary<string, Tuple<string, DateTime>> inlineImages = new Dictionary<string, Tuple<string, DateTime>>();

		// Token: 0x04000148 RID: 328
		private static Dictionary<string, Tuple<string, DateTime>> inlineStyles = new Dictionary<string, Tuple<string, DateTime>>();

		// Token: 0x04000149 RID: 329
		private bool setNoCacheNoStore = true;

		// Token: 0x0400014A RID: 330
		private int isDownLevelClient = -1;

		// Token: 0x0400014B RID: 331
		private UserAgent userAgent;

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x0600026D RID: 621
		private delegate string ResoruceCreator(string fullFileName);
	}
}
