using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public sealed class EsentDatabaseLeakInSpaceException : EsentStateException
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x000116E2 File Offset: 0x0000F8E2
		public EsentDatabaseLeakInSpaceException() : base("Some database pages have become unreachable even from the avail tree, only an offline defragmentation can return the lost space.", JET_err.DatabaseLeakInSpace)
		{
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000116F4 File Offset: 0x0000F8F4
		private EsentDatabaseLeakInSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
