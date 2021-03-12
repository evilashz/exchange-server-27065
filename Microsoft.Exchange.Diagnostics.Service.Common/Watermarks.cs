using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200001C RID: 28
	public sealed class Watermarks
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00005AD5 File Offset: 0x00003CD5
		public Watermarks(string directory)
		{
			this.directory = directory;
			this.mappings = Watermark.LoadWatermarksFromDirectory(directory);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public Watermark Get(string jobName)
		{
			Watermark latestWatermark;
			if (!this.mappings.TryGetValue(jobName, out latestWatermark))
			{
				Logger.LogInformationMessage("No watermark found for '{0}' job, defaulting.", new object[]
				{
					jobName
				});
				latestWatermark = Watermark.LatestWatermark;
			}
			return latestWatermark;
		}

		// Token: 0x040002EF RID: 751
		private readonly string directory;

		// Token: 0x040002F0 RID: 752
		private IDictionary<string, Watermark> mappings;
	}
}
