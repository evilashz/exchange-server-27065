using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B22 RID: 2850
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class HttpWebRequestEventArgs : EventArgs
	{
		// Token: 0x06003D89 RID: 15753 RVA: 0x000A0480 File Offset: 0x0009E680
		public HttpWebRequestEventArgs(HttpWebRequest request)
		{
			this.Request = request;
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000A048F File Offset: 0x0009E68F
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000A0497 File Offset: 0x0009E697
		public HttpWebRequest Request { get; private set; }
	}
}
