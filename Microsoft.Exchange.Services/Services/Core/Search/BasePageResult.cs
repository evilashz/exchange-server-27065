using System;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200025B RID: 603
	internal class BasePageResult
	{
		// Token: 0x06000FC5 RID: 4037 RVA: 0x0004CFEC File Offset: 0x0004B1EC
		public BasePageResult(BaseQueryView view)
		{
			this.view = view;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0004CFFB File Offset: 0x0004B1FB
		public BaseQueryView View
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x04000BD9 RID: 3033
		private BaseQueryView view;
	}
}
