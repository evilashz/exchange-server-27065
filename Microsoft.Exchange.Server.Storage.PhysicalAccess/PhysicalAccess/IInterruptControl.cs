using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000049 RID: 73
	public interface IInterruptControl
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000356 RID: 854
		bool WantToInterrupt { get; }

		// Token: 0x06000357 RID: 855
		void RegisterRead(bool probe, TableClass tableClass);

		// Token: 0x06000358 RID: 856
		void RegisterWrite(TableClass tableClass);

		// Token: 0x06000359 RID: 857
		void Reset();
	}
}
