using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IMonitorEvents
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64
		EventHandler<DownloadCompleteEventArgs> DownloadsCompletedEventHandler { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65
		EventHandler<EventArgs> MessagesDownloadedEventHandler { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66
		EventHandler<EventArgs> MessagesUploadedEventHandler { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67
		EventHandler<RoundtripCompleteEventArgs> RoundtripCompleteEventHandler { get; }
	}
}
