using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000711 RID: 1809
	internal sealed class CannotAttachSelfException : ServicePermanentException
	{
		// Token: 0x06003729 RID: 14121 RVA: 0x000C5500 File Offset: 0x000C3700
		public CannotAttachSelfException() : base(CoreResources.IDs.ErrorCannotAttachSelf)
		{
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000C5512 File Offset: 0x000C3712
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
