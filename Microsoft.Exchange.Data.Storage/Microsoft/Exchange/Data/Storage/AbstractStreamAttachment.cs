using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E4 RID: 740
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractStreamAttachment : AbstractAttachment, IStreamAttachment, IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06001F89 RID: 8073 RVA: 0x000865FD File Offset: 0x000847FD
		public virtual Stream GetContentStream()
		{
			throw new NotImplementedException();
		}
	}
}
