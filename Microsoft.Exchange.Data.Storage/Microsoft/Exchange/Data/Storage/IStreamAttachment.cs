using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E3 RID: 739
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStreamAttachment : IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06001F88 RID: 8072
		Stream GetContentStream();
	}
}
