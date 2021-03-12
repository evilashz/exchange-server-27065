using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200080F RID: 2063
	[Serializable]
	internal sealed class MissingInformationEmailAddressException : ServicePermanentException
	{
		// Token: 0x06003C19 RID: 15385 RVA: 0x000D58BD File Offset: 0x000D3ABD
		public MissingInformationEmailAddressException() : base(CoreResources.IDs.ErrorMissingInformationEmailAddress)
		{
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x000D58CF File Offset: 0x000D3ACF
		public MissingInformationEmailAddressException(Enum messageId) : base(ResponseCodeType.ErrorMissingInformationEmailAddress, messageId)
		{
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000D58DD File Offset: 0x000D3ADD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
