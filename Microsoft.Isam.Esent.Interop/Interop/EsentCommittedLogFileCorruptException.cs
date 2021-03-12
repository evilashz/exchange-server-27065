using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	public sealed class EsentCommittedLogFileCorruptException : EsentCorruptionException
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00011F86 File Offset: 0x00010186
		public EsentCommittedLogFileCorruptException() : base("One or more logs were found to be corrupt during recovery.  These log files are required to maintain durable ACID semantics, but not required to maintain consistency if the JET_bitIgnoreLostLogs bit and JET_paramDeleteOutOfRangeLogs is specified during recovery.", JET_err.CommittedLogFileCorrupt)
		{
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00011F98 File Offset: 0x00010198
		private EsentCommittedLogFileCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
