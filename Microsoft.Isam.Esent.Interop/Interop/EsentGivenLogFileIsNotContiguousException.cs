using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public sealed class EsentGivenLogFileIsNotContiguousException : EsentInconsistentException
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x00011D1E File Offset: 0x0000FF1E
		public EsentGivenLogFileIsNotContiguousException() : base("Restore log file is not contiguous", JET_err.GivenLogFileIsNotContiguous)
		{
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00011D30 File Offset: 0x0000FF30
		private EsentGivenLogFileIsNotContiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
