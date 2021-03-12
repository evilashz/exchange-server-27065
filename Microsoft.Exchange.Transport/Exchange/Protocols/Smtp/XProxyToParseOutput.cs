using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000496 RID: 1174
	internal class XProxyToParseOutput
	{
		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000D9CA8 File Offset: 0x000D7EA8
		// (set) Token: 0x0600353D RID: 13629 RVA: 0x000D9CB0 File Offset: 0x000D7EB0
		public string DecodedCertificateSubject { get; set; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000D9CB9 File Offset: 0x000D7EB9
		// (set) Token: 0x0600353F RID: 13631 RVA: 0x000D9CC1 File Offset: 0x000D7EC1
		public List<INextHopServer> Destinations { get; set; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000D9CCA File Offset: 0x000D7ECA
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000D9CD2 File Offset: 0x000D7ED2
		public ErrorPolicies? ErrorPolicies { get; set; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000D9CDB File Offset: 0x000D7EDB
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000D9CE3 File Offset: 0x000D7EE3
		public bool? ForceHelo { get; set; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000D9CEC File Offset: 0x000D7EEC
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x000D9CF4 File Offset: 0x000D7EF4
		public Fqdn Fqdn { get; set; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000D9CFD File Offset: 0x000D7EFD
		// (set) Token: 0x06003547 RID: 13639 RVA: 0x000D9D05 File Offset: 0x000D7F05
		public bool IsDnsRoutingEnabled { get; set; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000D9D0E File Offset: 0x000D7F0E
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x000D9D16 File Offset: 0x000D7F16
		public bool? IsLast { get; set; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000D9D1F File Offset: 0x000D7F1F
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000D9D27 File Offset: 0x000D7F27
		public bool IsProbeConnection { get; set; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000D9D30 File Offset: 0x000D7F30
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x000D9D38 File Offset: 0x000D7F38
		public string NextHopDomain { get; set; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000D9D41 File Offset: 0x000D7F41
		// (set) Token: 0x0600354F RID: 13647 RVA: 0x000D9D49 File Offset: 0x000D7F49
		public int? OutboundIPPool { get; set; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000D9D52 File Offset: 0x000D7F52
		// (set) Token: 0x06003551 RID: 13649 RVA: 0x000D9D5A File Offset: 0x000D7F5A
		public int? Port { get; set; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06003552 RID: 13650 RVA: 0x000D9D63 File Offset: 0x000D7F63
		// (set) Token: 0x06003553 RID: 13651 RVA: 0x000D9D6B File Offset: 0x000D7F6B
		public bool? RequireOorg { get; set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000D9D74 File Offset: 0x000D7F74
		// (set) Token: 0x06003555 RID: 13653 RVA: 0x000D9D7C File Offset: 0x000D7F7C
		public bool? RequireTls { get; set; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000D9D85 File Offset: 0x000D7F85
		// (set) Token: 0x06003557 RID: 13655 RVA: 0x000D9D8D File Offset: 0x000D7F8D
		public RiskLevel? Risk { get; set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000D9D96 File Offset: 0x000D7F96
		// (set) Token: 0x06003559 RID: 13657 RVA: 0x000D9D9E File Offset: 0x000D7F9E
		public string SessionId { get; set; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000D9DA7 File Offset: 0x000D7FA7
		// (set) Token: 0x0600355B RID: 13659 RVA: 0x000D9DAF File Offset: 0x000D7FAF
		public bool? ShouldSkipTls { get; set; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x000D9DB8 File Offset: 0x000D7FB8
		// (set) Token: 0x0600355D RID: 13661 RVA: 0x000D9DC0 File Offset: 0x000D7FC0
		public RequiredTlsAuthLevel? TlsAuthLevel { get; set; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000D9DC9 File Offset: 0x000D7FC9
		// (set) Token: 0x0600355F RID: 13663 RVA: 0x000D9DD1 File Offset: 0x000D7FD1
		public List<SmtpDomainWithSubdomains> TlsDomains { get; set; }
	}
}
