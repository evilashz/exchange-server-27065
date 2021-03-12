using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF6 RID: 3574
	internal static class SchemaExtensions
	{
		// Token: 0x06005C8E RID: 23694 RVA: 0x0012089C File Offset: 0x0011EA9C
		public static PropertyDefinition ResolveProperty(this Schema schema, string propertyName)
		{
			ArgumentValidator.ThrowIfNull("schema", schema);
			ArgumentValidator.ThrowIfNullOrEmpty("propertyName", propertyName);
			PropertyDefinition result;
			if (!schema.TryGetPropertyByName(propertyName, out result))
			{
				throw new InvalidPropertyException(propertyName);
			}
			return result;
		}
	}
}
