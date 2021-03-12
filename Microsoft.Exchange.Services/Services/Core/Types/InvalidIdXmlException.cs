using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B3 RID: 1971
	internal sealed class InvalidIdXmlException : ServicePermanentException
	{
		// Token: 0x06003AB7 RID: 15031 RVA: 0x000CF47E File Offset: 0x000CD67E
		public InvalidIdXmlException() : base((CoreResources.IDs)3852956793U)
		{
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000CF490 File Offset: 0x000CD690
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
