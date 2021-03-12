using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000741 RID: 1857
	[Serializable]
	internal sealed class DeleteDistinguishedFolderException : ServicePermanentException
	{
		// Token: 0x060037EA RID: 14314 RVA: 0x000C6638 File Offset: 0x000C4838
		public DeleteDistinguishedFolderException(Exception innerException) : base((CoreResources.IDs)3448951775U, innerException)
		{
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x000C664B File Offset: 0x000C484B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
