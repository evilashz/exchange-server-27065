using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class RulesValidationException : LocalizedException
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00006D06 File Offset: 0x00004F06
		public RulesValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006D0F File Offset: 0x00004F0F
		public RulesValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006D19 File Offset: 0x00004F19
		protected RulesValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
