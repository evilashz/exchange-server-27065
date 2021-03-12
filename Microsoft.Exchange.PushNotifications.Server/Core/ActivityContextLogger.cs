using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000003 RID: 3
	internal sealed class ActivityContextLogger : ExtensibleLogger
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000222E File Offset: 0x0000042E
		public ActivityContextLogger() : base(new ActivityContextLogConfig())
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000223C File Offset: 0x0000043C
		public static void Initialize()
		{
			if (ActivityContextLogger.instance == null)
			{
				ActivityContext.RegisterMetadata(typeof(ServiceCommonMetadata));
				ActivityContext.RegisterMetadata(typeof(ServiceLatencyMetadata));
				ActivityContext.RegisterMetadata(typeof(BudgetMetadata));
				ActivityContext.RegisterMetadata(typeof(ActivityContextLogger.PushNotificationData));
				ActivityContextLogger.instance = new ActivityContextLogger();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002298 File Offset: 0x00000498
		protected override ICollection<KeyValuePair<string, object>> GetComponentSpecificData(IActivityScope activityScope, string eventId)
		{
			Dictionary<string, object> dictionary = null;
			if (activityScope != null)
			{
				dictionary = new Dictionary<string, object>(ActivityContextLogger.EnumToShortKeyMapping.Count);
				ExtensibleLogger.CopyProperties(activityScope, dictionary, ActivityContextLogger.EnumToShortKeyMapping);
			}
			return dictionary;
		}

		// Token: 0x04000010 RID: 16
		public static readonly Dictionary<Enum, string> EnumToShortKeyMapping = new Dictionary<Enum, string>
		{
			{
				ActivityStandardMetadata.Action,
				"Cmd"
			},
			{
				ActivityContextLogger.PushNotificationData.ServiceCommandError,
				"CmdErr"
			}
		};

		// Token: 0x04000011 RID: 17
		private static ActivityContextLogger instance;

		// Token: 0x02000004 RID: 4
		public enum PushNotificationData
		{
			// Token: 0x04000013 RID: 19
			ServiceCommandError
		}
	}
}
