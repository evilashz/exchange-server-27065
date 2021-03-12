using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	public sealed class EsentBadPageLinkException : EsentCorruptionException
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x000115CA File Offset: 0x0000F7CA
		public EsentBadPageLinkException() : base("Database corrupted", JET_err.BadPageLink)
		{
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000115DC File Offset: 0x0000F7DC
		private EsentBadPageLinkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
