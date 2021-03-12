using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000121 RID: 289
	[Serializable]
	public sealed class EsentExistingLogFileIsNotContiguousException : EsentInconsistentException
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x0001202E File Offset: 0x0001022E
		public EsentExistingLogFileIsNotContiguousException() : base("Existing log file is not contiguous", JET_err.ExistingLogFileIsNotContiguous)
		{
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00012040 File Offset: 0x00010240
		private EsentExistingLogFileIsNotContiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
