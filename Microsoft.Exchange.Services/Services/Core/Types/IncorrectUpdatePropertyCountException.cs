using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A3 RID: 1955
	[Serializable]
	internal class IncorrectUpdatePropertyCountException : ServicePermanentException
	{
		// Token: 0x06003A8A RID: 14986 RVA: 0x000CF162 File Offset: 0x000CD362
		public IncorrectUpdatePropertyCountException() : base(CoreResources.IDs.ErrorIncorrectUpdatePropertyCount)
		{
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003A8B RID: 14987 RVA: 0x000CF174 File Offset: 0x000CD374
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
