using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200037F RID: 895
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class OwaSubPage : UserControl
	{
		// Token: 0x06002181 RID: 8577 RVA: 0x000BFEE2 File Offset: 0x000BE0E2
		public OwaSubPage()
		{
			this.owaContext = OwaContext.Current;
			this.IsStandalonePage = false;
			this.IsInOEHResponse = false;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000BFF03 File Offset: 0x000BE103
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x000BFF0B File Offset: 0x000BE10B
		public QueryStringParameters QueryStringParameters { get; set; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x000BFF14 File Offset: 0x000BE114
		public TextWriter SanitizingResponse
		{
			get
			{
				return this.owaContext.SanitizingResponseWriter;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000BFF21 File Offset: 0x000BE121
		protected static bool IsRtl
		{
			get
			{
				return Culture.IsRtl;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000BFF28 File Offset: 0x000BE128
		protected OwaContext OwaContext
		{
			get
			{
				return this.owaContext;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x000BFF30 File Offset: 0x000BE130
		protected UserContext UserContext
		{
			get
			{
				return this.owaContext.UserContext;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000BFF3D File Offset: 0x000BE13D
		protected ISessionContext SessionContext
		{
			get
			{
				return this.owaContext.SessionContext;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000BFF4A File Offset: 0x000BE14A
		protected override void OnPreRender(EventArgs e)
		{
			this.SanitizingResponse.Write("\n<!-- OwaSubPage = ");
			Utilities.HtmlEncode(base.GetType().ToString(), base.Response.Output);
			this.SanitizingResponse.Write(" -->\n");
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000BFF87 File Offset: 0x000BE187
		protected void RenderInlineScripts()
		{
			Utilities.RenderInlineScripts(base.Response.Output, this.UserContext);
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000BFFA0 File Offset: 0x000BE1A0
		public void RenderExternalScriptFiles()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.RenderExternalScriptFiles(stringBuilder);
			base.Response.Output.Write(stringBuilder.ToString());
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000BFFD0 File Offset: 0x000BE1D0
		public void RenderExternalScriptFiles(StringBuilder builder)
		{
			builder.Append("<div id=\"divExternalScriptFiles\" class=\"h\">");
			builder.Append("<div>");
			Utilities.HtmlEncode(Utilities.GetScriptFullPath("uglobal.js"), builder);
			builder.Append("</div>");
			foreach (string fileName in this.ExternalScriptFilesIncludeChildSubPages)
			{
				builder.Append("<div>");
				Utilities.HtmlEncode(Utilities.GetScriptFullPath(fileName), builder);
				builder.Append("</div>");
			}
			builder.Append("</div>");
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000C007C File Offset: 0x000BE27C
		protected void RenderVariableDeclarationStart()
		{
			if (this.IsStandalonePage)
			{
				this.SanitizingResponse.Write("<script  type=\"text/javascript\">");
			}
			else
			{
				this.SanitizingResponse.Write("<script type=\"text/javascript\" id=\"divVariableDeclarations\">if(0){");
			}
			this.SanitizingResponse.Write("createGlobalVariables = function $createGlobalVariables(oPage){");
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000C00B8 File Offset: 0x000BE2B8
		protected void RenderVariableDeclarationEnd()
		{
			if (this.IsStandalonePage)
			{
				this.SanitizingResponse.Write("};</script>");
				return;
			}
			this.SanitizingResponse.Write("};}</script>");
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000C00E4 File Offset: 0x000BE2E4
		protected void RenderSegmentationBitsForJavascript()
		{
			uint[] segmentationBitsForJavascript = this.UserContext.SegmentationBitsForJavascript;
			this.SanitizingResponse.Write("[");
			this.SanitizingResponse.Write(segmentationBitsForJavascript[0]);
			this.SanitizingResponse.Write(", ");
			this.SanitizingResponse.Write(segmentationBitsForJavascript[1]);
			this.SanitizingResponse.Write("]");
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x000C0149 File Offset: 0x000BE349
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x000C0151 File Offset: 0x000BE351
		internal bool IsStandalonePage { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000C015A File Offset: 0x000BE35A
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x000C0162 File Offset: 0x000BE362
		internal bool IsInOEHResponse { get; set; }

		// Token: 0x06002194 RID: 8596 RVA: 0x000C016C File Offset: 0x000BE36C
		protected string GetParameter(string name, bool isParameterRequired)
		{
			if (this.QueryStringParameters == null)
			{
				return Utilities.GetQueryStringParameter(HttpContext.Current.Request, name, isParameterRequired);
			}
			string value = this.QueryStringParameters.GetValue(name);
			if (value == null && isParameterRequired)
			{
				throw new OwaInvalidRequestException(string.Format("Required URL parameter missing: {0}", name));
			}
			return value;
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000C01BA File Offset: 0x000BE3BA
		protected List<OwaSubPage> ChildSubPages
		{
			get
			{
				if (this.childSubPages == null)
				{
					this.childSubPages = new List<OwaSubPage>();
				}
				return this.childSubPages;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002196 RID: 8598
		public abstract IEnumerable<string> ExternalScriptFiles { get; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000C04B4 File Offset: 0x000BE6B4
		public IEnumerable<string> ExternalScriptFilesIncludeChildSubPages
		{
			get
			{
				foreach (string scriptFile in this.ExternalScriptFiles)
				{
					yield return scriptFile;
				}
				foreach (OwaSubPage owaSubPage in this.ChildSubPages)
				{
					foreach (string scriptFile2 in owaSubPage.ExternalScriptFilesIncludeChildSubPages)
					{
						yield return scriptFile2;
					}
				}
				yield break;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002198 RID: 8600
		public abstract SanitizedHtmlString Title { get; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002199 RID: 8601
		public abstract string PageType { get; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000C04D1 File Offset: 0x000BE6D1
		public virtual bool SupportIM
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000C04D4 File Offset: 0x000BE6D4
		public virtual string BodyCssClass
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x000C04DB File Offset: 0x000BE6DB
		public virtual string HtmlAdditionalAttributes
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x040017C7 RID: 6087
		private OwaContext owaContext;

		// Token: 0x040017C8 RID: 6088
		private List<OwaSubPage> childSubPages;
	}
}
