using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	public sealed class EsentSessionWriteConflictException : EsentUsageException
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x000129CE File Offset: 0x00010BCE
		public EsentSessionWriteConflictException() : base("Attempt to replace the same record by two diffrerent cursors in the same session", JET_err.SessionWriteConflict)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000129E0 File Offset: 0x00010BE0
		private EsentSessionWriteConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
