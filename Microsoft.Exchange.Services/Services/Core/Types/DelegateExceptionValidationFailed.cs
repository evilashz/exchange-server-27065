using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200073A RID: 1850
	internal class DelegateExceptionValidationFailed : ServicePermanentException
	{
		// Token: 0x060037B9 RID: 14265 RVA: 0x000C5C7D File Offset: 0x000C3E7D
		public DelegateExceptionValidationFailed(Exception innerException) : base((CoreResources.IDs)4097108255U, innerException)
		{
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000C5C90 File Offset: 0x000C3E90
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
