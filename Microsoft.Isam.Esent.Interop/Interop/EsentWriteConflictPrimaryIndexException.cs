using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	public sealed class EsentWriteConflictPrimaryIndexException : EsentStateException
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x0001295E File Offset: 0x00010B5E
		public EsentWriteConflictPrimaryIndexException() : base("Update attempted on uncommitted primary index", JET_err.WriteConflictPrimaryIndex)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00012970 File Offset: 0x00010B70
		private EsentWriteConflictPrimaryIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
