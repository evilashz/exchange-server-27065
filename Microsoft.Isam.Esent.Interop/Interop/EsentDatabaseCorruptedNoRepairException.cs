using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public sealed class EsentDatabaseCorruptedNoRepairException : EsentUsageException
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x00012E4A File Offset: 0x0001104A
		public EsentDatabaseCorruptedNoRepairException() : base("Corrupted db but repair not allowed", JET_err.DatabaseCorruptedNoRepair)
		{
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00012E5C File Offset: 0x0001105C
		private EsentDatabaseCorruptedNoRepairException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
