using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000213 RID: 531
	[Serializable]
	public sealed class EsentOSSnapshotTimeOutException : EsentOperationException
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x00013AA6 File Offset: 0x00011CA6
		public EsentOSSnapshotTimeOutException() : base("OS Shadow copy ended with time-out", JET_err.OSSnapshotTimeOut)
		{
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00013AB8 File Offset: 0x00011CB8
		private EsentOSSnapshotTimeOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
