using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009D7 RID: 2519
	internal class SearchPerfMarkerContainer
	{
		// Token: 0x0600473E RID: 18238 RVA: 0x000FECF2 File Offset: 0x000FCEF2
		public SearchPerfMarkerContainer() : this(Stopwatch.StartNew())
		{
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000FECFF File Offset: 0x000FCEFF
		private SearchPerfMarkerContainer(Stopwatch stopWatch)
		{
			this.stopWatch = stopWatch;
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x000FED1A File Offset: 0x000FCF1A
		public void SetPerfMarker(InstantSearchPerfKey key)
		{
			this.perfData.Add(new InstantSearchPerfMarkerType(key, (double)this.stopWatch.ElapsedTicks * 1000.0 / (double)Stopwatch.Frequency));
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x000FED4A File Offset: 0x000FCF4A
		internal InstantSearchPerfMarkerType[] GetMarkerSnapshot()
		{
			return this.perfData.ToArray();
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x000FED57 File Offset: 0x000FCF57
		internal List<InstantSearchPerfMarkerType> MarkerCollection
		{
			get
			{
				return this.perfData;
			}
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x000FED60 File Offset: 0x000FCF60
		internal SearchPerfMarkerContainer GetDeepCopy()
		{
			SearchPerfMarkerContainer searchPerfMarkerContainer = new SearchPerfMarkerContainer(this.stopWatch);
			searchPerfMarkerContainer.perfData.AddRange(this.MarkerCollection);
			return searchPerfMarkerContainer;
		}

		// Token: 0x040028E3 RID: 10467
		private List<InstantSearchPerfMarkerType> perfData = new List<InstantSearchPerfMarkerType>(7);

		// Token: 0x040028E4 RID: 10468
		private readonly Stopwatch stopWatch;
	}
}
