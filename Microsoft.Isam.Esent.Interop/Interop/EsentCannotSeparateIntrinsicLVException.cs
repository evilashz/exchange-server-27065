using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	public sealed class EsentCannotSeparateIntrinsicLVException : EsentUsageException
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00011736 File Offset: 0x0000F936
		public EsentCannotSeparateIntrinsicLVException() : base("illegal attempt to separate an LV which must be intrinsic", JET_err.CannotSeparateIntrinsicLV)
		{
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00011748 File Offset: 0x0000F948
		private EsentCannotSeparateIntrinsicLVException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
