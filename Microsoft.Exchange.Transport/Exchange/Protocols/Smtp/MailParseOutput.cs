using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200047D RID: 1149
	internal class MailParseOutput
	{
		// Token: 0x060034A8 RID: 13480 RVA: 0x000D6CC3 File Offset: 0x000D4EC3
		public MailParseOutput(RoutingAddress fromAddress)
		{
			this.FromAddress = fromAddress;
			this.ConsumerMailOptionalArguments = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000D6CE2 File Offset: 0x000D4EE2
		// (set) Token: 0x060034AA RID: 13482 RVA: 0x000D6CEA File Offset: 0x000D4EEA
		public string Auth { get; set; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060034AB RID: 13483 RVA: 0x000D6CF3 File Offset: 0x000D4EF3
		// (set) Token: 0x060034AC RID: 13484 RVA: 0x000D6CFB File Offset: 0x000D4EFB
		public MailDirectionality Directionality { get; set; }

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x000D6D04 File Offset: 0x000D4F04
		// (set) Token: 0x060034AE RID: 13486 RVA: 0x000D6D0C File Offset: 0x000D4F0C
		public DsnFormat DsnFormat { get; set; }

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060034AF RID: 13487 RVA: 0x000D6D15 File Offset: 0x000D4F15
		// (set) Token: 0x060034B0 RID: 13488 RVA: 0x000D6D1D File Offset: 0x000D4F1D
		public string EnvelopeId { get; set; }

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000D6D26 File Offset: 0x000D4F26
		// (set) Token: 0x060034B2 RID: 13490 RVA: 0x000D6D2E File Offset: 0x000D4F2E
		public RoutingAddress FromAddress { get; private set; }

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000D6D37 File Offset: 0x000D4F37
		// (set) Token: 0x060034B4 RID: 13492 RVA: 0x000D6D3F File Offset: 0x000D4F3F
		public string InternetMessageId { get; set; }

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000D6D48 File Offset: 0x000D4F48
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x000D6D50 File Offset: 0x000D4F50
		public BodyType MailBodyType { get; set; }

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000D6D59 File Offset: 0x000D4F59
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x000D6D61 File Offset: 0x000D4F61
		public MailCommandMessageContextParameters MessageContextParameters { get; set; }

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000D6D6A File Offset: 0x000D4F6A
		// (set) Token: 0x060034BA RID: 13498 RVA: 0x000D6D72 File Offset: 0x000D4F72
		public string Oorg { get; set; }

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000D6D7B File Offset: 0x000D4F7B
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x000D6D83 File Offset: 0x000D4F83
		public RoutingAddress OriginalFromAddress { get; set; }

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000D6D8C File Offset: 0x000D4F8C
		// (set) Token: 0x060034BE RID: 13502 RVA: 0x000D6D94 File Offset: 0x000D4F94
		public Guid ShadowMessageId { get; set; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000D6D9D File Offset: 0x000D4F9D
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x000D6DA5 File Offset: 0x000D4FA5
		public long Size { get; set; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000D6DAE File Offset: 0x000D4FAE
		// (set) Token: 0x060034C2 RID: 13506 RVA: 0x000D6DB6 File Offset: 0x000D4FB6
		public bool SmtpUtf8 { get; set; }

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000D6DBF File Offset: 0x000D4FBF
		// (set) Token: 0x060034C4 RID: 13508 RVA: 0x000D6DC7 File Offset: 0x000D4FC7
		public Guid SystemProbeId { get; set; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000D6DD0 File Offset: 0x000D4FD0
		// (set) Token: 0x060034C6 RID: 13510 RVA: 0x000D6DD8 File Offset: 0x000D4FD8
		public MailParseOutput.XAttrOrgIdData XAttrOrgId { get; set; }

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000D6DE1 File Offset: 0x000D4FE1
		// (set) Token: 0x060034C8 RID: 13512 RVA: 0x000D6DE9 File Offset: 0x000D4FE9
		public string XShadow { get; set; }

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000D6DF2 File Offset: 0x000D4FF2
		// (set) Token: 0x060034CA RID: 13514 RVA: 0x000D6DFA File Offset: 0x000D4FFA
		public Dictionary<string, string> ConsumerMailOptionalArguments { get; private set; }

		// Token: 0x0200047E RID: 1150
		public class XAttrOrgIdData
		{
			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x060034CB RID: 13515 RVA: 0x000D6E03 File Offset: 0x000D5003
			// (set) Token: 0x060034CC RID: 13516 RVA: 0x000D6E0B File Offset: 0x000D500B
			public string ExoAccountForest { get; set; }

			// Token: 0x17000FD3 RID: 4051
			// (get) Token: 0x060034CD RID: 13517 RVA: 0x000D6E14 File Offset: 0x000D5014
			// (set) Token: 0x060034CE RID: 13518 RVA: 0x000D6E1C File Offset: 0x000D501C
			public string ExoTenantContainer { get; set; }

			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x060034CF RID: 13519 RVA: 0x000D6E25 File Offset: 0x000D5025
			// (set) Token: 0x060034D0 RID: 13520 RVA: 0x000D6E2D File Offset: 0x000D502D
			public Guid ExternalOrgId { get; set; }

			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000D6E36 File Offset: 0x000D5036
			// (set) Token: 0x060034D2 RID: 13522 RVA: 0x000D6E3E File Offset: 0x000D503E
			public OrganizationId InternalOrgId { get; set; }
		}
	}
}
