using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000720 RID: 1824
	[Serializable]
	internal class FixupHolder
	{
		// Token: 0x06005192 RID: 20882 RVA: 0x0011E0D2 File Offset: 0x0011C2D2
		internal FixupHolder(long id, object fixupInfo, int fixupType)
		{
			this.m_id = id;
			this.m_fixupInfo = fixupInfo;
			this.m_fixupType = fixupType;
		}

		// Token: 0x040023E4 RID: 9188
		internal const int ArrayFixup = 1;

		// Token: 0x040023E5 RID: 9189
		internal const int MemberFixup = 2;

		// Token: 0x040023E6 RID: 9190
		internal const int DelayedFixup = 4;

		// Token: 0x040023E7 RID: 9191
		internal long m_id;

		// Token: 0x040023E8 RID: 9192
		internal object m_fixupInfo;

		// Token: 0x040023E9 RID: 9193
		internal int m_fixupType;
	}
}
