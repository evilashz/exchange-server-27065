using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000154 RID: 340
	public class LanguageSelection : OwaPage
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0005189F File Offset: 0x0004FA9F
		protected string Destination
		{
			get
			{
				return Utilities.GetQueryStringParameter(base.Request, "url", false);
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000518B4 File Offset: 0x0004FAB4
		protected override void OnLoad(EventArgs e)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(base.Request.QueryString.ToString());
			string text = nameValueCollection.Get("url");
			Uri uri;
			if (text != null && !Uri.TryCreate(text, UriKind.Relative, out uri))
			{
				base.Response.Redirect(this.deleteURLParam(nameValueCollection));
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00051903 File Offset: 0x0004FB03
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00051906 File Offset: 0x0004FB06
		protected bool ShowAccessibilityOption
		{
			get
			{
				return !Utilities.IsEcpUrl(this.Destination);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00051916 File Offset: 0x0004FB16
		protected string PageTitle
		{
			get
			{
				return LocalizedStrings.GetHtmlEncoded(Utilities.IsEacUrl(this.Destination) ? 1018921346 : -1066333875);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00051936 File Offset: 0x0004FB36
		protected string SignInHeader
		{
			get
			{
				return LocalizedStrings.GetHtmlEncoded(Utilities.IsEacUrl(this.Destination) ? 1018921346 : -740205329);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00051956 File Offset: 0x0004FB56
		protected bool IsEcpDestination
		{
			get
			{
				return Utilities.IsEcpUrl(this.Destination);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00051963 File Offset: 0x0004FB63
		public override string OwaVersion
		{
			get
			{
				return OwaVersionId.Current;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0005196C File Offset: 0x0004FB6C
		protected void RenderLanguageSelection()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "ll-cc", false);
			int lcid;
			if (!int.TryParse(queryStringParameter, out lcid))
			{
				lcid = Thread.CurrentThread.CurrentUICulture.LCID;
			}
			base.Response.Write("<select name=");
			base.Response.Write("lcid");
			base.Response.Write(" class=languageInputText>");
			CultureInfo[] supportedCultures = Microsoft.Exchange.Clients.Owa.Core.Culture.GetSupportedCultures(true);
			for (int i = 0; i < supportedCultures.Length; i++)
			{
				base.Response.Write("<option");
				if (supportedCultures[i].LCID == lcid)
				{
					base.Response.Write(" selected");
				}
				base.Response.Write(" value=\"");
				base.Response.Write(supportedCultures[i].LCID);
				base.Response.Write("\">");
				string s = supportedCultures[i].NativeName;
				if (supportedCultures[i].LCID == LanguageSelection.lcidForSrCyrlCS)
				{
					s = "српски (ћирилица, Србија и Црна Гора (бивша))";
				}
				else if (supportedCultures[i].LCID == LanguageSelection.lcidForSrLatnCS)
				{
					s = "srpski (latinica, Srbija i Crna Gora (bivša))";
				}
				Utilities.RenderDirectionEnhancedValue(base.Response.Output, Utilities.HtmlEncode(s), supportedCultures[i].TextInfo.IsRightToLeft);
				base.Response.Write("</option>");
			}
			base.Response.Write("</select>");
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00051AD0 File Offset: 0x0004FCD0
		protected void RenderTimeZoneSelection()
		{
			base.Response.Write("<select id=selTz name=");
			base.Response.Write("tzid");
			base.Response.Write(" class=languageInputText onchange=\"isTimezoneSelectedCheck()\">");
			base.Response.Write(" <option selected=\"selected\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(394323495));
			base.Response.Write("</option> ");
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				base.Response.Write("<option");
				base.Response.Write(" value=\"");
				Utilities.HtmlEncode(exTimeZone.Id, base.Response.Output);
				base.Response.Write("\">");
				Utilities.RenderDirectionEnhancedValue(base.Response.Output, exTimeZone.LocalizableDisplayName.ToString(Thread.CurrentThread.CurrentUICulture), OwaPage.IsRtl);
			}
			base.Response.Write("</select>");
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00051C00 File Offset: 0x0004FE00
		protected string deleteURLParam(NameValueCollection queryStringParams)
		{
			queryStringParams.Remove("url");
			string str = string.Empty;
			if (queryStringParams.Count > 0)
			{
				str = "?" + queryStringParams.ToString();
			}
			return base.Request.Url.AbsolutePath + str;
		}

		// Token: 0x04000866 RID: 2150
		private const string DestinationParameter = "url";

		// Token: 0x04000867 RID: 2151
		private const string LcidParameter = "ll-cc";

		// Token: 0x04000868 RID: 2152
		private const string SrLatnCSNativeNameSuffixedWithFormer = "srpski (latinica, Srbija i Crna Gora (bivša))";

		// Token: 0x04000869 RID: 2153
		private const string SrCyrlCSNativeNameSuffixedWithFormer = "српски (ћирилица, Србија и Црна Гора (бивша))";

		// Token: 0x0400086A RID: 2154
		private static readonly int lcidForSrCyrlCS = new CultureInfo("sr-Cyrl-CS").LCID;

		// Token: 0x0400086B RID: 2155
		private static readonly int lcidForSrLatnCS = new CultureInfo("sr-Latn-CS").LCID;
	}
}
