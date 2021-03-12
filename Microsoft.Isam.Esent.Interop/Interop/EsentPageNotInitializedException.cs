using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public sealed class EsentPageNotInitializedException : EsentCorruptionException
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x000122B2 File Offset: 0x000104B2
		public EsentPageNotInitializedException() : base("Blank database page", JET_err.PageNotInitialized)
		{
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000122C4 File Offset: 0x000104C4
		private EsentPageNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
