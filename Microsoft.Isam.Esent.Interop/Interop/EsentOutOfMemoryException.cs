using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public sealed class EsentOutOfMemoryException : EsentMemoryException
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x000121D2 File Offset: 0x000103D2
		public EsentOutOfMemoryException() : base("Out of Memory", JET_err.OutOfMemory)
		{
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000121E4 File Offset: 0x000103E4
		private EsentOutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
