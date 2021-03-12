using System;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000042 RID: 66
	internal sealed class RegexCharacterClass
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00007B8C File Offset: 0x00005D8C
		public RegexCharacterClass(char ch)
		{
			this.ch = ch;
			this.type = RegexCharacterClass.ValueType.Character;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007BA2 File Offset: 0x00005DA2
		public RegexCharacterClass(RegexCharacterClass.ValueType type)
		{
			this.type = type;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007BB1 File Offset: 0x00005DB1
		public RegexCharacterClass.ValueType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007BBC File Offset: 0x00005DBC
		public override int GetHashCode()
		{
			int result;
			switch (this.type)
			{
			case RegexCharacterClass.ValueType.BeginCharacterClass:
				result = 568260;
				break;
			case RegexCharacterClass.ValueType.EndCharacterClass:
				result = 568261;
				break;
			case RegexCharacterClass.ValueType.SpaceCharacterClass:
				result = 32;
				break;
			case RegexCharacterClass.ValueType.NonSpaceCharacterClass:
				result = 568257;
				break;
			case RegexCharacterClass.ValueType.NonDigitCharacterClass:
				result = 568258;
				break;
			case RegexCharacterClass.ValueType.DigitCharacterClass:
				result = 568256;
				break;
			case RegexCharacterClass.ValueType.WordCharacterClass:
				result = 568259;
				break;
			case RegexCharacterClass.ValueType.NonWordCharacterClass:
				result = 568262;
				break;
			default:
				result = (int)this.ch;
				break;
			}
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007C40 File Offset: 0x00005E40
		public override bool Equals(object obj)
		{
			RegexCharacterClass regexCharacterClass = obj as RegexCharacterClass;
			return regexCharacterClass != null && this.GetHashCode() == regexCharacterClass.GetHashCode();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007C68 File Offset: 0x00005E68
		public override string ToString()
		{
			string result;
			switch (this.type)
			{
			case RegexCharacterClass.ValueType.BeginCharacterClass:
				result = "^";
				break;
			case RegexCharacterClass.ValueType.EndCharacterClass:
				result = "$";
				break;
			case RegexCharacterClass.ValueType.SpaceCharacterClass:
				result = "\\s";
				break;
			case RegexCharacterClass.ValueType.NonSpaceCharacterClass:
				result = "\\S";
				break;
			case RegexCharacterClass.ValueType.NonDigitCharacterClass:
				result = "\\D";
				break;
			case RegexCharacterClass.ValueType.DigitCharacterClass:
				result = "\\d";
				break;
			case RegexCharacterClass.ValueType.WordCharacterClass:
				result = "\\w";
				break;
			case RegexCharacterClass.ValueType.NonWordCharacterClass:
				result = "\\W";
				break;
			default:
				result = char.ToString(this.ch);
				break;
			}
			return result;
		}

		// Token: 0x040000CA RID: 202
		private char ch;

		// Token: 0x040000CB RID: 203
		private RegexCharacterClass.ValueType type;

		// Token: 0x02000043 RID: 67
		internal enum ValueType
		{
			// Token: 0x040000CD RID: 205
			Character,
			// Token: 0x040000CE RID: 206
			BeginCharacterClass,
			// Token: 0x040000CF RID: 207
			EndCharacterClass,
			// Token: 0x040000D0 RID: 208
			SpaceCharacterClass,
			// Token: 0x040000D1 RID: 209
			NonSpaceCharacterClass,
			// Token: 0x040000D2 RID: 210
			NonDigitCharacterClass,
			// Token: 0x040000D3 RID: 211
			DigitCharacterClass,
			// Token: 0x040000D4 RID: 212
			WordCharacterClass,
			// Token: 0x040000D5 RID: 213
			NonWordCharacterClass
		}
	}
}
