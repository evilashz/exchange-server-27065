using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public sealed class EsentOutOfBuffersException : EsentMemoryException
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00012226 File Offset: 0x00010426
		public EsentOutOfBuffersException() : base("Out of database page buffers", JET_err.OutOfBuffers)
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00012238 File Offset: 0x00010438
		private EsentOutOfBuffersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
