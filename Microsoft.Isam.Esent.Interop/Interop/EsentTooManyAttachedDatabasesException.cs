using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	public sealed class EsentTooManyAttachedDatabasesException : EsentUsageException
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x00013822 File Offset: 0x00011A22
		public EsentTooManyAttachedDatabasesException() : base("Too many open databases", JET_err.TooManyAttachedDatabases)
		{
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00013834 File Offset: 0x00011A34
		private EsentTooManyAttachedDatabasesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
