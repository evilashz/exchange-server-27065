using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000225 RID: 549
	internal class SearchProgressEvent : EventArgs
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x00043E7E File Offset: 0x0004207E
		internal SearchProgressEvent(LocalizedString activity, LocalizedString statusDescription, int percentCompleted) : this(activity, statusDescription, percentCompleted, 0L, ByteQuantifiedSize.Zero)
		{
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00043E90 File Offset: 0x00042090
		internal SearchProgressEvent(LocalizedString activity, LocalizedString statusDescription, int percentCompleted, long resultItems, ByteQuantifiedSize resultSize)
		{
			this.activity = activity;
			this.statusDescription = statusDescription;
			this.percentCompleted = percentCompleted;
			this.resultItems = resultItems;
			this.resultSize = resultSize;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00043EBD File Offset: 0x000420BD
		internal LocalizedString Activity
		{
			get
			{
				return this.activity;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00043EC5 File Offset: 0x000420C5
		internal LocalizedString StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00043ECD File Offset: 0x000420CD
		internal int PercentCompleted
		{
			get
			{
				return this.percentCompleted;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00043ED5 File Offset: 0x000420D5
		internal long ResultItems
		{
			get
			{
				return this.resultItems;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00043EDD File Offset: 0x000420DD
		internal ByteQuantifiedSize ResultSize
		{
			get
			{
				return this.resultSize;
			}
		}

		// Token: 0x04000A64 RID: 2660
		private LocalizedString activity;

		// Token: 0x04000A65 RID: 2661
		private LocalizedString statusDescription;

		// Token: 0x04000A66 RID: 2662
		private int percentCompleted;

		// Token: 0x04000A67 RID: 2663
		private long resultItems;

		// Token: 0x04000A68 RID: 2664
		private ByteQuantifiedSize resultSize;
	}
}
