using System;
using System.Threading.Tasks;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000002 RID: 2
	internal static class AsynchronousTaskHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static ExportException WaitForAsynchronousTask(Task task)
		{
			ExportException ex = null;
			if (task != null)
			{
				AggregateException ex2 = null;
				try
				{
					if (task.Wait(3600000))
					{
						ex2 = task.Exception;
						if (ex2 != null)
						{
							Tracer.TraceError("AsynchronousTaskHandler.WaitForAsynchronousTask: Exception returned by asynchronous task: {0}", new object[]
							{
								ex2
							});
						}
					}
					else
					{
						ex = new ExportException(ExportErrorType.AsynchronousTaskTimeout);
					}
				}
				catch (AggregateException ex3)
				{
					ex2 = ex3;
					Tracer.TraceError("AsynchronousTaskHandler.WaitForAsynchronousTask: Exception caught from asynchronous task: {0}", new object[]
					{
						ex3
					});
				}
				finally
				{
					if (ex2 != null)
					{
						ExportException ex4 = ex2.InnerException as ExportException;
						ex = (ex4 ?? new ExportException(ExportErrorType.Unknown, ex2.InnerException));
					}
					try
					{
						task.Dispose();
					}
					catch (InvalidOperationException ex5)
					{
						Tracer.TraceError("AsynchronousTaskHandler.WaitForAsynchronousTask - Exception disposing task: {0}", new object[]
						{
							ex5
						});
						if (ex == null)
						{
							ex = new ExportException(ExportErrorType.OperationNotSupportedWithCurrentStatus, ex5);
						}
					}
				}
			}
			return ex;
		}

		// Token: 0x04000001 RID: 1
		private const int WaitingTimeout = 3600000;
	}
}
