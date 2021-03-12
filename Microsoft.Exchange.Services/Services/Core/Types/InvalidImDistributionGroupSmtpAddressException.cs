using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B5 RID: 1973
	internal sealed class InvalidImDistributionGroupSmtpAddressException : ServicePermanentException
	{
		// Token: 0x06003ABB RID: 15035 RVA: 0x000CF4B0 File Offset: 0x000CD6B0
		public InvalidImDistributionGroupSmtpAddressException() : base(CoreResources.IDs.ErrorInvalidImDistributionGroupSmtpAddress)
		{
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000CF4C2 File Offset: 0x000CD6C2
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
