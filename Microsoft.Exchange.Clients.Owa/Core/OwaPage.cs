using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200005A RID: 90
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class OwaPage : Page
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00014E0B File Offset: 0x0001300B
		public OwaPage()
		{
			this.owaContext = OwaContext.Current;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00014E2C File Offset: 0x0001302C
		protected static bool IsRtl
		{
			get
			{
				return Microsoft.Exchange.Clients.Owa.Core.Culture.IsRtl;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00014E33 File Offset: 0x00013033
		protected static bool SMimeEnabledPerServer
		{
			get
			{
				return OwaConfigurationManager.Configuration.IsSMimeEnabledOnCurrentServerr;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00014E3F File Offset: 0x0001303F
		public OwaPage(bool setNoCacheNoStore)
		{
			this.setNoCacheNoStore = setNoCacheNoStore;
			this.owaContext = OwaContext.Current;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00014E67 File Offset: 0x00013067
		public TextWriter SanitizingResponse
		{
			get
			{
				return this.owaContext.SanitizingResponseWriter;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00014E74 File Offset: 0x00013074
		public virtual string OwaVersion
		{
			get
			{
				return Globals.ApplicationVersion;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00014E7B File Offset: 0x0001307B
		protected OwaContext OwaContext
		{
			get
			{
				return this.owaContext;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00014E83 File Offset: 0x00013083
		protected UserContext UserContext
		{
			get
			{
				return this.owaContext.UserContext;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00014E90 File Offset: 0x00013090
		protected ISessionContext SessionContext
		{
			get
			{
				return this.owaContext.SessionContext;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00014E9D File Offset: 0x0001309D
		protected bool IsDownLevelClient
		{
			get
			{
				if (this.isDownLevelClient == -1)
				{
					this.isDownLevelClient = (Utilities.IsDownLevelClient(base.Request) ? 1 : 0);
				}
				return this.isDownLevelClient == 1;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00014EC8 File Offset: 0x000130C8
		public string Identity
		{
			get
			{
				return base.GetType().BaseType.Name;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00014EDA File Offset: 0x000130DA
		protected string ExchangePrincipalDisplayName
		{
			get
			{
				return Utilities.GetMailboxOwnerDisplayName(this.owaContext.UserContext.MailboxSession);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00014EF1 File Offset: 0x000130F1
		protected virtual bool UseStrictMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00014EF4 File Offset: 0x000130F4
		protected virtual bool HasFrameset
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00014EF7 File Offset: 0x000130F7
		protected virtual bool IsTextHtml
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00014EFC File Offset: 0x000130FC
		protected void RenderIdentity()
		{
			base.Response.Output.Write("<input type=hidden name=\"");
			base.Response.Output.Write("hidpid");
			base.Response.Output.Write("\" value=\"");
			Utilities.HtmlEncode(this.Identity, base.Response.Output);
			base.Response.Output.Write("\">");
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00014F73 File Offset: 0x00013173
		protected bool IsPostFromMyself()
		{
			return this.IsPostFromPage(this.Identity);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00014F81 File Offset: 0x00013181
		protected bool IsPostFromPage<T>() where T : OwaPage
		{
			return this.IsPostFromPage(typeof(T).Name);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00014F98 File Offset: 0x00013198
		private bool IsPostFromPage(string pageIdentity)
		{
			if (Utilities.IsPostRequest(base.Request))
			{
				string formParameter = Utilities.GetFormParameter(base.Request, "hidpid", false);
				if (formParameter != null && string.CompareOrdinal(formParameter, pageIdentity) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00014FD4 File Offset: 0x000131D4
		public override void ProcessRequest(HttpContext context)
		{
			try
			{
				base.ProcessRequest(context);
			}
			catch (ThreadAbortException)
			{
				OwaContext.Get(context).UnlockMinResourcesOnCriticalError();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00015008 File Offset: 0x00013208
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
			Utilities.HtmlEncode(Globals.CopyrightMessage, base.Response.Output);
			base.Response.Write(" -->");
			base.Response.Write("\n<!-- OwaPage = ");
			Utilities.HtmlEncode(base.GetType().ToString(), base.Response.Output);
			base.Response.Write(" -->\n");
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001510C File Offset: 0x0001330C
		protected override void OnInit(EventArgs e)
		{
			Microsoft.Exchange.Clients.Owa.Core.Culture.SetThreadCulture(this.owaContext);
			if (this.setNoCacheNoStore)
			{
				Utilities.MakePageNoCacheNoStore(base.Response);
			}
			this.EnableViewState = false;
			base.OnInit(e);
			if (!this.owaContext.IsAsyncRequest)
			{
				this.owaContext.IsAsyncRequest = base.AsyncMode;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00015164 File Offset: 0x00013364
		protected override void OnError(EventArgs e)
		{
			Exception lastError = base.Server.GetLastError();
			base.Server.ClearError();
			Utilities.HandleException(this.OwaContext, lastError, true);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00015195 File Offset: 0x00013395
		protected void RenderScriptHandler(string eventName, string handlerCode)
		{
			this.RenderScriptHandler(eventName, handlerCode, false);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000151A0 File Offset: 0x000133A0
		protected void RenderScriptHandler(string eventName, string handlerCode, bool returnFalse)
		{
			Utilities.RenderScriptHandler(base.Response.Output, eventName, handlerCode, returnFalse);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000151B5 File Offset: 0x000133B5
		public void RenderOnClick(string handlerCode)
		{
			this.RenderOnClick(handlerCode, false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000151BF File Offset: 0x000133BF
		public void RenderOnClick(string handlerCode, bool returnFalse)
		{
			Utilities.RenderScriptHandler(this.SanitizingResponse, "onclick", handlerCode, returnFalse);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000151D3 File Offset: 0x000133D3
		protected void RenderInlineScripts()
		{
			Utilities.RenderScriptTagStart(base.Response.Output);
			Utilities.RenderInlineScripts(base.Response.Output, this.SessionContext);
			Utilities.RenderScriptTagEnd(base.Response.Output);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001520B File Offset: 0x0001340B
		protected void RenderExternalScripts(ScriptFlags scriptFlags, IEnumerable<string> fileNames)
		{
			Utilities.RenderExternalScripts(base.Response.Output, scriptFlags, fileNames);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0001521F File Offset: 0x0001341F
		protected void RenderExternalScripts(ScriptFlags scriptFlags, params string[] fileNames)
		{
			Utilities.RenderExternalScripts(base.Response.Output, scriptFlags, (IEnumerable<string>)fileNames);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00015238 File Offset: 0x00013438
		protected void RenderScripts(params string[] fileNames)
		{
			Utilities.RenderScripts(this.SanitizingResponse, this.SessionContext, ScriptFlags.IncludeUglobal, fileNames);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0001524D File Offset: 0x0001344D
		protected bool IsSilverlightEnabled
		{
			get
			{
				return this.UserContext.IsFeatureEnabled(Feature.Silverlight);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00015264 File Offset: 0x00013464
		protected void RenderSilverlight(string rootVisualType, string id, int width, int height, string alternateHtml)
		{
			SanitizedHtmlString value = SanitizedHtmlString.Format("<div style=\"width:{1}px;height:{2}px;\"><object \r\n                id=\"{0}\" \r\n                data=\"data:application/x-silverlight-2,\"\r\n                type=\"application/x-silverlight-2\"\r\n                width=\"{1}\" \r\n                height=\"{2}\"\r\n                ><param name=\"source\" value=\"/OWA/{3}/ClientBin/{4}.xap\"/>\r\n                <param name=\"initParams\" value=\"RootVisualType={5}.{6}\"/>\r\n                <param name=\"onError\" value=\"{7}\" />\r\n                <param name=\"minRuntimeVersion\" value=\"{8}\" />\r\n                <param name=\"autoUpgrade\" value=\"true\" />{9}</object></div>", new object[]
			{
				id,
				width,
				height,
				Globals.ApplicationVersion,
				"OwaSl",
				"Microsoft.Exchange.Clients.Owa.Silverlight",
				rootVisualType,
				"SL_OnPluginError",
				"2.0.31005.0",
				alternateHtml
			});
			base.Response.Output.Write(value);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000152E0 File Offset: 0x000134E0
		protected void RenderSegmentationBitsForJavascript()
		{
			uint[] segmentationBitsForJavascript = this.UserContext.SegmentationBitsForJavascript;
			this.SanitizingResponse.Write("[");
			this.SanitizingResponse.Write(segmentationBitsForJavascript[0]);
			this.SanitizingResponse.Write(", ");
			this.SanitizingResponse.Write(segmentationBitsForJavascript[1]);
			this.SanitizingResponse.Write("]");
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00015345 File Offset: 0x00013545
		protected void RenderEndOfFileDiv()
		{
			this.SanitizingResponse.Write("<div id=divEOF style=display:none></div>");
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00015358 File Offset: 0x00013558
		protected string GetSaveAttachmentToDiskMessage(Strings.IDs attachmentMessageId)
		{
			Strings.IDs localizedId;
			if (this.SessionContext.BrowserType == BrowserType.IE)
			{
				localizedId = 1297545050;
			}
			else if (this.SessionContext.BrowserType == BrowserType.Safari)
			{
				localizedId = 175065296;
			}
			else
			{
				localizedId = -1815684119;
			}
			return string.Format(LocalizedStrings.GetNonEncoded(attachmentMessageId), LocalizedStrings.GetNonEncoded(localizedId));
		}

		// Token: 0x040001A5 RID: 421
		protected const string SilverlightXapName = "OwaSl";

		// Token: 0x040001A6 RID: 422
		protected const string SilverlightRootNamespace = "Microsoft.Exchange.Clients.Owa.Silverlight";

		// Token: 0x040001A7 RID: 423
		protected const string SilverlightPluginErrorHandler = "SL_OnPluginError";

		// Token: 0x040001A8 RID: 424
		private const string PageIdentityHiddenName = "hidpid";

		// Token: 0x040001A9 RID: 425
		private bool setNoCacheNoStore = true;

		// Token: 0x040001AA RID: 426
		private OwaContext owaContext;

		// Token: 0x040001AB RID: 427
		private int isDownLevelClient = -1;
	}
}
