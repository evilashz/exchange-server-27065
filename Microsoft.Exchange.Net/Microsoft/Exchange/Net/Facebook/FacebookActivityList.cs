using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000712 RID: 1810
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookActivityList
	{
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00046313 File Offset: 0x00044513
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x0004631B File Offset: 0x0004451B
		[DataMember(Name = "data")]
		public List<FacebookActivity> Activities { get; set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x00046324 File Offset: 0x00044524
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x0004632C File Offset: 0x0004452C
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
