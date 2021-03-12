using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D4 RID: 468
	internal class OwaAsyncResult : IAsyncResult
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x0003F9B2 File Offset: 0x0003DBB2
		internal OwaAsyncResult(AsyncCallback callback, object extraData, string channelId)
		{
			this.callback = callback;
			this.extraData = extraData;
			this.channelId = channelId;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0003F9CF File Offset: 0x0003DBCF
		public string PendingGetId
		{
			get
			{
				return this.channelId;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0003F9D7 File Offset: 0x0003DBD7
		public object AsyncState
		{
			get
			{
				return this.extraData;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
		public bool CompletedSynchronously
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.completedSynchronously;
				}
				return result;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0003FA20 File Offset: 0x0003DC20
		public bool IsCompleted
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.isCompleted;
				}
				return result;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0003FA60 File Offset: 0x0003DC60
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.setEvent == null)
				{
					this.setEvent = new ManualResetEvent(false);
				}
				return this.setEvent;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0003FA7C File Offset: 0x0003DC7C
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x0003FABC File Offset: 0x0003DCBC
		public Exception Exception
		{
			get
			{
				Exception result;
				lock (this)
				{
					result = this.exception;
				}
				return result;
			}
			set
			{
				lock (this)
				{
					if (this.isCompleted || this.exception != null)
					{
						throw new OwaInvalidOperationException("The request is already finished, or an exception was already registered for this OwaAsyncResult." + ((this.exception != null) ? ("Previous exception message: " + this.exception.Message) : string.Empty) + ((value != null) ? (" Current exception message: " + value.Message) : string.Empty));
					}
					this.exception = value;
				}
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0003FB58 File Offset: 0x0003DD58
		internal void CompleteRequest(bool completedSynchronously)
		{
			lock (this)
			{
				if (this.isCompleted)
				{
					return;
				}
				this.isCompleted = true;
				this.completedSynchronously = completedSynchronously;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
			if (this.setEvent != null)
			{
				this.setEvent.Set();
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0003FBD0 File Offset: 0x0003DDD0
		internal void CompleteRequest(bool completedSynchronously, Exception exception)
		{
			this.Exception = exception;
			this.CompleteRequest(completedSynchronously);
		}

		// Token: 0x040009CD RID: 2509
		private readonly string channelId;

		// Token: 0x040009CE RID: 2510
		private bool isCompleted;

		// Token: 0x040009CF RID: 2511
		private bool completedSynchronously;

		// Token: 0x040009D0 RID: 2512
		private AsyncCallback callback;

		// Token: 0x040009D1 RID: 2513
		private object extraData;

		// Token: 0x040009D2 RID: 2514
		private ManualResetEvent setEvent;

		// Token: 0x040009D3 RID: 2515
		private Exception exception;
	}
}
