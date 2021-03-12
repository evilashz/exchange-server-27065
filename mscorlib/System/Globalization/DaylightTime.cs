using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000387 RID: 903
	[ComVisible(true)]
	[Serializable]
	public class DaylightTime
	{
		// Token: 0x06002E1E RID: 11806 RVA: 0x000B0E7C File Offset: 0x000AF07C
		private DaylightTime()
		{
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000B0E84 File Offset: 0x000AF084
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this.m_start = start;
			this.m_end = end;
			this.m_delta = delta;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000B0EA1 File Offset: 0x000AF0A1
		public DateTime Start
		{
			get
			{
				return this.m_start;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000B0EA9 File Offset: 0x000AF0A9
		public DateTime End
		{
			get
			{
				return this.m_end;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000B0EB1 File Offset: 0x000AF0B1
		public TimeSpan Delta
		{
			get
			{
				return this.m_delta;
			}
		}

		// Token: 0x0400134A RID: 4938
		internal DateTime m_start;

		// Token: 0x0400134B RID: 4939
		internal DateTime m_end;

		// Token: 0x0400134C RID: 4940
		internal TimeSpan m_delta;
	}
}
