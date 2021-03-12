using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x02000392 RID: 914
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x06002E5D RID: 11869 RVA: 0x000B1CC0 File Offset: 0x000AFEC0
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000B1D0C File Offset: 0x000AFF0C
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x04001379 RID: 4985
		internal int era;

		// Token: 0x0400137A RID: 4986
		internal long ticks;

		// Token: 0x0400137B RID: 4987
		internal int yearOffset;

		// Token: 0x0400137C RID: 4988
		internal int minEraYear;

		// Token: 0x0400137D RID: 4989
		internal int maxEraYear;

		// Token: 0x0400137E RID: 4990
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x0400137F RID: 4991
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x04001380 RID: 4992
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}
