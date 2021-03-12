using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000723 RID: 1827
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CustomSerializationException : StoragePermanentException
	{
		// Token: 0x060047BA RID: 18362 RVA: 0x00130495 File Offset: 0x0012E695
		public CustomSerializationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x0013049E File Offset: 0x0012E69E
		public CustomSerializationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
