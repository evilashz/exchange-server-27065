using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000206 RID: 518
	internal class NonUniqueProxyAddressError : NonUniqueAddressError
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x00073448 File Offset: 0x00071648
		public NonUniqueProxyAddressError(LocalizedString description, ObjectId objectId, string dataSourceName) : base(description, objectId, description)
		{
		}
	}
}
