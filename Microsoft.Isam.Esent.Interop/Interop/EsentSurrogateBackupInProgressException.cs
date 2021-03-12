using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	public sealed class EsentSurrogateBackupInProgressException : EsentStateException
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x000120BA File Offset: 0x000102BA
		public EsentSurrogateBackupInProgressException() : base("A surrogate backup is in progress.", JET_err.SurrogateBackupInProgress)
		{
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000120CC File Offset: 0x000102CC
		private EsentSurrogateBackupInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
