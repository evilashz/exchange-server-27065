using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000741 RID: 1857
	[Serializable]
	public class InvalidSyncStateVersionException : StoragePermanentException
	{
		// Token: 0x06004811 RID: 18449 RVA: 0x00130948 File Offset: 0x0012EB48
		public InvalidSyncStateVersionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x00130951 File Offset: 0x0012EB51
		public InvalidSyncStateVersionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0013095B File Offset: 0x0012EB5B
		protected InvalidSyncStateVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
