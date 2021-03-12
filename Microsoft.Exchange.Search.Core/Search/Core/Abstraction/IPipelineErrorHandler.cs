using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200003B RID: 59
	internal interface IPipelineErrorHandler
	{
		// Token: 0x06000125 RID: 293
		DocumentResolution HandleException(IPipelineComponent component, ComponentException exception);
	}
}
