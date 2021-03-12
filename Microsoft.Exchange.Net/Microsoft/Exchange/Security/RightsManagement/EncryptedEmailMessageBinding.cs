using System;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000989 RID: 2441
	internal abstract class EncryptedEmailMessageBinding
	{
		// Token: 0x06003515 RID: 13589
		public abstract IStorage ConvertToEncryptedStorage(IStream stream, bool create);
	}
}
