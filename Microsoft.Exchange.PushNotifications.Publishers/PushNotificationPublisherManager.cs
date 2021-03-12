using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000013 RID: 19
	internal class PushNotificationPublisherManager : PushNotificationDisposable
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000385D File Offset: 0x00001A5D
		public PushNotificationPublisherManager(IPushNotificationOptics optics = null)
		{
			this.optics = (optics ?? PushNotificationOptics.Default);
			this.registeredPublishers = new Dictionary<string, PushNotificationPublisherBase>();
			this.unsuitablePublishers = new HashSet<string>();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000388C File Offset: 0x00001A8C
		public void RegisterPublisher(PushNotificationPublisherBase publisher)
		{
			ArgumentValidator.ThrowIfNull("publisher", publisher);
			if (this.HasPublisher(publisher.AppId))
			{
				throw new ArgumentException("Publisher already registered :" + publisher.AppId);
			}
			this.registeredPublishers[publisher.AppId] = publisher;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038DA File Offset: 0x00001ADA
		public virtual void RegisterUnsuitablePublisher(string publisherName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("publisherName", publisherName);
			if (this.HasPublisher(publisherName))
			{
				throw new ArgumentException("Publisher already registered :" + publisherName);
			}
			this.unsuitablePublishers.Add(publisherName);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool HasPublisher(string appId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("appId", appId);
			return this.registeredPublishers.ContainsKey(appId) || this.HasUnsuitablePublisher(appId);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003932 File Offset: 0x00001B32
		public bool HasUnsuitablePublisher(string appId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("appId", appId);
			return this.unsuitablePublishers.Contains(appId);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000394C File Offset: 0x00001B4C
		public void Publish(MulticastNotification notification, PushNotificationPublishingContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			this.optics.ReportReceived(notification, context);
			if (notification != null && notification.IsValid)
			{
				using (IEnumerator<Notification> enumerator = notification.GetFragments().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Notification notification2 = enumerator.Current;
						this.Publish(notification2, context);
					}
					return;
				}
			}
			this.optics.ReportDiscardedByValidation(notification);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000039CC File Offset: 0x00001BCC
		public void Publish(Notification notification, PushNotificationPublishingContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			this.optics.ReportReceived(notification, context);
			if (notification == null || string.IsNullOrWhiteSpace(notification.AppId))
			{
				this.optics.ReportDiscardedByValidation(notification);
				return;
			}
			PushNotificationPublisherBase pushNotificationPublisherBase;
			if (this.registeredPublishers.TryGetValue(notification.AppId, out pushNotificationPublisherBase))
			{
				try
				{
					pushNotificationPublisherBase.Publish(notification, context);
					return;
				}
				catch (ObjectDisposedException)
				{
					this.optics.ReportDiscardedByDisposedPublisher(notification);
					return;
				}
			}
			if (notification.AppId == "MonitoringProbeAppId")
			{
				PushNotificationsMonitoring.PublishSuccessNotification("EnterpriseNotificationProcessed", "");
				return;
			}
			if (this.HasUnsuitablePublisher(notification.AppId))
			{
				this.optics.ReportDiscardedByUnsuitablePublisher(notification);
				return;
			}
			this.optics.ReportDiscardedByUnknownPublisher(notification);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003A98 File Offset: 0x00001C98
		public void Publish(PushNotification notification)
		{
			if (notification == null || string.IsNullOrWhiteSpace(notification.AppId))
			{
				this.optics.ReportDiscardedByValidation(notification, null);
				return;
			}
			PushNotificationPublisherBase pushNotificationPublisherBase;
			if (this.registeredPublishers.TryGetValue(notification.AppId, out pushNotificationPublisherBase))
			{
				try
				{
					pushNotificationPublisherBase.Publish(notification);
					return;
				}
				catch (ObjectDisposedException)
				{
					this.optics.ReportDiscardedByDisposedPublisher(notification);
					return;
				}
			}
			if (this.HasUnsuitablePublisher(notification.AppId))
			{
				this.optics.ReportDiscardedByUnsuitablePublisher(notification);
				return;
			}
			this.optics.ReportDiscardedByUnknownPublisher(notification);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003B28 File Offset: 0x00001D28
		public override string ToString()
		{
			return string.Format("{{publishers:[{0}]; unsuitablePublishers:[{1}]}}", string.Join<PushNotificationPublisherBase>("; ", this.registeredPublishers.Values), string.Join<string>("; ", this.unsuitablePublishers));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B5C File Offset: 0x00001D5C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				foreach (PushNotificationPublisherBase pushNotificationPublisherBase in this.registeredPublishers.Values)
				{
					pushNotificationPublisherBase.Dispose();
				}
				stopwatch.Stop();
				PushNotificationsCrimsonEvents.PushNotificationPublisherDisposed.Log<long>(stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003BD8 File Offset: 0x00001DD8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PushNotificationPublisherManager>(this);
		}

		// Token: 0x0400002F RID: 47
		private Dictionary<string, PushNotificationPublisherBase> registeredPublishers;

		// Token: 0x04000030 RID: 48
		private HashSet<string> unsuitablePublishers;

		// Token: 0x04000031 RID: 49
		private IPushNotificationOptics optics;
	}
}
