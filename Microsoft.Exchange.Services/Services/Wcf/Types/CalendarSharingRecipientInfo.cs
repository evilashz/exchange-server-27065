using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A2B RID: 2603
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarSharingRecipientInfo
	{
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x00102845 File Offset: 0x00100A45
		// (set) Token: 0x0600496C RID: 18796 RVA: 0x0010284D File Offset: 0x00100A4D
		[DataMember]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x00102856 File Offset: 0x00100A56
		// (set) Token: 0x0600496E RID: 18798 RVA: 0x0010285E File Offset: 0x00100A5E
		[DataMember]
		public bool IsInsideOrganization { get; set; }

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600496F RID: 18799 RVA: 0x00102867 File Offset: 0x00100A67
		// (set) Token: 0x06004970 RID: 18800 RVA: 0x0010286F File Offset: 0x00100A6F
		[DataMember]
		public string CurrentDetailLevel { get; set; }

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06004971 RID: 18801 RVA: 0x00102878 File Offset: 0x00100A78
		// (set) Token: 0x06004972 RID: 18802 RVA: 0x00102880 File Offset: 0x00100A80
		[DataMember]
		public string HandlerType { get; set; }

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06004973 RID: 18803 RVA: 0x00102889 File Offset: 0x00100A89
		// (set) Token: 0x06004974 RID: 18804 RVA: 0x00102891 File Offset: 0x00100A91
		[DataMember]
		public string[] AllowedDetailLevels { get; set; }
	}
}
