using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000736 RID: 1846
	internal class DelegateExceptionAlreadyExists : ServicePermanentException
	{
		// Token: 0x060037B0 RID: 14256 RVA: 0x000C5C0C File Offset: 0x000C3E0C
		public DelegateExceptionAlreadyExists(Exception innerException) : base(CoreResources.IDs.ErrorDelegateAlreadyExists, innerException)
		{
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000C5C1F File Offset: 0x000C3E1F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
