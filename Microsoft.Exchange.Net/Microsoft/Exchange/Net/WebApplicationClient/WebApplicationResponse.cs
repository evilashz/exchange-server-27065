using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B17 RID: 2839
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class WebApplicationResponse
	{
		// Token: 0x06003D5A RID: 15706 RVA: 0x0009FF77 File Offset: 0x0009E177
		public virtual void SetResponse(HttpWebResponse response)
		{
			this.StatusCode = response.StatusCode;
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x0009FF85 File Offset: 0x0009E185
		// (set) Token: 0x06003D5C RID: 15708 RVA: 0x0009FF8D File Offset: 0x0009E18D
		public HttpStatusCode StatusCode { get; private set; }
	}
}
