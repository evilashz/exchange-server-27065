using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200018B RID: 395
	internal interface IServerDataObject : IPropertyContainer, IProperty
	{
		// Token: 0x06001110 RID: 4368
		void Bind(object item);

		// Token: 0x06001111 RID: 4369
		void Unbind();

		// Token: 0x06001112 RID: 4370
		bool CanConvertItemClassUsingCurrentSchema(string itemClass);

		// Token: 0x06001113 RID: 4371
		PropertyDefinition[] GetPrefetchProperties();

		// Token: 0x06001114 RID: 4372
		void SetChangedProperties(PropertyDefinition[] changedProperties);

		// Token: 0x06001115 RID: 4373
		IProperty GetPropBySchemaLinkId(int id);
	}
}
