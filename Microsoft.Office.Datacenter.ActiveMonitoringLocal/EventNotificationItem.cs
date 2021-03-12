using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200008E RID: 142
	public class EventNotificationItem : NotificationItem
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
		public EventNotificationItem(string serviceName, string component, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, notificationReason, severity)
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001D8F6 File Offset: 0x0001BAF6
		public EventNotificationItem(string serviceName, string component, string notificationReason, string message, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, message, severity)
		{
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001D905 File Offset: 0x0001BB05
		public EventNotificationItem(string serviceName, string component, string notificationReason, string message, string stateAttribute1, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, message, stateAttribute1, severity)
		{
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001D918 File Offset: 0x0001BB18
		public static void Publish(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("serviceName and component must have non-null/non-empty values.");
			}
			NotificationItem notificationItem = new EventNotificationItem(serviceName, component, tag, notificationReason, severity);
			notificationItem.Publish(throwOnError);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001D954 File Offset: 0x0001BB54
		public static void Publish(string serviceName, string component, string tag, string notificationReason, string stateAttribute1, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("serviceName and component must have non-null/non-empty values.");
			}
			NotificationItem notificationItem = new EventNotificationItem(serviceName, component, tag, notificationReason, stateAttribute1, severity);
			notificationItem.Publish(throwOnError);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001D992 File Offset: 0x0001BB92
		public static void PublishPeriodic(string serviceName, string component, string tag, string notificationReason, string periodicKey, TimeSpan period, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (string.IsNullOrEmpty(periodicKey))
			{
				throw new ArgumentException("periodicKey must have non-null/non-empty value.");
			}
			if (EventNotificationItem.CanPublishPeriodic(periodicKey, period))
			{
				EventNotificationItem.Publish(serviceName, component, tag, notificationReason, severity, throwOnError);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		private static bool CanPublishPeriodic(string periodicKey, TimeSpan period)
		{
			bool result;
			lock (EventNotificationItem.periodicEventDictionary)
			{
				DateTime d;
				if (!EventNotificationItem.periodicEventDictionary.TryGetValue(periodicKey, out d) || DateTime.UtcNow > d + period)
				{
					EventNotificationItem.periodicEventDictionary[periodicKey] = DateTime.UtcNow;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0400048A RID: 1162
		private static readonly Dictionary<string, DateTime> periodicEventDictionary = new Dictionary<string, DateTime>();
	}
}
