using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000737 RID: 1847
	internal class DelegateExceptionCannotAddOwner : ServicePermanentException
	{
		// Token: 0x060037B2 RID: 14258 RVA: 0x000C5C26 File Offset: 0x000C3E26
		public DelegateExceptionCannotAddOwner(Exception innerException) : base(CoreResources.IDs.ErrorDelegateCannotAddOwner, innerException)
		{
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x000C5C39 File Offset: 0x000C3E39
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
