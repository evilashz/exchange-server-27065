using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000192 RID: 402
	[Serializable]
	public sealed class EsentDatabaseLockedException : EsentUsageException
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x00012C8A File Offset: 0x00010E8A
		public EsentDatabaseLockedException() : base("Database exclusively locked", JET_err.DatabaseLocked)
		{
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00012C9C File Offset: 0x00010E9C
		private EsentDatabaseLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
