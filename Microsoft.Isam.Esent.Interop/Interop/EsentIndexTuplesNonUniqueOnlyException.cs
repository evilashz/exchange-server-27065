using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001CE RID: 462
	[Serializable]
	public sealed class EsentIndexTuplesNonUniqueOnlyException : EsentUsageException
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0001331A File Offset: 0x0001151A
		public EsentIndexTuplesNonUniqueOnlyException() : base("tuple index must be a non-unique index", JET_err.IndexTuplesNonUniqueOnly)
		{
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001332C File Offset: 0x0001152C
		private EsentIndexTuplesNonUniqueOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
