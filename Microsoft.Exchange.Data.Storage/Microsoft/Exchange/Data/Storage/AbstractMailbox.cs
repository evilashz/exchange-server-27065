using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D6 RID: 726
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractMailbox : AbstractStorePropertyBag, IXSOMailbox, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x00086343 File Offset: 0x00084543
		public virtual void Save()
		{
			throw new NotImplementedException();
		}
	}
}
