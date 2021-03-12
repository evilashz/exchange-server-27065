using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000825 RID: 2085
	internal sealed class NonPrimarySmtpAddressException : ServicePermanentException
	{
		// Token: 0x06003C61 RID: 15457 RVA: 0x000D5C1C File Offset: 0x000D3E1C
		public NonPrimarySmtpAddressException(string smtpAddress) : base(CoreResources.IDs.ErrorNonPrimarySmtpAddress)
		{
			base.ConstantValues.Add("Primary", smtpAddress);
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x000D5C3F File Offset: 0x000D3E3F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x04002161 RID: 8545
		private const string PrimaryKey = "Primary";
	}
}
