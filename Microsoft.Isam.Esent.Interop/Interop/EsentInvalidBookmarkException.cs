using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200014C RID: 332
	[Serializable]
	public sealed class EsentInvalidBookmarkException : EsentUsageException
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x000124E2 File Offset: 0x000106E2
		public EsentInvalidBookmarkException() : base("Invalid bookmark", JET_err.InvalidBookmark)
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000124F4 File Offset: 0x000106F4
		private EsentInvalidBookmarkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
