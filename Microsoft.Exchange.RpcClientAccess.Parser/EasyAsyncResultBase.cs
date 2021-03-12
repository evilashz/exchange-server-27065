using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200009D RID: 157
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EasyAsyncResultBase : IAsyncResult
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		public EasyAsyncResultBase(object asyncState)
		{
			this.asyncState = asyncState;
			this.isCompleted = false;
			this.completionEvent = null;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000D9E7 File Offset: 0x0000BBE7
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.completionEvent == null)
				{
					lock (this.completionLock)
					{
						if (this.completionEvent == null)
						{
							this.completionEvent = new ManualResetEvent(this.isCompleted);
						}
					}
				}
				return this.completionEvent;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000DA58 File Offset: 0x0000BC58
		public bool IsCompleted
		{
			get
			{
				bool result;
				lock (this.completionLock)
				{
					result = this.isCompleted;
				}
				return result;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public void WaitForCompletion()
		{
			lock (this.completionLock)
			{
				if (this.isCompleted)
				{
					return;
				}
			}
			this.AsyncWaitHandle.WaitOne();
			lock (this.completionLock)
			{
				Util.DisposeIfPresent(this.completionEvent);
				this.completionEvent = null;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000DB28 File Offset: 0x0000BD28
		internal void InvokeCallback()
		{
			lock (this.completionLock)
			{
				if (this.isCompleted)
				{
					return;
				}
				this.isCompleted = true;
			}
			if (this.completionEvent != null)
			{
				this.completionEvent.Set();
			}
			this.InternalCallback();
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000DB90 File Offset: 0x0000BD90
		protected object CompletionLock
		{
			get
			{
				return this.completionLock;
			}
		}

		// Token: 0x060003E0 RID: 992
		protected abstract void InternalCallback();

		// Token: 0x04000260 RID: 608
		private readonly object completionLock = new object();

		// Token: 0x04000261 RID: 609
		private object asyncState;

		// Token: 0x04000262 RID: 610
		private bool isCompleted;

		// Token: 0x04000263 RID: 611
		private ManualResetEvent completionEvent;
	}
}
