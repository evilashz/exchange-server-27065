using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public sealed class EsentRecordDeletedException : EsentStateException
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x0001227A File Offset: 0x0001047A
		public EsentRecordDeletedException() : base("Record has been deleted", JET_err.RecordDeleted)
		{
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001228C File Offset: 0x0001048C
		private EsentRecordDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
