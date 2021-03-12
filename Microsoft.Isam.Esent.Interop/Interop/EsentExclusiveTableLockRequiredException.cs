using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public sealed class EsentExclusiveTableLockRequiredException : EsentUsageException
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x00013096 File Offset: 0x00011296
		public EsentExclusiveTableLockRequiredException() : base("Must have exclusive lock on table.", JET_err.ExclusiveTableLockRequired)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000130A8 File Offset: 0x000112A8
		private EsentExclusiveTableLockRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
