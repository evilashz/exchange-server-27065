using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200016D RID: 365
	[DataContract]
	public class CreateAttachmentNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00033218 File Offset: 0x00031418
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x00033220 File Offset: 0x00031420
		[DataMember]
		public string Id { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00033229 File Offset: 0x00031429
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x00033231 File Offset: 0x00031431
		[DataMember]
		public AttachmentResultCode ResultCode { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0003323A File Offset: 0x0003143A
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x00033242 File Offset: 0x00031442
		[DataMember]
		public FileAttachmentDataProviderItem Item { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003324B File Offset: 0x0003144B
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x00033253 File Offset: 0x00031453
		[DataMember]
		public CreateAttachmentResponse Response { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003325C File Offset: 0x0003145C
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x00033264 File Offset: 0x00031464
		public byte[] Bytes { get; set; }
	}
}
