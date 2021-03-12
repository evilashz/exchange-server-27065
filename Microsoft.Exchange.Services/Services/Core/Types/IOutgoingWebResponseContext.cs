using System;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007DD RID: 2013
	public interface IOutgoingWebResponseContext
	{
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003B34 RID: 15156
		// (set) Token: 0x06003B35 RID: 15157
		HttpStatusCode StatusCode { get; set; }

		// Token: 0x17000DFD RID: 3581
		// (set) Token: 0x06003B36 RID: 15158
		string ETag { set; }

		// Token: 0x17000DFE RID: 3582
		// (set) Token: 0x06003B37 RID: 15159
		string Expires { set; }

		// Token: 0x17000DFF RID: 3583
		// (set) Token: 0x06003B38 RID: 15160
		string ContentType { set; }

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003B39 RID: 15161
		NameValueCollection Headers { get; }

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003B3A RID: 15162
		// (set) Token: 0x06003B3B RID: 15163
		bool SuppressContent { get; set; }
	}
}
