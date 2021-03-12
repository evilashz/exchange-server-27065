using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF5 RID: 3573
	internal static class PropertyProviderExtensions
	{
		// Token: 0x06005C8D RID: 23693 RVA: 0x00120838 File Offset: 0x0011EA38
		public static EwsPropertyProvider GetEwsPropertyProvider(this PropertyProvider baseProvider, EntitySchema schema)
		{
			ArgumentValidator.ThrowIfNull("baseProvider", baseProvider);
			ArgumentValidator.ThrowIfNull("schema", schema);
			if (baseProvider is EwsPropertyProvider)
			{
				return baseProvider as EwsPropertyProvider;
			}
			if (baseProvider is AggregatedPropertyProvider)
			{
				AggregatedPropertyProvider aggregatedPropertyProvider = baseProvider as AggregatedPropertyProvider;
				PropertyProvider propertyProvider = aggregatedPropertyProvider.SelectProvider(schema);
				if (propertyProvider is EwsPropertyProvider)
				{
					return propertyProvider as EwsPropertyProvider;
				}
			}
			throw new InvalidOperationException("Invalid provider type.");
		}
	}
}
