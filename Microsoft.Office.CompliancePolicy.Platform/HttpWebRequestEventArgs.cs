using System;
using System.Net;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000040 RID: 64
	internal class HttpWebRequestEventArgs : EventArgs
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00005D0E File Offset: 0x00003F0E
		public HttpWebRequestEventArgs(HttpWebRequest request)
		{
			this.Request = request;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005D1D File Offset: 0x00003F1D
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005D25 File Offset: 0x00003F25
		public HttpWebRequest Request { get; private set; }
	}
}
