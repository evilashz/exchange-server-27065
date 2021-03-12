using System;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x0200000A RID: 10
	public abstract class HttpApplicationBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000024F0 File Offset: 0x000006F0
		public virtual void CompleteRequest()
		{
			throw new NotImplementedException();
		}
	}
}
