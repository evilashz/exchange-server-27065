using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000205 RID: 517
	internal class NonUniqueAddressError : ObjectValidationError
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x00073438 File Offset: 0x00071638
		public NonUniqueAddressError(LocalizedString description, ObjectId objectId, string dataSourceName) : base(description, objectId, description)
		{
		}
	}
}
