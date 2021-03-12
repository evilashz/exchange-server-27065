using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public sealed class EsentBadEmptyPageException : EsentCorruptionException
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x000116FE File Offset: 0x0000F8FE
		public EsentBadEmptyPageException() : base("Database corrupted. Searching an unexpectedly empty page.", JET_err.BadEmptyPage)
		{
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00011710 File Offset: 0x0000F910
		private EsentBadEmptyPageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
