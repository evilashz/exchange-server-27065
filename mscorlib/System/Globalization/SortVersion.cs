using System;

namespace System.Globalization
{
	// Token: 0x020003B3 RID: 947
	[Serializable]
	public sealed class SortVersion : IEquatable<SortVersion>
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000BFE22 File Offset: 0x000BE022
		public int FullVersion
		{
			get
			{
				return this.m_NlsVersion;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000BFE2A File Offset: 0x000BE02A
		public Guid SortId
		{
			get
			{
				return this.m_SortId;
			}
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000BFE32 File Offset: 0x000BE032
		public SortVersion(int fullVersion, Guid sortId)
		{
			this.m_SortId = sortId;
			this.m_NlsVersion = fullVersion;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000BFE48 File Offset: 0x000BE048
		internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
		{
			this.m_NlsVersion = nlsVersion;
			if (customVersion == Guid.Empty)
			{
				byte[] bytes = BitConverter.GetBytes(effectiveId);
				byte h = (byte)((uint)effectiveId >> 24);
				byte i = (byte)((effectiveId & 16711680) >> 16);
				byte j = (byte)((effectiveId & 65280) >> 8);
				byte k = (byte)(effectiveId & 255);
				customVersion = new Guid(0, 0, 0, 0, 0, 0, 0, h, i, j, k);
			}
			this.m_SortId = customVersion;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000BFEB8 File Offset: 0x000BE0B8
		public override bool Equals(object obj)
		{
			SortVersion sortVersion = obj as SortVersion;
			return sortVersion != null && this.Equals(sortVersion);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000BFEDE File Offset: 0x000BE0DE
		public bool Equals(SortVersion other)
		{
			return !(other == null) && this.m_NlsVersion == other.m_NlsVersion && this.m_SortId == other.m_SortId;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000BFF0C File Offset: 0x000BE10C
		public override int GetHashCode()
		{
			return this.m_NlsVersion * 7 | this.m_SortId.GetHashCode();
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000BFF28 File Offset: 0x000BE128
		public static bool operator ==(SortVersion left, SortVersion right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null || right.Equals(left);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000BFF41 File Offset: 0x000BE141
		public static bool operator !=(SortVersion left, SortVersion right)
		{
			return !(left == right);
		}

		// Token: 0x040015DB RID: 5595
		private int m_NlsVersion;

		// Token: 0x040015DC RID: 5596
		private Guid m_SortId;
	}
}
