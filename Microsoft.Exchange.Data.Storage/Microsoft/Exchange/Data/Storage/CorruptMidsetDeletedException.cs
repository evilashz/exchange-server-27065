using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000721 RID: 1825
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CorruptMidsetDeletedException : StoragePermanentException
	{
		// Token: 0x060047B0 RID: 18352 RVA: 0x00130411 File Offset: 0x0012E611
		public CorruptMidsetDeletedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0013041B File Offset: 0x0012E61B
		protected CorruptMidsetDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
