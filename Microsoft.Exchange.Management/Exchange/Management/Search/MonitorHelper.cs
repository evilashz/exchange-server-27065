using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000158 RID: 344
	internal class MonitorHelper
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00039F4F File Offset: 0x0003814F
		internal string MOMEventSource
		{
			get
			{
				return "MSExchange Monitoring ExchangeSearch";
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00039F56 File Offset: 0x00038156
		internal void AddMonitoringEvent(SearchTestResult searchTestResult, LocalizedString localizedString)
		{
			searchTestResult.DetailEvents.Add(new MonitoringEvent(this.MOMEventSource, 1002, EventTypeEnumeration.Information, localizedString, searchTestResult.Database));
			this.PushMessage(localizedString);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00039F87 File Offset: 0x00038187
		internal void PushMessage(LocalizedString msg)
		{
			this.messageStack.Push(msg);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00039F95 File Offset: 0x00038195
		internal LocalizedString PopMessage()
		{
			return this.messageStack.Pop();
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00039FA2 File Offset: 0x000381A2
		internal LocalizedString PeekMessage()
		{
			return this.messageStack.Peek();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00039FAF File Offset: 0x000381AF
		internal bool HasMessage()
		{
			return this.messageStack.Count > 0;
		}

		// Token: 0x04000626 RID: 1574
		private const string momEventSource = "MSExchange Monitoring ExchangeSearch";

		// Token: 0x04000627 RID: 1575
		private Stack<LocalizedString> messageStack = new Stack<LocalizedString>(5);
	}
}
