using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200011D RID: 285
	internal interface IVersionable
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060009FD RID: 2557
		ObjectSchema ObjectSchema { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060009FE RID: 2558
		ExchangeObjectVersion ExchangeVersion { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060009FF RID: 2559
		ExchangeObjectVersion MaximumSupportedExchangeObjectVersion { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A00 RID: 2560
		bool IsReadOnly { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A01 RID: 2561
		bool ExchangeVersionUpgradeSupported { get; }

		// Token: 0x06000A02 RID: 2562
		bool IsPropertyAccessible(PropertyDefinition propertyDefinition);
	}
}
