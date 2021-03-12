using System;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200008A RID: 138
	internal class ResultWatcher<TResult> : CrimsonWatcher<TResult> where TResult : WorkItemResult, IPersistence, new()
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001D532 File Offset: 0x0001B732
		public ResultWatcher() : this(null, null, true, null)
		{
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001D53E File Offset: 0x0001B73E
		public ResultWatcher(string serviceName, EventBookmark bookmark, bool isReadExistingEvents) : this(serviceName, bookmark, isReadExistingEvents, null)
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001D54A File Offset: 0x0001B74A
		public ResultWatcher(string serviceName, EventBookmark bookmark, bool isReadExistingEvents, string channelName) : base(bookmark, isReadExistingEvents, channelName)
		{
			this.ServiceName = serviceName;
			base.QueryUserPropertyCondition = "(IsNotified=1)";
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001D568 File Offset: 0x0001B768
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001D570 File Offset: 0x0001B770
		public string ServiceName { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001D579 File Offset: 0x0001B779
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001D581 File Offset: 0x0001B781
		public ResultWatcher<TResult>.ResultArrivedDelegate ResultArrivedCallback { get; set; }

		// Token: 0x06000711 RID: 1809 RVA: 0x0001D58A File Offset: 0x0001B78A
		protected override void ResultArrivedHandler(TResult result)
		{
			if (this.ResultArrivedCallback != null)
			{
				this.ResultArrivedCallback(result);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
		protected override string GetDefaultXPathQuery()
		{
			return CrimsonHelper.BuildXPathQueryString(base.ChannelName, this.ServiceName, base.QueryStartTime, base.QueryEndTime, base.QueryUserPropertyCondition);
		}

		// Token: 0x04000474 RID: 1140
		private static readonly string resultClassName = typeof(TResult).Name;

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x06000715 RID: 1813
		public delegate void ResultArrivedDelegate(TResult result);
	}
}
