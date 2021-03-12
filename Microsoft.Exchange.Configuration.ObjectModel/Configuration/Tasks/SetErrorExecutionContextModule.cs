using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200028C RID: 652
	internal class SetErrorExecutionContextModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00055134 File Offset: 0x00053334
		private IErrorExecutionContext ErrorExecutionContext
		{
			get
			{
				if (SetErrorExecutionContextModule.executionContext == null)
				{
					lock (SetErrorExecutionContextModule.syncObject)
					{
						if (SetErrorExecutionContextModule.executionContext == null)
						{
							SetErrorExecutionContextModule.executionContext = new ErrorExecutionContext(this.context.InvocationInfo.ShellHostName);
						}
					}
				}
				return SetErrorExecutionContextModule.executionContext;
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000551A4 File Offset: 0x000533A4
		public SetErrorExecutionContextModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000551B3 File Offset: 0x000533B3
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000551B6 File Offset: 0x000533B6
		public void Init(ITaskEvent task)
		{
			task.Error += this.SetErrorExecutionContext;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000551CA File Offset: 0x000533CA
		public void Dispose()
		{
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000551CC File Offset: 0x000533CC
		private void SetErrorExecutionContext(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			IErrorContextException ex = e.Data as IErrorContextException;
			if (ex != null)
			{
				ex.SetContext(this.ErrorExecutionContext);
			}
		}

		// Token: 0x040006DC RID: 1756
		private readonly TaskContext context;

		// Token: 0x040006DD RID: 1757
		private static volatile IErrorExecutionContext executionContext;

		// Token: 0x040006DE RID: 1758
		private static object syncObject = new object();
	}
}
