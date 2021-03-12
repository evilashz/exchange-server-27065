using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000098 RID: 152
	public class Location
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000743D File Offset: 0x0000563D
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00007445 File Offset: 0x00005645
		public string DisplayName { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000744E File Offset: 0x0000564E
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00007456 File Offset: 0x00005656
		public string Annotation { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000745F File Offset: 0x0000565F
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00007467 File Offset: 0x00005667
		public PostalAddress Address { get; set; }
	}
}
