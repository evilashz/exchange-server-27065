using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F48 RID: 3912
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditQueryStrategy<T>
	{
		// Token: 0x06008657 RID: 34391
		bool RecordFilter(IReadOnlyPropertyBag propertyBag, out bool stopNow);

		// Token: 0x06008658 RID: 34392
		T Convert(IReadOnlyPropertyBag propertyBag);

		// Token: 0x06008659 RID: 34393
		Exception GetTimeoutException(TimeSpan timeout);

		// Token: 0x0600865A RID: 34394
		Exception GetQueryFailedException();
	}
}
