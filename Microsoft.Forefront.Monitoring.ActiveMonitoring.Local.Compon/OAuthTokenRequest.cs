using System;
using System.IdentityModel.Tokens;
using Microsoft.Exchange.Hygiene.Deployment.Common;
using Microsoft.Exchange.Security.OAuth.OAuthProtocols;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000047 RID: 71
	public class OAuthTokenRequest
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000C000 File Offset: 0x0000A200
		public string AcsUrl { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000C009 File Offset: 0x0000A209
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000C011 File Offset: 0x0000A211
		public string Audience { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000C01A File Offset: 0x0000A21A
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000C022 File Offset: 0x0000A222
		public string Issuer { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000C02B File Offset: 0x0000A22B
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000C033 File Offset: 0x0000A233
		public string Resource { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000C03C File Offset: 0x0000A23C
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000C044 File Offset: 0x0000A244
		public JwtSecurityToken JwtInputToken { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000C04D File Offset: 0x0000A24D
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000C055 File Offset: 0x0000A255
		public NetHelpersWebResponse AcsNetResponse { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C05E File Offset: 0x0000A25E
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000C066 File Offset: 0x0000A266
		public string AcsTokenResultString { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000C06F File Offset: 0x0000A26F
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000C077 File Offset: 0x0000A277
		internal OAuth2AccessTokenRequest AcsTokenRequest { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000C080 File Offset: 0x0000A280
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000C088 File Offset: 0x0000A288
		internal OAuth2AccessTokenResponse AcsTokenResponse { get; set; }
	}
}
