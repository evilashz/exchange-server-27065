using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D6 RID: 214
	internal interface IAppendUpdateCommand : IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060005CE RID: 1486
		void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings);
	}
}
