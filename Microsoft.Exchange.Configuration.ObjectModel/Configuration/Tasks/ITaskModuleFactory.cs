using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200027E RID: 638
	public interface ITaskModuleFactory
	{
		// Token: 0x060015EF RID: 5615
		IEnumerable<ITaskModule> Create(TaskContext context);
	}
}
