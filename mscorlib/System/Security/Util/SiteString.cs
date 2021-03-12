using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Util
{
	// Token: 0x0200034F RID: 847
	[Serializable]
	internal class SiteString
	{
		// Token: 0x06002B0C RID: 11020 RVA: 0x0009FC24 File Offset: 0x0009DE24
		protected internal SiteString()
		{
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0009FC2C File Offset: 0x0009DE2C
		public SiteString(string site)
		{
			this.m_separatedSite = SiteString.CreateSeparatedSite(site);
			this.m_site = site;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x0009FC47 File Offset: 0x0009DE47
		private SiteString(string site, ArrayList separatedSite)
		{
			this.m_separatedSite = separatedSite;
			this.m_site = site;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0009FC60 File Offset: 0x0009DE60
		private static ArrayList CreateSeparatedSite(string site)
		{
			if (site == null || site.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
			}
			ArrayList arrayList = new ArrayList();
			int num = -1;
			int num2 = site.IndexOf('[');
			if (num2 == 0)
			{
				num = site.IndexOf(']', num2 + 1);
			}
			if (num != -1)
			{
				string value = site.Substring(num2 + 1, num - num2 - 1);
				arrayList.Add(value);
				return arrayList;
			}
			string[] array = site.Split(SiteString.m_separators);
			for (int i = array.Length - 1; i > -1; i--)
			{
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
				}
				if (array[i].Equals(""))
				{
					if (i != array.Length - 1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
				}
				else if (array[i].Equals("*"))
				{
					if (i != 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
				else
				{
					if (!SiteString.AllLegalCharacters(array[i]))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
			}
			return arrayList;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0009FD88 File Offset: 0x0009DF88
		private static bool AllLegalCharacters(string str)
		{
			foreach (char c in str)
			{
				if (!SiteString.IsLegalDNSChar(c) && !SiteString.IsNetbiosSplChar(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0009FDC1 File Offset: 0x0009DFC1
		private static bool IsLegalDNSChar(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-';
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x0009FDEC File Offset: 0x0009DFEC
		private static bool IsNetbiosSplChar(char c)
		{
			if (c <= '@')
			{
				switch (c)
				{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '-':
				case '.':
					break;
				case '"':
				case '*':
				case '+':
				case ',':
					return false;
				default:
					if (c != '@')
					{
						return false;
					}
					break;
				}
			}
			else if (c != '^' && c != '_')
			{
				switch (c)
				{
				case '{':
				case '}':
				case '~':
					break;
				case '|':
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0009FE6E File Offset: 0x0009E06E
		public override string ToString()
		{
			return this.m_site;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0009FE76 File Offset: 0x0009E076
		public override bool Equals(object o)
		{
			return o != null && o is SiteString && this.Equals((SiteString)o, true);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0009FE94 File Offset: 0x0009E094
		public override int GetHashCode()
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			return textInfo.GetCaseInsensitiveHashCode(this.m_site);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0009FEB8 File Offset: 0x0009E0B8
		internal bool Equals(SiteString ss, bool ignoreCase)
		{
			if (this.m_site == null)
			{
				return ss.m_site == null;
			}
			return ss.m_site != null && this.IsSubsetOf(ss, ignoreCase) && ss.IsSubsetOf(this, ignoreCase);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0009FEEA File Offset: 0x0009E0EA
		public virtual SiteString Copy()
		{
			return new SiteString(this.m_site, this.m_separatedSite);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0009FEFD File Offset: 0x0009E0FD
		public virtual bool IsSubsetOf(SiteString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0009FF08 File Offset: 0x0009E108
		public virtual bool IsSubsetOf(SiteString operand, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (operand == null)
			{
				return false;
			}
			if (this.m_separatedSite.Count == operand.m_separatedSite.Count && this.m_separatedSite.Count == 0)
			{
				return true;
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count - 1)
			{
				return false;
			}
			if (this.m_separatedSite.Count > operand.m_separatedSite.Count && operand.m_separatedSite.Count > 0 && !operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*"))
			{
				return false;
			}
			if (string.Compare(this.m_site, operand.m_site, comparisonType) == 0)
			{
				return true;
			}
			for (int i = 0; i < operand.m_separatedSite.Count - 1; i++)
			{
				if (string.Compare((string)this.m_separatedSite[i], (string)operand.m_separatedSite[i], comparisonType) != 0)
				{
					return false;
				}
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count)
			{
				return operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
			}
			return this.m_separatedSite.Count != operand.m_separatedSite.Count || string.Compare((string)this.m_separatedSite[this.m_separatedSite.Count - 1], (string)operand.m_separatedSite[this.m_separatedSite.Count - 1], comparisonType) == 0 || operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A00C6 File Offset: 0x0009E2C6
		public virtual SiteString Intersect(SiteString operand)
		{
			if (operand == null)
			{
				return null;
			}
			if (this.IsSubsetOf(operand))
			{
				return this.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return operand.Copy();
			}
			return null;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A00EE File Offset: 0x0009E2EE
		public virtual SiteString Union(SiteString operand)
		{
			if (operand == null)
			{
				return this;
			}
			if (this.IsSubsetOf(operand))
			{
				return operand.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x0400110A RID: 4362
		protected string m_site;

		// Token: 0x0400110B RID: 4363
		protected ArrayList m_separatedSite;

		// Token: 0x0400110C RID: 4364
		protected static char[] m_separators = new char[]
		{
			'.'
		};
	}
}
