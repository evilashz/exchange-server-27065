using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x020004B2 RID: 1202
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class StorageLoggerBase : IExtensibleLogger, IWorkloadLogger
	{
		// Token: 0x06003567 RID: 13671 RVA: 0x000D778C File Offset: 0x000D598C
		internal StorageLoggerBase(IExtensibleLogger logger)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.logger = logger;
			this.ActivityId = (ActivityContext.ActivityId ?? Guid.NewGuid());
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06003568 RID: 13672
		protected abstract string TenantName { get; }

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06003569 RID: 13673
		protected abstract Guid MailboxGuid { get; }

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000D77D4 File Offset: 0x000D59D4
		// (set) Token: 0x0600356B RID: 13675 RVA: 0x000D77DC File Offset: 0x000D59DC
		private protected Guid ActivityId { protected get; private set; }

		// Token: 0x0600356C RID: 13676 RVA: 0x000D77E5 File Offset: 0x000D59E5
		public void LogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			this.logger.LogActivityEvent(activityScope, eventType);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000D77F4 File Offset: 0x000D59F4
		public virtual void LogEvent(ILogEvent logEvent)
		{
			ArgumentValidator.ThrowIfNull("logEvent", logEvent);
			this.AppendEventData(logEvent.GetEventData());
			this.logger.LogEvent(logEvent);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000D781C File Offset: 0x000D5A1C
		public void LogEvent(IEnumerable<ILogEvent> logEvents)
		{
			ArgumentValidator.ThrowIfNull("logEvents", logEvents);
			foreach (ILogEvent logEvent in logEvents)
			{
				this.LogEvent(logEvent);
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000D7870 File Offset: 0x000D5A70
		protected virtual void AppendEventData(ICollection<KeyValuePair<string, object>> eventData)
		{
			if (!string.IsNullOrEmpty(this.TenantName))
			{
				eventData.Add(new KeyValuePair<string, object>(StorageLoggerBase.TenantNameFieldName, this.TenantName));
			}
			eventData.Add(new KeyValuePair<string, object>(StorageLoggerBase.MailboxGuidFieldName, this.MailboxGuid));
			eventData.Add(new KeyValuePair<string, object>(StorageLoggerBase.ActivityIdFieldName, this.ActivityId));
		}

		// Token: 0x04001C5F RID: 7263
		internal static readonly string TenantNameFieldName = "TenantName";

		// Token: 0x04001C60 RID: 7264
		internal static readonly string MailboxGuidFieldName = "MailboxGuid";

		// Token: 0x04001C61 RID: 7265
		internal static readonly string ActivityIdFieldName = "ActivityId";

		// Token: 0x04001C62 RID: 7266
		private readonly IExtensibleLogger logger;
	}
}
