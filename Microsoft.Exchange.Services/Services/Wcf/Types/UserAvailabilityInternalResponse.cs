using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A3E RID: 2622
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UserAvailabilityInternalResponse
	{
		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x0010315D File Offset: 0x0010135D
		// (set) Token: 0x06004A0C RID: 18956 RVA: 0x00103165 File Offset: 0x00101365
		[DataMember]
		public ResponseMessage ResponseMessage { get; set; }

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x0010316E File Offset: 0x0010136E
		// (set) Token: 0x06004A0E RID: 18958 RVA: 0x00103176 File Offset: 0x00101376
		[DataMember]
		public UserAvailabilityCalendarView CalendarView { get; set; }
	}
}
