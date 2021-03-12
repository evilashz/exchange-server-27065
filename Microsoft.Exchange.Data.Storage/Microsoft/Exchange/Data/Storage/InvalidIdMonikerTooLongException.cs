using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D7A RID: 3450
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdMonikerTooLongException : StoragePermanentException
	{
		// Token: 0x06007715 RID: 30485 RVA: 0x0020DAE9 File Offset: 0x0020BCE9
		internal InvalidIdMonikerTooLongException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x0020DAF6 File Offset: 0x0020BCF6
		private InvalidIdMonikerTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
