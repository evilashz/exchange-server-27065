using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000751 RID: 1873
	[Serializable]
	public class NoMoreConnectionsException : StoragePermanentException
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x00130D4A File Offset: 0x0012EF4A
		public NoMoreConnectionsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00130D53 File Offset: 0x0012EF53
		public NoMoreConnectionsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00130D5D File Offset: 0x0012EF5D
		protected NoMoreConnectionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
