using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000751 RID: 1873
	[DataContract]
	public class LinkedInPhoneNumber : IExtensibleDataObject
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x0004CBF1 File Offset: 0x0004ADF1
		// (set) Token: 0x060024AC RID: 9388 RVA: 0x0004CBF9 File Offset: 0x0004ADF9
		[DataMember(Name = "phoneNumber")]
		public string Number { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x0004CC02 File Offset: 0x0004AE02
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x0004CC0A File Offset: 0x0004AE0A
		[DataMember(Name = "phoneType")]
		public string Type { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x0004CC13 File Offset: 0x0004AE13
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x0004CC1B File Offset: 0x0004AE1B
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
