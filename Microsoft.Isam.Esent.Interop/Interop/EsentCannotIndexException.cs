using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200015E RID: 350
	[Serializable]
	public sealed class EsentCannotIndexException : EsentUsageException
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x000126DA File Offset: 0x000108DA
		public EsentCannotIndexException() : base("Cannot index escrow column", JET_err.CannotIndex)
		{
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000126EC File Offset: 0x000108EC
		private EsentCannotIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
