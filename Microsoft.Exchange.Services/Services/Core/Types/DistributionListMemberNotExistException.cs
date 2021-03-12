using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000756 RID: 1878
	internal class DistributionListMemberNotExistException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x0600383A RID: 14394 RVA: 0x000C7189 File Offset: 0x000C5389
		public DistributionListMemberNotExistException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorDistributionListMemberNotExist, propertyPath)
		{
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000C719C File Offset: 0x000C539C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
