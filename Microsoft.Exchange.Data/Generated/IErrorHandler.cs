using System;

namespace Microsoft.Exchange.Data.Generated
{
	// Token: 0x0200024B RID: 587
	public interface IErrorHandler
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600140F RID: 5135
		int ErrNum { get; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001410 RID: 5136
		int WrnNum { get; }

		// Token: 0x06001411 RID: 5137
		void AddError(string msg, int lin, int col, int len, int severity);
	}
}
