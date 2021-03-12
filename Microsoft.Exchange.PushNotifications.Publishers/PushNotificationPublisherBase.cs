using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000010 RID: 16
	internal abstract class PushNotificationPublisherBase : PushNotificationDisposable
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00002F8B File Offset: 0x0000118B
		protected PushNotificationPublisherBase(PushNotificationPublisherSettings settings, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			settings.Validate();
			this.Settings = settings;
			this.Tracer = tracer;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002FBD File Offset: 0x000011BD
		public string AppId
		{
			get
			{
				return this.Settings.AppId;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002FCA File Offset: 0x000011CA
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002FD2 File Offset: 0x000011D2
		public PushNotificationPublisherSettings Settings { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002FDB File Offset: 0x000011DB
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002FE3 File Offset: 0x000011E3
		public ITracer Tracer { get; private set; }

		// Token: 0x06000087 RID: 135
		public abstract void Publish(Notification notification, PushNotificationPublishingContext context);

		// Token: 0x06000088 RID: 136
		public abstract void Publish(PushNotification notification);

		// Token: 0x06000089 RID: 137 RVA: 0x00002FEC File Offset: 0x000011EC
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{appId:{0}}}", this.AppId);
			}
			return this.toStringCache;
		}

		// Token: 0x04000024 RID: 36
		private string toStringCache;
	}
}
