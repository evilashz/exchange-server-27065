using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200069C RID: 1692
	internal class AuthMetadata
	{
		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x06004E9A RID: 20122 RVA: 0x00121BEC File Offset: 0x0011FDEC
		// (set) Token: 0x06004E9B RID: 20123 RVA: 0x00121BF4 File Offset: 0x0011FDF4
		public string ServiceName { get; set; }

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x00121BFD File Offset: 0x0011FDFD
		// (set) Token: 0x06004E9D RID: 20125 RVA: 0x00121C05 File Offset: 0x0011FE05
		public string Issuer { get; set; }

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x06004E9E RID: 20126 RVA: 0x00121C0E File Offset: 0x0011FE0E
		// (set) Token: 0x06004E9F RID: 20127 RVA: 0x00121C16 File Offset: 0x0011FE16
		public string Realm { get; set; }

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x00121C1F File Offset: 0x0011FE1F
		// (set) Token: 0x06004EA1 RID: 20129 RVA: 0x00121C27 File Offset: 0x0011FE27
		public string IssuingEndpoint { get; set; }

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x00121C30 File Offset: 0x0011FE30
		// (set) Token: 0x06004EA3 RID: 20131 RVA: 0x00121C38 File Offset: 0x0011FE38
		public string AuthorizationEndpoint { get; set; }

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x00121C41 File Offset: 0x0011FE41
		// (set) Token: 0x06004EA5 RID: 20133 RVA: 0x00121C49 File Offset: 0x0011FE49
		public string KeysEndpoint { get; set; }

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x00121C52 File Offset: 0x0011FE52
		// (set) Token: 0x06004EA7 RID: 20135 RVA: 0x00121C5A File Offset: 0x0011FE5A
		public string[] CertificateStrings { get; set; }
	}
}
