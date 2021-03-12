using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E38 RID: 3640
	public class ErrorResponse
	{
		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x00123FD8 File Offset: 0x001221D8
		// (set) Token: 0x06005DDE RID: 24030 RVA: 0x00123FE0 File Offset: 0x001221E0
		public HttpStatusCode HttpStatusCode { get; set; }

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x00123FE9 File Offset: 0x001221E9
		// (set) Token: 0x06005DE0 RID: 24032 RVA: 0x00123FF1 File Offset: 0x001221F1
		public ErrorDetails ErrorDetails { get; set; }
	}
}
