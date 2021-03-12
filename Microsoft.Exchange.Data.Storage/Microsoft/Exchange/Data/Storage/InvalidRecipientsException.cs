using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000740 RID: 1856
	[Serializable]
	public class InvalidRecipientsException : StoragePermanentException
	{
		// Token: 0x0600480F RID: 18447 RVA: 0x00130934 File Offset: 0x0012EB34
		public InvalidRecipientsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0013093E File Offset: 0x0012EB3E
		protected InvalidRecipientsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
