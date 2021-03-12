using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x0200001C RID: 28
	internal class DeviceThrottlingManager : IThrottlingManager
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000036C1 File Offset: 0x000018C1
		public bool TryApproveNotification(PushNotification notification, out OverBudgetException overBudgetException)
		{
			return this.TryApproveNotification(notification, DeviceThrottlingManager.ThrottlingType.Send, out overBudgetException);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036CC File Offset: 0x000018CC
		public void ReportInvalidNotifications(PushNotification notification)
		{
			OverBudgetException ex;
			if (!this.TryApproveNotification(notification, DeviceThrottlingManager.ThrottlingType.Invalid, out ex))
			{
				string text = (ex != null) ? ex.ToTraceString() : string.Empty;
				PushNotificationTracker.ReportDropped(notification, text);
				PushNotificationsCrimsonEvents.DeviceOverBudget.LogPeriodic<string, string, string>(notification.RecipientId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.ToFullString(), text);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003720 File Offset: 0x00001920
		private bool TryApproveNotification(PushNotification notification, DeviceThrottlingManager.ThrottlingType throttlingType, out OverBudgetException overBudgetException)
		{
			DeviceBudgetKey budgetKey = new DeviceBudgetKey(notification.RecipientId, notification.TenantId);
			bool result;
			using (IDeviceBudget deviceBudget = DeviceBudget.Acquire(budgetKey))
			{
				switch (throttlingType)
				{
				case DeviceThrottlingManager.ThrottlingType.Send:
					result = deviceBudget.TryApproveSendNotification(out overBudgetException);
					break;
				case DeviceThrottlingManager.ThrottlingType.Invalid:
					result = deviceBudget.TryApproveInvalidNotification(out overBudgetException);
					break;
				default:
					throw new NotSupportedException(throttlingType.ToString());
				}
			}
			return result;
		}

		// Token: 0x04000043 RID: 67
		public static readonly DeviceThrottlingManager Default = new DeviceThrottlingManager();

		// Token: 0x0200001D RID: 29
		private enum ThrottlingType
		{
			// Token: 0x04000045 RID: 69
			Send,
			// Token: 0x04000046 RID: 70
			Invalid
		}
	}
}
