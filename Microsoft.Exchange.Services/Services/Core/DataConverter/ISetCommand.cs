using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000CF RID: 207
	internal interface ISetCommand : IPropertyCommand
	{
		// Token: 0x060005B4 RID: 1460
		void Set();

		// Token: 0x060005B5 RID: 1461
		void SetPhase2();

		// Token: 0x060005B6 RID: 1462
		void SetPhase3();
	}
}
