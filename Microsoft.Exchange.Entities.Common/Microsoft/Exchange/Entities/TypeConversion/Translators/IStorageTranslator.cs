using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion.Translators
{
	// Token: 0x02000080 RID: 128
	internal interface IStorageTranslator<in TStorageObject, TEntity>
	{
		// Token: 0x060002CC RID: 716
		void SetPropertiesFromStorageObjectOnEntity(TStorageObject sourceStoreObject, TEntity destinationEntity);

		// Token: 0x060002CD RID: 717
		void SetPropertiesFromEntityOnStorageObject(TEntity sourceEntity, TStorageObject destinationStoreObject);

		// Token: 0x060002CE RID: 718
		void SetPropertiesFromStoragePropertyValuesOnEntity(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, TEntity destinationEntity);

		// Token: 0x060002CF RID: 719
		TEntity ConvertToEntity(TStorageObject sourceStoreObject);

		// Token: 0x060002D0 RID: 720
		TEntity ConvertToEntity(TStorageObject sourceStoreObject1, TStorageObject sourceStoreObject2);

		// Token: 0x060002D1 RID: 721
		TEntity ConvertToEntity(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session);

		// Token: 0x060002D2 RID: 722
		IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Map(string entityPropertyName);

		// Token: 0x060002D3 RID: 723
		IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Map(IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> properties);
	}
}
