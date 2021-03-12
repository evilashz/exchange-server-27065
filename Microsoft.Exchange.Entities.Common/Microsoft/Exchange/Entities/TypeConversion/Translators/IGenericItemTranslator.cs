using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.TypeConversion.Translators
{
	// Token: 0x02000083 RID: 131
	internal interface IGenericItemTranslator
	{
		// Token: 0x060002EC RID: 748
		IItem ConvertToEntity(IItem storageItem);
	}
}
