using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000093 RID: 147
	[DataContract(Name = "AlternateMailbox", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class AlternateMailbox
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001789C File Offset: 0x00015A9C
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x000178A4 File Offset: 0x00015AA4
		[DataMember(Name = "Type", IsRequired = true, Order = 1)]
		public string Type { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000178AD File Offset: 0x00015AAD
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000178B5 File Offset: 0x00015AB5
		[DataMember(Name = "DisplayName", IsRequired = true, Order = 2)]
		public string DisplayName { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x000178BE File Offset: 0x00015ABE
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x000178C6 File Offset: 0x00015AC6
		[DataMember(Name = "LegacyDN", IsRequired = true, Order = 3)]
		public string LegacyDN { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000178CF File Offset: 0x00015ACF
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000178D7 File Offset: 0x00015AD7
		[DataMember(Name = "Server", IsRequired = true, Order = 4)]
		public string Server { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000178E0 File Offset: 0x00015AE0
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000178E8 File Offset: 0x00015AE8
		[DataMember(Name = "SmtpAddress", IsRequired = true, Order = 5)]
		public string SmtpAddress { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000178F1 File Offset: 0x00015AF1
		// (set) Token: 0x060003CF RID: 975 RVA: 0x000178F9 File Offset: 0x00015AF9
		[DataMember(Name = "OwnerSmtpAddress", IsRequired = true, Order = 6)]
		public string OwnerSmtpAddress { get; set; }
	}
}
