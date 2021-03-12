using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D2 RID: 466
	[Serializable]
	public sealed class EsentIndexTuplesCannotRetrieveFromIndexException : EsentUsageException
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0001338A File Offset: 0x0001158A
		public EsentIndexTuplesCannotRetrieveFromIndexException() : base("cannot call RetrieveColumn() with RetrieveFromIndex on a tuple index", JET_err.IndexTuplesCannotRetrieveFromIndex)
		{
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001339C File Offset: 0x0001159C
		private EsentIndexTuplesCannotRetrieveFromIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
