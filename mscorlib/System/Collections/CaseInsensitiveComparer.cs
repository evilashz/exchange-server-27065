using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200045D RID: 1117
	[ComVisible(true)]
	[Serializable]
	public class CaseInsensitiveComparer : IComparer
	{
		// Token: 0x0600364D RID: 13901 RVA: 0x000D0B0E File Offset: 0x000CED0E
		public CaseInsensitiveComparer()
		{
			this.m_compareInfo = CultureInfo.CurrentCulture.CompareInfo;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000D0B26 File Offset: 0x000CED26
		public CaseInsensitiveComparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_compareInfo = culture.CompareInfo;
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000D0B48 File Offset: 0x000CED48
		public static CaseInsensitiveComparer Default
		{
			get
			{
				return new CaseInsensitiveComparer(CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000D0B54 File Offset: 0x000CED54
		public static CaseInsensitiveComparer DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer == null)
				{
					CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer;
			}
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000D0B78 File Offset: 0x000CED78
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x040017F9 RID: 6137
		private CompareInfo m_compareInfo;

		// Token: 0x040017FA RID: 6138
		private static volatile CaseInsensitiveComparer m_InvariantCaseInsensitiveComparer;
	}
}
