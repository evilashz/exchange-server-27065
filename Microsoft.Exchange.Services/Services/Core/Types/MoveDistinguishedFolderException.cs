using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000818 RID: 2072
	[Serializable]
	internal sealed class MoveDistinguishedFolderException : ServicePermanentException
	{
		// Token: 0x06003C2D RID: 15405 RVA: 0x000D59BF File Offset: 0x000D3BBF
		public MoveDistinguishedFolderException() : base((CoreResources.IDs)3771523283U)
		{
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000D59D1 File Offset: 0x000D3BD1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
