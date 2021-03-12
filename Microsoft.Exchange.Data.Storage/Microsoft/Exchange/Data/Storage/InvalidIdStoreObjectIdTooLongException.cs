using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D7B RID: 3451
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdStoreObjectIdTooLongException : StoragePermanentException
	{
		// Token: 0x06007717 RID: 30487 RVA: 0x0020DB00 File Offset: 0x0020BD00
		internal InvalidIdStoreObjectIdTooLongException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x0020DB0D File Offset: 0x0020BD0D
		private InvalidIdStoreObjectIdTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
