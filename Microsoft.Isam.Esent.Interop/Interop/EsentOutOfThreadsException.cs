using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public sealed class EsentOutOfThreadsException : EsentMemoryException
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public EsentOutOfThreadsException() : base("Could not start thread", JET_err.OutOfThreads)
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000114D3 File Offset: 0x0000F6D3
		private EsentOutOfThreadsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
