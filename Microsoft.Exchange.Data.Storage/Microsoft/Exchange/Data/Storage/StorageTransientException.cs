using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public class StorageTransientException : TransientException
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x0006A28A File Offset: 0x0006848A
		public StorageTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0006A293 File Offset: 0x00068493
		public StorageTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0006A29D File Offset: 0x0006849D
		protected StorageTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
