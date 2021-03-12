using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000035 RID: 53
	internal interface IPipelineComponent : IDocumentProcessor, INotifyFailed
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000120 RID: 288
		string Name { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000121 RID: 289
		string Description { get; }
	}
}
