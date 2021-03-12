using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public sealed class EsentConsistentTimeMismatchException : EsentInconsistentException
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00011C92 File Offset: 0x0000FE92
		public EsentConsistentTimeMismatchException() : base("Database last consistent time unmatched", JET_err.ConsistentTimeMismatch)
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		private EsentConsistentTimeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
