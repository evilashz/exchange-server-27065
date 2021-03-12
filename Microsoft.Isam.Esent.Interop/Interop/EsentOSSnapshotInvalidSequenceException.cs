using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000212 RID: 530
	[Serializable]
	public sealed class EsentOSSnapshotInvalidSequenceException : EsentUsageException
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x00013A8A File Offset: 0x00011C8A
		public EsentOSSnapshotInvalidSequenceException() : base("OS Shadow copy API used in an invalid sequence", JET_err.OSSnapshotInvalidSequence)
		{
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00013A9C File Offset: 0x00011C9C
		private EsentOSSnapshotInvalidSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
