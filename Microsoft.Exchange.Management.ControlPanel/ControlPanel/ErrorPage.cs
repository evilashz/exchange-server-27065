using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D4 RID: 468
	public class ErrorPage : EcpPage
	{
		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x00072708 File Offset: 0x00070908
		protected bool ShowDebugInformation
		{
			get
			{
				return !string.IsNullOrEmpty(this.debugInformation);
			}
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x00072718 File Offset: 0x00070918
		protected bool ShowDiagnosticInformation
		{
			get
			{
				return Datacenter.GetExchangeSku() == Datacenter.ExchangeSku.ExchangeDatacenter;
			}
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x00072722 File Offset: 0x00070922
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x0007272A File Offset: 0x0007092A
		private protected string SignOutReturnVdir { protected get; private set; }

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00072733 File Offset: 0x00070933
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x0007273B File Offset: 0x0007093B
		private protected bool ShowSignOutLink { protected get; private set; }

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00072744 File Offset: 0x00070944
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x0007274C File Offset: 0x0007094C
		private protected bool ShowSignOutHint { protected get; private set; }

		// Token: 0x06002568 RID: 9576 RVA: 0x00072758 File Offset: 0x00070958
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.EnsureChildControls();
			if (HttpContext.Current.Error != null)
			{
				this.debugInformation = HttpContext.Current.Error.ToTraceString();
				HttpContext.Current.ClearError();
			}
			if (HttpContext.Current.Request.ServerVariables["X-ECP-ERROR"] != null)
			{
				HttpContext.Current.Response.AddHeader("X-ECP-ERROR", HttpContext.Current.Request.ServerVariables["X-ECP-ERROR"]);
			}
			string text = base.Request.QueryString["cause"] ?? "unexpected";
			ErrorPageContents contentsForErrorType = ErrorPageContents.GetContentsForErrorType(text);
			string text2 = null;
			if (text == "browsernotsupported")
			{
				string helpId = EACHelpId.BrowserNotSupportedHelp.ToString();
				IThemable themable = this.Page as IThemable;
				if (themable != null && themable.FeatureSet == FeatureSet.Options)
				{
					helpId = OptionsHelpId.OwaOptionsBrowserNotSupportedHelp.ToString();
				}
				text2 = string.Format(contentsForErrorType.ErrorMessageText, HelpUtil.BuildEhcHref(helpId));
			}
			else if (text == "nocookies")
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (browser != null && browser.IsBrowser("IE"))
				{
					text2 = string.Format(Strings.CookiesDisabledMessageForIE, HelpUtil.BuildEhcHref(EACHelpId.CookiesDisabledMessageForIE.ToString()));
				}
			}
			else if (text == "liveidmismatch")
			{
				string value = HttpContextExtensions.CurrentUserLiveID();
				if (string.IsNullOrEmpty(value))
				{
					contentsForErrorType = ErrorPageContents.GetContentsForErrorType("unexpected");
				}
				else
				{
					string arg = EcpUrl.EcpVDir + "logoff.aspx?src=exch&ru=" + HttpUtility.UrlEncode(HttpContext.Current.GetRequestUrl().OriginalString);
					text2 = string.Format(contentsForErrorType.ErrorMessageText, HttpContextExtensions.CurrentUserLiveID(), arg);
				}
			}
			else if (text == "verificationfailed")
			{
				text2 = string.Format(contentsForErrorType.ErrorMessageText, EcpUrl.EcpVDir);
			}
			else if (text == "verificationprocessingerror")
			{
				text2 = contentsForErrorType.ErrorMessageText;
			}
			else if (text == "noroles")
			{
				this.ShowSignOutHint = true;
				this.ShowSignOutLink = true;
				this.SignOutReturnVdir = "/ecp/";
			}
			else if (text == "cannotaccessoptionswithbeparamorcookie")
			{
				this.ShowSignOutLink = true;
				this.SignOutReturnVdir = "/owa/";
			}
			if (string.IsNullOrEmpty(text2))
			{
				this.msgText.Text = contentsForErrorType.ErrorMessageText;
			}
			else
			{
				this.msgText.Text = text2;
			}
			base.Title = contentsForErrorType.PageTitle;
			this.msgTitle.Text = Strings.ErrorTitle(contentsForErrorType.ErrorMessageTitle);
			this.msgCode.Text = ((int)contentsForErrorType.StatusCode).ToString(CultureInfo.InvariantCulture);
			HttpContext.Current.Response.StatusCode = (int)contentsForErrorType.StatusCode;
			HttpContext.Current.Response.SubStatusCode = contentsForErrorType.SubStatusCode;
			HttpContext.Current.Response.TrySkipIisCustomErrors = true;
			this.causeMarker.Text = "<!-- cause:" + contentsForErrorType.CauseMarker + " -->";
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x00072A92 File Offset: 0x00070C92
		protected void RenderDebugInformation()
		{
			base.Response.Write(HttpUtility.HtmlEncode(this.debugInformation).Replace("&#13;&#10;", "<br />"));
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00072ABC File Offset: 0x00070CBC
		protected void RenderDiagnosticInformation()
		{
			try
			{
				base.Response.Write("<div class=\"errLbl\">");
				base.Response.Write(Strings.UserEmailAddress);
				base.Response.Write("</div><div>");
				base.Response.Write(HttpUtility.HtmlEncode(HttpContextExtensions.CurrentUserLiveID()));
				base.Response.Write("</div>");
				string s = ActivityContext.ActivityId.FormatForLog();
				base.Response.Write("<div class=\"errLbl\">");
				base.Response.Write(Strings.CorrelationId);
				base.Response.Write("</div><div>");
				base.Response.Write(HttpUtility.HtmlEncode(s));
				base.Response.Write("</div>");
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 230, "RenderDiagnosticInformation", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\error.aspx.cs");
				Server server = topologyConfigurationSession.FindLocalServer();
				base.Response.Write("<div class=\"errLbl\">");
				base.Response.Write(Strings.ClientAccessServerName);
				base.Response.Write("</div><div>");
				base.Response.Write(HttpUtility.HtmlEncode(server.Fqdn));
				base.Response.Write("</div>");
				base.Response.Write("<div class=\"errLbl\">");
				base.Response.Write(Strings.ClientAccessServerVersion);
				base.Response.Write("</div><div>");
				base.Response.Write(HttpUtility.HtmlEncode(Globals.ApplicationVersion));
				base.Response.Write("</div>");
				base.Response.Write("<div class=\"errLbl\">");
				base.Response.Write(Strings.UTCTime);
				base.Response.Write("</div><div>");
				base.Response.Write(HttpUtility.HtmlEncode(DateTime.UtcNow.ToString("o")));
				base.Response.Write("</div>");
			}
			catch (Exception exception)
			{
				ExTraceGlobals.EventLogTracer.TraceError<EcpTraceFormatter<Exception>>(0, 0L, "Application Error: {0}", exception.GetTraceFormatter());
				EcpPerfCounters.AspNetErrors.Increment();
				throw;
			}
		}

		// Token: 0x04001ED3 RID: 7891
		protected Label msgCode;

		// Token: 0x04001ED4 RID: 7892
		protected Label msgTitle;

		// Token: 0x04001ED5 RID: 7893
		protected Literal msgText;

		// Token: 0x04001ED6 RID: 7894
		protected Literal causeMarker;

		// Token: 0x04001ED7 RID: 7895
		private string debugInformation;
	}
}
