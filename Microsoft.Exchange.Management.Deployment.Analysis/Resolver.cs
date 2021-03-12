using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000037 RID: 55
	// (Invoke) Token: 0x0600019C RID: 412
	internal delegate T Resolver<T>(T originalValue, T currentValue, T updatedValue);
}
