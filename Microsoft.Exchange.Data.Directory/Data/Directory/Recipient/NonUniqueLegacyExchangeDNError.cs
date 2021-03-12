using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000207 RID: 519
	internal class NonUniqueLegacyExchangeDNError : NonUniqueAddressError
	{
		// Token: 0x06001B4B RID: 6987 RVA: 0x00073458 File Offset: 0x00071658
		public NonUniqueLegacyExchangeDNError(LocalizedString description, ObjectId objectId, string dataSourceName) : base(description, objectId, description)
		{
		}
	}
}
