using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000215 RID: 533
	[Serializable]
	public sealed class EsentOSSnapshotInvalidSnapIdException : EsentUsageException
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x00013ADE File Offset: 0x00011CDE
		public EsentOSSnapshotInvalidSnapIdException() : base("invalid JET_OSSNAPID", JET_err.OSSnapshotInvalidSnapId)
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00013AF0 File Offset: 0x00011CF0
		private EsentOSSnapshotInvalidSnapIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
