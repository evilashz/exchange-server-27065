using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200079D RID: 1949
	internal sealed class ImGroupDisplayNameAlreadyExistsException : ServicePermanentException
	{
		// Token: 0x06003A7D RID: 14973 RVA: 0x000CF0AB File Offset: 0x000CD2AB
		public ImGroupDisplayNameAlreadyExistsException() : base((CoreResources.IDs)3809605342U)
		{
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000CF0BD File Offset: 0x000CD2BD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
