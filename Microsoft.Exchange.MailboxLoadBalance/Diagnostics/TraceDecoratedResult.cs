using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200006B RID: 107
	[DataContract]
	internal struct TraceDecoratedResult
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000A9D7 File Offset: 0x00008BD7
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000A9DF File Offset: 0x00008BDF
		[DataMember]
		public object Result { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		[DataMember]
		public IList<DiagnosticLog> Logs { get; set; }
	}
}
