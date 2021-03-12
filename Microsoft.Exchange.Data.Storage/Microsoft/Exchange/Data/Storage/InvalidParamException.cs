using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200073C RID: 1852
	[Serializable]
	public class InvalidParamException : StoragePermanentException
	{
		// Token: 0x06004801 RID: 18433 RVA: 0x00130855 File Offset: 0x0012EA55
		public InvalidParamException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0013085E File Offset: 0x0012EA5E
		public InvalidParamException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00130868 File Offset: 0x0012EA68
		protected InvalidParamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
