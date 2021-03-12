using System;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000109 RID: 265
	public class Error : OwaPage
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000405A3 File Offset: 0x0003E7A3
		public bool IsUserRtl
		{
			get
			{
				if (base.UserContext != null)
				{
					return base.UserContext.IsRtl;
				}
				return base.OwaContext.Culture != null && base.OwaContext.Culture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000405E0 File Offset: 0x0003E7E0
		protected override void OnLoad(EventArgs e)
		{
			this.errorInformation = base.OwaContext.ErrorInformation;
			if (this.errorInformation == null)
			{
				this.errorInformation = new ErrorInformation();
				this.errorInformation.Message = LocalizedStrings.GetNonEncoded(641346049);
			}
			else if (this.errorInformation.Exception != null)
			{
				Exception ex = this.errorInformation.Exception;
				if (ex is AsyncLocalizedExceptionWrapper)
				{
					ex = AsyncExceptionWrapperHelper.GetRootException(ex);
				}
				try
				{
					base.OwaContext.HttpContext.Response.Headers.Add("X-OWA-Error", ex.GetType().FullName);
				}
				catch (HttpException arg)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Exception happened while trying to append error headers. Exception will be ignored: {0}", arg);
				}
			}
			this.OnInit(e);
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000406A8 File Offset: 0x0003E8A8
		protected bool HasErrorDetails
		{
			get
			{
				return this.errorInformation.MessageDetails != null;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000406BB File Offset: 0x0003E8BB
		protected void RenderIcon()
		{
			ThemeManager.RenderBaseThemeFileUrl(base.Response.Output, this.errorInformation.Icon, false);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000406DC File Offset: 0x0003E8DC
		protected void RenderError()
		{
			if (this.errorInformation.IsErrorMessageHtmlEncoded)
			{
				base.Response.Write(this.errorInformation.Message);
			}
			else
			{
				Utilities.HtmlEncode(this.errorInformation.Message, base.Response.Output);
			}
			if (this.IsPreviousPageLinkEnabled)
			{
				this.RenderBackLink();
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00040738 File Offset: 0x0003E938
		protected void RenderErrorDetails()
		{
			if (!this.errorInformation.IsDetailedErrorHtmlEncoded)
			{
				Utilities.HtmlEncode(this.errorInformation.MessageDetails, base.Response.Output);
				return;
			}
			base.Response.Write(this.errorInformation.MessageDetails);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00040784 File Offset: 0x0003E984
		protected void RenderDebugInformation()
		{
			if (!Globals.ShowDebugInformation)
			{
				return;
			}
			UserContext userContext = null;
			bool flag = false;
			try
			{
				userContext = base.OwaContext.TryGetUserContext();
				if (userContext != null)
				{
					userContext.Lock();
					flag = true;
				}
				Exception ex = this.errorInformation.Exception;
				if (ex == null)
				{
					ex = Globals.InitializationError;
				}
				Utilities.RenderDebugInformation(base.Response.Output, base.OwaContext, ex);
			}
			finally
			{
				if (userContext != null && flag)
				{
					userContext.Unlock();
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00040800 File Offset: 0x0003EA00
		protected bool ShowDebugInformation
		{
			get
			{
				return Globals.ShowDebugInformation && !this.errorInformation.HideDebugInformation;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00040819 File Offset: 0x0003EA19
		protected bool ShowSendReport
		{
			get
			{
				return this.ShowDebugInformation && Globals.EnableEmailReports && !base.IsDownLevelClient && base.SessionContext != null && this.errorInformation.Exception != null;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0004084D File Offset: 0x0003EA4D
		protected bool IsPreviousPageLinkEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.errorInformation.PreviousPageUrl);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00040862 File Offset: 0x0003EA62
		protected bool IsExternalLinkPresent
		{
			get
			{
				return !string.IsNullOrEmpty(this.errorInformation.ExternalPageLink);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00040877 File Offset: 0x0003EA77
		protected ErrorInformation ErrorInformation
		{
			get
			{
				return this.errorInformation;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0004087F File Offset: 0x0003EA7F
		protected void RenderExternalLink()
		{
			base.Response.Write(this.errorInformation.ExternalPageLink);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00040897 File Offset: 0x0003EA97
		protected void RenderBackLink()
		{
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(161749640), "<a href=\"" + this.errorInformation.PreviousPageUrl + "\">", "</a>"));
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000408D2 File Offset: 0x0003EAD2
		protected void RenderBackground()
		{
			ThemeManager.RenderBaseThemeFileUrl(base.Response.Output, this.errorInformation.Background, false);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000408F0 File Offset: 0x0003EAF0
		protected bool RedirectForFailover
		{
			get
			{
				return this.ErrorInformation.OwaEventHandlerErrorCode == OwaEventHandlerErrorCode.MailboxFailoverWithRedirection;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00040904 File Offset: 0x0003EB04
		protected string ResourcePath
		{
			get
			{
				if (this.resourcePath == null)
				{
					if (Globals.OwaVDirType == OWAVDirType.Calendar)
					{
						PublishingUrl publishingUrl = (PublishingUrl)base.OwaContext.HttpContext.Items["AnonymousUserContextPublishedUrl"];
						this.resourcePath = AnonymousSessionContext.GetEscapedPathFromUri(publishingUrl.Uri) + "/";
					}
					else
					{
						this.resourcePath = OwaUrl.ApplicationRoot.ImplicitUrl;
					}
				}
				return this.resourcePath;
			}
		}

		// Token: 0x0400063D RID: 1597
		private ErrorInformation errorInformation;

		// Token: 0x0400063E RID: 1598
		private string resourcePath;
	}
}
