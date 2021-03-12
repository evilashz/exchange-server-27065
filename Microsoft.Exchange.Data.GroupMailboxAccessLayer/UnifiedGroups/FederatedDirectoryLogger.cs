using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class FederatedDirectoryLogger
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002404 File Offset: 0x00000604
		public static void AppendToLog(ILogEvent logEvent)
		{
			ArgumentValidator.ThrowIfNull("logEvent", logEvent);
			FederatedDirectoryLogger.AppendEventData(logEvent.GetEventData());
			FederatedDirectoryLogger.Instance.Value.LogEvent(logEvent);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000242C File Offset: 0x0000062C
		private static void AppendEventData(ICollection<KeyValuePair<string, object>> eventData)
		{
			eventData.Add(FederatedDirectoryLogger.ApplicationIdKeyValuePair.Value);
		}

		// Token: 0x04000004 RID: 4
		private static readonly Lazy<ExtensibleLogger> Instance = new Lazy<ExtensibleLogger>(() => new ExtensibleLogger(FederatedDirectoryLogConfiguration.Default));

		// Token: 0x04000005 RID: 5
		private static readonly string ApplicationIdName = "ApplicationId";

		// Token: 0x04000006 RID: 6
		private static readonly Lazy<KeyValuePair<string, object>> ApplicationIdKeyValuePair = new Lazy<KeyValuePair<string, object>>(() => new KeyValuePair<string, object>(FederatedDirectoryLogger.ApplicationIdName, ApplicationName.Current.Name));
	}
}
