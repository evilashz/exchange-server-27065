using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public sealed class EsentInvalidIndexIdException : EsentUsageException
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x000132C6 File Offset: 0x000114C6
		public EsentInvalidIndexIdException() : base("Illegal index id", JET_err.InvalidIndexId)
		{
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000132D8 File Offset: 0x000114D8
		private EsentInvalidIndexIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
