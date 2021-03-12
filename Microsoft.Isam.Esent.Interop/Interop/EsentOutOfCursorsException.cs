using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public sealed class EsentOutOfCursorsException : EsentMemoryException
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x0001220A File Offset: 0x0001040A
		public EsentOutOfCursorsException() : base("Out of table cursors", JET_err.OutOfCursors)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001221C File Offset: 0x0001041C
		private EsentOutOfCursorsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
