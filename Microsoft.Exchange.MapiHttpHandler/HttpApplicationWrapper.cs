using System;
using System.Web;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000019 RID: 25
	public class HttpApplicationWrapper : HttpApplicationBase
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000074FB File Offset: 0x000056FB
		public HttpApplicationWrapper(HttpApplication application)
		{
			this.application = application;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000750A File Offset: 0x0000570A
		public HttpApplication Instance
		{
			get
			{
				return this.application;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007512 File Offset: 0x00005712
		public override void CompleteRequest()
		{
			this.application.CompleteRequest();
		}

		// Token: 0x0400009D RID: 157
		private readonly HttpApplication application;
	}
}
