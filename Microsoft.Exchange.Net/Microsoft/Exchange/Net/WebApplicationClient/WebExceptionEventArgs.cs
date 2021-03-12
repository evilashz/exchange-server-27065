using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B2D RID: 2861
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class WebExceptionEventArgs : HttpWebResponseEventArgs
	{
		// Token: 0x06003DBA RID: 15802 RVA: 0x000A0B5E File Offset: 0x0009ED5E
		public WebExceptionEventArgs(HttpWebRequest request, WebException exception) : base(request, exception.Response as HttpWebResponse)
		{
			this.Exception = exception;
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x000A0B79 File Offset: 0x0009ED79
		// (set) Token: 0x06003DBC RID: 15804 RVA: 0x000A0B81 File Offset: 0x0009ED81
		public WebException Exception { get; private set; }
	}
}
