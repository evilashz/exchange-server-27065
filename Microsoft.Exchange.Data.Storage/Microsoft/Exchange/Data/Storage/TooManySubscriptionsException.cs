using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000785 RID: 1925
	[Serializable]
	public class TooManySubscriptionsException : StoragePermanentException
	{
		// Token: 0x060048E9 RID: 18665 RVA: 0x00131991 File Offset: 0x0012FB91
		public TooManySubscriptionsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x0013199A File Offset: 0x0012FB9A
		public TooManySubscriptionsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x001319A4 File Offset: 0x0012FBA4
		protected TooManySubscriptionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
