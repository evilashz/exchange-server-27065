using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000037 RID: 55
	internal interface IExEventLog
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000125 RID: 293
		ExEventSourceInfo EventSource { get; }

		// Token: 0x06000126 RID: 294
		bool IsEventCategoryEnabled(short categoryNumber, ExEventLog.EventLevel level);

		// Token: 0x06000127 RID: 295
		void SetEventPeriod(int seconds);

		// Token: 0x06000128 RID: 296
		bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs);

		// Token: 0x06000129 RID: 297
		bool LogEventWithExtraData(ExEventLog.EventTuple tuple, string periodicKey, byte[] extraData, params object[] messageArgs);

		// Token: 0x0600012A RID: 298
		bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0);

		// Token: 0x0600012B RID: 299
		bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1);

		// Token: 0x0600012C RID: 300
		bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1, object arg2);

		// Token: 0x0600012D RID: 301
		bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x0600012E RID: 302
		bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs);

		// Token: 0x0600012F RID: 303
		bool LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs);

		// Token: 0x06000130 RID: 304
		bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, out bool fEventSuppressed, params object[] messageArgs);
	}
}
