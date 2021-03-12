using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000792 RID: 1938
	[Serializable]
	public class VirusScanInProgressException : StorageTransientException
	{
		// Token: 0x0600490C RID: 18700 RVA: 0x00131B78 File Offset: 0x0012FD78
		public VirusScanInProgressException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00131B81 File Offset: 0x0012FD81
		public VirusScanInProgressException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00131B8B File Offset: 0x0012FD8B
		protected VirusScanInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
