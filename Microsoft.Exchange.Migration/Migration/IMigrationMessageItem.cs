using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C1 RID: 193
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationMessageItem : IMigrationStoreObject, IDisposable, IPropertyBag, IReadOnlyPropertyBag, IMigrationAttachmentMessage
	{
	}
}
