using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200071C RID: 1820
	[Serializable]
	public class ConnectionFailedTransientException : StorageTransientException
	{
		// Token: 0x060047A1 RID: 18337 RVA: 0x001302DA File Offset: 0x0012E4DA
		public ConnectionFailedTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x001302E3 File Offset: 0x0012E4E3
		public ConnectionFailedTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x001302ED File Offset: 0x0012E4ED
		protected ConnectionFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
