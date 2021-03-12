using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public sealed class EsentBadBookmarkException : EsentObsoleteException
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x000115E6 File Offset: 0x0000F7E6
		public EsentBadBookmarkException() : base("Bookmark has no corresponding address in database", JET_err.BadBookmark)
		{
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000115F8 File Offset: 0x0000F7F8
		private EsentBadBookmarkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
