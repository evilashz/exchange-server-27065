using System;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp.PerformanceCounters;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002B RID: 43
	internal class NspiPerformanceCountersWrapper : IAddressBookPerformanceCounters
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000A5F6 File Offset: 0x000087F6
		public IExPerformanceCounter PID
		{
			get
			{
				return NspiPerformanceCounters.PID;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000A5FD File Offset: 0x000087FD
		public IExPerformanceCounter NspiConnectionsCurrent
		{
			get
			{
				return NspiPerformanceCounters.NspiConnectionsCurrent;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A604 File Offset: 0x00008804
		public IExPerformanceCounter NspiConnectionsTotal
		{
			get
			{
				return NspiPerformanceCounters.NspiConnectionsTotal;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A60B File Offset: 0x0000880B
		public IExPerformanceCounter NspiConnectionsRate
		{
			get
			{
				return NspiPerformanceCounters.NspiConnectionsRate;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A612 File Offset: 0x00008812
		public IExPerformanceCounter NspiRequests
		{
			get
			{
				return NspiPerformanceCounters.NspiRequests;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000A619 File Offset: 0x00008819
		public IExPerformanceCounter NspiRequestsTotal
		{
			get
			{
				return NspiPerformanceCounters.NspiRequestsTotal;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000A620 File Offset: 0x00008820
		public IExPerformanceCounter NspiRequestsAverageLatency
		{
			get
			{
				return NspiPerformanceCounters.NspiRequestsAverageLatency;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000A627 File Offset: 0x00008827
		public IExPerformanceCounter NspiRequestsRate
		{
			get
			{
				return NspiPerformanceCounters.NspiRequestsRate;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000A62E File Offset: 0x0000882E
		public IExPerformanceCounter NspiBrowseRequests
		{
			get
			{
				return NspiPerformanceCounters.NspiBrowseRequests;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000A635 File Offset: 0x00008835
		public IExPerformanceCounter NspiBrowseRequestsTotal
		{
			get
			{
				return NspiPerformanceCounters.NspiBrowseRequestsTotal;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000A63C File Offset: 0x0000883C
		public IExPerformanceCounter NspiBrowseRequestsAverageLatency
		{
			get
			{
				return NspiPerformanceCounters.NspiBrowseRequestsAverageLatency;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000A643 File Offset: 0x00008843
		public IExPerformanceCounter NspiBrowseRequestsRate
		{
			get
			{
				return NspiPerformanceCounters.NspiBrowseRequestsRate;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A64A File Offset: 0x0000884A
		public IExPerformanceCounter RfrRequests
		{
			get
			{
				return NspiPerformanceCounters.RfrRequests;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000A651 File Offset: 0x00008851
		public IExPerformanceCounter RfrRequestsTotal
		{
			get
			{
				return NspiPerformanceCounters.RfrRequestsTotal;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A658 File Offset: 0x00008858
		public IExPerformanceCounter RfrRequestsRate
		{
			get
			{
				return NspiPerformanceCounters.RfrRequestsRate;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A65F File Offset: 0x0000885F
		public IExPerformanceCounter RfrRequestsAverageLatency
		{
			get
			{
				return NspiPerformanceCounters.RfrRequestsAverageLatency;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A666 File Offset: 0x00008866
		public IExPerformanceCounter ThumbnailPhotoAverageTime
		{
			get
			{
				return NspiPerformanceCounters.ThumbnailPhotoAverageTime;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000A66D File Offset: 0x0000886D
		public IExPerformanceCounter ThumbnailPhotoAverageTimeBase
		{
			get
			{
				return NspiPerformanceCounters.ThumbnailPhotoAverageTimeBase;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A674 File Offset: 0x00008874
		public IExPerformanceCounter ThumbnailPhotoFromMailboxCount
		{
			get
			{
				return NspiPerformanceCounters.ThumbnailPhotoFromMailboxCount;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000A67B File Offset: 0x0000887B
		public IExPerformanceCounter ThumbnailPhotoFromDirectoryCount
		{
			get
			{
				return NspiPerformanceCounters.ThumbnailPhotoFromDirectoryCount;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A682 File Offset: 0x00008882
		public IExPerformanceCounter ThumbnailPhotoNotPresentCount
		{
			get
			{
				return NspiPerformanceCounters.ThumbnailPhotoNotPresentCount;
			}
		}
	}
}
