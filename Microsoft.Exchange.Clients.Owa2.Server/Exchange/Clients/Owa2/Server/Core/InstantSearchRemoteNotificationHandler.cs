using System;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000184 RID: 388
	internal class InstantSearchRemoteNotificationHandler : IInstantSearchNotificationHandler, IDisposable
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x00034CB2 File Offset: 0x00032EB2
		public InstantSearchRemoteNotificationHandler(UserContext userContext)
		{
			this.userContext = userContext;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00034CCC File Offset: 0x00032ECC
		public void RegisterNotifier(string subscriptionId)
		{
			lock (this.syncRoot)
			{
				this.Notifier = new InstantSearchNotifier(subscriptionId, this.userContext);
			}
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00034D18 File Offset: 0x00032F18
		public void DeliverInstantSearchPayload(InstantSearchPayloadType instantSearchPayloadType)
		{
			lock (this.syncRoot)
			{
				if (this.Notifier != null)
				{
					this.Notifier.AddPayload(new InstantSearchNotificationPayload(this.notifier.SubscriptionId, instantSearchPayloadType));
				}
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00034D78 File Offset: 0x00032F78
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x00034D80 File Offset: 0x00032F80
		private InstantSearchNotifier Notifier
		{
			get
			{
				return this.notifier;
			}
			set
			{
				if (this.notifier == value)
				{
					return;
				}
				if (this.notifier != null)
				{
					this.notifier.UnregisterWithPendingRequestNotifier();
				}
				this.notifier = value;
				if (value != null)
				{
					value.RegisterWithPendingRequestNotifier();
				}
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00034DB0 File Offset: 0x00032FB0
		public void Dispose()
		{
			lock (this.syncRoot)
			{
				this.Notifier = null;
			}
		}

		// Token: 0x04000873 RID: 2163
		private readonly UserContext userContext;

		// Token: 0x04000874 RID: 2164
		private readonly object syncRoot = new object();

		// Token: 0x04000875 RID: 2165
		private InstantSearchNotifier notifier;
	}
}
