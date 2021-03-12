using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E3 RID: 483
	[Serializable]
	public sealed class EsentBadItagSequenceException : EsentStateException
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x00013566 File Offset: 0x00011766
		public EsentBadItagSequenceException() : base("Bad itagSequence for tagged column", JET_err.BadItagSequence)
		{
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00013578 File Offset: 0x00011778
		private EsentBadItagSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
