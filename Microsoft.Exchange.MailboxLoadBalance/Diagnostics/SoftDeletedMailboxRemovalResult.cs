using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000069 RID: 105
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMailboxRemovalResult
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000A950 File Offset: 0x00008B50
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000A958 File Offset: 0x00008B58
		[DataMember]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000A961 File Offset: 0x00008B61
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000A969 File Offset: 0x00008B69
		[DataMember]
		public Guid DatabaseGuid { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000A972 File Offset: 0x00008B72
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000A97A File Offset: 0x00008B7A
		[DataMember]
		public bool Success { get; set; }
	}
}
