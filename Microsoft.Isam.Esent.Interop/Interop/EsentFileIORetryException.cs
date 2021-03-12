using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200021F RID: 543
	[Serializable]
	public sealed class EsentFileIORetryException : EsentObsoleteException
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00013BF6 File Offset: 0x00011DF6
		public EsentFileIORetryException() : base("instructs the JET_ABORTRETRYFAILCALLBACK caller to retry the specified I/O", JET_err.FileIORetry)
		{
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00013C08 File Offset: 0x00011E08
		private EsentFileIORetryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
