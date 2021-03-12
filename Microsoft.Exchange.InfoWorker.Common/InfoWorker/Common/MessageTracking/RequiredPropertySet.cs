using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200029F RID: 671
	internal static class RequiredPropertySet
	{
		// Token: 0x04000CA7 RID: 3239
		internal const RequiredProperty None = RequiredProperty.None;

		// Token: 0x04000CA8 RID: 3240
		internal const RequiredProperty ServerDomainTarget = RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target;

		// Token: 0x04000CA9 RID: 3241
		internal const RequiredProperty ServerDomainData = RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Data;
	}
}
