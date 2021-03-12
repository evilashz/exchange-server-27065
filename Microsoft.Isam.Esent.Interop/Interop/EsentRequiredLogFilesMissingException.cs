using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public sealed class EsentRequiredLogFilesMissingException : EsentInconsistentException
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00011BB2 File Offset: 0x0000FDB2
		public EsentRequiredLogFilesMissingException() : base("The required log files for recovery is missing.", JET_err.RequiredLogFilesMissing)
		{
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		private EsentRequiredLogFilesMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
