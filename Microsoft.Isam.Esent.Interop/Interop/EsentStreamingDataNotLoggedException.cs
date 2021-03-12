using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public sealed class EsentStreamingDataNotLoggedException : EsentObsoleteException
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00011C5A File Offset: 0x0000FE5A
		public EsentStreamingDataNotLoggedException() : base("Illegal attempt to replay a streaming file operation where the data wasn't logged. Probably caused by an attempt to roll-forward with circular logging enabled", JET_err.StreamingDataNotLogged)
		{
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00011C6C File Offset: 0x0000FE6C
		private EsentStreamingDataNotLoggedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
