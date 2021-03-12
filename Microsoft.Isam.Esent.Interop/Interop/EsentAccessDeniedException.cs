using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000204 RID: 516
	[Serializable]
	public sealed class EsentAccessDeniedException : EsentObsoleteException
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x00013902 File Offset: 0x00011B02
		public EsentAccessDeniedException() : base("Access denied", JET_err.AccessDenied)
		{
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00013914 File Offset: 0x00011B14
		private EsentAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
