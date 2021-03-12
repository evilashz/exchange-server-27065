using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200000D RID: 13
	public static class TaskExtension
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00006288 File Offset: 0x00004488
		public static Task Continue<T>(this Task<T> task, Action<T> nextTask, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			return task.ContinueWith(delegate(Task<T> t)
			{
				nextTask(t.Result);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | options, TaskScheduler.Current);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000062D8 File Offset: 0x000044D8
		public static Task<T2> Continue<T1, T2>(this Task<T1> task, Func<T1, Task<T2>> nextTask, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			return task.ContinueWith<Task<T2>>((Task<T1> t) => nextTask(t.Result), cancellationToken, TaskContinuationOptions.AttachedToParent | options, TaskScheduler.Current).Unwrap<T2>();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006328 File Offset: 0x00004528
		public static Task<T2> Continue<T1, T2>(this Task<T1> task, Func<Task<T1>, T2> nextTask, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			return task.ContinueWith<T2>((Task<T1> t) => nextTask(t), cancellationToken, TaskContinuationOptions.AttachedToParent | options, TaskScheduler.Current);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000638C File Offset: 0x0000458C
		public static Task DelayedContinue<T>(this Task<T> task, Action<T> nextTask, int waitAmount, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			return task.ContinueWith(delegate(Task<T> t)
			{
				TaskExtension.NonBlockingWait<T>(waitAmount, t, traceContext).Continue(nextTask, cancellationToken, TaskContinuationOptions.AttachedToParent);
			}, cancellationToken, options, TaskScheduler.Current);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000640C File Offset: 0x0000460C
		public static Task<T2> DelayedContinue<T1, T2>(this Task<T1> task, Func<T1, Task<T2>> nextTask, int waitAmount, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task<T2>> task2 = task.ContinueWith<Task<T2>>((Task<T1> t) => TaskExtension.NonBlockingWait<T1>(waitAmount, t, traceContext).Continue(nextTask, cancellationToken, TaskContinuationOptions.AttachedToParent), cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap<T2>();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006490 File Offset: 0x00004690
		public static Task<T2> DelayedContinue<T1, T2>(this Task<T1> task, Func<Task<T1>, T2> nextTask, int waitAmount, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task<T2>> task2 = task.ContinueWith<Task<T2>>((Task<T1> t) => TaskExtension.NonBlockingWait<T1>(waitAmount, t, traceContext).Continue(nextTask, cancellationToken, TaskContinuationOptions.AttachedToParent), cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap<T2>();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000065F0 File Offset: 0x000047F0
		public static Task ConditionalContinue<T>(this Task<T> task, Action<T> nextTask, Func<Task<T>, bool> condition, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task> task2 = task.ContinueWith<Task>(delegate(Task<T> t)
			{
				if (condition(t))
				{
					return t.ContinueWith(delegate(Task<T> innerT)
					{
						nextTask(innerT.Result);
					}, TaskContinuationOptions.AttachedToParent);
				}
				if (maxRetries > 0)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ConditionalContinue]: Condition not met, will retry.", null, "ConditionalContinue", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 184);
					Task<T> task3 = TaskExtension.NonBlockingWait<T>(waitAmount, t, traceContext);
					return task3.ConditionalContinue(nextTask, condition, waitAmount, --maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
				}
				string text = "Condition not met and retry count exhausted.";
				WTFDiagnostics.TraceError(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ConditionalContinue]: " + text, null, "ConditionalContinue", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 193);
				throw new TimeoutException(text);
			}, cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006740 File Offset: 0x00004940
		public static Task<T2> ConditionalContinue<T1, T2>(this Task<T1> task, Func<T1, Task<T2>> nextTask, Func<Task<T1>, bool> condition, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task<T2>> task2 = task.ContinueWith<Task<T2>>(delegate(Task<T1> t)
			{
				if (condition(t))
				{
					return t.Continue(nextTask, cancellationToken, TaskContinuationOptions.AttachedToParent);
				}
				if (maxRetries > 0)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ConditionalContinue]: Condition not met, will retry.", null, "ConditionalContinue", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 238);
					Task<T1> task3 = TaskExtension.NonBlockingWait<T1>(waitAmount, t, traceContext);
					return task3.ConditionalContinue(nextTask, condition, waitAmount, --maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
				}
				string text = "Condition not met and retry count exhausted.";
				WTFDiagnostics.TraceError(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ConditionalContinue]: " + text, null, "ConditionalContinue", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 246);
				throw new TimeoutException(text);
			}, cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap<T2>();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006890 File Offset: 0x00004A90
		public static Task<T2> ConditionalContinue<T1, T2>(this Task<T1> task, Func<Task<T1>, T2> nextTask, Func<Task<T1>, bool> condition, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task<T2>> task2 = task.ContinueWith<Task<T2>>(delegate(Task<T1> t)
			{
				if (condition(t))
				{
					return t.Continue(nextTask, cancellationToken, TaskContinuationOptions.AttachedToParent);
				}
				if (maxRetries > 0)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ConditionalContinue]: Condition not met, will retry.", null, "ConditionalContinue", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 291);
					Task<T1> task3 = TaskExtension.NonBlockingWait<T1>(waitAmount, t, traceContext);
					return task3.ConditionalContinue(nextTask, condition, waitAmount, --maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
				}
				string text = "Condition not met and retry count exhausted.";
				WTFDiagnostics.TraceError(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ConditionalContinue]: " + text, null, "ConditionalContinue", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 299);
				throw new TimeoutException(text);
			}, cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap<T2>();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006978 File Offset: 0x00004B78
		public static Task<T> StartWithRetry<T>(Func<T> function, Func<Exception, bool> retryCriteria, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskCreationOptions options, TracingContext traceContext)
		{
			Task<Task<T>> task = Task.Factory.StartNew<Task<T>>(delegate()
			{
				Task<bool> task2 = Task.Factory.StartNew<bool>(() => true);
				return task2.ContinueWithRetry((Task<bool> b) => function(), retryCriteria, waitAmount, maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
			}, cancellationToken, options, TaskScheduler.Current);
			return task.Unwrap<T>();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006A64 File Offset: 0x00004C64
		public static Task<T> StartWithRetry<T>(Func<Task<T>> function, Func<Exception, bool> retryCriteria, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskCreationOptions options, TracingContext traceContext)
		{
			Task<Task<T>> task = Task.Factory.StartNew<Task<T>>(delegate()
			{
				Task<bool> task2 = Task.Factory.StartNew<bool>(() => true);
				return task2.ContinueWithRetry((bool b) => function(), retryCriteria, waitAmount, maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
			}, cancellationToken, options, TaskScheduler.Current);
			return task.Unwrap<T>();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006C3C File Offset: 0x00004E3C
		public static Task<T2> ContinueWithRetry<T1, T2>(this Task<T1> task, Func<T1, Task<T2>> function, Func<Exception, bool> retryCriteria, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task<T2>> task2 = task.ContinueWith<Task<T2>>(delegate(Task<T1> t)
			{
				Task<T2> result;
				try
				{
					Task<T2> task3 = function(t.Result);
					result = task3;
				}
				catch (Exception ex)
				{
					bool flag = retryCriteria(ex);
					if (maxRetries > 0 && flag)
					{
						WTFDiagnostics.TraceError<string>(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ContinueWithRetry]: Delegate failed, will retry. Exception message: {0}", ex.Message, null, "ContinueWithRetry", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 490);
						Task<T1> task4 = TaskExtension.NonBlockingWait<T1>(waitAmount, task, traceContext);
						Task<T2> task5 = task4.ContinueWithRetry(function, retryCriteria, 2 * waitAmount, --maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
						result = task5;
					}
					else
					{
						if (!flag)
						{
							WTFDiagnostics.TraceError<string>(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ContinueWithRetry]: Delegate failed with fatal error. Exception message: {0}", ex.Message, null, "ContinueWithRetry", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 497);
							throw;
						}
						string text = string.Format("Delegate failed and retry count has been exhausted. Exception message: {0}", ex.Message);
						WTFDiagnostics.TraceError(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ContinueWithRetry]: " + text, null, "ContinueWithRetry", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 503);
						throw new TimeoutException(text, ex);
					}
				}
				return result;
			}, cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap<T2>();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006E68 File Offset: 0x00005068
		public static Task<T2> ContinueWithRetry<T1, T2>(this Task<T1> task, Func<Task<T1>, T2> function, Func<Exception, bool> retryCriteria, int waitAmount, int maxRetries, CancellationToken cancellationToken, TaskContinuationOptions options, TracingContext traceContext)
		{
			Task<Task<T2>> task2 = task.ContinueWith<Task<T2>>(delegate(Task<T1> t)
			{
				T2 functionResult = default(T2);
				Task<T2> result;
				try
				{
					functionResult = function(t);
					result = Task.Factory.StartNew<T2>(() => functionResult, TaskCreationOptions.AttachedToParent);
				}
				catch (Exception ex)
				{
					bool flag = retryCriteria(ex);
					if (maxRetries > 0 && flag)
					{
						WTFDiagnostics.TraceError<string>(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ContinueWithRetry]: Delegate failed, will retry. Exception message: {0}", ex.Message, null, "ContinueWithRetry", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 574);
						Task<T1> task3 = TaskExtension.NonBlockingWait<T1>(waitAmount, task, traceContext);
						Task<T2> task4 = task3.ContinueWithRetry(function, retryCriteria, 2 * waitAmount, --maxRetries, cancellationToken, TaskContinuationOptions.None, traceContext);
						result = task4;
					}
					else
					{
						if (!flag)
						{
							WTFDiagnostics.TraceError<string>(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ContinueWithRetry]: Delegate failed with fatal error. Exception message: {0}", ex.Message, null, "ContinueWithRetry", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 581);
							throw;
						}
						string text = string.Format("Delegate failed and retry count has been exhausted. Exception message: {0}", ex.Message);
						WTFDiagnostics.TraceError(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.ContinueWithRetry]: " + text, null, "ContinueWithRetry", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 587);
						throw new TimeoutException(text, ex);
					}
				}
				return result;
			}, cancellationToken, options, TaskScheduler.Current);
			return task2.Unwrap<T2>();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006F88 File Offset: 0x00005188
		internal static Task<T> NonBlockingWait<T>(int waitAmount, Task<T> precedingTask, TracingContext traceContext)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>(TaskCreationOptions.AttachedToParent);
			TaskExtension.TaskCompletionSourceWithTimer<T> taskCompletionSourceWithTimer = new TaskExtension.TaskCompletionSourceWithTimer<T>
			{
				TaskCompletionSource = taskCompletionSource
			};
			Timer timer = new Timer(delegate(object x)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.CoreTracer, traceContext, "[TaskExtensions.NonBlockingWait]: Wait completed.", null, "NonBlockingWait", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TaskExtensions.cs", 617);
				TaskExtension.TaskCompletionSourceWithTimer<T> taskCompletionSourceWithTimer2 = (TaskExtension.TaskCompletionSourceWithTimer<T>)x;
				switch (precedingTask.Status)
				{
				case TaskStatus.RanToCompletion:
					taskCompletionSourceWithTimer2.TaskCompletionSource.SetResult(precedingTask.Result);
					break;
				case TaskStatus.Canceled:
					taskCompletionSourceWithTimer2.TaskCompletionSource.SetCanceled();
					break;
				case TaskStatus.Faulted:
					taskCompletionSourceWithTimer2.TaskCompletionSource.SetException(precedingTask.Exception);
					break;
				}
				taskCompletionSourceWithTimer2.Timer.Dispose();
			}, taskCompletionSourceWithTimer, -1, -1);
			taskCompletionSourceWithTimer.Timer = timer;
			timer.Change(waitAmount, -1);
			return taskCompletionSource.Task;
		}

		// Token: 0x0200000E RID: 14
		internal class TaskCompletionSourceWithTimer<T>
		{
			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000195 RID: 405 RVA: 0x00006FEE File Offset: 0x000051EE
			// (set) Token: 0x06000196 RID: 406 RVA: 0x00006FF6 File Offset: 0x000051F6
			internal TaskCompletionSource<T> TaskCompletionSource { get; set; }

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00006FFF File Offset: 0x000051FF
			// (set) Token: 0x06000198 RID: 408 RVA: 0x00007007 File Offset: 0x00005207
			internal Timer Timer { get; set; }
		}
	}
}
