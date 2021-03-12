using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200005D RID: 93
	public class AsyncEnumerator : IDisposable
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00008D11 File Offset: 0x00006F11
		public AsyncEnumerator(AsyncResultCallback callback, object asyncState, Func<AsyncEnumerator, IEnumerator<int>> enumeratorCallback) : this(callback, asyncState, enumeratorCallback, true)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008D1D File Offset: 0x00006F1D
		public AsyncEnumerator(AsyncCallback callback, object asyncState, Func<AsyncEnumerator, IEnumerator<int>> enumeratorCallback) : this(callback, asyncState, enumeratorCallback, true)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008D2C File Offset: 0x00006F2C
		public AsyncEnumerator(AsyncResultCallback callback, object asyncState, Func<AsyncEnumerator, IEnumerator<int>> enumeratorCallback, bool startAsyncOperation)
		{
			this.abortFunctions = new List<Action>();
			this.pendingResults = new List<IAsyncResult>();
			this.completedResults = new List<IAsyncResult>();
			base..ctor();
			this.AsyncResult = new AsyncResult(this, callback, asyncState);
			this.enumerator = enumeratorCallback(this);
			if (startAsyncOperation)
			{
				this.Begin();
			}
			this.ConstructorDone = true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008DAC File Offset: 0x00006FAC
		public AsyncEnumerator(AsyncCallback callback, object asyncState, Func<AsyncEnumerator, IEnumerator<int>> enumeratorCallback, bool startAsyncOperation)
		{
			this.abortFunctions = new List<Action>();
			this.pendingResults = new List<IAsyncResult>();
			this.completedResults = new List<IAsyncResult>();
			base..ctor();
			this.AsyncResult = new AsyncResult(this, delegate(AsyncResult ar)
			{
				if (callback != null)
				{
					callback(ar);
				}
			}, asyncState);
			this.enumerator = enumeratorCallback(this);
			if (startAsyncOperation)
			{
				this.Begin();
			}
			this.ConstructorDone = true;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008E2B File Offset: 0x0000702B
		protected AsyncEnumerator()
		{
			this.abortFunctions = new List<Action>();
			this.pendingResults = new List<IAsyncResult>();
			this.completedResults = new List<IAsyncResult>();
			base..ctor();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008E54 File Offset: 0x00007054
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.enumerator.Dispose();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008E70 File Offset: 0x00007070
		public IList<IAsyncResult> CompletedAsyncResults
		{
			get
			{
				return this.completedResults;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008E78 File Offset: 0x00007078
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00008E80 File Offset: 0x00007080
		public AsyncResult AsyncResult
		{
			get
			{
				return this.result;
			}
			protected set
			{
				this.result = value;
				this.result.OnAbort += this.Abort;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008EA0 File Offset: 0x000070A0
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00008EA8 File Offset: 0x000070A8
		public bool IsAborted { get; private set; }

		// Token: 0x060001DC RID: 476 RVA: 0x00008F30 File Offset: 0x00007130
		public AsyncCallback GetAsyncCallback()
		{
			if (!this.IsAborted && this.isDisposed)
			{
				throw new ObjectDisposedException("AsyncEnumerator disposed");
			}
			this.ThrowForMoreAsyncsAfterCompletion();
			bool flag = false;
			AsyncCallback asyncCallback;
			try
			{
				List<IAsyncResult> obj;
				Monitor.Enter(obj = this.pendingResults, ref flag);
				int callbackIndex = this.pendingResults.Count;
				this.pendingResults.Add(null);
				this.abortFunctions.Add(null);
				asyncCallback = delegate(IAsyncResult ar)
				{
					lock (this.pendingResults)
					{
						this.pendingResults[callbackIndex] = ar;
					}
					if (Interlocked.Decrement(ref this.pendingAsyncOps) == 0)
					{
						this.Advance();
					}
				};
			}
			finally
			{
				if (flag)
				{
					List<IAsyncResult> obj;
					Monitor.Exit(obj);
				}
			}
			return asyncCallback;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000904C File Offset: 0x0000724C
		public AsyncResultCallback GetAsyncResultCallback()
		{
			if (!this.IsAborted && this.isDisposed)
			{
				throw new ObjectDisposedException("AsyncEnumerator disposed");
			}
			this.ThrowForMoreAsyncsAfterCompletion();
			bool flag = false;
			AsyncResultCallback asyncResultCallback;
			try
			{
				List<IAsyncResult> obj;
				Monitor.Enter(obj = this.pendingResults, ref flag);
				int callbackIndex = this.pendingResults.Count;
				this.pendingResults.Add(null);
				this.abortFunctions.Add(null);
				asyncResultCallback = delegate(AsyncResult ar)
				{
					lock (this.pendingResults)
					{
						this.pendingResults[callbackIndex] = ar;
					}
					if (Interlocked.Decrement(ref this.pendingAsyncOps) == 0)
					{
						this.Advance();
					}
				};
			}
			finally
			{
				if (flag)
				{
					List<IAsyncResult> obj;
					Monitor.Exit(obj);
				}
			}
			return asyncResultCallback;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009168 File Offset: 0x00007368
		public AsyncResultCallback<T> GetAsyncResultCallback<T>()
		{
			if (!this.IsAborted && this.isDisposed)
			{
				throw new ObjectDisposedException("AsyncEnumerator disposed");
			}
			this.ThrowForMoreAsyncsAfterCompletion();
			bool flag = false;
			AsyncResultCallback<T> asyncResultCallback;
			try
			{
				List<IAsyncResult> obj;
				Monitor.Enter(obj = this.pendingResults, ref flag);
				int callbackIndex = this.pendingResults.Count;
				this.pendingResults.Add(null);
				this.abortFunctions.Add(null);
				asyncResultCallback = delegate(AsyncResult<T> ar)
				{
					lock (this.pendingResults)
					{
						this.pendingResults[callbackIndex] = ar;
					}
					if (Interlocked.Decrement(ref this.pendingAsyncOps) == 0)
					{
						this.Advance();
					}
				};
			}
			finally
			{
				if (flag)
				{
					List<IAsyncResult> obj;
					Monitor.Exit(obj);
				}
			}
			return asyncResultCallback;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009208 File Offset: 0x00007408
		public T AddAsync<T>(T asyncResult) where T : IAsyncResult
		{
			this.ThrowForMoreAsyncsAfterCompletion();
			lock (this.pendingResults)
			{
				this.pendingResults[this.pendingResults.Count - 1] = asyncResult;
				if (this.IsAborted)
				{
					this.AbortPendingResult(this.pendingResults.Count - 1);
				}
			}
			return asyncResult;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009284 File Offset: 0x00007484
		public T AddAsync<T>(T asyncResult, Action abortRequest) where T : IAsyncResult
		{
			this.ThrowForMoreAsyncsAfterCompletion();
			lock (this.pendingResults)
			{
				this.pendingResults[this.pendingResults.Count - 1] = asyncResult;
				this.abortFunctions[this.pendingResults.Count - 1] = abortRequest;
				if (this.IsAborted)
				{
					this.AbortPendingResult(this.pendingResults.Count - 1);
				}
			}
			return asyncResult;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009318 File Offset: 0x00007518
		public AsyncResult AddAsyncEnumerator(Func<AsyncEnumerator, IEnumerator<int>> enumerator)
		{
			return this.AddAsync<AsyncResult>(new AsyncEnumerator(this.GetAsyncResultCallback(), null, enumerator).AsyncResult);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009332 File Offset: 0x00007532
		public AsyncResult<T> AddAsyncEnumerator<T>(Func<AsyncEnumerator<T>, IEnumerator<int>> enumerator)
		{
			return this.AddAsync<AsyncResult<T>>(new AsyncEnumerator<T>(this.GetAsyncResultCallback<T>(), null, enumerator).AsyncResult);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000934C File Offset: 0x0000754C
		public void End()
		{
			this.AsyncResult.CompletedSynchronously = !this.ConstructorDone;
			this.AsyncResult.IsCompleted = true;
			this.VerifySuccessfullyCompleted();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009374 File Offset: 0x00007574
		public void Begin()
		{
			if (!this.enumerationStarted)
			{
				this.enumerationStarted = true;
				this.Advance();
				return;
			}
			throw new InvalidOperationException("Async enumeration already started");
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009396 File Offset: 0x00007596
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000939E File Offset: 0x0000759E
		protected bool ConstructorDone { get; set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x000093A8 File Offset: 0x000075A8
		private void Advance()
		{
			try
			{
				while (!this.IsAborted && !this.isDisposed)
				{
					List<IAsyncResult> list = this.pendingResults;
					this.pendingResults = this.completedResults;
					this.completedResults = list;
					this.pendingResults.Clear();
					this.abortFunctions.Clear();
					bool flag = this.enumerator.MoveNext();
					this.DisposeResults(this.completedResults);
					if (!flag)
					{
						if (!this.AsyncResult.IsCompleted)
						{
							throw new InvalidOperationException("Should call AsyncEnumerator.End before stopping the enumerator");
						}
						this.enumerator.Dispose();
						this.AsyncCompleted();
					}
					else
					{
						this.ThrowForMoreAsyncsAfterCompletion();
						if (this.enumerator.Current != this.pendingResults.Count)
						{
							throw new InvalidOperationException("for some reason number of async callbacks and expected callbacks doesn't match");
						}
						lock (this.pendingResults)
						{
							using (List<IAsyncResult>.Enumerator enumerator = this.pendingResults.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (enumerator.Current == null)
									{
										throw new InvalidOperationException("Not all async operations were added");
									}
								}
							}
						}
						if (Interlocked.Add(ref this.pendingAsyncOps, this.enumerator.Current) == 0)
						{
							continue;
						}
					}
					return;
				}
				this.DisposeResults(this.pendingResults);
				this.enumerator.Dispose();
			}
			catch (Exception exception)
			{
				this.enumerator.Dispose();
				if (this.AsyncResult.IsCompleted)
				{
					throw;
				}
				this.AsyncResult.Exception = exception;
				this.AsyncResult.IsCompleted = true;
				this.AsyncResult.CompletedSynchronously = !this.ConstructorDone;
				this.AsyncCompleted();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000959C File Offset: 0x0000779C
		protected virtual void AsyncCompleted()
		{
			this.DisposeResults(this.pendingResults);
			this.DisposeResults(this.completedResults);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000095B6 File Offset: 0x000077B6
		protected virtual void VerifySuccessfullyCompleted()
		{
			if (!this.AsyncResult.IsCompleted)
			{
				throw new InvalidOperationException("Wrong completion was called");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000095D0 File Offset: 0x000077D0
		protected virtual void ThrowForMoreAsyncsAfterCompletion()
		{
			if (this.AsyncResult.IsCompleted)
			{
				throw new InvalidOperationException("Can't do more asyncCalls after End has been called on AsyncEnumerator");
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000095EA File Offset: 0x000077EA
		protected void Begin(IEnumerator<int> enumerator, bool startEnumeration)
		{
			if (this.enumerator != null)
			{
				throw new InvalidOperationException("AsyncEnumerator already being used");
			}
			this.enumerator = enumerator;
			if (startEnumeration)
			{
				this.Begin();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009610 File Offset: 0x00007810
		private void Abort()
		{
			if (!this.IsAborted)
			{
				this.IsAborted = true;
				lock (this.pendingResults)
				{
					for (int i = 0; i < this.pendingResults.Count; i++)
					{
						this.AbortPendingResult(i);
					}
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009678 File Offset: 0x00007878
		private void AbortPendingResult(int iResult)
		{
			if (!this.pendingResults[iResult].IsCompleted)
			{
				if (this.abortFunctions[iResult] != null)
				{
					this.abortFunctions[iResult]();
				}
				AsyncResult asyncResult = this.pendingResults[iResult] as AsyncResult;
				if (asyncResult != null)
				{
					asyncResult.Abort();
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000096D4 File Offset: 0x000078D4
		private void DisposeResults(IList<IAsyncResult> asyncResults)
		{
			foreach (IAsyncResult asyncResult in asyncResults)
			{
				IDisposable disposable = asyncResult as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x04000195 RID: 405
		private List<Action> abortFunctions;

		// Token: 0x04000196 RID: 406
		private List<IAsyncResult> pendingResults;

		// Token: 0x04000197 RID: 407
		private List<IAsyncResult> completedResults;

		// Token: 0x04000198 RID: 408
		private AsyncResult result;

		// Token: 0x04000199 RID: 409
		protected IEnumerator<int> enumerator;

		// Token: 0x0400019A RID: 410
		private bool enumerationStarted;

		// Token: 0x0400019B RID: 411
		private bool isDisposed;

		// Token: 0x0400019C RID: 412
		private int pendingAsyncOps;
	}
}
