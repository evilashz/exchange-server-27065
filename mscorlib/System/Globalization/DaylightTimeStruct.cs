using System;

namespace System.Globalization
{
	// Token: 0x02000388 RID: 904
	internal struct DaylightTimeStruct
	{
		// Token: 0x06002E23 RID: 11811 RVA: 0x000B0EB9 File Offset: 0x000AF0B9
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000B0ED0 File Offset: 0x000AF0D0
		public DateTime Start { get; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000B0ED8 File Offset: 0x000AF0D8
		public DateTime End { get; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
		public TimeSpan Delta { get; }
	}
}
