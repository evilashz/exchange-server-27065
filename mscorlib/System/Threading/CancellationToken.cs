using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200051E RID: 1310
	[ComVisible(false)]
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct CancellationToken
	{
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000E79B4 File Offset: 0x000E5BB4
		[__DynamicallyInvokable]
		public static CancellationToken None
		{
			[__DynamicallyInvokable]
			get
			{
				return default(CancellationToken);
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000E79CA File Offset: 0x000E5BCA
		[__DynamicallyInvokable]
		public bool IsCancellationRequested
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_source != null && this.m_source.IsCancellationRequested;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003E5B RID: 15963 RVA: 0x000E79E1 File Offset: 0x000E5BE1
		[__DynamicallyInvokable]
		public bool CanBeCanceled
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_source != null && this.m_source.CanBeCanceled;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000E79F8 File Offset: 0x000E5BF8
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_source == null)
				{
					this.InitializeDefaultSource();
				}
				return this.m_source.WaitHandle;
			}
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000E7A13 File Offset: 0x000E5C13
		internal CancellationToken(CancellationTokenSource source)
		{
			this.m_source = source;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000E7A1C File Offset: 0x000E5C1C
		[__DynamicallyInvokable]
		public CancellationToken(bool canceled)
		{
			this = default(CancellationToken);
			if (canceled)
			{
				this.m_source = CancellationTokenSource.InternalGetStaticSource(canceled);
			}
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000E7A34 File Offset: 0x000E5C34
		private static void ActionToActionObjShunt(object obj)
		{
			Action action = obj as Action;
			action();
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000E7A4E File Offset: 0x000E5C4E
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(CancellationToken.s_ActionToActionObjShunt, callback, false, true);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000E7A6C File Offset: 0x000E5C6C
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(CancellationToken.s_ActionToActionObjShunt, callback, useSynchronizationContext, true);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000E7A8A File Offset: 0x000E5C8A
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback, state, false, true);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000E7AA4 File Offset: 0x000E5CA4
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x000E7AB0 File Offset: 0x000E5CB0
		internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000E7ABC File Offset: 0x000E5CBC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (!this.CanBeCanceled)
			{
				return default(CancellationTokenRegistration);
			}
			SynchronizationContext targetSyncContext = null;
			ExecutionContext executionContext = null;
			if (!this.IsCancellationRequested)
			{
				if (useSynchronizationContext)
				{
					targetSyncContext = SynchronizationContext.Current;
				}
				if (useExecutionContext)
				{
					executionContext = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.OptimizeDefaultCase);
				}
			}
			return this.m_source.InternalRegister(callback, state, targetSyncContext, executionContext);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000E7B1C File Offset: 0x000E5D1C
		[__DynamicallyInvokable]
		public bool Equals(CancellationToken other)
		{
			if (this.m_source == null && other.m_source == null)
			{
				return true;
			}
			if (this.m_source == null)
			{
				return other.m_source == CancellationTokenSource.InternalGetStaticSource(false);
			}
			if (other.m_source == null)
			{
				return this.m_source == CancellationTokenSource.InternalGetStaticSource(false);
			}
			return this.m_source == other.m_source;
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x000E7B77 File Offset: 0x000E5D77
		[__DynamicallyInvokable]
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000E7B8F File Offset: 0x000E5D8F
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.m_source == null)
			{
				return CancellationTokenSource.InternalGetStaticSource(false).GetHashCode();
			}
			return this.m_source.GetHashCode();
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000E7BB0 File Offset: 0x000E5DB0
		[__DynamicallyInvokable]
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000E7BBA File Offset: 0x000E5DBA
		[__DynamicallyInvokable]
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000E7BC7 File Offset: 0x000E5DC7
		[__DynamicallyInvokable]
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000E7BD7 File Offset: 0x000E5DD7
		internal void ThrowIfSourceDisposed()
		{
			if (this.m_source != null && this.m_source.IsDisposed)
			{
				CancellationToken.ThrowObjectDisposedException();
			}
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000E7BF3 File Offset: 0x000E5DF3
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException(Environment.GetResourceString("OperationCanceled"), this);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x000E7C0A File Offset: 0x000E5E0A
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("CancellationToken_SourceDisposed"));
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x000E7C1C File Offset: 0x000E5E1C
		private void InitializeDefaultSource()
		{
			this.m_source = CancellationTokenSource.InternalGetStaticSource(false);
		}

		// Token: 0x04001A01 RID: 6657
		private CancellationTokenSource m_source;

		// Token: 0x04001A02 RID: 6658
		private static readonly Action<object> s_ActionToActionObjShunt = new Action<object>(CancellationToken.ActionToActionObjShunt);
	}
}
