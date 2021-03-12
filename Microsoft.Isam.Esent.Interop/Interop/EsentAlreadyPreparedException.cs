using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F2 RID: 498
	[Serializable]
	public sealed class EsentAlreadyPreparedException : EsentUsageException
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x0001370A File Offset: 0x0001190A
		public EsentAlreadyPreparedException() : base("Attempted to update record when record update was already in progress", JET_err.AlreadyPrepared)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001371C File Offset: 0x0001191C
		private EsentAlreadyPreparedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
