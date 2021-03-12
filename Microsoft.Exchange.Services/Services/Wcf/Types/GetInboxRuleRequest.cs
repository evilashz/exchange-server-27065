using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA8 RID: 2728
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetInboxRuleRequest : BaseJsonRequest
	{
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x00106A02 File Offset: 0x00104C02
		// (set) Token: 0x06004CF1 RID: 19697 RVA: 0x00106A0A File Offset: 0x00104C0A
		[DataMember]
		public string DescriptionTimeFormat { get; set; }

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x00106A13 File Offset: 0x00104C13
		// (set) Token: 0x06004CF3 RID: 19699 RVA: 0x00106A1B File Offset: 0x00104C1B
		[DataMember]
		public string DescriptionTimeZone { get; set; }

		// Token: 0x06004CF4 RID: 19700 RVA: 0x00106A24 File Offset: 0x00104C24
		public override string ToString()
		{
			return string.Format("GetInboxRuleRequest: DescriptionTimeFormat = {0}, DescriptionTimeZone = {1}", this.DescriptionTimeZone, this.DescriptionTimeFormat);
		}
	}
}
