using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000109 RID: 265
	internal interface IPostSavePropertyCommand
	{
		// Token: 0x06000791 RID: 1937
		void ExecutePostSaveOperation(StoreObject item);
	}
}
