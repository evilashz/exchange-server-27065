using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F8 RID: 504
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PhotoGarbageCollectionStatsTracker : IDisposable
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0004D864 File Offset: 0x0004BA64
		public PhotoGarbageCollectionStatsTracker(string marker, IPerformanceDataLogger logger)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("marker", marker);
			this.marker = marker;
			this.logger = (logger ?? NullPerformanceDataLogger.Instance);
			this.fileCount = 0U;
			this.deletedFileCount = 0U;
			this.totalFileSizeInMB = 0.0;
			this.totalDeletedFileSizeInMB = 0.0;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004D8BF File Offset: 0x0004BABF
		public void Account(FileInfo file)
		{
			if (file == null)
			{
				return;
			}
			this.fileCount += 1U;
			this.totalFileSizeInMB += (double)file.Length / 1048576.0;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004D8F1 File Offset: 0x0004BAF1
		public void AccountDeleted(FileInfo file)
		{
			if (file == null)
			{
				return;
			}
			this.deletedFileCount += 1U;
			this.totalDeletedFileSizeInMB += (double)file.Length / 1048576.0;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004D923 File Offset: 0x0004BB23
		public void Dispose()
		{
			this.Stop();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004D92C File Offset: 0x0004BB2C
		public void Stop()
		{
			this.logger.Log(this.marker, "FileCount", this.fileCount);
			this.logger.Log(this.marker, "TotalFileSizeInMB", (uint)Math.Round(this.totalFileSizeInMB));
			this.logger.Log(this.marker, "DeletedFileCount", this.deletedFileCount);
			this.logger.Log(this.marker, "TotalDeletedFileSizeInMB", (uint)Math.Round(this.totalDeletedFileSizeInMB));
		}

		// Token: 0x040009D5 RID: 2517
		private const string FileCount = "FileCount";

		// Token: 0x040009D6 RID: 2518
		private const string DeletedFileCount = "DeletedFileCount";

		// Token: 0x040009D7 RID: 2519
		private const string TotalFileSizeInMB = "TotalFileSizeInMB";

		// Token: 0x040009D8 RID: 2520
		private const string TotalDeletedFileSizeInMB = "TotalDeletedFileSizeInMB";

		// Token: 0x040009D9 RID: 2521
		private const double BytesInMegabyte = 1048576.0;

		// Token: 0x040009DA RID: 2522
		private readonly string marker;

		// Token: 0x040009DB RID: 2523
		private readonly IPerformanceDataLogger logger;

		// Token: 0x040009DC RID: 2524
		private uint fileCount;

		// Token: 0x040009DD RID: 2525
		private uint deletedFileCount;

		// Token: 0x040009DE RID: 2526
		private double totalFileSizeInMB;

		// Token: 0x040009DF RID: 2527
		private double totalDeletedFileSizeInMB;
	}
}
