using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F9 RID: 505
	[Serializable]
	public sealed class EsentTooManySortsException : EsentMemoryException
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x000137CE File Offset: 0x000119CE
		public EsentTooManySortsException() : base("Too many sort processes", JET_err.TooManySorts)
		{
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000137E0 File Offset: 0x000119E0
		private EsentTooManySortsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
