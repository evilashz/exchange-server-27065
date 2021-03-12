using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001AF RID: 431
	internal sealed class ClientExceptionLoggerEvent : ILogEvent
	{
		// Token: 0x060023B4 RID: 9140 RVA: 0x0006D4B8 File Offset: 0x0006B6B8
		internal ClientExceptionLoggerEvent(ClientWatson watson)
		{
			this.watson = watson;
			this.datapointProperties = new Dictionary<string, object>
			{
				{
					"TIME",
					watson.Time
				},
				{
					"MSG",
					HttpUtility.UrlEncode(watson.Message)
				},
				{
					"URL",
					watson.Url
				},
				{
					"LOC",
					HttpUtility.UrlEncode(watson.Location)
				},
				{
					"REQID",
					watson.RequestId
				},
				{
					"ST",
					HttpUtility.UrlEncode(watson.StackTrace)
				}
			};
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x0006D554 File Offset: 0x0006B754
		public string EventId
		{
			get
			{
				return this.watson.ErrorType;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0006D561 File Offset: 0x0006B761
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return this.datapointProperties;
		}

		// Token: 0x04001E23 RID: 7715
		private readonly ClientWatson watson;

		// Token: 0x04001E24 RID: 7716
		private Dictionary<string, object> datapointProperties;
	}
}
