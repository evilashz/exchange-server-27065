using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000218 RID: 536
	[Serializable]
	public sealed class EsentInvalidLogDataSequenceException : EsentStateException
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x00013B32 File Offset: 0x00011D32
		public EsentInvalidLogDataSequenceException() : base("Some how the log data provided got out of sequence with the current state of the instance", JET_err.InvalidLogDataSequence)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00013B44 File Offset: 0x00011D44
		private EsentInvalidLogDataSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
