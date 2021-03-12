using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000758 RID: 1880
	internal class DuplicateSOAPHeaderException : ServicePermanentException
	{
		// Token: 0x0600383E RID: 14398 RVA: 0x000C71C3 File Offset: 0x000C53C3
		public DuplicateSOAPHeaderException() : base((CoreResources.IDs)4197444273U)
		{
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000C71D5 File Offset: 0x000C53D5
		public DuplicateSOAPHeaderException(Exception innerException) : base((CoreResources.IDs)4197444273U, innerException)
		{
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000C71E8 File Offset: 0x000C53E8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
