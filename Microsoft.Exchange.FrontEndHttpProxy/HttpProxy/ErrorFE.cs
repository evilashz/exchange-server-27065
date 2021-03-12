using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000051 RID: 81
	public class ErrorFE : OwaPage
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		protected bool HasErrorDetails
		{
			get
			{
				return this.errorInformation.MessageDetails != null;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000DDCF File Offset: 0x0000BFCF
		protected bool IsPreviousPageLinkEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.errorInformation.PreviousPageUrl);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		protected bool IsExternalLinkPresent
		{
			get
			{
				return !string.IsNullOrEmpty(this.errorInformation.ExternalPageLink);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		protected bool IsOfflineEnabledClient
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.Request != null)
				{
					HttpCookie httpCookie = HttpContext.Current.Request.Cookies.Get("offline");
					if (httpCookie != null && httpCookie.Value == "1")
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000DE4E File Offset: 0x0000C04E
		protected bool RenderAddToFavoritesButton
		{
			get
			{
				return !string.IsNullOrEmpty(this.errorInformation.RedirectionUrl) && this.isIE;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000DE6A File Offset: 0x0000C06A
		protected Microsoft.Exchange.Clients.Owa.Core.ErrorInformation ErrorInformation
		{
			get
			{
				return this.errorInformation;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000DE72 File Offset: 0x0000C072
		protected bool RenderDiagnosticInfo
		{
			get
			{
				return this.renderDiagnosticInfo;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000DE7A File Offset: 0x0000C07A
		protected string DiagnosticInfo
		{
			get
			{
				return this.diagnosticInfo;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000DE82 File Offset: 0x0000C082
		protected string ResourcePath
		{
			get
			{
				if (this.resourcePath == null)
				{
					this.resourcePath = OwaUrl.AuthFolder.ImplicitUrl;
				}
				return this.resourcePath;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		protected string LoadFailedMessageValue
		{
			get
			{
				string text = this.errorInformation.HttpCode.ToString();
				if (this.errorInformation.MessageDetails != null)
				{
					text = text + ":" + this.errorInformation.MessageDetails;
				}
				return text;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		protected override void OnLoad(EventArgs e)
		{
			ErrorFE.FEErrorCodes feerrorCodes = ErrorFE.FEErrorCodes.Unknown;
			string text = null;
			if (HttpContext.Current != null && HttpContext.Current.Items != null)
			{
				if (HttpContext.Current.Items.Contains("CafeError"))
				{
					feerrorCodes = (ErrorFE.FEErrorCodes)HttpContext.Current.Items["CafeError"];
				}
				if (HttpContext.Current.Items.Contains("redirectUrl"))
				{
					text = (HttpContext.Current.Items["redirectUrl"] as string);
				}
			}
			string s;
			int httpCode;
			if (string.IsNullOrEmpty(s = HttpContext.Current.Request.QueryString["httpCode"]) || !int.TryParse(s, out httpCode))
			{
				if (!string.IsNullOrEmpty(text))
				{
					httpCode = 302;
				}
				else
				{
					httpCode = 500;
				}
			}
			bool sharePointApp;
			if (!bool.TryParse(HttpContext.Current.Request.QueryString["sharepointapp"], out sharePointApp))
			{
				sharePointApp = false;
			}
			bool siteMailbox;
			if (!bool.TryParse(HttpContext.Current.Request.QueryString["sm"], out siteMailbox))
			{
				siteMailbox = false;
			}
			ErrorMode value;
			if (!Enum.TryParse<ErrorMode>(HttpContext.Current.Request.QueryString["m"], out value))
			{
				value = ErrorMode.Frowny;
			}
			string groupMailboxDestination = HttpContext.Current.Request.QueryString["gm"];
			this.errorInformation = new Microsoft.Exchange.Clients.Owa.Core.ErrorInformation(httpCode, feerrorCodes.ToString(), sharePointApp);
			this.errorInformation.SiteMailbox = siteMailbox;
			this.errorInformation.GroupMailboxDestination = groupMailboxDestination;
			this.errorInformation.RedirectionUrl = text;
			this.errorInformation.ErrorMode = new ErrorMode?(value);
			this.isIE = (BrowserType.IE == Utilities.GetBrowserType(this.Context.Request.UserAgent));
			this.CompileDiagnosticInfo();
			this.AddDiagnosticsHeaders();
			this.OnInit(e);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		protected void RenderTitle()
		{
			if (this.errorInformation.ErrorMode == ErrorMode.MailboxNotReady)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-594631022));
				return;
			}
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(933672694));
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E11A File Offset: 0x0000C31A
		protected void RenderIcon()
		{
			ThemeManager.RenderBaseThemeFileUrl(base.Response.Output, this.errorInformation.Icon, false);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E138 File Offset: 0x0000C338
		protected void AddDiagnosticsHeaders()
		{
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["owaError"]))
			{
				base.Response.AddHeader("X-OWA-Error", HttpContext.Current.Request.QueryString["owaError"]);
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["authError"]))
			{
				base.Response.AddHeader("X-Auth-Error", HttpContext.Current.Request.QueryString["authError"]);
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["owaVer"]))
			{
				base.Response.AddHeader("X-OWA-Version", HttpContext.Current.Request.QueryString["owaVer"]);
			}
			if (!string.IsNullOrWhiteSpace(Environment.MachineName))
			{
				base.Response.AddHeader(WellKnownHeader.XFEServer, Environment.MachineName);
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["be"]))
			{
				base.Response.AddHeader(WellKnownHeader.XBEServer, HttpContext.Current.Request.QueryString["be"]);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E28C File Offset: 0x0000C48C
		protected void CompileDiagnosticInfo()
		{
			this.renderDiagnosticInfo = false;
			StringBuilder stringBuilder = new StringBuilder();
			if (HttpContext.Current.Request.Cookies["ClientId"] != null)
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("X-ClientId: ");
				string value = HttpContext.Current.Request.Cookies["ClientId"].Value;
				string text = ClientIdCookie.ParseToPrintableString(value);
				stringBuilder.Append(text.ToUpperInvariant());
				stringBuilder.Append("\n");
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["owaError"]))
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("X-OWA-Error: ");
				stringBuilder.Append(HttpContext.Current.Request.QueryString["owaError"]);
				stringBuilder.Append("\n");
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["authError"]))
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("X-Auth-Error: ");
				stringBuilder.Append(HttpContext.Current.Request.QueryString["authError"]);
				stringBuilder.Append("\n");
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["owaVer"]))
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("X-OWA-Version: ");
				stringBuilder.Append(HttpContext.Current.Request.QueryString["owaVer"]);
				stringBuilder.Append("\n");
			}
			if (!string.IsNullOrWhiteSpace(Environment.MachineName))
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("X-FEServer: ");
				stringBuilder.Append(Environment.MachineName);
				stringBuilder.Append("\n");
			}
			if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["be"]))
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("X-BEServer: ");
				stringBuilder.Append(HttpContext.Current.Request.QueryString["be"]);
				stringBuilder.Append("\n");
			}
			long fileTime;
			if (long.TryParse(HttpContext.Current.Request.QueryString["ts"], out fileTime))
			{
				this.renderDiagnosticInfo = true;
				stringBuilder.Append("Date: ");
				stringBuilder.Append(DateTime.FromFileTimeUtc(fileTime).ToString());
				stringBuilder.Append("\n");
			}
			else if (this.renderDiagnosticInfo)
			{
				stringBuilder.Append("Date: ");
				stringBuilder.Append(DateTime.UtcNow.ToString());
				stringBuilder.Append("\n");
			}
			this.diagnosticInfo = stringBuilder.ToString();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E56C File Offset: 0x0000C76C
		protected void RenderErrorHeader()
		{
			if (!this.errorInformation.SharePointApp)
			{
				if (this.errorInformation.GroupMailbox)
				{
					return;
				}
				if (this.errorInformation.ErrorMode == ErrorMode.MailboxNotReady)
				{
					return;
				}
				if (this.errorInformation.HttpCode == 404)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(-392503097));
					return;
				}
				if (this.errorInformation.HttpCode == 302)
				{
					return;
				}
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(629133816));
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E60C File Offset: 0x0000C80C
		protected void RenderErrorSubHeader()
		{
			if (this.errorInformation.SharePointApp)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(735230835));
				return;
			}
			if (this.errorInformation.GroupMailbox)
			{
				if (this.errorInformation.GroupMailboxDestination == "conv")
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(-526376074));
					return;
				}
				if (this.errorInformation.GroupMailboxDestination == "cal")
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(1147057944));
					return;
				}
			}
			else
			{
				if (this.errorInformation.ErrorMode == ErrorMode.MailboxNotReady)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(-146632527));
					return;
				}
				if (this.errorInformation.ErrorMode == ErrorMode.MailboxSoftDeleted)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1935911806));
					return;
				}
				if (this.errorInformation.ErrorMode == ErrorMode.AccountDisabled)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(425733410));
					return;
				}
				if (this.errorInformation.ErrorMode == ErrorMode.SharedMailboxAccountDisabled)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(-432125413));
					return;
				}
				if (this.errorInformation.HttpCode == 404)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(1252002283));
					return;
				}
				if (this.errorInformation.HttpCode == 503)
				{
					base.Response.Write(LocalizedStrings.GetHtmlEncoded(1252002321));
					return;
				}
				if (this.errorInformation.HttpCode == 302)
				{
					return;
				}
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(1252002318));
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		protected void RenderErrorSubHeader2()
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E800 File Offset: 0x0000CA00
		protected void RenderRefreshButtonText()
		{
			if (this.errorInformation.ErrorMode == ErrorMode.MailboxNotReady)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(867248262));
				return;
			}
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(1939504838));
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E85C File Offset: 0x0000CA5C
		protected void RenderErrorDetails()
		{
			if (!this.errorInformation.GroupMailbox)
			{
				Strings.IDs ds;
				if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.QueryString["msg"] != null && Enum.TryParse<Strings.IDs>(HttpContext.Current.Request.QueryString["msg"], out ds))
				{
					string text = ErrorFE.SafeErrorMessagesNoHtmlEncoding.Contains(ds) ? Strings.GetLocalizedString(ds) : LocalizedStrings.GetHtmlEncoded(ds);
					List<string> list = Microsoft.Exchange.Clients.Common.ErrorInformation.ParseMessageParameters(text, HttpContext.Current.Request);
					if (list != null && list.Count > 0)
					{
						for (int i = 0; i < list.Count; i++)
						{
							list[i] = EncodingUtilities.HtmlEncode(list[i]);
						}
						if (ErrorFE.MessagesToRenderLogoutLinks.Contains(ds) || ErrorFE.MessagesToRenderLoginLinks.Contains(ds))
						{
							ErrorFE.AddSafeLinkToMessageParametersList(ds, HttpContext.Current.Request, ref list);
						}
						base.Response.Write(string.Format(text, list.ToArray()));
						return;
					}
					if (!ErrorFE.MessagesToRenderLogoutLinks.Contains(ds) && !ErrorFE.MessagesToRenderLoginLinks.Contains(ds))
					{
						base.Response.Write(text);
						return;
					}
					list = new List<string>();
					ErrorFE.AddSafeLinkToMessageParametersList(ds, HttpContext.Current.Request, ref list);
					if (list.Count > 0)
					{
						base.Response.Write(string.Format(text, list.ToArray()));
						return;
					}
				}
				else
				{
					if (this.errorInformation.HttpCode == 404)
					{
						base.Response.Write(LocalizedStrings.GetHtmlEncoded(236137810));
						return;
					}
					if (this.errorInformation.HttpCode == 302)
					{
						LegacyRedirectTypeOptions? legacyRedirectTypeOptions = HttpContext.Current.Items["redirectType"] as LegacyRedirectTypeOptions?;
						if (legacyRedirectTypeOptions == null || legacyRedirectTypeOptions != LegacyRedirectTypeOptions.Manual)
						{
							base.Response.Redirect(this.errorInformation.RedirectionUrl);
							return;
						}
						base.Response.Write(LocalizedStrings.GetHtmlEncoded(967320822));
						base.Response.Write("<br/>");
						base.Response.Write(string.Format("<a href=\"{0}\">{0}</a>", this.errorInformation.RedirectionUrl));
						base.Response.Headers.Add("X-OWA-FEError", ErrorFE.FEErrorCodes.CasRedirect.ToString());
						return;
					}
					else
					{
						base.Response.Write(LocalizedStrings.GetHtmlEncoded(236137783));
					}
				}
				return;
			}
			if (this.errorInformation.GroupMailboxDestination == "conv")
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-364732161));
				return;
			}
			if (this.errorInformation.GroupMailboxDestination == "cal")
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-292781713));
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000EB45 File Offset: 0x0000CD45
		protected void RenderOfflineInfo()
		{
			if (!this.IsOfflineEnabledClient)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1051316555));
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000EB64 File Offset: 0x0000CD64
		protected void RenderOfflineDetails()
		{
			if (!this.IsOfflineEnabledClient)
			{
				string str;
				using (StringWriter stringWriter = new StringWriter())
				{
					ThemeManager.RenderBaseThemeFileUrl(stringWriter, ThemeFileId.OwaSettings, false);
					str = stringWriter.ToString();
				}
				string s = string.Format(LocalizedStrings.GetHtmlEncoded(510910463), "<img src=\"" + OwaUrl.AuthFolder.ImplicitUrl + str + "\"/>");
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(107625936));
				base.Response.Write("<br/>");
				base.Response.Write(s);
				base.Response.Write("<br/>");
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1055173478));
				base.Response.Write("<br/>");
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-295658591));
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EC54 File Offset: 0x0000CE54
		protected void RenderExternalLink()
		{
			base.Response.Write(this.errorInformation.ExternalPageLink);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		protected void RenderBackLink()
		{
			base.Response.Write(string.Format(LocalizedStrings.GetHtmlEncoded(161749640), "<a href=\"" + this.errorInformation.PreviousPageUrl + "\">", "</a>"));
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000ECA7 File Offset: 0x0000CEA7
		protected void RenderBackground()
		{
			ThemeManager.RenderBaseThemeFileUrl(base.Response.Output, this.errorInformation.Background, false);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		private static string GetLocalizedLiveIdSignoutLinkMessage(HttpRequest request)
		{
			string explicitUrl = OwaUrl.Logoff.GetExplicitUrl(request);
			return "<BR><BR>" + string.Format(CultureInfo.InvariantCulture, Strings.LogonErrorLogoutUrlText, new object[]
			{
				explicitUrl
			});
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000ED08 File Offset: 0x0000CF08
		private static void AddSafeLinkToMessageParametersList(Strings.IDs messageId, HttpRequest request, ref List<string> messageParameters)
		{
			string item = string.Empty;
			string realm = string.Empty;
			if (ErrorFE.MessagesToRenderLogoutLinks.Contains(messageId))
			{
				item = ErrorFE.GetLocalizedLiveIdSignoutLinkMessage(request);
				messageParameters.Insert(0, item);
				return;
			}
			if (ErrorFE.MessagesToRenderLoginLinks.Contains(messageId))
			{
				string dnsSafeHost = request.Url.DnsSafeHost;
				if (messageParameters != null && messageParameters.Count > 0)
				{
					realm = messageParameters[0];
				}
				item = Utilities.GetAccessURLFromHostnameAndRealm(dnsSafeHost, realm, false);
				messageParameters.Insert(0, item);
				messageParameters.Remove(dnsSafeHost);
			}
		}

		// Token: 0x0400014E RID: 334
		internal const string RedirectUrl = "redirectUrl";

		// Token: 0x0400014F RID: 335
		internal const string RedirectType = "redirectType";

		// Token: 0x04000150 RID: 336
		private const string CafeErrorKey = "CafeError";

		// Token: 0x04000151 RID: 337
		private const string HttpCodeQueryKey = "httpCode";

		// Token: 0x04000152 RID: 338
		private const string ErrorMessageQueryKey = "msg";

		// Token: 0x04000153 RID: 339
		private const string ErrorMessageParameterQueryKey = "msgParam";

		// Token: 0x04000154 RID: 340
		private const string SharePointAppQueryKey = "sharepointapp";

		// Token: 0x04000155 RID: 341
		private const string SiteMailboxQueryKey = "sm";

		// Token: 0x04000156 RID: 342
		private const string GroupMailboxQueryKey = "gm";

		// Token: 0x04000157 RID: 343
		private const string ConversationsDestination = "conv";

		// Token: 0x04000158 RID: 344
		private const string CalendarDestination = "cal";

		// Token: 0x04000159 RID: 345
		private const string OfflineEnabledParameterName = "offline";

		// Token: 0x0400015A RID: 346
		private static readonly Strings.IDs[] SafeErrorMessagesNoHtmlEncoding = new Strings.IDs[]
		{
			1799660809,
			-1420330575,
			-870357301,
			637041586,
			-106213327,
			-2011393914,
			1317300008
		};

		// Token: 0x0400015B RID: 347
		private static readonly Strings.IDs[] MessagesToRenderLogoutLinks = new Strings.IDs[]
		{
			1799660809,
			-1420330575,
			-870357301,
			637041586,
			-106213327,
			-2011393914,
			1753500428
		};

		// Token: 0x0400015C RID: 348
		private static readonly Strings.IDs[] MessagesToRenderLoginLinks = new Strings.IDs[]
		{
			1317300008
		};

		// Token: 0x0400015D RID: 349
		private Microsoft.Exchange.Clients.Owa.Core.ErrorInformation errorInformation;

		// Token: 0x0400015E RID: 350
		private bool renderDiagnosticInfo;

		// Token: 0x0400015F RID: 351
		private string diagnosticInfo = string.Empty;

		// Token: 0x04000160 RID: 352
		private string resourcePath;

		// Token: 0x04000161 RID: 353
		private bool isIE = true;

		// Token: 0x02000052 RID: 82
		internal enum FEErrorCodes
		{
			// Token: 0x04000163 RID: 355
			Unknown,
			// Token: 0x04000164 RID: 356
			SSLCertificateProblem,
			// Token: 0x04000165 RID: 357
			CAS14WithNoWIA,
			// Token: 0x04000166 RID: 358
			NoFbaSSL,
			// Token: 0x04000167 RID: 359
			NoLegacyCAS,
			// Token: 0x04000168 RID: 360
			CasRedirect
		}
	}
}
