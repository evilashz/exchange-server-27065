using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D1 RID: 209
	internal interface ISetUpdateCommand : IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060005B9 RID: 1465
		void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings);
	}
}
