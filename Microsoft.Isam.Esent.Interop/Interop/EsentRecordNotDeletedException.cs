using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200015F RID: 351
	[Serializable]
	public sealed class EsentRecordNotDeletedException : EsentOperationException
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x000126F6 File Offset: 0x000108F6
		public EsentRecordNotDeletedException() : base("Record has not been deleted", JET_err.RecordNotDeleted)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00012708 File Offset: 0x00010908
		private EsentRecordNotDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
