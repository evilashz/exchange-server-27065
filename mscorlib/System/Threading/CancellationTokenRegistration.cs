using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000517 RID: 1303
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable
	{
		// Token: 0x06003E1F RID: 15903 RVA: 0x000E6CD0 File Offset: 0x000E4ED0
		internal CancellationTokenRegistration(CancellationCallbackInfo callbackInfo, SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo)
		{
			this.m_callbackInfo = callbackInfo;
			this.m_registrationInfo = registrationInfo;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000E6CE0 File Offset: 0x000E4EE0
		[FriendAccessAllowed]
		internal bool TryDeregister()
		{
			if (this.m_registrationInfo.Source == null)
			{
				return false;
			}
			CancellationCallbackInfo cancellationCallbackInfo = this.m_registrationInfo.Source.SafeAtomicRemove(this.m_registrationInfo.Index, this.m_callbackInfo);
			return cancellationCallbackInfo == this.m_callbackInfo;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000E6D34 File Offset: 0x000E4F34
		[__DynamicallyInvokable]
		public void Dispose()
		{
			bool flag = this.TryDeregister();
			CancellationCallbackInfo callbackInfo = this.m_callbackInfo;
			if (callbackInfo != null)
			{
				CancellationTokenSource cancellationTokenSource = callbackInfo.CancellationTokenSource;
				if (cancellationTokenSource.IsCancellationRequested && !cancellationTokenSource.IsCancellationCompleted && !flag && cancellationTokenSource.ThreadIDExecutingCallbacks != Thread.CurrentThread.ManagedThreadId)
				{
					cancellationTokenSource.WaitForCallbackToComplete(this.m_callbackInfo);
				}
			}
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x000E6D8A File Offset: 0x000E4F8A
		[__DynamicallyInvokable]
		public static bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x000E6D94 File Offset: 0x000E4F94
		[__DynamicallyInvokable]
		public static bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000E6DA1 File Offset: 0x000E4FA1
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is CancellationTokenRegistration && this.Equals((CancellationTokenRegistration)obj);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000E6DBC File Offset: 0x000E4FBC
		[__DynamicallyInvokable]
		public bool Equals(CancellationTokenRegistration other)
		{
			return this.m_callbackInfo == other.m_callbackInfo && this.m_registrationInfo.Source == other.m_registrationInfo.Source && this.m_registrationInfo.Index == other.m_registrationInfo.Index;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000E6E18 File Offset: 0x000E5018
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.m_registrationInfo.Source != null)
			{
				return this.m_registrationInfo.Source.GetHashCode() ^ this.m_registrationInfo.Index.GetHashCode();
			}
			return this.m_registrationInfo.Index.GetHashCode();
		}

		// Token: 0x040019DE RID: 6622
		private readonly CancellationCallbackInfo m_callbackInfo;

		// Token: 0x040019DF RID: 6623
		private readonly SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> m_registrationInfo;
	}
}
