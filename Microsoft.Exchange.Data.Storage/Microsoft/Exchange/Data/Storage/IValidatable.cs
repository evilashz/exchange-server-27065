using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IValidatable
	{
		// Token: 0x06000418 RID: 1048
		void Validate(ValidationContext context, IList<StoreObjectValidationError> validationErrors);

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000419 RID: 1049
		bool ValidateAllProperties { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600041A RID: 1050
		Schema Schema { get; }
	}
}
