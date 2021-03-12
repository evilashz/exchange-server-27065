using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200021A RID: 538
	internal interface ICriticalFeature
	{
		// Token: 0x060012BB RID: 4795
		bool IsCriticalException(Exception ex);
	}
}
