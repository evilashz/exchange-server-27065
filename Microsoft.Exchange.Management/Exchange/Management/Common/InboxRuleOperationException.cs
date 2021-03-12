using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	internal class InboxRuleOperationException : LocalizedException
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0002A209 File Offset: 0x00028409
		public InboxRuleOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002A212 File Offset: 0x00028412
		public InboxRuleOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002A21C File Offset: 0x0002841C
		protected InboxRuleOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
