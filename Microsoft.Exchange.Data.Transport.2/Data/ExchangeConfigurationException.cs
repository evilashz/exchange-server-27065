using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class ExchangeConfigurationException : LocalizedException
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00006540 File Offset: 0x00004740
		public ExchangeConfigurationException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000654E File Offset: 0x0000474E
		public ExchangeConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006557 File Offset: 0x00004757
		public ExchangeConfigurationException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006566 File Offset: 0x00004766
		public ExchangeConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006570 File Offset: 0x00004770
		protected ExchangeConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
