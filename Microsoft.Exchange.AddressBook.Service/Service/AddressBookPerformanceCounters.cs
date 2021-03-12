using System;
using Microsoft.Exchange.AddressBook.Service.PerformanceCounters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200003B RID: 59
	internal class AddressBookPerformanceCounters : IAddressBookPerformanceCounters
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00011544 File Offset: 0x0000F744
		public IExPerformanceCounter PID
		{
			get
			{
				return AddressBookCounters.PID;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0001154B File Offset: 0x0000F74B
		public IExPerformanceCounter NspiConnectionsCurrent
		{
			get
			{
				return AddressBookCounters.NspiConnectionsCurrent;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00011552 File Offset: 0x0000F752
		public IExPerformanceCounter NspiConnectionsTotal
		{
			get
			{
				return AddressBookCounters.NspiConnectionsTotal;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00011559 File Offset: 0x0000F759
		public IExPerformanceCounter NspiConnectionsRate
		{
			get
			{
				return AddressBookCounters.NspiConnectionsRate;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00011560 File Offset: 0x0000F760
		public IExPerformanceCounter NspiRequests
		{
			get
			{
				return AddressBookCounters.NspiRpcRequests;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00011567 File Offset: 0x0000F767
		public IExPerformanceCounter NspiRequestsTotal
		{
			get
			{
				return AddressBookCounters.NspiRpcRequestsTotal;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0001156E File Offset: 0x0000F76E
		public IExPerformanceCounter NspiRequestsAverageLatency
		{
			get
			{
				return AddressBookCounters.NspiRpcRequestsAverageLatency;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00011575 File Offset: 0x0000F775
		public IExPerformanceCounter NspiRequestsRate
		{
			get
			{
				return AddressBookCounters.NspiRpcRequestsRate;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0001157C File Offset: 0x0000F77C
		public IExPerformanceCounter NspiBrowseRequests
		{
			get
			{
				return AddressBookCounters.NspiRpcBrowseRequests;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00011583 File Offset: 0x0000F783
		public IExPerformanceCounter NspiBrowseRequestsTotal
		{
			get
			{
				return AddressBookCounters.NspiRpcBrowseRequestsTotal;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0001158A File Offset: 0x0000F78A
		public IExPerformanceCounter NspiBrowseRequestsAverageLatency
		{
			get
			{
				return AddressBookCounters.NspiRpcBrowseRequestsAverageLatency;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00011591 File Offset: 0x0000F791
		public IExPerformanceCounter NspiBrowseRequestsRate
		{
			get
			{
				return AddressBookCounters.NspiRpcBrowseRequestsRate;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00011598 File Offset: 0x0000F798
		public IExPerformanceCounter RfrRequests
		{
			get
			{
				return AddressBookCounters.RfrRpcRequests;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0001159F File Offset: 0x0000F79F
		public IExPerformanceCounter RfrRequestsTotal
		{
			get
			{
				return AddressBookCounters.RfrRpcRequestsTotal;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000115A6 File Offset: 0x0000F7A6
		public IExPerformanceCounter RfrRequestsRate
		{
			get
			{
				return AddressBookCounters.RfrRpcRequestsRate;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000115AD File Offset: 0x0000F7AD
		public IExPerformanceCounter RfrRequestsAverageLatency
		{
			get
			{
				return AddressBookCounters.RfrRpcRequestsAverageLatency;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public IExPerformanceCounter ThumbnailPhotoAverageTime
		{
			get
			{
				return AddressBookCounters.ThumbnailPhotoAverageTime;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000115BB File Offset: 0x0000F7BB
		public IExPerformanceCounter ThumbnailPhotoAverageTimeBase
		{
			get
			{
				return AddressBookCounters.ThumbnailPhotoAverageTimeBase;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000115C2 File Offset: 0x0000F7C2
		public IExPerformanceCounter ThumbnailPhotoFromMailboxCount
		{
			get
			{
				return AddressBookCounters.ThumbnailPhotoFromMailboxCount;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000115C9 File Offset: 0x0000F7C9
		public IExPerformanceCounter ThumbnailPhotoFromDirectoryCount
		{
			get
			{
				return AddressBookCounters.ThumbnailPhotoFromDirectoryCount;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000115D0 File Offset: 0x0000F7D0
		public IExPerformanceCounter ThumbnailPhotoNotPresentCount
		{
			get
			{
				return AddressBookCounters.ThumbnailPhotoNotPresentCount;
			}
		}
	}
}
