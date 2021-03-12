using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000016 RID: 22
	internal class CancelableAsyncResultWrapper<T> : ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x000062B5 File Offset: 0x000044B5
		public CancelableAsyncResultWrapper(Task<T> wrappedTask)
		{
			this.wrappedTask = wrappedTask;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000062C4 File Offset: 0x000044C4
		public Task<T> Task
		{
			get
			{
				return this.wrappedTask;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000062CC File Offset: 0x000044CC
		object IAsyncResult.AsyncState
		{
			get
			{
				return ((IAsyncResult)this.wrappedTask).AsyncState;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000062D9 File Offset: 0x000044D9
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.wrappedTask).AsyncWaitHandle;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000062E6 File Offset: 0x000044E6
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return ((IAsyncResult)this.wrappedTask).CompletedSynchronously;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000062F3 File Offset: 0x000044F3
		bool IAsyncResult.IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.wrappedTask).IsCompleted;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006300 File Offset: 0x00004500
		bool ICancelableAsyncResult.IsCanceled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006307 File Offset: 0x00004507
		void ICancelableAsyncResult.Cancel()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400006D RID: 109
		private Task<T> wrappedTask;
	}
}
