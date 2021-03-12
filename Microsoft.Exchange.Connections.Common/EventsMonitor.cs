using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EventsMonitor : IMonitorEvents
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000248B File Offset: 0x0000068B
		internal EventsMonitor(EventHandler<DownloadCompleteEventArgs> downloadsCompletedEventHandler, EventHandler<EventArgs> messagesDownloadedEventHandler, EventHandler<EventArgs> messagesUploadedEventHandler, EventHandler<RoundtripCompleteEventArgs> roundtripCompleteEventHandler)
		{
			this.DownloadsCompletedEventHandler = downloadsCompletedEventHandler;
			this.MessagesDownloadedEventHandler = messagesDownloadedEventHandler;
			this.MessagesUploadedEventHandler = messagesUploadedEventHandler;
			this.RoundtripCompleteEventHandler = roundtripCompleteEventHandler;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024B0 File Offset: 0x000006B0
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000024B8 File Offset: 0x000006B8
		public EventHandler<DownloadCompleteEventArgs> DownloadsCompletedEventHandler { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000024C1 File Offset: 0x000006C1
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000024C9 File Offset: 0x000006C9
		public EventHandler<EventArgs> MessagesDownloadedEventHandler { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000024D2 File Offset: 0x000006D2
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000024DA File Offset: 0x000006DA
		public EventHandler<EventArgs> MessagesUploadedEventHandler { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000024E3 File Offset: 0x000006E3
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000024EB File Offset: 0x000006EB
		public EventHandler<RoundtripCompleteEventArgs> RoundtripCompleteEventHandler { get; private set; }
	}
}
