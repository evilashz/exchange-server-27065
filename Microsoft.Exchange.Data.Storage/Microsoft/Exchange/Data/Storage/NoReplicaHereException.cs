using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000755 RID: 1877
	[Serializable]
	public class NoReplicaHereException : StoragePermanentException
	{
		// Token: 0x0600484F RID: 18511 RVA: 0x00130DA5 File Offset: 0x0012EFA5
		public NoReplicaHereException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x00130DAE File Offset: 0x0012EFAE
		public NoReplicaHereException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00130DB8 File Offset: 0x0012EFB8
		protected NoReplicaHereException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
