using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F6 RID: 2550
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NotificationSettingsRequest
	{
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x00100D8C File Offset: 0x000FEF8C
		// (set) Token: 0x06004804 RID: 18436 RVA: 0x00100D94 File Offset: 0x000FEF94
		[DataMember]
		public bool EnableReminders { get; set; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x00100D9D File Offset: 0x000FEF9D
		// (set) Token: 0x06004806 RID: 18438 RVA: 0x00100DA5 File Offset: 0x000FEFA5
		[DataMember]
		public bool EnableReminderSound { get; set; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00100DAE File Offset: 0x000FEFAE
		// (set) Token: 0x06004808 RID: 18440 RVA: 0x00100DB6 File Offset: 0x000FEFB6
		[DataMember]
		public int NewItemNotify { get; set; }
	}
}
