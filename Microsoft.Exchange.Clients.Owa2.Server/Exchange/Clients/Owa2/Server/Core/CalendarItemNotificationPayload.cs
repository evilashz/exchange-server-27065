using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000162 RID: 354
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	internal class CalendarItemNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00031B07 File Offset: 0x0002FD07
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x00031B0F File Offset: 0x0002FD0F
		[DataMember]
		public string FolderId { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00031B18 File Offset: 0x0002FD18
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x00031B20 File Offset: 0x0002FD20
		[DataMember]
		public EwsCalendarItemType Item { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00031B29 File Offset: 0x0002FD29
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00031B31 File Offset: 0x0002FD31
		[DataMember(EmitDefaultValue = false)]
		public bool Reload { get; set; }
	}
}
