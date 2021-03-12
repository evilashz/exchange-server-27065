using System;
using Microsoft.Exchange.Clients;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200005D RID: 93
	public class Logon : OwaPage
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00012754 File Offset: 0x00010954
		protected static string UserNameLabel
		{
			get
			{
				if (Datacenter.IsPartnerHostedOnly(false))
				{
					return LocalizedStrings.GetHtmlEncoded(1677919363);
				}
				switch (OwaVdirConfiguration.Instance.LogonFormat)
				{
				case LogonFormats.PrincipalName:
					return LocalizedStrings.GetHtmlEncoded(1677919363);
				case LogonFormats.UserName:
					return LocalizedStrings.GetHtmlEncoded(537815319);
				}
				return LocalizedStrings.GetHtmlEncoded(78658498);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000127B4 File Offset: 0x000109B4
		protected static string UserNamePlaceholder
		{
			get
			{
				if (Datacenter.IsPartnerHostedOnly(false))
				{
					return Strings.GetLocalizedString(1677919363);
				}
				switch (OwaVdirConfiguration.Instance.LogonFormat)
				{
				case LogonFormats.PrincipalName:
					return Strings.GetLocalizedString(-1896713583);
				case LogonFormats.UserName:
					return Strings.GetLocalizedString(-40289791);
				}
				return Strings.GetLocalizedString(609186145);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00012814 File Offset: 0x00010A14
		protected bool ReplaceCurrent
		{
			get
			{
				string a = base.Request.QueryString["replaceCurrent"];
				return a == "1" || base.IsDownLevelClient;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001284F File Offset: 0x00010A4F
		protected bool ShowOwaLightOption
		{
			get
			{
				return !UrlUtilities.IsEcpUrl(this.Destination) && OwaVdirConfiguration.Instance.LightSelectionEnabled;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0001286A File Offset: 0x00010A6A
		protected bool ShowPublicPrivateSelection
		{
			get
			{
				return OwaVdirConfiguration.Instance.PublicPrivateSelectionEnabled;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00012876 File Offset: 0x00010A76
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00012879 File Offset: 0x00010A79
		protected string LogoffUrl
		{
			get
			{
				return base.Request.Url.Scheme + "://" + base.Request.Url.Authority + OwaUrl.Logoff.GetExplicitUrl(base.Request);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000128B8 File Offset: 0x00010AB8
		protected Logon.LogonReason Reason
		{
			get
			{
				string text = base.Request.QueryString["reason"];
				if (text == null)
				{
					return Logon.LogonReason.None;
				}
				string key;
				switch (key = text)
				{
				case "1":
					return Logon.LogonReason.Logoff;
				case "2":
					return Logon.LogonReason.InvalidCredentials;
				case "3":
					return Logon.LogonReason.Timeout;
				case "4":
					return Logon.LogonReason.ChangePasswordLogoff;
				case "5":
					return Logon.LogonReason.BlockedByClientAccessRules;
				case "6":
					return Logon.LogonReason.EACBlockedByClientAccessRules;
				}
				return Logon.LogonReason.None;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00012984 File Offset: 0x00010B84
		protected string Destination
		{
			get
			{
				string text = base.Request.QueryString["url"];
				if (text == null || string.Equals(text, this.LogoffUrl, StringComparison.Ordinal))
				{
					return base.Request.GetBaseUrl();
				}
				return text;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000129C8 File Offset: 0x00010BC8
		protected string CloseWindowUrl
		{
			get
			{
				Uri uri;
				string result;
				if (Uri.TryCreate(this.Destination, UriKind.Absolute, out uri) && uri.AbsolutePath.EndsWith("/closewindow.aspx", StringComparison.OrdinalIgnoreCase))
				{
					result = this.Destination;
				}
				else
				{
					result = this.BaseUrl + "?ae=Dialog&t=CloseWindow&exsvurl=1";
				}
				return result;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00012A13 File Offset: 0x00010C13
		protected string PageTitle
		{
			get
			{
				return LocalizedStrings.GetHtmlEncoded(this.IsEcpDestination ? 1018921346 : -1066333875);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00012A2E File Offset: 0x00010C2E
		protected string SignInHeader
		{
			get
			{
				return LocalizedStrings.GetHtmlEncoded(this.IsEcpDestination ? 1018921346 : -740205329);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00012A49 File Offset: 0x00010C49
		protected bool IsEcpDestination
		{
			get
			{
				return UrlUtilities.IsEacUrl(this.Destination);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00012A56 File Offset: 0x00010C56
		protected string LoadFailedMessageValue
		{
			get
			{
				return "logon page loaded";
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00012A5D File Offset: 0x00010C5D
		private string BaseUrl
		{
			get
			{
				return base.Request.Url.Scheme + "://" + base.Request.Url.Authority + OwaUrl.ApplicationRoot.GetExplicitUrl(base.Request);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00012A99 File Offset: 0x00010C99
		private string Default14Url
		{
			get
			{
				return base.Request.Url.Scheme + "://" + base.Request.Url.Authority + OwaUrl.Default14Page.GetExplicitUrl(base.Request);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00012AD5 File Offset: 0x00010CD5
		protected void RenderLogonHref()
		{
			base.Response.Write("logon.aspx?replaceCurrent=1");
			if (this.Reason != Logon.LogonReason.None)
			{
				base.Response.Write("&reason=");
				base.Response.Write((int)this.Reason);
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00012B15 File Offset: 0x00010D15
		protected override void OnPreRender(EventArgs e)
		{
			base.Response.Headers.Set("X-Frame-Options", "SAMEORIGIN");
			base.OnPreRender(e);
		}

		// Token: 0x040001DC RID: 476
		private const string Option = "<option value=\"{0}\"{1}>{2}</option>";

		// Token: 0x040001DD RID: 477
		private const string DestinationParameter = "url";

		// Token: 0x040001DE RID: 478
		private const string FlagsParameter = "flags";

		// Token: 0x040001DF RID: 479
		private const string LiveIdAuthenticationModuleSaveUrlOnLogoffParameter = "&exsvurl=1";

		// Token: 0x040001E0 RID: 480
		private const string EcpCloseWindowUrl = "/closewindow.aspx";

		// Token: 0x0200005E RID: 94
		protected enum LogonReason
		{
			// Token: 0x040001E2 RID: 482
			None,
			// Token: 0x040001E3 RID: 483
			Logoff,
			// Token: 0x040001E4 RID: 484
			InvalidCredentials,
			// Token: 0x040001E5 RID: 485
			Timeout,
			// Token: 0x040001E6 RID: 486
			ChangePasswordLogoff,
			// Token: 0x040001E7 RID: 487
			BlockedByClientAccessRules,
			// Token: 0x040001E8 RID: 488
			EACBlockedByClientAccessRules
		}
	}
}
