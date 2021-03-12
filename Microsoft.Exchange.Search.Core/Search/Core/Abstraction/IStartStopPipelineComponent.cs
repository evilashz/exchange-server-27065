using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200003F RID: 63
	internal interface IStartStopPipelineComponent : IPipelineComponent, IDocumentProcessor, INotifyFailed, IStartStop, IDisposable
	{
	}
}
