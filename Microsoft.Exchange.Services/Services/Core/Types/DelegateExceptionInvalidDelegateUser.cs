using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000738 RID: 1848
	internal class DelegateExceptionInvalidDelegateUser : ServicePermanentException
	{
		// Token: 0x060037B4 RID: 14260 RVA: 0x000C5C40 File Offset: 0x000C3E40
		public DelegateExceptionInvalidDelegateUser(CoreResources.IDs errorCode) : base(errorCode)
		{
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000C5C4E File Offset: 0x000C3E4E
		public DelegateExceptionInvalidDelegateUser(CoreResources.IDs errorCode, Exception innerException) : base(errorCode, innerException)
		{
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000C5C5D File Offset: 0x000C3E5D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
