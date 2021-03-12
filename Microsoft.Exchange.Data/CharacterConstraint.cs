using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	internal class CharacterConstraint : CharacterRegexConstraint
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public CharacterConstraint(char[] characters, bool valid) : base(CharacterConstraint.ConstructPattern(characters, valid))
		{
			StringBuilder stringBuilder = new StringBuilder(characters.Length * 5);
			for (int i = 0; i < characters.Length - 1; i++)
			{
				this.AppendVisibleString(characters[i], stringBuilder);
				stringBuilder.Append(", ");
			}
			this.AppendVisibleString(characters[characters.Length - 1], stringBuilder);
			this.characterString = stringBuilder.ToString();
			this.Characters = (char[])characters.Clone();
			this.showAsValid = valid;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000CF09 File Offset: 0x0000B109
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000CF11 File Offset: 0x0000B111
		public char[] Characters { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000CF1A File Offset: 0x0000B11A
		public bool ShowAsValid
		{
			get
			{
				return this.showAsValid;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000CF24 File Offset: 0x0000B124
		internal static string ConstructPattern(char[] characters, bool valid)
		{
			if (characters == null)
			{
				throw new ArgumentNullException("characters");
			}
			if (characters.Length == 0)
			{
				throw new ArgumentOutOfRangeException("characters", "characters must contain at least one character");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in characters)
			{
				if (c == ']')
				{
					flag = true;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (flag)
			{
				string value = stringBuilder.ToString();
				stringBuilder = new StringBuilder();
				stringBuilder.Append(']');
				stringBuilder.Append(value);
			}
			string text = Regex.Escape(stringBuilder.ToString());
			stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			if (!valid)
			{
				stringBuilder.Append('^');
			}
			if (text.Length > 0 && text[0] == ']')
			{
				stringBuilder.Append('\\');
			}
			stringBuilder.Append(text);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D007 File Offset: 0x0000B207
		private void AppendVisibleString(char c, StringBuilder sb)
		{
			if (char.IsControl(c))
			{
				sb.AppendFormat("'0x{0:X2}'", (int)c);
				return;
			}
			sb.AppendFormat("'{0}'", c);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D036 File Offset: 0x0000B236
		protected override LocalizedString CustomErrorMessage(string value, PropertyDefinition propertyDefinition)
		{
			if (this.ShowAsValid)
			{
				return DataStrings.ConstraintViolationStringContainsInvalidCharacters2(this.characterString, value);
			}
			return DataStrings.ConstraintViolationStringContainsInvalidCharacters(this.characterString, value);
		}

		// Token: 0x0400011E RID: 286
		private const char Escape = '\\';

		// Token: 0x0400011F RID: 287
		private const char RightBracket = ']';

		// Token: 0x04000120 RID: 288
		private string characterString;

		// Token: 0x04000121 RID: 289
		private bool showAsValid;
	}
}
