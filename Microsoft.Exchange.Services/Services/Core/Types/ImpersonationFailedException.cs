using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A0 RID: 1952
	internal class ImpersonationFailedException : ServicePermanentException
	{
		// Token: 0x06003A84 RID: 14980 RVA: 0x000CF113 File Offset: 0x000CD313
		internal ImpersonationFailedException(Exception innerException) : base(CoreResources.IDs.ErrorImpersonationFailed, innerException)
		{
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000CF126 File Offset: 0x000CD326
		internal ImpersonationFailedException() : base(CoreResources.IDs.ErrorImpersonationFailed)
		{
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000CF138 File Offset: 0x000CD338
		internal ImpersonationFailedException(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000CF142 File Offset: 0x000CD342
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
