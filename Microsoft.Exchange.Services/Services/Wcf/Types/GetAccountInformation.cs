using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A7F RID: 2687
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAccountInformation : SetUserData
	{
		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x00106154 File Offset: 0x00104354
		// (set) Token: 0x06004C22 RID: 19490 RVA: 0x0010615C File Offset: 0x0010435C
		[DataMember]
		public MyAccountStatisticsData Statistics { get; set; }

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x00106165 File Offset: 0x00104365
		// (set) Token: 0x06004C24 RID: 19492 RVA: 0x0010616D File Offset: 0x0010436D
		[DataMember]
		public MyAccountMailboxData Mailbox { get; set; }

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x00106176 File Offset: 0x00104376
		// (set) Token: 0x06004C26 RID: 19494 RVA: 0x0010617E File Offset: 0x0010437E
		[DataMember]
		public CountryData[] CountryList { get; set; }
	}
}
