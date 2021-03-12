using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200084D RID: 2125
	internal static class PropertyDefinitionHelper
	{
		// Token: 0x06003D4F RID: 15695 RVA: 0x000D7420 File Offset: 0x000D5620
		internal static PropertyDefinition GenerateInternetHeaderPropertyDefinition(string internetHeaderName)
		{
			return GuidNamePropertyDefinition.CreateCustom(internetHeaderName, typeof(string), PropertyIdGuids.PSETIDInternetHeaders, internetHeaderName, PropertyFlags.None);
		}
	}
}
