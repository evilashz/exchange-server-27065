using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C4 RID: 964
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMeetingMessageInstance : IMeetingMessage, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
	}
}
