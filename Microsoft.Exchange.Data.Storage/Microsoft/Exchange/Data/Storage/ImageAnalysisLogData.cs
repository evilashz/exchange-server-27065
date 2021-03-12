using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200035C RID: 860
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ImageAnalysisLogData
	{
		// Token: 0x06002662 RID: 9826 RVA: 0x0009A1B5 File Offset: 0x000983B5
		public ImageAnalysisLogData(long timeMs)
		{
			this.operationTimeMs = timeMs;
			this.thumbnailWidth = 0;
			this.thumbnailHeight = 0;
			this.thumbnailSize = 0L;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0009A1D4 File Offset: 0x000983D4
		public ImageAnalysisLogData(long timeMs, int thumbnailWidth, int thumbnailHeight, long thumbnailSize)
		{
			this.operationTimeMs = timeMs;
			this.thumbnailWidth = thumbnailWidth;
			this.thumbnailHeight = thumbnailHeight;
			this.thumbnailSize = thumbnailSize;
		}

		// Token: 0x040016F1 RID: 5873
		public long operationTimeMs;

		// Token: 0x040016F2 RID: 5874
		public int thumbnailWidth;

		// Token: 0x040016F3 RID: 5875
		public int thumbnailHeight;

		// Token: 0x040016F4 RID: 5876
		public long thumbnailSize;
	}
}
