using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001B2 RID: 434
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeriodicFailureEvent : ILogEvent
	{
		// Token: 0x060023BE RID: 9150 RVA: 0x0006D66C File Offset: 0x0006B86C
		public PeriodicFailureEvent(string activityContextId, string tenantName, string requestUrl, Exception exceptionToFilterBy, string flightInfo)
		{
			this.activityContextId = activityContextId;
			this.tenantName = this.TranslateStringValueToLog(tenantName);
			this.requestUrl = this.TranslateStringValueToLog(requestUrl);
			this.exceptionToFilterBy = this.TranslateStringValueToLog(exceptionToFilterBy.GetTraceFormatter().ToString());
			this.flightInfo = this.TranslateStringValueToLog(flightInfo);
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x0006D6C6 File Offset: 0x0006B8C6
		public string EventId
		{
			get
			{
				return "AppError";
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0006D6D0 File Offset: 0x0006B8D0
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("ACTID", this.activityContextId),
				new KeyValuePair<string, object>("TNAME", HttpUtility.UrlEncode(this.tenantName)),
				new KeyValuePair<string, object>("URL", HttpUtility.UrlEncode(this.requestUrl)),
				new KeyValuePair<string, object>("EX", HttpUtility.UrlEncode(this.exceptionToFilterBy)),
				new KeyValuePair<string, object>("INFO", HttpUtility.UrlEncode(this.flightInfo))
			};
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0006D785 File Offset: 0x0006B985
		private string TranslateStringValueToLog(string value)
		{
			if (value == null)
			{
				return "<null>";
			}
			if (value == string.Empty)
			{
				return "<empty>";
			}
			return value;
		}

		// Token: 0x04001E26 RID: 7718
		private const string eventId = "AppError";

		// Token: 0x04001E27 RID: 7719
		private readonly string activityContextId;

		// Token: 0x04001E28 RID: 7720
		private readonly string tenantName;

		// Token: 0x04001E29 RID: 7721
		private readonly string requestUrl;

		// Token: 0x04001E2A RID: 7722
		private readonly string exceptionToFilterBy;

		// Token: 0x04001E2B RID: 7723
		private readonly string flightInfo;
	}
}
