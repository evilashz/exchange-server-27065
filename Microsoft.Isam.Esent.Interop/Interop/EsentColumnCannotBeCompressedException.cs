using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	public sealed class EsentColumnCannotBeCompressedException : EsentUsageException
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00013662 File Offset: 0x00011862
		public EsentColumnCannotBeCompressedException() : base("Only JET_coltypLongText and JET_coltypLongBinary columns can be compressed", JET_err.ColumnCannotBeCompressed)
		{
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00013674 File Offset: 0x00011874
		private EsentColumnCannotBeCompressedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
