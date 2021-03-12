using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000087 RID: 135
	[DataContract]
	internal sealed class CreateCalendarEventActionResult : ReplayActionResult
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000AA60 File Offset: 0x00008C60
		public CreateCalendarEventActionResult()
		{
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000AA68 File Offset: 0x00008C68
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000AA70 File Offset: 0x00008C70
		[DataMember]
		public byte[] SourceItemId { get; set; }

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000AA79 File Offset: 0x00008C79
		public CreateCalendarEventActionResult(byte[] sourceItemId)
		{
			this.SourceItemId = sourceItemId;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000AA88 File Offset: 0x00008C88
		public override string ToString()
		{
			return base.ToString() + ", RemoteItemId: " + TraceUtils.DumpEntryId(this.SourceItemId);
		}
	}
}
