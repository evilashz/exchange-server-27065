using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000185 RID: 389
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMPromptSchema : SimpleProviderObjectSchema
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x0002DA9D File Offset: 0x0002BC9D
		private static SimpleProviderPropertyDefinition CreatePropertyDefinition(string propertyName, Type propertyType, object defaultValue)
		{
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x040006B5 RID: 1717
		public static SimpleProviderPropertyDefinition AudioData = UMPromptSchema.CreatePropertyDefinition("AudioData", typeof(byte[]), null);

		// Token: 0x040006B6 RID: 1718
		public static SimpleProviderPropertyDefinition Name = UMPromptSchema.CreatePropertyDefinition("Name", typeof(string), null);
	}
}
