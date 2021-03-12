using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000219 RID: 537
	public interface ITaskModule
	{
		// Token: 0x060012B9 RID: 4793
		void Init(ITaskEvent task);

		// Token: 0x060012BA RID: 4794
		void Dispose();
	}
}
