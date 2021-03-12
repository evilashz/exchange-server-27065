using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000224 RID: 548
	[DataContract]
	public class MigrationReportGroupDetails
	{
		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x0007C25E File Offset: 0x0007A45E
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x0007C266 File Offset: 0x0007A466
		[DataMember]
		public string Data { get; set; }
	}
}
