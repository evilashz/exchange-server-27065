using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A54 RID: 2644
	internal interface IBingError
	{
		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06004B00 RID: 19200
		string Code { get; }

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06004B01 RID: 19201
		string Message { get; }
	}
}
