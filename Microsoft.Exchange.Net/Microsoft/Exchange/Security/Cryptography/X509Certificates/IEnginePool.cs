using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A90 RID: 2704
	internal interface IEnginePool
	{
		// Token: 0x06003A63 RID: 14947
		void ReturnTo(SafeChainEngineHandle item);
	}
}
