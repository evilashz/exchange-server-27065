using System;
using System.Net;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000041 RID: 65
	internal class HttpWebResponseEventArgs : HttpWebRequestEventArgs
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00005D2E File Offset: 0x00003F2E
		public HttpWebResponseEventArgs(HttpWebRequest request, HttpWebResponse response) : base(request)
		{
			this.Response = response;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005D3E File Offset: 0x00003F3E
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005D46 File Offset: 0x00003F46
		public HttpWebResponse Response { get; set; }
	}
}
