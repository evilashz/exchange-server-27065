using System;
using System.CodeDom.Compiler;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C0 RID: 1472
	[DataServiceKey("locale")]
	public class AppLocalizedBranding
	{
		// Token: 0x060016C2 RID: 5826 RVA: 0x0002E3DC File Offset: 0x0002C5DC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static AppLocalizedBranding CreateAppLocalizedBranding(DataServiceStreamLink appBannerLogo, DataServiceStreamLink heroIllustration, string locale)
		{
			return new AppLocalizedBranding
			{
				appBannerLogo = appBannerLogo,
				heroIllustration = heroIllustration,
				locale = locale
			};
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x0002E405 File Offset: 0x0002C605
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x0002E40D File Offset: 0x0002C60D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink appBannerLogo
		{
			get
			{
				return this._appBannerLogo;
			}
			set
			{
				this._appBannerLogo = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0002E416 File Offset: 0x0002C616
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x0002E41E File Offset: 0x0002C61E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string appBannerLogoUrl
		{
			get
			{
				return this._appBannerLogoUrl;
			}
			set
			{
				this._appBannerLogoUrl = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0002E427 File Offset: 0x0002C627
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0002E42F File Offset: 0x0002C62F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0002E438 File Offset: 0x0002C638
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0002E440 File Offset: 0x0002C640
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink heroIllustration
		{
			get
			{
				return this._heroIllustration;
			}
			set
			{
				this._heroIllustration = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0002E449 File Offset: 0x0002C649
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0002E451 File Offset: 0x0002C651
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string heroIllustrationUrl
		{
			get
			{
				return this._heroIllustrationUrl;
			}
			set
			{
				this._heroIllustrationUrl = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0002E45A File Offset: 0x0002C65A
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0002E462 File Offset: 0x0002C662
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string locale
		{
			get
			{
				return this._locale;
			}
			set
			{
				this._locale = value;
			}
		}

		// Token: 0x04001A53 RID: 6739
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _appBannerLogo;

		// Token: 0x04001A54 RID: 6740
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _appBannerLogoUrl;

		// Token: 0x04001A55 RID: 6741
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001A56 RID: 6742
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _heroIllustration;

		// Token: 0x04001A57 RID: 6743
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _heroIllustrationUrl;

		// Token: 0x04001A58 RID: 6744
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _locale;
	}
}
