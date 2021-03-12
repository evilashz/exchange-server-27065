using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200016B RID: 363
	public class EdsTriggerNotificationItem : NotificationItem
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x000429F1 File Offset: 0x00040BF1
		internal EdsTriggerNotificationItem(string triggerName, ResultSeverityLevel severity, DateTime timeStamp) : base(EdsTriggerNotificationItem.edsNotificationServiceName, EdsTriggerNotificationItem.TriggerComponentName, triggerName, triggerName, severity)
		{
			base.TimeStamp = timeStamp;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00042A0D File Offset: 0x00040C0D
		internal EdsTriggerNotificationItem(string triggerName, double triggerValue, ResultSeverityLevel severity, DateTime timeStamp) : this(triggerName, severity, timeStamp)
		{
			base.SampleValue = triggerValue;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00042A20 File Offset: 0x00040C20
		internal static string TriggerComponentName
		{
			get
			{
				return "Trigger";
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00042A27 File Offset: 0x00040C27
		public static string GenerateResultName(string triggerName)
		{
			return NotificationItem.GenerateResultName(EdsTriggerNotificationItem.edsNotificationServiceName, EdsTriggerNotificationItem.TriggerComponentName, triggerName);
		}

		// Token: 0x04000783 RID: 1923
		private static readonly string edsNotificationServiceName = ExchangeComponent.Eds.Name;
	}
}
