using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B66 RID: 2918
	internal class CompletedAsyncResult : AsyncResultBase
	{
		// Token: 0x060052B3 RID: 21171 RVA: 0x0010B892 File Offset: 0x00109A92
		public CompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
			base.Complete(true);
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0010B8A3 File Offset: 0x00109AA3
		public static void End(IAsyncResult result)
		{
			AsyncResultBase.End<CompletedAsyncResult>(result);
		}
	}
}
