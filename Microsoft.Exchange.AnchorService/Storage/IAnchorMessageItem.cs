using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorMessageItem : IAnchorStoreObject, IDisposable, IPropertyBag, IReadOnlyPropertyBag, IAnchorAttachmentMessage
	{
	}
}
