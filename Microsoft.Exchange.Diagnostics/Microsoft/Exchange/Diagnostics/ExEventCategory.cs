using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200002B RID: 43
	[CLSCompliant(true)]
	[Serializable]
	public class ExEventCategory
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000465F File Offset: 0x0000285F
		public ExEventCategory(string name, int number, ExEventLog.EventLevel level)
		{
			this.m_name = name;
			this.m_number = number;
			this.m_level = level;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000468A File Offset: 0x0000288A
		public ExEventCategory()
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000046A0 File Offset: 0x000028A0
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000046A8 File Offset: 0x000028A8
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000046B1 File Offset: 0x000028B1
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000046B9 File Offset: 0x000028B9
		public int Number
		{
			get
			{
				return this.m_number;
			}
			set
			{
				this.m_number = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000046C2 File Offset: 0x000028C2
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000046CA File Offset: 0x000028CA
		public ExEventLog.EventLevel EventLevel
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x040000BD RID: 189
		private string m_name;

		// Token: 0x040000BE RID: 190
		private int m_number = -1;

		// Token: 0x040000BF RID: 191
		private ExEventLog.EventLevel m_level = ExEventLog.EventLevel.Expert;
	}
}
