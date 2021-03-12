using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public interface ITask
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001C8 RID: 456
		TaskResult Result { get; }

		// Token: 0x060001C9 RID: 457
		void Initialize(Action resumeDelegate);

		// Token: 0x060001CA RID: 458
		void OnCompleted();

		// Token: 0x060001CB RID: 459
		IEnumerator<ITask> Process();
	}
}
