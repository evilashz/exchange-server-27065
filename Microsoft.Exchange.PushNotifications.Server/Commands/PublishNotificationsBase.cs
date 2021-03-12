using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.PushNotifications.Server.Core;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x0200000C RID: 12
	internal abstract class PublishNotificationsBase<TRequest> : ServiceCommand<TRequest, ServiceCommandResultNone>
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000029E5 File Offset: 0x00000BE5
		public PublishNotificationsBase(TRequest request, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(request, asyncCallback, asyncState)
		{
			this.PublisherManager = publisherManager;
			this.NotificationSource = base.Description;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002A04 File Offset: 0x00000C04
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002A0C File Offset: 0x00000C0C
		private protected string NotificationSource { protected get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002A15 File Offset: 0x00000C15
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002A1D File Offset: 0x00000C1D
		private protected PushNotificationPublisherManager PublisherManager { protected get; private set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002A26 File Offset: 0x00000C26
		protected sealed override ServiceCommandResultNone InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime)
		{
			this.Publish();
			return ServiceCommandResultNone.Instance;
		}

		// Token: 0x0600005A RID: 90
		protected abstract void Publish();

		// Token: 0x0600005B RID: 91 RVA: 0x00002A34 File Offset: 0x00000C34
		protected override ResourceKey[] InternalGetResources()
		{
			return new ResourceKey[]
			{
				ProcessorResourceKey.Local
			};
		}
	}
}
