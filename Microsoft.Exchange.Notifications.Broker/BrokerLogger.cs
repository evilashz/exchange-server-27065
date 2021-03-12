using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BrokerLogger : ExtensibleLogger
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002283 File Offset: 0x00000483
		public BrokerLogger() : base(new BrokerLogConfiguration())
		{
			ActivityContext.RegisterMetadata(typeof(LogField));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000229F File Offset: 0x0000049F
		public static void Initialize()
		{
			if (BrokerLogger.instance == null)
			{
				BrokerLogger.instance = new BrokerLogger();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B4 File Offset: 0x000004B4
		public static ActivityScope StartEvent(string eventId)
		{
			ActivityScope activityScope = ActivityContext.Start(null);
			activityScope.SetProperty(ExtensibleLoggerMetadata.EventId, eventId);
			return activityScope;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022D8 File Offset: 0x000004D8
		public static void Set(LogField field, object value)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.SetProperty(field, value.ToString());
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002300 File Offset: 0x00000500
		public static void Append(LogField field, object value)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.AppendToProperty(field, value.ToString());
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000232C File Offset: 0x0000052C
		protected override ICollection<KeyValuePair<string, object>> GetComponentSpecificData(IActivityScope activityScope, string eventId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(20);
			IEnumerable<KeyValuePair<string, object>> formattableMetadata = activityScope.GetFormattableMetadata();
			foreach (KeyValuePair<string, object> keyValuePair in formattableMetadata)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x0400000C RID: 12
		internal const string SubscribeEvent = "Subscribe";

		// Token: 0x0400000D RID: 13
		internal const string UnsubscribeEvent = "Unsubscribe";

		// Token: 0x0400000E RID: 14
		internal const string GetNextNotificationEvent = "GetNextNotification";

		// Token: 0x0400000F RID: 15
		internal const string HandleNotificationEvent = "HandleNotification";

		// Token: 0x04000010 RID: 16
		internal const string RemoteSubscribeEvent = "RemoteSubscribe";

		// Token: 0x04000011 RID: 17
		internal const string RemoteUnsubscribeEvent = "RemoteUnsubscribe";

		// Token: 0x04000012 RID: 18
		internal const string KeepAliveEvent = "KeepAlive";

		// Token: 0x04000013 RID: 19
		internal const string CheckMailboxEvent = "CheckMailbox";

		// Token: 0x04000014 RID: 20
		internal const string InitializeServiceEvent = "InitializeService";

		// Token: 0x04000015 RID: 21
		internal const string CleanupServiceEvent = "CleanupService";

		// Token: 0x04000016 RID: 22
		internal const string MailboxManagerCleanupTimerEvent = "MailboxManagerCleanupTimer";

		// Token: 0x04000017 RID: 23
		private static BrokerLogger instance;
	}
}
