using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public sealed class ExchangeSettingsIdParameter : ADIdParameter
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x000211B1 File Offset: 0x0001F3B1
		public ExchangeSettingsIdParameter()
		{
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000211B9 File Offset: 0x0001F3B9
		public ExchangeSettingsIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000211C2 File Offset: 0x0001F3C2
		public ExchangeSettingsIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000211CB File Offset: 0x0001F3CB
		public ExchangeSettingsIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000211D4 File Offset: 0x0001F3D4
		public ExchangeSettingsIdParameter(ExchangeSettings exchangeSettings) : base(exchangeSettings.Id)
		{
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000211E2 File Offset: 0x0001F3E2
		public static ExchangeSettingsIdParameter Parse(string identity)
		{
			return new ExchangeSettingsIdParameter(identity);
		}
	}
}
