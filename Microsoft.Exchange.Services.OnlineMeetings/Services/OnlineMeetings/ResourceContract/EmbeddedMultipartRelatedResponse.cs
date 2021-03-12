using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000084 RID: 132
	internal class EmbeddedMultipartRelatedResponse
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00009FD6 File Offset: 0x000081D6
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00009FDE File Offset: 0x000081DE
		public object Root { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00009FE7 File Offset: 0x000081E7
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00009FEF File Offset: 0x000081EF
		public Dictionary<string, object> Parts { get; set; }
	}
}
