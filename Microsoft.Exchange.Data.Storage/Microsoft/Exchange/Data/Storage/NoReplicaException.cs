using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000754 RID: 1876
	[Serializable]
	public class NoReplicaException : StoragePermanentException
	{
		// Token: 0x0600484C RID: 18508 RVA: 0x00130D88 File Offset: 0x0012EF88
		public NoReplicaException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x00130D91 File Offset: 0x0012EF91
		public NoReplicaException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00130D9B File Offset: 0x0012EF9B
		protected NoReplicaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
