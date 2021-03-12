using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x0200006B RID: 107
	internal static class TaskFactoryExtensions
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00009F90 File Offset: 0x00008190
		internal static TaskScheduler GetTargetScheduler(this TaskFactory factory)
		{
			ArgumentValidator.ThrowIfNull("factory", factory);
			return factory.Scheduler ?? TaskScheduler.Current;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009FAC File Offset: 0x000081AC
		internal static Task Iterate<T>(this TaskFactory factory, IEnumerable<T> source) where T : Task
		{
			return factory.Iterate(source, null, factory.CancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009FC8 File Offset: 0x000081C8
		internal static Task Iterate<T>(this TaskFactory factory, IEnumerable<T> source, CancellationToken cancellationToken) where T : Task
		{
			return factory.Iterate(source, null, cancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009FDF File Offset: 0x000081DF
		internal static Task Iterate<T>(this TaskFactory factory, IEnumerable<T> source, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler) where T : Task
		{
			return factory.Iterate(source, null, cancellationToken, creationOptions, scheduler);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A0D8 File Offset: 0x000082D8
		internal static Task Iterate<T>(this TaskFactory factory, IEnumerable<T> source, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler) where T : Task
		{
			ArgumentValidator.ThrowIfNull("factory", factory);
			ArgumentValidator.ThrowIfNull("source", source);
			ArgumentValidator.ThrowIfNull("scheduler", scheduler);
			ArgumentValidator.ThrowIfNull("cancellationToken", cancellationToken);
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator == null)
			{
				throw new InvalidOperationException("Invalid enumerable - GetEnumerator returned null");
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(state, creationOptions);
			tcs.Task.ContinueWith(delegate(Task<object> x)
			{
				enumerator.Dispose();
			}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			Action<Task> recursiveAsyncIterator = null;
			recursiveAsyncIterator = delegate(Task unusedAntecedentTask)
			{
				try
				{
					if (cancellationToken.IsCancellationRequested)
					{
						tcs.TrySetCanceled();
					}
					else if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						t.IgnoreExceptions();
						t.ContinueWith(recursiveAsyncIterator).IgnoreExceptions();
					}
					else
					{
						tcs.TrySetResult(null);
					}
				}
				catch (Exception ex)
				{
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null && ex2.CancellationToken == cancellationToken)
					{
						tcs.TrySetCanceled();
					}
					else
					{
						tcs.TrySetException(ex);
					}
				}
			};
			factory.StartNew(delegate()
			{
				recursiveAsyncIterator(null);
			}, CancellationToken.None, TaskCreationOptions.None, scheduler).IgnoreExceptions();
			return tcs.Task;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A1CF File Offset: 0x000083CF
		private static Task IgnoreExceptions(this Task task)
		{
			task.ContinueWith(delegate(Task t)
			{
				AggregateException exception = t.Exception;
			}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			return task;
		}
	}
}
