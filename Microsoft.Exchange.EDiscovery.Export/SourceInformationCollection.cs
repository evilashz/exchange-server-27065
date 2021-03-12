using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000023 RID: 35
	internal class SourceInformationCollection
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005343 File Offset: 0x00003543
		public SourceInformationCollection(int count)
		{
			this.sources = new List<SourceInformation>(count);
			this.sourcesIndex = new Dictionary<string, int>(count);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005363 File Offset: 0x00003563
		public IEnumerable<SourceInformation> Values
		{
			get
			{
				return this.sources;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000536B File Offset: 0x0000356B
		public int Count
		{
			get
			{
				return this.sources.Count;
			}
		}

		// Token: 0x1700007F RID: 127
		public SourceInformation this[string sourceId]
		{
			get
			{
				return this.sources[this.sourcesIndex[sourceId]];
			}
			set
			{
				if (this.sourcesIndex.ContainsKey(sourceId))
				{
					this.sources[this.sourcesIndex[sourceId]] = value;
					return;
				}
				this.sourcesIndex[sourceId] = this.sources.Count;
				this.sources.Add(value);
			}
		}

		// Token: 0x17000080 RID: 128
		public SourceInformation this[int index]
		{
			get
			{
				return this.sources[index];
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000053F9 File Offset: 0x000035F9
		public int GetSourceIndex(string sourceId)
		{
			return this.sourcesIndex[sourceId];
		}

		// Token: 0x0400008D RID: 141
		private readonly List<SourceInformation> sources;

		// Token: 0x0400008E RID: 142
		private readonly Dictionary<string, int> sourcesIndex;
	}
}
