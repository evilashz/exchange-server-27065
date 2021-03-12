using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000067 RID: 103
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class QueueManagerDiagnosticData
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000A8EE File Offset: 0x00008AEE
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000A8F6 File Offset: 0x00008AF6
		[DataMember]
		public IList<QueueDiagnosticData> ProcessingQueues { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A8FF File Offset: 0x00008AFF
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000A907 File Offset: 0x00008B07
		[DataMember]
		public IList<QueueDiagnosticData> InjectionQueues { get; set; }
	}
}
