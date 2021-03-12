using System;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000074 RID: 116
	internal interface IWorkerProcess
	{
		// Token: 0x06000405 RID: 1029
		void Retire();

		// Token: 0x06000406 RID: 1030
		void Stop();

		// Token: 0x06000407 RID: 1031
		void Pause();

		// Token: 0x06000408 RID: 1032
		void Continue();
	}
}
