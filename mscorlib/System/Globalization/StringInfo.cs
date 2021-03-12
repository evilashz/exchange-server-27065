using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020003A4 RID: 932
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StringInfo
	{
		// Token: 0x06003047 RID: 12359 RVA: 0x000B9148 File Offset: 0x000B7348
		[__DynamicallyInvokable]
		public StringInfo() : this("")
		{
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000B9155 File Offset: 0x000B7355
		[__DynamicallyInvokable]
		public StringInfo(string value)
		{
			this.String = value;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000B9164 File Offset: 0x000B7364
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_str = string.Empty;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000B9171 File Offset: 0x000B7371
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_str.Length == 0)
			{
				this.m_indexes = null;
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000B9188 File Offset: 0x000B7388
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			StringInfo stringInfo = value as StringInfo;
			return stringInfo != null && this.m_str.Equals(stringInfo.m_str);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000B91B2 File Offset: 0x000B73B2
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_str.GetHashCode();
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000B91BF File Offset: 0x000B73BF
		private int[] Indexes
		{
			get
			{
				if (this.m_indexes == null && 0 < this.String.Length)
				{
					this.m_indexes = StringInfo.ParseCombiningCharacters(this.String);
				}
				return this.m_indexes;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000B91EE File Offset: 0x000B73EE
		// (set) Token: 0x0600304F RID: 12367 RVA: 0x000B91F6 File Offset: 0x000B73F6
		[__DynamicallyInvokable]
		public string String
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_str;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("String", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.m_str = value;
				this.m_indexes = null;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000B921E File Offset: 0x000B741E
		[__DynamicallyInvokable]
		public int LengthInTextElements
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Indexes == null)
				{
					return 0;
				}
				return this.Indexes.Length;
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000B9234 File Offset: 0x000B7434
		public string SubstringByTextElements(int startingTextElement)
		{
			if (this.Indexes != null)
			{
				return this.SubstringByTextElements(startingTextElement, this.Indexes.Length - startingTextElement);
			}
			if (startingTextElement < 0)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000B9288 File Offset: 0x000B7488
		public string SubstringByTextElements(int startingTextElement, int lengthInTextElements)
		{
			if (startingTextElement < 0)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (this.String.Length == 0 || startingTextElement >= this.Indexes.Length)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
			}
			if (lengthInTextElements < 0)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (startingTextElement > this.Indexes.Length - lengthInTextElements)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
			}
			int num = this.Indexes[startingTextElement];
			if (startingTextElement + lengthInTextElements == this.Indexes.Length)
			{
				return this.String.Substring(num);
			}
			return this.String.Substring(num, this.Indexes[lengthInTextElements + startingTextElement] - num);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000B9351 File Offset: 0x000B7551
		[__DynamicallyInvokable]
		public static string GetNextTextElement(string str)
		{
			return StringInfo.GetNextTextElement(str, 0);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000B935C File Offset: 0x000B755C
		internal static int GetCurrentTextElementLen(string str, int index, int len, ref UnicodeCategory ucCurrent, ref int currentCharCount)
		{
			if (index + currentCharCount == len)
			{
				return currentCharCount;
			}
			int num;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index + currentCharCount, out num);
			if (CharUnicodeInfo.IsCombiningCategory(unicodeCategory) && !CharUnicodeInfo.IsCombiningCategory(ucCurrent) && ucCurrent != UnicodeCategory.Format && ucCurrent != UnicodeCategory.Control && ucCurrent != UnicodeCategory.OtherNotAssigned && ucCurrent != UnicodeCategory.Surrogate)
			{
				int num2 = index;
				for (index += currentCharCount + num; index < len; index += num)
				{
					unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
					if (!CharUnicodeInfo.IsCombiningCategory(unicodeCategory))
					{
						ucCurrent = unicodeCategory;
						currentCharCount = num;
						break;
					}
				}
				return index - num2;
			}
			int result = currentCharCount;
			ucCurrent = unicodeCategory;
			currentCharCount = num;
			return result;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000B93F0 File Offset: 0x000B75F0
		[__DynamicallyInvokable]
		public static string GetNextTextElement(string str, int index)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (index >= 0 && index < length)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
				return str.Substring(index, StringInfo.GetCurrentTextElementLen(str, index, length, ref unicodeCategory, ref num));
			}
			if (index == length)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000B9456 File Offset: 0x000B7656
		[__DynamicallyInvokable]
		public static TextElementEnumerator GetTextElementEnumerator(string str)
		{
			return StringInfo.GetTextElementEnumerator(str, 0);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000B9460 File Offset: 0x000B7660
		[__DynamicallyInvokable]
		public static TextElementEnumerator GetTextElementEnumerator(string str, int index)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (index < 0 || index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return new TextElementEnumerator(str, index, length);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000B94A8 File Offset: 0x000B76A8
		[__DynamicallyInvokable]
		public static int[] ParseCombiningCharacters(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			int[] array = new int[length];
			if (length == 0)
			{
				return array;
			}
			int num = 0;
			int i = 0;
			int num2;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, 0, out num2);
			while (i < length)
			{
				array[num++] = i;
				i += StringInfo.GetCurrentTextElementLen(str, i, length, ref unicodeCategory, ref num2);
			}
			if (num < length)
			{
				int[] array2 = new int[num];
				Array.Copy(array, array2, num);
				return array2;
			}
			return array;
		}

		// Token: 0x04001463 RID: 5219
		[OptionalField(VersionAdded = 2)]
		private string m_str;

		// Token: 0x04001464 RID: 5220
		[NonSerialized]
		private int[] m_indexes;
	}
}
