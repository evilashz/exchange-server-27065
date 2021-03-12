using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	public sealed class EsentPermissionDeniedException : EsentUsageException
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x0001385A File Offset: 0x00011A5A
		public EsentPermissionDeniedException() : base("Permission denied", JET_err.PermissionDenied)
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001386C File Offset: 0x00011A6C
		private EsentPermissionDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
