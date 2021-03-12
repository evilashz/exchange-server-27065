using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	public sealed class EsentOutOfFileHandlesException : EsentMemoryException
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x000122CE File Offset: 0x000104CE
		public EsentOutOfFileHandlesException() : base("Out of file handles", JET_err.OutOfFileHandles)
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000122E0 File Offset: 0x000104E0
		private EsentOutOfFileHandlesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
