using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	public sealed class EsentInvalidLogSequenceException : EsentCorruptionException
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x000118F6 File Offset: 0x0000FAF6
		public EsentInvalidLogSequenceException() : base("Timestamp in next log does not match expected", JET_err.InvalidLogSequence)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00011908 File Offset: 0x0000FB08
		private EsentInvalidLogSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
