using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000038 RID: 56
	internal class ExEventLogWrapper : IExEventLog
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005708 File Offset: 0x00003908
		public ExEventLogWrapper(ExEventLog eventLog)
		{
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			this.eventLog = eventLog;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005722 File Offset: 0x00003922
		public ExEventSourceInfo EventSource
		{
			get
			{
				return this.eventLog.EventSource;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000572F File Offset: 0x0000392F
		public bool IsEventCategoryEnabled(short categoryNumber, ExEventLog.EventLevel level)
		{
			return this.eventLog.IsEventCategoryEnabled(categoryNumber, level);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000573E File Offset: 0x0000393E
		public void SetEventPeriod(int seconds)
		{
			this.eventLog.SetEventPeriod(seconds);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000574C File Offset: 0x0000394C
		public bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			return this.eventLog.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000575C File Offset: 0x0000395C
		public bool LogEventWithExtraData(ExEventLog.EventTuple tuple, string periodicKey, byte[] extraData, params object[] messageArgs)
		{
			return this.eventLog.LogEventWithExtraData(tuple, periodicKey, extraData, messageArgs);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000576E File Offset: 0x0000396E
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0)
		{
			return this.eventLog.LogEvent(organizationId, tuple, periodicKey, periodicKey, arg0);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005781 File Offset: 0x00003981
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1)
		{
			return this.eventLog.LogEvent(organizationId, tuple, periodicKey, periodicKey, arg0, arg1);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005796 File Offset: 0x00003996
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1, object arg2)
		{
			return this.eventLog.LogEvent(organizationId, tuple, periodicKey, periodicKey, arg0, arg1, arg2);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000057B0 File Offset: 0x000039B0
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1, object arg2, object arg3)
		{
			return this.eventLog.LogEvent(organizationId, tuple, periodicKey, new object[]
			{
				periodicKey,
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000057EB File Offset: 0x000039EB
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			return this.eventLog.LogEvent(organizationId, tuple, periodicKey, periodicKey, messageArgs);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000057FE File Offset: 0x000039FE
		public bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs)
		{
			return this.eventLog.LogEvent(organizationId, tuple, periodicKey, out fEventSuppressed, messageArgs);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005812 File Offset: 0x00003A12
		public bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs)
		{
			return this.eventLog.LogEvent(tuple, periodicKey, out fEventSuppressed, messageArgs);
		}

		// Token: 0x040000E7 RID: 231
		private readonly ExEventLog eventLog;
	}
}
