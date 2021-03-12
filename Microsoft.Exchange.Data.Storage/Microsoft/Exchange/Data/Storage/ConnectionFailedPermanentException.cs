using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	public class ConnectionFailedPermanentException : StoragePermanentException
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00067542 File Offset: 0x00065742
		public ConnectionFailedPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0006754B File Offset: 0x0006574B
		public ConnectionFailedPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00067555 File Offset: 0x00065755
		protected ConnectionFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
