using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200045E RID: 1118
	[Obsolete("Please use StringComparer instead.")]
	[ComVisible(true)]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		// Token: 0x06003652 RID: 13906 RVA: 0x000D0BB4 File Offset: 0x000CEDB4
		public CaseInsensitiveHashCodeProvider()
		{
			this.m_text = CultureInfo.CurrentCulture.TextInfo;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000D0BCC File Offset: 0x000CEDCC
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.m_text = culture.TextInfo;
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x000D0BEE File Offset: 0x000CEDEE
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				return new CaseInsensitiveHashCodeProvider(CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000D0BFA File Offset: 0x000CEDFA
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider == null)
				{
					CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider;
			}
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000D0C20 File Offset: 0x000CEE20
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			return this.m_text.GetCaseInsensitiveHashCode(text);
		}

		// Token: 0x040017FB RID: 6139
		private TextInfo m_text;

		// Token: 0x040017FC RID: 6140
		private static volatile CaseInsensitiveHashCodeProvider m_InvariantCaseInsensitiveHashCodeProvider;
	}
}
