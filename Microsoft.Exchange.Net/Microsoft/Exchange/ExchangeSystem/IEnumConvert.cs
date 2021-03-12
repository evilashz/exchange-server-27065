using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000077 RID: 119
	internal interface IEnumConvert
	{
		// Token: 0x06000412 RID: 1042
		bool TryParse(string value, EnumParseOptions options, out object result);
	}
}
