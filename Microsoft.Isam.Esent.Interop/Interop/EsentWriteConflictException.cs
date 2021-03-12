using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	public sealed class EsentWriteConflictException : EsentStateException
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0001290A File Offset: 0x00010B0A
		public EsentWriteConflictException() : base("Write lock failed due to outstanding write lock", JET_err.WriteConflict)
		{
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001291C File Offset: 0x00010B1C
		private EsentWriteConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
