using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A2 RID: 1954
	internal class IncorrectSchemaVersionException : ServicePermanentException
	{
		// Token: 0x06003A88 RID: 14984 RVA: 0x000CF149 File Offset: 0x000CD349
		public IncorrectSchemaVersionException() : base((CoreResources.IDs)3510999536U)
		{
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003A89 RID: 14985 RVA: 0x000CF15B File Offset: 0x000CD35B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
