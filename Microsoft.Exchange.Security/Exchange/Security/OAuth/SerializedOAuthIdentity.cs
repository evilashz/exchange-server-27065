using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	internal sealed class SerializedOAuthIdentity
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00037899 File Offset: 0x00035A99
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x000378A1 File Offset: 0x00035AA1
		public string OrganizationName { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000378AA File Offset: 0x00035AAA
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x000378B2 File Offset: 0x00035AB2
		public string PartnerApplicationDn { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000378BB File Offset: 0x00035ABB
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x000378C3 File Offset: 0x00035AC3
		public string PartnerApplicationAppId { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000378CC File Offset: 0x00035ACC
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x000378D4 File Offset: 0x00035AD4
		public string PartnerApplicationRealm { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000378DD File Offset: 0x00035ADD
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x000378E5 File Offset: 0x00035AE5
		public string UserDn { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000378EE File Offset: 0x00035AEE
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x000378F6 File Offset: 0x00035AF6
		public string OfficeExtensionId { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000378FF File Offset: 0x00035AFF
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00037907 File Offset: 0x00035B07
		public string V1ProfileAppId { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00037910 File Offset: 0x00035B10
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00037918 File Offset: 0x00035B18
		public string Scope { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00037921 File Offset: 0x00035B21
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00037929 File Offset: 0x00035B29
		public string OrganizationIdBase64 { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00037932 File Offset: 0x00035B32
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0003793A File Offset: 0x00035B3A
		public string IsFromSameOrgExchange { get; set; }
	}
}
