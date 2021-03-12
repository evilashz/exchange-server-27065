using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000718 RID: 1816
	internal sealed class CannotOpenFileAttachmentException : ServicePermanentException
	{
		// Token: 0x0600373C RID: 14140 RVA: 0x000C55CD File Offset: 0x000C37CD
		public CannotOpenFileAttachmentException(Exception innerException) : base(CoreResources.IDs.ErrorCannotOpenFileAttachment, innerException)
		{
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000C55E0 File Offset: 0x000C37E0
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
