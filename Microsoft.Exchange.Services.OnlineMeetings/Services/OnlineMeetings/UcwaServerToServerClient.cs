using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200001D RID: 29
	internal abstract class UcwaServerToServerClient
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003288 File Offset: 0x00001488
		protected UcwaServerToServerClient(string ucwaUrl, ICredentials oauthCredentials)
		{
			this.UcwaUrl = ucwaUrl;
			this.OAuthCredentials = oauthCredentials;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000329E File Offset: 0x0000149E
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000032A6 File Offset: 0x000014A6
		protected string UcwaUrl { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000032AF File Offset: 0x000014AF
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000032B7 File Offset: 0x000014B7
		protected ICredentials OAuthCredentials { get; set; }
	}
}
