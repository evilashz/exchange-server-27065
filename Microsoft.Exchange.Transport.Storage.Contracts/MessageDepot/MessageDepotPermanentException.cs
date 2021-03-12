using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal class MessageDepotPermanentException : DataSourceOperationException
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021D4 File Offset: 0x000003D4
		public MessageDepotPermanentException(LocalizedString errorMessage, Exception innerException = null) : base(errorMessage, innerException)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DE File Offset: 0x000003DE
		protected MessageDepotPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
