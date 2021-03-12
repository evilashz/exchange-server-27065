using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000764 RID: 1892
	internal class ExceededMaxSubscriptionLimitException : ServicePermanentException
	{
		// Token: 0x06003878 RID: 14456 RVA: 0x000C77B4 File Offset: 0x000C59B4
		public ExceededMaxSubscriptionLimitException() : base(CoreResources.IDs.ErrorExceededSubscriptionCount)
		{
			base.ConstantValues.Add("PolicyLimit", CallContext.Current.Budget.ThrottlingPolicy.EwsMaxSubscriptions.Value.ToString());
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000C7805 File Offset: 0x000C5A05
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000C780C File Offset: 0x000C5A0C
		public static void Throw()
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				throw new ExceededMaxSubscriptionLimitException();
			}
			throw new ServerBusyException();
		}

		// Token: 0x04001F55 RID: 8021
		private const string PolicyLimitKey = "PolicyLimit";
	}
}
