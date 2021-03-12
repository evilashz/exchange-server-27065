using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	public sealed class EsentBadParentPageLinkException : EsentCorruptionException
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0001161E File Offset: 0x0000F81E
		public EsentBadParentPageLinkException() : base("Database corrupted", JET_err.BadParentPageLink)
		{
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00011630 File Offset: 0x0000F830
		private EsentBadParentPageLinkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
