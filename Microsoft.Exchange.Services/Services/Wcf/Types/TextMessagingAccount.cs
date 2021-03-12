using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A95 RID: 2709
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TextMessagingAccount : SetTextMessagingAccountData
	{
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x00106557 File Offset: 0x00104757
		// (set) Token: 0x06004C86 RID: 19590 RVA: 0x0010655F File Offset: 0x0010475F
		[DataMember]
		public CarrierData[] CarrierList { get; set; }

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x00106568 File Offset: 0x00104768
		// (set) Token: 0x06004C88 RID: 19592 RVA: 0x00106570 File Offset: 0x00104770
		[DataMember]
		public E164NumberData E164NotificationPhoneNumber { get; set; }

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x00106579 File Offset: 0x00104779
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x00106581 File Offset: 0x00104781
		[DataMember]
		public bool EasEnabled { get; set; }

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x0010658A File Offset: 0x0010478A
		// (set) Token: 0x06004C8C RID: 19596 RVA: 0x00106592 File Offset: 0x00104792
		[DataMember]
		public bool NotificationPhoneNumberVerified { get; set; }

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x0010659B File Offset: 0x0010479B
		// (set) Token: 0x06004C8E RID: 19598 RVA: 0x001065A3 File Offset: 0x001047A3
		[DataMember]
		public RegionData[] RegionList { get; set; }

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x001065AC File Offset: 0x001047AC
		// (set) Token: 0x06004C90 RID: 19600 RVA: 0x001065B4 File Offset: 0x001047B4
		[DataMember]
		public CarrierData[] VoiceMailCarrierList { get; set; }
	}
}
