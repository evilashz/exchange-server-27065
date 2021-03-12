using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200003A RID: 58
	internal interface IAddressBookPerformanceCounters
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000274 RID: 628
		IExPerformanceCounter PID { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000275 RID: 629
		IExPerformanceCounter NspiConnectionsCurrent { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000276 RID: 630
		IExPerformanceCounter NspiConnectionsTotal { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000277 RID: 631
		IExPerformanceCounter NspiConnectionsRate { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000278 RID: 632
		IExPerformanceCounter NspiRequests { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000279 RID: 633
		IExPerformanceCounter NspiRequestsTotal { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600027A RID: 634
		IExPerformanceCounter NspiRequestsAverageLatency { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600027B RID: 635
		IExPerformanceCounter NspiRequestsRate { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600027C RID: 636
		IExPerformanceCounter NspiBrowseRequests { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600027D RID: 637
		IExPerformanceCounter NspiBrowseRequestsTotal { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600027E RID: 638
		IExPerformanceCounter NspiBrowseRequestsAverageLatency { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600027F RID: 639
		IExPerformanceCounter NspiBrowseRequestsRate { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000280 RID: 640
		IExPerformanceCounter RfrRequests { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000281 RID: 641
		IExPerformanceCounter RfrRequestsTotal { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000282 RID: 642
		IExPerformanceCounter RfrRequestsRate { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000283 RID: 643
		IExPerformanceCounter RfrRequestsAverageLatency { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000284 RID: 644
		IExPerformanceCounter ThumbnailPhotoAverageTime { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000285 RID: 645
		IExPerformanceCounter ThumbnailPhotoAverageTimeBase { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000286 RID: 646
		IExPerformanceCounter ThumbnailPhotoFromMailboxCount { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000287 RID: 647
		IExPerformanceCounter ThumbnailPhotoFromDirectoryCount { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000288 RID: 648
		IExPerformanceCounter ThumbnailPhotoNotPresentCount { get; }
	}
}
