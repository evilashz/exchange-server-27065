using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000794 RID: 1940
	internal abstract class BaseTestStep : ITestStep
	{
		// Token: 0x0600268A RID: 9866
		protected abstract void StartTest();

		// Token: 0x0600268B RID: 9867 RVA: 0x0005169C File Offset: 0x0004F89C
		protected virtual void Finally()
		{
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0005169E File Offset: 0x0004F89E
		protected virtual void ExceptionThrown(ScenarioException e)
		{
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600268D RID: 9869
		protected abstract TestId Id { get; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000516A0 File Offset: 0x0004F8A0
		public virtual object Result
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000516A3 File Offset: 0x0004F8A3
		// (set) Token: 0x06002690 RID: 9872 RVA: 0x000516AB File Offset: 0x0004F8AB
		public TimeSpan? MaxRunTime { get; set; }

		// Token: 0x06002691 RID: 9873 RVA: 0x000516B4 File Offset: 0x0004F8B4
		public IAsyncResult BeginExecute(IHttpSession session, AsyncCallback callback, object state)
		{
			this.session = session;
			this.asyncResult = new LazyAsyncResult(callback, state);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.KickoffTest), null);
			return this.asyncResult;
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000516E4 File Offset: 0x0004F8E4
		public void EndExecute(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = result as LazyAsyncResult;
			if (!lazyAsyncResult.IsCompleted)
			{
				lazyAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (lazyAsyncResult.Exception != null)
			{
				Exception ex = lazyAsyncResult.Exception as Exception;
				throw ex;
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00051722 File Offset: 0x0004F922
		public Task CreateTask(IHttpSession session)
		{
			return Task.Factory.FromAsync<IHttpSession>(new Func<IHttpSession, AsyncCallback, object, IAsyncResult>(this.BeginExecute), new Action<IAsyncResult>(this.EndExecute), session, null, TaskCreationOptions.None);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0005174C File Offset: 0x0004F94C
		protected void ExecutionCompletedSuccessfully()
		{
			this.session.NotifyTestFinished(this.Id);
			this.Finally();
			ScenarioException exception = null;
			if (this.MaxRunTime != null)
			{
				exception = this.session.VerifyScenarioExceededRunTime(this.MaxRunTime);
			}
			this.asyncResult.Complete(null, exception);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000517A4 File Offset: 0x0004F9A4
		protected void AsyncCallbackWrapper(AsyncCallback wrappedCallback, IAsyncResult result)
		{
			try
			{
				wrappedCallback.DynamicInvoke(new object[]
				{
					result
				});
			}
			catch (Exception ex)
			{
				this.ReportException(ex);
				this.Finally();
				this.asyncResult.Complete(null, ex);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000517F4 File Offset: 0x0004F9F4
		private void KickoffTest(object target)
		{
			try
			{
				this.session.NotifyTestStarted(this.Id);
				this.StartTest();
			}
			catch (Exception ex)
			{
				this.ReportException(ex);
				this.Finally();
				this.asyncResult.Complete(null, ex);
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x00051848 File Offset: 0x0004FA48
		private void ReportException(Exception e)
		{
			ScenarioException scenarioException = e.GetScenarioException();
			if (scenarioException != null)
			{
				this.ExceptionThrown(e.GetScenarioException());
			}
		}

		// Token: 0x0400232A RID: 9002
		protected const string UrlEncodedFormContentType = "application/x-www-form-urlencoded";

		// Token: 0x0400232B RID: 9003
		protected const string JsonContentType = "application/json; charset=utf-8";

		// Token: 0x0400232C RID: 9004
		protected LazyAsyncResult asyncResult;

		// Token: 0x0400232D RID: 9005
		protected IHttpSession session;
	}
}
