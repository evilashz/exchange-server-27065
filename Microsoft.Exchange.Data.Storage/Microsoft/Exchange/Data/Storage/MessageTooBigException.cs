using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200074E RID: 1870
	[Serializable]
	public class MessageTooBigException : StoragePermanentException
	{
		// Token: 0x06004832 RID: 18482 RVA: 0x00130ACA File Offset: 0x0012ECCA
		public MessageTooBigException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00130AD4 File Offset: 0x0012ECD4
		protected MessageTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
