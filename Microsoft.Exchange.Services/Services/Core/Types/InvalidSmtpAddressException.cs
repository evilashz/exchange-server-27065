using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D4 RID: 2004
	internal class InvalidSmtpAddressException : ServicePermanentException
	{
		// Token: 0x06003B11 RID: 15121 RVA: 0x000CFB8B File Offset: 0x000CDD8B
		public InvalidSmtpAddressException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidSmtpAddress, innerException)
		{
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000CFB9E File Offset: 0x000CDD9E
		public InvalidSmtpAddressException() : base(CoreResources.IDs.ErrorInvalidSmtpAddress)
		{
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000CFBB0 File Offset: 0x000CDDB0
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
