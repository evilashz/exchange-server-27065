using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000066 RID: 102
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class QueueDiagnosticData
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000A891 File Offset: 0x00008A91
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000A899 File Offset: 0x00008A99
		[DataMember]
		public Guid QueueGuid { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000A8A2 File Offset: 0x00008AA2
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000A8AA File Offset: 0x00008AAA
		[DataMember]
		public int QueueLength { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000A8B3 File Offset: 0x00008AB3
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000A8BB File Offset: 0x00008ABB
		[DataMember]
		public RequestDiagnosticData CurrentRequest { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000A8CC File Offset: 0x00008ACC
		[DataMember]
		public IList<RequestDiagnosticData> Requests { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000A8D5 File Offset: 0x00008AD5
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000A8DD File Offset: 0x00008ADD
		[DataMember]
		public bool IsActive { get; set; }
	}
}
