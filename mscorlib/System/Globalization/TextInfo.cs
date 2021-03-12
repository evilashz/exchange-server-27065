using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003A7 RID: 935
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000B99D5 File Offset: 0x000B7BD5
		internal static TextInfo Invariant
		{
			get
			{
				if (TextInfo.s_Invariant == null)
				{
					TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
				}
				return TextInfo.s_Invariant;
			}
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000B99F8 File Offset: 0x000B7BF8
		internal TextInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_cultureName = this.m_cultureData.CultureName;
			this.m_textInfoName = this.m_cultureData.STEXTINFO;
			IntPtr handleOrigin;
			this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out handleOrigin);
			this.m_handleOrigin = handleOrigin;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000B9A4E File Offset: 0x000B7C4E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_cultureData = null;
			this.m_cultureName = null;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000B9A60 File Offset: 0x000B7C60
		private void OnDeserialized()
		{
			if (this.m_cultureData == null)
			{
				if (this.m_cultureName == null)
				{
					if (this.customCultureName != null)
					{
						this.m_cultureName = this.customCultureName;
					}
					else if (this.m_win32LangID == 0)
					{
						this.m_cultureName = "ar-SA";
					}
					else
					{
						this.m_cultureName = CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName;
					}
				}
				this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
				this.m_textInfoName = this.m_cultureData.STEXTINFO;
				IntPtr handleOrigin;
				this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out handleOrigin);
				this.m_handleOrigin = handleOrigin;
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000B9B07 File Offset: 0x000B7D07
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000B9B0F File Offset: 0x000B7D0F
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_useUserOverride = false;
			this.customCultureName = this.m_cultureName;
			this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000B9B3A File Offset: 0x000B7D3A
		internal static int GetHashCodeOrdinalIgnoreCase(string s)
		{
			return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000B9B45 File Offset: 0x000B7D45
		internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
		{
			return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000B9B54 File Offset: 0x000B7D54
		[SecuritySafeCritical]
		internal static bool TryFastFindStringOrdinalIgnoreCase(int searchFlags, string source, int startIndex, string value, int count, ref int foundIndex)
		{
			return TextInfo.InternalTryFindStringOrdinalIgnoreCase(searchFlags, source, count, startIndex, value, value.Length, ref foundIndex);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000B9B69 File Offset: 0x000B7D69
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCase(string str1, string str2)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(str1, 0, str2, 0, str1.Length, str2.Length);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000B9B80 File Offset: 0x000B7D80
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000B9B90 File Offset: 0x000B7D90
		internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (source.Length == 0 && value.Length == 0)
			{
				return 0;
			}
			int result = -1;
			if (TextInfo.TryFastFindStringOrdinalIgnoreCase(4194304, source, startIndex, value, count, ref result))
			{
				return result;
			}
			int num = startIndex + count;
			int num2 = num - value.Length;
			while (startIndex <= num2)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex++;
			}
			return -1;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000B9BF8 File Offset: 0x000B7DF8
		internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (value.Length == 0)
			{
				return startIndex;
			}
			int result = -1;
			if (TextInfo.TryFastFindStringOrdinalIgnoreCase(8388608, source, startIndex, value, count, ref result))
			{
				return result;
			}
			int num = startIndex - count + 1;
			if (value.Length > 0)
			{
				startIndex -= value.Length - 1;
			}
			while (startIndex >= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex--;
			}
			return -1;
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000B9C65 File Offset: 0x000B7E65
		public virtual int ANSICodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTANSICODEPAGE;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000B9C72 File Offset: 0x000B7E72
		public virtual int OEMCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTOEMCODEPAGE;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000B9C7F File Offset: 0x000B7E7F
		public virtual int MacCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTMACCODEPAGE;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x000B9C8C File Offset: 0x000B7E8C
		public virtual int EBCDICCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000B9C99 File Offset: 0x000B7E99
		[ComVisible(false)]
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000B9CAB File Offset: 0x000B7EAB
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string CultureName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_textInfoName;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000B9CB3 File Offset: 0x000B7EB3
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000B9CBC File Offset: 0x000B7EBC
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000B9CE0 File Offset: 0x000B7EE0
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000B9D19 File Offset: 0x000B7F19
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000B9D33 File Offset: 0x000B7F33
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000B9D3C File Offset: 0x000B7F3C
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000B9D5D File Offset: 0x000B7F5D
		[__DynamicallyInvokable]
		public virtual string ListSeparator
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = this.m_cultureData.SLIST;
				}
				return this.m_listSeparator;
			}
			[ComVisible(false)]
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.m_listSeparator = value;
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000B9D84 File Offset: 0x000B7F84
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual char ToLower(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, false);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000B9DB6 File Offset: 0x000B7FB6
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, false);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000B9DDF File Offset: 0x000B7FDF
		private static char ToLowerAsciiInvariant(char c)
		{
			if ('A' <= c && c <= 'Z')
			{
				c |= ' ';
			}
			return c;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000B9DF3 File Offset: 0x000B7FF3
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual char ToUpper(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, true);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000B9E25 File Offset: 0x000B8025
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, true);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000B9E4E File Offset: 0x000B804E
		private static char ToUpperAsciiInvariant(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c = (char)((int)c & -33);
			}
			return c;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000B9E62 File Offset: 0x000B8062
		private static bool IsAscii(char c)
		{
			return c < '\u0080';
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000B9E6C File Offset: 0x000B806C
		private bool IsAsciiCasingSameAsInvariant
		{
			get
			{
				if (this.m_IsAsciiCasingSameAsInvariant == TextInfo.Tristate.NotInitialized)
				{
					this.m_IsAsciiCasingSameAsInvariant = ((CultureInfo.GetCultureInfo(this.m_textInfoName).CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0) ? TextInfo.Tristate.True : TextInfo.Tristate.False);
				}
				return this.m_IsAsciiCasingSameAsInvariant == TextInfo.Tristate.True;
			}
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000B9EAC File Offset: 0x000B80AC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000B9ED6 File Offset: 0x000B80D6
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000B9EE3 File Offset: 0x000B80E3
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return "TextInfo - " + this.m_cultureData.CultureName;
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000B9EFC File Offset: 0x000B80FC
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
				if (char.CheckLetter(unicodeCategory))
				{
					i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					int num2 = i;
					bool flag = unicodeCategory == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategory))
						{
							if (unicodeCategory == UnicodeCategory.LowercaseLetter)
							{
								flag = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategory))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000BA049 File Offset: 0x000B8249
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000BA088 File Offset: 0x000B8288
		private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000BA162 File Offset: 0x000B8362
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & 1 << (int)category) != 0;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000BA173 File Offset: 0x000B8373
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000BA18A File Offset: 0x000B838A
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool IsRightToLeft
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.IsRightToLeft;
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000BA197 File Offset: 0x000B8397
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000BA19F File Offset: 0x000B839F
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str)
		{
			return this.GetCaseInsensitiveHashCode(str, false, 0L);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000BA1AB File Offset: 0x000B83AB
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return TextInfo.InternalGetCaseInsHash(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x060030AC RID: 12460
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern char InternalChangeCaseChar(IntPtr handle, IntPtr handleOrigin, string localeName, char ch, bool isToUpper);

		// Token: 0x060030AD RID: 12461
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalChangeCaseString(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool isToUpper);

		// Token: 0x060030AE RID: 12462
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetCaseInsHash(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool forceRandomizedHashing, long additionalEntropy);

		// Token: 0x060030AF RID: 12463
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalCompareStringOrdinalIgnoreCase(string string1, int index1, string string2, int index2, int length1, int length2);

		// Token: 0x060030B0 RID: 12464
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalTryFindStringOrdinalIgnoreCase(int searchFlags, string source, int sourceCount, int startIndex, string target, int targetCount, ref int foundIndex);

		// Token: 0x04001473 RID: 5235
		[OptionalField(VersionAdded = 2)]
		private string m_listSeparator;

		// Token: 0x04001474 RID: 5236
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001475 RID: 5237
		[OptionalField(VersionAdded = 3)]
		private string m_cultureName;

		// Token: 0x04001476 RID: 5238
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x04001477 RID: 5239
		[NonSerialized]
		private string m_textInfoName;

		// Token: 0x04001478 RID: 5240
		[NonSerialized]
		private IntPtr m_dataHandle;

		// Token: 0x04001479 RID: 5241
		[NonSerialized]
		private IntPtr m_handleOrigin;

		// Token: 0x0400147A RID: 5242
		[NonSerialized]
		private TextInfo.Tristate m_IsAsciiCasingSameAsInvariant;

		// Token: 0x0400147B RID: 5243
		internal static volatile TextInfo s_Invariant;

		// Token: 0x0400147C RID: 5244
		[OptionalField(VersionAdded = 2)]
		private string customCultureName;

		// Token: 0x0400147D RID: 5245
		[OptionalField(VersionAdded = 1)]
		internal int m_nDataItem;

		// Token: 0x0400147E RID: 5246
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x0400147F RID: 5247
		[OptionalField(VersionAdded = 1)]
		internal int m_win32LangID;

		// Token: 0x04001480 RID: 5248
		private const int wordSeparatorMask = 536672256;

		// Token: 0x02000B3A RID: 2874
		private enum Tristate : byte
		{
			// Token: 0x04003388 RID: 13192
			NotInitialized,
			// Token: 0x04003389 RID: 13193
			True,
			// Token: 0x0400338A RID: 13194
			False
		}
	}
}
