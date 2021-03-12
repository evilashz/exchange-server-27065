using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	public sealed class EsentNTSystemCallFailedException : EsentOperationException
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x00011602 File Offset: 0x0000F802
		public EsentNTSystemCallFailedException() : base("A call to the operating system failed", JET_err.NTSystemCallFailed)
		{
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00011614 File Offset: 0x0000F814
		private EsentNTSystemCallFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
