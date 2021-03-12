using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	public sealed class EsentTooManyInstancesException : EsentQuotaException
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x00012D4E File Offset: 0x00010F4E
		public EsentTooManyInstancesException() : base("Cannot start any more database instances", JET_err.TooManyInstances)
		{
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00012D60 File Offset: 0x00010F60
		private EsentTooManyInstancesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
