using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B6 RID: 1718
	[Serializable]
	internal sealed class ApplyConversationActionException : ServicePermanentException
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x000BE341 File Offset: 0x000BC541
		public ApplyConversationActionException() : base(CoreResources.IDs.ErrorApplyConversationActionFailed)
		{
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000BE353 File Offset: 0x000BC553
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
