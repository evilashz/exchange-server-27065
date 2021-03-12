using System;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200001D RID: 29
	internal class Segmentator
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003E4B File Offset: 0x0000204B
		public Segmentator(int numOfThreads)
		{
			if (numOfThreads < 1)
			{
				numOfThreads = 1;
			}
			this.numOfSegments = Math.Min(numOfThreads, 5);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003E68 File Offset: 0x00002068
		public DownloadParameter[] SegmentTheFile(long fileSize)
		{
			Logger.LoggerMessage("Segmenting the file with size: " + fileSize);
			long num = fileSize / (long)this.numOfSegments;
			DownloadParameter[] array = new DownloadParameter[this.numOfSegments];
			for (int i = 0; i < this.numOfSegments; i++)
			{
				if (i == 0)
				{
					array[i].StartPosition = 0;
				}
				else
				{
					array[i].StartPosition = (int)((long)i * num) + 1;
				}
				if (i + 1 == this.numOfSegments)
				{
					array[i].EndPosition = (int)fileSize - 1;
				}
				else
				{
					array[i].EndPosition = (int)((long)(i + 1) * num);
				}
				array[i].PathToFile = Path.GetTempFileName();
			}
			return array;
		}

		// Token: 0x0400004D RID: 77
		private int numOfSegments;
	}
}
