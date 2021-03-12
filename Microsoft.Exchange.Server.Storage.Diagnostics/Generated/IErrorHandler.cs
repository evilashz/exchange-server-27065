using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics.Generated
{
	// Token: 0x0200003D RID: 61
	public interface IErrorHandler
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001BE RID: 446
		int ErrNum { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001BF RID: 447
		int WrnNum { get; }

		// Token: 0x060001C0 RID: 448
		void AddError(string msg, int lin, int col, int len, int severity);
	}
}
