using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000018 RID: 24
	internal interface IDisposableDocument : IDocument, IPropertyBag, IReadOnlyPropertyBag, IDisposeTrackable, IDisposable
	{
	}
}
