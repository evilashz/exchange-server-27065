using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000027 RID: 39
	public class Education : Page
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00009940 File Offset: 0x00007B40
		protected override void OnLoad(EventArgs e)
		{
			string[] userLanguages = HttpContext.Current.Request.UserLanguages;
			if (userLanguages != null)
			{
				int num = Math.Min(5, userLanguages.Length);
				for (int i = 0; i < num; i++)
				{
					string text = Utilities.ValidateLanguageTag(userLanguages[i]);
					if (text != null)
					{
						CultureInfo supportedBrowserLanguage = Utilities.GetSupportedBrowserLanguage(text);
						if (supportedBrowserLanguage != null)
						{
							Thread.CurrentThread.CurrentCulture = supportedBrowserLanguage;
							Thread.CurrentThread.CurrentUICulture = supportedBrowserLanguage;
						}
					}
				}
			}
			this.OnInit(e);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000099AD File Offset: 0x00007BAD
		protected bool IsEducationUrlAvailable
		{
			get
			{
				return !string.IsNullOrEmpty(this.EducationUrl);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000099BF File Offset: 0x00007BBF
		protected string EducationUrl
		{
			get
			{
				return HttpContext.Current.Request.Params[Utilities.EducationUrlParameter];
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000099DA File Offset: 0x00007BDA
		protected string DestinationUrlParameter
		{
			get
			{
				return HttpContext.Current.Request.Params[Utilities.DestinationUrlParameter];
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000099F8 File Offset: 0x00007BF8
		protected string Destination
		{
			get
			{
				string text = this.DestinationUrlParameter;
				string text2 = HttpContext.Current.Request.Params[Utilities.LiveIdUrlParameter];
				if (!string.IsNullOrEmpty(text2))
				{
					string userDomain = Utilities.GetUserDomain(text2);
					if (!string.IsNullOrEmpty(userDomain))
					{
						if (text.IndexOf('?') > 0)
						{
							text = string.Format("{0}&{1}={2}&{3}={4}", new object[]
							{
								text,
								"realm",
								userDomain,
								Utilities.UserNameParameter,
								text2
							});
						}
						else
						{
							text = string.Format("{0}?{1}={2}&{3}={4}", new object[]
							{
								text,
								"realm",
								userDomain,
								Utilities.UserNameParameter,
								text2
							});
						}
					}
				}
				return text;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00009AB4 File Offset: 0x00007CB4
		protected string UserId
		{
			get
			{
				string text = HttpContext.Current.Request.Params[Utilities.LiveIdUrlParameter];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00009AE8 File Offset: 0x00007CE8
		protected string UserDomain
		{
			get
			{
				string text = HttpContext.Current.Request.Params[Utilities.LiveIdUrlParameter];
				if (text == null)
				{
					return string.Empty;
				}
				return Utilities.GetUserDomain(text);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00009B20 File Offset: 0x00007D20
		protected bool ShowWarning
		{
			get
			{
				string text = HttpContext.Current.Request.Params[Utilities.LiveIdUrlParameter];
				if (text == null)
				{
					return false;
				}
				string userDomain = Utilities.GetUserDomain(text);
				return string.IsNullOrEmpty(userDomain) || !SmtpAddress.IsValidDomain(userDomain);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009B68 File Offset: 0x00007D68
		protected void RenderImage(string imageName)
		{
			string s = Utilities.ImagesPath + imageName;
			base.Response.Write(s);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00009B8D File Offset: 0x00007D8D
		protected string EducationMessage
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)4265046865U));
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00009BA3 File Offset: 0x00007DA3
		protected string LiveIdLabel
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)2723658401U));
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00009BB9 File Offset: 0x00007DB9
		protected string GetLiveIdMessage
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)2886870364U));
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00009BCF File Offset: 0x00007DCF
		protected string InvalidLiveIdWarning
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString(Strings.IDs.InvalidLiveIdWarning));
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00009BE5 File Offset: 0x00007DE5
		protected string OutlookWebAccess
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)3228633421U));
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00009BFB File Offset: 0x00007DFB
		protected string ConnectedToExchange
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)3976540663U));
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00009C11 File Offset: 0x00007E11
		protected string LogonCopyright
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)2308016970U));
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00009C27 File Offset: 0x00007E27
		protected string AddToFavorites
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)3266846781U));
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00009C3D File Offset: 0x00007E3D
		protected string GoThereNowButtonText
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)3147877247U));
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00009C53 File Offset: 0x00007E53
		protected string NextButtonText
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)4082986936U));
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00009C69 File Offset: 0x00007E69
		protected string WhyMessage
		{
			get
			{
				return Utilities.HtmlEncode(Strings.GetLocalizedString((Strings.IDs)3937131501U));
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00009C7F File Offset: 0x00007E7F
		protected string HelpLink
		{
			get
			{
				return "http://outlookliveanswers.com/forums/p/6581/20456.aspx#20456";
			}
		}

		// Token: 0x0400013B RID: 315
		protected string logonTopLeftImg = "lgntopl.gif";

		// Token: 0x0400013C RID: 316
		protected string logonTopRightImg = "lgntopr.gif";

		// Token: 0x0400013D RID: 317
		protected string logonBottomLeftImg = "lgnbotl.gif";

		// Token: 0x0400013E RID: 318
		protected string logonBottomRightImg = "lgnbotr.gif";

		// Token: 0x0400013F RID: 319
		protected string exchangeLogoImg = "lgnexlogo.gif";
	}
}
