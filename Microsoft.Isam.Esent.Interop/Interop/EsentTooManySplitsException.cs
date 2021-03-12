using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000205 RID: 517
	[Serializable]
	public sealed class EsentTooManySplitsException : EsentObsoleteException
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x0001391E File Offset: 0x00011B1E
		public EsentTooManySplitsException() : base("Infinite split", JET_err.TooManySplits)
		{
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00013930 File Offset: 0x00011B30
		private EsentTooManySplitsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
