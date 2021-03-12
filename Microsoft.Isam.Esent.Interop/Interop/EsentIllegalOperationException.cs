using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public sealed class EsentIllegalOperationException : EsentUsageException
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x00012FD2 File Offset: 0x000111D2
		public EsentIllegalOperationException() : base("Oper. not supported on table", JET_err.IllegalOperation)
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00012FE4 File Offset: 0x000111E4
		private EsentIllegalOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
