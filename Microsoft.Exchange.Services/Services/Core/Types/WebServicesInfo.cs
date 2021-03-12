using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C3 RID: 2243
	internal class WebServicesInfo
	{
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000DB8F0 File Offset: 0x000D9AF0
		// (set) Token: 0x06003F7A RID: 16250 RVA: 0x000DB8F8 File Offset: 0x000D9AF8
		public Uri Url { get; private set; }

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000DB901 File Offset: 0x000D9B01
		// (set) Token: 0x06003F7C RID: 16252 RVA: 0x000DB909 File Offset: 0x000D9B09
		public string ServerFullyQualifiedDomainName { get; private set; }

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000DB912 File Offset: 0x000D9B12
		// (set) Token: 0x06003F7E RID: 16254 RVA: 0x000DB91A File Offset: 0x000D9B1A
		public int ServerVersionNumber { get; private set; }

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000DB923 File Offset: 0x000D9B23
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x000DB92B File Offset: 0x000D9B2B
		public string SiteDistinguishedName { get; private set; }

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000DB934 File Offset: 0x000D9B34
		// (set) Token: 0x06003F82 RID: 16258 RVA: 0x000DB93C File Offset: 0x000D9B3C
		public string ServerDistinguishedName { get; private set; }

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x000DB945 File Offset: 0x000D9B45
		// (set) Token: 0x06003F84 RID: 16260 RVA: 0x000DB94D File Offset: 0x000D9B4D
		public bool IsCafeUrl { get; private set; }

		// Token: 0x06003F85 RID: 16261 RVA: 0x000DB958 File Offset: 0x000D9B58
		private WebServicesInfo(Uri url, string serverFqdn, int serverVersionNumber, bool isCafeUrl, string serverDistinguishedName = null, string siteDistinguishedName = null)
		{
			this.Url = url;
			this.ServerFullyQualifiedDomainName = serverFqdn;
			this.ServerVersionNumber = ((serverVersionNumber >= Server.E15MinVersion) ? Server.E15MinVersion : serverVersionNumber);
			this.ServerDistinguishedName = serverDistinguishedName;
			this.SiteDistinguishedName = siteDistinguishedName;
			this.IsCafeUrl = isCafeUrl;
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000DB9A7 File Offset: 0x000D9BA7
		public static WebServicesInfo CreateFromWebServicesService(WebServicesService webServicesService)
		{
			return new WebServicesInfo(webServicesService.Url, webServicesService.ServerFullyQualifiedDomainName, webServicesService.ServerVersionNumber, false, webServicesService.ServerDistinguishedName, webServicesService.Site.DistinguishedName);
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x000DB9D2 File Offset: 0x000D9BD2
		public static WebServicesInfo Create(Uri url, string serverFqdn, int serverVersionNumber, bool isCafeUrl)
		{
			return new WebServicesInfo(url, serverFqdn, serverVersionNumber, isCafeUrl, null, null);
		}
	}
}
