using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public sealed class EsentTableLockedException : EsentUsageException
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x00012EF2 File Offset: 0x000110F2
		public EsentTableLockedException() : base("Table is exclusively locked", JET_err.TableLocked)
		{
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00012F04 File Offset: 0x00011104
		private EsentTableLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
