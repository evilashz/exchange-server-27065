using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public sealed class EsentCommittedLogFilesMissingException : EsentCorruptionException
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x00011F32 File Offset: 0x00010132
		public EsentCommittedLogFilesMissingException() : base("One or more logs that were committed to this database, are missing.  These log files are required to maintain durable ACID semantics, but not required to maintain consistency if the JET_bitReplayIgnoreLostLogs bit is specified during recovery.", JET_err.CommittedLogFilesMissing)
		{
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00011F44 File Offset: 0x00010144
		private EsentCommittedLogFilesMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
