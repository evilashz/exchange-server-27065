using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D7 RID: 471
	[Serializable]
	public sealed class EsentNullInvalidException : EsentUsageException
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00013416 File Offset: 0x00011616
		public EsentNullInvalidException() : base("Null not valid", JET_err.NullInvalid)
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00013428 File Offset: 0x00011628
		private EsentNullInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
