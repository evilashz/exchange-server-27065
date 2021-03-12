using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public sealed class EsentRecoveredWithoutUndoDatabasesConsistentException : EsentStateException
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x00011F6A File Offset: 0x0001016A
		public EsentRecoveredWithoutUndoDatabasesConsistentException() : base("Soft recovery successfully replayed all operations and intended to skip the Undo phase of recovery, but the Undo phase was not required", JET_err.RecoveredWithoutUndoDatabasesConsistent)
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00011F7C File Offset: 0x0001017C
		private EsentRecoveredWithoutUndoDatabasesConsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
