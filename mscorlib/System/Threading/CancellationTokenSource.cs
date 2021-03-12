using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000518 RID: 1304
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CancellationTokenSource : IDisposable
	{
		// Token: 0x06003E27 RID: 15911 RVA: 0x000E6E78 File Offset: 0x000E5078
		private static void LinkedTokenCancelDelegate(object source)
		{
			CancellationTokenSource cancellationTokenSource = source as CancellationTokenSource;
			cancellationTokenSource.Cancel();
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000E6E92 File Offset: 0x000E5092
		[__DynamicallyInvokable]
		public bool IsCancellationRequested
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_state >= 2;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x000E6EA2 File Offset: 0x000E50A2
		internal bool IsCancellationCompleted
		{
			get
			{
				return this.m_state == 3;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x000E6EAF File Offset: 0x000E50AF
		internal bool IsDisposed
		{
			get
			{
				return this.m_disposed;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003E2C RID: 15916 RVA: 0x000E6EC2 File Offset: 0x000E50C2
		// (set) Token: 0x06003E2B RID: 15915 RVA: 0x000E6EB7 File Offset: 0x000E50B7
		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this.m_threadIDExecutingCallbacks;
			}
			set
			{
				this.m_threadIDExecutingCallbacks = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x000E6ECC File Offset: 0x000E50CC
		[__DynamicallyInvokable]
		public CancellationToken Token
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000E6EDA File Offset: 0x000E50DA
		internal bool CanBeCanceled
		{
			get
			{
				return this.m_state != 0;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x000E6EE8 File Offset: 0x000E50E8
		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_kernelEvent != null)
				{
					return this.m_kernelEvent;
				}
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_kernelEvent, manualResetEvent, null) != null)
				{
					((IDisposable)manualResetEvent).Dispose();
				}
				if (this.IsCancellationRequested)
				{
					this.m_kernelEvent.Set();
				}
				return this.m_kernelEvent;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000E6F48 File Offset: 0x000E5148
		internal CancellationCallbackInfo ExecutingCallback
		{
			get
			{
				return this.m_executingCallback;
			}
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000E6F52 File Offset: 0x000E5152
		[__DynamicallyInvokable]
		public CancellationTokenSource()
		{
			this.m_state = 1;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000E6F6C File Offset: 0x000E516C
		private CancellationTokenSource(bool set)
		{
			this.m_state = (set ? 3 : 0);
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000E6F8C File Offset: 0x000E518C
		[__DynamicallyInvokable]
		public CancellationTokenSource(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.InitializeWithTimer((int)num);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000E6FD2 File Offset: 0x000E51D2
		[__DynamicallyInvokable]
		public CancellationTokenSource(int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			this.InitializeWithTimer(millisecondsDelay);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000E6FF9 File Offset: 0x000E51F9
		private void InitializeWithTimer(int millisecondsDelay)
		{
			this.m_state = 1;
			this.m_timer = new Timer(CancellationTokenSource.s_timerCallback, this, millisecondsDelay, -1);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x000E7019 File Offset: 0x000E5219
		[__DynamicallyInvokable]
		public void Cancel()
		{
			this.Cancel(false);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x000E7022 File Offset: 0x000E5222
		[__DynamicallyInvokable]
		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x000E7034 File Offset: 0x000E5234
		[__DynamicallyInvokable]
		public void CancelAfter(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.CancelAfter((int)num);
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x000E706C File Offset: 0x000E526C
		[__DynamicallyInvokable]
		public void CancelAfter(int millisecondsDelay)
		{
			this.ThrowIfDisposed();
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (this.m_timer == null)
			{
				Timer timer = new Timer(CancellationTokenSource.s_timerCallback, this, -1, -1);
				if (Interlocked.CompareExchange<Timer>(ref this.m_timer, timer, null) != null)
				{
					timer.Dispose();
				}
			}
			try
			{
				this.m_timer.Change(millisecondsDelay, -1);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x000E70EC File Offset: 0x000E52EC
		private static void TimerCallbackLogic(object obj)
		{
			CancellationTokenSource cancellationTokenSource = (CancellationTokenSource)obj;
			if (!cancellationTokenSource.IsDisposed)
			{
				try
				{
					cancellationTokenSource.Cancel();
				}
				catch (ObjectDisposedException)
				{
					if (!cancellationTokenSource.IsDisposed)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000E7130 File Offset: 0x000E5330
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000E7140 File Offset: 0x000E5340
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_disposed)
				{
					return;
				}
				if (this.m_timer != null)
				{
					this.m_timer.Dispose();
				}
				CancellationTokenRegistration[] linkingRegistrations = this.m_linkingRegistrations;
				if (linkingRegistrations != null)
				{
					this.m_linkingRegistrations = null;
					for (int i = 0; i < linkingRegistrations.Length; i++)
					{
						linkingRegistrations[i].Dispose();
					}
				}
				this.m_registeredCallbacksLists = null;
				if (this.m_kernelEvent != null)
				{
					this.m_kernelEvent.Close();
					this.m_kernelEvent = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x000E71CB File Offset: 0x000E53CB
		internal void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				CancellationTokenSource.ThrowObjectDisposedException();
			}
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000E71DA File Offset: 0x000E53DA
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("CancellationTokenSource_Disposed"));
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000E71EC File Offset: 0x000E53EC
		internal static CancellationTokenSource InternalGetStaticSource(bool set)
		{
			if (!set)
			{
				return CancellationTokenSource._staticSource_NotCancelable;
			}
			return CancellationTokenSource._staticSource_Set;
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000E71FC File Offset: 0x000E53FC
		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
		{
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				this.ThrowIfDisposed();
			}
			if (!this.IsCancellationRequested)
			{
				if (this.m_disposed && !AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
				{
					return default(CancellationTokenRegistration);
				}
				int num = Thread.CurrentThread.ManagedThreadId % CancellationTokenSource.s_nLists;
				CancellationCallbackInfo cancellationCallbackInfo = new CancellationCallbackInfo(callback, stateForCallback, targetSyncContext, executionContext, this);
				SparselyPopulatedArray<CancellationCallbackInfo>[] array = this.m_registeredCallbacksLists;
				if (array == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo>[] array2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
					array = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this.m_registeredCallbacksLists, array2, null);
					if (array == null)
					{
						array = array2;
					}
				}
				SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num]);
				if (sparselyPopulatedArray == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> value = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num], value, null);
					sparselyPopulatedArray = array[num];
				}
				SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo = sparselyPopulatedArray.Add(cancellationCallbackInfo);
				CancellationTokenRegistration result = new CancellationTokenRegistration(cancellationCallbackInfo, registrationInfo);
				if (!this.IsCancellationRequested)
				{
					return result;
				}
				if (!result.TryDeregister())
				{
					return result;
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000E72F0 File Offset: 0x000E54F0
		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this.m_state, 2, 1) == 1)
			{
				Timer timer = this.m_timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				if (this.m_kernelEvent != null)
				{
					this.m_kernelEvent.Set();
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x000E7358 File Offset: 0x000E5558
		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			List<Exception> list = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this.m_registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this.m_state, 3);
				return;
			}
			try
			{
				for (int i = 0; i < registeredCallbacksLists.Length; i++)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[i]);
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this.m_executingCallback = sparselyPopulatedArrayFragment[j];
								if (this.m_executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										if (this.m_executingCallback.TargetSyncContext != null)
										{
											this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception item)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (list == null)
										{
											list = new List<Exception>();
										}
										list.Add(item);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.m_state = 3;
				this.m_executingCallback = null;
				Thread.MemoryBarrier();
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000E74B4 File Offset: 0x000E56B4
		private void CancellationCallbackCoreWork_OnSyncContext(object obj)
		{
			this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments)obj);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000E74C4 File Offset: 0x000E56C4
		private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
		{
			CancellationCallbackInfo cancellationCallbackInfo = args.m_currArrayFragment.SafeAtomicRemove(args.m_currArrayIndex, this.m_executingCallback);
			if (cancellationCallbackInfo == this.m_executingCallback)
			{
				if (cancellationCallbackInfo.TargetExecutionContext != null)
				{
					cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				}
				cancellationCallbackInfo.ExecuteCallback();
			}
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000E751C File Offset: 0x000E571C
		[__DynamicallyInvokable]
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			bool canBeCanceled = token2.CanBeCanceled;
			if (token1.CanBeCanceled)
			{
				cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[canBeCanceled ? 2 : 1];
				cancellationTokenSource.m_linkingRegistrations[0] = token1.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
			}
			if (canBeCanceled)
			{
				int num = 1;
				if (cancellationTokenSource.m_linkingRegistrations == null)
				{
					cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[1];
					num = 0;
				}
				cancellationTokenSource.m_linkingRegistrations[num] = token2.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
			}
			return cancellationTokenSource;
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000E75A0 File Offset: 0x000E57A0
		[__DynamicallyInvokable]
		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			if (tokens.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("CancellationToken_CreateLinkedToken_TokensIsEmpty"));
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i].CanBeCanceled)
				{
					cancellationTokenSource.m_linkingRegistrations[i] = tokens[i].InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
				}
			}
			return cancellationTokenSource;
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000E7620 File Offset: 0x000E5820
		internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == callbackInfo)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x040019E0 RID: 6624
		private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);

		// Token: 0x040019E1 RID: 6625
		private static readonly CancellationTokenSource _staticSource_NotCancelable = new CancellationTokenSource(false);

		// Token: 0x040019E2 RID: 6626
		private static readonly int s_nLists = (PlatformHelper.ProcessorCount > 24) ? 24 : PlatformHelper.ProcessorCount;

		// Token: 0x040019E3 RID: 6627
		private volatile ManualResetEvent m_kernelEvent;

		// Token: 0x040019E4 RID: 6628
		private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] m_registeredCallbacksLists;

		// Token: 0x040019E5 RID: 6629
		private const int CANNOT_BE_CANCELED = 0;

		// Token: 0x040019E6 RID: 6630
		private const int NOT_CANCELED = 1;

		// Token: 0x040019E7 RID: 6631
		private const int NOTIFYING = 2;

		// Token: 0x040019E8 RID: 6632
		private const int NOTIFYINGCOMPLETE = 3;

		// Token: 0x040019E9 RID: 6633
		private volatile int m_state;

		// Token: 0x040019EA RID: 6634
		private volatile int m_threadIDExecutingCallbacks = -1;

		// Token: 0x040019EB RID: 6635
		private bool m_disposed;

		// Token: 0x040019EC RID: 6636
		private CancellationTokenRegistration[] m_linkingRegistrations;

		// Token: 0x040019ED RID: 6637
		private static readonly Action<object> s_LinkedTokenCancelDelegate = new Action<object>(CancellationTokenSource.LinkedTokenCancelDelegate);

		// Token: 0x040019EE RID: 6638
		private volatile CancellationCallbackInfo m_executingCallback;

		// Token: 0x040019EF RID: 6639
		private volatile Timer m_timer;

		// Token: 0x040019F0 RID: 6640
		private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);
	}
}
