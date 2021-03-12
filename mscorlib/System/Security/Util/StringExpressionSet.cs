using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000350 RID: 848
	[Serializable]
	internal class StringExpressionSet
	{
		// Token: 0x06002B1D RID: 11037 RVA: 0x000A0128 File Offset: 0x0009E328
		public StringExpressionSet() : this(true, null, false)
		{
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A0133 File Offset: 0x0009E333
		public StringExpressionSet(string str) : this(true, str, false)
		{
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000A013E File Offset: 0x0009E33E
		public StringExpressionSet(bool ignoreCase, bool throwOnRelative) : this(ignoreCase, null, throwOnRelative)
		{
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000A0149 File Offset: 0x0009E349
		[SecuritySafeCritical]
		public StringExpressionSet(bool ignoreCase, string str, bool throwOnRelative)
		{
			this.m_list = null;
			this.m_ignoreCase = ignoreCase;
			this.m_throwOnRelative = throwOnRelative;
			if (str == null)
			{
				this.m_expressions = null;
				return;
			}
			this.AddExpressions(str);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000A0178 File Offset: 0x0009E378
		protected virtual StringExpressionSet CreateNewEmpty()
		{
			return new StringExpressionSet();
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000A0180 File Offset: 0x0009E380
		[SecuritySafeCritical]
		public virtual StringExpressionSet Copy()
		{
			StringExpressionSet stringExpressionSet = this.CreateNewEmpty();
			if (this.m_list != null)
			{
				stringExpressionSet.m_list = new ArrayList(this.m_list);
			}
			stringExpressionSet.m_expressions = this.m_expressions;
			stringExpressionSet.m_ignoreCase = this.m_ignoreCase;
			stringExpressionSet.m_throwOnRelative = this.m_throwOnRelative;
			return stringExpressionSet;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000A01D2 File Offset: 0x0009E3D2
		public void SetThrowOnRelative(bool throwOnRelative)
		{
			this.m_throwOnRelative = throwOnRelative;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000A01DB File Offset: 0x0009E3DB
		private static string StaticProcessWholeString(string str)
		{
			return str.Replace(StringExpressionSet.m_alternateDirectorySeparator, StringExpressionSet.m_directorySeparator);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000A01ED File Offset: 0x0009E3ED
		private static string StaticProcessSingleString(string str)
		{
			return str.Trim(StringExpressionSet.m_trimChars);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000A01FA File Offset: 0x0009E3FA
		protected virtual string ProcessWholeString(string str)
		{
			return StringExpressionSet.StaticProcessWholeString(str);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000A0202 File Offset: 0x0009E402
		protected virtual string ProcessSingleString(string str)
		{
			return StringExpressionSet.StaticProcessSingleString(str);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000A020C File Offset: 0x0009E40C
		[SecurityCritical]
		public void AddExpressions(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return;
			}
			str = this.ProcessWholeString(str);
			if (this.m_expressions == null)
			{
				this.m_expressions = str;
			}
			else
			{
				this.m_expressions = this.m_expressions + StringExpressionSet.m_separators[0].ToString() + str;
			}
			this.m_expressionsArray = null;
			string[] array = this.Split(str);
			if (this.m_list == null)
			{
				this.m_list = new ArrayList();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					string text = this.ProcessSingleString(array[i]);
					int num = text.IndexOf('\0');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					if (text != null && !text.Equals(""))
					{
						if (this.m_throwOnRelative)
						{
							if (Path.IsRelative(text))
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
							}
							text = StringExpressionSet.CanonicalizePath(text);
						}
						this.m_list.Add(text);
					}
				}
			}
			this.Reduce();
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000A031C File Offset: 0x0009E51C
		[SecurityCritical]
		public void AddExpressions(string[] str, bool checkForDuplicates, bool needFullPath)
		{
			this.AddExpressions(StringExpressionSet.CreateListFromExpressions(str, needFullPath), checkForDuplicates);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000A032C File Offset: 0x0009E52C
		[SecurityCritical]
		public void AddExpressions(ArrayList exprArrayList, bool checkForDuplicates)
		{
			this.m_expressionsArray = null;
			this.m_expressions = null;
			if (this.m_list != null)
			{
				this.m_list.AddRange(exprArrayList);
			}
			else
			{
				this.m_list = new ArrayList(exprArrayList);
			}
			if (checkForDuplicates)
			{
				this.Reduce();
			}
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000A0368 File Offset: 0x0009E568
		[SecurityCritical]
		internal static ArrayList CreateListFromExpressions(string[] str, bool needFullPath)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == null)
				{
					throw new ArgumentNullException("str");
				}
				string text = StringExpressionSet.StaticProcessWholeString(str[i]);
				if (text != null && text.Length != 0)
				{
					string text2 = StringExpressionSet.StaticProcessSingleString(text);
					int num = text2.IndexOf('\0');
					if (num != -1)
					{
						text2 = text2.Substring(0, num);
					}
					if (text2 != null && text2.Length != 0)
					{
						if (PathInternal.IsPartiallyQualified(text2))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
						}
						text2 = StringExpressionSet.CanonicalizePath(text2, needFullPath);
						arrayList.Add(text2);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000A040E File Offset: 0x0009E60E
		[SecurityCritical]
		protected void CheckList()
		{
			if (this.m_list == null && this.m_expressions != null)
			{
				this.CreateList();
			}
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000A0428 File Offset: 0x0009E628
		protected string[] Split(string expressions)
		{
			if (this.m_throwOnRelative)
			{
				List<string> list = new List<string>();
				string[] array = expressions.Split(new char[]
				{
					'"'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (i % 2 == 0)
					{
						string[] array2 = array[i].Split(new char[]
						{
							';'
						});
						for (int j = 0; j < array2.Length; j++)
						{
							if (array2[j] != null && !array2[j].Equals(""))
							{
								list.Add(array2[j]);
							}
						}
					}
					else
					{
						list.Add(array[i]);
					}
				}
				string[] array3 = new string[list.Count];
				IEnumerator enumerator = list.GetEnumerator();
				int num = 0;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					array3[num++] = (string)obj;
				}
				return array3;
			}
			return expressions.Split(StringExpressionSet.m_separators);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000A0510 File Offset: 0x0009E710
		[SecurityCritical]
		protected void CreateList()
		{
			string[] array = this.Split(this.m_expressions);
			this.m_list = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					string text = this.ProcessSingleString(array[i]);
					int num = text.IndexOf('\0');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					if (text != null && !text.Equals(""))
					{
						if (this.m_throwOnRelative)
						{
							if (Path.IsRelative(text))
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
							}
							text = StringExpressionSet.CanonicalizePath(text);
						}
						this.m_list.Add(text);
					}
				}
			}
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000A05BD File Offset: 0x0009E7BD
		[SecuritySafeCritical]
		public bool IsEmpty()
		{
			if (this.m_list == null)
			{
				return this.m_expressions == null;
			}
			return this.m_list.Count == 0;
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000A05E0 File Offset: 0x0009E7E0
		[SecurityCritical]
		public bool IsSubsetOf(StringExpressionSet ses)
		{
			if (this.IsEmpty())
			{
				return true;
			}
			if (ses == null || ses.IsEmpty())
			{
				return false;
			}
			this.CheckList();
			ses.CheckList();
			for (int i = 0; i < this.m_list.Count; i++)
			{
				if (!this.StringSubsetStringExpression((string)this.m_list[i], ses, this.m_ignoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000A064C File Offset: 0x0009E84C
		[SecurityCritical]
		public bool IsSubsetOfPathDiscovery(StringExpressionSet ses)
		{
			if (this.IsEmpty())
			{
				return true;
			}
			if (ses == null || ses.IsEmpty())
			{
				return false;
			}
			this.CheckList();
			ses.CheckList();
			for (int i = 0; i < this.m_list.Count; i++)
			{
				if (!StringExpressionSet.StringSubsetStringExpressionPathDiscovery((string)this.m_list[i], ses, this.m_ignoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000A06B4 File Offset: 0x0009E8B4
		[SecurityCritical]
		public StringExpressionSet Union(StringExpressionSet ses)
		{
			if (ses == null || ses.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return ses.Copy();
			}
			this.CheckList();
			ses.CheckList();
			StringExpressionSet stringExpressionSet = (ses.m_list.Count > this.m_list.Count) ? ses : this;
			StringExpressionSet stringExpressionSet2 = (ses.m_list.Count <= this.m_list.Count) ? ses : this;
			StringExpressionSet stringExpressionSet3 = stringExpressionSet.Copy();
			stringExpressionSet3.Reduce();
			for (int i = 0; i < stringExpressionSet2.m_list.Count; i++)
			{
				stringExpressionSet3.AddSingleExpressionNoDuplicates((string)stringExpressionSet2.m_list[i]);
			}
			stringExpressionSet3.GenerateString();
			return stringExpressionSet3;
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000A076C File Offset: 0x0009E96C
		[SecurityCritical]
		public StringExpressionSet Intersect(StringExpressionSet ses)
		{
			if (this.IsEmpty() || ses == null || ses.IsEmpty())
			{
				return this.CreateNewEmpty();
			}
			this.CheckList();
			ses.CheckList();
			StringExpressionSet stringExpressionSet = this.CreateNewEmpty();
			for (int i = 0; i < this.m_list.Count; i++)
			{
				for (int j = 0; j < ses.m_list.Count; j++)
				{
					if (this.StringSubsetString((string)this.m_list[i], (string)ses.m_list[j], this.m_ignoreCase))
					{
						if (stringExpressionSet.m_list == null)
						{
							stringExpressionSet.m_list = new ArrayList();
						}
						stringExpressionSet.AddSingleExpressionNoDuplicates((string)this.m_list[i]);
					}
					else if (this.StringSubsetString((string)ses.m_list[j], (string)this.m_list[i], this.m_ignoreCase))
					{
						if (stringExpressionSet.m_list == null)
						{
							stringExpressionSet.m_list = new ArrayList();
						}
						stringExpressionSet.AddSingleExpressionNoDuplicates((string)ses.m_list[j]);
					}
				}
			}
			stringExpressionSet.GenerateString();
			return stringExpressionSet;
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000A089C File Offset: 0x0009EA9C
		[SecuritySafeCritical]
		protected void GenerateString()
		{
			if (this.m_list != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				IEnumerator enumerator = this.m_list.GetEnumerator();
				bool flag = true;
				while (enumerator.MoveNext())
				{
					if (!flag)
					{
						stringBuilder.Append(StringExpressionSet.m_separators[0]);
					}
					else
					{
						flag = false;
					}
					string text = (string)enumerator.Current;
					if (text != null)
					{
						int num = text.IndexOf(StringExpressionSet.m_separators[0]);
						if (num != -1)
						{
							stringBuilder.Append('"');
						}
						stringBuilder.Append(text);
						if (num != -1)
						{
							stringBuilder.Append('"');
						}
					}
				}
				this.m_expressions = stringBuilder.ToString();
				return;
			}
			this.m_expressions = null;
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000A093D File Offset: 0x0009EB3D
		[SecurityCritical]
		public string UnsafeToString()
		{
			this.CheckList();
			this.Reduce();
			this.GenerateString();
			return this.m_expressions;
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000A0957 File Offset: 0x0009EB57
		[SecurityCritical]
		public string[] UnsafeToStringArray()
		{
			if (this.m_expressionsArray == null && this.m_list != null)
			{
				this.m_expressionsArray = (string[])this.m_list.ToArray(typeof(string));
			}
			return this.m_expressionsArray;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000A0990 File Offset: 0x0009EB90
		[SecurityCritical]
		private bool StringSubsetStringExpression(string left, StringExpressionSet right, bool ignoreCase)
		{
			for (int i = 0; i < right.m_list.Count; i++)
			{
				if (this.StringSubsetString(left, (string)right.m_list[i], ignoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000A09D4 File Offset: 0x0009EBD4
		[SecurityCritical]
		private static bool StringSubsetStringExpressionPathDiscovery(string left, StringExpressionSet right, bool ignoreCase)
		{
			for (int i = 0; i < right.m_list.Count; i++)
			{
				if (StringExpressionSet.StringSubsetStringPathDiscovery(left, (string)right.m_list[i], ignoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000A0A14 File Offset: 0x0009EC14
		protected virtual bool StringSubsetString(string left, string right, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (right == null || left == null || right.Length == 0 || left.Length == 0 || right.Length > left.Length)
			{
				return false;
			}
			if (right.Length == left.Length)
			{
				return string.Compare(right, left, comparisonType) == 0;
			}
			if (left.Length - right.Length == 1 && left[left.Length - 1] == StringExpressionSet.m_directorySeparator)
			{
				return string.Compare(left, 0, right, 0, right.Length, comparisonType) == 0;
			}
			if (right[right.Length - 1] == StringExpressionSet.m_directorySeparator)
			{
				return string.Compare(right, 0, left, 0, right.Length, comparisonType) == 0;
			}
			return left[right.Length] == StringExpressionSet.m_directorySeparator && string.Compare(right, 0, left, 0, right.Length, comparisonType) == 0;
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000A0AF4 File Offset: 0x0009ECF4
		protected static bool StringSubsetStringPathDiscovery(string left, string right, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (right == null || left == null || right.Length == 0 || left.Length == 0)
			{
				return false;
			}
			if (right.Length == left.Length)
			{
				return string.Compare(right, left, comparisonType) == 0;
			}
			string text;
			string text2;
			if (right.Length < left.Length)
			{
				text = right;
				text2 = left;
			}
			else
			{
				text = left;
				text2 = right;
			}
			return string.Compare(text, 0, text2, 0, text.Length, comparisonType) == 0 && ((text.Length == 3 && text.EndsWith(":\\", StringComparison.Ordinal) && ((text[0] >= 'A' && text[0] <= 'Z') || (text[0] >= 'a' && text[0] <= 'z'))) || text2[text.Length] == StringExpressionSet.m_directorySeparator);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000A0BC0 File Offset: 0x0009EDC0
		[SecuritySafeCritical]
		protected void AddSingleExpressionNoDuplicates(string expression)
		{
			int i = 0;
			this.m_expressionsArray = null;
			this.m_expressions = null;
			if (this.m_list == null)
			{
				this.m_list = new ArrayList();
			}
			while (i < this.m_list.Count)
			{
				if (this.StringSubsetString((string)this.m_list[i], expression, this.m_ignoreCase))
				{
					this.m_list.RemoveAt(i);
				}
				else
				{
					if (this.StringSubsetString(expression, (string)this.m_list[i], this.m_ignoreCase))
					{
						return;
					}
					i++;
				}
			}
			this.m_list.Add(expression);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000A0C60 File Offset: 0x0009EE60
		[SecurityCritical]
		protected void Reduce()
		{
			this.CheckList();
			if (this.m_list == null)
			{
				return;
			}
			for (int i = 0; i < this.m_list.Count - 1; i++)
			{
				int j = i + 1;
				while (j < this.m_list.Count)
				{
					if (this.StringSubsetString((string)this.m_list[j], (string)this.m_list[i], this.m_ignoreCase))
					{
						this.m_list.RemoveAt(j);
					}
					else if (this.StringSubsetString((string)this.m_list[i], (string)this.m_list[j], this.m_ignoreCase))
					{
						this.m_list[i] = this.m_list[j];
						this.m_list.RemoveAt(j);
						j = i + 1;
					}
					else
					{
						j++;
					}
				}
			}
		}

		// Token: 0x06002B3D RID: 11069
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetLongPathName(string path, StringHandleOnStack retLongPath);

		// Token: 0x06002B3E RID: 11070 RVA: 0x000A0D50 File Offset: 0x0009EF50
		[SecurityCritical]
		internal static string CanonicalizePath(string path)
		{
			return StringExpressionSet.CanonicalizePath(path, true);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000A0D5C File Offset: 0x0009EF5C
		[SecurityCritical]
		internal static string CanonicalizePath(string path, bool needFullPath)
		{
			if (needFullPath)
			{
				string text = Path.GetFullPathInternal(path);
				if (path.EndsWith(StringExpressionSet.m_directorySeparator.ToString() + ".", StringComparison.Ordinal))
				{
					if (text.EndsWith(StringExpressionSet.m_directorySeparator))
					{
						text += ".";
					}
					else
					{
						text = text + StringExpressionSet.m_directorySeparator.ToString() + ".";
					}
				}
				path = text;
			}
			else if (path.IndexOf('~') != -1)
			{
				string text2 = null;
				StringExpressionSet.GetLongPathName(path, JitHelpers.GetStringHandleOnStack(ref text2));
				path = ((text2 != null) ? text2 : path);
			}
			if (path.IndexOf(':', 2) != -1)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
			}
			return path;
		}

		// Token: 0x0400110D RID: 4365
		[SecurityCritical]
		protected ArrayList m_list;

		// Token: 0x0400110E RID: 4366
		protected bool m_ignoreCase;

		// Token: 0x0400110F RID: 4367
		[SecurityCritical]
		protected string m_expressions;

		// Token: 0x04001110 RID: 4368
		[SecurityCritical]
		protected string[] m_expressionsArray;

		// Token: 0x04001111 RID: 4369
		protected bool m_throwOnRelative;

		// Token: 0x04001112 RID: 4370
		protected static readonly char[] m_separators = new char[]
		{
			';'
		};

		// Token: 0x04001113 RID: 4371
		protected static readonly char[] m_trimChars = new char[]
		{
			' '
		};

		// Token: 0x04001114 RID: 4372
		protected static readonly char m_directorySeparator = '\\';

		// Token: 0x04001115 RID: 4373
		protected static readonly char m_alternateDirectorySeparator = '/';
	}
}
