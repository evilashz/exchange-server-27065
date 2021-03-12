using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000191 RID: 401
	[Serializable]
	public sealed class EsentDatabaseCorruptedException : EsentCorruptionException
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x00012C6E File Offset: 0x00010E6E
		public EsentDatabaseCorruptedException() : base("Non database file or corrupted db", JET_err.DatabaseCorrupted)
		{
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00012C80 File Offset: 0x00010E80
		private EsentDatabaseCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
