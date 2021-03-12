using System;

namespace System.Collections.Concurrent
{
	// Token: 0x02000483 RID: 1155
	internal struct VolatileBool
	{
		// Token: 0x0600386E RID: 14446 RVA: 0x000D7DDF File Offset: 0x000D5FDF
		public VolatileBool(bool value)
		{
			this.m_value = value;
		}

		// Token: 0x04001870 RID: 6256
		public volatile bool m_value;
	}
}
