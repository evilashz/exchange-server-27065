using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D79 RID: 3449
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdMalformedException : StoragePermanentException
	{
		// Token: 0x06007712 RID: 30482 RVA: 0x0020DAC4 File Offset: 0x0020BCC4
		internal InvalidIdMalformedException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x06007713 RID: 30483 RVA: 0x0020DAD1 File Offset: 0x0020BCD1
		internal InvalidIdMalformedException(Exception innerException) : base(LocalizedString.Empty, innerException)
		{
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x0020DADF File Offset: 0x0020BCDF
		private InvalidIdMalformedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
