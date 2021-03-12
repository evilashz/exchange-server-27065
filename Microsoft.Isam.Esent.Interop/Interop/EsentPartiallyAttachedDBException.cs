using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public sealed class EsentPartiallyAttachedDBException : EsentUsageException
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x00012E12 File Offset: 0x00011012
		public EsentPartiallyAttachedDBException() : base("Database is partially attached. Cannot complete attach operation", JET_err.PartiallyAttachedDB)
		{
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00012E24 File Offset: 0x00011024
		private EsentPartiallyAttachedDBException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
