using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A4 RID: 1700
	internal sealed class ADConfigurationException : ServicePermanentException
	{
		// Token: 0x0600346E RID: 13422 RVA: 0x000BCF0B File Offset: 0x000BB10B
		public ADConfigurationException() : base(CoreResources.IDs.ErrorMailboxConfiguration)
		{
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000BCF1D File Offset: 0x000BB11D
		public ADConfigurationException(Exception innerException) : base(CoreResources.IDs.ErrorMailboxConfiguration, innerException)
		{
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000BCF30 File Offset: 0x000BB130
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
