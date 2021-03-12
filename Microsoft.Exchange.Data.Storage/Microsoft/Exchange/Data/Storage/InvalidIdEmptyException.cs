using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D7F RID: 3455
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdEmptyException : StoragePermanentException
	{
		// Token: 0x0600771F RID: 30495 RVA: 0x0020DB5C File Offset: 0x0020BD5C
		internal InvalidIdEmptyException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x06007720 RID: 30496 RVA: 0x0020DB69 File Offset: 0x0020BD69
		private InvalidIdEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
