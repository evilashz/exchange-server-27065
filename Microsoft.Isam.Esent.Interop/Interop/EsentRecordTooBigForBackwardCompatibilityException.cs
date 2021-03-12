using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	public sealed class EsentRecordTooBigForBackwardCompatibilityException : EsentStateException
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x000129EA File Offset: 0x00010BEA
		public EsentRecordTooBigForBackwardCompatibilityException() : base("record would be too big if represented in a database format from a previous version of Jet", JET_err.RecordTooBigForBackwardCompatibility)
		{
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000129FC File Offset: 0x00010BFC
		private EsentRecordTooBigForBackwardCompatibilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
