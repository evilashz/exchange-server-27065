using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullTask : ITask
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009124 File Offset: 0x00007324
		public TaskResult Result
		{
			get
			{
				return TaskResult.Success;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009127 File Offset: 0x00007327
		public void Initialize(Action resumeDelegate)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009129 File Offset: 0x00007329
		public void OnCompleted()
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009174 File Offset: 0x00007374
		public IEnumerator<ITask> Process()
		{
			yield break;
		}
	}
}
