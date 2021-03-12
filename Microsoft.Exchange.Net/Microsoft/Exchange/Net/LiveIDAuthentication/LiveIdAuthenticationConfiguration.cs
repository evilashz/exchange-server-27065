using System;

namespace Microsoft.Exchange.Net.LiveIDAuthentication
{
	// Token: 0x02000764 RID: 1892
	public class LiveIdAuthenticationConfiguration
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0004E69F File Offset: 0x0004C89F
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x0004E6A7 File Offset: 0x0004C8A7
		public string MsoTokenIssuerUri { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0004E6B0 File Offset: 0x0004C8B0
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x0004E6B8 File Offset: 0x0004C8B8
		public Uri LiveServiceLogin1Uri { get; set; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0004E6C1 File Offset: 0x0004C8C1
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x0004E6C9 File Offset: 0x0004C8C9
		public Uri LiveServiceLogin2Uri { get; set; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x0004E6D2 File Offset: 0x0004C8D2
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x0004E6DA File Offset: 0x0004C8DA
		public Uri MsoServiceLogin2Uri { get; set; }

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0004E6E3 File Offset: 0x0004C8E3
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x0004E6EB File Offset: 0x0004C8EB
		public Uri MsoGetUserRealmUri { get; set; }
	}
}
