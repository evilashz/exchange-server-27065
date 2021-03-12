using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D7 RID: 215
	internal interface IDeleteUpdateCommand : IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060005CF RID: 1487
		void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings);
	}
}
