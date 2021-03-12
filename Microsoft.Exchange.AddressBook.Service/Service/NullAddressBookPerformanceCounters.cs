using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200003E RID: 62
	internal class NullAddressBookPerformanceCounters : IAddressBookPerformanceCounters
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00011750 File Offset: 0x0000F950
		public IExPerformanceCounter PID
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00011757 File Offset: 0x0000F957
		public IExPerformanceCounter NspiConnectionsCurrent
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0001175E File Offset: 0x0000F95E
		public IExPerformanceCounter NspiConnectionsTotal
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00011765 File Offset: 0x0000F965
		public IExPerformanceCounter NspiConnectionsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001176C File Offset: 0x0000F96C
		public IExPerformanceCounter NspiRequests
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00011773 File Offset: 0x0000F973
		public IExPerformanceCounter NspiRequestsTotal
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0001177A File Offset: 0x0000F97A
		public IExPerformanceCounter NspiRequestsAverageLatency
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00011781 File Offset: 0x0000F981
		public IExPerformanceCounter NspiRequestsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00011788 File Offset: 0x0000F988
		public IExPerformanceCounter NspiBrowseRequests
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0001178F File Offset: 0x0000F98F
		public IExPerformanceCounter NspiBrowseRequestsTotal
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00011796 File Offset: 0x0000F996
		public IExPerformanceCounter NspiBrowseRequestsAverageLatency
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001179D File Offset: 0x0000F99D
		public IExPerformanceCounter NspiBrowseRequestsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public IExPerformanceCounter RfrRequests
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000117AB File Offset: 0x0000F9AB
		public IExPerformanceCounter RfrRequestsTotal
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000117B2 File Offset: 0x0000F9B2
		public IExPerformanceCounter RfrRequestsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000117B9 File Offset: 0x0000F9B9
		public IExPerformanceCounter RfrRequestsAverageLatency
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public IExPerformanceCounter ThumbnailPhotoAverageTime
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000117C7 File Offset: 0x0000F9C7
		public IExPerformanceCounter ThumbnailPhotoAverageTimeBase
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000117CE File Offset: 0x0000F9CE
		public IExPerformanceCounter ThumbnailPhotoFromMailboxCount
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000117D5 File Offset: 0x0000F9D5
		public IExPerformanceCounter ThumbnailPhotoFromDirectoryCount
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000117DC File Offset: 0x0000F9DC
		public IExPerformanceCounter ThumbnailPhotoNotPresentCount
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}
	}
}
