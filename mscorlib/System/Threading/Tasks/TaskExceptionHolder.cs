using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000548 RID: 1352
	internal class TaskExceptionHolder
	{
		// Token: 0x06004077 RID: 16503 RVA: 0x000F0368 File Offset: 0x000EE568
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
			TaskExceptionHolder.EnsureADUnloadCallbackRegistered();
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000F037C File Offset: 0x000EE57C
		[SecuritySafeCritical]
		private static bool ShouldFailFastOnUnobservedException()
		{
			return CLRConfig.CheckThrowUnobservedTaskExceptions();
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000F0392 File Offset: 0x000EE592
		private static void EnsureADUnloadCallbackRegistered()
		{
			if (TaskExceptionHolder.s_adUnloadEventHandler == null && Interlocked.CompareExchange<EventHandler>(ref TaskExceptionHolder.s_adUnloadEventHandler, new EventHandler(TaskExceptionHolder.AppDomainUnloadCallback), null) == null)
			{
				AppDomain.CurrentDomain.DomainUnload += TaskExceptionHolder.s_adUnloadEventHandler;
			}
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000F03C7 File Offset: 0x000EE5C7
		private static void AppDomainUnloadCallback(object sender, EventArgs e)
		{
			TaskExceptionHolder.s_domainUnloadStarted = true;
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000F03D4 File Offset: 0x000EE5D4
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled && !Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !TaskExceptionHolder.s_domainUnloadStarted)
				{
					foreach (ExceptionDispatchInfo exceptionDispatchInfo in this.m_faultExceptions)
					{
						Exception sourceException = exceptionDispatchInfo.SourceException;
						AggregateException ex = sourceException as AggregateException;
						if (ex != null)
						{
							AggregateException ex2 = ex.Flatten();
							using (IEnumerator<Exception> enumerator2 = ex2.InnerExceptions.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Exception ex3 = enumerator2.Current;
									if (ex3 is ThreadAbortException)
									{
										return;
									}
								}
								continue;
							}
						}
						if (sourceException is ThreadAbortException)
						{
							return;
						}
					}
					AggregateException ex4 = new AggregateException(Environment.GetResourceString("TaskExceptionHolder_UnhandledException"), this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs = new UnobservedTaskExceptionEventArgs(ex4);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, unobservedTaskExceptionEventArgs);
					if (TaskExceptionHolder.s_failFastOnUnobservedException && !unobservedTaskExceptionEventArgs.m_observed)
					{
						throw ex4;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000F054C File Offset: 0x000EE74C
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000F0559 File Offset: 0x000EE759
		internal void Add(object exceptionObject)
		{
			this.Add(exceptionObject, false);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000F0563 File Offset: 0x000EE763
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000F0578 File Offset: 0x000EE778
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo cancellationException = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = cancellationException;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000F05B4 File Offset: 0x000EE7B4
		private void AddFaultException(object exceptionObject)
		{
			List<ExceptionDispatchInfo> list = this.m_faultExceptions;
			if (list == null)
			{
				list = (this.m_faultExceptions = new List<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				list.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					list.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception source = enumerator.Current;
								list.Add(ExceptionDispatchInfo.Capture(source));
							}
							goto IL_B3;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException(Environment.GetResourceString("TaskExceptionHolder_UnknownExceptionType"), "exceptionObject");
					}
					list.AddRange(enumerable2);
				}
			}
			IL_B3:
			for (int i = 0; i < list.Count; i++)
			{
				Type type = list[i].SourceException.GetType();
				if (type != typeof(ThreadAbortException) && type != typeof(AppDomainUnloadedException))
				{
					this.MarkAsUnhandled();
					return;
				}
				if (i == list.Count - 1)
				{
					this.MarkAsHandled(false);
				}
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000F06F0 File Offset: 0x000EE8F0
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000F070B File Offset: 0x000EE90B
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000F072C File Offset: 0x000EE92C
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			array[array.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000F0790 File Offset: 0x000EE990
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000F07B4 File Offset: 0x000EE9B4
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x04001AA7 RID: 6823
		private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();

		// Token: 0x04001AA8 RID: 6824
		private static volatile bool s_domainUnloadStarted;

		// Token: 0x04001AA9 RID: 6825
		private static volatile EventHandler s_adUnloadEventHandler;

		// Token: 0x04001AAA RID: 6826
		private readonly Task m_task;

		// Token: 0x04001AAB RID: 6827
		private volatile List<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04001AAC RID: 6828
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04001AAD RID: 6829
		private volatile bool m_isHandled;
	}
}
