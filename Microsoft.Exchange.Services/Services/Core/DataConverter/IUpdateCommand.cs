using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D0 RID: 208
	internal interface IUpdateCommand : IPropertyCommand
	{
		// Token: 0x060005B7 RID: 1463
		void Update();

		// Token: 0x060005B8 RID: 1464
		void PostUpdate();
	}
}
