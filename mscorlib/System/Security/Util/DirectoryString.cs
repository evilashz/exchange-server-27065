using System;
using System.Collections;

namespace System.Security.Util
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	internal class DirectoryString : SiteString
	{
		// Token: 0x06002B7C RID: 11132 RVA: 0x000A265F File Offset: 0x000A085F
		public DirectoryString()
		{
			this.m_site = "";
			this.m_separatedSite = new ArrayList();
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000A267D File Offset: 0x000A087D
		public DirectoryString(string directory, bool checkForIllegalChars)
		{
			this.m_site = directory;
			this.m_checkForIllegalChars = checkForIllegalChars;
			this.m_separatedSite = this.CreateSeparatedString(directory);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000A26A0 File Offset: 0x000A08A0
		private ArrayList CreateSeparatedString(string directory)
		{
			if (directory == null || directory.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
			}
			ArrayList arrayList = new ArrayList();
			string[] array = directory.Split(DirectoryString.m_separators);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					if (array[i].Equals("*"))
					{
						if (i != array.Length - 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
					else
					{
						if (this.m_checkForIllegalChars && array[i].IndexOfAny(DirectoryString.m_illegalDirectoryCharacters) != -1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000A2765 File Offset: 0x000A0965
		public virtual bool IsSubsetOf(DirectoryString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000A2770 File Offset: 0x000A0970
		public virtual bool IsSubsetOf(DirectoryString operand, bool ignoreCase)
		{
			if (operand == null)
			{
				return false;
			}
			if (operand.m_separatedSite.Count == 0)
			{
				return this.m_separatedSite.Count == 0 || (this.m_separatedSite.Count > 0 && string.Compare((string)this.m_separatedSite[0], "*", StringComparison.Ordinal) == 0);
			}
			if (this.m_separatedSite.Count == 0)
			{
				return string.Compare((string)operand.m_separatedSite[0], "*", StringComparison.Ordinal) == 0;
			}
			return base.IsSubsetOf(operand, ignoreCase);
		}

		// Token: 0x0400112C RID: 4396
		private bool m_checkForIllegalChars;

		// Token: 0x0400112D RID: 4397
		private new static char[] m_separators = new char[]
		{
			'/'
		};

		// Token: 0x0400112E RID: 4398
		protected static char[] m_illegalDirectoryCharacters = new char[]
		{
			'\\',
			':',
			'*',
			'?',
			'"',
			'<',
			'>',
			'|'
		};
	}
}
