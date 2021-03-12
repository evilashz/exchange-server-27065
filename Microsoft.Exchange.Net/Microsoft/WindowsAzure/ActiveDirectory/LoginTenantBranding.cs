using System;
using System.CodeDom.Compiler;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B4 RID: 1460
	[DataServiceKey("locale")]
	public class LoginTenantBranding
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x0002DAB0 File Offset: 0x0002BCB0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static LoginTenantBranding CreateLoginTenantBranding(DataServiceStreamLink bannerLogo, DataServiceStreamLink illustration, string locale, DataServiceStreamLink tileLogo)
		{
			return new LoginTenantBranding
			{
				bannerLogo = bannerLogo,
				illustration = illustration,
				locale = locale,
				tileLogo = tileLogo
			};
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
		// (set) Token: 0x060015FA RID: 5626 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string backgroundColor
		{
			get
			{
				return this._backgroundColor;
			}
			set
			{
				this._backgroundColor = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0002DAF1 File Offset: 0x0002BCF1
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x0002DAF9 File Offset: 0x0002BCF9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink bannerLogo
		{
			get
			{
				return this._bannerLogo;
			}
			set
			{
				this._bannerLogo = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0002DB02 File Offset: 0x0002BD02
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0002DB0A File Offset: 0x0002BD0A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string bannerLogoUrl
		{
			get
			{
				return this._bannerLogoUrl;
			}
			set
			{
				this._bannerLogoUrl = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0002DB13 File Offset: 0x0002BD13
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0002DB1B File Offset: 0x0002BD1B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string boilerPlateText
		{
			get
			{
				return this._boilerPlateText;
			}
			set
			{
				this._boilerPlateText = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0002DB24 File Offset: 0x0002BD24
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink illustration
		{
			get
			{
				return this._illustration;
			}
			set
			{
				this._illustration = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0002DB35 File Offset: 0x0002BD35
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x0002DB3D File Offset: 0x0002BD3D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string illustrationUrl
		{
			get
			{
				return this._illustrationUrl;
			}
			set
			{
				this._illustrationUrl = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0002DB46 File Offset: 0x0002BD46
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x0002DB4E File Offset: 0x0002BD4E
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

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0002DB57 File Offset: 0x0002BD57
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x0002DB5F File Offset: 0x0002BD5F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string metadataUrl
		{
			get
			{
				return this._metadataUrl;
			}
			set
			{
				this._metadataUrl = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0002DB68 File Offset: 0x0002BD68
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0002DB70 File Offset: 0x0002BD70
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink tileLogo
		{
			get
			{
				return this._tileLogo;
			}
			set
			{
				this._tileLogo = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0002DB79 File Offset: 0x0002BD79
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0002DB81 File Offset: 0x0002BD81
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string tileLogoUrl
		{
			get
			{
				return this._tileLogoUrl;
			}
			set
			{
				this._tileLogoUrl = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0002DB8A File Offset: 0x0002BD8A
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x0002DB92 File Offset: 0x0002BD92
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userIdLabel
		{
			get
			{
				return this._userIdLabel;
			}
			set
			{
				this._userIdLabel = value;
			}
		}

		// Token: 0x040019F5 RID: 6645
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _backgroundColor;

		// Token: 0x040019F6 RID: 6646
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _bannerLogo;

		// Token: 0x040019F7 RID: 6647
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _bannerLogoUrl;

		// Token: 0x040019F8 RID: 6648
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _boilerPlateText;

		// Token: 0x040019F9 RID: 6649
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _illustration;

		// Token: 0x040019FA RID: 6650
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _illustrationUrl;

		// Token: 0x040019FB RID: 6651
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _locale;

		// Token: 0x040019FC RID: 6652
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _metadataUrl;

		// Token: 0x040019FD RID: 6653
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _tileLogo;

		// Token: 0x040019FE RID: 6654
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _tileLogoUrl;

		// Token: 0x040019FF RID: 6655
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userIdLabel;
	}
}
