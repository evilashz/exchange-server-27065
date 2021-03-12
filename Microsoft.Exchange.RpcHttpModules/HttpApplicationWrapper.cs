using System;
using System.Web;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x0200000B RID: 11
	public class HttpApplicationWrapper : HttpApplicationBase
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000024FF File Offset: 0x000006FF
		public HttpApplicationWrapper(HttpApplication application)
		{
			this.application = application;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000250E File Offset: 0x0000070E
		public HttpApplication Instance
		{
			get
			{
				return this.application;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002516 File Offset: 0x00000716
		public override void CompleteRequest()
		{
			this.application.CompleteRequest();
		}

		// Token: 0x04000009 RID: 9
		private readonly HttpApplication application;
	}
}
