using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	public sealed class EsentIndexTuplesTextBinaryColumnsOnlyException : EsentUsageException
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x00013336 File Offset: 0x00011536
		public EsentIndexTuplesTextBinaryColumnsOnlyException() : base("tuple index must be on a text/binary column", JET_err.IndexTuplesTextBinaryColumnsOnly)
		{
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00013348 File Offset: 0x00011548
		private EsentIndexTuplesTextBinaryColumnsOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
