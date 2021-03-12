using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200021E RID: 542
	[Serializable]
	public sealed class EsentFileIOAbortException : EsentObsoleteException
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x00013BDA File Offset: 0x00011DDA
		public EsentFileIOAbortException() : base("instructs the JET_ABORTRETRYFAILCALLBACK caller to abort the specified I/O", JET_err.FileIOAbort)
		{
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00013BEC File Offset: 0x00011DEC
		private EsentFileIOAbortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
