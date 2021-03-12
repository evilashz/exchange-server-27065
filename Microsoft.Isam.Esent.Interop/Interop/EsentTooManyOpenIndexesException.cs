using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	public sealed class EsentTooManyOpenIndexesException : EsentMemoryException
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0001323A File Offset: 0x0001143A
		public EsentTooManyOpenIndexesException() : base("Out of index description blocks", JET_err.TooManyOpenIndexes)
		{
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001324C File Offset: 0x0001144C
		private EsentTooManyOpenIndexesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
