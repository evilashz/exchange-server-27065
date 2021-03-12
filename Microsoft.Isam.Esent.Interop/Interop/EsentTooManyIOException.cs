using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public sealed class EsentTooManyIOException : EsentResourceException
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x000114DD File Offset: 0x0000F6DD
		public EsentTooManyIOException() : base("System busy due to too many IOs", JET_err.TooManyIO)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000114EC File Offset: 0x0000F6EC
		private EsentTooManyIOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
