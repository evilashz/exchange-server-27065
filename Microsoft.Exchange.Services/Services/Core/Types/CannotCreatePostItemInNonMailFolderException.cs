using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000712 RID: 1810
	[Serializable]
	internal sealed class CannotCreatePostItemInNonMailFolderException : ServicePermanentException
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x000C5519 File Offset: 0x000C3719
		public CannotCreatePostItemInNonMailFolderException() : base((CoreResources.IDs)3792171687U)
		{
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000C552B File Offset: 0x000C372B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
