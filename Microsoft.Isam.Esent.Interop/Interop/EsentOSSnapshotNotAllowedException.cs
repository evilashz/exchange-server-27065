using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000214 RID: 532
	[Serializable]
	public sealed class EsentOSSnapshotNotAllowedException : EsentStateException
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x00013AC2 File Offset: 0x00011CC2
		public EsentOSSnapshotNotAllowedException() : base("OS Shadow copy not allowed (backup or recovery in progress)", JET_err.OSSnapshotNotAllowed)
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00013AD4 File Offset: 0x00011CD4
		private EsentOSSnapshotNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
