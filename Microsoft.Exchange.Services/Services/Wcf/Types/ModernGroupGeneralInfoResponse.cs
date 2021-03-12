using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009EA RID: 2538
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernGroupGeneralInfoResponse
	{
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x001006E4 File Offset: 0x000FE8E4
		// (set) Token: 0x060047AA RID: 18346 RVA: 0x001006EC File Offset: 0x000FE8EC
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x001006F5 File Offset: 0x000FE8F5
		// (set) Token: 0x060047AC RID: 18348 RVA: 0x001006FD File Offset: 0x000FE8FD
		[DataMember]
		public string SmtpAddress { get; set; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x060047AD RID: 18349 RVA: 0x00100706 File Offset: 0x000FE906
		// (set) Token: 0x060047AE RID: 18350 RVA: 0x0010070E File Offset: 0x000FE90E
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x060047AF RID: 18351 RVA: 0x00100717 File Offset: 0x000FE917
		// (set) Token: 0x060047B0 RID: 18352 RVA: 0x0010071F File Offset: 0x000FE91F
		[DataMember]
		public ModernGroupObjectType ModernGroupType { get; set; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x060047B1 RID: 18353 RVA: 0x00100728 File Offset: 0x000FE928
		// (set) Token: 0x060047B2 RID: 18354 RVA: 0x00100730 File Offset: 0x000FE930
		[DataMember]
		public bool IsOwner { get; set; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060047B3 RID: 18355 RVA: 0x00100739 File Offset: 0x000FE939
		// (set) Token: 0x060047B4 RID: 18356 RVA: 0x00100741 File Offset: 0x000FE941
		[DataMember]
		public bool IsMember { get; set; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060047B5 RID: 18357 RVA: 0x0010074A File Offset: 0x000FE94A
		// (set) Token: 0x060047B6 RID: 18358 RVA: 0x00100752 File Offset: 0x000FE952
		[DataMember]
		public bool ShouldEscalate { get; set; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x0010075B File Offset: 0x000FE95B
		// (set) Token: 0x060047B8 RID: 18360 RVA: 0x00100763 File Offset: 0x000FE963
		[DataMember]
		public bool RequireSenderAuthenticationEnabled { get; set; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x0010076C File Offset: 0x000FE96C
		// (set) Token: 0x060047BA RID: 18362 RVA: 0x00100774 File Offset: 0x000FE974
		[DataMember]
		public bool AutoSubscribeNewGroupMembers { get; set; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x0010077D File Offset: 0x000FE97D
		// (set) Token: 0x060047BC RID: 18364 RVA: 0x00100785 File Offset: 0x000FE985
		[DataMember]
		public int OwnersCount { get; set; }
	}
}
