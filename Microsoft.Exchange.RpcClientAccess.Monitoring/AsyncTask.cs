using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AsyncTask : BaseTask
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00006CD3 File Offset: 0x00004ED3
		public AsyncTask(IContext context, AsyncTask.BeginDelegate beginDelegate, AsyncTask.EndDelegate endDelegate) : base(context, Strings.AsyncTaskTitle, Strings.AsyncTaskDescription, TaskType.Infrastructure, new ContextProperty[0])
		{
			this.beginDelegate = beginDelegate;
			this.endDelegate = endDelegate;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006DC0 File Offset: 0x00004FC0
		protected override IEnumerator<ITask> Process()
		{
			IAsyncResult asyncResult = this.beginDelegate(delegate(IAsyncResult result)
			{
				base.Resume();
			}, this);
			yield return null;
			base.Result = this.endDelegate(asyncResult);
			yield break;
		}

		// Token: 0x040000EB RID: 235
		private readonly AsyncTask.BeginDelegate beginDelegate;

		// Token: 0x040000EC RID: 236
		private readonly AsyncTask.EndDelegate endDelegate;

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060001EF RID: 495
		public delegate IAsyncResult BeginDelegate(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060001F3 RID: 499
		public delegate TaskResult EndDelegate(IAsyncResult asyncResult);
	}
}
