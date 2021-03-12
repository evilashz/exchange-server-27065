using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TplTaskEngine
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00005E20 File Offset: 0x00004020
		public static Task BeginExecute(ITask task, CancellationToken cancellationToken)
		{
			return Task.Factory.FromAsync<TaskResult>(TaskEngine.BeginExecute(task, null, null), (IAsyncResult asyncResult) => TaskEngine.EndExecute(asyncResult), TaskCreationOptions.AttachedToParent);
		}
	}
}
