using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200075D RID: 1885
	internal sealed class EmailAddressMismatchException : ServicePermanentException
	{
		// Token: 0x06003853 RID: 14419 RVA: 0x000C74C6 File Offset: 0x000C56C6
		public EmailAddressMismatchException() : base(CoreResources.IDs.ErrorEmailAddressMismatch)
		{
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000C74D8 File Offset: 0x000C56D8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
