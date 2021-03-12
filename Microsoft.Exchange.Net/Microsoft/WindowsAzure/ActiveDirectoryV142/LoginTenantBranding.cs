using System;
using System.CodeDom.Compiler;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000601 RID: 1537
	[DataServiceKey("locale")]
	public class LoginTenantBranding
	{
		// Token: 0x06001B54 RID: 6996 RVA: 0x00031E3C File Offset: 0x0003003C
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

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00031E6C File Offset: 0x0003006C
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x00031E74 File Offset: 0x00030074
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

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00031E7D File Offset: 0x0003007D
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x00031E85 File Offset: 0x00030085
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

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00031E8E File Offset: 0x0003008E
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x00031E96 File Offset: 0x00030096
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

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00031E9F File Offset: 0x0003009F
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x00031EA7 File Offset: 0x000300A7
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

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00031EB0 File Offset: 0x000300B0
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x00031EB8 File Offset: 0x000300B8
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

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00031EC1 File Offset: 0x000300C1
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x00031EC9 File Offset: 0x000300C9
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00031ED2 File Offset: 0x000300D2
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x00031EDA File Offset: 0x000300DA
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

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00031EE3 File Offset: 0x000300E3
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x00031EEB File Offset: 0x000300EB
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x00031EF4 File Offset: 0x000300F4
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x00031EFC File Offset: 0x000300FC
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

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00031F05 File Offset: 0x00030105
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x00031F0D File Offset: 0x0003010D
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

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00031F16 File Offset: 0x00030116
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x00031F1E File Offset: 0x0003011E
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

		// Token: 0x04001C70 RID: 7280
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _backgroundColor;

		// Token: 0x04001C71 RID: 7281
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _bannerLogo;

		// Token: 0x04001C72 RID: 7282
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _bannerLogoUrl;

		// Token: 0x04001C73 RID: 7283
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _boilerPlateText;

		// Token: 0x04001C74 RID: 7284
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _illustration;

		// Token: 0x04001C75 RID: 7285
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _illustrationUrl;

		// Token: 0x04001C76 RID: 7286
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _locale;

		// Token: 0x04001C77 RID: 7287
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _metadataUrl;

		// Token: 0x04001C78 RID: 7288
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _tileLogo;

		// Token: 0x04001C79 RID: 7289
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _tileLogoUrl;

		// Token: 0x04001C7A RID: 7290
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userIdLabel;
	}
}
